using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Auth.Commands.EmailConfirmation;
using CleanArchitecture.Application.Features.Auth.Commands.Registration;
using CleanArchitecture.Application.Features.Auth.Rules;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.Mail;
using CleanArchitecture.Application.Interfaces.Storage;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class RegistrationCommandHandler : BaseHandler, IRequestHandler<RegistrationCommandRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMailService _mailService;
    private readonly LinkGeneratorHelper _linkGeneratorHelper;
    private readonly EmailConfirmationCommandRequest _emailConfirmationCommandRequest;
    private readonly ILocalStorage _localStorage;
    private readonly IUrlHelper urlHelper;

    public UserManager<User> UserManager { get; }
    public RoleManager<Role> RoleManager { get; }
    public AuthRules AuthRules { get; }

    public RegistrationCommandHandler(IUnitOfWork unitOfWork,
                                      IMapper mapper,
                                      IHttpContextAccessor httpContextAccessor,
                                      UserManager<User> userManager,
                                      RoleManager<Role> roleManager,
                                      AuthRules authRules,
                                      IMailService mailService,
                                      LinkGeneratorHelper linkGeneratorHelper,
                                      EmailConfirmationCommandRequest emailConfirmationCommandRequest,
                                      ILocalStorage localStorage)
        : base(unitOfWork, mapper, httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        UserManager = userManager;
        RoleManager = roleManager;
        AuthRules = authRules;
        _mailService = mailService;
        _linkGeneratorHelper = linkGeneratorHelper;
        _emailConfirmationCommandRequest = emailConfirmationCommandRequest;
        _localStorage = localStorage;
    }

    public async Task<Unit> Handle(RegistrationCommandRequest request, CancellationToken cancellationToken)
    {
        await AuthRules.UserShouldnotBeExistsAsync(await UserManager.FindByEmailAsync(request.Email));
        User user = Mapper.Map<User, RegistrationCommandRequest>(request);
        user.UserName = request.Email;
        user.SecurityStamp = Guid.NewGuid().ToString();
        IdentityResult result = await UserManager.CreateAsync(user, request.Password);


        /*bool isSent = await EmailConfirmationDTOAsync(user);
        if (!isSent)
        {
            await UserManager.DeleteAsync(user);
            await UserManager.UpdateAsync(user);
            await AuthRules.NoMistakeShouldHappenWhileEmailConfirmationAsync(isSent);
        }*/
        if (result.Succeeded)
        {
            if (await RoleManager.RoleExistsAsync("USER"))
            {
                await RoleManager.CreateAsync(new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "user",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                });
            }
            await UserManager.AddToRoleAsync(user, "USER");
            if (request.Image is not null)
            {
                var photo = await _localStorage.UploadAsync(1, "Users", request.Image);
                user.Picture = photo.Path;
                await UserManager.UpdateAsync(user);
            }
        }
        string emailBody = GenerateEmailBody(user.UserName);
        await _mailService.SendMailAsync(user.Email,"Success Registering",emailBody);

        return Unit.Value;
    }
    static string GenerateEmailBody(string userName)
    {
        return $@"
        <html>
        <body>
            <p>Dear {userName},</p>
            <p>You have registered on Our Website : CleanArchitecture</p>
            <p>Thank you!!</p>
        </body>
        </html>";
    }


    private string MessageBody(string name, string confirmationLink)
    {
        string emailBody = $@"
<html>
<body style='font-family: Arial, sans-serif; color: #333;'>
    <div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
        <div style='text-align: center;'>
            <img src='https://yourcompanylogo.com/logo.png' alt='Your Company' style='width: 150px; margin-bottom: 20px;'/>
        </div>
        <h2 style='color: #007BFF;'>Hello {name},</h2>
        <p>Thank you for registering with us! Please confirm your email address by clicking the button below:</p>
        <div style='text-align: center; margin: 30px 0;'>
            <a href='{confirmationLink}' style='background-color: #007BFF; color: #fff; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Confirm Email</a>
        </div>
        <p>If the button above does not work, copy and paste the following link into your browser:</p>
        <p><a href='{confirmationLink}' style='color: #007BFF;'>{confirmationLink}</a></p>
        <p>If you did not create an account, no further action is required.</p>
        <p>Best regards,<br/>The Your Company Team</p>
        <div style='text-align: center; margin-top: 20px;'>
            <a href='https://www.yourcompany.com' style='color: #007BFF;'>Visit our website</a> | 
            <a href='https://www.yourcompany.com/contact' style='color: #007BFF;'>Contact Support</a>
        </div>
    </div>
</body>
</html>";
        return emailBody;
    }
}

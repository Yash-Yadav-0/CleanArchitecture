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
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class RegistrationCommandHandler : BaseHandler, IRequestHandler<RegistrationCommandRequest, Unit>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IMailService mailService;
    private readonly UrlFactoryHelper urlFactoryHelper;
    private readonly EmailConfirmationCommandRequest emailConfirmationCommandRequest;
    private readonly ILocalStorage localStorage;

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
                                      UrlFactoryHelper urlFactoryHelper,
                                      EmailConfirmationCommandRequest emailConfirmationCommandRequest,
                                      ILocalStorage localStorage)
        : base(unitOfWork, mapper, httpContextAccessor)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.httpContextAccessor = httpContextAccessor;
        UserManager = userManager;
        RoleManager = roleManager;
        AuthRules = authRules;
        this.mailService = mailService;
        this.urlFactoryHelper = urlFactoryHelper;
        this.emailConfirmationCommandRequest = emailConfirmationCommandRequest;
        this.localStorage = localStorage;
    }

    public async Task<Unit> Handle(RegistrationCommandRequest request, CancellationToken cancellationToken)
    {
        await AuthRules.UserShouldnotBeExistsAsync(await UserManager.FindByEmailAsync(request.Email));
        User user = Mapper.Map<User, RegistrationCommandRequest>(request);
        user.UserName = request.Email;
        user.SecurityStamp = Guid.NewGuid().ToString();
        IdentityResult result = await UserManager.CreateAsync(user, request.Password);
        bool isSent = await EmailConfirmationDTOAsync(user);
        if (!isSent)
        {
            await UserManager.DeleteAsync(user);
            await UserManager.UpdateAsync(user);
            await AuthRules.NoMistakeShouldHappenWhileEmailConfirmationAsync(isSent);
        }
        if (result.Succeeded)
        {
            if (!await RoleManager.RoleExistsAsync("user"))
            {
                await RoleManager.CreateAsync(new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "user",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                });
            }
            if (request.Image is not null)
            {
                var photo = await localStorage.UploadAsync(1, "Users", request.Image);
                user.Picture = photo.Path;
                await UserManager.UpdateAsync(user);
            }
        }

        return Unit.Value;
    }

    private async Task<bool> EmailConfirmationDTOAsync(User user)
    {
        var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = EncoderHelper.UrlEncoder(token);

        emailConfirmationCommandRequest.Email = user.Email;
        emailConfirmationCommandRequest.Token = encodedToken;

        var request = httpContextAccessor.HttpContext.Request;
        IUrlHelper? urlHelper = urlFactoryHelper.CreateUrlHelper();

        // For debugging: Log or inspect the URL components
        Console.WriteLine($"Scheme: {request.Scheme}, Host: {request.Host}");

        // Simplified URL generation for testing
        var confirmationLink = urlHelper.Action(
            "EmailConfirmation",
            "Auth",
            new { email = emailConfirmationCommandRequest.Email, token = emailConfirmationCommandRequest.Token },
            request.Scheme,
            request.Host.ToString()
        );
        Console.WriteLine($"Confirmation Link: {confirmationLink}");

        if (string.IsNullOrEmpty(confirmationLink))
        {
            return false;
        }

        // Html body
        string emailBody = Message(user.FullName, confirmationLink);

        await mailService.SendMessageAsync(user.Email, "Email Activation", emailBody);
        return true;
    }

    private string Message(string name, string confirmationLink)
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


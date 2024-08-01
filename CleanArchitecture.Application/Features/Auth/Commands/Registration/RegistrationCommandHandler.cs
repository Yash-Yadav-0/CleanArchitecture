﻿using CleanArchitecture.Application.Bases;
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
using Microsoft.IdentityModel.Logging;

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
        LogHelper.LogInformation("Handling Registration for Email: {Email}", request.Email);
        await AuthRules.UserShouldnotBeExistsAsync(await UserManager.FindByEmailAsync(request.Email));
        User user = Mapper.Map<User, RegistrationCommandRequest>(request);
        user.UserName = request.Email;
        user.SecurityStamp = Guid.NewGuid().ToString();
        IdentityResult result = await UserManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            LoggerHelper.LogError("User creation failed for Email: {Email}", new Exception("User creation failed for Email:{Email}"), request.Email);
            throw new InvalidOperationException("User creation failed");
        }

        LoggerHelper.LogInformation("User created successfully for Email: {Email}", request.Email);

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

        if (request.Image != null)
        {
            var photo = await _localStorage.UploadAsync(1, "Users", request.Image);
            user.Picture = photo.Path;
            await UserManager.UpdateAsync(user);
            LoggerHelper.LogInformation("User profile picture added for Email: {Email}", request.Email);
        }

        string emailBody = GenerateEmailBody(user.UserName);
        await _mailService.SendMailAsync(user.Email, "Success Registering", emailBody);
        LoggerHelper.LogInformation("Email sent successfully to Email: {Email}", user.Email);

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
}

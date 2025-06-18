using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using ARM.Common.Exceptions;
using ARM.Common.Extensions;
using ARM.Common.Templates;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Dtos.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ARM.Application.Services.Auth;

public class EmailService(IHttpContextAccessor httpContextAccessor,
    ICustomerRepository cutsomerRepository, IOtpService otpService,
    IOptions<SmtpSettingsDto> smtpOptions) : IEmailService
{
    private readonly SmtpSettingsDto _smtpSettings = smtpOptions.Value;
    public async Task SendOtpAsync(string email, string otpCode)
    {
        var message = new MailMessage(_smtpSettings.From, email)
        {
            Subject = "OTP",
            Body = otpCode,
            IsBodyHtml = false
        };

        using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
            EnableSsl = true
        };

        await client.SendMailAsync(message);
    }

    public async Task SendMessageAsync(string email, string name, string surname)
    {
        var newMessage = new MailMessage(_smtpSettings.From, email)
        {
            Subject = "Login",
            Body = $"Sucess!\nHello, {name} {surname}!",
            IsBodyHtml = false
        };
        
        using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
            EnableSsl = true
        };

        await client.SendMailAsync(newMessage);
    }

    public async Task SendVerificationLinkAsync()
    {
        var httpContext = httpContextAccessor.HttpContext;

        var customerId = httpContext.GetCustomerId();
        var email = httpContext.GetEmail();

        var user = await cutsomerRepository.GetByIdAsync(customerId);
    
        if (user.EmailVerified)
            throw new AppException(ExceptionType.EmailAlreadyConfirmed, "EmailAlreadyConfirmed");
        
        var lang = httpContext.Request.Headers["Accept-Language"]
            .ToString()
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault()
            ?.Trim()
            .ToLower();
        
        var request = httpContext.Request;
        var baseUrl = $"{request.Scheme}://{request.Host.Value}";

        var token = await otpService.GenerateAndSaveEmailVerificationTokenAsync(customerId);
        var verifyUrl = $"{baseUrl}/api/b/ve?t={WebUtility.UrlEncode(token)}";

        var htmlBody = ConfirmEmailTemplate.GetTemplate(verifyUrl, lang);

        var subject = lang switch
        {
            "ru" => "Подтверждение почты",
            "az" => "Email təsdiqi",
            _ => "Confirm Email"
        };

        var message = new MailMessage(_smtpSettings.From, email)
        {
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true
        };

        using var smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
            EnableSsl = true
        };

        await smtpClient.SendMailAsync(message);
    }
}
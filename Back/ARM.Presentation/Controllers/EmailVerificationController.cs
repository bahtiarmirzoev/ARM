using static ARM.Common.Templates.EmailVerificationPageTemplate;
using ARM.RequestPipeline.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ARM.Presentation.Controllers;

[ApiController]
[Route("api/b")]
public class EmailVerificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmailVerificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ve")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string t)
    {
        string lang = string.Empty;
        if (string.IsNullOrWhiteSpace(lang))
        {
            lang = HttpContext.Request.Headers["Accept-Language"]
                .ToString()
                .Split(',')
                .FirstOrDefault()?
                .Trim()
                .ToLower() ?? "en";
        }

        var supportedLangs = new[] { "en", "ru", "az" };
        if (!supportedLangs.Contains(lang))
            lang = "en";
        
        if (string.IsNullOrWhiteSpace(t))
            return Content(GetErrorTemplate("Token is missing", lang), "text/html");

        try
        {
            var result = await _mediator.Send(new VerifyEmailCommand(t));

            if (result)
                return Content(GetSuccessTemplate(lang), "text/html");

            return Content(GetErrorTemplate("Couldn't confirm email", lang), "text/html");
        }
        catch (Exception ex)
        {
            return Content(GetErrorTemplate(ex.Message, lang), "text/html");
        }
    }
}
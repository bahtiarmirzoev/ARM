using ARM.Core.Abstractions.Services.Auth;
using ARM.RequestPipeline.Commands.Customer;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Customer.Commands;

public class SendVerificationEmailCommandHandler(IEmailService emailService)
    : IRequestHandler<SendVerificationEmailCommand, bool>
{
    public async Task<bool> Handle(SendVerificationEmailCommand request, CancellationToken cancellationToken)
    {
        //var userId = request.User.GetUserId();
        //var userEmail = request.User.GetUserEmail();
        await emailService.SendVerificationLinkAsync();
        return true;
    }
}
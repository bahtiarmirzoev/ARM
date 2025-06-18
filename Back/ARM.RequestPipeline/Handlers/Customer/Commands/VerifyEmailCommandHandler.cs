using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.Customer;
using MediatR;

namespace ARM.RequestPipeline.Handlers.Customer.Commands;

public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, bool>
{
    private readonly IRedisCacheService _redisCache;
    private readonly ICustomerService _customerService;

    public VerifyEmailCommandHandler(IRedisCacheService redisCache, ICustomerService customerService)
    {
        _redisCache = redisCache;
        _customerService = customerService;
    }

    public async Task<bool> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var customerIdString = await _redisCache.GetAsync<string>($"emailVerifyToken:{request.Token}");
        if (string.IsNullOrEmpty(customerIdString))
            throw new AppException(ExceptionType.InvalidRequest, "InvalidOrExpiredToken");
        
        await _customerService.ConfirmEmailAsync();
        
        await _redisCache.KeyDeleteAsync($"emailVerifyToken:{request.Token}");

        return true;
    }
}
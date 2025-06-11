using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Services.Main;
using ARM.RequestPipeline.Commands.User;
using MediatR;

namespace ARM.RequestPipeline.Handlers.User.Commands;

public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, bool>
{
    private readonly IRedisCacheService _redisCache;
    private readonly IUserService _userService;

    public VerifyEmailCommandHandler(IRedisCacheService redisCache, IUserService userService)
    {
        _redisCache = redisCache;
        _userService = userService;
    }

    public async Task<bool> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var userIdString = await _redisCache.GetAsync<string>($"emailVerifyToken:{request.Token}");
        if (string.IsNullOrEmpty(userIdString))
            throw new AppException(ExceptionType.InvalidRequest, "InvalidOrExpiredToken");
        
        await _userService.ConfirmEmailAsync();
        
        await _redisCache.KeyDeleteAsync($"emailVerifyToken:{request.Token}");

        return true;
    }
}
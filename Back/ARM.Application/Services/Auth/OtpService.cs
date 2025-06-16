using ARM.Common.Exceptions;
using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Create;
using NanoidDotNet;

namespace ARM.Application.Services.Auth;

public class OtpService(IRedisCacheService redisCache) : IOtpService
{
    private readonly TimeSpan _otpLifetime = TimeSpan.FromMinutes(5);
    public async Task<string> GenerateAndSaveOtpAsync(string si)
    {
        var otpTimestampKey = $"otpTimestamp:{si}";

        var otp = new Random().Next(000001, 999999).ToString();
        await redisCache.SetAsync($"otp:{si}", otp, _otpLifetime);

        var nowMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        await redisCache.SetAsync(otpTimestampKey, nowMs, _otpLifetime);
        return otp;
    }

    public async Task<string> GenerateAndSaveEmailVerificationTokenAsync(string userId)
    {
        var token = Nanoid.Generate(size: 256);
        await redisCache.SetAsync($"emailVerifyToken:{token}", userId, TimeSpan.FromMinutes(5));
        return token;
    }

    public async Task<bool> VerifyOtpAsync(string si, string otp)
    {
        var storedOtp = await redisCache.GetAsync<string>($"otp:{si}");
        return storedOtp == otp;
    }
    public async Task SavePendingUserAsync(string si, CreateUserDto userDto)
        => await redisCache.SetAsync($"pendingUser:{si}", userDto, TimeSpan.FromMinutes(5));

    public async Task<CreateUserDto?> GetPendingUserAsync(string si)
        => await redisCache.GetAsync<CreateUserDto>($"pendingUser:{si}");

    public async Task ClearOtpAndPendingUserAsync(string si)
    {
        await redisCache.KeyDeleteAsync($"otp:{si}");
        await redisCache.KeyDeleteAsync($"pendingUser:{si}");
    }

    public async Task SavePendingCustomerAsync(string si, CreateCustomerDto customerDto)
        => await redisCache.SetAsync($"pendingCustomer:{si}", customerDto, TimeSpan.FromMinutes(5));

    public async Task<CreateCustomerDto?> GetPendingCustomerAsync(string si)
        => await redisCache.GetAsync<CreateCustomerDto>($"pendingCustomer:{si}");

    public async Task ClearOtpAndPendingCustomerAsync(string si)
    {
        await redisCache.KeyDeleteAsync($"otp:{si}");
        await redisCache.KeyDeleteAsync($"pendingCustomer:{si}");
    }
}
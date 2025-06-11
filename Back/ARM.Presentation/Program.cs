using System.Globalization;
using System.Text;
using ARM.Application.Services.Main;
using ARM.Application.Services.Auth;
using ARM.Application.Validators.Create;
using ARM.Core.Abstractions.Repositories.Auth;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Abstractions.Services.Auth;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Abstractions.UOW;
using ARM.Infrastructure.Repositories.Main;
using ARM.Core.Dtos.Auth;
using ARM.Infrastructure.Context;
using ARM.Infrastructure.Repositories.Auth;
using ARM.Infrastructure.UOW;
using ARM.Presentation.Middlewares;
using ARM.RequestPipeline.Commands.User;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("atk"))
                context.Token = context.Request.Cookies["atk"];
           
            return Task.CompletedTask;
        }
    };
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration.GetSection("JWT:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("JWT:Audience").Value,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Secret").Value))
    };
});



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>());

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "csrf";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("az"), new CultureInfo("ru"), new CultureInfo("en") };
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpContextAccessor();

builder.Services.AddDataProtection();

builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();

builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IRepairOrderService, RepairOrderService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IServiceRequestService, ServiceRequestService>();
builder.Services.AddScoped<IWorkingHourService, WorkingHourService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IBlackListedService, BlackListedService>();
builder.Services.AddScoped<IUserActiveSessionsService, UserActiveSessionsService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IUserActiveSessionsRepository, UserActiveSessionsRepository>();
builder.Services.AddScoped<IBlackListedRepository, BlackListedRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IRepairLogRepository, RepairLogRepository>();
builder.Services.AddScoped<IRepairOrderRepository, RepairOrderRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
builder.Services.AddScoped<IWorkingHourRepository, WorkingHourRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:3001")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("Redis:Configuration").Value;
    options.InstanceName = "ARMAPP"; 
});

var redisConnectionString = builder.Configuration["Redis:Configuration"];


builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(redisConnectionString!));

builder.Services.AddSingleton<ISubscriber>(sp =>
    sp.GetRequiredService<IConnectionMultiplexer>().GetSubscriber());

builder.Services.Configure<SmtpSettingsDto>(builder.Configuration.GetSection("SS"));

builder.Services.AddDbContext<ARMContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ARM")));

var app = builder.Build();

app.UseRequestLocalization();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<UnifiedResponseMiddleware>();
app.UseMiddleware<SecurityHeadersMiddleware>();
app.UseMiddleware<CsrfMiddleware>();

app.MapControllers();

app.Run();

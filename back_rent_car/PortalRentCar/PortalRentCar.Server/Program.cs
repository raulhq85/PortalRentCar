using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using PortalRentCar.DataAcces;
using PortalRentCar.Repositories.Interfaces;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Services.Profiles;
using PortalRentCar.Shared.Configuracion;
using Scrutor;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
const string corConfiguration = "Blazor";
builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy(corConfiguration, config =>
    {
        config.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<PortalRentCarDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortalRentCar"));

    options.ConfigureWarnings(warnings =>
    {
        warnings.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning);
    });
});

builder.Services.AddDbContext<RentCarSecurityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RentCarSecurity")));


builder.Services.AddIdentity<RentCarIdentityUser, IdentityRole>(policies =>
{
    policies.Password.RequireDigit = false;
    policies.Password.RequireLowercase = true;
    policies.Password.RequireUppercase = true;
    policies.Password.RequireNonAlphanumeric = false;
    policies.Password.RequiredLength = 8;

    policies.User.RequireUniqueEmail = true;

    policies.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    policies.Lockout.MaxFailedAccessAttempts = 3;
    policies.Lockout.AllowedForNewUsers = true;
})
    .AddEntityFrameworkStores<RentCarSecurityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Scan(selector => selector
    .FromAssemblies(typeof(ITipoVehiculoRepository).Assembly,
        typeof(ITipoVehiculoService).Assembly)
    .AddClasses(false)
    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
    .AsMatchingInterface()
    .WithTransientLifetime());

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<TipoVehiculoProfile>();
    config.AddProfile<MarcaProfile>();
    config.AddProfile<VehiculoProfile>();
    config.AddProfile<ClienteProfile>();
   config.AddProfile<AlquilerProfile>();
});

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var secretKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ??
                                           throw new InvalidOperationException("No se configuro el SecretKey"));

    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseCors(corConfiguration);

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PortalRentCarDbContext>();

    dbContext.Database.Migrate();

    var securityDbContext = scope.ServiceProvider.GetRequiredService<RentCarSecurityDbContext>();
    securityDbContext.Database.Migrate();

    await UserDataSeeder.Seed(scope.ServiceProvider);
}

app.Run();

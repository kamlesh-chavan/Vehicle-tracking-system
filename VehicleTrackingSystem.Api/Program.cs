using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using VehicleTrackingSystem.Bal;
using VehicleTrackingSystem.Bal.Services.AuthService;
using VehicleTrackingSystem.Bal.Services.DeviceService;
using VehicleTrackingSystem.Bal.Services.LocationService;
using VehicleTrackingSystem.Bal.Services.VehicleDeviceService;
using VehicleTrackingSystem.Bal.Services.VehicleService;
using VehicleTrackingSystem.Bal.TokenProvider;
using VehicleTrackingSystem.Core.Http;
using VehicleTrackingSystem.Core.Model;
using VehicleTrackingSystem.Dal.Context;
using VehicleTrackingSystem.Dal.Repos.Locations;
using VehicleTrackingSystem.Dal.Repos.RolesRepo;
using VehicleTrackingSystem.Dal.Repos.Users;
using VehicleTrackingSystem.Dal.Repos.VehicleDevice;

var builder = WebApplication.CreateBuilder(args);

//use extension - Read data from Azure key-vault Aure using Configuration and other extension
//builder.WebHost.ConfigureApp();

// Add services to the container.

//Add DB context
builder.Services.AddDbContext<PostgresDbContext>(options =>
                  options.UseNpgsql(builder.Configuration["GlobalConstant:ConnectionString"])
                         .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddMemoryCache();
//

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AppSettings:Secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

// Add all dependencies here
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddTransient<IVehicleService, VehicleService>();
builder.Services.AddTransient<IVehicleLocationMapperRepository, VehicleLocationMapperRepository>();
builder.Services.AddTransient<IVehicleDeviceRepository, VehicleDeviceRepository>();
builder.Services.AddTransient<IVehicleRepository, VehicleRepository>();
builder.Services.AddTransient<IDeviceRepository, DeviceRepository>();
builder.Services.AddTransient<IAuthenticateService, AuthenticateService>();
builder.Services.AddTransient<IRolesRepository, RolesRepository>();
builder.Services.AddSingleton<IHttpClientAdapter, HttpClientAdapter>();
builder.Services.AddTransient<IDeviceService, DeviceService>();
builder.Services.AddTransient<IVehicleService, VehicleService>();
builder.Services.AddTransient<IVehicleDeviceService, VehicleDeviceService>();
builder.Services.AddTransient<AppSettings>();

//Use app insights
if (builder.Configuration["GlobalConstant:AppInsightsKey"] != null)
{
    builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["GlobalConstant:AppInsightsKey"]);
}

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vehicle Tracking System API", Version = "v1" });
});

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

app.MapControllers();

app.Run();

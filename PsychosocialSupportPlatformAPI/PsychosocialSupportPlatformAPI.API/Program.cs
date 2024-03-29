using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PsychosocialSupportPlatformAPI.API.Chat;
using PsychosocialSupportPlatformAPI.API.Configurators;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken;
using PsychosocialSupportPlatformAPI.Business.Messages;
using PsychosocialSupportPlatformAPI.Business.Statistics;
using PsychosocialSupportPlatformAPI.Business.Users;
using PsychosocialSupportPlatformAPI.Business.Videos;
using PsychosocialSupportPlatformAPI.DataAccess;
using PsychosocialSupportPlatformAPI.DataAccess.Messages;
using PsychosocialSupportPlatformAPI.DataAccess.Statistics;
using PsychosocialSupportPlatformAPI.DataAccess.Users;
using PsychosocialSupportPlatformAPI.DataAccess.Videos;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

SwaggerConfigurator.ConfigureSwaggerGen(builder.Services);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
         .AddJwtBearer(options =>
         {
             options.SaveToken = true;
             options.RequireHttpsMetadata = false;
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateAudience = true,
                 ValidateIssuer = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ClockSkew = TimeSpan.Zero,

                 ValidAudience = builder.Configuration["JWT:Audience"],

                 ValidIssuer = builder.Configuration["JWT:Issuer"],


                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? string.Empty)),
                 LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

                 NameClaimType = ClaimTypes.Name //JWT �zerinde Name claimne kar��l�k gelen de�eri User.Identity.Name propertysinden elde edebiliriz.

             };


             options.Events = new JwtBearerEvents
             {
                 OnMessageReceived = context =>
                 {
                     var accessToken = context.Request.Query["access_token"];
                     var path = context.HttpContext.Request.Path;
                     if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/message")))
                     {
                         context.Token = accessToken;
                     }
                     return Task.CompletedTask;
                 }
             };
         });


builder.Services.AddAuthorizationBuilder();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<PsychosocialSupportPlatformDBContext>(options => options.UseSqlServer(connectionString));

IdentityConfigurator.ConfigureIdentity(builder.Services);


builder.Services.AddSignalR();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<IVideoService, VideoService>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IVideoStatisticsRepository, VideoStatisticsRepository>();
builder.Services.AddScoped<IVideoStatisticsService, VideoStatisticsService>();

builder.Services.AddHttpClient();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(origin => true)));

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
//app.UseRouting();
app.UseCors();
app.UseWebSockets();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

var basePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedVideos");
if (!Directory.Exists(basePath))
{
    Directory.CreateDirectory(basePath);
}
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(basePath),
    //FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "UploadedVideos")),
    RequestPath = "/UploadedVideos"
});

app.MapHub<ChatHub>("message");


app.Run();

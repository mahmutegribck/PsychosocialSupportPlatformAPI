using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using PsychosocialSupportPlatformAPI.API.Chat;
using PsychosocialSupportPlatformAPI.API.Configurators;
using PsychosocialSupportPlatformAPI.Business.Appointments;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Business.Mails;
using PsychosocialSupportPlatformAPI.Business.Messages;
using PsychosocialSupportPlatformAPI.Business.Statistics.Appointments;
using PsychosocialSupportPlatformAPI.Business.Statistics.Videos;
using PsychosocialSupportPlatformAPI.Business.Users;
using PsychosocialSupportPlatformAPI.Business.Videos;
using PsychosocialSupportPlatformAPI.DataAccess;
using PsychosocialSupportPlatformAPI.DataAccess.Appointments;
using PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules;
using PsychosocialSupportPlatformAPI.DataAccess.Messages;
using PsychosocialSupportPlatformAPI.DataAccess.Statistics;
using PsychosocialSupportPlatformAPI.DataAccess.Statistics.Appointments;
using PsychosocialSupportPlatformAPI.DataAccess.Users;
using PsychosocialSupportPlatformAPI.DataAccess.Videos;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

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

                 NameClaimType = ClaimTypes.Name //JWT üzerinde Name claimne karþýlýk gelen deðeri User.Identity.Name propertysinden elde edebiliriz.

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

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddTransient<IVideoService, VideoService>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();

builder.Services.AddScoped<IVideoStatisticsRepository, VideoStatisticsRepository>();
builder.Services.AddScoped<IVideoStatisticsService, VideoStatisticsService>();

builder.Services.AddScoped<IDoctorScheduleRepository, DoctorScheduleRepository>();
builder.Services.AddScoped<IDoctorScheduleService, DoctorScheduleService>();

builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

builder.Services.AddScoped<IAppointmentScheduleRepository, AppointmentScheduleRepository>();
builder.Services.AddScoped<IAppointmentScheduleService, AppointmentScheduleService>();

builder.Services.AddScoped<IAppointmentStatisticsRepository, AppointmentStatisticsRepository>();
builder.Services.AddScoped<IAppointmentStatisticsService, AppointmentStatisticsService>();

builder.Services.AddTransient<IMailService, MailService>();


builder.Services.AddHttpClient();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(origin => true)));

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

app.UseStaticFiles();

app.UseHttpsRedirection();
//app.UseRouting();
app.UseWebSockets();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("message");

app.Run();

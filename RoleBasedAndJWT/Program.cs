using NLog;
using NLog.Web;
using System.Text.Json.Serialization;
using Model;
using ViewModelAnd;
using Bussiness.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.Extensions.Configuration;
using Model.ModelClass;
using Bussiness.Interfaces;
using Bussiness;
using System.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using RoleBasedAndJWT;
using Microsoft.AspNetCore.Builder;
using WebApi.Authorization;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
//logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    var services = builder.Services;
    var env = builder.Environment;
  
    Microsoft.Extensions.Configuration.ConfigurationManager configuration = builder.Configuration;
    builder.Services.AddScoped<IRoleCRUDInterface, RoleCRUD>();
    builder.Services.AddScoped<IAuthenticationInterface, AuthenticationService>();
    builder.Services.AddScoped<IRoleCheckHelper, RoleCheckHelper>();
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));
    // Add services to the container.
    builder.Services.AddRazorPages();
    
   

    services.AddCors();

    // For Identity Remove Comment Fitrst Attempt 
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddRoles<IdentityRole>();
 ///    .AddDefaultTokenProviders();


    //builder.Services.AddIdentityCore<IdentityUser>(
    //options => options.SignIn.RequireConfirmedAccount = true)
    //.AddRoles<IdentityRole>()
    //.AddEntityFrameworkStores<ApplicationDbContext>();

    // Adding Authentication First Attempt

    //builder.Services.AddAuthentication(options =>
    //{
    //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


    //})
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => {
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["X-Access-Token"];
                    return Task.CompletedTask;
                }           
        };
        });

    // Adding Jwt Bearer First Attempt 
    //.AddJwtBearer(options =>
    //{
    //    options.SaveToken = true;
    //    options.RequireHttpsMetadata = false;
    //    options.TokenValidationParameters = new TokenValidationParameters()
    //    {
    //        ValidateIssuer = true,
    //        ValidateAudience = true,
    //        ValidateLifetime = true,
    //        ValidateIssuerSigningKey = true,
    //        ClockSkew = TimeSpan.Zero,

    //        ValidAudience = configuration["JWT:ValidAudience"],
    //        ValidIssuer = configuration["JWT:ValidIssuer"],
    //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    //    };
    //});
    //builder.Services.AddControllers(); after New Authentication
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    // configure strongly typed settings object
    services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services New Thing?
    services.AddScoped<IJwtUtils, JwtUtils>();
   
    services.AddScoped<IUserInterface, UserService>();


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    // builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // builder.Services.AddScoped<IroleCRUDInterface, RoleCRUDService>();
    builder.Services.AddSwaggerGen();
    var app = builder.Build();
    
   /// app.UseMiddleware<JwtMiddleware>();
    // configure HTTP request pipeline
    {
        // global cors policy
        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        // global error handler
        app.UseMiddleware<ErrorHandlerMiddleware>();

        // custom jwt auth middleware
       /// app.UseMiddleware<JwtMiddleware>();

   
    }
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler("/Error");
    
    }
    //  app.UseElmah();

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    ///Elmah 
    //builder.Services.AddElmah<XmlFileErrorLog>(options =>
    //{
    //    options.LogPath = "~/log";
    //    options.Notifiers.Add(new MyNotifier());
    //    options.Filters.Add(new CmsErrorLogFilter());
    //});
   
    
    // Authentication & Authorization
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapRazorPages();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
   
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
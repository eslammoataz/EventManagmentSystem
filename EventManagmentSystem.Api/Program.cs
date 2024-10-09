using EventManagmentSystem.Application;
using EventManagmentSystem.Application.Helpers.AuthenticationHandler;
using EventManagmentSystem.Application.Services.EmailService;
using EventManagmentSystem.Domain.Models;
using EventManagmentSystem.Infrastructure;
using EventManagmentSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register Application Layer services
builder.Services.AddApplicationProjectDependencies();

// Register Infrastructure Layer services
builder.Services.AddInfrastructureProjectDependencies(builder.Configuration);

// Add services to the container, such as controllers
builder.Services.AddControllers();

// Enable Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    // Optional: Customize Swagger options here
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Event Management API",
        Version = "v1",
        Description = "API documentation for Event Management System"
    });

    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);

    //c.CustomSchemaIds(type => type.FullName);
    //c.UseAllOfToExtendReferenceSchemas();


    // Define the security scheme for an access token without the "Bearer" prefix
    c.AddSecurityDefinition(
     "Bearer",
     new OpenApiSecurityScheme
     {
         Description =
             "Access token authorization. Enter your token in the format: 'Bearer {token}'",
         Name = "Authorization",
         In = ParameterLocation.Header,
         Type = SecuritySchemeType.ApiKey,
         Scheme = "Bearer"
     }
 );

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement()
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
        }
    );
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<ApplicationDbContext>()
       .AddDefaultTokenProviders();



builder.Services.AddAuthentication("CustomToken")
        .AddScheme<AuthenticationSchemeOptions, CustomTokenAuthenticationHandler>("CustomToken", null);

// Add authorization services
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserPolicy", policy =>
        policy.RequireAuthenticatedUser());
});


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    //options.Password.RequireDigit = true;
    //options.Password.RequireLowercase = true;
    //options.Password.RequireNonAlphanumeric = true;
    //options.Password.RequireUppercase = true;
    //options.Password.RequiredLength = 6;
    //options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.RequireUniqueEmail = false;
});

// Bind Gmail settings from appsettings.json
builder.Services.Configure<GmailSettings>(builder.Configuration.GetSection("GmailSettings"));

//builder.Services.PostConfigure<GmailSettings>(gmailSettings =>
//{
//    gmailSettings.SmtpPassword = Environment.GetEnvironmentVariable("GmailSmtpPassword");
//});


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Check if there are pending migrations
    var pendingMigrations = dbContext.Database.GetPendingMigrations();

    if (pendingMigrations.Any())
    {
        Console.WriteLine("Applying pending migrations...");
        dbContext.Database.Migrate();  // Apply pending migrations
    }
    else
    {
        Console.WriteLine("No pending migrations.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Serve the Swagger UI at the app’s root
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Management API v1");
        c.RoutePrefix = string.Empty;  // Set Swagger UI to serve at the app’s root (/)
    });
}

app.UseCors("AllowAllOrigins");


// Other middleware components
app.UseHttpsRedirection();
app.UseAuthentication(); // Needed for ASP.NET Identity
app.UseAuthorization();

app.MapControllers();

app.Run();

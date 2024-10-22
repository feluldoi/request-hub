using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RequestHub.Server.Data;
using RequestHub.Server.ServicesServer.AuthServiceServer;
using RequestHub.Server.ServicesServer.EmailServiceServer;
using RequestHub.Server.ServicesServer.FileUploadServiceServer;
using RequestHub.Server.ServicesServer.TicketServiceServer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//laod config from appsettings.json, envionmental variables, and user secrets
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddEnvironmentVariables()
                      .AddUserSecrets<Program>(); // Only applies to development

// Add services to the container.

builder.Services.AddBlazorise();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IFileUploadServiceServer, FileUploadServiceServer>();
builder.Services.AddScoped<IAuthServiceServer, AuthServiceServer>();

builder.Services.AddScoped<IEmailServiceServer, EmailServiceServer>();
builder.Services.AddScoped<ITicketServiceServer, TicketServiceServer>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();





//Determine the environment
var isDev = builder.Environment.IsDevelopment();
var isProd = builder.Environment.IsProduction();

//Display environemnt
if (isDev)
{
    Console.WriteLine("Development Envionrment Running");

}
else if (isProd)
{
    Console.WriteLine("Production Envionrment Running");

};


//connection string
var devConnectionStr = builder.Configuration["AZURE_CONNECTIONSTRING"];
var prodConnectionStr = builder.Configuration.GetConnectionString("AZURE_CONNECTIONSTRING");


//Handle ConnectionString

builder.Services.AddDbContext<DataContext>(options =>
{
    if (isDev)
    {
        if (string.IsNullOrEmpty(devConnectionStr))
        {
            throw new InvalidOperationException("Developer Connection String Not Set");

        }
        else
        {
            options.UseSqlServer(devConnectionStr, sqlServerOptionsAction: sqlOptions =>
            {
                //added for azure
                sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
            });
        }

    }
    else if (isProd)
    {
        if (string.IsNullOrEmpty(prodConnectionStr))
        {
            throw new InvalidOperationException("Production Connection String Not Set");
        }
        else
        {
            options.UseSqlServer(prodConnectionStr, sqlServerOptionsAction: sqlOptions =>
            {
                //added for azure
                sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
            });
        }
    }


});



//Determine the JWT
var devJWT = builder.Configuration["JWT"];
var prodJWT = Environment.GetEnvironmentVariable("JWT");


//Handle JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        //if evironment is Dev
        if (isDev)
        {
            if (string.IsNullOrEmpty(devJWT))
            {
                throw new InvalidOperationException("Dev JWT Token is not set.");
            }
            else
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(devJWT)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }
        }
        else if (isProd)
        {
            if (string.IsNullOrEmpty(prodJWT))
            {
                throw new InvalidOperationException("Prod JWT Token is not set.");
            }
            else
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(prodJWT)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }
        }
    });





var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();

}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

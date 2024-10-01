using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RequestHub.Server.Data;
using RequestHub.Server.ServicesServer.AuthServiceServer;
using RequestHub.Server.ServicesServer.EmailServiceServer;
using RequestHub.Server.ServicesServer.FileUploadServiceServer;
using RequestHub.Server.ServicesServer.TicketServiceServer;
using System.Data.Common;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddBlazorise();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IFileUploadServiceServer, FileUploadServiceServer>();


//Local Development - user secrets
//var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

//Local Development - user secrets
//builder.Services.AddDbContext<DataContext>(options =>
//{
//    if (connectionString != null)
//    {
//        options.UseSqlServer(connectionString);
//    }
//    else
//    {
//        throw new InvalidOperationException("connection string is null or empty.");
//    }

//});



// Then, retrieve the connection string
//var connectionString = Environment.GetEnvironmentVariable("AzureConnectionString");
var connectionString = builder.Configuration["AzureConnectionString"];

builder.Services.AddDbContext<DataContext>(options =>
{
    if (connectionString != null)
    {
        options.UseSqlServer(connectionString);
    }
    else
    {
        throw new InvalidOperationException("connection string is null or empty.");
    }

});




////connection string in user secrets and Azure
//builder.Services.AddDbContext<DataContext>(options =>
//{
//    if (connectionString != null)
//    {
//        options.UseSqlServer(connectionString);
//    }
//    else
//    {
//        throw new InvalidOperationException("connection string is null or empty.");
//    }
    
//});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddScoped<IAuthServiceServer, AuthServiceServer>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["JWT"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("JWT Token is not set.");
        }


        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8
                 .GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };


    });

builder.Services.AddScoped<IEmailServiceServer, EmailServiceServer>();
builder.Services.AddScoped<ITicketServiceServer, TicketServiceServer>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();

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

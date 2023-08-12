using IdentityServer4.Test;
using locationapi.Identity;
using locationapi.Models.Common;
using locationapi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
//const string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;database=IdentityServer4.Quickstart.EntityFramework-4.0.0;trusted_connection=yes;";

//builder.Services.AddIdentityServer()
//    .AddTestUsers(TestUsers.Users)
//    .AddConfigurationStore(options =>
//    {
//        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
//            sql => sql.MigrationsAssembly(migrationsAssembly));
//    })
//    .AddOperationalStore(options =>
//    {
//        options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
//            sql => sql.MigrationsAssembly(migrationsAssembly));
//    });

//builder.Services.AddIdentityServer()
//    .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
//        //.AddTestUsers(InMemoryConfig.GetUsers())
//        .AddInMemoryClients(InMemoryConfig.GetClients())
//        .AddDeveloperSigningCredential(); //not something we want to use in a production environment;

//builder.Services.AddAuthentication(opt =>
//{
//    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
//.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, opt =>
//{
//    opt.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    opt.Authority = "https://localhost:7217/";
//    opt.ClientId = "zartbit";
//    opt.GetClaimsFromUserInfoEndpoint = true;    
 
//    opt.ResponseType = OpenIdConnectResponseType.Code;
//    opt.SaveTokens = true;
//    opt.ClientSecret = "zbNessd1Lstryke";
//    opt.UsePkce = false;

//    opt.ClaimActions.DeleteClaim("sid");
//    opt.ClaimActions.DeleteClaim("idp");
//});
//JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
//builder.Services.AddAuthorization(options =>
//{ 
//        options.AddPolicy("AdminOnly", policy => policy.RequireClaim("rol","Admin"));    
//});

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

//jwt
var appSettings = appSettingsSection.Get<AppSettings>();
var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);
builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(d =>
{
    d.RequireHttpsMetadata = false;
    d.SaveToken = true;
    d.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(llave),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.UseIdentityServer();

app.Run();

using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using TopSaladSolution.API.Attributes;
using TopSaladSolution.Application.Implement;
using TopSaladSolution.Application.Implement.Auth;
using TopSaladSolution.Application.Interfaces;
using TopSaladSolution.Application.Interfaces.Auth;
using TopSaladSolution.AutoMapperProfile;
using TopSaladSolution.Common.Utilities;
using TopSaladSolution.DataAccess.Common.UnitOfWorkBase.DI;
using TopSaladSolution.Infrastructure.EF;
using TopSaladSolution.Infrastructure.Entities;

var builder = WebApplication.CreateBuilder(args);
var crossDomainOrigins = "topSaladSolutionCrossDomainOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    var security = new Dictionary<string, IEnumerable<string>>
    {
        {"Bearer", new string[] { }},
    };
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "TopSalad API",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

#region [Config-Cors-Domain]
builder.Services.AddCors(o => o.AddPolicy(name: crossDomainOrigins, builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
#endregion


builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<TopSaladDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            SaveSigninToken = true,
            ClockSkew = TimeSpan.Zero,

            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:SecretKey"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                context.NoResult();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                string response =
                    JsonConvert.SerializeObject("The access token provided is not valid.");
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                    response =
                        JsonConvert.SerializeObject("The access token provided has expired.");
                }

                context.Response.WriteAsync(response);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                var payload = new ResUnauthorized();

                if (context.AuthenticateFailure == null)
                {
                    payload.Status = false;
                    payload.Message = "Unauthorize";
                    payload.StatusCode = HttpStatusCode.Unauthorized;
                }
                return context.Response.WriteAsJsonAsync(payload);
            },
            OnMessageReceived = context =>
            {
                //if (context.Request.Path.ToString().StartsWith("/chatHub"))
                //    context.Token = context.Request.Query["access_token"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddDbContext<DbContext, TopSaladDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TopSaladSolutionDb")));
builder.Services.AddUnitOfWorkPool(optionsBuilder =>
{
    optionsBuilder.AddUnitOfWork<TopSaladDbContext>(builder.Configuration["Systems:Pool:Default"]);
});


#region Services
//builder.Services.AddScoped<IAuthorizationFilter, Authorize>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStorageService, FileStorageService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
#endregion

#region Add Repository
//builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(ProductProfile));
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ApplicationMapper());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "/swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

app.UseCors(crossDomainOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();


internal class ResUnauthorized
{
    public HttpStatusCode StatusCode { get; set; }
    public bool Status { get; set; }
    public string Message { get; set; }
}
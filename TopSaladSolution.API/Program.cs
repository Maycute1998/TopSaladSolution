using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TopSaladSolution.AutoMapperProfile;
using TopSaladSolution.Common.Utilities;
using TopSaladSolution.Infrastructure.EF;
using TopSaladSolution.Infrastructure.Repositories;
using TopSaladSolution.Interface.Services;
using TopSaladSolution.Service;

var builder = WebApplication.CreateBuilder(args);
var crossDomainOrigins = "topSaladSolutionCrossDomainOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
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

builder.Services.AddDbContext<DbContext, TopSaladDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TopSaladSolutionDb")));


#region Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStorageService, FileStorageService>();
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
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();

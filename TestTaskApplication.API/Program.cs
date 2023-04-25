using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TestTaskApplication.API.DIExtensions;
using TestTaskApplication.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("ConnectionString")));

//builder.Services.AddMappings();

builder.Services.AddCustomServices();
builder.Services.AddRepositories();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Swagger",
            Version = "v1",
            Description = "Test task"
        }
    );

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.ConfigureCustomExceptionMiddleware();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
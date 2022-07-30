
using MediatR.Infrastructure.Data;
using MediatR.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMemoryCache();

builder.Services.AddDbContext<ApplicationContext>(options =>
                                    options.UseSqlServer(builder.Configuration["DefaultConnection"]));

builder.Services.AddApplication();

builder.Services.AddSwaggerGen(s =>
{
    s.EnableAnnotations();
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "Item API", Version = "v1" });
    s.CustomSchemaIds(x => x.FullName);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Item V1");
});

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

var serviceProvider = builder.Services.BuildServiceProvider();

// Seed database.
using (var scope = serviceProvider.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var applicationContext = services.GetRequiredService<ApplicationContext>();

        await ApplicationContextSeed.SeedAsync(applicationContext);
    }
    catch (Exception exception)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(exception, "An error occurred seeding the DB.");
    }
}

app.Run();
using Backend.Data;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Backend.Interfaces;
using Backend.Services;
using Backend.Repositories;
using Backend.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(Options =>
{
    Options.AddPolicy("CorsPolicy", policyBuilder =>
    {
        policyBuilder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins(
                "https://localhost:3000",
                "http://localhost:3000"
            );
    });
});

builder.Services.AddOpenApi();

builder.Services.AddScoped<IUserRowRepo, UserRowRepo>();
builder.Services.AddScoped<IUserRowService, UserRowService>();

builder.Services.AddScoped<IUserTableRepo, UserTableRepo>();
builder.Services.AddScoped<IUserTableService, UserTableService>();

builder.Services.AddScoped<IUserColumnRepo, UserColumnRepo>();
builder.Services.AddScoped<IUserColumnService, UserColumnService>();

builder.Services.AddDbContextFactory<AppDBContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddControllers().AddNewtonsoftJson(Options =>
{
    Options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Flex Table Api"));

}
app.UseCors("CorsPolicy");

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

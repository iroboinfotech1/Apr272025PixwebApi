using FluentValidation.AspNetCore;
using Microsoft.Extensions.FileProviders;
using sms.space.management.api.BusinessLogic.Filters;
using sms.space.management.application;
using sms.space.management.application.Services;
using sms.space.management.data.access;
using sms.space.management.domain.Entities.BookMeeting;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.RegisterDataAccess(builder.Configuration);
//test checkin
builder.Services.AddFluentValidationAutoValidation();

builder.Logging.AddTraceSource($"{Directory.GetCurrentDirectory()}\\Logs\\Log.txt");

builder.Services.AddSingleton<JwtService>();

// Add services to the container.

//builder.Services.AddControllers();

builder.Services
    .AddControllers(config =>
    {
        config.Filters.Add(typeof(CustomExceptionFilter));
        config.Filters.Add(typeof(ValidationFilter));
    })
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddDirectoryBrowser();


//builder.Services.AddScoped<ICountry>(provider => provider.GetRequiredService<sms.space.management.application.Implementations.Country>());
//builder.Services.AddScoped<sms.space.management.application.Implementations.Country>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//To do exclude the production env from the configuring the swagger ui
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin()
    );

app.UseHttpsRedirection();

app.UseAuthorization();

try
{
    app.UseStaticFiles();
    app.UseStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
        RequestPath = "/StaticFiles"
    });
    app.UseDirectoryBrowser(new DirectoryBrowserOptions()
    {
        FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
        RequestPath = "/StaticFiles"
    });
}
catch (Exception) { 

}

app.MapControllers();

app.Run();

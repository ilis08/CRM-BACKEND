using CRM_BACKEND.API.Extensions;
using LoggerService;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureRepositoryManager();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    //Return 406 Not Acceptable if client tries to negotiate for the media type the server doesn't support.
    config.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson();


var app = builder.Build();

// Configure the HTTP request pipeline.

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
{
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "CRM_BACKEND.API");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

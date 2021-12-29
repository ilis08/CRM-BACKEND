using CRM_BACKEND.API.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureRepositoryManager();

builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

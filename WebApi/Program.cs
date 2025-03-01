using ClassLibrary.Services.Interfaces;
using ClassLibrary.Services.Services;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

// Servicio Conexion -------------------------------------------------
builder.Services.AddTransient<IDbConnection>(options =>
    new SqlConnection(builder.Configuration.GetConnectionString("SqlServerWeb"))
);
// -------------------------------------------------------------------

// Dependency injection ----------------------------------------------
builder.Services.AddScoped<ApiKeyFilter>();
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICoreService, CoreService>();
// -------------------------------------------------------------------

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Swagger -----------------------------------------------------------
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Projects Api v1");
    options.RoutePrefix = string.Empty;
});
// -------------------------------------------------------------------

// Cors --------------------------------------------------------------
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowCredentials();
    options.SetIsOriginAllowed(origin => true);
});
// -------------------------------------------------------------------

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

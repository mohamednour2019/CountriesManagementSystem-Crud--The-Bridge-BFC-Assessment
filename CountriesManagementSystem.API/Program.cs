using CountriesManagementSystem.API.Middlewares;
using CountriesManagementSystem.Application;
using CountriesManagementSystem.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationLayer();
builder.Services.AddInfraLayer(builder.Configuration);

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});
var app = builder.Build();

//migrate db and seeding data
await app.Services.ApplyDatabaseMigrations();

app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

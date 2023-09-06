using CommerceApi.DAL;
using CommerceApi.BLL;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

services.RegisterDALDependencies(builder.Configuration);
services.RegisterBLLDependencies(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
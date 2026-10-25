using ClientHub.Caching;
using ClientHub.Persistense;
using ClientHub.Persistense.EntityFramework;
using ClientHub.UseCases;
using ClientHub.WebApi;
using ClientHub.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddPersistense(builder.Configuration);
builder.Services.AddUseCases();
builder.Services.AddCaching(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.MapControllers();

app.UseMiddleware<EndpointHandlerMiddleware>();

app.Run();

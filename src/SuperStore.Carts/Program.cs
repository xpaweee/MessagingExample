

using SuperStore.Carts.Services;
using SuperStore.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMessagging();
builder.Services.AddHostedService<MessagingBackgroundService>();

var app = builder.Build();

app.MapGet("/", () => "Carts Service!");

var scope = app.Services.CreateScope();


app.Run();
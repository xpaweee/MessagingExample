
using SuperStore.Funds.Messages;
using SuperStore.Shared;
using SuperStore.Shared.Publishers.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMessagging();

var app = builder.Build();

app.MapGet("/", () => "Funds Service!");

app.MapGet("/message/send/EU/{country}", async (IMessagePublisher messagePublisher, string country, string messageId) =>
{
    var message = new FundsMessage(123, 10.00m);
    await messagePublisher.PublishAsync("Funds", $"EU.{country}", message, messageId);
});





app.Run();

using WebApiP41.Models;
using Microsoft.AspNetCore.Mvc;
using WebApiP41.Middleware;
using WebApiP41.Services;

var builder = WebApplication.CreateBuilder(args);

// додати контролер
builder.Services.AddControllers();

builder.Services.AddSingleton<IDataProvider<Message>>(sp =>
{
    var dataFile = builder.Configuration["MessageService:MessageDataFilePath"];
    return new MessageProvider(dataFile);
});

builder.Services.AddSingleton<MessageService>();

builder.Services.AddSingleton<RouteCounterService>(sp =>
{
    var dataFile =builder.Configuration["RouteCounterService:RouteCounterDataFilePath"];
    return new RouteCounterService(dataFile);
});

builder.Services.AddOpenApi();


var app = builder.Build();

app.UseMiddleware<RouteCounterMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "Messages API V1");
       
    });
}

app.MapControllers();
/*
app.MapGet("/api/messages", async (string? from, [FromServices] MessageService messageService) =>
{
    var messages = from is not null
    ? await messageService.GetMessagesByFromAsync(from)
    : await messageService.GetAllMessagesAsync();
    return Results.Ok(messages);
});

app.MapPost("api/messages", async ([FromServices] MessageService messageService, [FromBody] Message message) =>
{
    await messageService.AddMessageAsync(message);
    return Results.Ok(message);
});

app.MapGet("/api/messages/{id:int}", async (int id, [FromServices] MessageService messageService) =>
{
    var message = await messageService.GetMessageByIdAsync(id);
    return message is not null ? Results.Ok(message) : Results.NotFound();
});
app.MapPut("/api/messages/{id:int}",
    async (int id, [FromServices] MessageService messageService, [FromBody] Message updatedMessage) =>
    {
        var message = await messageService.UpdateMessageAsync(id, updatedMessage);
        return message is not null ? Results.Ok(message) : Results.NotFound();
    });
app.MapDelete("/api/messages/{id:int}", async (int id, MessageService messageService) =>
{
    var result = await messageService.DeleteMessageAsync(id);
    if (!result)
    {
        return Results.NotFound(); 
    }
    return Results.Ok();
});
*/

app.Run();



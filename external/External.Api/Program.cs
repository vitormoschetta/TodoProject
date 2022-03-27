using External.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapPost("/todoitem", (TodoItem request) =>
{
    return Results.Ok();
});

app.MapPut("/todoitem", (TodoItem request) =>
{
    return Results.Ok();
});

app.MapDelete("/todoitem", (Guid id) =>
{
    return Results.Ok();
});

app.Run();
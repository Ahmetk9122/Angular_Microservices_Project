using Microsoft.EntityFrameworkCore;
using Todos.WebAPI.Context;
using Todos.WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseInMemoryDatabase("MyDb");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("todos/create", (string work, ApplicationDbContext context) =>
{
    Todo todo = new()
    {
        Work = work,
    };
    context.Add(todo);
    context.SaveChanges();
    return new { Messages = "Todo create is successful" };
});

app.MapGet("todos/getall", (ApplicationDbContext context) =>
{
    var todos = context.Todos.ToList();
    return todos;
});

app.Run();

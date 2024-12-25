using Categories.WebAPI.Context;
using Categories.WebAPI.Dtos;
using Categories.WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/categories/getall", async (ApplicationDbContext context, CancellationToken cancellationToken) =>
{
    var categories = await context.Categories.ToListAsync();
    return categories;
});

app.MapPost("/categories/create", async (CreateCategoryDto createCategoryDto, ApplicationDbContext context, CancellationToken cancellationToken) =>
{
    bool isNameExists = await context.Categories.AnyAsync(x => x.Name == createCategoryDto.Name, cancellationToken);
    if (isNameExists)
        return Results.BadRequest(new { Message = "Category already exists!" });

    Category category = new Category() { Name = createCategoryDto.Name };
    await context.Categories.AddAsync(category, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return Results.Ok(new { Message = "Category create is successful" });
});

app.Run();

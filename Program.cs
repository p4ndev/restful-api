using Etag.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<EtagMiddleware>();

app.MapPost("/product", (Product model) => {
    Console.WriteLine("New product added...");
    return Results.StatusCode(StatusCodes.Status201Created);
})
    .WithName("PostProduct")
        .WithOpenApi();

app.MapGet("/product", () => {
    var product = new Product("Sunglass", true);
    return Results.Ok(product);
})
    .WithName("GetProduct")
        .WithOpenApi();

app.Run();
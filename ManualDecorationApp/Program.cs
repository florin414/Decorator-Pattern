using ManualDecoration.Services;
using ManualDecorationApp.Extensions;
using ManualDecorationApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<IFoodService, RetryPolicySpoonacularFoodService>();

//builder.Services.AddSingleton<SpoonacularFoodService>();
//builder.Services.AddSingleton<IFoodService>(x =>
//    new ResilientSpoonacularFoodService(x.GetRequiredService<SpoonacularFoodService>()));

builder.Services.AddResilient<SpoonacularFoodService, IFoodService, ResilientSpoonacularFoodService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/foods", GetGroceryProductsAsync)
    .WithName("GetGroceryProducts");

app.Run();

async Task<IResult> GetGroceryProductsAsync(string query, int number, IFoodService foodService)
{
    var foods = await foodService.GetGroceryProductsAsync(query, number);
    return foods != null ? Results.Ok(foods) : Results.NotFound();
}

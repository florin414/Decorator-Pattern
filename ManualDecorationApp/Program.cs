var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfig(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var env = builder.Environment.EnvironmentName;
var appName = builder.Environment.ApplicationName;

builder.Configuration.AddSystemsManager($"/{env.ToLower()}/spoonacularfood-api");

builder.Configuration.AddSecretsManager(configurator: options =>
{
    options.SecretFilter = entry => entry.Name.StartsWith($"{env}_{appName}");
    options.KeyGenerator = (_, s) =>
        s.Replace($"{env}_{appName}_", string.Empty).Replace("__", ":");
});

//builder.Services.AddSingleton<IFoodService, RetryPolicySpoonacularFoodService>();

//builder.Services.AddSingleton<SpoonacularFoodService>();
//builder.Services.AddSingleton<IFoodService>(x =>
//    new ResilientSpoonacularFoodService(x.GetRequiredService<SpoonacularFoodService>()));

builder.Services.AddResilient<
    SpoonacularFoodService,
    IFoodService,
    ResilientSpoonacularFoodService
>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/foods", GetGroceryProductsByQueryAndNumberAsync).WithName("GetGroceryProductsByQueryAndNumber");

app.Run();

async Task<IResult> GetGroceryProductsByQueryAndNumberAsync(string query, int number, IFoodService foodService)
{
    var foods = await foodService.GetGroceryProductsByQueryAndNumberAsync(query, number);
    return foods != null ? Results.Ok(foods) : Results.NotFound();
}

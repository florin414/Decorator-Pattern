var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfig(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "SpoonacularFoodApi", Version = "v1" })
);

builder.Configuration.AddAmazonSystemsManager(builder.Environment);
builder.Configuration.AddAmazonSecretsManager(builder.Environment);
builder.Services.AddSpoonacularFoodService();

var app = builder.Build();

app.UseSwaggerIfDevelopment();
app.UseHttpsRedirection();
app.MapSpoonacularFoodApi();

app.Run();

namespace ManualDecorationApp.Extensions;

public static class SwaggerApiExtensions
{
    public static IApplicationBuilder UseSwaggerIfDevelopment(this IApplicationBuilder app)
    {
        if (
            app.ApplicationServices.GetService(typeof(IWebHostEnvironment))
                is IWebHostEnvironment env
            && env.IsDevelopment()
        )
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}

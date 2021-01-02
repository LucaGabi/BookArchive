using Microsoft.AspNetCore.Builder;

namespace BookArchive.API
{
    public static class SwaggerConfig
    {
        public static void UseSwaggerPage(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.DocumentTitle = "BookArchive APIs";
                c.DefaultModelsExpandDepth(0);
            });
        }
    }
}

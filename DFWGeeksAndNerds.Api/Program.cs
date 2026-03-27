
using Microsoft.OpenApi;

namespace DFWGeeksAndNerds.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSwaggerGen(C =>
            {
                C.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DFW Geeks and Nerds API",
                    Description = "An ASP.NET Core Web API for managing DFW Geeks and Nerds data."

                });
            } );

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // This will enable the middleware to serve the generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DFW Geeks and Nerds API v1"));

            app.UseAuthorization();

            // This will map the controllers to the endpoints based on the attributes defined in the controllers.
            app.MapControllers();

            app.Run();
        }
    }
}

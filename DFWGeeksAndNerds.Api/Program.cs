
//Program.cs
using DFWGeeksAndNerds.Api.Providers;
using Microsoft.OpenApi;

namespace DFWGeeksAndNerds.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
             // Add services to the container.
            var builder = WebApplication.CreateBuilder(args);

            // This line of code adds the NerdsandGeeksDbContext to the service collection, which allows the application to use it for database operations.
            // .AddDbContext registers the DbContext with the dependency injection container, so it can be injected into controllers and other services that need to interact with the database.
            // dependency injection is a contianer with services and methods that can be used throughout the project. 
            // dependency injection is used used builder.Services -> builder is the container. .Add -> something
            builder.Services.AddDbContext<NerdsandGeeksDbContext>();

            // This line of code adds the NerdsandGeeksProvider to the service collection, which allows the application to use it for data access operations related to "nerds and geeks".
            builder.Services.AddSwaggerGen(C =>
            {
                C.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DFW Geeks and Nerds API",
                    Description = "An ASP.NET Core Web API for managing DFW Geeks and Nerds data."

                });
            } );
            
            // Registers controller support for attribute-routed API endpoints.
            /* 
            Think of AddControllers as hiring and training the kitchen staff.

            1. Turns on the MVC controller system for APIs.

            What that means: Controller discovery
            ASP.NET scans assemblies to find classes marked as controllers.
            Action execution pipeline
            It sets up the machinery to invoke action methods when a route matches.
            Model binding
            Incoming route/query/header/body data is mapped into action parameters and models.
            JSON input/output formatters
            Request bodies can be parsed from JSON, and responses can be serialized to JSON.
            Validation
            Data annotation validation runs automatically, especially with ApiController behavior.
            Filters and API behavior features
            Authorization, exception/action filters, and automatic 400 responses for invalid models are wired in.

            2. Registers the services needed to discover controller classes and execute action methods.
            3. Enables model binding, validation, filters, formatters, and API response behavior.
            4. Works with MapControllers so attribute routes become live endpoints.

            Key pieces AddControllers enables:
            Controller activation through dependency injection.
            Input parsing from JSON request bodies.
            Automatic validation errors for invalid models.
            Content negotiation (JSON output by default).
            Filter pipeline (authorization, action, exception filters).

            AddControllers does not map URLs by itself. You still need to define routes using attributes in your controllers or use MapControllers in the middleware pipeline to enable routing to those controllers.
            */ 
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // Registers built-in OpenAPI support.
            builder.Services.AddOpenApi();
            
            // Finalizes service registration and builds the middleware pipeline host.
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // These lines of code set up the swagger service. Swagger is like a menu for a waiter. 
            // It tells the waiter (the API) what you want to order (the endpoints) and how to prepare it (the request and response formats).

            // Serves generated Swagger JSON endpoint.
            app.UseSwagger();
            // Serves interactive Swagger UI and points it to your v1 JSON doc.
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DFW Geeks and Nerds API v1"));
            // Runs authorization checks for endpoints that require auth policies.
            app.UseAuthorization();

            // This will map the controllers to the endpoints based on the attributes defined in the controllers.
            app.MapControllers();

            app.Run();
        }
    }
}

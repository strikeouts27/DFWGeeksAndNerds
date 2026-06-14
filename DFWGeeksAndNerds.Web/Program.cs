using DFWGeeksAndNerds.Shared.Services;
using DFWGeeksAndNerds.Web.Components;

namespace DFWGeeksAndNerds.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddBlazorBootstrap();
            builder.Services.AddHttpClient(
                "GeeksAndNerdsAPI", client =>
                {
                    client.BaseAddress = new Uri("http://localhost:8000/");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                }
                );

            builder.Services.AddScoped<EventsDataService>(); 
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}

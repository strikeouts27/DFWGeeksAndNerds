using DFWGeeksAndNerds.Services;    
    
// Create the app builder: loads config/logging defaults and prepares DI + hosting using command-line args.
var builder = WebApplication.CreateBuilder(args);

// Register MVC services so controllers and Razor views are available.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("GeeksAndNerdsAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5003/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// this will create a new service for each inital call to the service. Every time a page or controller calls for the service, it will create a new instance of the service. 
// look up singletons in your spare time. -> when you want the data to be reused for logins and stuff. no session states. 
builder.Services.AddScoped<EventsDataService>();

// Build the app after all services are registered.
var app = builder.Build();

// Configure middleware for production-specific behavior.
if (!app.Environment.IsDevelopment())
{
    // Send users to a friendly error page if an unhandled exception occurs.
    app.UseExceptionHandler("/Home/Error");
    // In production, instruct browsers to use HTTPS for future requests.
    app.UseHsts();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();
// Enable endpoint routing so requests can be matched to controllers.
app.UseRouting();

// Apply authorization rules before dispatching to endpoints.
app.UseAuthorization();

// Map static assets (CSS/JS/images) from wwwroot for efficient serving.
app.MapStaticAssets();

// Define the conventional MVC route:
// /{controller}/{action}/{id?} with Home/Index as defaults.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    // Enables static asset metadata integration for this endpoint mapping.
    .WithStaticAssets();

// Start listening for incoming HTTP requests.
app.Run();

// Create the app builder: loads config/logging defaults and prepares DI + hosting using command-line args.
var builder = WebApplication.CreateBuilder(args);

// Register MVC services so controllers and Razor views are available.
builder.Services.AddControllersWithViews();

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

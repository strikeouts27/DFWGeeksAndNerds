// ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;

// This class represents the application's database context.
// EF Core uses it to manage database connections and queries.
public class ApplicationDbContext : DbContext
{
    // The constructor receives configuration options from dependency injection
    // and passes them to the base DbContext class.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
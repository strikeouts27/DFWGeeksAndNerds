using DFWGeeksAndNerds.Api.Models;
using Microsoft.EntityFrameworkCore;

// Provider folder is used for the database and api functionality to work. 

 
namespace DFWGeeksAndNerds.Api.Providers
{
    public class NerdsandGeeksDbContext : DbContext
    {
        private readonly IConfiguration _config;

        // this code tells the api that we can access find these respective tables in the database through these properties. 
        public virtual DbSet<EventDTO> Events { get; set; }

        public virtual DbSet<GameDTO> Games { get; set; }



        // Nerdsand GeeksDbContext is created on this line which gives the application the ability to connect to the database and perform CRUD operations on it.
        // config is targetting the appsettings.json file which gives it the connection string to connect to the database.
        // IConfiguration is the type that config conforms to. 
        // config is used to access the configuration settings in the appsettings.json file, which includes the connection string for the database.
        // container has a specific meaning in the programming world so do not call config a container call it a variable. 

        public NerdsandGeeksDbContext(DbContextOptions<NerdsandGeeksDbContext> options, IConfiguration config) : base(options)
        {
            // _config is assigned the value of config when the object is initialized.
            // This allows the DbContext to access the configuration settings, such as the connection string, when it needs to connect to the database.
            _config = config;
        }

        // This method overrides configuring entity framework to connect to a database. 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // this commented code would tell DBCONTEXT to run the default onConfiguration. base.OnConfiguring(optionsBuilder);
            // this code checks if optionsbuilder is not configured. 
            if (!optionsBuilder.IsConfigured)
            {
                // if optionsbuilder is not configured, use the connection settings we made in appsettings.json.
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            }
        }

        // This method says what tables in the database connect to which models. The id in the model is the primary key in the database. 
        // Earlier in this file we specified tables, now we are specifying the primary keys for those tables.
        // We can also input restrictions for the tables here. Not null, required. 
        // the best validation would be on the website. fail fast quickly. 
        // website, the models, the database all have data validation failsafe available. 
        // if data is not transmitting understanding where the locks are is critical.
        // entity framework uses ORM's to map relationships between code and tables. 
        // most of the time we want a 1 to 1 representation of data to models. 
        // models had to match the tables. 
        // the table has a primary key, we made the id field the primary key table 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventDTO>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<GameDTO>()
                .HasKey(g => g.Id);


            base.OnModelCreating(modelBuilder);
        }
    }
}

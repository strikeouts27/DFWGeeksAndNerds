namespace DFWGeeksAndNerds.Api.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

    }

    // DTO stands for Data Transfer Object, it is a design pattern used to transfer data between software application subsystems.
    // DTO's never work past the api. 
    // DTO's are pure data. They will be what is sent to the client THe programmer is the converter. we just make a shipment of pure data. 
    public class GameDTO
    {
        
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}

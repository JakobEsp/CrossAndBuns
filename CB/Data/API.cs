using CB.Data;
using System.Net;

public class API
{
    // Create an int id field
    public int Id { get; set; }
    // Create a IPAdress list field
    public List<IPAddress>? IP { get; set; } = new();
    // Create a string domain field
    public string? Domain { get; set; } 
    // Create a dynamic list 
    public List<Parser> Text { get; set; } = new();
    // Create a string days field
    private string? days { get; set; }
    // Create a string days property
    public string? Days
    {
        // Get the value of days
        get { return days; }
        // Set the value of days
        set
        {
            // Check if value is null
            if (value == null)
                days = "0"; // Set days to 0
            else // If value isn't null
                days = value; // Set days to value
        }
    }

    private readonly int _id = 0; // Create an int id field

    public API()
    {
        Id = Interlocked.Increment(ref _id); // Increment the id field in the default ctor
    }
}

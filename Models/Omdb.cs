public class OmdbSearchResult
{
    public List<OmdbMovie> Search { get; set; }
}

public class OmdbMovie
{
    public string Title { get; set; }
    public string Year { get; set; }
    // Add other properties as needed
}
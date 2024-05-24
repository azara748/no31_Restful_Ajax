namespace AJAPI.Models;

public class josn回傳
{
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public List<Category> Categories { get; set; }
    public List<SpotImagesSpot>? SpotsResult { get; set; }
}


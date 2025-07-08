public class Location
{
    private string locationName;
    private LocationType locationType;

    public string LocationName => locationName;
    public LocationType LocationType => locationType;

    public Location()
    {
        locationName = string.Empty;
        locationType = LocationType.None;
    }

    public Location(string locationName, LocationType locationType)
    {
        this.locationName = locationName;
        this.locationType = locationType;
    }
}

namespace IOberverExample
{
    public struct Location
    {
        public double Latitude { get; }
        public double Longitude { get; }
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
using System;

namespace IOberverExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new LocationTracker();
            var reporter1 = new LocationReporter("FixedGPS");
            reporter1.Subscribe(provider);
            var reporter2 = new LocationReporter("MobileGPS");
            reporter2.Subscribe(provider);

            provider.TrackLocation(new Location(47.6456, -122.1312));
            reporter1.Unsubscribe();
            provider.TrackLocation(new Location(47.6677, -122.1199));
            provider.TrackLocation(null);
            provider.EndTransmission();
        }
    }
}

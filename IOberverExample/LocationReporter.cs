using System;

namespace IOberverExample
{
    public class LocationReporter : IObserver<Location>
    {
        private IDisposable unsubscriber;
        private string _name;

        public string Name => _name;

        public LocationReporter(string name)
        {
            _name = name;
        }

        public virtual void Subscribe(IObservable<Location> provider)
        {
            if (provider != null)
               unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }

        public virtual void OnCompleted()
        {
            Console.WriteLine($"The location tracker has completed transmitting data to {Name}");
            Unsubscribe();
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"{Name}: The location cannot be determined");
        }

        public void OnNext(Location value)
        {
            Console.WriteLine($"{Name}: The current location is {value.Latitude} {value.Longitude}");
        }
    }
}
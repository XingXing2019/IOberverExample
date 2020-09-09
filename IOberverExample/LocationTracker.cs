using System;
using System.Collections.Generic;

namespace IOberverExample
{
    public class LocationTracker : IObservable<Location>
    {
        private List<IObserver<Location>> _observers;
        public LocationTracker()
        {
            _observers = new List<IObserver<Location>>();
        }

        public IDisposable Subscribe(IObserver<Location> observer)
        {
            if(!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        public void TrackLocation(Location? location)
        {
            foreach (var observer in _observers)
            {
                if(location.HasValue)
                    observer.OnNext(location.Value);
                else
                    observer.OnError(new LocationUnknownException());
            }
        }

        public void EndTransmission()
        {
            foreach (var observer in _observers.ToArray())
            {
                if(_observers.Contains(observer))
                    observer.OnCompleted();
            }
            _observers.Clear();
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<Location>> _observers;
            private readonly IObserver<Location> _observer;

            public Unsubscriber(List<IObserver<Location>> observers, IObserver<Location> observer)
            {
                _observers = observers;
                _observer = observer;
            }
            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}
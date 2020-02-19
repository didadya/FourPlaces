using Plugin.Geolocator;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMob.Models;
using ProjetDevMob.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace ProjetDevMob.ViewModels
{
	public class MapViewModel : ViewModelBase
	{
        private const int V = 1000;
        private ObservableCollection<DataPlace> ListePlaces;



        public Map mainMap { get; private set; }

        public MapViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Map";
            mainMap = new Map();
            ListePlaces = new ObservableCollection<DataPlace>();
            mainMap.IsShowingUser = true;
            getCurrentLocation();
            mainMap.MapType = MapType.Street;
        }

        private void LoadPins()
        {
            foreach (var el in ListePlaces)
            {
                var position = new Position(el.latitude, el.longitude); // Latitude, Longitude
                var pin = new Pin
                {
                    Type = PinType.SavedPin,
                    Position = position,
                    Label = "\"" + el.title + "\""
                };
                mainMap.Pins.Add(pin);
            }
        }

        public async void getCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1000));
            mainMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                                         Distance.FromMiles(1)));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            ListePlaces = new ObservableCollection<DataPlace>();
            LoadPins();
        }
    }
}

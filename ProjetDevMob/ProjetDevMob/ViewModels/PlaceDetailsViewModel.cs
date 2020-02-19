using Prism.Commands;
using Prism.Navigation;
using ProjetDevMob.Models;
using ProjetDevMob.Services;
using System;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjetDevMob.ViewModels
{
    public class PlaceDetailsViewModel : ViewModelBase
	{
        ListviewAggregatedPlace plac;

        private ImageSource _imageName ;
        public ImageSource ImageName
        {
            get { return _imageName; }
            set { SetProperty(ref _imageName, value); }
        }

        private string _heure;
        public string Heure
        {
            get { return _heure; }
            set { SetProperty(ref _heure, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _coordonnee;
        public string Coordonnee
        {
            get { return _coordonnee; }
            set { SetProperty(ref _coordonnee, value); }
        }

        private string _adress;
        public string Adress
        {
            get { return _adress; }
            set { SetProperty(ref _adress, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _tag;
        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        public DelegateCommand Supprimer { get; private set; }

        public PlaceDetailsViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Détails Place";
            Supprimer = new DelegateCommand(supprimerAsync);
        }

       

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Console.WriteLine("ssf");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Console.WriteLine("ssf");
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            Console.WriteLine("aaaaf");

            plac = parameters.GetValue<ListviewAggregatedPlace>("ListviewAggregatedPlace");
            ImageName = plac.TiltePhoto.TilteImage;
            Heure = DateTime.Now.ToString();
            Name = plac.DdataPlace.title;
            Tag = "sur Map";
            Description = plac.DdataPlace.description;
            Coordonnee = "Latitude :" + plac.DdataPlace.latitude + " - " + " Longitude :" + plac.DdataPlace.longitude;
            getAdress();
            //Adress = adress; // plac.Adress;

        }
        private async void supprimerAsync()
        {
            var answer = await App.Current.MainPage.DisplayAlert("Question?", "Voulez-vous vraiment supprimer ?", "Yes", "No");

            if (answer)
            {
                await App.Current.MainPage.DisplayAlert("Oups", "Je ne peux pas supprimer :)", "OK");
            }

        }

        private async void getAdress()
        {
            var placemarks = await Geocoding.GetPlacemarksAsync(plac.DdataPlace.latitude, plac.DdataPlace.longitude);
            //string adress = "";
            var placemark = placemarks?.FirstOrDefault();
            if (placemark != null)
            {
                Adress = placemark.FeatureName + " " + placemark.Thoroughfare + " " + ", " + placemark.PostalCode + " " + placemark.Locality + " - " + placemark.CountryName;
            }
        }

    }
}

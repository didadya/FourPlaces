using Acr.UserDialogs;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Media;
using Prism.Commands;
using Prism.Navigation;
using ProjetDevMob.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDevMob.ViewModels
{
    public class AddPlaceViewModel : ViewModelBase
    {

        public DelegateCommand PrendrePhoto { get; private set; }
        public DelegateCommand CommandAddEnreg { get; private set; }

        private int _id;
        public int id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private string _photoImage;
        public string PhotoImage
        {
            get { return _photoImage; }
            set { SetProperty(ref _photoImage, value); }
        }

        private int _longueurImage = 250;
        public int LongueurImage
        {
            get { return _longueurImage; }
            set { SetProperty(ref _longueurImage, value); }
        }

        private string _titre;
        public string Titre
        {
            get { return _titre; }
            set { SetProperty(ref _titre, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private Byte[] _imageData;
        public Byte[] imageData
        {
            get { return _imageData; }
            set { SetProperty(ref _imageData, value); }
        }

        public AddPlaceViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "Ajouter un lieu";
            PrendrePhoto = new DelegateCommand(prendrePhotoAsync);
            CommandAddEnreg = new DelegateCommand(AddEnregAsync, CanAddEnreg).ObservesProperty(() => Titre).ObservesProperty(() => Description).ObservesProperty(() => PhotoImage);
        }

        private async void prendrePhotoAsync()
        {
            string dateTime = DateTime.Now.ToString("yyyy-dd-M_HH-mm-ss");
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "TestPhoto",
                Name = dateTime+".jpg"
            });
            if (file != null)
            {
                Console.WriteLine("Ennem path: " + file.Path);
                PhotoImage = file.Path;
                imageData = File.ReadAllBytes(file.Path);
            }

        }

        private async void AddEnregAsync()
        {
            var answer = await App.Current.MainPage.DisplayAlert("Question?", "Voulez-vous vraiment enregistrer ?", "Yes", "No");

            if (answer)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1000));

                // Enregistrement de photo
                var imageContent = new ByteArrayContent(imageData);
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                MultipartFormDataContent requestContent = new MultipartFormDataContent();
                requestContent.Add(imageContent, "file", "file.jpg");

                HttpClient _client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://td-api.julienmialon.com/images");
                request.Content = requestContent;
                HttpResponseMessage response = await _client.SendAsync(request);

                string result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ImageResponse res = JsonConvert.DeserializeObject<ImageResponse>(result.ToString());
                    id = res.data.id;
                }

                // Enregesitrement du lieu
                DataPlace Enreg = new DataPlace(Title, Description, id, position.Latitude, position.Longitude);
                HttpRequestMessage request2 = new HttpRequestMessage(HttpMethod.Post, "https://td-api.julienmialon.com/places");
                if (Enreg != null)
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(Enreg), Encoding.UTF8, "application/json");
                }
                var response2 = await _client.SendAsync(request2);
                Task<string> result2 = response2.Content.ReadAsStringAsync();
                try
                {
                    AuthVerif verification = JsonConvert.DeserializeObject<AuthVerif>(result.ToString());
                    if (response2.IsSuccessStatusCode && verification.Is_success == true)
                    {
                        UserDialogs.Instance.Toast("Enregistrement avec succès!");
                        resetInputs();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Erreur", result.ToString(), "OK");
                    }
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("Erreur", e.Message, "OK");
                }
            }
        }



        private bool CanAddEnreg()
        {
            bool isValid = true;
            if (Titre == null || Titre == "")
                isValid = false;

            if (Description == null || Description == "")
                isValid = false;

            if (PhotoImage == null)
                isValid = false;

            return isValid;
        }

        private void resetInputs()
        {
            Titre = null;
            Description = null;
            PhotoImage = null;
        }

    }
}

using Newtonsoft.Json;
using ProjetDevMob.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjetDevMob.Views
{
    public partial class ListePlaces : ContentPage
    {
        public List<DataPlace> _places = new List<DataPlace>();

        public ObservableCollection<ListviewAggregatedPlace> listViewPlaces = new ObservableCollection<ListviewAggregatedPlace>();


        public ListePlaces()
        {
            InitializeComponent();

            GetPlaces();
        }

                
        public async void GetPlaces()
        {
            int num = 0;
            string url = "";
            HttpClient _client = new HttpClient();
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://td-api.julienmialon.com/places");
                var response = await _client.SendAsync(request);
                Task<string> result = response.Content.ReadAsStringAsync();
                Place verification = JsonConvert.DeserializeObject<Place>(result.Result.ToString());
                _places = verification.Data.ToList();

                int nombre = _places.Count();

                Stream stream = await _client.GetStreamAsync("https://td-api.julienmialon.com/images/1");
                Image img = new Image();
                img.Source = ImageSource.FromStream(() => stream);
                Photo verif = new Photo(img.Source);
                listViewPlaces.Add(new ListviewAggregatedPlace(_places[0], verif));
                ListEnreg.ItemsSource = listViewPlaces;

                for (int i = 1; i < nombre; i++)
                {
                    num = _places[i].id;
                    url = "https://td-api.julienmialon.com/images/" + num;
                    stream = await _client.GetStreamAsync(url);
                    img = new Image();
                    img.Source = ImageSource.FromStream(() => stream);
                    verif = new Photo(img.Source);

                    listViewPlaces.Add(new ListviewAggregatedPlace(_places[i], verif));
                }

                //ListEnreg.ItemsSource = listViewPlaces;
            }
            catch(Exception e)
            {
                throw e;
            }            
        }
    }
}

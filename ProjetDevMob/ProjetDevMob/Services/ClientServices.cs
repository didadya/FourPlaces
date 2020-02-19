using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjetDevMob.Models;

namespace ProjetDevMob.Services
{
    class ClientServices : IClientServices
    {
        HttpClient _client = new HttpClient();

        private List<DataPlace> _places;

        // retourne la liste de toutes les places
        public List<DataPlace> GetListePlaces()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://td-api.julienmialon.com/places");
            var response = _client.SendAsync(request);

            Task<string> result = response.Result.Content.ReadAsStringAsync();

            Place verification = JsonConvert.DeserializeObject<Place>(result.Result.ToString());

            _places = new List<DataPlace>();

            _places = verification.Data.ToList();

            return _places;

        }
    }
}

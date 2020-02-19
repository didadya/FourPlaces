using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using ProjetDevMob.Models;

namespace ProjetDevMob.ViewModels
{
    class InscriptionViewModel : ViewModelBase
    {
        private string _email = "";
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _firstName = "";
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName = "";
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _password = "";
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _confirmPassword = "";
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

        public DelegateCommand InscriptionCommand { get; private set; }

        public InscriptionViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Inscription";
            InscriptionCommand = new DelegateCommand(commandInscriptionAsync, canInscription).ObservesProperty(() => Email).ObservesProperty(() => FirstName).ObservesProperty(() => LastName).ObservesProperty(() => Password).ObservesProperty(() => ConfirmPassword);
        }

        private async void commandInscriptionAsync()
        {
            if (Password != ConfirmPassword)
                await App.Current.MainPage.DisplayAlert("Erreur", "Le mot de passe n'est pas le même !", "OK");
            else
                await InscriptionAsync();

        }

        private async Task InscriptionAsync()
        {
            HttpClient _client = new HttpClient();
            var userProfil = new UserProfil(Email = this.Email, FirstName = this.FirstName, LastName = this.LastName, Password = this.Password);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://td-api.julienmialon.com/auth/register");
            if (userProfil != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(userProfil), Encoding.UTF8, "application/json");
            }
            var response = await _client.SendAsync(request);

            Task<string> result = response.Content.ReadAsStringAsync();

            try
            {
                AuthVerif verification = JsonConvert.DeserializeObject<AuthVerif>(result.Result.ToString());
                if (verification.Is_success == true)
                {
                    await NavigationService.NavigateAsync("LoginPage");
                    UserDialogs.Instance.Toast("Enregistrement avec succès!");
                    resetInputs();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Erreur", result.Result.ToString(), "OK");
                }
            }
            catch(Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Erreur", e.Message, "OK");
            }                
        }

        private bool canInscription()
        {
            if (Email == "")
                return false;

            if (FirstName == "")
                return false;

            if (LastName == "")
                return false;

            if (Password == "")
                return false;

            if (ConfirmPassword == "")
                return false;

            return true;
        }

        private void resetInputs()
        {
            Email = null;
            FirstName = null;
            LastName = null;
            Password = null;
            ConfirmPassword = null;
        }
    }
}

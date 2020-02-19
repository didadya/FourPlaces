using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using ProjetDevMob.Models;
using ProjetDevMob.Services;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDevMob.ViewModels
{
    class LoginPageViewModel : ViewModelBase
    {

        private IClientServices _clientServices;
        private string _userName = "";
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password = "";
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public DelegateCommand CommandMovePage { get; private set; }
        public DelegateCommand CommandGoInscription { get; private set; }

        public LoginPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Login";
            CommandMovePage = new DelegateCommand(MovePage, CanLogin).ObservesProperty(() => UserName).ObservesProperty(() => Password);
            CommandGoInscription = new DelegateCommand(commandGoInscription);
        }

        private void commandGoInscription()
        {
            NavigationService.NavigateAsync("Inscription");
        }

        private bool CanLogin()
        {
            if (UserName == "")
                return false;

            if (Password == "")
                return false;

            return true;
        }

        private void MovePage()
        {
            var navigationParam = new NavigationParameters();
            navigationParam.Add("UserName", UserName);
            navigationParam.Add("Password", Password);

            HttpClient _client = new HttpClient();
            var user = new User(UserName, Password);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://td-api.julienmialon.com/auth/login");
            if (user != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            }

            var response = _client.SendAsync(request);

            Task<string> result = response.Result.Content.ReadAsStringAsync();

            AuthVerif verification = JsonConvert.DeserializeObject<AuthVerif>(result.Result.ToString());

            if (verification.Is_success == true)
            {
                //
                NavigationService.NavigateAsync("MasterPage/NavigationPage/HomePage", navigationParam);
                resetInputs();
            }  
            else
                App.Current.MainPage.DisplayAlert("Erreur", "Login invalide, réessayez !", "OK");
        }

        private void resetInputs()
        {
            UserName = null;
            Password = null;
        }

    }
}

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProjetDevMob.Models;
using ProjetDevMob.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjetDevMob.ViewModels
{
	public class MasterPageViewModel : ViewModelBase
	{
        public DelegateCommand CommandGoAccueil { get; private set; }
        public DelegateCommand CommandGoNouveau { get; private set; }
        public DelegateCommand CommandGoCamera { get; private set; }
        public DelegateCommand CommandGoMap { get; private set; }
        public DelegateCommand CommandGoListePlaces { get; private set; }
        public DelegateCommand CommandGererProfil { get; private set; }
        public DelegateCommand CommandDeconnection { get; private set; }

        public MasterPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            // Title = "Détails";
            CommandGoAccueil = new DelegateCommand(commandGoAccueil);
            CommandGoNouveau = new DelegateCommand(commandGoNouveau);
            CommandGoCamera = new DelegateCommand(commandGoCamera);
            CommandGoMap = new DelegateCommand(commandGoMap);
            CommandGoListePlaces = new DelegateCommand(commandGoListePlaces);
            CommandGererProfil = new DelegateCommand(commandGererProfil);
            CommandDeconnection = new DelegateCommand(commandDeconnection);
        }

        private void commandGererProfil()
        {
            
        }

        private void commandDeconnection()
        {
            NavigationService.NavigateAsync("LoginPage");
        }

        private void commandGoAccueil()
        {
            NavigationService.NavigateAsync("NavigationPage/HomePage");
        }

        private void commandGoNouveau()
        {
            NavigationService.NavigateAsync("NavigationPage/AddPlace");
        }
        private void commandGoCamera()
        {
            NavigationService.NavigateAsync("NavigationPage/TestCamera");
        }
        private void commandGoMap()
        {
            NavigationService.NavigateAsync("NavigationPage/Map");
        }
        private void commandGoListePlaces()
        {
            NavigationService.NavigateAsync("NavigationPage/ListePlaces");
        }
    }
}

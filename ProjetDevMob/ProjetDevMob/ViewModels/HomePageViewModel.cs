using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Xamarin.Forms;

namespace ProjetDevMob.ViewModels
{
	public class HomePageViewModel : ViewModelBase
	{
        public HomePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Accueil";
        } 
    }
}

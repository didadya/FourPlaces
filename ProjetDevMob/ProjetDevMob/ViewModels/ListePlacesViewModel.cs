using Prism.Commands;
using Prism.Navigation;
using ProjetDevMob.Models;
using ProjetDevMob.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjetDevMob.ViewModels
{
    public class ListePlacesViewModel : ViewModelBase
	{

        private ObservableCollection<ListviewAggregatedPlace> _filtered;
        public ObservableCollection<ListviewAggregatedPlace> FilteredEnreg
        {
            get { return _filtered; }
            set { SetProperty(ref _filtered, value); }
        }

        public DelegateCommand<ListviewAggregatedPlace> CommandPlaceDetails { get; private set; }
        public DelegateCommand SortDown { get; private set; }
        public DelegateCommand SortUp { get; private set; }


        public ListePlacesViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Liste des lieux";
            CommandPlaceDetails = new DelegateCommand<ListviewAggregatedPlace>(PlaceDetails);
            SortDown = new DelegateCommand(TrierUp);
            SortUp = new DelegateCommand(TrierDown);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            FilteredEnreg = new ObservableCollection<ListviewAggregatedPlace>();
            
        }
        		
		private void PlaceDetails(ListviewAggregatedPlace placeSelected)
        {
            var navigationParam = new NavigationParameters();
            navigationParam.Add("ListviewAggregatedPlace", placeSelected);
            NavigationService.NavigateAsync("PlaceDetails", navigationParam);
        }

        private void TrierUp()
        {
            var tmpList = FilteredEnreg.OrderByDescending(x => x.DdataPlace.title).ToList();
            FilteredEnreg.Clear();
            foreach (var el in tmpList)
            {
                FilteredEnreg.Add(el);
            }
        }
        private void TrierDown()
        {
            var tmpList = FilteredEnreg.OrderBy(x => x.DdataPlace.title).ToList();
            FilteredEnreg.Clear();
            foreach (var el in tmpList)
            {
                FilteredEnreg.Add(el);
            }
        }

    }
}

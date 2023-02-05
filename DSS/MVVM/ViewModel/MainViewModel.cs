using DSS.Core;

namespace DSS.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand PilotManagerCommand { get; set; }

        public HomeViewModel HomeVM;
        public PilotManagerViewModel PilotManagerVM;

        public object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set {
                _currentView = value;
                onPropertyChanged();
            }
        }

        public MainViewModel() {
            HomeVM = new HomeViewModel();
            PilotManagerVM = new PilotManagerViewModel();
            _currentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => {
                CurrentView = HomeVM;
            });

            PilotManagerCommand = new RelayCommand(o => {
                CurrentView = PilotManagerVM;
            });
        }
    }
}

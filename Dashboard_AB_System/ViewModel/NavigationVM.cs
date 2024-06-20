using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Dashboard_AB_System.Services;

namespace Dashboard_AB_System.ViewModel
{
    public class NavigationVM : INotifyPropertyChanged
    {
        private readonly UserDataService _userDataService;
        private object _currentView;
        private string _firstName;
        private string _lastName;
        private bool _isMessagesPopupVisible;
        private bool _isSettingsPopupVisible;
        private bool _isPersonalDetailsPopupVisible;
        private bool _isWorksPopupVisible;
        private bool _isDarkMode;
        private bool _isLoggedIn;
        private string _loggedInUsername;

        private Random _random = new Random();

        public NavigationVM()
        {
            _userDataService = new UserDataService();

            // Initialize commands
            HomeCommand = new RelayCommand(Home);
            About_UsCommand = new RelayCommand(About_Us);
            ShowMessagesCommand = new RelayCommand(ToggleMessagesPopup);
            ShowSettingsCommand = new RelayCommand(ToggleSettingsPopup);
            PostCommand = new RelayCommand(Post);
            ShowPersonalDetailsCommand = new RelayCommand(TogglePersonalDetailsPopup);
            ShowWorksCommand = new RelayCommand(ToggleWorksPopup);


            Login("Vren");

        }

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            private set { _isLoggedIn = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; }
        public ICommand About_UsCommand { get; }
        public ICommand ShowMessagesCommand { get; }
        public ICommand ShowSettingsCommand { get; }
        public ICommand PostCommand { get; }
        public ICommand ShowPersonalDetailsCommand { get; }
        public ICommand ShowWorksCommand { get; }
        public void Login(string username)
        {
            // Fetch user details from database using UserDataService
            var userDetails = _userDataService.GetUserByUsername(username);

            FirstName = userDetails.FirstName;
            LastName = userDetails.LastName;

            // Check if user details are valid
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
            {
                IsLoggedIn = true;
                Console.WriteLine($"Logged in as: {username}");
            }
            else
            {
                // Handle authentication failure or user not found
                FirstName = "Unknown";
                LastName = "User";
                IsLoggedIn = false;
                Console.WriteLine($"Authentication failed for: {username}");
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Home(object obj) => CurrentView = new HomeVM();
        private void About_Us(object obj) => CurrentView = new About_UsVM();
        private void Post(object obj) => CurrentView = new PostVM();
        private void ToggleMessagesPopup(object obj) => IsMessagesPopupVisible = !IsMessagesPopupVisible;
        private void ToggleSettingsPopup(object obj) => IsSettingsPopupVisible = !IsSettingsPopupVisible;
        private void TogglePersonalDetailsPopup(object obj) => IsPersonalDetailsPopupVisible = !IsPersonalDetailsPopupVisible;
        private void ToggleWorksPopup(object obj) => IsWorksPopupVisible = !IsWorksPopupVisible;

        public bool IsMessagesPopupVisible
        {
            get => _isMessagesPopupVisible;
            set { _isMessagesPopupVisible = value; OnPropertyChanged(); }
        }

        public bool IsSettingsPopupVisible
        {
            get => _isSettingsPopupVisible;
            set { _isSettingsPopupVisible = value; OnPropertyChanged(); }
        }

        public bool IsPersonalDetailsPopupVisible
        {
            get => _isPersonalDetailsPopupVisible;
            set { _isPersonalDetailsPopupVisible = value; OnPropertyChanged(); }
        }

        public bool IsWorksPopupVisible
        {
            get => _isWorksPopupVisible;
            set { _isWorksPopupVisible = value; OnPropertyChanged(); }
        }

        public bool IsDarkMode
        {
            get => _isDarkMode;
            set { _isDarkMode = value; OnPropertyChanged(); }
        }
    }

    // RelayCommand class to simplify ICommand implementation
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);
    }
}

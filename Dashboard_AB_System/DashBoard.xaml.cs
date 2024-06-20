using Dashboard_AB_System.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace Dashboard_AB_System
{
    public partial class DashBoard : Window
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_MouseEnter(object sender, MouseEventArgs e)
        {
            string popupText = "";
            if (sender == HomeButton)
            {
                popupText = "Home";
            }
            else if (sender == SettingsButton)
            {
                popupText = "Settings";
            }
            else if (sender == AboutUsButton)
            {
                popupText = "Profile";
            }
            else if (sender == MessagesButton)
            {
                popupText = "Messages";
            }
            else if (sender == PostButton)
            {
                popupText = "New Post";
            }





            // Update the text of the popup
            PopupControl.PopupText.Text = popupText;

            // Calculate the position of the button
            var button = sender as FrameworkElement;
            var buttonPosition = button.TranslatePoint(new Point(0, button.ActualHeight), this);

            // Calculate the width and height of the popup
            double popupWidth = PopupControl.ActualWidth;
            double popupHeight = PopupControl.ActualHeight;

            // Calculate the left position of the popup to center it horizontally under the button
            double popupLeft = buttonPosition.X - (popupWidth / 2);

            // Calculate the top position of the popup to align it vertically with the button
            double popupTop = buttonPosition.Y;

            // Set the Margin and HorizontalAlignment of the popup to center it horizontally underneath the button
            PopupControl.Margin = new Thickness(popupLeft, popupTop, 0, 0);
            PopupControl.HorizontalAlignment = HorizontalAlignment.Left;

            // Show the popup
            PopupControl.Visibility = Visibility.Visible;
        }

        private void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
            // Hide the popup
            PopupControl.Visibility = Visibility.Collapsed;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text;
            // Implement search functionality here
            MessageBox.Show($"Searching for: {query}");
        }

    }
}
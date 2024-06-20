using Dashboard_AB_System.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard_AB_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new NavigationVM();
        }


        public void SetCredentials(string username, string password)
        {
            username_txtBox.Text = username;
            password_txtBox.Text = password;
        }

        private void signup_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Registration registration = new Registration();
            registration.SetLoginWindow(this);
            registration.Show();
            Close();
        }

        private void Sign_in_Btn_Click(object sender, RoutedEventArgs e)
        {
            string username = username_txtBox.Text;
            string inputPassword = password_txtBox.Text;

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ArchiSpiresDBEntities"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT FirstName, LastName, Role FROM Users WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", inputPassword);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string firstName = reader["FirstName"].ToString();
                                string lastName = reader["LastName"].ToString();
                                string role = reader["Role"].ToString();

                                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                                // Set user information
                                var navigationVM = ((NavigationVM)this.DataContext);
                                navigationVM.FirstName = firstName;
                                navigationVM.LastName = lastName;

                                // Navigate based on the user's role
                                if (role == "Student")
                                {
                                    Student_DashBoard studentWindow = new Student_DashBoard();
                                    studentWindow.Show();
                                }
                                else if (role == "Architect")
                                {
                                    DashBoard architectWindow = new DashBoard();
                                    architectWindow.Show();
                                }
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
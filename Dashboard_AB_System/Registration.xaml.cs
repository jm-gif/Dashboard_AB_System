using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dashboard_AB_System
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private MainWindow mainWindow;

        public Registration()
        {
            InitializeComponent();
        }

        public void SetLoginWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Navigate back to login page
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Navigate back to login page
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        private void Sign_up_Btn_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (string.IsNullOrEmpty(firstname_txtBox.Text) ||
                string.IsNullOrEmpty(lastname_txbx.Text) ||
                string.IsNullOrEmpty(email_txtBox.Text) ||
                string.IsNullOrEmpty(password_txtBox.Text) ||
                string.IsNullOrEmpty(confirmpassword_txtBox.Text))
            {
                MessageBox.Show("Please fill all the fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password_txtBox.Text != confirmpassword_txtBox.Text)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string firstName = firstname_txtBox.Text;
            string lastName = lastname_txbx.Text;
            string email = email_txtBox.Text;
            string username = firstName; // Using first name as username
            string password = password_txtBox.Text; // Hash the password before storing in a real application
            string role = Student.IsChecked == true ? "Student" : "Architect"; // Determine the role

            try
            {
                // Retrieve the connection string from App.config
                string connectionString = ConfigurationManager.ConnectionStrings["ArchiSpiresDBEntities"].ConnectionString;


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Users (FirstName, LastName, Email, Username, Password, Role) VALUES (@FirstName, @LastName, @Email, @Username, @Password, @Role)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Role", role);

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);



                            // Create and show the login window
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.SetCredentials(firstName, password);
                            mainWindow.Show();

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

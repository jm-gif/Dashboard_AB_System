using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Dashboard_AB_System.View
{
    /// <summary>
    /// Interaction logic for Post.xaml
    /// </summary>
    public partial class Post : UserControl
    {
        private BitmapImage currentBitmap;
        private readonly PostRepository postRepository;
        private readonly Home home;
        public Post()
        {
            InitializeComponent();
            postRepository = new PostRepository();
            home = new Home();
        }

        private void Border_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    currentBitmap = new BitmapImage(new Uri(file));
                    PreviewImage.Source = currentBitmap; // Show the preview
                    PreviewImage.Visibility = Visibility.Visible;
                    DragDropText.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                currentBitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                PreviewImage.Source = currentBitmap; // Show the preview
                PreviewImage.Visibility = Visibility.Visible;
                DragDropText.Visibility = Visibility.Collapsed;
            }
        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentBitmap != null && !string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                byte[] imageData = ConvertBitmapToByteArray(currentBitmap);
                string description = DescriptionTextBox.Text;

                // Save the post to the database
                postRepository.CreatePost(imageData, description);

                MessageBox.Show("Post saved successfully!");

                // Navigate to HomeWindow after posting
                DashBoard dashboard = new DashBoard();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Please select an image and write a description before posting.");
            }
        }

        private byte[] ConvertBitmapToByteArray(BitmapImage bitmapImage)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        private void DescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrEmpty(DescriptionTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}

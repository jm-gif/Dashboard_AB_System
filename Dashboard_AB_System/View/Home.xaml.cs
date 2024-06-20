using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Dashboard_AB_System.View
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private bool isEnlarged = false;
        private double normalWidth;
        private double normalHeight;
        private double enlargedWidth;
        private double enlargedHeight;

        public ObservableCollection<PostItem> Posts { get; set; }
        private readonly PostRepository postRepository;

        public Home()
        {
            InitializeComponent();
            postRepository = new PostRepository();
            Posts = new ObservableCollection<PostItem>();
            DataContext = this;
            LoadPosts();
        }

        public void LoadPosts()
        {
            Posts.Clear();
            DataTable posts = postRepository.GetAllPosts();
            foreach (DataRow row in posts.Rows)
            {
                byte[] imageBytes = (byte[])row["Image"];
                string description = (string)row["Description"];
                BitmapImage bitmap = ConvertByteArrayToBitmap(imageBytes);
                Posts.Add(new PostItem { Image = bitmap, Description = description, Id = (int)row["Id"] }); // Include Id
            }

            // Log loaded posts
            foreach (PostItem post in Posts)
            {
                Console.WriteLine($"Loaded Post: Description: {post.Description}");
            }
        }

        private BitmapImage ConvertByteArrayToBitmap(byte[] byteArray)
        {
            BitmapImage bitmap = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
            }
            return bitmap;
        }

        // Event handler for the Delete option in the ContextMenu
        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.DataContext is PostItem post)
            {
                // Confirm deletion
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this post?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Remove from ObservableCollection
                    Posts.Remove(post);

                    // Delete from the database
                    postRepository.DeletePost(post.Id);

                    MessageBox.Show("Post deleted successfully!");
                }
            }
        }

        // Event handler for image click to toggle between normal and enlarged sizes
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var image = sender as Image;

            if (!isEnlarged)
            {
                // Store the normal size of the image
                normalWidth = image.ActualWidth;
                normalHeight = image.ActualHeight;

                // Set the scale transform to enlarge the image
                var transform = new ScaleTransform(1.5, 1.5);
                image.LayoutTransform = transform;

                // Set the flag to indicate that the image is enlarged
                isEnlarged = true;

                // Show the description text block
                var parentGrid = image.Parent as Grid;
                if (parentGrid != null && parentGrid.Children.Count > 1)
                {
                    var descriptionTextBlock = parentGrid.Children[1] as TextBlock;
                    if (descriptionTextBlock != null)
                    {
                        descriptionTextBlock.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                // Reset the scale transform to original size
                var transform = new ScaleTransform(1, 1);
                image.LayoutTransform = transform;

                // Set the flag to indicate that the image is normal size
                isEnlarged = false;

                // Hide the description text block
                var parentGrid = image.Parent as Grid;
                if (parentGrid != null && parentGrid.Children.Count > 1)
                {
                    var descriptionTextBlock = parentGrid.Children[1] as TextBlock;
                    if (descriptionTextBlock != null)
                    {
                        descriptionTextBlock.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        public class PostItem
        {
            public int Id { get; set; } // Unique identifier for each post
            public BitmapImage Image { get; set; }
            public string Description { get; set; }
        }
    }
}

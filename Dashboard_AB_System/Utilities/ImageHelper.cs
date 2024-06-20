using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dashboard_AB_System
{
    public static class ImageHelper
    {
        public static readonly DependencyProperty BackgroundImageProperty =
            DependencyProperty.RegisterAttached("BackgroundImage", typeof(ImageSource), typeof(ImageHelper), new PropertyMetadata(null, OnBackgroundImageChanged));

        public static ImageSource GetBackgroundImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(BackgroundImageProperty);
        }

        public static void SetBackgroundImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(BackgroundImageProperty, value);
        }

        private static void OnBackgroundImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Image image && e.NewValue is ImageSource newImage)
            {
                // Logic to handle the background image overlay
                image.Source = newImage;
            }
        }
    }
}

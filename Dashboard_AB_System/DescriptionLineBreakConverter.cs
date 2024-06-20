using System;

using System.Globalization;

using System.Windows.Data;

namespace Dashboard_AB_System
{
    public class DescriptionLineBreakConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string description = value as string;
            if (description == null)
                return value;

            // Split description into lines of 6 to 7 words
            string[] words = description.Split(' ');
            string result = "";
            int count = 0;
            foreach (string word in words)
            {
                result += word + " ";
                count++;
                if (count >= 6 && count % 7 == 0)
                {
                    result += Environment.NewLine;
                }
            }
            return result.Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Globalization;

// Converters 
namespace FreakzDEV.JsonVisualizer
{

  public class FontSizeConverter : IValueConverter
  {
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      return value switch
      {
        "Small" => 12,
        "Medium" => 16,
        "Large" => 20,
        "Title" => 24,
        _ => 14,
      };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }

  public class FontAttributesConverter : IValueConverter
  {
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      return value switch
      {
        "Bold" => FontAttributes.Bold,
        "Italic" => FontAttributes.Italic,
        _ => FontAttributes.None,
      };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }

  public class MarginConverter : IValueConverter
  {
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      if (value is int top)
      {
        return new Thickness(0, top, 0, 0);
      }

      return Thickness.Zero;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
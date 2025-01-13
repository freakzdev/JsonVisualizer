using System.Text.Json;
using System.Collections.ObjectModel;

namespace FreakzDEV.JsonVisualizer
{
  public class Collection : CollectionView
  {

    // File Bindable Property
    public static readonly BindableProperty FileProperty = BindableProperty.Create(
        nameof(File),
        typeof(string),
        typeof(Collection),
        string.Empty,
        propertyChanged: OnFileChanged
    );

    public string File
    {
      get => (string)GetValue(FileProperty);
      set => SetValue(FileProperty, value);
    }

    private static async void OnFileChanged(BindableObject bindable, object oldValue, object newValue)
    {
      var control = (Collection)bindable;
      control.ItemsSource = await control.LoadAsync();
    }

    // Class constructor
    public Collection()
    {
      ItemTemplate = new DataTemplate(Build);
    }

    // Component builder
    private static ContentView Build()
    {
      Label label = new()
      {
        HorizontalTextAlignment = TextAlignment.Justify
      };

      label.SetBinding(Label.TextProperty, "Text");
      label.SetBinding(Label.FontAttributesProperty, new Binding("Attributes", converter: new FontAttributesConverter()));
      label.SetBinding(Label.FontSizeProperty, new Binding("Size", converter: new FontSizeConverter()));
      label.SetBinding(Label.MarginProperty, new Binding("MarginTop", converter: new MarginConverter()));

      return new ContentView { Content = label };
    }

    // Load Tasks
    private async Task<ObservableCollection<Structure>> LoadAsync()
    {
      using var stream = await FileSystem.OpenAppPackageFileAsync(File);
      using StreamReader reader = new(stream);
      string json = await reader.ReadToEndAsync();
      var data = JsonSerializer.Deserialize<List<Structure>>(json) ?? throw new InvalidOperationException("Resource file is invalid");
      return [.. data];
    }

  }
}
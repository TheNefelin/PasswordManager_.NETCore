# Maui App .NET 9

* Dependencies
```
CommunityToolkit.Maui
CommunityToolkit.Mvvm
```

* Structure
```
/MyMauiApp
│
├── /Models
│   └── MyModel.cs          # Modelos de datos (representan los datos de la API o negocio)
│
├── /ViewModels
│   └── MyViewModel.cs      # ViewModels (lógica de presentación y estado)
│
├── /Views
│   └── MyPage.xaml         # Views (interfaz de usuario)
│   └── MyPage.xaml.cs      # Code-behind de la View (opcional, para lógica específica de UI)
│
├── /Services
│   └── IApiService.cs      # Interfaz del servicio
│   └── ApiService.cs       # Implementación del servicio (llamadas a la API)
│
├── /Utils
│   └── Helpers.cs          # Utilidades o clases de ayuda
│
├── App.xaml                # Configuración de la aplicación
├── App.xaml.cs             # Punto de entrada de la aplicación
├── MauiProgram.cs          # Configuración de la inyección de dependencias
│
└── /Resources
    ├── /Styles             # Estilos globales
    └── /Fonts              # Fuentes personalizadas
```

# MVVM
## 1. Model
```
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```
## 2. ViewModel
```
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Product> _products;

    public MainViewModel()
    {
        LoadProducts();
    }

    private void LoadProducts()
    {
        Products = new ObservableCollection<Product>
        {
            new Product { Name = "Producto 1", Price = 10.0m },
            new Product { Name = "Producto 2", Price = 20.0m },
            new Product { Name = "Producto 3", Price = 30.0m }
        };
    }

    [RelayCommand]
    private void ShowAlert()
    {
        Shell.Current.DisplayAlert("¡Hola!", "Has hecho clic en el botón.", "OK");
    }
}
```
## 3. View
```
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="YourNamespace.MainPage">
    <VerticalStackLayout>
        <CollectionView ItemsSource="{Binding Products}">
            <CollectionView.ItemTemplate>
                <DataTemplate>
                    <VerticalStackLayout>
                        <Label Text="{Binding Name}" />
                        <Label Text="{Binding Price, StringFormat='Precio: {0:C}'}" />
                    </VerticalStackLayout>
                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>
        <Button Text="Mostrar alerta" Command="{Binding ShowAlertCommand}" />
    </VerticalStackLayout>
</ContentPage>
```
## 4. ViewModel Register
```
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Registra el ViewModel y la View
        builder.Services.AddTransientWithShellRoute<MainPage, MainViewModel>("Main");

        return builder.Build();
    }
}
```
## 5. Binding
* **One-Way Binding:** Displays data from the ViewModel in the View.
```
<Label Text="{Binding Name}" />
```
* **Two-Way Binding:** Synchronize data between the View and the ViewModel.
```
<Entry Text="{Binding Name, Mode=TwoWay}" />
```
* **Commands:** Execute actions on the ViewModel from the View.
```
<Button Command="{Binding ShowAlertCommand}" />
```

# Custom Component (A)
## 1. XAML Create a ContentView
```
<ContentView xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="YourNamespace.Components.LoadingContent">
    
    <Grid IsVisible="{Binding IsLoading}"
          BackgroundColor="{StaticResource BackgroundColor80}"
          VerticalOptions="Fill"
          HorizontalOptions="Fill">

        <ActivityIndicator IsRunning="{Binding IsLoading}"
                           Color="{StaticResource Primary}"
                           VerticalOptions="Center"
                           HorizontalOptions="Center"/>
        
        <Grid.GestureRecognizers>
            <TapGestureRecognizer Command="{Binding TapCommand}" />
        </Grid.GestureRecognizers>
    </Grid>

</ContentView>
```
## 2. C# Implement the Code-Behind
```
public partial class LoadingContent : ContentView
{
    public static readonly BindableProperty IsLoadingProperty =
        BindableProperty.Create(
            nameof(IsLoading), typeof(bool), typeof(LoadingContent), false);

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    public static readonly BindableProperty TapCommandProperty =
        BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(LoadingContent));

    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    public LoadingContent()
    {
        InitializeComponent();
    }
}
```
## 3. Use the Component
```
<components:LoadingContent IsLoading="{Binding IsBusy}" TapCommand="{Binding OnTapCommand}" />
```

# Custom Component (B)
## 1. XAML
```
<Grid xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MauiAppAdmin.Components.LoadingComponent"
             IsVisible="{Binding IsLoading}"
             BackgroundColor="{StaticResource BackgroundColor80}"
             VerticalOptions="Fill" HorizontalOptions="Fill"
      >
    
    <ActivityIndicator 
        IsRunning="{Binding IsLoading}" 
        Color="{StaticResource Primary}" 
        VerticalOptions="Center" 
        HorizontalOptions="Center"
    />
    
</Grid>
```
## 2. C#
```
public partial class LoadingComponent : Grid
{
	public LoadingComponent()
	{
		InitializeComponent();

        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += OnGridTapped;
        this.GestureRecognizers.Add(tapGesture);
    }

    private void OnGridTapped(object sender, TappedEventArgs e)
    {
        Console.WriteLine("Se hizo tap en el Grid Loading.");
    }
}
```
## 3. Use
```
<components:LoadingComponent IsVisible="{Binding IsLoading}"/>
```
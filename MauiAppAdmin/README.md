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


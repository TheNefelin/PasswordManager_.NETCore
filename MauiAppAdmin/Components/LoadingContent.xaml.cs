namespace MauiAppAdmin.Components;

public partial class LoadingContent : ContentView
{
    public static readonly BindableProperty IsLoadingProperty =
        BindableProperty.Create(
            propertyName: nameof(IsLoading),
            returnType: typeof(bool),
            declaringType: typeof(LoadingContent),
            defaultValue: false,
            propertyChanged: OnIsLoadingChanged);

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    private static void OnIsLoadingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is LoadingContent control)
        {
            control.Content.IsVisible = (bool)newValue;
        }
    }

    public LoadingContent()
    {
        InitializeComponent();
    }

    private void OnGridTapped(object sender, EventArgs e)
    {
        // Este método se ejecutará cuando se haga clic en el Grid
        Console.WriteLine("Grid Bloqueando");
    }
}

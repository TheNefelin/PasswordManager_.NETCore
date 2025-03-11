namespace MauiAppAdmin.Components;

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
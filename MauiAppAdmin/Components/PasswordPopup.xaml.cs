using CommunityToolkit.Maui.Views;

namespace MauiAppAdmin.Components;

public partial class PasswordPopup : Popup
{
    public event EventHandler<string> PasswordSubmitted;

    public PasswordPopup()
	{
		InitializeComponent();
	}

    private void OnAcceptClicked(object sender, EventArgs e)
    {
        string password = PasswordEntry.Text;
        PasswordSubmitted?.Invoke(this, password); // Envía la contraseña
        Close();
    }
}
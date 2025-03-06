namespace MauiAppAdmin.Components
{
    using System.Windows.Input;
    using Microsoft.Maui.Controls;

    public class TextChangedBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                nameof(Command),
                typeof(ICommand),
                typeof(TextChangedBehavior),
                default(ICommand));


        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry);
            entry.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            base.OnDetachingFrom(entry);
            entry.TextChanged -= OnEntryTextChanged;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Command == null)
            {
                System.Diagnostics.Debug.WriteLine("Command es null en TextChangedBehavior.");
                return;
            }

            if (Command != null && Command.CanExecute(e.NewTextValue))
            {
                Command.Execute(e.NewTextValue);
            }
        }
    }
}

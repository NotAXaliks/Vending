using Avalonia.Controls;
using Avalonia.Interactivity;

namespace VendingDesktop.Views.Modals;

public partial class AddMachineModal : UserControl
{
    public AddMachineModal()
    {
        InitializeComponent();
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        var parent = Parent as Panel;
        parent?.Children.Remove(this);
    }

    private void OnCreateClick(object? sender, RoutedEventArgs e)
    {
        var parent = Parent as Panel;
        parent?.Children.Remove(this);
    }
}
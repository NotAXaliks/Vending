using Avalonia.Controls;
using Avalonia.Interactivity;
using VendingDesktop.ViewModels;

namespace VendingDesktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void UserMenuButton_OnPointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        MenuPopup.IsOpen = !MenuPopup.IsOpen;
    }

    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}
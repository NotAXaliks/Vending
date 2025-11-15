using Avalonia.Controls;
using Avalonia.Interactivity;

namespace VendingDesktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(); // <-- вот здесь
    }

    private void UserMenuButton_OnPointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        MenuPopup.IsOpen = !MenuPopup.IsOpen;
    }

    private void PageListButtonClick(object? sender, RoutedEventArgs e)
    {
        PageList.IsPaneOpen = !PageList.IsPaneOpen;
    }
}
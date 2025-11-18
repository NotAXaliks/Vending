using Avalonia.Controls;
using Avalonia.Input;
using VendingDesktop.ViewModels;

namespace VendingDesktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void UserMenuOpened(object? sender, PointerPressedEventArgs e)
    {
        MenuPopup.IsOpen = !MenuPopup.IsOpen;
    }
    
    private void PageSelected(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border && border.DataContext is PageListItemTemplate item)
        {
            if (item.HasChildren) item.Toggle();
            else (DataContext as MainWindowViewModel)?.SelectPage(item);
        }
    }
}
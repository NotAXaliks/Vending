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

    private void UserMenuButton_OnPointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        MenuPopup.IsOpen = !MenuPopup.IsOpen;
    }
    
    private void PageItem_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border && border.DataContext is PageListItemTemplate item)
        {
            if (item.HasChildren) item.Toggle();
            else (DataContext as MainWindowViewModel)?.SelectPage(item);
        }
    }

    private void ChildPageItem_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border && border.DataContext is PageListItemTemplate child)
        {
            (DataContext as MainWindowViewModel)?.SelectPage(child);
        }
    }
}
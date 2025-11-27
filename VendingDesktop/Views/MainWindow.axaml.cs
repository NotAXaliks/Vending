using System;
using Avalonia.Controls;
using Avalonia.Input;
using VendingDesktop.ViewModels;

namespace VendingDesktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnAccountPopupClosed(object? sender, EventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
            vm.IsAccountPageListOpen = false;
    }

    private void OpenAccountPopup(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
            vm.IsAccountPageListOpen = true;
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
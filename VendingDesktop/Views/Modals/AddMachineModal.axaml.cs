using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using VendingDesktop.ViewModels;

namespace VendingDesktop.Views.Modals;

public partial class AddMachineModal : UserControl
{
    public required Action OnClose;

    public AddMachineModal()
    {
        InitializeComponent();
        DataContext = new AddMachineModalViewModel();
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        CloseModal();
    }

    private void OnCreateClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is AddMachineModalViewModel vm)
        {
            var errors = vm.GetErrors();
            if (errors.Count != 0)
            {
                ErrorsBlock.Text = string.Join(Environment.NewLine, errors);
                return;
            }
        }

        CloseModal();
    }

    private void CloseModal()
    {
        if (Parent is Panel panel) panel.Children.Remove(this);

        OnClose.Invoke();
    }
}
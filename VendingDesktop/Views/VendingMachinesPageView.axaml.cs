using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using VendingDesktop.Dtos;
using VendingDesktop.services;
using VendingDesktop.ViewModels;
using VendingDesktop.Views.Modals;

namespace VendingDesktop.Views;

public partial class VendingMachinesPageView : UserControl
{
    private System.Timers.Timer? _debounceTimer;

    public VendingMachinesPageView()
    {
        InitializeComponent();

        Refresh();
    }

    public async void Refresh()
    {
        if (DataContext is VendingMachinesPageViewModel viewModel)
        {
            var machinesResponse = await MachinesService.GetMachines(new GetMachinesRequest
                { Show = viewModel.PageSize, Page = viewModel.Page, Filter = viewModel.Filter });
            if (machinesResponse.Data == null) return;

            viewModel.UpdateMachines(machinesResponse.Data.Machines, machinesResponse.Data.FoundCount,
                machinesResponse.Data.TotalCount);
        }
    }

    private void OnSetPageSize(object? sender, SelectionChangedEventArgs e)
    {
        Refresh();
    }

    private void OnPreviousPageClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is VendingMachinesPageViewModel viewModel)
        {
            if (viewModel.Page <= 1) return;

            viewModel.Page -= 1;

            Refresh();
        }
    }

    private void OnNextPageClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is VendingMachinesPageViewModel viewModel)
        {
            if (viewModel.Page >= viewModel.TotalPages) return;

            viewModel.Page += 1;

            Refresh();
        }
    }

    private void OnFilterChanged(object? sender, TextChangedEventArgs e)
    {
        _debounceTimer?.Stop();
        _debounceTimer = new System.Timers.Timer(300) { AutoReset = false };
        _debounceTimer.Elapsed += (_, _) => { Dispatcher.UIThread.Post(Refresh); };
        _debounceTimer.Start();
    }

    private void OnCreateMachineClick(object? sender, RoutedEventArgs e)
    {
        var addMachine = new AddMachineModal()
        {
            OnClose = () => Refresh(),
        };
        MainGrid.Children.Add(addMachine);
    }
}
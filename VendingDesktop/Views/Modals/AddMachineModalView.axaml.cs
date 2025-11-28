using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using VendingDesktop.Dtos;
using VendingDesktop.services;
using VendingDesktop.ViewModels;

namespace VendingDesktop.Views.Modals;

public partial class AddMachineModalView : UserControl
{
    public required Action OnClose;

    public AddMachineModalView()
    {
        InitializeComponent();
        DataContext = new AddMachinesModalViewModel();
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        CloseModal();
    }

    private async void OnCreateClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is AddMachinesModalViewModel vm)
        {
            var errors = vm.GetErrors();
            if (errors.Count != 0)
            {
                ErrorsBlock.Text = string.Join(Environment.NewLine, errors);
                return;
            }

            var parsedManufactureDate = DateTimeOffset.Parse(vm.ManufactureDateString);
            DateTimeOffset? parsedNextMaintenanceDate = string.IsNullOrWhiteSpace(vm.NextMaintenanceDateString)
                ? null
                : DateTimeOffset.Parse(vm.NextMaintenanceDateString);

            var machinesResponse = await MachinesService.CreateMachine(new CreateMachineRequest
            {
                ModelId = vm.SelectedModel!.Id,
                Name = vm.Name,
                Location = vm.Location,
                Address = vm.Address,
                Coordinates = vm.Coordinates,
                PaymentTypes = vm.SelectedPaymentOptions.Select(p => p.Type).ToArray(),
                SerialNumber = vm.SerialNumber,
                InventoryNumber = vm.InventoryNumber,
                Modem = vm.Modem,
                WorkTime = vm.WorkTime,
                Timezone = vm.SelectedTimezone.Timezone,
                Priority = vm.SelectedPriority.Priority,
                WorkMode = vm.SelectedWorkMode.Mode,
                Notes = vm.Notes,
                ManufactureDate = new DateTimeOffset(parsedManufactureDate.Year, parsedManufactureDate.Month,
                    parsedManufactureDate.Day, 0, 0, 0, 0, TimeSpan.Zero).ToUnixTimeMilliseconds(),
                NextMaintenanceDate = parsedNextMaintenanceDate == null
                    ? null
                    : new DateTimeOffset(parsedNextMaintenanceDate.Value.Year,
                        parsedNextMaintenanceDate.Value.Year,
                        parsedNextMaintenanceDate.Value.Year, 0, 0, 0, 0, TimeSpan.Zero).ToUnixTimeMilliseconds(),
            });
            if (!machinesResponse.IsSuccess) return;
        }

        CloseModal();
    }

    private void CloseModal()
    {
        if (Parent is Panel panel) panel.Children.Remove(this);

        OnClose.Invoke();
    }
}
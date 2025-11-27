using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using VendingDesktop.Dtos;

namespace VendingDesktop.ViewModels;

public partial class VendingMachinesPageViewModel : ViewModelBase
{
    [ObservableProperty] private int _page = 1;
    [ObservableProperty] private int _pageSize = 25;
    [ObservableProperty] private string _filter;
    [ObservableProperty] private int _foundCount;
    [ObservableProperty] private int _totalMachines;

    public ObservableCollection<MachineWithIndexItemTemplate> Machines { get; set; } = [];

    public int[] PageSizeOptions { get; } = [10, 25, 50, 100];

    public int TotalPages => (int)Math.Ceiling((double)TotalMachines / PageSize);

    public string TotalFoundText => $"Всего найдено: {TotalMachines} шт.";

    public string PaginationText =>
        $"Записи с {(Page - 1) * PageSize + 1} до {Math.Min(Page * PageSize, FoundCount)} из {FoundCount} записей";

    public void UpdateMachines(MachineWithModelDto[] machines, int foundMachines, int totalMachines)
    {
        FoundCount = foundMachines;
        TotalMachines = totalMachines;

        Machines.Clear();

        var machineItems = machines.Select((machine, i) => new MachineWithIndexItemTemplate(
            machine.Id,
            machine.Name,
            machine.Model.Name,
            machine.Model.Manufacturer,
            machine.Modem ?? "-1",
            machine.Location + " " + machine.Address,
            DateTimeOffset.FromUnixTimeMilliseconds(machine.StartDate).ToString("dd.MM.yyyy"),
            i
        ));

        foreach (var item in machineItems)
        {
            Machines.Add(item);
        }
    }
}

public record MachineWithIndexItemTemplate(
    int Id,
    string Name,
    string Model,
    string Manufacturer,
    string Modem,
    string Location,
    string StartDate,
    int Index);
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VendingDesktop.Dtos;

namespace VendingDesktop.ViewModels;

public class VendingMachinesPageViewModel : ViewModelBase
{
    private int _page = 1;
    private int _pageSize = 25;
    private string _filter;
    private int _foundCount;
    private int _totalMachines;

    public ObservableCollection<MachineWithIndexItemTemplate> Machines { get; set; } = [];

    public int[] PageSizeOptions { get; } = [10, 25, 50, 100];

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (_pageSize != value)
            {
                _pageSize = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PaginationText));
            }
        }
    }

    public int Page
    {
        get => _page;
        set
        {
            if (_page != value)
            {
                _page = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PaginationText));
            }
        }
    }

    public int FoundCount
    {
        get => _foundCount;
        set
        {
            if (_foundCount != value)
            {
                _foundCount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PaginationText));
            }
        }
    }

    public int TotalMachines
    {
        get => _totalMachines;
        set
        {
            if (_totalMachines != value)
            {
                _totalMachines = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalFoundText));
            }
        }
    }

    public string Filter
    {
        get => _filter;
        set
        {
            if (_filter != value)
            {
                _filter = value;
                OnPropertyChanged();
            }
        }
    }
    
    public int TotalPages => (int)Math.Ceiling((double)TotalMachines / PageSize);

    public string TotalFoundText => $"Всего найдено: {TotalMachines} шт.";

    public string PaginationText => $"Записи с {(Page - 1) * PageSize + 1} до {Math.Min(Page * PageSize, FoundCount)} из {FoundCount} записей";

    public void UpdateMachines(MachineDto[] machines, int foundMachines, int totalMachines)
    {
        FoundCount = foundMachines;
        TotalMachines = totalMachines;

        Machines.Clear();

        var machineItems = machines.Select((machine, i) => new MachineWithIndexItemTemplate(
            machine.Id,
            machine.Name,
            machine.Model,
            machine.Manufacturer,
            machine.Modem,
            machine.Location,
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
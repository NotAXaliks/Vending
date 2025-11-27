using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using VendingDesktop.Dtos;

namespace VendingDesktop.ViewModels;

public partial class AddMachinesModalViewModel : ViewModelBase
{
    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _manufacturer = string.Empty;
    [ObservableProperty] private string _workMode = string.Empty;
    [ObservableProperty] private string _address = string.Empty;
    [ObservableProperty] private string _location = string.Empty;
    [ObservableProperty] private string _coordinates = string.Empty;
    [ObservableProperty] private string _machineNumber = string.Empty;
    [ObservableProperty] private string _workTime = "Стандартный";
    [ObservableProperty] private string _timezone = "UTC +3:00";
    [ObservableProperty] private string _productMatrix = string.Empty;
    [ObservableProperty] private string _rfidInspectionCard = string.Empty;
    [ObservableProperty] private string _rfidCollectionCard = string.Empty;
    [ObservableProperty] private string _rfidLoadingCard = string.Empty;
    [ObservableProperty] private string _kitOnlineId = string.Empty;
    [ObservableProperty] private string _priority = "Средний";
    [ObservableProperty] private string _notes = string.Empty;
    [ObservableProperty] private ModelOptionTemplate? _selectedModel = null;
    
    public string[] ManufacturerOptions { get; set; } = ["Manufacturer1", "Manufacturer2", "Manufacturer3"];
    public ObservableCollection<ModelOptionTemplate> ModelOptions { get; set; } = [new("Model1", 1), new("Model2", 2)];
    public string[] WorkModeOptions { get; set; } = ["Стандартный"];
    public string[] ProductMatrixOptions { get; set; } = ["ProductMatrix1", "ProductMatrix2", "ProductMatrix3"];
    public string[] PriorityOptions { get; set; } = ["Высокий", "Средний", "Низкий"];

    public string[] TimezoneOptions { get; set; } =
    [
        "UTC -12:00", "UTC -11:00", "UTC -10:00", "UTC -9:00", "UTC -8:00", "UTC -7:00", "UTC -6:00", "UTC -5:00",
        "UTC -4:00", "UTC -3:00", "UTC -2:00", "UTC -1:00", "UTC 0:00", "UTC +1:00", "UTC +2:00", "UTC +3:00",
        "UTC +4:00", "UTC +5:00", "UTC +6:00", "UTC +7:00", "UTC +8:00", "UTC +9:00", "UTC +10:00", "UTC +11:00",
        "UTC +12:00"
    ];

    public ObservableCollection<PaymentOptionTemplate> PaymentOptions { get; set; } =
        [new("Монетопр."), new("Купюропр."), new("Модель б/н опл."), new("QR-платежи")];
    
    public MachineWorkMode WorkModeToEnum(string workModeString)
    {
        if (workModeString == WorkModeOptions[0]) return MachineWorkMode.Standart;

        return MachineWorkMode.Standart;
    }

    public MachinePriority PriorityToEnum(string priorityString)
    {
        if (priorityString == PriorityOptions[0]) return MachinePriority.High;
        if (priorityString == PriorityOptions[1]) return MachinePriority.Medium;
        if (priorityString == PriorityOptions[2]) return MachinePriority.Low;

        return MachinePriority.Medium;
    }

    public MachineTimezone TimezoneToEnum(string timezoneString)
    {
        if (timezoneString == TimezoneOptions[0]) return MachineTimezone.UTC_12;
        if (timezoneString == TimezoneOptions[1]) return MachineTimezone.UTC_11;
        if (timezoneString == TimezoneOptions[2]) return MachineTimezone.UTC_10;
        if (timezoneString == TimezoneOptions[3]) return MachineTimezone.UTC_9;
        if (timezoneString == TimezoneOptions[4]) return MachineTimezone.UTC_8;
        if (timezoneString == TimezoneOptions[5]) return MachineTimezone.UTC_7;
        if (timezoneString == TimezoneOptions[6]) return MachineTimezone.UTC_6;
        if (timezoneString == TimezoneOptions[7]) return MachineTimezone.UTC_5;
        if (timezoneString == TimezoneOptions[8]) return MachineTimezone.UTC_4;
        if (timezoneString == TimezoneOptions[9]) return MachineTimezone.UTC_3;
        if (timezoneString == TimezoneOptions[10]) return MachineTimezone.UTC_2;
        if (timezoneString == TimezoneOptions[11]) return MachineTimezone.UTC_1;
        if (timezoneString == TimezoneOptions[12]) return MachineTimezone.UTC_0;
        if (timezoneString == TimezoneOptions[13]) return MachineTimezone.UTC1;
        if (timezoneString == TimezoneOptions[14]) return MachineTimezone.UTC2;
        if (timezoneString == TimezoneOptions[15]) return MachineTimezone.UTC3;
        if (timezoneString == TimezoneOptions[16]) return MachineTimezone.UTC4;
        if (timezoneString == TimezoneOptions[17]) return MachineTimezone.UTC5;
        if (timezoneString == TimezoneOptions[18]) return MachineTimezone.UTC6;
        if (timezoneString == TimezoneOptions[19]) return MachineTimezone.UTC7;
        if (timezoneString == TimezoneOptions[20]) return MachineTimezone.UTC8;
        if (timezoneString == TimezoneOptions[21]) return MachineTimezone.UTC9;
        if (timezoneString == TimezoneOptions[22]) return MachineTimezone.UTC10;
        if (timezoneString == TimezoneOptions[23]) return MachineTimezone.UTC11;
        if (timezoneString == TimezoneOptions[24]) return MachineTimezone.UTC12;

        // Отправляем по умолчанию UTC+3
        return MachineTimezone.UTC3;
    }

    public List<string> GetErrors()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Name)) errors.Add("Поле \"Название ТА\" должно быть указано");
        if (string.IsNullOrWhiteSpace(Manufacturer)) errors.Add("Поле \"Производитель ТА\" должно быть указано");
        // if (string.IsNullOrWhiteSpace(Model)) errors.Add("Поле \"Модель ТА\" должно быть указано");
        if (string.IsNullOrWhiteSpace(WorkTime)) errors.Add("Поле \"Режим работы\" должно быть указано");
        if (string.IsNullOrWhiteSpace(Address)) errors.Add("Поле \"Адрес\" должно быть указано");
        if (string.IsNullOrWhiteSpace(Location)) errors.Add("Поле \"Место\" должно быть указано");
        if (string.IsNullOrWhiteSpace(MachineNumber)) errors.Add("Поле \"Номер автомата\" должно быть указано");
        if (string.IsNullOrWhiteSpace(Timezone)) errors.Add("Поле \"Часовой пояс\" должно быть указано");
        if (string.IsNullOrWhiteSpace(ProductMatrix)) errors.Add("Поле \"Товарная матрица\" должно быть указано");
        if (!PaymentOptions.Any(p => p.IsChecked))
            errors.Add("Хотя бы один элемент из поля \"Платежные системы\" должен быть выбран!");
        if (string.IsNullOrWhiteSpace(Priority)) errors.Add("Поле \"Приоритет обслуживания\" должно быть указано");

        return errors;
    }
}

public record PaymentOptionTemplate(string Name, bool IsChecked = false);

public record ModelOptionTemplate(string Name, int ModelId, bool IsSelected = false);
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using VendingDesktop.Dtos;

namespace VendingDesktop.ViewModels;

public partial class AddMachinesModalViewModel : ViewModelBase
{
    [ObservableProperty] private string _name = string.Empty;
    [ObservableProperty] private string _manufacturer = string.Empty;
    [ObservableProperty] private string _address = string.Empty;
    [ObservableProperty] private string _location = string.Empty;
    [ObservableProperty] private string _coordinates = string.Empty;
    [ObservableProperty] private string _inventoryNumber = string.Empty;
    [ObservableProperty] private string _serialNumber = string.Empty;
    [ObservableProperty] private string _workTime = string.Empty;
    [ObservableProperty] private string _productMatrix = string.Empty;
    [ObservableProperty] private string _rfidInspectionCard = string.Empty;
    [ObservableProperty] private string _rfidCollectionCard = string.Empty;
    [ObservableProperty] private string _rfidLoadingCard = string.Empty;
    [ObservableProperty] private string _kitOnlineId = string.Empty;
    [ObservableProperty] private string _modem = string.Empty;
    [ObservableProperty] private string _notes = string.Empty;

    [ObservableProperty] private PriorityOptionTemplate _selectedPriority = PriorityOptions[1];
    [ObservableProperty] private TimezoneOptionTemplate _selectedTimezone = TimezoneOptions[15];
    [ObservableProperty] private WorkModeOptionTemplate _selectedWorkMode = WorkModeOptions[0];
    [ObservableProperty] private ModelOptionTemplate? _selectedModel = null;
    [ObservableProperty] private PaymentOptionTemplate? _selectedPaymentOption = null;

    [ObservableProperty] private string _currentDate = DateTimeOffset.Now.ToString("dd.MM.yyyy");
    [ObservableProperty] private string? _nextMaintenanceDateString;

    [ObservableProperty]
    private string _manufactureDateString = DateTimeOffset.Now.AddMonths(-1).ToString("dd.MM.yyyy");

    public string[] ManufacturerOptions { get; set; } = ["Manufacturer1", "Manufacturer2", "Manufacturer3"];
    public ObservableCollection<ModelOptionTemplate> ModelOptions { get; set; } = [new("Model1", 1), new("Model2", 2)];
    public string[] ProductMatrixOptions { get; set; } = ["ProductMatrix1", "ProductMatrix2", "ProductMatrix3"];

    public static ObservableCollection<PriorityOptionTemplate> PriorityOptions { get; set; } =
    [
        new("Высокий", MachinePriority.High), new("Средний", MachinePriority.Medium), new("Низкий", MachinePriority.Low)
    ];

    public static ObservableCollection<TimezoneOptionTemplate> TimezoneOptions { get; set; } =
    [
        new("UTC -12:00", MachineTimezone.UTC_12), new("UTC -11:00", MachineTimezone.UTC_11),
        new("UTC -10:00", MachineTimezone.UTC_10), new("UTC -9:00", MachineTimezone.UTC_9),
        new("UTC -8:00", MachineTimezone.UTC_8), new("UTC -7:00", MachineTimezone.UTC_7),
        new("UTC -6:00", MachineTimezone.UTC_6), new("UTC -5:00", MachineTimezone.UTC_5),
        new("UTC -4:00", MachineTimezone.UTC_4), new("UTC -3:00", MachineTimezone.UTC_3),
        new("UTC -2:00", MachineTimezone.UTC_2), new("UTC -1:00", MachineTimezone.UTC_1),
        new("UTC", MachineTimezone.UTC), new("UTC +1:00", MachineTimezone.UTC1),
        new("UTC +2:00", MachineTimezone.UTC2), new("UTC +3:00", MachineTimezone.UTC3),
        new("UTC +4:00", MachineTimezone.UTC4), new("UTC +5:00", MachineTimezone.UTC5),
        new("UTC +6:00", MachineTimezone.UTC6), new("UTC +7:00", MachineTimezone.UTC7),
        new("UTC +8:00", MachineTimezone.UTC8), new("UTC +9:00", MachineTimezone.UTC9),
        new("UTC +10:00", MachineTimezone.UTC10), new("UTC +11:00", MachineTimezone.UTC11),
        new("UTC +12:00", MachineTimezone.UTC12),
    ];

    public static ObservableCollection<WorkModeOptionTemplate> WorkModeOptions { get; set; } =
    [
        new("Стандартный", MachineWorkMode.Standart),
    ];

    public static ObservableCollection<PaymentOptionTemplate> PaymentOptions { get; set; } =
    [
        new("Монетопр.", MachinePaymentType.Coins), new("Купюропр.", MachinePaymentType.Bill),
        new("Модель б/н опл.", MachinePaymentType.Card), new("QR-платежи", MachinePaymentType.QR)
    ];

    public List<PaymentOptionTemplate> SelectedPaymentOptions => PaymentOptions.Where(p => p.IsChecked).ToList();

    public List<string> GetErrors()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Name)) errors.Add("Поле \"Название ТА\" должно быть указано");
        if (string.IsNullOrWhiteSpace(Manufacturer)) errors.Add("Поле \"Производитель ТА\" должно быть указано");
        if (SelectedModel == null) errors.Add("Поле \"Модель ТА\" должно быть указано");
        if (string.IsNullOrWhiteSpace(Address)) errors.Add("Поле \"Адрес\" должно быть указано");
        if (string.IsNullOrWhiteSpace(Location)) errors.Add("Поле \"Место\" должно быть указано");
        if (string.IsNullOrWhiteSpace(InventoryNumber)) errors.Add("Поле \"Инвентарный номер\" должно быть указано");
        if (string.IsNullOrWhiteSpace(SerialNumber)) errors.Add("Поле \"Серийный номер\" должно быть указано");
        if (string.IsNullOrWhiteSpace(ManufactureDateString))
            errors.Add("Поле \"Дата производства\" должно быть указано");
        if (string.IsNullOrWhiteSpace(ProductMatrix)) errors.Add("Поле \"Товарная матрица\" должно быть указано");
        if (!PaymentOptions.Any(p => p.IsChecked))
            errors.Add("Хотя бы один элемент из поля \"Платежные системы\" должен быть выбран!");

        if (!string.IsNullOrWhiteSpace(WorkTime) &&
            !Regex.IsMatch(WorkTime, @"(?:[01]\d|2[0-3]):[0-5]\d-[0-5]\d:[0-5]\d"))
            errors.Add("Поле \"Время работы\" должно быть в формате чч:мм-чч:мм");

        return errors;
    }
}

public record PriorityOptionTemplate(string Name, MachinePriority Priority);

public record TimezoneOptionTemplate(string Name, MachineTimezone Timezone);

public record WorkModeOptionTemplate(string Name, MachineWorkMode Mode);

public record PaymentOptionTemplate(string Name, MachinePaymentType Type, bool IsChecked = false);

public record ModelOptionTemplate(string Name, int Id, bool IsSelected = false);
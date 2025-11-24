using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VendingDesktop.ViewModels;

public class AddMachineModalViewModel : ViewModelBase
{
    private string _name = string.Empty;
    private string _manufacturer = string.Empty;
    private string _model = string.Empty;
    private string _workMode = string.Empty;
    private string _address = string.Empty;
    private string _location = string.Empty;
    private string _machineNumber = string.Empty;
    private string _workTime = string.Empty;
    private string _timezone = "UTC +3:00";
    private string _productMatrix = string.Empty;
    private string _rfidInspectionCard = string.Empty;
    private string _rfidCollectionCard = string.Empty;
    private string _rfidLoadingCard = string.Empty;
    private string _kitOnlineId = string.Empty;
    private string _priority = string.Empty;
    private string _notes = string.Empty;

    public string[] ManufacturerOptions { get; set; } = ["Manufacturer1", "Manufacturer2", "Manufacturer3"];
    public string[] ModelOptions { get; set; } = ["Model1", "Model2", "Model3"];
    public string[] WorkModeOptions { get; set; } = ["WorkMode1", "WorkMode2", "WorkMode3"];
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
    
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Manufacturer
    {
        get => _manufacturer;
        set => SetProperty(ref _manufacturer, value);
    }

    public string Model
    {
        get => _model;
        set => SetProperty(ref _model, value);
    }

    public string WorkMode
    {
        get => _workMode;
        set => SetProperty(ref _workMode, value);
    }

    public string Address
    {
        get => _address;
        set => SetProperty(ref _address, value);
    }

    public string Location
    {
        get => _location;
        set => SetProperty(ref _location, value);
    }

    public string MachineNumber
    {
        get => _machineNumber;
        set => SetProperty(ref _machineNumber, value);
    }

    public string WorkTime
    {
        get => _workTime;
        set => SetProperty(ref _workTime, value);
    }

    public string Timezone
    {
        get => _timezone;
        set => SetProperty(ref _timezone, value);
    }

    public string ProductMatrix
    {
        get => _productMatrix;
        set => SetProperty(ref _productMatrix, value);
    }

    public string RFIDInspectionCard
    {
        get => _rfidInspectionCard;
        set => SetProperty(ref _rfidInspectionCard, value);
    }

    public string RFIDCollectionCard
    {
        get => _rfidCollectionCard;
        set => SetProperty(ref _rfidCollectionCard, value);
    }

    public string RFIDLoadingCard
    {
        get => _rfidLoadingCard;
        set => SetProperty(ref _rfidLoadingCard, value);
    }

    public string KitOnlineId
    {
        get => _kitOnlineId;
        set => SetProperty(ref _kitOnlineId, value);
    }

    public string Priority
    {
        get => _priority;
        set => SetProperty(ref _priority, value);
    }

    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }

    public List<string> GetErrors()
    {
        var errors = new List<string>();
        
        if (string.IsNullOrWhiteSpace(Name)) errors.Add("Поле \"Название ТА\" должно быть указано");
        if (string.IsNullOrWhiteSpace(Manufacturer)) errors.Add("Поле \"Производитель ТА\" должно быть указано");
        if (string.IsNullOrWhiteSpace(Model)) errors.Add("Поле \"Модель ТА\" должно быть указано");
        if (string.IsNullOrWhiteSpace(WorkTime)) errors.Add("Поле \"Режим работы\" должно быть указано");
        if (string.IsNullOrWhiteSpace(Address)) errors.Add("Поле \"Адрес\" должно быть указано");
        if (string.IsNullOrWhiteSpace(Location)) errors.Add("Поле \"Место\" должно быть указано");
        if (string.IsNullOrWhiteSpace(MachineNumber)) errors.Add("Поле \"Номер автомата\" должно быть указано");
        if (string.IsNullOrWhiteSpace(Timezone)) errors.Add("Поле \"Часовой пояс\" должно быть указано");
        if (string.IsNullOrWhiteSpace(ProductMatrix)) errors.Add("Поле \"Товарная матрица\" должно быть указано");
        if (!PaymentOptions.Any(p => p.IsChecked)) errors.Add("Хотя бы один элемент из поля \"Платежные системы\" должен быть выбран!");
        if (string.IsNullOrWhiteSpace(Priority)) errors.Add("Поле \"Приоритет обслуживания\" должно быть указано");

        return errors;
    }
}

public record PaymentOptionTemplate(string Name, bool IsChecked = false);
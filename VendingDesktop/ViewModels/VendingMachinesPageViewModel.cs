using System.Collections.ObjectModel;
using System.Linq;

namespace VendingDesktop.ViewModels;

public class VendingMachinesPageViewModel : ViewModelBase
{
    public ObservableCollection<MachineWithInexItemTemplate> Machines { get; set; }

    public VendingMachinesPageViewModel()
    {
        var baseMachines = new ObservableCollection<MachineItemTemplate>([
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
        ]);

        Machines = new ObservableCollection<MachineWithInexItemTemplate>(
            baseMachines.Select((machine, i) => new MachineWithInexItemTemplate(machine, i))
        );
    }
}

public record MachineItemTemplate(
    int Id,
    string Name,
    string Model,
    string Manufacturer,
    string Modem,
    string Location,
    string InWorkSince);

public record MachineWithInexItemTemplate(MachineItemTemplate Machine, int Index);
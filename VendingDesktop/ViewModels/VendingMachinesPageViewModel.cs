using System.Collections.ObjectModel;

namespace VendingDesktop.ViewModels;

public class VendingMachinesPageViewModel : ViewModelBase
{
    public ObservableCollection<MachineItemTemplate> Machines { get; set; } =
        [
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
            new MachineItemTemplate(903823, "БЦ <Московский>", "Saeco Cristallo 400", "380649 \"ООО Торговые Аппараты\"", "1824100025", "Суворова 121 у входа", "12.05.2018"),
        ];
}

public class MachineItemTemplate
{
    public MachineItemTemplate(int id, string name, string model, string manufacter, string modem, string location, string inWorkSince)
    {
        Id = id;
        Name = name;
        Model = model;
        Manufacter = manufacter;
        Modem = modem;
        Location = location;
        InWorkSince = inWorkSince;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Manufacter { get; set; }
    public string Modem { get; set; }
    public string Location { get; set; }
    public string InWorkSince { get; set; }
}
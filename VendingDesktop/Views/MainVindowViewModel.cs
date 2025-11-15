using System.Collections.ObjectModel;

namespace VendingDesktop.Views;

public class MenuItem
{
    public string Title { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public bool HasArrow { get; set; } = false;
}

public class MainWindowViewModel
{
    public ObservableCollection<MenuItem> MenuItems { get; } = new ObservableCollection<MenuItem>
    {
        new MenuItem { Title = "Главная", Icon = "avares://VendingDesktop/Assets/Search.svg" },
        new MenuItem { Title = "Монитор ТА", Icon = "avares://VendingDesktop/Assets/Monitor.svg" },
        new MenuItem { Title = "Детальные отчёты", Icon = "avares://VendingDesktop/Assets/Description.svg" },
        new MenuItem { Title = "Учёт ТМЦ", Icon = "avares://VendingDesktop/Assets/Cart.svg", HasArrow = true },
        new MenuItem { Title = "Администрирование", Icon = "avares://VendingDesktop/Assets/AdminSettings.svg" },
    };
}
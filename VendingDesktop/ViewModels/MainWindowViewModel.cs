using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

namespace VendingDesktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private bool _isMenuOpen = true;
    public bool IsMenuOpen
    {
        get => _isMenuOpen;
        set => SetProperty(ref _isMenuOpen, value);
    }

    private ViewModelBase _currentPage = new MainPageViewModel();
    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set => SetProperty(ref _currentPage, value);
    }

    private ListItemTemplate? _selectedListItem;
    public ListItemTemplate? SelectedListItem
    {
        get => _selectedListItem;
        set
        {
            if (SetProperty(ref _selectedListItem, value))
                SelectPage();
        }
    }

    public ObservableCollection<ListItemTemplate> Items { get; set; } =
    [
        new ListItemTemplate("Главная", typeof(MainPageViewModel), "Search"),
        new ListItemTemplate("Монитор ТА", typeof(MonitorPageViewModel), "Monitor")
    ];

    private void SelectPage()
    {
        if (SelectedListItem == null)
            return;

        CurrentPage = (ViewModelBase)Activator.CreateInstance(SelectedListItem.ModelType)!;
    }

    public void ToggleMenuCommand()
    {
        IsMenuOpen = !IsMenuOpen;
    }
}

public class ListItemTemplate
{
    public ListItemTemplate(string label, Type type, string icon)
    {
        ModelType = type;
        Label = label;

        Application.Current!.TryFindResource(icon, out var res);
        Icon = (StreamGeometry)res!;
    }

    public string Label { get; set; }
    public Type ModelType { get; set; }
    public StreamGeometry Icon { get; set; }
}
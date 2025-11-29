using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VendingDesktop.Dtos;

namespace VendingDesktop.ViewModels;

public partial class MainWindowViewModel(UserDto user) : ViewModelBase
{
    [ObservableProperty] private UserDto _user = user;

    [ObservableProperty] private bool _isAccountPageListOpen;

    [ObservableProperty] private bool _isPageListOpen = true;
    
    [ObservableProperty] private ViewModelBase _currentPage = new MainPageViewModel();

    [ObservableProperty] private PageListItemTemplate? _selectedPageListItem;
    
    public ObservableCollection<PageListItemTemplate> Pages { get; set; } =
    [
        new PageListItemTemplate("Главная", "Search", typeof(MainPageViewModel), null),
        new PageListItemTemplate("Монитор ТА", "Monitor", typeof(VendingMachinesPageViewModel), null),
        new PageListItemTemplate("Детальные отчёты", "Description", typeof(VendingMachinesPageViewModel), [
            new PageListItemTemplate("Торговые автоматы", "Description", typeof(VendingMachinesPageViewModel), null),
        ]),
        new PageListItemTemplate("Учёт ТМЦ", "Cart", typeof(VendingMachinesPageViewModel), [
            new PageListItemTemplate("Торговые автоматы", "Description", typeof(VendingMachinesPageViewModel), null),
        ]),
        new PageListItemTemplate("Администрирование", "AdminSettings", null, [
            new PageListItemTemplate("Торговые автоматы", "Description", typeof(VendingMachinesPageViewModel), null),
            new PageListItemTemplate("Компании", "Description", typeof(VendingMachinesPageViewModel), null),
            new PageListItemTemplate("Модемы", "Description", typeof(VendingMachinesPageViewModel), null),
            new PageListItemTemplate("Дополнительные", "Description", typeof(VendingMachinesPageViewModel), null),
        ]),
    ];

    public void SelectPage(PageListItemTemplate item)
    {
        SelectedPageListItem = item;

        if (!item.HasChildren && item.ModelType != null)
            CurrentPage = (ViewModelBase)Activator.CreateInstance(item.ModelType)!;
    }

    [RelayCommand]
    public void TogglePageList() => IsPageListOpen = !IsPageListOpen;
}

public partial class PageListItemTemplate : ViewModelBase
{
    public PageListItemTemplate(string label, string icon, Type? type,
        ObservableCollection<PageListItemTemplate>? children)
    {
        Label = label;
        if (type != null) ModelType = type;
        if (children != null) Children = children;

        Application.Current!.TryFindResource(icon, out var res);
        Icon = (StreamGeometry)res!;
    }

    [ObservableProperty] private string _label;

    [ObservableProperty] private Type? _modelType;

    [ObservableProperty] private StreamGeometry _icon;

    [ObservableProperty] private bool _isExpanded;

    public ObservableCollection<PageListItemTemplate> Children { get; set; } = [];

    public bool HasChildren => Children.Count > 0;
    public void Toggle() => IsExpanded = !IsExpanded;
}
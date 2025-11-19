using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace VendingDesktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private bool _isPageListOpen = true;

    public bool IsMenuOpen
    {
        get => _isPageListOpen;
        set => SetProperty(ref _isPageListOpen, value);
    }

    private bool _isAccountPageListOpen = false;

    public bool IsAccountPageListOpen
    {
        get =>  _isAccountPageListOpen;
        set =>  SetProperty(ref _isAccountPageListOpen, value);
    }

    private ViewModelBase _currentPage = new MainPageViewModel();

    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set => SetProperty(ref _currentPage, value);
    }

    private PageListItemTemplate? _selectedPageListItem;

    public PageListItemTemplate? SelectedPageListItem
    {
        get => _selectedPageListItem;
        private set => SetProperty(ref _selectedPageListItem, value);
    }

    public void SelectPage(PageListItemTemplate item)
    {
        SelectedPageListItem = item;

        if (!item.HasChildren && item.ModelType != null)
            CurrentPage = (ViewModelBase)Activator.CreateInstance(item.ModelType)!;
    }

    private AccountPageListItemTemplate? _selectedAccountPageListItem;

    public AccountPageListItemTemplate? SelectedAccountPageListItem
    {
        get => _selectedAccountPageListItem;
        set
        {
            if (SetProperty(ref _selectedAccountPageListItem, value))
                SelectAccountPage();
        }
    }

    public ObservableCollection<AccountPageListItemTemplate> AccountPages { get; set; } =
    [
        new AccountPageListItemTemplate("Мой профиль", "Account"),
        new AccountPageListItemTemplate("Мои сессии", "Lock"),
        new AccountPageListItemTemplate("Выход", "Logout")
    ];

    public ObservableCollection<PageListItemTemplate> Pages { get; set; } =
    [
        new PageListItemTemplate("Главная", "Search", typeof(MainPageViewModel), null),
        new PageListItemTemplate("Монитор ТА", "Monitor", typeof(MonitorPageViewModel), null),
        new PageListItemTemplate("Детальные отчёты", "Description", typeof(MonitorPageViewModel), [
            new PageListItemTemplate("Торговые автоматы", "Description", typeof(VendingMachinesPageViewModel), null),
        ]),
        new PageListItemTemplate("Учёт ТМЦ", "Cart", typeof(MonitorPageViewModel), [
            new PageListItemTemplate("Торговые автоматы", "Description", typeof(VendingMachinesPageViewModel), null),
        ]),
        new PageListItemTemplate("Администрирование", "AdminSettings", null, [
            new PageListItemTemplate("Торговые автоматы", "Description", typeof(VendingMachinesPageViewModel), null),
            new PageListItemTemplate("Компании", "Description", typeof(VendingMachinesPageViewModel), null),
            new PageListItemTemplate("Модемы", "Description", typeof(VendingMachinesPageViewModel), null),
            new PageListItemTemplate("Дополнительные", "Description", typeof(VendingMachinesPageViewModel), null),
        ]),
    ];

    private void SelectAccountPage()
    {
        if (SelectedPageListItem?.ModelType != null)
            CurrentPage = (ViewModelBase)Activator.CreateInstance(SelectedPageListItem.ModelType)!;
    }

    public void TogglePageListCommand() => IsMenuOpen = !IsMenuOpen;
}

public class AccountPageListItemTemplate
{
    public AccountPageListItemTemplate(string label, string icon)
    {
        Label = label;

        Application.Current!.TryFindResource(icon, out var res);
        Icon = (StreamGeometry)res!;
    }

    public string Label { get; set; }
    public StreamGeometry Icon { get; set; }
}

public class PageListItemTemplate : ViewModelBase
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

    public string Label { get; set; }
    public Type? ModelType { get; set; }
    public StreamGeometry Icon { get; set; }
    public ObservableCollection<PageListItemTemplate> Children { get; set; } = [];

    private bool _isExpanded;

    public bool IsExpanded
    {
        get => _isExpanded;
        set => SetProperty(ref _isExpanded, value);
    }

    public bool HasChildren => Children.Count > 0;
    public void Toggle() => IsExpanded = !IsExpanded;
}
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
        // Закрываем все открытые ветки, кроме текущей ветки
        foreach (var page in Pages)
            CloseAllExcept(page, item);

        SelectedPageListItem = item;

        // Открываем страницу только если нет детей
        if (!item.HasChildren)
        {
            CurrentPage = (ViewModelBase)Activator.CreateInstance(item.ModelType)!;
        }
    }

    private void CloseAllExcept(PageListItemTemplate item, PageListItemTemplate? except)
    {
        if (ReferenceEquals(item, except))
            return;

        item.IsExpanded = false;

        foreach (var child in item.Children)
            CloseAllExcept(child, except);
    }

    private AccountPageListItemTemplate? _selectedAccountPageListItem;

    public AccountPageListItemTemplate? SelectedAccountPageListItem
    {
        get => _selectedAccountPageListItem;
        set
        {
            if (SetProperty(ref _selectedAccountPageListItem, value))
                SelectPage();
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
        new PageListItemTemplate("Главная", typeof(MainPageViewModel), "Search", null),
        new PageListItemTemplate("Монитор ТА", typeof(MonitorPageViewModel), "Monitor", null),
        new PageListItemTemplate("Детальные отчёты", typeof(MonitorPageViewModel), "Description", null),
        new PageListItemTemplate("Учёт ТМЦ", typeof(MonitorPageViewModel), "Cart", null),
        new PageListItemTemplate("Администрирование", typeof(MonitorPageViewModel), "AdminSettings", [
            new PageListItemTemplate("Торговые автоматы", typeof(MainPageViewModel), "Description", null),
            new PageListItemTemplate("Компании", typeof(MainPageViewModel), "Description", null),
            new PageListItemTemplate("Пользователи", typeof(MainPageViewModel), "Description", null),
            new PageListItemTemplate("Модемы", typeof(MonitorPageViewModel), "Description", null),
            new PageListItemTemplate("Дополнительные", typeof(MonitorPageViewModel), "Description", null),
        ]),
    ];

    private void SelectPage()
    {
        if (SelectedPageListItem == null) return;

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
    public PageListItemTemplate(string label, Type type, string icon,
        ObservableCollection<PageListItemTemplate>? children)
    {
        ModelType = type;
        Label = label;
        if (children != null) Children = children;

        Application.Current!.TryFindResource(icon, out var res);
        Icon = (StreamGeometry)res!;
    }

    public string Label { get; set; }
    public Type ModelType { get; set; }
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

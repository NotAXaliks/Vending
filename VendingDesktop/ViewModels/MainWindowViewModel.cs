using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

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
        set
        {
            if (SetProperty(ref _selectedPageListItem, value))
                SelectPage();
        }
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
        new PageListItemTemplate("Главная", typeof(MainPageViewModel), "Search"),
        new PageListItemTemplate("Монитор ТА", typeof(MonitorPageViewModel), "Monitor")
    ];

    private void SelectPage()
    {
        if (SelectedPageListItem == null)
            return;

        CurrentPage = (ViewModelBase)Activator.CreateInstance(SelectedPageListItem.ModelType)!;
    }

    public void TogglePageListCommand()
    {
        IsMenuOpen = !IsMenuOpen;
    }
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

public class PageListItemTemplate
{
    public PageListItemTemplate(string label, Type type, string icon)
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
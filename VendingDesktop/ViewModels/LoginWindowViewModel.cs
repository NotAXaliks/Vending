using CommunityToolkit.Mvvm.ComponentModel;

namespace VendingDesktop.ViewModels;

public partial class LoginWindowViewModel : ViewModelBase
{
    [ObservableProperty] private string _email;
    [ObservableProperty] private string _password;
}
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using VendingDesktop.Dtos;
using VendingDesktop.services;
using VendingDesktop.ViewModels;

namespace VendingDesktop.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    public async void OnEnterClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is LoginWindowViewModel vm)
        {
            var loginResponse = await AuthService.Login(new LoginRequestDto(vm.Email, vm.Password, "DesktopApp"));
            if (!loginResponse.IsSuccess)
            {
                ErrorText.Text = string.IsNullOrWhiteSpace(loginResponse.Error)
                    ? "Пользователь не найден!"
                    : loginResponse.Error;
                return;
            }

            Window mainWindow = new MainWindow(loginResponse.Data!.User);

            mainWindow.Show();
            Close();
        }
    }
}
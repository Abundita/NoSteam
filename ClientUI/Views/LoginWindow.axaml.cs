using System.IO;
using Avalonia.Controls;
using ClientUI.Extensions;
using ClientUI.ViewModels;
using QRCoder;

namespace ClientUI.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        this.TryTranslateSelf();
    }
}
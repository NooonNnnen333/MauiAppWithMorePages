using Povtorenie333.System;

namespace Povtorenie333;

public partial class MainPage : ContentPage
{

    public MainPage(SystemPg111 vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Povtorenie333.System;

namespace Povtorenie333;

public partial class DetailObjectPage : ContentPage
{
    public DetailObjectPage(DetObjPg vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
using Povtorenie333;
using Povtorenie333.System; 
namespace Povtorenie333;

public partial class AppShell : Shell
{
    public AppShell()
    {
        
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(HouseCreatePage), typeof(HouseCreatePage));
        
        Routing.RegisterRoute(nameof(OfficeBuildCreatePage), typeof(OfficeBuildCreatePage));
        
        Routing.RegisterRoute(nameof(DetailObjectPage), typeof(DetailObjectPage));
        
    }
}
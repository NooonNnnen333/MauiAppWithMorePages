using classes;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;



namespace Povtorenie333.System
{

    [QueryProperty("Indx", "Indx")]
    public partial class DetObjPg : ObservableObject
    {
        private readonly SystemPg111 pg111;

        // Передаем SystemPg111 через конструктор
        public DetObjPg(SystemPg111 systemPg111)
        {
            pg111 = systemPg111;

            TypeRation = pg111.ManagementCompanySvo.quarters[pg111.indexxx].Type;
            name = pg111.ManagementCompanySvo.quarters[pg111.indexxx].Address;
            plotnoste = Convert.ToString(pg111.ManagementCompanySvo.quarters[pg111.indexxx].Population);
            
        }
        

        [ObservableProperty] 
        private string name;
        
        [ObservableProperty] 
        private string typeRation;
        
        [ObservableProperty] 
        private string plotnoste;
        
        

    }
}    
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
    [QueryProperty(nameof(Rationsss), "Rationsss")]
    [QueryProperty(nameof(Text), "Text")]
    public partial class HouseCreateSys : ObservableObject
    {
        private readonly SystemPg111 pg111;
    
        // Передаем SystemPg111 через конструктор
        public HouseCreateSys(SystemPg111 systemPg111)
        {
            pg111 = systemPg111;
        }
        
        [ObservableProperty]
        public ManagementCompany rationsss;

        [ObservableProperty] 
        private string squar;
        
        [ObservableProperty] 
        private string roomCount;
        
        [ObservableProperty]
        public string text;

        [RelayCommand]
        private async Task AddRations() 
        {
            // Проверка на null для ManagementCompanySvo
            pg111.ManagementCompanySvo.AddQuarters(new House(text, Convert.ToInt32(squar), Convert.ToInt32(roomCount)));
            pg111.Items.Add(pg111.ManagementCompanySvo.names[pg111.ManagementCompanySvo.names.Count-1]);

            pg111.BuildingsCount = Convert.ToString(pg111.managementCompanySvo.CountM());
            pg111.Sredn = Convert.ToString(pg111.ManagementCompanySvo.average);
            
            pg111.TextofName = string.Empty;
            
            
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
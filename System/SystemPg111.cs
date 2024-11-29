using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using classes;
using System.Text.Json;
using System.IO;
using System.Text;
using System.Xml;
using System;
using Microsoft.Maui.Storage;

namespace Povtorenie333.System;

public partial class SystemPg111 : ObservableObject
{
    public SystemPg111()
    {
        items = new ObservableCollection<string>(managementCompanySvo.names);

    }

    [ObservableProperty] 
    public ManagementCompany managementCompanySvo = new ManagementCompany(); // Управляющая коммпания из классов

    [ObservableProperty] 
    public int indexxx;

    [ObservableProperty] 
    public string sredn;
    
    [ObservableProperty]  
    public ObservableCollection<string> items;
    
    
    [ObservableProperty] 
    public string textofName;
    
    [ObservableProperty]
    private string buildingsCount;

    [ObservableProperty]
    public int i = 5;
    
    [RelayCommand]
    async Task CrtHS(string text)
    {
        if (string.IsNullOrEmpty(text))
            return;
        
        Dictionary<string, object> parametros = new Dictionary<string, object>() {{"Text", text}, {"Rationsss", managementCompanySvo}};
    
        await Shell.Current.GoToAsync(nameof(Povtorenie333.HouseCreatePage), parametros);
    }
    
    [RelayCommand]
    async Task CrtOB( string text )
    {
        if (string.IsNullOrEmpty(textofName))
            return;
        
        Dictionary<string, object> parametros = new Dictionary<string, object>() {{"Text", text}, {"Rationsss", managementCompanySvo}};
        
        await Shell.Current.GoToAsync(nameof(OfficeBuildCreatePage), parametros);
    }

    [RelayCommand]
    private void Delete(string buildingName)
    {
        if (string.IsNullOrWhiteSpace(buildingName))
            return;

        int indexxx = managementCompanySvo.names.IndexOf(buildingName);
        
        
        managementCompanySvo.quarters.RemoveAt(indexxx);
        Items.Remove(buildingName);
        
        Sredn = Convert.ToString(ManagementCompanySvo.average);
        
        // Обновляем счётчик
        BuildingsCount = Convert.ToString(managementCompanySvo.quarters.Count);
    }
    
    [RelayCommand]
    async Task Function(string Nobject)
    {
        
        int index = items.IndexOf(Nobject);
        indexxx = index;
        
        Dictionary<string, object> parametros = new Dictionary<string, object>() {{"Indx", index}};
        await Shell.Current.GoToAsync(nameof(DetailObjectPage), parametros);
        
    }

    [RelayCommand]
    private void Upor()
    {
        managementCompanySvo.Sorted();

        Items.Clear();
        foreach (var name in managementCompanySvo.names)
        {
            Items.Add(name);
        }
    }
    
    // Сохранение в формате JSON
    
    [ObservableProperty]
    string fileName = Path.Combine(FileSystem.AppDataDirectory, "JsData.json");
    
    public void ddd()
    {
        customers = new List<string>() { "fjgtignt" };
        var serializedData = JsonSerializer.Serialize(customers);
        File.WriteAllText(fileName, serializedData);
    }

    [ObservableProperty]
    public List<string> customers;
    
    [ObservableProperty] 
    public string nnn;
    
    public void deserialize()
    {
        var rawData = File.ReadAllText(fileName);
        customers = JsonSerializer.Deserialize<List<string>>(rawData);
        nnn = customers[0];
    }

}


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
using System.Windows.Input;
using Microsoft.Maui.Storage;

namespace Povtorenie333.System;

public partial class SystemPg111 : ObservableObject
{
    public SystemPg111()
    {
        save = new AsyncRelayCommand(SaveNote); // Иницилизируем метод SaveNote для сохранения
        load = new AsyncRelayCommand(OpenNote); // Иницилизируем метод OpenNote для открытия
        items = new ObservableCollection<string>(managementCompanySvo.names);
        OpenNote();
    }

    [ObservableProperty]
    public ManagementCompany managementCompanySvo = new ManagementCompany(); // Управляющая коммпания из классов

    [ObservableProperty] public int indexxx;

    [ObservableProperty] public string sredn;

    [ObservableProperty] public ObservableCollection<string> items;


    [ObservableProperty] public string textofName;

    [ObservableProperty] private string buildingsCount;

    [ObservableProperty] public int i = 5;

    [RelayCommand]
    async Task CrtHS(string text)
    {
        if (string.IsNullOrEmpty(text))
            return;

        Dictionary<string, object> parametros = new Dictionary<string, object>()
            { { "Text", text }, { "Rationsss", managementCompanySvo } };

        SaveNote();
        await Shell.Current.GoToAsync(nameof(Povtorenie333.HouseCreatePage), parametros);
    }

    [RelayCommand]
    async Task CrtOB(string text)
    {
        if (string.IsNullOrEmpty(textofName))
            return;

        Dictionary<string, object> parametros = new Dictionary<string, object>()
            { { "Text", text }, { "Rationsss", managementCompanySvo } };

        SaveNote();
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

        Dictionary<string, object> parametros = new Dictionary<string, object>() { { "Indx", index } };
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
    
    
    
    /*Сохранения файла*/
    
    
    
    [ObservableProperty]
    private string text;

    [ObservableProperty]
    private ICommand save;

    private async Task SaveNote()
    {
        text = "";
        foreach (var svoManagement in managementCompanySvo.names)
        {
            text += svoManagement;
            text += " ";
        }
        
        var path = FileSystem.Current.AppDataDirectory;
        var fullPath = Path.Combine(path, "MyFile.txt");

        File.WriteAllText(fullPath, text);

        await Shell.Current.DisplayAlert("Saved!", $"Note has been saved!{path}", "OK");
    }
    
    [ObservableProperty]
    private ICommand load;
    
    private async Task OpenNote()
    {
        var path = FileSystem.Current.AppDataDirectory;
        var fullPath = Path.Combine(path, "MyFile.txt");

        // Проверяем, существует ли файл
        if (!File.Exists(fullPath))
        {
            await Shell.Current.DisplayAlert("Error", "File not found.", "OK");
            return;
        }

        try
        {
            string textFromFile = File.ReadAllText(fullPath); // Считываем содержимое файла


            string[] lines = textFromFile.Split(" "); // Преобразуем текст в строку разделение(.Split), так как разделены адреса - пробелом 

            if (lines.Length == 0)
            {
                await Shell.Current.DisplayAlert("Error", "File is empty.", "OK");
                return;
            }

            // Добавляем строки в список
            foreach (var line in lines)
            {
                if (line != "") 
                    managementCompanySvo.AddQuarters(new House(line, 35, 11));  // Выводим из MyFile.txt "text" и добавляем дом с таким же именим, но с 35 жильцами и 11 комнатами
            }

            // Обновляем ObservableCollection для отображения в UI
            Items.Clear();
            foreach (var name in managementCompanySvo.names)
            {
                Items.Add(name);
            }

            await Shell.Current.DisplayAlert("Loaded!", "File has been loaded!", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
    
}


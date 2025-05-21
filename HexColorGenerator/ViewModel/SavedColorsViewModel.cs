using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using HexColorGenerator.Models;

namespace HexColorGenerator.ViewModel
{
    public class SavedColorsViewModel : INotifyPropertyChanged
    {
        public RelayCommand<SavedColor> DeleteCommand { get;}

        private const string SavedColorsFile = "Savedcolors.json";
        public ObservableCollection<SavedColor> SavedColors { get; set; }

        public SavedColorsViewModel()
        {
            SavedColors = new ObservableCollection<SavedColor> ();
            DeleteCommand = new RelayCommand<SavedColor>(DeleteColor);
            LoadColors();
        }

        public async Task addColor(string hexCode, string rgbCode)
        {
            if (!SavedColors.Any(c => c.HexCode == hexCode))
            {
                var addColorToast = Toast.Make("Color Added to Favorites", CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
                await addColorToast.Show();
                SavedColors.Add(new SavedColor { HexCode = hexCode, RgbCode = rgbCode });
                SaveColors();
                OnPropertyChanged(nameof(SavedColors));
            }
        }

       private void DeleteColor(SavedColor color)
        {
            if (color != null && SavedColors.Contains(color))
            {
                SavedColors.Remove(color);
                SaveColors();
            }
        }

        private string GetFilePath()
        {
            return Path.Combine(FileSystem.AppDataDirectory,SavedColorsFile);
        }

        public void SaveColors()
        {
            var json = JsonSerializer.Serialize(SavedColors);
            File.WriteAllText(GetFilePath(), json);
        }

        private void LoadColors()
        {
            var path = GetFilePath();
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var colors = JsonSerializer.Deserialize<ObservableCollection<SavedColor>>(json);
                if (colors != null)
                {
                    SavedColors.Clear();
                    foreach (var color in colors)
                        SavedColors.Add(color);
                    OnPropertyChanged(nameof(SavedColors));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

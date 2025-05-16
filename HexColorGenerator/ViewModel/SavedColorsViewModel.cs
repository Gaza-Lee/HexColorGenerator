using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HexColorGenerator.Models;

namespace HexColorGenerator.ViewModel
{
    public class SavedColorsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<SavedColor> SavedColors { get; set; }

        public SavedColorsViewModel()
        {
            SavedColors = new ObservableCollection<SavedColor> ();
        }

        public void addColor(string hexCode, string rgbCode)
        {
            if (!SavedColors.Any(c => c.HexCode == hexCode))
            {
                SavedColors.Add(new SavedColor { HexCode = hexCode, RgbCode = rgbCode });
                OnPropertyChanged(nameof(SavedColors));
            }
        }

        public void deleteColor(string hexValue)
        {
            SavedColors.Remove(new SavedColor { HexCode = hexValue });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

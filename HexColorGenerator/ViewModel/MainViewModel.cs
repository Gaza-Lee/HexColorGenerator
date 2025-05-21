using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HexColorGenerator.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int _red;
        private int _green;
        private int _blue;
        private string _hexCode;
        private string _rgbCode;
        private Color _currentColor;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Red
        {
            get => _red;
            set { if (_red != value) { _red = value; OnPropertyChanged(); UpdateColor(); } }
        }

        public int Green
        {
            get => _green;
            set { if (_green != value) { _green = value; OnPropertyChanged(); UpdateColor(); } }
        }

        public int Blue
        {
            get => _blue;
            set { if (_blue != value) { _blue = value; OnPropertyChanged(); UpdateColor(); } }
        }

        public string HexCode
        {
            get => _hexCode;
            set { _hexCode = value; OnPropertyChanged(); }
        }

        public string RgbCode
        {
            get => _rgbCode;
            set { _rgbCode = value; OnPropertyChanged(); }
        }

        public Color CurrentColor
        {
            get => _currentColor;
            set { _currentColor = value; OnPropertyChanged(); }
        }

        public ICommand GenerateRandomCommand { get; }
        public ICommand AddToFavoritesCommand { get; }
        public ICommand CopyHexCommand { get; }
        public ICommand CopyRgbCommand { get; }

        private readonly SavedColorsViewModel _savedColorsViewModel;

        public MainViewModel(SavedColorsViewModel savedColorsViewModel)
        {
            _savedColorsViewModel = savedColorsViewModel;
            GenerateRandomCommand = new RelayCommand(GenerateRandomColor);
            AddToFavoritesCommand = new RelayCommand(AddToFavorites);
            CopyHexCommand = new AsyncRelayCommand(CopyHexToClipboard);
            CopyRgbCommand = new AsyncRelayCommand(CopyRgbToClipboard);
            Red = 0; Green = 0; Blue = 0;
            UpdateColor();
        }

        private async Task CopyHexToClipboard()
        {
            if (!string.IsNullOrWhiteSpace(HexCode))
                await Clipboard.SetTextAsync(HexCode);
            var hexToast = Toast.Make("Hex Code Copied", CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
            await hexToast.Show();
        }

        private async Task CopyRgbToClipboard()
        {
            if (string.IsNullOrWhiteSpace(RgbCode))
                await Clipboard.SetTextAsync(RgbCode);
            var rgbToast = Toast.Make("rgb Code Copied", CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
            await rgbToast.Show();
        }


        private void UpdateColor()
        {
            CurrentColor = Color.FromRgb(Red, Green, Blue);
            HexCode = CurrentColor.ToHex();
            RgbCode = $"RGB({Red}, {Green}, {Blue})";

        }

        private void GenerateRandomColor()
        {
            var random = new Random();
            Red = random.Next(0, 256);
            Green = random.Next(0, 256);
            Blue = random.Next(0, 256);
        }

        private void AddToFavorites()
        {
            _savedColorsViewModel.addColor(HexCode, RgbCode);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

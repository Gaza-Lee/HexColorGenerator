using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Alerts;
using HexColorGenerator.ViewModel;
using System.Diagnostics;

namespace HexColorGenerator.Views;

public partial class MainPage : ContentPage
{
	private readonly SavedColorsViewModel _savedColorsViewModel = new SavedColorsViewModel();
	bool isRandom;
	string hexCode;
	string rgbCode;
	public MainPage()
	{
		InitializeComponent();
	}

	//When Slider moves. The color is also updated
    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
		//Ensure random button executes once
		if (!isRandom)
		{
            var red = (int)redSlider.Value;
            var green = (int)greenSlider.Value;
            var blue = (int)blueSlider.Value;

            //When sliders move Entry updates
            redEntry.Text = red.ToString();
            greenEntry.Text = green.ToString();
            blueEntry.Text = blue.ToString();

            Color color = Color.FromRgb(red, green, blue);

            SetColor(color);
        }
		
    }
	 //On moving the slider these elements background also changes. And Generates hexcode. The entry is also update with the color value
    private void SetColor(Color color)
    {
		Debug.WriteLine(color.ToString());
		btnRandom.Background = color;
		Container.BackgroundColor = color;
		hexCode = color.ToHex();
		lblHex.Text = hexCode;
		rgbCode = $"RGB ({redEntry.Text}, {greenEntry.Text}, {blueEntry.Text})";
		lblRGB.Text = rgbCode;
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
		if(int.TryParse(redEntry.Text, out int red) && red >= 0 && red <= 255)
		{
			redSlider.Value = red;
		}

		if(int.TryParse(greenEntry.Text,out int green) && green >= 0 && green <= 255)
		{
			greenSlider.Value = green;
		}
		if (int.TryParse(blueEntry.Text, out int blue) && blue >= 0 && blue <= 255)
		{
			blueSlider.Value = blue;
		}
    }

    private void btnRandom_Clicked(object sender, EventArgs e)
    {
		isRandom = true;
		var random = new Random();

		int red = random.Next(0, 256);
		int green = random.Next(0, 256);
		int blue = random.Next(0, 256);

		var color = Color.FromRgb(red, green, blue);

		redSlider.Value = red;
		greenSlider.Value = green;
		blueSlider.Value = blue;

		redEntry.Text = red.ToString();
		greenEntry.Text = green.ToString();
		blueEntry.Text = blue.ToString();

        SetColor(color);

		isRandom = false;

    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		if (sender is Label label)
		{
            switch (label)
            {
                case var _ when label == lblCopyHex:
                    await Clipboard.SetTextAsync(hexCode);
                    var toastHex = Toast.Make("Hex Color Copied", CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
                    await toastHex.Show();
                    break;

				case var _ when label == lblCopyRGB:
					await Clipboard.SetTextAsync(rgbCode.ToLower());
					var toastRGB = Toast.Make("RGB Code Copied",CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
					await toastRGB.Show();
					break;
            }
        }
    }

    private void favoriteColors_Tapped(object sender, TappedEventArgs e)
    {
		Navigation.PushAsync(new SavedColorsPage(_savedColorsViewModel));
    }

    private async void AddColor_Tapped(object sender, TappedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(hexCode))
        {
            _savedColorsViewModel.addColor(hexCode, rgbCode);
            var toastFavColor = Toast.Make("Added to Favorites", CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
            await toastFavColor.Show();
        }
    }
}
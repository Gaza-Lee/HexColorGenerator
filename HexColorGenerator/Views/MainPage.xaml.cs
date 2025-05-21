using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Alerts;
using HexColorGenerator.ViewModel;
using System.Diagnostics;

namespace HexColorGenerator.Views;

public partial class MainPage : ContentPage
{
	private readonly SavedColorsViewModel _savedColorsViewModel = new SavedColorsViewModel();
	private readonly MainViewModel _mainViewModel;
	public MainPage()
	{
		InitializeComponent();
		_mainViewModel = new MainViewModel(_savedColorsViewModel);
		BindingContext = _mainViewModel;
	}

	

    private void favoriteColors_Tapped(object sender, TappedEventArgs e)
    {
		Navigation.PushAsync(new SavedColorsPage(_savedColorsViewModel));
    }
}
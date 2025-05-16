using HexColorGenerator.ViewModel;

namespace HexColorGenerator.Views;

public partial class SavedColorsPage : ContentPage
{
	private readonly SavedColorsViewModel _savedColorsViewModel;
	public SavedColorsPage(SavedColorsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
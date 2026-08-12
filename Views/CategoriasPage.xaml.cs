namespace AppPickingMinhasCompras.Views;

using AppPickingMinhasCompras.ViewModels;

public partial class CategoriasPage : ContentPage
{
	private readonly CategoriaViewModel _viewModel;

	public CategoriasPage()
	{
		InitializeComponent();
		_viewModel = new CategoriaViewModel();
		BindingContext = _viewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.LoadCategoriasCommand.ExecuteAsync(null);
	}
}

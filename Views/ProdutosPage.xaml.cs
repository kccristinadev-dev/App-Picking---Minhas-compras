namespace AppPickingMinhasCompras.Views;

using AppPickingMinhasCompras.ViewModels;

public partial class ProdutosPage : ContentPage
{
	private readonly ProdutoViewModel _viewModel;

	public ProdutosPage()
	{
		InitializeComponent();
		_viewModel = new ProdutoViewModel();
		BindingContext = _viewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.LoadProdutosCommand.ExecuteAsync(null);
	}
}

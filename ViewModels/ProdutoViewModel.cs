using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AppPickingMinhasCompras.Models;
using AppPickingMinhasCompras.Services;

namespace AppPickingMinhasCompras.ViewModels;

public partial class ProdutoViewModel : ObservableObject
{
    private readonly IDbService _dbService;

    [ObservableProperty]
    private List<Produto> produtos = new();

    [ObservableProperty]
    private bool isLoading = false;

    [ObservableProperty]
    private string nomeProduto = string.Empty;

    [ObservableProperty]
    private string descricaoProduto = string.Empty;

    [ObservableProperty]
    private decimal precoProduto = 0;

    [ObservableProperty]
    private int quantidadeProduto = 1;

    public ProdutoViewModel()
    {
        _dbService = MauiProgram.CreateMauiApp().Services.GetRequiredService<IDbService>();
    }

    [RelayCommand]
    public async Task LoadProdutos()
    {
        try
        {
            IsLoading = true;
            Produtos = await _dbService.GetProdutosAsync();
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro ao carregar produtos: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    public async Task SalvarProduto()
    {
        if (string.IsNullOrWhiteSpace(NomeProduto))
        {
            await Application.Current!.MainPage!.DisplayAlert("Validação", "Nome do produto é obrigatório", "OK");
            return;
        }

        try
        {
            var produto = new Produto
            {
                Nome = NomeProduto,
                Descricao = DescricaoProduto,
                Preco = PrecoProduto,
                Quantidade = QuantidadeProduto
            };

            await _dbService.SaveProdutoAsync(produto);
            await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Produto salvo com sucesso", "OK");

            // Limpar campos
            NomeProduto = string.Empty;
            DescricaoProduto = string.Empty;
            PrecoProduto = 0;
            QuantidadeProduto = 1;

            await LoadProdutos();
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro ao salvar produto: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    public async Task DeleteProduto(Produto produto)
    {
        try
        {
            await _dbService.DeleteProdutoAsync(produto);
            await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Produto deletado com sucesso", "OK");
            await LoadProdutos();
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro ao deletar produto: {ex.Message}", "OK");
        }
    }
}

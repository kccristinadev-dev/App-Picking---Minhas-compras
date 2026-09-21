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
private List<Produto> produtosFiltrados = new();

[ObservableProperty]
private string textoBusca = string.Empty;

    [ObservableProperty]
    private bool isLoading = false;

    [ObservableProperty]
    private string nomeProduto = string.Empty;


    [ObservableProperty]
    private string descricaoProduto = string.Empty;

    [ObservableProperty]
    private decimal precoProduto = 0;
public ObservableCollection<Categoria> Categorias { get; } = new();

[ObservableProperty]
private Categoria? categoriaSelecionada;
[ObservableProperty]
private Categoria? categoriaFiltroSelecionada;
    [ObservableProperty]
    private int quantidadeProduto = 1;
public ObservableCollection<RelatorioCategoria> RelatorioCategorias { get; } = new();
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

ProdutosFiltrados = Produtos;
var categorias = await _dbService.GetCategoriasAsync();

Categorias.Clear();

foreach (var categoria in categorias)
{
    Categorias.Add(categoria);
await GerarRelatorio();
}

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

private async Task GerarRelatorio()
{
    var produtos = await _dbService.GetProdutosAsync();
    var categorias = await _dbService.GetCategoriasAsync();

    RelatorioCategorias.Clear();

    foreach (var categoria in categorias)
    {
        var total = produtos
            .Where(p => p.CategoriaId == categoria.Id)
            .Sum(p => p.Preco * p.Quantidade);

        RelatorioCategorias.Add(new RelatorioCategoria
        {
            NomeCategoria = categoria.Nome,
            TotalGasto = total
        });
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
if (CategoriaSelecionada == null)
{
    await Application.Current!.MainPage!.DisplayAlert(
        "Validação",
        "Selecione uma categoria",
        "OK");

    return;
}

        try
        {
            var produto = new Produto
            {
                Nome = NomeProduto,
                Descricao = DescricaoProduto,
                Preco = PrecoProduto,
                Quantidade = QuantidadeProduto,
CategoriaId = CategoriaSelecionada.Id
            };

            await _dbService.SaveProdutoAsync(produto);
            await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Produto salvo com sucesso", "OK");

            // Limpar campos
            NomeProduto = string.Empty;
            DescricaoProduto = string.Empty;
            PrecoProduto = 0;
            QuantidadeProduto = 1;
CategoriaSelecionada = null;
            await LoadProdutos();
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro ao salvar produto: {ex.Message}", "OK");
        }
    }
private void AplicarFiltros()
{
    IEnumerable<Produto> resultado = Produtos;

    if (!string.IsNullOrWhiteSpace(TextoBusca))
    {
        resultado = resultado.Where(p =>
            p.Nome.Contains(
                TextoBusca,
                StringComparison.OrdinalIgnoreCase));
    }

    if (CategoriaFiltroSelecionada != null)
    {
        resultado = resultado.Where(p =>
            p.CategoriaId == CategoriaFiltroSelecionada.Id);
    }

    ProdutosFiltrados = resultado.ToList();
}

partial void OnTextoBuscaChanged(string value)
{
    AplicarFiltros();
}

partial void OnCategoriaFiltroSelecionadaChanged(Categoria? value)
{
    AplicarFiltros();
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

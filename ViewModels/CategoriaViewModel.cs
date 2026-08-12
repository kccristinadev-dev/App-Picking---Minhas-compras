using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AppPickingMinhasCompras.Models;
using AppPickingMinhasCompras.Services;

namespace AppPickingMinhasCompras.ViewModels;

public partial class CategoriaViewModel : ObservableObject
{
    private readonly IDbService _dbService;

    [ObservableProperty]
    private List<Categoria> categorias = new();

    [ObservableProperty]
    private bool isLoading = false;

    [ObservableProperty]
    private string nomeCategorias = string.Empty;

    [ObservableProperty]
    private string descricaoCategoria = string.Empty;

    public CategoriaViewModel()
    {
        _dbService = MauiProgram.CreateMauiApp().Services.GetRequiredService<IDbService>();
    }

    [RelayCommand]
    public async Task LoadCategorias()
    {
        try
        {
            IsLoading = true;
            Categorias = await _dbService.GetCategoriasAsync();
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro ao carregar categorias: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    public async Task SalvarCategoria()
    {
        if (string.IsNullOrWhiteSpace(NomeCategorias))
        {
            await Application.Current!.MainPage!.DisplayAlert("Validação", "Nome da categoria é obrigatório", "OK");
            return;
        }

        try
        {
            var categoria = new Categoria
            {
                Nome = NomeCategorias,
                Descricao = DescricaoCategoria
            };

            await _dbService.SaveCategoriaAsync(categoria);
            await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Categoria salva com sucesso", "OK");

            // Limpar campos
            NomeCategorias = string.Empty;
            DescricaoCategoria = string.Empty;

            await LoadCategorias();
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro ao salvar categoria: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    public async Task DeleteCategoria(Categoria categoria)
    {
        try
        {
            await _dbService.DeleteCategoriaAsync(categoria);
            await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Categoria deletada com sucesso", "OK");
            await LoadCategorias();
        }
        catch (Exception ex)
        {
            await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro ao deletar categoria: {ex.Message}", "OK");
        }
    }
}

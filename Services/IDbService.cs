using AppPickingMinhasCompras.Models;

namespace AppPickingMinhasCompras.Services;

public interface IDbService
{
    Task InitializeAsync();
    
    // Produtos
    Task<List<Produto>> GetProdutosAsync();
    Task<Produto?> GetProdutoAsync(int id);
    Task<int> SaveProdutoAsync(Produto produto);
    Task<int> DeleteProdutoAsync(Produto produto);
    Task<int> UpdateProdutoAsync(Produto produto);

    // Categorias
    Task<List<Categoria>> GetCategoriasAsync();
    Task<Categoria?> GetCategoriaAsync(int id);
    Task<int> SaveCategoriaAsync(Categoria categoria);
    Task<int> DeleteCategoriaAsync(Categoria categoria);
    Task<int> UpdateCategoriaAsync(Categoria categoria);
}

using SQLite;
using AppPickingMinhasCompras.Models;

namespace AppPickingMinhasCompras.Services;

public class DbService : IDbService
{
    private SQLiteAsyncConnection? _connection;

    public DbService()
    {
    }

    public async Task InitializeAsync()
    {
        if (_connection != null)
            return;

        _connection = new SQLiteAsyncConnection(GetDatabasePath());

        // Criar tabelas
        await _connection.CreateTableAsync<Categoria>();
        await _connection.CreateTableAsync<Produto>();
    }

    // Produtos
    public async Task<List<Produto>> GetProdutosAsync()
    {
        await EnsureConnectionAsync();
        return await _connection!.Table<Produto>().ToListAsync();
    }

    public async Task<Produto?> GetProdutoAsync(int id)
    {
        await EnsureConnectionAsync();
        return await _connection!.Table<Produto>().Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveProdutoAsync(Produto produto)
    {
        await EnsureConnectionAsync();
        produto.DataCriacao = DateTime.Now;
        return await _connection!.InsertAsync(produto);
    }

    public async Task<int> UpdateProdutoAsync(Produto produto)
    {
        await EnsureConnectionAsync();
        produto.DataAtualizacao = DateTime.Now;
        return await _connection!.UpdateAsync(produto);
    }

    public async Task<int> DeleteProdutoAsync(Produto produto)
    {
        await EnsureConnectionAsync();
        return await _connection!.DeleteAsync(produto);
    }

    // Categorias
    public async Task<List<Categoria>> GetCategoriasAsync()
    {
        await EnsureConnectionAsync();
        return await _connection!.Table<Categoria>().ToListAsync();
    }

    public async Task<Categoria?> GetCategoriaAsync(int id)
    {
        await EnsureConnectionAsync();
        return await _connection!.Table<Categoria>().Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveCategoriaAsync(Categoria categoria)
    {
        await EnsureConnectionAsync();
        categoria.DataCriacao = DateTime.Now;
        return await _connection!.InsertAsync(categoria);
    }

    public async Task<int> UpdateCategoriaAsync(Categoria categoria)
    {
        await EnsureConnectionAsync();
        return await _connection!.UpdateAsync(categoria);
    }

    public async Task<int> DeleteCategoriaAsync(Categoria categoria)
    {
        await EnsureConnectionAsync();
        return await _connection!.DeleteAsync(categoria);
    }

    private async Task EnsureConnectionAsync()
    {
        if (_connection == null)
            await InitializeAsync();
    }

    private static string GetDatabasePath()
    {
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "apppicking.db");
        return databasePath;
    }
}

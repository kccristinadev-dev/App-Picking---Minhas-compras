using SQLite;
using AppPickingMinhasCompras.Models;
using AppPickingMinhasCompras.Constants;
using AppPickingMinhasCompras.Services;

namespace AppPickingMinhasCompras.Helpers;

/// <summary>
/// Classe responsável por todas as operações de banco de dados SQLite.
/// Implementa a interface IDbService para gerenciar Produtos e Categorias.
/// </summary>
public class SQLiteDataBaseHelpers : IDbService
{
    private SQLiteAsyncConnection? _connection;

    public SQLiteDataBaseHelpers()
    {
    }

    /// <summary>
    /// Inicializa a conexão com o banco de dados e cria as tabelas se não existirem.
    /// </summary>
    public async Task InitializeAsync()
    {
        if (_connection != null)
            return;

        // Criar conexão usando o caminho definido em AppConstants
        _connection = new SQLiteAsyncConnection(AppConstants.DatabasePath);

        // Criar tabelas
        await _connection.CreateTableAsync<Produto>();
        await _connection.CreateTableAsync<Categoria>();
    }

    // ===================== MÉTODOS PARA PRODUTOS =====================

    /// <summary>
    /// Retorna todos os produtos do banco de dados.
    /// </summary>
    public async Task<List<Produto>> GetProdutosAsync()
    {
        await EnsureConnectionAsync();
        return await _connection!.Table<Produto>().ToListAsync();
    }

    /// <summary>
    /// Busca um produto pelo ID.
    /// </summary>
    public async Task<Produto?> GetProdutoAsync(int id)
    {
        await EnsureConnectionAsync();
        return await _connection!.Table<Produto>()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Insere um novo produto no banco de dados.
    /// </summary>
    public async Task<int> SaveProdutoAsync(Produto produto)
    {
        await EnsureConnectionAsync();
        produto.DataCriacao = DateTime.Now;
        return await _connection!.InsertAsync(produto);
    }

    /// <summary>
    /// Atualiza um produto existente.
    /// Usa ExecuteAsync conforme solicitado.
    /// </summary>
    public async Task<int> UpdateProdutoAsync(Produto produto)
    {
        await EnsureConnectionAsync();
        produto.DataAtualizacao = DateTime.Now;
        
        // Usar ExecuteAsync com parâmetros para UPDATE seguro
        string sql = @"UPDATE Produto 
                      SET Nome = ?, Descricao = ?, Preco = ?, Quantidade = ?, 
                          DataAtualizacao = ?, Ativo = ? 
                      WHERE Id = ?";
        
        await _connection!.ExecuteAsync(
            sql,
            produto.Nome,
            produto.Descricao,
            produto.Preco,
            produto.Quantidade,
            produto.DataAtualizacao,
            produto.Ativo,
            produto.Id
        );

        return 1; // Retorna 1 para indicar sucesso
    }

    /// <summary>
    /// Deleta um produto do banco de dados.
    /// </summary>
    public async Task<int> DeleteProdutoAsync(Produto produto)
    {
        await EnsureConnectionAsync();
        return await _connection!.DeleteAsync(produto);
    }

    /// <summary>
    /// Busca produtos pela descrição usando LIKE.
    /// Usa parâmetros para evitar SQL injection.
    /// </summary>
    public async Task<List<Produto>> SearchProdutosByDescriptionAsync(string description)
    {
        await EnsureConnectionAsync();
        
        // Usar parâmetro ao invés de concatenação
        string sql = "SELECT * FROM Produto WHERE Descricao LIKE ?";
        return await _connection!.QueryAsync<Produto>(sql, $"%{description}%");
    }

    // ===================== MÉTODOS PARA CATEGORIAS =====================

    /// <summary>
    /// Retorna todas as categorias do banco de dados.
    /// </summary>
    public async Task<List<Categoria>> GetCategoriasAsync()
    {
        await EnsureConnectionAsync();
        return await _connection!.Table<Categoria>().ToListAsync();
    }

    /// <summary>
    /// Busca uma categoria pelo ID.
    /// </summary>
    public async Task<Categoria?> GetCategoriaAsync(int id)
    {
        await EnsureConnectionAsync();
        return await _connection!.Table<Categoria>()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Insere uma nova categoria no banco de dados.
    /// </summary>
    public async Task<int> SaveCategoriaAsync(Categoria categoria)
    {
        await EnsureConnectionAsync();
        categoria.DataCriacao = DateTime.Now;
        return await _connection!.InsertAsync(categoria);
    }

    /// <summary>
    /// Atualiza uma categoria existente.
    /// Usa ExecuteAsync com parâmetros para segurança.
    /// </summary>
    public async Task<int> UpdateCategoriaAsync(Categoria categoria)
    {
        await EnsureConnectionAsync();
        
        string sql = @"UPDATE Categorias 
                      SET Nome = ?, Descricao = ?, Ativo = ? 
                      WHERE Id = ?";
        
        await _connection!.ExecuteAsync(
            sql,
            categoria.Nome,
            categoria.Descricao,
            categoria.Ativo,
            categoria.Id
        );

        return 1; // Retorna 1 para indicar sucesso
    }

    /// <summary>
    /// Deleta uma categoria do banco de dados.
    /// </summary>
    public async Task<int> DeleteCategoriaAsync(Categoria categoria)
    {
        await EnsureConnectionAsync();
        return await _connection!.DeleteAsync(categoria);
    }

    // ===================== MÉTODO AUXILIAR =====================

    /// <summary>
    /// Garante que a conexão está inicializada antes de qualquer operação.
    /// </summary>
    private async Task EnsureConnectionAsync()
    {
        if (_connection == null)
            await InitializeAsync();
    }
}

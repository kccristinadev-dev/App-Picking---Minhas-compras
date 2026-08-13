using SQLite;
using AppPickingMinhasCompras.Models;
using AppPickingMinhasCompras.Constants;
using AppPickingMinhasCompras.Services;


namespace AppPickingMinhasCompras.Helpers;

public class SQLiteDataBaseHelpers{

private readonly SQLiteAsyncConnection _conn;

public SQLiteDataBaseHelpers(string pach) {
_conn = new SQLiteAsyncConnection(pach);
_conn.CreateTableAsync<Produto>().Waint();
}
public Task<int> Insert(Produto p){

return _conn.InsertAsync(p);

}

public Task<List<Produto>> Update(Produto p){

string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
return _conn.QueryAsync<Produto>(

sql, p.Descricao, p.Quantidade, p.Preco, p.Id
);
}

public Task<int> Delete(int ID){

return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);

}
public Task<List<Produto>> pegarP(){

return _conn.Table<Produto>().ToListAsync();

}

public Task<List<Produto>> buscasP(string q){

string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + "%'";

return _conn.QueryAsync<Produto>(sql);
}

}
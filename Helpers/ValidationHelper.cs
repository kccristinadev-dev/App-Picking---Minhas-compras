using  SQLite;
using  Produtos.Models;

namespace AppPickingMinhasCompras.Helpers;

public class SQLiteDataBaseHelpers{
   
  readonly SQLiteConnection _connection;

public SQLiteDataBaseHelpers(string pach) {
_conn = new SQLiteAsyncConnecton(pach);
_conn.CreateTableAsync<Produto>().waint();
}
public Task<int> Insert(Produto p){

return _conn.InsertAsync(p);

}

public Task<Lista<Produto>> Update(Produto p){

string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
return _conn.QueryAsync<Produto>(

sql, p.Descricao, p.Quantidade, p.Preco, p.Id
);
}

public Task<int> Delete(int ID){

return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);

}
public Task<List<Produto>> pegarP(){

 return _conn.Table<Produto>().ToListAsync().Waint ();

}

public Task<List<Produto>> buscasP(string q){


}

}
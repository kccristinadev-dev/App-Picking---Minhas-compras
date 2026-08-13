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

public void Update(Produto p){

string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
return _conn.QueryAsync<Produto>(sql);
}

public void Delete(int ID){

}
public void pegarP(){}

public void buscasP(string q){}

}
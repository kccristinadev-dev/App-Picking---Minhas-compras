using  SQLite;
using  Produtos.Models;

namespace AppPickingMinhasCompras.Helpers;

public class SQLiteDataBaseHelpers{
   
  readonly SQLiteConnection _connection;

public SQLiteDataBaseHelpers(string pach) {
_conn = new SQLiteAsyncConnecton(pach);
_conn CreateTableAsync<Produto>().waint();
}
public void Insert(Produto p){

}

public void Update(Produto p){

}

public void Delete(Produto p){

}
}
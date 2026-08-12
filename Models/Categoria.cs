using SQLite;

namespace AppPickingMinhasCompras.Models;

[Table("Categorias")]
public class Categoria
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull, Unique]
    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    [NotNull]
    public DateTime DataCriacao { get; set; } = DateTime.Now;

    public bool Ativo { get; set; } = true;
}

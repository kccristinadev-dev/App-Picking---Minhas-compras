using SQLite;

namespace AppPickingMinhasCompras.Models;

[Table("Produto")]
public class Produto
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string Nome { get; set; } = string.Empty;

    [NotNull]
    public string Descricao { get; set; } = string.Empty;

    [NotNull]
    public decimal Preco { get; set; }

    public int Quantidade { get; set; }

    [NotNull]
    public DateTime DataCriacao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public bool Ativo { get; set; } = true;
}

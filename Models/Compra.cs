using SQLite;

namespace AppPickingMinhasCompras.Models;

[Table("Compras")]
public class Compra
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public DateTime DataCompra { get; set; } = DateTime.Now;

    [NotNull]
    public decimal TotalCompra { get; set; }

    [NotNull]
    public string LocalCompra { get; set; } = string.Empty;

    public string Observacoes { get; set; } = string.Empty;

    public bool Pago { get; set; } = false;

    [NotNull]
    public DateTime DataCriacao { get; set; } = DateTime.Now;

    public DateTime? DataAtualizacao { get; set; }

    public bool Ativo { get; set; } = true;
}

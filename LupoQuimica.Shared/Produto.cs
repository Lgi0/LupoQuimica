using System.ComponentModel.DataAnnotations.Schema;

namespace LupoQuimica.Shared;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; } // Em vez de string Categoria
    [Column(TypeName = "nvarchar(max)")]
    public string ImagemUrl { get; set; } = string.Empty;
}

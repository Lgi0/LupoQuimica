namespace LupoQuimica.Shared;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string PrincipioAtivo { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; } // Em vez de string Categoria
    public string ImagemUrl { get; set; } = "https://via.placeholder.com/200";
}

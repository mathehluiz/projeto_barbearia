namespace BarberShopPlus.Models
{
    public class Produto
    {
        public string Nome { get; set; }
        public string GrupoID { get; set; }
        public string MarcaID { get; set; }
        public string ValorCompra { get; set; }
        public string ValorVenda { get; set; }
        public string ValorTotal { get; set; }
        public string MinimoEstoque { get; set; }
        public string Quantidade { get; set; }
        public string UnidadeMedida { get; set; }

        public Produto(string nome, string grupoID, string marcaID, string valorCompra, string valorVenda, string valorTotal, string minimoEstoque, string quantidade, string unidadeMedida)
        {
            Nome = nome;
            GrupoID = grupoID;
            MarcaID = marcaID;
            ValorCompra = valorCompra;
            ValorVenda = valorVenda;
            ValorTotal = valorTotal;
            MinimoEstoque = minimoEstoque;
            Quantidade = quantidade;
            UnidadeMedida = unidadeMedida;
        }
    }
}

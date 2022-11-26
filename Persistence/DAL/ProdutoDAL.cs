using BarberShopPlus.Models;
using MySql.Data.MySqlClient;

namespace BarberShopPlus.Persistence.DAL
{
    public class ProdutoDAL
    {
        private MySqlConnection _mySqlConnection;
        public ProdutoDAL(MySqlConnection mySqlConnection)
        {
            _mySqlConnection = mySqlConnection;
        }

        public void Inserir(Produto produto)
        {
            _mySqlConnection.Open();
            MySqlCommand cmd = _mySqlConnection.CreateCommand();
            cmd.CommandText = "insert into tb_produtos (prod_nome,prod_last_update,prod_unidade,prod_id_grupo,prod_id_marca) values (@Nome,@Time,@Unidade,@Id_grupo, @Id_marca);" +
                "insert into tb_estoque(estoque_quantidade,estoque_lastupdate,estoque_minimo) values (@Quantidade,@Time,@EstoqueMin);" +
                "insert into tb_precos(preco_unit_compra,preco_unit_venda,preco_total_gasto,preco_last_update, preco_total_lucro) values (@Valor_Unitario_Compra,@Valor_Unitario_Venda,@Valor_Total,@Time,@Lucro)";
            cmd.Parameters.Add("@Id_Grupo", MySqlDbType.Int32, 10).Value = produto.GrupoID;
            cmd.Parameters.Add("@Id_Marca", MySqlDbType.Int32, 10).Value = produto.MarcaID;
            cmd.Parameters.Add("@Nome", MySqlDbType.VarChar, 150).Value = produto.Nome;
            if (produto.UnidadeMedida == "LT")
            {
                var quantity = Convert.ToInt32(produto.Quantidade) * 1000;
                cmd.Parameters.Add("@Quantidade", MySqlDbType.Float, 10).Value = quantity;
            }
            else
            {
                cmd.Parameters.Add("@Quantidade", MySqlDbType.Float, 10).Value = produto.Quantidade;
            }
            cmd.Parameters.Add("@Unidade", MySqlDbType.VarChar, 10).Value = produto.UnidadeMedida;
            cmd.Parameters.Add("@Valor_Unitario_Compra", MySqlDbType.Decimal, 9).Value = Convert.ToDouble(produto.ValorCompra);
            cmd.Parameters.Add("@Valor_Unitario_Venda", MySqlDbType.Decimal, 9).Value = Convert.ToDouble(produto.ValorVenda);
            cmd.Parameters.Add("@Lucro", MySqlDbType.Decimal, 9).Value = (Convert.ToDecimal(produto.ValorVenda) - Convert.ToDecimal(produto.ValorCompra));
            cmd.Parameters.Add("@Valor_Total", MySqlDbType.Decimal, 9).Value = produto.ValorTotal;
            DateTime dateTime = DateTime.Now;
            var dateTimeDate = DateOnly.FromDateTime(dateTime);
            cmd.Parameters.Add("@Time", MySqlDbType.Date).Value = dateTimeDate;
            cmd.Parameters.Add("@EstoqueMin", MySqlDbType.Float, 10).Value = produto.MinimoEstoque;
            if (String.IsNullOrEmpty(produto.UnidadeMedida))
            {
                MessageBox.Show("O valor não pode ser nulo!");
            }
            else
            {
                cmd.ExecuteNonQuery();
                _mySqlConnection.Close();
            }
        }
        public Produto ObterPorNome(string nome)
        {
            Produto produto = null;
            var command = new MySqlCommand("Select prod_nome, prod_");
            return produto;
        }
    }
}

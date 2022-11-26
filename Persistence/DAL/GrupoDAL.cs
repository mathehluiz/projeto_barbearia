using BarberShopPlus.Models;
using MySql.Data.MySqlClient;

namespace BarberShopPlus.Persistence.DAL
{
    public class GrupoDAL
    {
        private MySqlConnection _mySqlConnection;
        public GrupoDAL(MySqlConnection mySqlConnection)
        {
            _mySqlConnection = mySqlConnection;
        }
        public void InserirGrupo(Grupo grupo)
        {
            _mySqlConnection.Open();
            MySqlCommand cmd = _mySqlConnection.CreateCommand();
            cmd.CommandText = "INSERT INTO tb_grupos(grupo_nome,grupo_last_update) values ('" + grupo.Nome + "',@Time)";
            DateTime dateTime = DateTime.Now;
            var dateTimeDate = DateOnly.FromDateTime(dateTime);
            cmd.Parameters.Add("@Time", MySqlDbType.Date).Value = dateTimeDate;
            cmd.ExecuteNonQuery();
            _mySqlConnection.Close();
        }
        public Grupo ObterPorNome(string nome)
        {
            Grupo grupo = null;
            var cmd = new MySqlCommand("SELECT grupo_id from TB_grupos where grupo_nome = '" + nome + "'");
            _mySqlConnection.Open();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    grupo.Id = reader.GetInt32(0);
                }
                return grupo;
            }
        }
    } 
}
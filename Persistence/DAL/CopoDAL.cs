
using MySql.Data.MySqlClient;

namespace BarberShopPlus.Persistence.DAL
{
    public class CopoDAL
    {
        private MySqlConnection _mySqlConnection;
        public CopoDAL(MySqlConnection mySqlConnection)
        {
            _mySqlConnection = mySqlConnection;
        }
    }
}

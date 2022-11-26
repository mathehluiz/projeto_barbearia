
namespace BarberShopPlus.Models
{
    public class Grupo
    {
        public string Nome { get; set; }
        public int Id { get; set; }

        public Grupo(string nome, int id = 0)
        {
            Nome = nome;
            Id = id;
        }
    }
}

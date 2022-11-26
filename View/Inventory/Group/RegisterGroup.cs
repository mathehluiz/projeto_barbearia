using BarberShopPlus.Models;
using BarberShopPlus.Persistence.DAL;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Projeto_Tabacaria.View.Inventory
{
    public partial class RegisterGroup : Form
    {
        public RegisterGroup()
        {
            InitializeComponent();
            lblReturnDB.Visible = false;
        }

        private void bntRegGroup_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conexaoBD"].ConnectionString;
                GrupoDAL grupoDAL = new GrupoDAL(new MySqlConnection(connectionString));
                Grupo grupo = new Grupo(txtGroupName.Text);
                grupoDAL.InserirGrupo(grupo);
                lblReturnDB.Visible = true;
                lblReturnDB.Text = "Grupo registrado";
                txtGroupName.Text = "";
            }
            catch
            {
                lblReturnDB.Visible = true;
                lblReturnDB.Text = "Erro";
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}

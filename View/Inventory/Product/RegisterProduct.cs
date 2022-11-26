using BarberShopPlus.Models;
using BarberShopPlus.Persistence.DAL;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Runtime.InteropServices;

namespace Projeto_Tabacaria.View.Inventory
{
    public partial class RegisterProduct : Form
    {
        int contador = 0;
        

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        public RegisterProduct()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }
            

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtQtd__TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtQtd.Text, out parsedValue))
            {
                txtQtd.Text = "0";

            }
            if (!double.TryParse(txtSaleValue.Text, out parsedValue))
            {
                txtSaleValue.Text = "0";

            }
            if (!double.TryParse(txtBuyValue.Text, out parsedValue))
            {
                txtBuyValue.Text = "0";

            }
            string ml = "ML";
            if (cmbUnidade_De_Medida.Text == ml || cmbUnidade_De_Medida.Text == "LT")
            {
                txtTotal.Text = txtBuyValue.Text;
                txtTotalSale.Text = txtSaleValue.Text;
            }
            else
            {
                decimal a = txtQtd.Text != "" ? Convert.ToDecimal(txtQtd.Text) : 0;
                decimal b = txtBuyValue.Text != "" ? Convert.ToDecimal(txtBuyValue.Text) : 0;
                decimal total = a * b;

                txtTotal.Text = total.ToString();
                txtTotalSale.Text = (Convert.ToInt32(txtQtd.Text) * Convert.ToInt32(txtSaleValue.Text)).ToString();
            }
            txtTotalProfit.Text = (Convert.ToDecimal(txtTotalSale.Text) - Convert.ToDecimal(txtTotal.Text)).ToString();
        }

        private void txtBuyValue__TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtQtd.Text, out parsedValue))
            {
                txtQtd.Text = "0";

            }
            if (!double.TryParse(txtSaleValue.Text, out parsedValue))
            {
                txtSaleValue.Text = "0";

            }
            if (!double.TryParse(txtBuyValue.Text, out parsedValue))
            {
                txtBuyValue.Text = "0";

            }
            string ml = "ML";
            if (cmbUnidade_De_Medida.Text == ml)
            {
                txtTotal.Text = txtBuyValue.Text;
                txtTotalSale.Text = txtSaleValue.Text;
            }
            else
            {
                decimal a = txtQtd.Text != "" ? Convert.ToDecimal(txtQtd.Text) : 0;
                decimal b = txtBuyValue.Text != "" ? Convert.ToDecimal(txtBuyValue.Text) : 0;
                decimal total = a * b;
                total = (decimal)System.Math.Round(total, 2);
                txtTotal.Text = total.ToString();
                txtTotalSale.Text = (Convert.ToInt32(txtQtd.Text) * Convert.ToInt32(txtSaleValue.Text)).ToString();
            }

            
            txtTotalProfit.Text = (Convert.ToDecimal(txtTotalSale.Text) - Convert.ToDecimal(txtTotal.Text)).ToString();
        }
        private void txtSaleValue__TextChanged(object sender, EventArgs e)
        {
            double parsedValue;
            if (!double.TryParse(txtQtd.Text, out parsedValue))
            {
                txtQtd.Text = "0";

            }
            if (!double.TryParse(txtSaleValue.Text, out parsedValue))
            {
                txtSaleValue.Text = "0";

            }
            if (!double.TryParse(txtBuyValue.Text, out parsedValue))
            {
                txtBuyValue.Text = "0";

            }
            string ml = "ML";
            if (cmbUnidade_De_Medida.Text == ml)
            {
                txtTotal.Text = txtBuyValue.Text;
                txtTotalSale.Text = txtSaleValue.Text;
            }
            else
            {
                decimal a = txtQtd.Text != "" ? Convert.ToDecimal(txtQtd.Text) : 0;
                decimal b = txtSaleValue.Text != "" ? Convert.ToDecimal(txtSaleValue.Text) : 0;
                decimal total = a * b;
                total = (decimal)System.Math.Round(total, 2);
                txtTotalSale.Text = total.ToString();
                txtTotalSale.Text = (Convert.ToInt32(txtQtd.Text) * Convert.ToInt32(txtSaleValue.Text)).ToString();
            }

            
            txtTotalProfit.Text = (Convert.ToDecimal(txtTotalSale.Text) - Convert.ToDecimal(txtTotal.Text)).ToString();
        }

        public void butRegisterProduct_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexaoBD"].ConnectionString;
            GrupoDAL grupoDAL = new GrupoDAL(new MySqlConnection(connectionString));
            ProdutoDAL produtoDAL = new ProdutoDAL(new MySqlConnection(connectionString));
            RefreshForm.Enabled = true;
            try
            {
                if (cmbUnidade_De_Medida.Text != "")
                {
                    var quantity = Convert.ToDouble(txtQtd.Text);
                    var quantityInventoryMin = Convert.ToDouble(txtInventoryMin.Text);
                    if (quantity <= 0 && quantityInventoryMin <= 0)
                    {
                        MessageBox.Show("A quantidade não pode ser 0", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        
                        Produto produto = new Produto(
                            txtProdName.Text,
                            grupoDAL.ObterPorNome(cmbGrupo.Text).Nome,
                            "1",
                            txtBuyValue.Text,
                            txtSaleValue.Text,
                            txtTotalSale.Text,
                            txtInventoryMin.Text,
                            txtQtd.Text,
                            cmbUnidade_De_Medida.Text
                            );
                        try
                        {
                            produtoDAL.Inserir(produto);
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("" + ex);
                        }
                        lblReturnDB.Visible = true;
                        lblReturnDB.Text = "Produto Registrado";
                        txtProdName.Text = "";
                        txtQtd.Text = "0";
                        txtBuyValue.Text = "0";
                        txtSaleValue.Text = "0";
                        txtTotal.Text = "0";
                        txtInventoryMin.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Unidade de medida não pode ser nula");
                }
            }
            catch(Exception ex1)
            {
                MessageBox.Show("" + ex1);
                lblReturnDB.Visible = true;
                lblReturnDB.Text = "ERRO";
                //dbConnections.CloseConnection();
            }
        }

        //private void RegisterProduct_Load(object sender, EventArgs e)
        //{


        //    //lblReturnDB.Visible = false;
        //    //if (dbConnections.connection.State != ConnectionState.Open)
        //    //{
        //    //    dbConnections.OpenConnection();
        //    //}

        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        string cmd_search_brand_group = "SELECT grupo_nome FROM tb_grupos";
        //        MySqlDataAdapter da1 = new MySqlDataAdapter(cmd_search_brand_group, dbConnections.connection);
        //        DataSet ds1 = new DataSet();
        //        da1.Fill(ds1, "tb_grupos");
        //        this.cmbGrupo.DisplayMember = "grupo_nome";
        //        this.cmbGrupo.ValueMember = "grupo_id";
        //        this.cmbGrupo.DataSource = ds1.Tables["tb_grupos"];
        //        dbConnections.CloseConnection();


        //        if (dbConnections.connection.State != ConnectionState.Open)
        //        {
        //            dbConnections.OpenConnection();
        //        }
        //        MySqlCommand cmd_search_brand_groupname = new MySqlCommand("SELECT grupo_id FROM tb_grupos WHERE grupo_nome = '" + cmbGrupo.Text + "'", dbConnections.connection);
        //        int GroupQueryResult = Convert.ToInt32(cmd_search_brand_groupname.ExecuteScalar());
        //        string query_brand_group = "SELECT marca_nome, marca_cod FROM tb_marca WHERE id_grupo_marca = '" + GroupQueryResult + "'";
        //        var cmd = dbConnections.connection.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = query_brand_group;
        //        cmd.ExecuteNonQuery();
        //        cmbGrupo.Text = "Selecione o grupo";

        //        MySqlDataAdapter da2 = new MySqlDataAdapter(cmd);
        //        da2.Fill(dt);

        //        if (cmbMarca.Items.Count > 0)
        //        {
        //            cmbMarca.SelectedIndex = 0;
        //            cmbMarca.Items.Clear();
        //        }
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            cmbMarca.Items.Add(dr["marca_nome"].ToString());
        //            cmbMarca.Text = cmbMarca.Items[0].ToString();
        //            cmbMarca.Text = "Selecione a marca";
        //        }

        //        dbConnections.CloseConnection();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Erro" + Convert.ToString(ex));
        //    }
        //    dbConnections.CloseConnection();

        //}

        //private void RefreshForm_Tick(object sender, EventArgs e)
        //{
        //    contador++;
        //    if (contador == 1)
        //    {
        //        contador = 0;
        //        RefreshForm.Enabled = false;
        //        if (dbConnections.connection.State != ConnectionState.Open)
        //        {
        //            dbConnections.OpenConnection();
        //        }
        //        string query_groups = "select grupo_nome from tb_grupos";
        //        MySqlDataAdapter da = new MySqlDataAdapter(query_groups, dbConnections.connection);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds, "tb_grupos");
        //        this.cmbGrupo.DisplayMember = "GRUPO_NOME";
        //        this.cmbGrupo.ValueMember = "GRUPO_ID";
        //        this.cmbGrupo.DataSource = ds.Tables["tb_grupos"];


        //        string query_marca = "select marca_nome from tb_marca";
        //        MySqlDataAdapter da1 = new MySqlDataAdapter(query_marca, dbConnections.connection);
        //        DataSet ds1 = new DataSet();
        //        da1.Fill(ds1, "tb_marca");
        //        this.cmbMarca.DisplayMember = "MARCA_NOME";
        //        this.cmbMarca.ValueMember = "MARCA_COD";
        //        this.cmbMarca.DataSource = ds1.Tables["tb_marca"];

        //        dbConnections.CloseConnection();

        //        txtSaleValue.Text = "0";
        //        txtTotal.Text = "0";
        //        txtBuyValue.Text = "0";
        //        txtQtd.Text = "0";
        //    }

        //}

        //private void cmbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (dbConnections.connection.State != ConnectionState.Open)
        //    {
        //        dbConnections.OpenConnection();
        //    }
        //    DataTable dt = new DataTable();

        //    try
        //    {

        //        if (dbConnections.connection.State != ConnectionState.Open)
        //        {
        //            dbConnections.OpenConnection();
        //        }
        //        MySqlCommand cmdsearchBrandGroupname = new MySqlCommand("SELECT grupo_id FROM tb_grupos WHERE grupo_nome = '" + cmbGrupo.Text + "'", dbConnections.connection);
        //        int GroupQueryResult = Convert.ToInt32(cmdsearchBrandGroupname.ExecuteScalar());
        //        string queryBrandGroup = "SELECT marca_nome, marca_cod FROM tb_marca WHERE id_grupo_marca = '" + GroupQueryResult + "'";
        //        var cmd = dbConnections.connection.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = queryBrandGroup;
        //        cmd.ExecuteNonQuery();


        //        if (cmbMarca.Items.Count > 0)
        //        {
        //            dt.Rows.Clear();
        //            cmbMarca.Items.Clear();
        //        }


        //        MySqlDataAdapter da2 = new MySqlDataAdapter(cmd);
        //        da2.Fill(dt);
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            cmbMarca.Items.Add(dr["marca_nome"].ToString());

        //        }
        //        cmbMarca.SelectedIndex = 0;


        //        dbConnections.CloseConnection();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Erro" + Convert.ToString(ex));
        //    }
        //    dbConnections.CloseConnection();
        //}

        //private void mnButton1_Click(object sender, EventArgs e)
        //{
        //    RegisterCup registerCup = new RegisterCup();
        //    registerCup.Show();
        //    this.Dispose();
        //}
    }
}
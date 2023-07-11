using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TugasAkhir
{
    public partial class Form1 : Form
    {
        private string stringConnection = "data source=HP" + "database=TugasAkhir;User ID=sa; Password=123";
        private SqlConnection connection;
        private string nama, jenis, harga, stok, admin;
        private DateTime tgl;
        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(stringConnection);
            refreshform();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtKode_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtHarga_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            connection.Open();
            string str = "select nama_admin from dbo.admins";
            SqlCommand cmd = new SqlCommand(str, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(str, connection);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            cmd.ExecuteReader();
            connection.Close();
            comboBox1.DisplayMember = "nama_admin";
            comboBox1.ValueMember = "id_admin";
            comboBox1.DataSource = dataSet.Tables[0];
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtStok_TextChanged(object sender, EventArgs e)
        {

        }

        private void start_Click(object sender, EventArgs e)
        {
            txtNama.Enabled = true;
            txtHarga.Enabled = true;
            txtStok.Enabled = true;
            txtJenis.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            refreshform();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tgl = dateTimePicker1.Value;
            nama = txtNama.Text;
            jenis = txtJenis.Text;
            harga = txtHarga.Text;
            stok = txtStok.Text;
            admin = comboBox1.Text;
            int pk = 0;
            connection.Open();
            string strr = "select id_admin from dbo.admin where nama_admin = @dd";
            SqlCommand sqlCommand = new SqlCommand(strr, connection);
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add(new SqlParameter("@dd", admin));
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                pk = int.Parse(reader["id_admin"].ToString());
            }
            reader.Close();
            string strs = "insert into dbo.produk (nama_produk,jenis_produk,harga_produk,stok)" +
                "values(@nama,@jenis,@harga,@stok)";
            SqlCommand cmd = new SqlCommand(strs, connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("nama", nama));
            cmd.Parameters.Add(new SqlParameter("jenis_produk", jenis));
            cmd.Parameters.Add(new SqlParameter("harga_produk",harga));
            cmd.Parameters.Add(new SqlParameter("stok", stok));
            cmd.ExecuteNonQuery();

            string str = "insert into dbo.data_order(id_admin,id_pembeli,tgl)" +
                "values(@ida,@idpem,@tglk)";
            SqlCommand cmds = new SqlCommand(str, connection);
            cmds.CommandType = CommandType.Text;
            cmds.Parameters.Add(new SqlParameter("ida",pk));
            cmds.Parameters.Add(new SqlParameter("idpem", nama));
            cmds.Parameters.Add(new SqlParameter("tglk", tgl));

            connection.Close();

            MessageBox.Show("Data Berhasil di simpan", "Sukses", MessageBoxButtons.OK,MessageBoxIcon.Information);
            refreshform();

        }

        private void refreshform()
        {
            txtNama.Text = "";
            txtJenis.Text = "";
            txtHarga.Text = "";
            txtStok.Text = "";
            txtNama.Enabled = false;
            txtJenis.Enabled = false;
            txtHarga.Enabled = false;
            txtStok.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }
    }
}

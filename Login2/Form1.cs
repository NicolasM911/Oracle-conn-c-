using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;
//using Oracle.ManagedDataAccess.Client;

namespace Login2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private OracleConnection conexion = new OracleConnection("DATA SOURCE = xe ; PASSWORD = 123 ; USER ID = testlogin ");
        private void btnTestCon_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                lblVersion.Text = $"La Version del Servidor es {conexion.ServerVersion}";
                lblConn.Text += $"la Conexion esta: ";
                lblConn.Text += conexion.State.ToString();
                lblConn.Text += "\nSe Accede a la base de datos";

                MessageBox.Show(@"
                Conexión Establecida Correctamente
                Conectado a la Base de Datos");
                lblVersion.Text = $"La Version del Servidor es {conexion.ServerVersion}";
                conexion.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Hay un Error al acceder a Su Base de Datos {ex.Message}");
                lblConn.Text = $"Hay un Error al acceder a Su Base de Datos {ex.Message}";
                
            }
            finally
            {
                conexion.Close();
                lblCerrado.Text += $" - Ahora la Conexion esta: {conexion.State.ToString()}";
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            conexion.Open();
            OracleCommand comando = new OracleCommand($"SELECT * FROM PERSONAS WHERE NOM_USER = :user AND PASS_USER = :pass", conexion);
            comando.Parameters.AddWithValue(":user", txtUser.Text);
            comando.Parameters.AddWithValue(":pass", txtPass.Text);

            OracleDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                MessageBox.Show("Conexion exitosa a oracle");
                Formulario formulario = new Formulario();
                conexion.Close();
                formulario.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usario y contraseña incorrectos");
            }
            conexion.Close();
        }
    }
}

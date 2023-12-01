using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaParaFinalCasales
{
    public partial class FrmPrincipal : Form
    {
        string OdSeleccionado;
     
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (cboOdontologo.SelectedIndex < 0)
            {
                MessageBox.Show("ELIJA UN ODONTOLOGO");
            }
            else
            {
                CargarGrilla();
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            CargarComboOdontologos();
        }

        private void CargarComboOdontologos()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            OleDbConnection cn = new OleDbConnection(cadenaConexion);
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                string consulta = "SELECT * FROM Odontologos";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta; 
                cn.Open(); 
                cmd.Connection = cn; 
                DataTable tabla = new DataTable(); 
                OleDbDataAdapter da = new OleDbDataAdapter(cmd); 
                da.Fill(tabla); 

                cboOdontologo.DataSource = tabla;
                cboOdontologo.DisplayMember = "nombre"; //Columna que quiero mostrar en el ComboBox      
                cboOdontologo.ValueMember = "matricula";  //Obtener valor de la opcion del combo
                cboOdontologo.SelectedIndex = -1; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboOdontologo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOdontologo.SelectedIndex != -1)
            {
                OdSeleccionado = cboOdontologo.Text;
            }

        }
        private void CargarGrilla()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            OleDbConnection cn = new OleDbConnection(cadenaConexion);
            OleDbCommand cmd = new OleDbCommand();

            string consulta = $@"
                        SELECT O.nombre AS nombre, T.fecha
                        FROM Odontologos O
                        INNER JOIN Turnos T ON O.matricula = T.matricula
                        WHERE O.nombre = '{OdSeleccionado}' AND T.fecha = @fecha;";

            cmd.Parameters.AddWithValue("fecha", dtpFecha.Value.Date);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = consulta;
            cn.Open();
            cmd.Connection = cn;
            DataTable tabla = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(tabla);

            dgvTurnos.DataSource = tabla;
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}

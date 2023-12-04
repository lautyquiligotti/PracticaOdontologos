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
        DataTable tablaHorarios;
        DataTable tablaTurnos;
        DataTable tablaMedicos;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            DateTime fecha = Convert.ToDateTime(dtpFecha.Text);
            if (fecha.DayOfWeek.ToString() == "Saturday" || fecha.DayOfWeek.ToString() == "Sunday") MessageBox.Show("El consultorio está cerrado los sabados y domingos.");
            else
            {
                dgvTurnos.Rows.Clear();
                int matricula = Convert.ToInt32(cboOdontologo.SelectedValue);
                foreach (DataRow hora in tablaHorarios.Rows)
                {
                    bool turnoDado = false;

                    foreach (DataRow turno in tablaTurnos.Rows)
                    {
                        // Creo la variable fechaTurno porque sino de la BD trae dd/MM/yyyy 00:00:0000 y al comparar con la otra fecha siempre da false.
                        DateTime fechaTurno = Convert.ToDateTime(turno["fecha"]);
                        DataRow medico = tablaMedicos.Rows.Find(Convert.ToInt32(turno["matricula"]));
                        if (turno["hora"].ToString() == hora["hora"].ToString() && Convert.ToInt32(turno["matricula"]) == matricula && fechaTurno.ToString("dd/MM/yyyy") == fecha.ToString("dd/MM/yyyy"))
                        {
                            dgvTurnos.Rows.Add(hora["hora"].ToString(), turno["paciente"].ToString(), medico["nombre"]);
                            turnoDado = true;
                            break;
                        }
                    }
                    if (!turnoDado) dgvTurnos.Rows.Add(hora["hora"].ToString(), "", "");
                }
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            Odontologos odo = new Odontologos();
            odo.listar(cboOdontologo);
            tablaMedicos = odo.getData();
            Horarios hora = new Horarios();
            tablaHorarios = hora.getData();
            Turnos turnos = new Turnos();
            tablaTurnos = turnos.getData();
        }

        private void cboOdontologo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOdontologo.SelectedIndex != -1)
            {
                OdSeleccionado = cboOdontologo.Text;
            }

        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            
        }

        //private void CargarComboOdontologos()
        //{
        //    string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
        //    OleDbConnection cn = new OleDbConnection(cadenaConexion);
        //    try
        //    {
        //        OleDbCommand cmd = new OleDbCommand();
        //        string consulta = "SELECT * FROM Odontologos";
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = consulta; 
        //        cn.Open(); 
        //        cmd.Connection = cn; 
        //        DataTable tabla = new DataTable(); 
        //        OleDbDataAdapter da = new OleDbDataAdapter(cmd); 
        //        da.Fill(tabla); 

        //        cboOdontologo.DataSource = tabla;
        //        cboOdontologo.DisplayMember = "nombre"; //Columna que quiero mostrar en el ComboBox      
        //        cboOdontologo.ValueMember = "matricula";  //Obtener valor de la opcion del combo
        //        cboOdontologo.SelectedIndex = -1; 
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void CargarGrilla()
        //{
        //    string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
        //    OleDbConnection cn = new OleDbConnection(cadenaConexion);
        //    OleDbCommand cmd = new OleDbCommand();

        //    string consulta = $@"
        //                SELECT O.nombre AS nombre, T.fecha
        //                FROM Odontologos O
        //                INNER JOIN Turnos T ON O.matricula = T.matricula
        //                WHERE O.nombre = '{OdSeleccionado}' AND T.fecha = @fecha;";

        //    cmd.Parameters.AddWithValue("fecha", dtpFecha.Value.Date);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = consulta;
        //    cn.Open();
        //    cmd.Connection = cn;
        //    DataTable tabla = new DataTable();
        //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //    da.Fill(tabla);

        //    dgvTurnos.DataSource = tabla;
        //}
    }
}

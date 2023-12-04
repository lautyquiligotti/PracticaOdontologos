using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;

namespace PracticaParaFinalCasales
{
    public class Odontologos
    {
        string cadena;
        OleDbConnection conector;
        OleDbCommand comando;
        OleDbDataAdapter adaptador;
        DataTable tabla;

        public Odontologos()
        {
            cadena = "provider=microsoft.jet.oledb.4.0;data source=DienteFeliz.mdb";

            conector = new OleDbConnection(cadena);
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Odontologos";

            adaptador = new OleDbDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);

            // Estas lineas se escriben para decirle al programa cuál es la Primary Key.
            // 
            DataColumn[] vec = new DataColumn[1]; // Creo un vector de una posicion para almacenar una columna
            vec[0] = tabla.Columns["matricula"]; // Cargo la columna en el vector
            tabla.PrimaryKey = vec; // Defino que la primary key de la tabla es la columna guardada en el vector
        }

        public void listar(ComboBox cbOdontologos)
        {
            cbOdontologos.DataSource = tabla;
            cbOdontologos.DisplayMember = "nombre";
            cbOdontologos.ValueMember = "matricula";
            cbOdontologos.SelectedIndex = -1;
        }

        public DataTable getData()
        {
            return tabla;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParaFinalCasales
{
    public class Horarios
    {
        string cadena;
        OleDbConnection conector;
        OleDbCommand comando;
        OleDbDataAdapter adaptador;
        DataTable tabla;

        public Horarios()
        {
            cadena = "provider=microsoft.jet.oledb.4.0;data source=DienteFeliz.mdb";

            conector = new OleDbConnection(cadena);
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Horarios";

            adaptador = new OleDbDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);

            // Estas lineas se escriben para decirle al programa cuál es la Primary Key.
            // 
            DataColumn[] vec = new DataColumn[1]; // Creo un vector de una posicion para almacenar una columna
            vec[0] = tabla.Columns["hora"]; // Cargo la columna en el vector
            tabla.PrimaryKey = vec;
        }

        public DataTable getData()
        {
            return tabla;
        }
    }
}

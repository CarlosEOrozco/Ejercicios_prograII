using proyecto_Practica01_.Properties;
using RepositoryExample.Data.Utils;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace RepositoryExample.Data
{
    public class DataHelper
    {
        private static DataHelper _instancia;
        private SqlConnection _connection;

        private DataHelper() //para asegurarme que solo se utilice dentro de la clase
        {
            _connection = new SqlConnection(Resources.cnnString);
        }

        public static DataHelper GetInstance() //unica instacia, sirve para llamarla
        {
            if (_instancia == null)
                _instancia = new DataHelper(); //creo el unico objeto, si no la habian pedido la creo, si ya estaba creada devuelvo la unica que tengo

            //esto es el patron singleton, garantizo que tenga una instacia global y unica
            //la responsabilidad de crear esa clase es de la propia clase
            //la clase la crea y la devuelve, no hago el new desde afuera
            //una vez que tenga la instancia puedo usar los metodos del objeto de esa clase
            return _instancia;
        }

        //Es importante tener los SP creados para las consultas de la BD
        //Este metodo devuelve una tabla que surge de la consulta en SQL
        public DataTable ExecuteSPQuery(string sp, List<ParameterSQL>? parametros)
        {
            DataTable t = new DataTable(); //crea una tabla
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection); //se crea una variable de ese tipo (query,conexion)
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                t.Load(cmd.ExecuteReader());
                _connection.Close();
            }
            catch (SqlException)
            {
                t = null;
            }

            return t;  //devuelve la tabla
        }


        public int ExecuteSPDML(string sp, List<ParameterSQL>? parametros)
        {
            int rows;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (var param in parametros)
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                rows = cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (SqlException)
            {
                rows = 0;
            }

            return rows;
        }

        public int ExecuteSPDMLTransact(string sp, List<ParameterSQL>? parametros, SqlTransaction transaction)
        {



            return 0;
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }


    }
}


using proyecto_Practica01_.Domain;
using RepositoryExample.Data.Utils;
using RepositoryExample.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_Practica01_.Data
{
    public class TipoCuentaRepository : ITipoCuentaRepository
    {
        public TipoCuenta GetById(int id)
        {
            TipoCuenta tipoCuenta = null;
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@TipoCuentaID", id)
            };

            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_TIPO_CUENTA_POR_ID", parametros);

            if (t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                tipoCuenta = new TipoCuenta
                {
                    TipoCuentaID = (int)row["TipoCuentaID"],
                    Nombre = row["Nombre"].ToString()
                };
            }
            return tipoCuenta;
        }

        public List<TipoCuenta> GetAll()
        {
            List<TipoCuenta> tiposCuenta = new List<TipoCuenta>();
            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_TIPOS_CUENTA", null);

            foreach (DataRow row in t.Rows)
            {
                TipoCuenta tipoCuenta = new TipoCuenta
                {
                    TipoCuentaID = (int)row["TipoCuentaID"],
                    Nombre = row["Nombre"].ToString()
                };
                tiposCuenta.Add(tipoCuenta);
            }
            return tiposCuenta;
        }

        public bool Save(TipoCuenta tipoCuenta)
        {
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@Nombre", tipoCuenta.Nombre)
            };

            int rowsAffected = DataHelper.GetInstance().ExecuteSPDML("SP_INSERTAR_TIPO_CUENTA", parametros);
            return rowsAffected > 0;
        }
    }
}

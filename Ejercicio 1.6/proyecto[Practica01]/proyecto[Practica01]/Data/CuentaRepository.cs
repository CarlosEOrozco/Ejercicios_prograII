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
    public class CuentaRepository : ICuentaRepository
    {
        public Cuenta GetById(int id)
        {
            Cuenta cuenta = null;
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@CuentaID", id)
            };

            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_CUENTA_POR_ID", parametros);

            if (t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                cuenta = new Cuenta
                {
                    CuentaID = (int)row["CuentaID"],
                    CBU = row["CBU"].ToString(),
                    Saldo = (decimal)row["Saldo"],
                    TipoCuentaID = (int)row["TipoCuentaID"],
                    UltimoMovimiento = (DateTime)row["UltimoMovimiento"],
                    ClienteID = (int)row["ClienteID"]
                };
            }
            return cuenta;
        }

        public List<Cuenta> GetByClienteId(int clienteId)
        {
            List<Cuenta> cuentas = new List<Cuenta>();
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@ClienteID", clienteId)
            };

            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_CUENTAS_POR_CLIENTE", parametros);

            foreach (DataRow row in t.Rows)
            {
                Cuenta cuenta = new Cuenta
                {
                    CuentaID = (int)row["CuentaID"],
                    CBU = row["CBU"].ToString(),
                    Saldo = (decimal)row["Saldo"],
                    TipoCuentaID = (int)row["TipoCuentaID"],
                    UltimoMovimiento = (DateTime)row["UltimoMovimiento"],
                    ClienteID = (int)row["ClienteID"]
                };
                cuentas.Add(cuenta);
            }
            return cuentas;
        }

        public bool Save(Cuenta cuenta)
        {
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@CBU", cuenta.CBU),
                new ParameterSQL("@Saldo", cuenta.Saldo),
                new ParameterSQL("@TipoCuentaID", cuenta.TipoCuentaID),
                new ParameterSQL("@UltimoMovimiento", cuenta.UltimoMovimiento),
                new ParameterSQL("@ClienteID", cuenta.ClienteID)
            };

            int rowsAffected = DataHelper.GetInstance().ExecuteSPDML("SP_INSERTAR_CUENTA", parametros);
            return rowsAffected > 0;
        }
    }
}

using Ejecicio1_5.Data.Utils;
using Ejecicio1_5.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecicio1_5.Data
{
    public class FormaPagoRepository : IFormaPagoRepository
    {
        private readonly DataHelper _dataHelper;

        public FormaPagoRepository()
        {
            _dataHelper = DataHelper.GetInstance();
        }

        public void GuardarFormaPago(FormaPago formaPago)
        {
            List<ParameterSQL> parametros = new List<ParameterSQL>
            {
            new ParameterSQL("@nombre", formaPago.Nombre)
            };

            _dataHelper.ExecuteSPDML("sp_GuardarFormaPago", parametros);
        }

        public List<FormaPago> ListarFormasPago()
        {
            var dataTable = _dataHelper.ExecuteSPQuery("sp_ListarFormasPago", null);
            List<FormaPago> formasPago = new List<FormaPago>();

            foreach (DataRow row in dataTable.Rows)
            {
                FormaPago formaPago = new FormaPago
                {
                    Id = Convert.ToInt32(row["id"]),
                    Nombre = row["nombre"].ToString()
                };

                formasPago.Add(formaPago);
            }

            return formasPago;
        }

        public FormaPago ObtenerFormaPago(int id)
        {
            List<ParameterSQL> parametros = new List<ParameterSQL>
            {
            new ParameterSQL("@id", id)
            };

            var dataTable = _dataHelper.ExecuteSPQuery("sp_ObtenerFormaPago", parametros);
            if (dataTable == null || dataTable.Rows.Count == 0) return null;

            DataRow row = dataTable.Rows[0];

            return new FormaPago
            {
                Id = Convert.ToInt32(row["id"]),
                Nombre = row["nombre"].ToString()
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Capas
using BLLv2;
using DALv2;
namespace CrudDeUbicaciones_HACB
{
    public partial class frmUbicaciones : System.Web.UI.Page
    {
        ubicaciones_BLL oUbicacionesBLL;
        ubicacionesDAL oUbicacionesDAL;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarUbicaciones();

            }
        }
        //Método encargado de listar los datos de la BD en un GRIDView
        public void ListarUbicaciones()
        {
            oUbicacionesDAL = new ubicacionesDAL();
            gvUbicaciones.DataSource = oUbicacionesDAL.Listar();
            gvUbicaciones.DataBind();
        }
        //Método encargado de recolectar los datos de nuestra interfáz
        public ubicaciones_BLL datosUbicacion()
        {
            int ID = 0;
            int.TryParse(txtID.Value, out ID);
            oUbicacionesBLL = new ubicaciones_BLL();

            //Recolectar datos de la capa de presentación
            oUbicacionesBLL.ID = ID;
            oUbicacionesBLL.Ubicacion = txtUbicacion.Text;
            oUbicacionesBLL.Latitud=txtLat.Text;
            oUbicacionesBLL.Longitud = txtLong.Text;

            return oUbicacionesBLL;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            oUbicacionesDAL = new ubicacionesDAL();
            oUbicacionesDAL.Agregar(datosUbicacion());
            ListarUbicaciones(); //Para mostrarlo en el GV
        }
    }
}
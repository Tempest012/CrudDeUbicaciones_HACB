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

            lblMensaje.Text = "Se agrego de manera exitosa";
        }

        protected void gvUbicaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnSeleccionar")
            {
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvUbicaciones.Rows[rowIndex];
                lblMensaje.Text = "ID seleccionado: " + hfIDSeleccionado.Value;

                if (int.TryParse(row.Cells[0].Text, out int idSeleccionado))
                {
                    hfIDSeleccionado.Value = idSeleccionado.ToString();
                    lblMensaje.Text = "ID seleccionado: " + idSeleccionado;
                }
                else
                {
                    lblMensaje.Text = "El valor de ID no es válido";
                }
            }
        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            oUbicacionesDAL = new ubicacionesDAL();

            
            if (!string.IsNullOrEmpty(hfIDSeleccionado.Value) && int.TryParse(hfIDSeleccionado.Value, out int idSeleccionado))
            {
                
                ubicaciones_BLL ubicacion = new ubicaciones_BLL
                {
                    ID = idSeleccionado,
                    Ubicacion = txtUbicacion.Text,  
                    Latitud = txtLat.Text,
                    Longitud = txtLong.Text
                };

                bool resultado = oUbicacionesDAL.Modificar(ubicacion);

                if (resultado)
                {
                    ListarUbicaciones();
                    lblMensaje.Text = "Modificación exitosa";
                }
                else
                {
                    lblMensaje.Text = "Hubo un problema al modificar la ubicación";
                }
            }
            else
            {
                lblMensaje.Text = "ID no válido Selecciona una fila primero";
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            oUbicacionesDAL = new ubicacionesDAL();
            
            if(!string.IsNullOrEmpty(hfIDSeleccionado.Value) && int.TryParse(hfIDSeleccionado.Value, out int idSeleccionado))
            {
                ubicaciones_BLL ubicacion = new ubicaciones_BLL
                {
                    ID = idSeleccionado
                };

                bool resultado=oUbicacionesDAL.Eliminar(ubicacion);

                if (resultado)
                {
                    ListarUbicaciones();
                    lblMensaje.Text = "Ubicación eliminada exitosamente";
                }
                else
                {
                    lblMensaje.Text = "Hubo un problema al eliminar la ubicación";
                }
            }
            else
            {
                lblMensaje.Text = "ID no válido Selecciona una fila primero";
            }

            
        }
    }
}
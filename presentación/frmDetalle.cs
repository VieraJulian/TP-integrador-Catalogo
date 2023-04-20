using Domain;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace presentación
{
    public partial class frmDetalle : Form
    {
        private Articulo articulo;
        public frmDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void frmDetalle_Load(object sender, EventArgs e)
        {
            lblCodigoDetalle.Text = articulo.Codigo;
            lblNombreDetalle.Text = articulo.Nombre;
            lblDescripcionDetalle.Text = articulo.Descripcion;
            lblPrecioDetalle.Text = articulo.Precio.ToString();
            lblCategoriaDetalle.Text = articulo.Categoria.Descripcion;
            lblMarcaDetalle.Text = articulo.Marca.Descripcion;

            try
            {
                pbxArticuloDetalle.Load(articulo.ImagenUrl);
            }
            catch (Exception ex)
            {
                pbxArticuloDetalle.Load("https://talentclick.com/wp-content/uploads/2021/08/placeholder-image.png");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                Articulo articuloBorrar = articulo;
                DialogResult respuesta = MessageBox.Show("¿Quieres eliminar este artículo: " + '"' + articuloBorrar.Nombre + '"' + ", de manera permanente?", "Eliminar Artículo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminar(articuloBorrar.Id);
                    Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
    }
}

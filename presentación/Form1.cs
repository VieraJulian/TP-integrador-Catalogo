using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Domain;

namespace presentación
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulos;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.listar();
            dgvArticulos.DataSource = listaArticulos;
            dgvArticulos.Columns["imagenUrl"].Visible = false;
            dgvArticulos.Columns["id"].Visible = false;

            cargarImagen(listaArticulos[0].ImagenUrl);
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(articuloSeleccionado.ImagenUrl);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticle.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticle.Load("https://talentclick.com/wp-content/uploads/2021/08/placeholder-image.png");
            }
        }
    }
}

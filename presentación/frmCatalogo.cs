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
    public partial class frmCatalogo : Form
    {
        private List<Articulo> listaArticulos;

        public frmCatalogo()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            cargar();

            cboCampo.Items.Add("Sin seleccionar");
            cboCampo.Items.Add("Código");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Precio");
            cboCampo.SelectedIndex = 0;

            List<Marca> marcas = marcaNegocio.listar();
            cboMarca.Items.Add("Sin seleccionar");
            cboMarca.SelectedIndex = 0;
            marcas.ForEach(m => cboMarca.Items.Add(m));

            List<Categoria> categorias = categoriaNegocio.listar();
            cboCategoria.Items.Add("Sin seleccionar");
            cboCategoria.SelectedIndex = 0;
            categorias.ForEach(c => cboCategoria.Items.Add(c));

        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(articuloSeleccionado.ImagenUrl);
            }
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
                dgvArticulos.Columns["imagenUrl"].Visible = false;
                dgvArticulos.Columns["id"].Visible = false;

                cargarImagen(listaArticulos[0].ImagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarArticulo agregar = new frmAgregarArticulo();
            agregar.ShowDialog();

            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmAgregarArticulo modificar = new frmAgregarArticulo(seleccionado);
            modificar.ShowDialog();

            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;

            try
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                DialogResult respuesta = MessageBox.Show("¿Quieres eliminar este artículo: " + '"' + seleccionado.Nombre + '"' + ", de manera permanente?", "Eliminar Artículo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();

            if (opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Igual a");
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.SelectedIndex = 0;
            }
            else if (opcion == "Sin seleccionar")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Seleccione un campo");
                cboCriterio.SelectedIndex = 0;
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Empieza con");
                cboCriterio.Items.Add("Contiene");
                cboCriterio.SelectedIndex = 0;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtBuscador.Text;
                string marca = cboMarca.SelectedItem.ToString();
                string categoria = cboCategoria.SelectedItem.ToString();

                dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro);

            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

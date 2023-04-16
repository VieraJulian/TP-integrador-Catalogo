using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DataAccess;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            DatabaseConnector datos = new DatabaseConnector();

            try
            {
                datos.setearConsulta("SELECT codigo, nombre, ARTICULOS.descripcion, imagenUrl, precio, MARCAS.Descripcion as marca, CATEGORIAS.Descripcion as categoria from ARTICULOS INNER JOIN MARCAS ON MARCAS.Id = ARTICULOS.IdMarca INNER JOIN CATEGORIAS ON CATEGORIAS.Id = ARTICULOS.IdCategoria;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo article = new Articulo();
                    article.Codigo = (string)datos.Lector["codigo"];
                    article.Nombre = (string)datos.Lector["nombre"];
                    article.Descripcion = (string)datos.Lector["descripcion"];
                    article.ImagenUrl = (string)datos.Lector["imagenUrl"];
                    article.Precio = (decimal)datos.Lector["precio"];
                    article.Marca = new Marca();
                    article.Marca.Descripcion = (string)datos.Lector["marca"];
                    article.Categoria = new Categoria();
                    article.Categoria.Descripcion = (string)datos.Lector["categoria"];

                    lista.Add(article);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Articulo articulo)
        {
            DatabaseConnector datos = new DatabaseConnector();

            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio) VALUES (@Codigo, @Nombre, @Descripcion, @Precio);");
                
                datos.setearParametro("@Codigo", articulo.Codigo);
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@Precio", articulo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}

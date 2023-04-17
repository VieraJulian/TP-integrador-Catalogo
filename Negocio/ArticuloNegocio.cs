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
                datos.setearConsulta("SELECT ARTICULOS.Id, codigo, nombre, ARTICULOS.descripcion, imagenUrl, precio, MARCAS.Descripcion as marca, CATEGORIAS.Descripcion as categoria, idMarca, idCategoria from ARTICULOS INNER JOIN MARCAS ON MARCAS.Id = ARTICULOS.IdMarca INNER JOIN CATEGORIAS ON CATEGORIAS.Id = ARTICULOS.IdCategoria;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo article = new Articulo();
                    article.Id = (int)datos.Lector["Id"];
                    article.Codigo = (string)datos.Lector["codigo"];
                    article.Nombre = (string)datos.Lector["nombre"];
                    article.Descripcion = (string)datos.Lector["descripcion"];

                    if (!(datos.Lector["imagenUrl"] is DBNull))
                    {
                        article.ImagenUrl = (string)datos.Lector["imagenUrl"];

                    }

                    article.Precio = (decimal)datos.Lector["precio"];
                    article.Marca = new Marca();
                    article.Marca.Id = (int)datos.Lector["idMarca"];
                    article.Marca.Descripcion = (string)datos.Lector["marca"];
                    article.Categoria = new Categoria();
                    article.Categoria.Id = (int)datos.Lector["idCategoria"];
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
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria, ImagenUrl) VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @IdMarca, @IdCategoria, @ImagenUrl);");
                
                datos.setearParametro("@Codigo", articulo.Codigo);
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@IdMarca", articulo.Marca.Id);
                datos.setearParametro("@IdCategoria", articulo.Categoria.Id);
                datos.setearParametro("@ImagenUrl", articulo.ImagenUrl);

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

        public void modificar(Articulo articulo)
        {
            DatabaseConnector datos = new DatabaseConnector();

            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio WHERE Id = @Id;");

                datos.setearParametro("@Codigo", articulo.Codigo);
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@IdMarca", articulo.Marca.Id);
                datos.setearParametro("@IdCategoria", articulo.Categoria.Id);
                datos.setearParametro("@ImagenUrl", articulo.ImagenUrl);
                datos.setearParametro("@Id", articulo.Id);

                datos.ejecutarAccion();
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
    }
}

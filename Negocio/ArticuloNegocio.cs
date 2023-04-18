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

        public void eliminar(int id)
        {
            DatabaseConnector datos = new DatabaseConnector();

            try
            {
                datos.setearConsulta("DELETE FROM ARTICULOS WHERE Id = @Id;");
                datos.setearParametro("@Id", id);

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

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            DatabaseConnector datos = new DatabaseConnector();

            try
            {
                string consulta = "SELECT ARTICULOS.Id, codigo, nombre, ARTICULOS.descripcion, imagenUrl, precio, MARCAS.Descripcion as marca, CATEGORIAS.Descripcion as categoria, idMarca, idCategoria from ARTICULOS INNER JOIN MARCAS ON MARCAS.Id = ARTICULOS.IdMarca INNER JOIN CATEGORIAS ON CATEGORIAS.Id = ARTICULOS.IdCategoria WHERE ";

                if (campo == "Código")
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "codigo like '" + filtro + "%'";
                            break;
                        case "Contiene":
                            consulta += "codigo like '%" + filtro + "%'";
                            break;
                    }
                }

                if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "nombre like '" + filtro + "%'";
                            break;
                        case "Contiene":
                            consulta += "nombre like '%" + filtro + "%'";
                            break;
                    }
                }

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Igual a":
                            consulta += "precio = " + filtro;
                            break;
                        case "Mayor a":
                            consulta += "precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "precio < " + filtro;
                            break;
                    }
                }

                if (campo == "Sin seleccionar")
                {
                    consulta = "SELECT ARTICULOS.Id, codigo, nombre, ARTICULOS.descripcion, imagenUrl, precio, MARCAS.Descripcion as marca, CATEGORIAS.Descripcion as categoria, idMarca, idCategoria from ARTICULOS INNER JOIN MARCAS ON MARCAS.Id = ARTICULOS.IdMarca INNER JOIN CATEGORIAS ON CATEGORIAS.Id = ARTICULOS.IdCategoria;";
                }

                datos.setearConsulta(consulta);
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
        }
    }
}

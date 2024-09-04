using ProductosCRUD.Entities;
using System.Data;
using System.Data.SqlClient;

namespace ProductosCRUD.Services
{
    public class ProductoService
    {
        private readonly string _dbConnection;

        public ProductoService(IConfiguration configuration)
        {
            _dbConnection = configuration.GetConnectionString("connectionSQL")!;
        }

        public async Task<List<Producto>> Get()
        {
            List<Producto> productos = new List<Producto>();

            using (var con = new SqlConnection(_dbConnection))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_getProductos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        productos.Add(new Producto
                        {
                            IdProducto = Convert.ToInt32(reader["IdProducto"]),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            CantidadStock = Convert.ToInt32(reader["CantidadStock"])
                        });
                    }
                }
            }

            return productos;
        }

        public async Task<Producto> GetById(int Id)
        {
            Producto producto = new Producto();

            using (var con = new SqlConnection(_dbConnection))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SP_getByIdProducto", con);
                cmd.Parameters.AddWithValue("@IdProducto", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        producto = new Producto
                        {
                            IdProducto = Convert.ToInt32(reader["IdProducto"]),
                            Nombre = reader["Nombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            CantidadStock = Convert.ToInt32(reader["CantidadStock"])
                        };
                    }
                }
            }

            return producto;
        }
        public async Task<bool> Create(Producto producto)
        {
            bool result = true;

            using (var con = new SqlConnection(_dbConnection))
            {
                SqlCommand cmd = new SqlCommand("SP_createProducto", con);
                cmd.Parameters.AddWithValue("@NombreProducto", producto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@CantidadStock", producto.CantidadStock);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    result = await cmd.ExecuteNonQueryAsync()> 0 ? true: false;
                }
                catch {
                    result = false;
                }
            }

            return result;
        }
        public async Task<bool> Edit(Producto producto)
        {
            bool result = true;

            using (var con = new SqlConnection(_dbConnection))
            {
                SqlCommand cmd = new SqlCommand("SP_editProducto", con);
                cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                cmd.Parameters.AddWithValue("@NombreProducto", producto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@CantidadStock", producto.CantidadStock);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    result = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        public async Task<bool> Delete(int Id)
        {
            bool result = true;

            using (var con = new SqlConnection(_dbConnection))
            {
                SqlCommand cmd = new SqlCommand("SP_deleteProducto", con);
                cmd.Parameters.AddWithValue("@IdProducto", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    result = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }



    }
}

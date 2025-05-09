using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace inventario
{
    internal class conexiones
    {
        private OleDbConnection conexion;
        private DataGridView Grilla;

        public void Abrirconexiones(DataGridView Grilla)
        {
            try
            {
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Tomcat\\Desktop\\trabajos tomi\\inventario\\inventario\\InventarioProductos.accdb";

                conexion = new OleDbConnection(connectionString);
                conexion.Open();
                MessageBox.Show("Conectado.");

                this.Grilla = Grilla;

                CargarProductosEnGrilla();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }

        }
        public void CargarProductosEnGrilla()
        {
            if (conexion == null || Grilla == null) return;

            string query = "SELECT * FROM productos ORDER BY codigo";

            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conexion);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            Grilla.DataSource = dt;
        }


        public void Nuevoproducto(int codigo, string nombre, string descripcion, int precio, int stock, string categoria)
        {
            try
            {
                string query = "INSERT INTO Productos (Codigo, Nombre, Descripcion, Precio, Stock, IDcategoria) " +
                               "VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @Stock, @Categoria)";
                using (OleDbCommand cmd = new OleDbCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@Categoria", categoria);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Producto agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProductosEnGrilla(); // Opcional: refresca la grilla
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        public void Modificarproductos(int codigo, string nombre, string descripcion, int precio, int stock, string categoria)
        {
            try
            {
                if (conexion == null || conexion.State != ConnectionState.Open)
                {
                    MessageBox.Show("NO ESTA CONECTADA LA BASE"); 
                    return;
                }

                string query = "UPDATE productos SET Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, Stock = @Stock, IDcategoria = @Categoria WHERE Codigo = @Codigo";
                using (OleDbCommand cmd = new OleDbCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@Categoria", categoria);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Producto modificado");
                       
                    }
                    else
                    {
                        MessageBox.Show("No se encuentra producto con ese codigo");
                    }
                }
            }
            catch (Exception excp)
            {
                MessageBox.Show("Error al modificar el producto: " + excp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Buscar(string buscar)
        {
            try
            {
                if (String.IsNullOrEmpty(buscar))
                {
                    
                    return;
                }
                string query = "SELECT * FROM productos WHERE nombre LIKE @Buscar OR descripcion LIKE @Buscar OR codigo LIKE @Buscar OR IDcategoria LIKE @Buscar";
                using (OleDbCommand cmd = new OleDbCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Buscar", "%" + buscar + "%");
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    Grilla.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("busqueda equivocada: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }


    }
}    

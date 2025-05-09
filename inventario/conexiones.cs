using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventario
{
    internal class conexiones
    {
        private OleDbConnection conexion;
        private DataGridView Grilla;

        public void inicializar(DataGridView tabla)
        {
            try
            {
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Tomcat\\Documents\\InventarioProductos.accdb";

                conexion = new OleDbConnection(connectionString);
                conexion.Open();
                MessageBox.Show("Conectado.");

                this.Grilla = Grilla; 

                CargarDatos();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error" );
            }

        }
        public void CargarDatos()
        {
            if (conexion == null || Grilla == null) return;

            string query = "SELECT * FROM productos ORDER BY codigo";

            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conexion);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            Grilla.DataSource = dt;
        }

    }
}    

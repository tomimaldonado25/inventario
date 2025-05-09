using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventario
{
    public partial class FormInventario : Form
    {
        private conexiones conexion;
        private object numPrecio;

        public FormInventario()
        {
            InitializeComponent();
            conexion = new conexiones();
        
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            conexion.Abrirconexiones(dgvProductos);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                int codigo = Convert.ToInt32(txtCodigo.Text);
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                int precio = Convert.ToInt32(txtPrecio.Text);
                int stock = Convert.ToInt32(txtStock.Text);
                string categoria = txtCategoria.Text.Trim();

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(categoria))
                {
                    MessageBox.Show("Todos los campos deben estar completos.");
                    return;
                }

                conexion.Nuevoproducto(codigo, nombre, descripcion, precio, stock, categoria);
              
                conexion.CargarProductosEnGrilla();
                
                txtCodigo.Clear();
                txtNombre.Clear();
                txtDescripcion.Clear();
                txtPrecio.Clear();
                txtStock.Clear();
                txtCategoria.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("Verificá que Código, Precio y Stock sean números válidos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscar.Text;

            conexion.Buscar(busqueda);

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigo = Convert.ToInt32(txtCodigo.Text);
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                int precio = Convert.ToInt32(txtPrecio.Text);
                int stock = Convert.ToInt32(txtStock.Text);
                string categoria = txtCategoria.Text.Trim();

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(categoria))
                {
                    MessageBox.Show("Debes completar los campos .");
                    return;
                }

                conexion.Modificarproductos(codigo, nombre, descripcion, precio, stock, categoria);
                conexion.CargarProductosEnGrilla();

                MessageBox.Show("Producto modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos
                txtCodigo.Clear();
                txtNombre.Clear();
                txtDescripcion.Clear();
                txtPrecio.Clear();
                txtStock.Clear();
                txtCategoria.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("Código, Precio y Stock deben ser números válidos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

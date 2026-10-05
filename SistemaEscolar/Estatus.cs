using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaEscolar
{
    public partial class Estatus : Form
    {
        // Conexión a la base de datos
        public string ConexionCatalogos = "Server=localhost\\SQLEXPRESS;"
                                    + " Database=Catalogos;"
                                    + " Integrated Security=True; "
                                    + " TrustServerCertificate=True";

        // Almacena el ID del estatus seleccionado
        private int idEstatusSeleccionado = 0;

        public Estatus()
        {
            InitializeComponent();
        }
        private void CargarEstatus()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();

                    // CONSULTA SQL para la tabla Estatus
                    string consulta = "SELECT [id_estatus], [clave_estatus], [nombre_estatus], [usuario]" +
                                      " FROM [Catalogos].[dbo].[Estatus]";

                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tablaEstatus = new DataTable();

                    adaptador.Fill(tablaEstatus);

                    // Carga datos ald dgv
                    dgvEstatus.DataSource = tablaEstatus;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de los estatus: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();

                    // Consulta INSERT para la tabla Estatus
                    string consulta = "INSERT INTO Estatus (clave_estatus, nombre_estatus, usuario, fecha_hora_creacion) " +
                                      "VALUES (@clave, @nombre, @usuario, GETDATE())";

                    SqlCommand comando = new SqlCommand(consulta, conexion);

                    comando.Parameters.AddWithValue("@clave", txtClaveEstatus.Text);
                    comando.Parameters.AddWithValue("@nombre", txtNombreEstatus.Text);
                    comando.Parameters.AddWithValue("@usuario", txtUsuario.Text);

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Estatus agregado correctamente.");
                }

                CargarEstatus(); // Recargar la tabla
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el estatus: " + ex.Message);
            }
        }

        private void dgvEstatus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEstatus.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvEstatus.SelectedRows[0];

                // Copia los valores de las celdas a los TextBoxes.
                txtClaveEstatus.Text = fila.Cells["clave_estatus"].Value.ToString();
                txtNombreEstatus.Text = fila.Cells["nombre_estatus"].Value.ToString();
                txtUsuario.Text = fila.Cells["usuario"].Value.ToString();

                // Almacena el ID.
                idEstatusSeleccionado = Convert.ToInt32(fila.Cells["id_estatus"].Value);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEstatus.SelectedRows.Count > 0)
            {
                try
                {
                    // Obtiene el ID.
                    int idParaEditar = Convert.ToInt32(dgvEstatus.SelectedRows[0].Cells["id_estatus"].Value);

                    using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                    {
                        conexion.Open();
                        // Consulta UPDATE para la tabla Estatus
                        string consulta = "UPDATE Estatus SET clave_estatus = @clave, nombre_estatus = @nombre, usuario = @usuario WHERE id_estatus = @id";

                        SqlCommand comando = new SqlCommand(consulta, conexion);

                        comando.Parameters.AddWithValue("@clave", txtClaveEstatus.Text);
                        comando.Parameters.AddWithValue("@nombre", txtNombreEstatus.Text);
                        comando.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                        comando.Parameters.AddWithValue("@id", idParaEditar);

                        comando.ExecuteNonQuery();
                        MessageBox.Show("Estatus actualizado correctamente.");
                    }

                    CargarEstatus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el estatus en la base de datos: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa para editar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEstatus.SelectedRows.Count > 0)
            {
                try
                {
                    // Obtiene el ID del estatus.
                    int idEliminar = Convert.ToInt32(dgvEstatus.SelectedRows[0].Cells["id_estatus"].Value);

                    var resultado = MessageBox.Show("¿Está seguro de que desea eliminar este estatus?", "Confirmar eliminación", MessageBoxButtons.YesNo);

                    if (resultado == DialogResult.Yes)
                    {
                        using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                        {
                            conexion.Open();
                            // Consulta DELETE para la tabla Estatus.
                            string consulta = "DELETE FROM Estatus WHERE id_estatus = @id";

                            SqlCommand comando = new SqlCommand(consulta, conexion);
                            comando.Parameters.AddWithValue("@id", idEliminar);

                            comando.ExecuteNonQuery();
                            MessageBox.Show("Estatus eliminado correctamente.");
                        }

                        CargarEstatus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el estatus: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa para eliminar.");
                return;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Estatus_Load(object sender, EventArgs e)
        {
            CargarEstatus();
        }
    }
}
           
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
    public partial class Carrera : Form
    {
        // Conexión a la base de datos 
        public string ConexionCatalogos = "Server=localhost\\SQLEXPRESS;"
                                    + " Database=Catalogos;"
                                    + " Integrated Security=True; "
                                    + " TrustServerCertificate=True";

        // Almacena el ID de la carrera seleccionada.
        private int idCarreraSeleccionada = 0;

        public Carrera()
        {
            InitializeComponent();
        }
        private void CargarCarreras()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();

                    // CONSULTA SQL para la tabla Carrera
                    string consulta = "SELECT [id_carrera], [nombre_carrera], [siglas_carrera]" +
                                      " FROM [Catalogos].[dbo].[Carrera]";

                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);

                    DataTable tablaCarreras = new DataTable();

                    adaptador.Fill(tablaCarreras);

                    // Cargar datos al dgv
                    dgvCarrera.DataSource = tablaCarreras;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de las carreras: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();

                    // Consulta INSERT para la tabla Carrera
                    string consulta = "INSERT INTO Carrera (nombre_carrera, siglas_carrera, fecha_hora_creacion) " +
                                      "VALUES (@nombre, @siglas, GETDATE())";

                    SqlCommand comando = new SqlCommand(consulta, conexion);

                    // Parámetros usando los TextBox de la carrera.
                    comando.Parameters.AddWithValue("@nombre", txtNombreCarrera.Text);
                    comando.Parameters.AddWithValue("@siglas", txtSiglasCarrera.Text); 

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Carrera agregada correctamente.");
                }

                CargarCarreras(); // Recargar la tabla
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la carrera: " + ex.Message);
            }
        }

        private void dgvCarrera_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCarrera.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvCarrera.SelectedRows[0];

                // Copia los valores de las celdas de Carrera a los TextBox
                txtNombreCarrera.Text = fila.Cells["nombre_carrera"].Value.ToString();
                txtSiglasCarrera.Text = fila.Cells["siglas_carrera"].Value.ToString();

                // Almacena el ID de la carrera seleccionada
                idCarreraSeleccionada = Convert.ToInt32(fila.Cells["id_carrera"].Value);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCarrera.SelectedRows.Count > 0)
            {
                try
                {
                    int idParaEditar = Convert.ToInt32(dgvCarrera.SelectedRows[0].Cells["id_carrera"].Value);

                    using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                    {
                        conexion.Open();
                        // Consulta UPDATE para la tabla Carrera usando siglas_carrera
                        string consulta = "UPDATE Carrera SET nombre_carrera = @nombre, siglas_carrera = @siglas WHERE id_carrera = @id";

                        SqlCommand comando = new SqlCommand(consulta, conexion);
                        comando.Parameters.AddWithValue("@nombre", txtNombreCarrera.Text);
                        comando.Parameters.AddWithValue("@siglas", txtSiglasCarrera.Text);
                        comando.Parameters.AddWithValue("@id", idParaEditar);

                        comando.ExecuteNonQuery();
                        MessageBox.Show("Carrera actualizada correctamente.");
                    }

                    CargarCarreras();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la carrera en la base de datos: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa para editar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCarrera.SelectedRows.Count > 0)
            {
                try
                {
                    int idEliminar = Convert.ToInt32(dgvCarrera.SelectedRows[0].Cells["id_carrera"].Value);

                    var resultado = MessageBox.Show("¿Está seguro de que desea eliminar esta carrera?", "Confirmar eliminación", MessageBoxButtons.YesNo);

                    if (resultado == DialogResult.Yes)
                    {
                        using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                        {
                            conexion.Open();
                            // Consulta DELETE para la tabla Carrera
                            string consulta = "DELETE FROM Carrera WHERE id_carrera = @id";

                            SqlCommand comando = new SqlCommand(consulta, conexion);
                            comando.Parameters.AddWithValue("@id", idEliminar);

                            comando.ExecuteNonQuery();
                            MessageBox.Show("Carrera eliminada correctamente.");
                        }

                        CargarCarreras();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la carrera: " + ex.Message);
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

        private void Carrera_Load(object sender, EventArgs e)
        {
            CargarCarreras(); 
        }
    }
}


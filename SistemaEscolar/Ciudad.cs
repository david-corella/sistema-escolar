using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace SistemaEscolar
{
    public partial class Ciudad : Form
    {
        // Conexión a la base de datos
        public string ConexionCatalogos = "Server=localhost\\SQLEXPRESS;"
                                       + " Database=Catalogos;"
                                       + " Integrated Security=True; "
                                       + " TrustServerCertificate=True";

        public Ciudad()
        {
            InitializeComponent();
        }

        private void Ciudad_Load(object sender, EventArgs e)
        {
            CargarEstadosEnComboBox();
            CargarCiudades();
        }

        private void CargarEstadosEnComboBox()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();
                    string consulta = "SELECT id_estado, nombre_estado FROM Estado";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tablaEstados = new DataTable();
                    adaptador.Fill(tablaEstados);

                    // Configuración del ComboBox
                    cbEstado.DataSource = tablaEstados;
                    cbEstado.DisplayMember = "nombre_estado"; // El texto que ve el usuario
                    cbEstado.ValueMember = "id_estado";       // El valor interno que se guardará
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de estados: " + ex.Message);
            }
        }

        private void CargarCiudades()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();
                    // Consulta que une las tablas Ciudad y Estado para mostrar el nombre del estado
                    string consulta = @"SELECT c.id_ciudad, 
                                               c.nombre_ciudad, 
                                               c.siglas_ciudad, 
                                               e.nombre_estado 
                                          FROM Ciudad c
                                          JOIN Estado e ON c.id_estado = e.id_estado";

                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tablaCiudades = new DataTable();
                    adaptador.Fill(tablaCiudades);
                    dgvCiudad.DataSource = tablaCiudades;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de las ciudades: " + ex.Message);
            }
        }

        private void dgvCiudad_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCiudad.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvCiudad.SelectedRows[0];

                txtNombreCiudad.Text = fila.Cells["nombre_ciudad"].Value.ToString();
                txtSiglasCiudad.Text = fila.Cells["siglas_ciudad"].Value.ToString();

                // Hacemos que el ComboBox seleccione el estado correcto
                cbEstado.Text = fila.Cells["nombre_estado"].Value.ToString();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();
                    string consulta = "INSERT INTO Ciudad (nombre_ciudad, siglas_ciudad, id_estado, fecha_hora_creacion) VALUES (@nombre, @siglas, @id_estado, GETDATE())";
                    SqlCommand comando = new SqlCommand(consulta, conexion);

                    comando.Parameters.AddWithValue("@nombre", txtNombreCiudad.Text);
                    comando.Parameters.AddWithValue("@siglas", txtSiglasCiudad.Text);
                    // Obtenemos el ID del estado seleccionado en el ComboBox
                    comando.Parameters.AddWithValue("@id_estado", cbEstado.SelectedValue);

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Ciudad agregada correctamente.");
                }
                CargarCiudades();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la ciudad: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCiudad.SelectedRows.Count > 0)
            {
                try
                {
                    int idParaEditar = Convert.ToInt32(dgvCiudad.SelectedRows[0].Cells["id_ciudad"].Value);

                    using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                    {
                        conexion.Open();
                        string consulta = "UPDATE Ciudad SET nombre_ciudad = @nombre, siglas_ciudad = @siglas, id_estado = @id_estado WHERE id_ciudad = @id";
                        SqlCommand comando = new SqlCommand(consulta, conexion);

                        comando.Parameters.AddWithValue("@nombre", txtNombreCiudad.Text);
                        comando.Parameters.AddWithValue("@siglas", txtSiglasCiudad.Text);
                        comando.Parameters.AddWithValue("@id_estado", cbEstado.SelectedValue);
                        comando.Parameters.AddWithValue("@id", idParaEditar);

                        comando.ExecuteNonQuery();
                        MessageBox.Show("Ciudad actualizada correctamente.");
                    }
                    CargarCiudades();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar en la base de datos: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para editar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCiudad.SelectedRows.Count > 0)
            {
                int idCiudadSeleccionada = Convert.ToInt32(dgvCiudad.SelectedRows[0].Cells["id_ciudad"].Value);

                var resultado = MessageBox.Show("¿Está seguro de que desea eliminar esta ciudad?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                        {
                            conexion.Open();
                            string consulta = "DELETE FROM Ciudad WHERE id_ciudad = @id";
                            SqlCommand comando = new SqlCommand(consulta, conexion);
                            comando.Parameters.AddWithValue("@id", idCiudadSeleccionada);
                            comando.ExecuteNonQuery();
                            MessageBox.Show("Ciudad eliminada correctamente.");
                        }
                        CargarCiudades();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar la ciudad: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa para eliminar.");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

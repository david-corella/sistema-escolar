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
    public partial class Pais : Form
    {
        // Conexión a la base de datos
        public string ConexionCatalogos = "Server=localhost\\SQLEXPRESS;"
                                       + " Database=Catalogos;"
                                       + " Integrated Security=True; "
                                       + " TrustServerCertificate=True";
        private void Pais_Load(object sender, EventArgs e)
        {
            CargarPaises();
        }

        private void CargarPaises()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();
                    string consulta = "SELECT id_pais, nombre_pais, sigla_pais FROM Pais";

                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tablaPaises = new DataTable();
                    adaptador.Fill(tablaPaises);
                    dgvPais.DataSource = tablaPaises;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de los países: " + ex.Message);
            }
        }
        public Pais()
        {
            InitializeComponent();
        }

        private void dgvPais_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPais.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvPais.SelectedRows[0];

                txtNombrePais.Text = fila.Cells["nombre_pais"].Value.ToString();
                txtSiglaPais.Text = fila.Cells["sigla_pais"].Value.ToString();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();
                    string consulta = "INSERT INTO Pais (nombre_pais, sigla_pais, fecha_hora_creacion) VALUES (@nombre, @sigla, GETDATE())";
                    SqlCommand comando = new SqlCommand(consulta, conexion);

                    comando.Parameters.AddWithValue("@nombre", txtNombrePais.Text);
                    comando.Parameters.AddWithValue("@sigla", txtSiglaPais.Text);

                    comando.ExecuteNonQuery();
                    MessageBox.Show("País agregado correctamente.");
                }
                CargarPaises();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el país: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvPais.SelectedRows.Count > 0)
            {
                try
                {
                    int idParaEditar = Convert.ToInt32(dgvPais.SelectedRows[0].Cells["id_pais"].Value);

                    using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                    {
                        conexion.Open();
                        string consulta = "UPDATE Pais SET nombre_pais = @nombre, sigla_pais = @sigla WHERE id_pais = @id";
                        SqlCommand comando = new SqlCommand(consulta, conexion);

                        comando.Parameters.AddWithValue("@nombre", txtNombrePais.Text);
                        comando.Parameters.AddWithValue("@sigla", txtSiglaPais.Text);
                        comando.Parameters.AddWithValue("@id", idParaEditar);

                        comando.ExecuteNonQuery();
                        MessageBox.Show("País actualizado correctamente.");
                    }
                    CargarPaises();
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
            if (dgvPais.SelectedRows.Count > 0)
            {
                int idPaisSeleccionado = Convert.ToInt32(dgvPais.SelectedRows[0].Cells["id_pais"].Value);

                var resultado = MessageBox.Show("¿Está seguro de que desea eliminar este país?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                        {
                            conexion.Open();
                            string consulta = "DELETE FROM Pais WHERE id_pais = @id";
                            SqlCommand comando = new SqlCommand(consulta, conexion);
                            comando.Parameters.AddWithValue("@id", idPaisSeleccionado);
                            comando.ExecuteNonQuery();
                            MessageBox.Show("País eliminado correctamente.");
                        }
                        CargarPaises();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el país: " + ex.Message);
                    }
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

        
    }
}

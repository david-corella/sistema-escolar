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
    public partial class Aula : Form
    {
        // Conexión a la base de datos
        public string ConexionCatalogos = "Server=localhost\\SQLEXPRESS;"
                                       + " Database=Catalogos;"
                                       + " Integrated Security=True; "
                                       + " TrustServerCertificate=True";

        public Aula()
        {
            InitializeComponent();
        }

        private void Aula_Load(object sender, EventArgs e)
        {
            CargarAulas();
        }

        private void CargarAulas()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();
                    // Consulta para la tabla Aula.
                    string consulta = @"SELECT id_aula, 
                                               edificio, 
                                               aula, 
                                               piso, 
                                               capacidad_maxima 
                                          FROM Aula";

                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tablaAulas = new DataTable();
                    adaptador.Fill(tablaAulas);
                    // Asigna los datos al dgv
                    dgvAula.DataSource = tablaAulas;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de las aulas: " + ex.Message);
            }
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();
                    // Consulta INSERT adaptada para Aula.
                    string consulta = "INSERT INTO Aula (edificio, aula, piso, capacidad_maxima, fecha_hora_creacion) VALUES (@edificio, @aula, @piso, @capacidad, GETDATE())";
                    SqlCommand comando = new SqlCommand(consulta, conexion);

                    // Asigna valores desde los TextBox a los parámetros.
                    comando.Parameters.AddWithValue("@edificio", txtEdificio.Text);
                    comando.Parameters.AddWithValue("@aula", txtAula.Text);
                    comando.Parameters.AddWithValue("@piso", txtPiso.Text);
                    comando.Parameters.AddWithValue("@capacidad", int.Parse(txtCmaxima.Text));

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Aula agregada correctamente.");
                }
                CargarAulas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el aula: " + ex.Message);
            }
        }
        

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (dgvAula.SelectedRows.Count > 0)
            {

                try
                {
                    int idParaEditar = Convert.ToInt32(dgvAula.SelectedRows[0].Cells["IDAula"].Value);

                    using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                    {
                        conexion.Open();
                        string consulta = "UPDATE Aula SET edificio = @edificio, aula = @aula, piso = @piso, capacidad_maxima = @capacidad WHERE id_aula = @id";
                        SqlCommand comando = new SqlCommand(consulta, conexion);

                        comando.Parameters.AddWithValue("@edificio", txtEdificio.Text);
                        comando.Parameters.AddWithValue("@aula", txtAula.Text);
                        comando.Parameters.AddWithValue("@piso", txtPiso.Text);
                        comando.Parameters.AddWithValue("@capacidad", int.Parse(txtCmaxima.Text));
                        comando.Parameters.AddWithValue("@id", idParaEditar);

                        comando.ExecuteNonQuery();
                        MessageBox.Show("Aula actualizada correctamente.");
                    }
                    CargarAulas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar en la base de datos: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa para editar.");
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (dgvAula.SelectedRows.Count > 0)
            {
                int idAulaSeleccionada = Convert.ToInt32(dgvAula.SelectedRows[0].Cells["IDAula"].Value);

                var resultado = MessageBox.Show("¿Está seguro de que desea eliminar esta aula?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                        {
                            conexion.Open();
                            // Consulta DELETE adaptada para Aula.
                            string consulta = "DELETE FROM Aula WHERE id_aula = @id";
                            SqlCommand comando = new SqlCommand(consulta, conexion);
                            comando.Parameters.AddWithValue("@id", idAulaSeleccionada);
                            comando.ExecuteNonQuery();
                            MessageBox.Show("Aula eliminada correctamente.");
                        }
                        CargarAulas();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el aula: " + ex.Message);
                    }
                }

            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa para eliminar.");
                return;
            }
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvAula_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAula.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvAula.SelectedRows[0];

                
                    // Copia los valores de la fila seleccionada a los TextBox.
                    txtEdificio.Text = fila.Cells["edificio"].Value.ToString();
                    txtAula.Text = fila.Cells["aula"].Value.ToString();
                    txtPiso.Text = fila.Cells["piso"].Value.ToString();
                    txtCmaxima.Text = fila.Cells["capacidadMaxima"].Value.ToString();
                
            }
        }
    }
}

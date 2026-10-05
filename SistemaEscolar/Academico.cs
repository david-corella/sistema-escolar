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
    public partial class Academico : Form
    {
        // Conexión a la base de datos
        public string ConexionCatalogos = "Server=localhost\\SQLEXPRESS;"
                                       + " Database=Catalogos;"
                                       + " Integrated Security=True; "
                                       + " TrustServerCertificate=True";
        public Academico()
        {
            InitializeComponent();
        }

        private void Academico_Load_1(object sender, EventArgs e)
        {
            CargarAcademicos();
        }
        

        private void CargarAcademicos()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();

                    // Consulta adaptada para la tabla Academico
                    string consulta = @"SELECT id_academico, 
                                               nombre_academico, 
                                               apellidos_academicos, 
                                               grado
                                          FROM Academico";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tablaAcademicos = new DataTable();
                    adaptador.Fill(tablaAcademicos);

                    // Asigna los datos al dgv
                    dgvAcademico.DataSource = tablaAcademicos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de los académicos: " + ex.Message);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();
                    string consulta = "INSERT INTO Academico (nombre_academico, apellidos_academicos, grado, fecha_hora_creacion) VALUES (@nombre, @apellidos, @grado, GETDATE())";
                    SqlCommand comando = new SqlCommand(consulta, conexion);

                    comando.Parameters.AddWithValue("@nombre", txtNombreAcademicos.Text);
                    comando.Parameters.AddWithValue("@apellidos", txtApellidosAcademicos.Text);
                    comando.Parameters.AddWithValue("@grado", int.Parse(txtGradoAcademico.Text));

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Académico agregado correctamente.");
                }
                CargarAcademicos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar al académico: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvAcademico.SelectedRows.Count > 0)
            {
                try
                {
                    int idParaEditar = Convert.ToInt32(dgvAcademico.SelectedRows[0].Cells["id_academico"].Value);

                    using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                    {
                        conexion.Open();
                        string consulta = "UPDATE Academico SET nombre_academico = @nombre, apellidos_academicos = @apellidos, grado = @grado WHERE id_academico = @id";
                        SqlCommand comando = new SqlCommand(consulta, conexion);

                        comando.Parameters.AddWithValue("@nombre", txtNombreAcademicos.Text);
                        comando.Parameters.AddWithValue("@apellidos", txtApellidosAcademicos.Text);
                        comando.Parameters.AddWithValue("@grado", int.Parse(txtGradoAcademico.Text));
                        comando.Parameters.AddWithValue("@id", idParaEditar);

                        comando.ExecuteNonQuery();
                        MessageBox.Show("Académico actualizado correctamente.");
                    }
                    CargarAcademicos();
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
            if (dgvAcademico.SelectedRows.Count > 0)
            {
                int idAcademicoSeleccionado = Convert.ToInt32(dgvAcademico.SelectedRows[0].Cells["id_academico"].Value);

                var resultado = MessageBox.Show("¿Está seguro de que desea eliminar a este académico?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                        {
                            conexion.Open();
                            string consulta = "DELETE FROM Academico WHERE id_academico = @id";
                            SqlCommand comando = new SqlCommand(consulta, conexion);
                            comando.Parameters.AddWithValue("@id", idAcademicoSeleccionado);
                            comando.ExecuteNonQuery();
                            MessageBox.Show("Académico eliminado correctamente.");
                        }
                        CargarAcademicos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar al académico: " + ex.Message);
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

        private void dgvAcademico_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAcademico.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvAcademico.SelectedRows[0];

                txtNombreAcademicos.Text = fila.Cells["nombre_academico"].Value.ToString();
                txtApellidosAcademicos.Text = fila.Cells["apellidos_academicos"].Value.ToString();
                txtGradoAcademico.Text = fila.Cells["grado"].Value.ToString();
            }
        }

        
    }
}

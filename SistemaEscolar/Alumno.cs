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
    public partial class Alumno : Form
    {
        // Conexión a la base de datos
        public string ConexionCatalogos = "Server=localhost\\SQLEXPRESS;"
                                    + " Database=Catalogos;"
                                    + " Integrated Security=True; "
                                    + " TrustServerCertificate=True";

        // Variable para almacenar el ID del alumno seleccionado.
        private int idAlumnoSeleccionado = 0;

        public Alumno()
        {
            InitializeComponent();
        }
        
        private void CargarAlumnos()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();

                    // Consulta para la tabla Alumno
                    string consulta = "SELECT [id_alumno], [nombre_alumno], [apellidos_alumno], [estatus]" +
                                      " FROM [Catalogos].[dbo].[Alumno]";

                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tablaAlumnos = new DataTable();

                    adaptador.Fill(tablaAlumnos);

                    // Asignar datos al dgv
                    dgvAlumno.DataSource = tablaAlumnos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de los alumnos: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();

                    // Consulta INSERT para la tabla Alumno
                    string consulta = "INSERT INTO Alumno (nombre_alumno, apellidos_alumno, estatus, fecha_hora_creacion) " +
                                      "VALUES (@nombre, @apellidos, @estatus, GETDATE())";

                    SqlCommand comando = new SqlCommand(consulta, conexion);

                    // Parámetros usando los TextBox del alumno
                    comando.Parameters.AddWithValue("@nombre", txtNombreAlumno.Text);
                    comando.Parameters.AddWithValue("@apellidos", txtApellidosAlumno.Text);
                    comando.Parameters.AddWithValue("@estatus", txtEstatusAlumno.Text);

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Alumno agregado correctamente.");
                }

                CargarAlumnos(); // Recargar la tabla
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el alumno: " + ex.Message);
            }
        }

        private void dgvAlumno_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAlumno.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvAlumno.SelectedRows[0];

                // Copia los valores de las celdas del alumno a los TextBox 
                txtNombreAlumno.Text = fila.Cells["nombre_alumno"].Value.ToString();
                txtApellidosAlumno.Text = fila.Cells["apellidos_alumno"].Value.ToString();
                txtEstatusAlumno.Text = fila.Cells["estatus"].Value.ToString();

                // Almacena el ID del alumno seleccionado.
                idAlumnoSeleccionado = Convert.ToInt32(fila.Cells["id_alumno"].Value);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Revisar que haya algo seleccionado.
            if (dgvAlumno.SelectedRows.Count > 0)
            {
                try
                {
                    // Obtiene el ID directamente de la fila seleccionada.
                    int idParaEditar = Convert.ToInt32(dgvAlumno.SelectedRows[0].Cells["id_alumno"].Value);

                    using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                    {
                        conexion.Open();
                        // Consulta UPDATE para la tabla Alumno
                        string consulta = "UPDATE Alumno SET nombre_alumno = @nombre, apellidos_alumno = @apellidos, estatus = @estatus WHERE id_alumno = @id";

                        SqlCommand comando = new SqlCommand(consulta, conexion);
                        comando.Parameters.AddWithValue("@nombre", txtNombreAlumno.Text);
                        comando.Parameters.AddWithValue("@apellidos", txtApellidosAlumno.Text);
                        comando.Parameters.AddWithValue("@estatus", txtEstatusAlumno.Text);
                        comando.Parameters.AddWithValue("@id", idParaEditar);

                        comando.ExecuteNonQuery();
                        MessageBox.Show("Alumno actualizado correctamente.");
                    }

                    CargarAlumnos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el alumno en la base de datos: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa para editar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAlumno.SelectedRows.Count > 0)
            {
                try
                {
                    // Obtiene el ID del alumno
                    int idEliminar = Convert.ToInt32(dgvAlumno.SelectedRows[0].Cells["id_alumno"].Value);

                    var resultado = MessageBox.Show("¿Está seguro de que desea eliminar este alumno?", "Confirmar eliminación", MessageBoxButtons.YesNo);

                    if (resultado == DialogResult.Yes)
                    {
                        using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                        {
                            conexion.Open();
                            // Consulta DELETE para la tabla Alumno
                            string consulta = "DELETE FROM Alumno WHERE id_alumno = @id";

                            SqlCommand comando = new SqlCommand(consulta, conexion);
                            comando.Parameters.AddWithValue("@id", idEliminar);

                            comando.ExecuteNonQuery();
                            MessageBox.Show("Alumno eliminado correctamente.");
                        }

                        CargarAlumnos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el alumno: " + ex.Message);
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

        private void Alumno_Load_1(object sender, EventArgs e)
        {
            CargarAlumnos();
        }
    }
}
 

       
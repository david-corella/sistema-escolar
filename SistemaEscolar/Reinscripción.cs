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
    public partial class Reinscripción : Form
    {
        //Conexiones a las bases de datos
        public string ConexionCatalogos = "Server=localhost\\SQLEXPRESS;"
                            + " Database=Catalogos;"
                            + " Integrated Security=True; "
                            + " TrustServerCertificate=True";

        public string ConexionTablas = "Server=localhost\\SQLEXPRESS;"
                            + " Database=Tablas;"
                            + " Integrated Security=True; "
                            + " TrustServerCertificate=True";


        public Reinscripción()
        {
            InitializeComponent();
        }
        private void Reinscripción_Load(object sender, EventArgs e)
                {
                    CargarGruposEnComboBox();
                    CargarAlumnosEnComboBox();
                    CargarReinscripciones();
                }


        private void CargarGruposEnComboBox()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionTablas))
                {
                    string consulta = "SELECT id_grupo, Maestro + ' - ' + Carrera AS Descripcion FROM Grupo";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    cbIDgrupo.DataSource = tabla;
                    cbIDgrupo.DisplayMember = "Descripcion";
                    cbIDgrupo.ValueMember = "id_grupo";
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar grupos: " + ex.Message); }
        }

        private void CargarAlumnosEnComboBox()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    string consulta = "SELECT id_alumno, nombre_alumno + ' ' + apellidos_alumno AS nombre_completo FROM Alumno";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    cbIDalumno.DataSource = tabla;
                    cbIDalumno.DisplayMember = "nombre_completo";
                    cbIDalumno.ValueMember = "id_alumno";
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar alumnos: " + ex.Message); }
        }

        private void CargarReinscripciones()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionTablas))
                {
                    // Hacemos un JOIN para mostrar nombres en lugar de solo IDs
                    string consulta = @"SELECT Reinscripcion.id_reinscripcion, 
                                       Grupo.Maestro + ' - ' + Grupo.Carrera AS Grupo, 
                                       Alumno.nombre_alumno + ' ' + Alumno.apellidos_alumno AS Alumno,
                                       Reinscripcion.calificacion
                                FROM Reinscripcion 
                                JOIN Grupo ON Reinscripcion.id_grupo = Grupo.id_grupo
                                JOIN Catalogos.dbo.Alumno ON Reinscripcion.id_alumno = Alumno.id_alumno";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tablaReinscripciones = new DataTable();
                    adaptador.Fill(tablaReinscripciones);
                    dgvReinscripcion.DataSource = tablaReinscripciones;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las reinscripciones: " + ex.Message);
            }
        }
        private void dgvReinscripcion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvReinscripcion.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvReinscripcion.SelectedRows[0];

                cbIDgrupo.Text = fila.Cells["Grupo"].Value.ToString();
                cbIDalumno.Text = fila.Cells["Alumno"].Value.ToString();
                txtCalificacion.Text = fila.Cells["calificacion"].Value.ToString();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionTablas))
                {
                    conexion.Open();
                    string consulta = "INSERT INTO Reinscripcion (id_grupo, id_alumno, calificacion, fecha_hora_creacion) VALUES (@id_grupo, @id_alumno, @calificacion, GETDATE())";
                    SqlCommand comando = new SqlCommand(consulta, conexion);

                    comando.Parameters.AddWithValue("@id_grupo", cbIDgrupo.SelectedValue);
                    comando.Parameters.AddWithValue("@id_alumno", cbIDalumno.SelectedValue);
                    comando.Parameters.AddWithValue("@calificacion", int.Parse(txtCalificacion.Text));

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Reinscripción agregada correctamente.");
                }
                CargarReinscripciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la reinscripción: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvReinscripcion.SelectedRows.Count > 0)
            {
                try
                {
                    int idParaEditar = Convert.ToInt32(dgvReinscripcion.SelectedRows[0].Cells["id_reinscripcion"].Value);

                    using (SqlConnection conexion = new SqlConnection(ConexionTablas))
                    {
                        conexion.Open();
                        string consulta = "UPDATE Reinscripcion SET id_grupo = @id_grupo, id_alumno = @id_alumno, calificacion = @calificacion WHERE id_reinscripcion = @id";
                        SqlCommand comando = new SqlCommand(consulta, conexion);

                        comando.Parameters.AddWithValue("@id_grupo", cbIDgrupo.SelectedValue);
                        comando.Parameters.AddWithValue("@id_alumno", cbIDalumno.SelectedValue);
                        comando.Parameters.AddWithValue("@calificacion", int.Parse(txtCalificacion.Text));
                        comando.Parameters.AddWithValue("@id", idParaEditar);

                        comando.ExecuteNonQuery();
                        MessageBox.Show("Reinscripción actualizada correctamente.");
                    }
                    CargarReinscripciones();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la reinscripción: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para editar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvReinscripcion.SelectedRows.Count > 0)
            {
                int idSeleccionado = Convert.ToInt32(dgvReinscripcion.SelectedRows[0].Cells["id_reinscripcion"].Value);

                var resultado = MessageBox.Show("¿Está seguro de que desea eliminar esta reinscripción?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conexion = new SqlConnection(ConexionTablas))
                        {
                            conexion.Open();
                            string consulta = "DELETE FROM Reinscripcion WHERE id_reinscripcion = @id";
                            SqlCommand comando = new SqlCommand(consulta, conexion);
                            comando.Parameters.AddWithValue("@id", idSeleccionado);
                            comando.ExecuteNonQuery();
                            MessageBox.Show("Reinscripción eliminada correctamente.");
                        }
                        CargarReinscripciones();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar la reinscripción: " + ex.Message);
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

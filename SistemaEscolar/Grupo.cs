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
    public partial class Grupo : Form
    {
        public string ConexionCatalogos = "Server=localhost\\SQLEXPRESS;"
                            + " Database=Catalogos;"
                            + " Integrated Security=True; "
                            + " TrustServerCertificate=True";

        public string ConexionTablas = "Server=localhost\\SQLEXPRESS;"
                            + " Database=Tablas;"
                            + " Integrated Security=True; "
                            + " TrustServerCertificate=True";
        public Grupo()
        {
            InitializeComponent();
        }

        private void Grupo_Load(object sender, EventArgs e)
        {
            //Llenamos todos los ComboBox con datos de la BD Catalogos
            CargarAlumnosEnComboBox();
            CargarMaestrosEnComboBox();
            CargarAulasEnComboBox();
            CargarCarrerasEnComboBox();
            CargarHorariosEnComboBox(); 

            // Llenamos la tabla principal con datos de la BD Tablas
            CargarGrupos();
        }

        private void CargarAlumnosEnComboBox()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    // Unimos nombre y apellidos para mostrar el nombre completo
                    string consulta = "SELECT id_alumno, nombre_alumno + ' ' + apellidos_alumno AS nombre_completo FROM Alumno";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    cbAlumno.DataSource = tabla;
                    cbAlumno.DisplayMember = "nombre_completo";
                    cbAlumno.ValueMember = "id_alumno"; 
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar alumnos: " + ex.Message); }
        }

        private void CargarMaestrosEnComboBox()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    string consulta = "SELECT id_academico, nombre_academico + ' ' + apellidos_academicos AS nombre_completo FROM Academico";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    cbMaestro.DataSource = tabla;
                    cbMaestro.DisplayMember = "nombre_completo";
                    cbMaestro.ValueMember = "id_academico";
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar maestros: " + ex.Message); }
        }

        private void CargarAulasEnComboBox()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    string consulta = "SELECT id_aula, edificio + ' - ' + aula AS descripcion FROM Aula";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    cbAula.DataSource = tabla;
                    cbAula.DisplayMember = "descripcion";
                    cbAula.ValueMember = "id_aula";
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar aulas: " + ex.Message); }
        }

        private void CargarCarrerasEnComboBox()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    string consulta = "SELECT id_carrera, nombre_carrera FROM Carrera";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    cbCarrera.DataSource = tabla;
                    cbCarrera.DisplayMember = "nombre_carrera";
                    cbCarrera.ValueMember = "id_carrera";
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar carreras: " + ex.Message); }
        }

        private void CargarHorariosEnComboBox()
        {
            // Este ComboBox se llena con una lista fija, no desde la base de datos
            cbHorario.Items.Add("07:00 - 08:00");
            cbHorario.Items.Add("08:00 - 09:00");
            cbHorario.Items.Add("9:00 - 10:00");
            cbHorario.Items.Add("10:00 - 11:00");
            cbHorario.Items.Add("11:00 - 12:00");
            cbHorario.Items.Add("12:00 - 13:00");
            cbHorario.Items.Add("13:00 - 14:00");
            cbHorario.Items.Add("14:00 - 15:00");
            cbHorario.Items.Add("15:00 - 16:00");
            cbHorario.Items.Add("16:00 - 17:00");
            cbHorario.Items.Add("17:00 - 18:00");
            cbHorario.Items.Add("18:00 - 19:00");
            cbHorario.Items.Add("19:00 - 20:00");
        }


        private void CargarGrupos()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionTablas))
                {
                    string consulta = "SELECT id_grupo, Alumno, Maestro, Aula, Horario, Carrera FROM Grupo";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tablaGrupos = new DataTable();
                    adaptador.Fill(tablaGrupos);
                    dgvGrupos.DataSource = tablaGrupos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los grupos: " + ex.Message);
            }
        }

        private void dgvGrupos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrupos.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvGrupos.SelectedRows[0];

                // Asignamos el texto de la celda al ComboBox correspondiente
                cbAlumno.Text = fila.Cells["Alumno"].Value.ToString();
                cbMaestro.Text = fila.Cells["Maestro"].Value.ToString();
                cbAula.Text = fila.Cells["Aula"].Value.ToString();
                cbHorario.Text = fila.Cells["Horario"].Value.ToString();
                cbCarrera.Text = fila.Cells["Carrera"].Value.ToString();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionTablas))
                {
                    conexion.Open();
                    string consulta = "INSERT INTO Grupo (Alumno, Maestro, Aula, Horario, Carrera) VALUES (@alumno, @maestro, @aula, @horario, @carrera)";
                    SqlCommand comando = new SqlCommand(consulta, conexion);

                    // Guardamos el texto seleccionado de cada ComboBox
                    comando.Parameters.AddWithValue("@alumno", cbAlumno.Text);
                    comando.Parameters.AddWithValue("@maestro", cbMaestro.Text);
                    comando.Parameters.AddWithValue("@aula", cbAula.Text);
                    comando.Parameters.AddWithValue("@horario", cbHorario.Text);
                    comando.Parameters.AddWithValue("@carrera", cbCarrera.Text);

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Grupo agregado correctamente.");
                }
                CargarGrupos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el grupo: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvGrupos.SelectedRows.Count > 0)
            {
                try
                {
                    int idParaEditar = Convert.ToInt32(dgvGrupos.SelectedRows[0].Cells["id_grupo"].Value);

                    using (SqlConnection conexion = new SqlConnection(ConexionTablas))
                    {
                        conexion.Open();
                        string consulta = "UPDATE Grupo SET Alumno = @alumno, Maestro = @maestro, Aula = @aula, Horario = @horario, Carrera = @carrera WHERE id_grupo = @id";
                        SqlCommand comando = new SqlCommand(consulta, conexion);

                        comando.Parameters.AddWithValue("@alumno", cbAlumno.Text);
                        comando.Parameters.AddWithValue("@maestro", cbMaestro.Text);
                        comando.Parameters.AddWithValue("@aula", cbAula.Text);
                        comando.Parameters.AddWithValue("@horario", cbHorario.Text);
                        comando.Parameters.AddWithValue("@carrera", cbCarrera.Text);
                        comando.Parameters.AddWithValue("@id", idParaEditar);

                        comando.ExecuteNonQuery();
                        MessageBox.Show("Grupo actualizado correctamente.");
                    }
                    CargarGrupos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el grupo: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un grupo para editar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvGrupos.SelectedRows.Count > 0)
            {
                int idGrupoSeleccionado = Convert.ToInt32(dgvGrupos.SelectedRows[0].Cells["id_grupo"].Value);

                var resultado = MessageBox.Show("¿Está seguro de que desea eliminar este grupo?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conexion = new SqlConnection(ConexionTablas))
                        {
                            conexion.Open();
                            string consulta = "DELETE FROM Grupo WHERE id_grupo = @id";
                            SqlCommand comando = new SqlCommand(consulta, conexion);
                            comando.Parameters.AddWithValue("@id", idGrupoSeleccionado);
                            comando.ExecuteNonQuery();
                            MessageBox.Show("Grupo eliminado correctamente.");
                        }
                        CargarGrupos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el grupo: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un grupo completo para eliminar.");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

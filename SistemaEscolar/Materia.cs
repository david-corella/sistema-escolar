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
    public partial class Materia : Form
    {
        // Conexión a la base de datos
        public string ConexionCatalogos = "Server=localhost\\SQLEXPRESS;"
                            + " Database=Catalogos;"
                            + " Integrated Security=True; "
                            + " TrustServerCertificate=True";

        // Variable para almacenar el id de la materia seleccionada en el dgv
        private int idMateriaSeleccionada = 0;

        public Materia()
        {
            InitializeComponent();
        }
        private void Materia_Load(object sender, EventArgs e)
        {
            CargarMaterias();
        }

        private void CargarMaterias()
        {
            try
            {
                //using para que la conexión se cierre sola
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();

                    string consulta = "SELECT [id_materia] " + 
                        " ,[nombre_materia], [creditos]" + " FROM [Catalogos].[dbo].[Materia]";

                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);

                    
                    DataTable tablaMaterias = new DataTable();

                    
                    adaptador.Fill(tablaMaterias);
                    
                    //Cargamos datos al dgv
                    dgvMaterias.DataSource = tablaMaterias;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de las materias: " + ex.Message);
            }
        }
        
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();

                    string consulta = "INSERT INTO Materia (nombre_materia, creditos, fecha_hora_creacion) VALUES (@nombre, @creditos, GETDATE())";

                    SqlCommand comando = new SqlCommand(consulta, conexion);

                    comando.Parameters.AddWithValue("@nombre", txtNombreMateria.Text);
                    comando.Parameters.AddWithValue("@creditos", int.Parse(txtCreditos.Text));

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Materia agregada correctamente.");
                }

                CargarMaterias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la materia: " + ex.Message);
            }
        }
        private void dgvMaterias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMaterias.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvMaterias.SelectedRows[0];

                // Copia los valores de las celdas a los TextBox correspondientes.
                txtNombreMateria.Text = fila.Cells["nombre_materia"].Value.ToString();
                txtCreditos.Text = fila.Cells["creditos"].Value.ToString();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvMaterias.SelectedRows.Count > 0)
            {
                int creditos;
                if (int.TryParse(txtCreditos.Text, out creditos))
                {
                    try
                    {
                        int idParaEditar = Convert.ToInt32(dgvMaterias.SelectedRows[0].Cells["id_materia"].Value);

                        using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                        {
                            conexion.Open();
                            string consulta = "UPDATE Materia SET nombre_materia = @nombre, creditos = @creditos WHERE id_materia = @id";

                            SqlCommand comando = new SqlCommand(consulta, conexion);
                            comando.Parameters.AddWithValue("@nombre", txtNombreMateria.Text);
                            comando.Parameters.AddWithValue("@creditos", creditos);
                            comando.Parameters.AddWithValue("@id", idParaEditar);

                            comando.ExecuteNonQuery();
                            MessageBox.Show("Materia actualizada correctamente.");
                        }

                        CargarMaterias();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar en la base de datos: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("El valor en el campo 'Créditos' debe ser un número válido y no puede estar vacío.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa para editar.");
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMaterias.SelectedRows.Count > 0)
            {
                try
                {
                    int idEliminar = Convert.ToInt32(dgvMaterias.SelectedRows[0].Cells["id_materia"].Value);

                    var resultado = MessageBox.Show("¿Está seguro de que desea eliminar esta materia?", "Confirmar eliminación", MessageBoxButtons.YesNo);

                    if (resultado == DialogResult.Yes)
                    {

                        using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                        {
                            conexion.Open();
                            string consulta = "DELETE FROM Materia WHERE id_materia = @id";

                            SqlCommand comando = new SqlCommand(consulta, conexion);
                            comando.Parameters.AddWithValue("@id", idEliminar);

                            comando.ExecuteNonQuery();
                            MessageBox.Show("Materia eliminada correctamente.");
                        }

                        CargarMaterias();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la materia: " + ex.Message);
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


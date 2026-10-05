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
    public partial class Estado : Form
    {
        // Conexión a la base de datos
        public string ConexionCatalogos = "Server=localhost\\SQLEXPRESS;"
                                       + " Database=Catalogos;"
                                       + " Integrated Security=True; "
                                       + " TrustServerCertificate=True";
        
        
        public Estado()
        {
            InitializeComponent();
        }

        private void Estado_Load(object sender, EventArgs e)
        {
            CargarPaisesEnComboBox();
            CargarEstados();
        }
        private void CargarPaisesEnComboBox()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();
                    string consulta = "SELECT id_pais, nombre_pais FROM Pais";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tablaPaises = new DataTable();
                    adaptador.Fill(tablaPaises);

                    // Configuración del ComboBox
                    cbIDPais.DataSource = tablaPaises;
                    cbIDPais.DisplayMember = "nombre_pais"; // El texto que ve el usuario
                    cbIDPais.ValueMember = "id_pais";       // El valor interno que se guardará
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de países: " + ex.Message);
            }
        }

        private void CargarEstados()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();
                    // Consulta que une las tablas Estado y Pais para mostrar el nombre del país
                    string consulta = @"SELECT e.id_estado, 
                                               e.nombre_estado, 
                                               e.sigla_estado, 
                                               p.nombre_pais 
                                          FROM Estado e
                                          JOIN Pais p ON e.id_pais = p.id_pais";

                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                    DataTable tablaEstados = new DataTable();
                    adaptador.Fill(tablaEstados);
                    dgvEstado.DataSource = tablaEstados;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de los estados: " + ex.Message);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                {
                    conexion.Open();
                    string consulta = "INSERT INTO Estado (nombre_estado, sigla_estado, id_pais, fecha_hora_creacion) VALUES (@nombre, @sigla, @id_pais, GETDATE())";
                    SqlCommand comando = new SqlCommand(consulta, conexion);

                    comando.Parameters.AddWithValue("@nombre", txtNombreEstado.Text);
                    comando.Parameters.AddWithValue("@sigla", txtSigla.Text);
                    // Obtenemos el ID del país seleccionado en el ComboBox
                    comando.Parameters.AddWithValue("@id_pais", cbIDPais.SelectedValue);

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Estado agregado correctamente.");
                }
                CargarEstados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el estado: " + ex.Message);
            }
        }

        private void dgvEstado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEstado.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvEstado.SelectedRows[0];

                txtNombreEstado.Text = fila.Cells["nombre_estado"].Value.ToString();
                txtSigla.Text = fila.Cells["sigla_estado"].Value.ToString();

                // Para que el ComboBox seleccione el país correcto
                cbIDPais.Text = fila.Cells["nombre_pais"].Value.ToString();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEstado.SelectedRows.Count > 0)
            {
                try
                {
                    int idParaEditar = Convert.ToInt32(dgvEstado.SelectedRows[0].Cells["id_estado"].Value);

                    using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                    {
                        conexion.Open();
                        string consulta = "UPDATE Estado SET nombre_estado = @nombre, sigla_estado = @sigla, id_pais = @id_pais WHERE id_estado = @id";
                        SqlCommand comando = new SqlCommand(consulta, conexion);

                        comando.Parameters.AddWithValue("@nombre", txtNombreEstado.Text);
                        comando.Parameters.AddWithValue("@sigla", txtSigla.Text);
                        comando.Parameters.AddWithValue("@id_pais", cbIDPais.SelectedValue);
                        comando.Parameters.AddWithValue("@id", idParaEditar);

                        comando.ExecuteNonQuery();
                        MessageBox.Show("Estado actualizado correctamente.");
                    }
                    CargarEstados();
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
            if (dgvEstado.SelectedRows.Count > 0)
            {
                int idEstadoSeleccionado = Convert.ToInt32(dgvEstado.SelectedRows[0].Cells["id_estado"].Value);

                var resultado = MessageBox.Show("¿Está seguro de que desea eliminar este estado?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conexion = new SqlConnection(ConexionCatalogos))
                        {
                            conexion.Open();
                            string consulta = "DELETE FROM Estado WHERE id_estado = @id";
                            SqlCommand comando = new SqlCommand(consulta, conexion);
                            comando.Parameters.AddWithValue("@id", idEstadoSeleccionado);
                            comando.ExecuteNonQuery();
                            MessageBox.Show("Estado eliminado correctamente.");
                        }
                        CargarEstados();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el estado: " + ex.Message);
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

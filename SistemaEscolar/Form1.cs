using SistemaEscolar.Catalogos;
using System;
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
    public partial class Form1 : Form
    {
        public string ConexionCatalogos = "Server=localhost\\SQLEXPRESS;"
                            + " Database=Catalogos;"
                            + " Integrated Security=True; "
                            + " TrustServerCertificate=True";

        public string ConexionTablas = "Server=localhost\\SQLEXPRESS;"
                            + " Database=Tablas;"
                            + " Integrated Security=True; "
                            + " TrustServerCertificate=True";
        public Form1()
        {
            InitializeComponent();
        }
        private void btnMateria_Click(object sender, EventArgs e)
        {
            //Crear objeto ventana Materia.
            Materia ventanaMateria = new Materia();

            //Mostrar ventana en pantalla.
            ventanaMateria.Show();
        }

        private void btnAula_Click(object sender, EventArgs e)
        {
            Aula ventanaAula = new Aula();
            ventanaAula.Show();
        }

        private void btnAcademico_Click(object sender, EventArgs e)
        {
            Academico ventanaAcademico = new Academico();
            ventanaAcademico.Show();
        }

        private void btnAlumno_Click(object sender, EventArgs e)
        {
            Alumno ventanaAlumno = new Alumno();
            ventanaAlumno.Show();
        }

        private void btnCarrera_Click(object sender, EventArgs e)
        {
            Carrera ventanaCarrera = new Carrera();
            ventanaCarrera.Show();
        }

        private void btnCiudad_Click(object sender, EventArgs e)
        {
            Ciudad ventanaCiudad = new Ciudad();
            ventanaCiudad.Show();
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            Estado ventanaEstado = new Estado();
            ventanaEstado.Show();
        }

        private void btnPais_Click(object sender, EventArgs e)
        {
            Pais ventanaPais = new Pais();
            ventanaPais.Show();
        }

        private void btnEstatus_Click(object sender, EventArgs e)
        {
            Estatus ventanaEstatus = new Estatus();
            ventanaEstatus.Show();
        }

        private void btnGrupo_Click(object sender, EventArgs e)
        {
            Grupo ventanaGrupo = new Grupo();
            ventanaGrupo.Show();
        }

        private void btnReinscripcion_Click(object sender, EventArgs e)
        {
            Reinscripción ventanaReinscripcion = new Reinscripción();
            ventanaReinscripcion.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

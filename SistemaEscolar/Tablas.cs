using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEscolar.Tablas
{
    public class Grupo
    {
        public int IDGrupo { get; set; }    
        public string Alumno { get; set; }
        public string Maestro { get; set; }
        public string Aula { get; set; }    
        public DateTime Horario { get; set; }
        public string Carrera { get; set; }

        public Grupo(int idGrupo, string alumno, string maestro, string aula, DateTime horario, string carrera)
        {
            IDGrupo = idGrupo;
            Alumno = alumno;
            Maestro = maestro;
            Aula = aula;
            Horario = horario;
            Carrera = carrera;
        }
    }

    public class Reinscripcion
    {
        public int IDReinscripcion { get; set; }
        public int IDGrupo { get; set; }
        public int IDAlumno { get; set; }
        public int Calificacion { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public Reinscripcion(int idReinscripcion, int idGrupo, int idAlumno, int calificacion)
        {
            IDReinscripcion = idReinscripcion;
            IDGrupo = idGrupo;
            IDAlumno = idAlumno;
            Calificacion = calificacion;
            FechaHoraCreacion = DateTime.Now;
        }

    }
}

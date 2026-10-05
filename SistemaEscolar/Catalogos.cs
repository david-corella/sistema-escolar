using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SistemaEscolar.Catalogos
{
    public class Materia
    {
        public int IDMateria { get; set; }
        public string NombreMateria { get; set; }
        public int Creditos { get; set; }
        public DateTime FechaHoraCreacion { get; set; }

        public Materia(int idMateria, string nombreMateria, int creditos)
        {
            IDMateria = idMateria;
            NombreMateria = nombreMateria;
            Creditos = creditos;
            FechaHoraCreacion = DateTime.Now;
        }
    }

    public class Aula
    {
        public int IDAula { get; set; }
        public string Edificio { get; set; }
        public string NdeAula { get; set; }    
        public string Piso { get; set; }    

        public int Capacidad { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public Aula(int idAula, string edificio, string nombreAula, int capacidad)
        {
            IDAula = idAula;
            Edificio = edificio;
            NdeAula = nombreAula;
            Capacidad = capacidad;
            FechaHoraCreacion = DateTime.Now;
        }
    } 
    
    public class Academico 
    {
        public int IDAcademico { get; set; }
        public string NombreAcademico { get; set; }
        public string Apellidos { get; set; }
        public string Grado { get; set; }   
        public DateTime FechaHoraCreacion { get; set; }
        public Academico(int idAcademico, string nombreAcademico, string apellidos, string grado)
        {
            IDAcademico = idAcademico;
            NombreAcademico = nombreAcademico;
            Apellidos = apellidos;
            Grado = grado;
            FechaHoraCreacion = DateTime.Now;
        }
    } 
    
    public class Alumno 
    {
        public int IDAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public string Apellidos { get; set; }
        public string Estatus { get; set; } 
        public DateTime FechaHoraCreacion { get; set; }
        public Alumno(int idAlumno, string nombreAlumno, string apellidos, string estatus)
        {
            IDAlumno = idAlumno;
            NombreAlumno = nombreAlumno;
            Apellidos = apellidos;
            Estatus = estatus;
            FechaHoraCreacion = DateTime.Now;
        }
    }

    public class Carrera 
    {
        public int IDCarrera { get; set; }
        public string NombreCarrera { get; set; }
        public string SiglasCarrera { get; set; } 
        public DateTime FechaHoraCreacion { get; set; }
        public Carrera(int idCarrera, string nombreCarrera, string siglasCarrera)
        {
            IDCarrera = idCarrera;
            NombreCarrera = nombreCarrera;
            SiglasCarrera = siglasCarrera;
            FechaHoraCreacion = DateTime.Now;
        }
    } 
    
    public class Ciudad 
    {
        public int IDCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public string SiglasCiudad { get; set; } 
        public int IDEstado { get; set; }   
        public DateTime FechaHoraCreacion { get; set; }
        public Estado Estado { get; set; }
        public Ciudad(int idCiudad, string nombreCiudad, string siglasCiudad, int idEstado)
        {
            IDCiudad = idCiudad;
            NombreCiudad = nombreCiudad;
            SiglasCiudad = siglasCiudad;
            IDEstado = idEstado;
            FechaHoraCreacion = DateTime.Now;
        }
    } 
    
    public class Estado 
    {
        public int IDEstado { get; set; }
        public string NombreEstado { get; set; }
        public string SiglasEstado { get; set; } 
        public int IDPais { get; set; }   
        public DateTime FechaHoraCreacion { get; set; }
        public Pais Pais { get; set; }
        public Estado(int idEstado, string nombreEstado, string siglasEstado, int idPais)
        {
            IDEstado = idEstado;
            NombreEstado = nombreEstado;
            SiglasEstado = siglasEstado;
            IDPais = idPais;
            FechaHoraCreacion = DateTime.Now;
        }
    }
    
    public class Pais 
    {
        public int IDPais { get; set; }
        public string NombrePais { get; set; }
        public string SiglasPais { get; set; } 
        public DateTime FechaHoraCreacion { get; set; }
        public Pais(int idPais, string nombrePais, string siglasPais)
        {
            IDPais = idPais;
            NombrePais = nombrePais;
            SiglasPais = siglasPais;
            FechaHoraCreacion = DateTime.Now;
        }
    }

    public class Estatus 
    { 
        public int IDStatus { get; set; }
        public string ClaveStatus { get; set; }
        public string NombreStatus { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string Usuario { get; set; }
        public Estatus(int idStatus, string claveStatus, string nombreStatus, string usuario)
        {
            IDStatus = idStatus;
            ClaveStatus = claveStatus;
            NombreStatus = nombreStatus;
            Usuario = usuario;
            FechaHoraCreacion = DateTime.Now;
        }   
    }
}

# SistemaEscolar

Sistema de gestión escolar de escritorio desarrollado en **C# (Windows Forms)** sobre
**.NET Framework 4.7.2**, con persistencia en **SQL Server**. Proyecto académico de la materia de
Desarrollo de Software (UNISON).

## Funcionalidad

Dos grupos de módulos, cada uno contra su propia base de datos:

- **Catálogos** (`Catalogos`): `País`, `Estado`, `Ciudad`, `Estatus`, `Carrera`, `Materia`, `Aula`.
- **Tablas** (`Tablas`): `Alumno`, `Grupo`, `Académico` y `Reinscripción`.

Cada formulario implementa operaciones **CRUD** (Altas, Bajas, Consultas y Actualizaciones)
integradas con controles de Windows Forms (`DataGridView`, `ComboBox`, `TextBox`), con validación de
campos y manejo de eventos de interfaz.

## Tecnologías

- **Lenguaje:** C#
- **UI:** Windows Forms
- **Framework:** .NET Framework 4.7.2
- **Base de datos:** SQL Server (Microsoft.Data.SqlClient)
- **IDE:** Visual Studio 2022

## Requisitos

- Windows 10/11
- [Visual Studio 2022](https://visualstudio.microsoft.com/) con la carga de trabajo
  **"Desarrollo de escritorio con .NET"**
- [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads) (instancia
  `SQLEXPRESS`) y SQL Server Management Studio (opcional)

## Base de datos

El proyecto espera **dos bases de datos**: `Catalogos` y `Tablas`.

1. Crea ambas bases en tu instancia de SQL Server Express.
2. Ejecuta el script de estructura correspondiente (tablas y catálogos).
3. Ajusta la cadena de conexión si tu instancia no es `localhost\SQLEXPRESS`. Está definida al inicio
   de cada formulario:

   ```csharp
   public string ConexionCatalogos = "Server=localhost\\SQLEXPRESS;"
                                   + " Database=Catalogos;"
                                   + " Integrated Security=True; "
                                   + " TrustServerCertificate=True";
   ```

   > Nota: la cadena se repite en cada formulario. Una mejora pendiente es centralizarla en un único
   > helper o en `App.config`.

## Compilar y ejecutar

1. Abre `SistemaEscolar.sln` en Visual Studio 2022.
2. Restaura los paquetes NuGet (Visual Studio lo hace automáticamente al compilar).
3. Conecta tu instancia de SQL Server Express con las bases `Catalogos` y `Tablas`.
4. Compila y ejecuta con **F5** (o `Ctrl+F5`).

## Autor

**David Corella** — Ingeniero en Sistemas de Información (UNISON).
Portafolio: <https://david-corella.github.io/>

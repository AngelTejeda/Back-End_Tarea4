namespace Tarea_4
{
    public class Program
    {
        public static void Main()
        {
            /* Pendientes */
            //SQL Exception (Llave Foránea y Attribute Check)
            //Preguntarle al profe del copy-paste
            //Modelos DTO en los controladores
            //Stack Trace
            //SQLException
            //dbContext

            /* Documento */
            //Crear nuevo proyecto API Web
            //Agregar referencia del proyecto
            //Cambios en EmployeeSC (ExtensionMethod SetMessage, DbExceptionMessages, SQLException, dbContext)
            //Agregar SC Customer y Product
            //Agregar DTO Customer y Porduct
            //Agregar los controladores


            /* ---------------- CAMBIOS ---------------- */

            /* --- Models --- */
            // Se agregaron los atributos que no pueden ser null a EmployeeDTO.
            // Se cambió el nombre de EmployeeBasicDataDTO a EmployeePersonalInfoDTO.
            // Se tradujeron los atributos de los modelos.
            // Se cambiaron los atributos de EmployeePersonalInfoDTO.
            // Se agregó un constructor en EmployeePersoanlInfoDTO.

            // Se agregaron las vistas para Customer y Product.

            /* --- BackEnd --- */
            // EmployeeSC ahora es una clase estática y no hereda dbContext de BaseSC.
            // Todos los métoods de EmployeeSC son estáticos y reciben un NorthwindContext con el que van a trabajar.
            // Mover los bloque try-catch a EmployeeController.

            // Se agregaron los Service Components para Customer y Product.

            /* --- Excepciones --- */
            // Se agregó la clase ExceptionExtensionMethods con los métodos SetMessage, GetFullMessage y GetFullStackTrace.
            // Se agregó la clase DbExceptionMessages.
            // En API Rest regresar el StatusCode correspondiente al tipo de excepción que se produjo.
            // Funciones auxiliares para detectar el tipo de excepción (IsSqlException).


            /* ---------------- API REST ---------------- */

            // Se agrega un nuevo proyecto a la solución (ASP .NET Core Web Application).
            // Agregar la referencia al proyecto existente.
            // Marcar API Rest como Startup Project.
            // Se crean los controladores para Employee, Customer y Product.
        }
    }
}
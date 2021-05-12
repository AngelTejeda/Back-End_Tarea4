using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace Tarea_4.BackEnd
{
    public static class ExceptionTypes
    {
        public static bool IsSqlException(Exception ex)
        {
            return ex is DbUpdateException
                && ex.InnerException != null
                && ex.InnerException is SqlException;
        }
    }
}

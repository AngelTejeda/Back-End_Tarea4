using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_4.ExceptionHandling
{
    public static class SqlExceptionMessages
    {
        public static string GetCustomSqlExceptionMessage(SqlException ex)
        {
            return ex.Number switch
            {
                515 => "No value was provided for one or more required fields.", //NULL value in NOT NULL field.
                547 => "One or more fields break a constraint.", //Constraint Violation
                2627 => "The provided ID already exists.", //Duplicate Key
                2628 => "One or more fields would be truncated.", //Truncation
                _ => null,  // Unexpected SQL Exception
            };
        }
    }
}

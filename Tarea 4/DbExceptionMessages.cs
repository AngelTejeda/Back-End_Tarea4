using System;

namespace Tarea_4
{
    public static class DbExceptionMessages
    {
        public static string FieldIsRequired(string field)
        {
            return $"The field \"{field}\" is required.";
        }

        public static string InstanceNotFound(string instanceName, object id)
        {
            return $"No {instanceName} with id {id} was found.";
        }

        public static string FailedToAdd(string instanceName, Exception ex)
        {
            return $"An error ocurred while adding the {instanceName}: {ex.Message}";
        }

        public static string FailedToUpdate(string instanceName, object id, Exception ex)
        {
            return $"An error ocurred while updating the {instanceName} with id {id}: {ex.Message}";
        }

        public static string FailedToDelete(string instanceName, object id, Exception ex)
        {
            return $"An error ocurred while deleting the {instanceName} with id {id}: {ex.Message}";
        }

        public static string UnexpectedFailure(Exception ex)
        {
            return $"An unexpected error ocurred while updating the DataBase: {ex.GetFullMessage()}";
        }
    }
}

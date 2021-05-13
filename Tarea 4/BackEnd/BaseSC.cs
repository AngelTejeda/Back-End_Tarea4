using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarea_4.DataAccess;
using Tarea_4.Models;

namespace Tarea_4.BackEnd
{
    public class BaseSC
    {
        protected NorthwindContext dbContext = new();
        
        /// <summary>
        /// Materializes an IQueryable into a List of a given model.
        /// </summary>
        /// <typeparam name="DbObject">Class of the DataBase Object.</typeparam>
        /// <typeparam name="Model">Model of the Object</typeparam>
        /// <param name="dataBaseIQueryable">IQueryable wich will be materialized.</param>
        /// <returns>A List with the corresponding objects.</returns>
        public static List<Model> MaterializeIQueryable<DbObject, Model>(IQueryable<DbObject> dataBaseIQueryable)
            where Model : IReadable<DbObject>, new()
            where DbObject : class
        {
            List<Model> modelList = new();

            dataBaseIQueryable = dataBaseIQueryable.AsNoTracking();

            foreach (DbObject dataBaseObject in dataBaseIQueryable)
            {
                Model model = new();
                model.CopyInfoFromDataBaseObject(dataBaseObject);

                modelList.Add(model);
            };

            return modelList;
        }

        protected static int CalculateLastPage(int totalElements, int elementsPerPage)
        {
            return Convert.ToInt32(Math.Ceiling((double)totalElements / elementsPerPage));
        }

        protected static IQueryable<T> GetPage<T>(IQueryable<T> elementsQuery, int elementsPerPage, int page)
        {
            if (elementsPerPage <= 0)
                throw new ArgumentOutOfRangeException(nameof(elementsPerPage));

            if (page <= 0)
                throw new ArgumentOutOfRangeException(nameof(page));

            return elementsQuery
                .Skip((page - 1) * elementsPerPage)
                .Take(elementsPerPage);
        }
    }
}

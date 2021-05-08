using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_4.Models
{
    public class Pagination<T> where T : class
    {
        public int? PreviousPage { get; set; }
        public int CurrentPage { get; set; }
        public int? NextPage { get; set; }
        public int LastPage { get; set; }
        public List<T> ResponseList { get; set; }

        public Pagination()
        {

        }

        public Pagination(int page, int lastPage)
        {
            LastPage = lastPage;
            ResponseList = new List<T>();

            if (page > lastPage)
                page = lastPage;

            PreviousPage = page - 1;
            CurrentPage = page;
            NextPage = page + 1;

            if (page == lastPage)
                NextPage = null;

            if (page == 1)
                PreviousPage = null;
        }
    }
}

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
        public int? CurrentPage { get; set; }
        public int? NextPage { get; set; }
        public List<T> ResponseList { get; set; }

        public Pagination()
        {

        }

        public Pagination(int? previousPage, int? currentPage, int? nextPage, List<T> responseList)
        {
            PreviousPage = previousPage;
            CurrentPage = currentPage;
            NextPage = nextPage;
            ResponseList = responseList;
        }
    }
}

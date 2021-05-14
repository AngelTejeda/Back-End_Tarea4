using System.Collections.Generic;

namespace Tarea_4.Models
{
    public class Pagination<T> where T : class
    {
        public int? PreviousPage { get; set; }
        public int? CurrentPage { get; set; }
        public int? NextPage { get; set; }
        public int LastPage { get; set; }
        public List<T> ResponseList { get; set; }


        public Pagination(int page, int lastPage)
        {
            ResponseList = new List<T>();
            LastPage = lastPage;

            // If there is no information.
            if (lastPage == 0)
            {
                PreviousPage = null;
                CurrentPage = null;
                NextPage = null;

                return;
            }

            // If the requested page doesn't exist, return the last page.
            if (page > lastPage)
                page = lastPage;

            // Next and Previous Pages are calculated based on CurrentPage.
            PreviousPage = page - 1;
            CurrentPage = page;
            NextPage = page + 1;

            // If the requested page is the last one.
            if (page == lastPage)
                NextPage = null;

            // If the requested page is the first one.
            if (page == 1)
                PreviousPage = null;
        }
    }
}

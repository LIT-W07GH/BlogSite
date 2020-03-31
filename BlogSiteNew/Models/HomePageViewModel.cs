using System.Collections.Generic;
using BlogSiteNew.Data;

namespace BlogSiteNew.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<BlogPost> Posts { get; set; }
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
    }
}
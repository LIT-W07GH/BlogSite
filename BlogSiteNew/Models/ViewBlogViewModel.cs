using System.Collections.Generic;
using BlogSiteNew.Data;

namespace BlogSiteNew.Models
{
    public class ViewBlogViewModel
    {
        public BlogPost Post { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public string CommenterName { get; set; }
    }
}
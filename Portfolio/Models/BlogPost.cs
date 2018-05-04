using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Models
{
    [Table("BlogPosts")]
    public class BlogPost
    {
        [Key]
        public int BlogPostKey { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}

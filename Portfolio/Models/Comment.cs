using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int CommentKey { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }

        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("BlogPosts")]
        public int BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
    }
}

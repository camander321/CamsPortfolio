using System.Linq;

namespace Portfolio.Models
{
    public interface IBlogPostRepository
    {
        IQueryable<BlogPost> BlogPosts { get; }
        BlogPost Save(BlogPost post);
        BlogPost Edit(BlogPost post);
        void Remove(BlogPost post);
        void RemoveAll();
    }
}

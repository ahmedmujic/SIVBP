
namespace SIVBP_Keširanje.Models
{
    public partial class PostType
    {
        public PostType()
        {
            Post = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<Post> Post { get; set; }
    }
}

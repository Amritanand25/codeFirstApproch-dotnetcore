using System.Text.Json.Serialization;

namespace codeFirstApprochExample.Models
{
    public class Courses
    {
        public Courses()
        {
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ENums.CourseLevel Level { get; set; }
        public float FullPrice { get; set; }

        public int? AuthorId { get; set; }
        public virtual Author? Author { get; set; }
        public virtual ICollection<Tag> Tags {get; set;}
    }
}

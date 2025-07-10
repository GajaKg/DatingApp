using System.ComponentModel.DataAnnotations.Schema;

namespace datingapp.data.Entities
{
    [Table("Photos")] // without this it will create table Photo based on class name
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; } = String.Empty;
        public bool IsMain { get; set; }
        public string PublicId { get; set; } = String.Empty;

        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
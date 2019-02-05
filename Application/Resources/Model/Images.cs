using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Application.Resources.Model
{
    [Table("Images")]
    public class Images
    {
        [PrimaryKey] [AutoIncrement] public int Id { get; set; }

        public string ImageBytes { get; set; }

        public string PlaceName { get; set; }
    }
}
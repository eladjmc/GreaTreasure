using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace GreaTreasure.Models
{
    public class Library
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "שם ספרייה")]
        public string Name { get; set; }
        [Display(Name = "ז'אנר")]
        public string Genre { get; set; } = string.Empty;
        [Display(Name = "רוחב")]
        public int Width { get; set; }
        public List<Shelf> Shelves { get; set; }
        [NotMapped]
        public Shelf addShelf { get { return null; } set { if (value == null) return; Shelves.Add(value); } }
        public Library(string name, string genre, int width)
        {
            Name = name;
            Genre = genre;
            Width = width;
            Shelves = new List<Shelf>();
        }
        public Library()
        {
            Shelves = new List<Shelf>();
        }
        

    }
}

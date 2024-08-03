using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreaTreasure.Models
{
    public class Shelf
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "שם מדף")]
        public string Name { get; set; }
        [Display(Name ="גובה")]
        public int Height { get; set; }
        public Library Library { get; set; } 
        public List<Book> Books { get; set; }
        [NotMapped] public int Width { get { if (Library == null) return 0; return Library.Width; } }
        [NotMapped] public string LibName { get { if (Library == null) return ""; return Library.Name; } }
        [NotMapped, Display(Name = "רוחב תפוס")]
        public int OccupiedWidth
        {
            get
            {
                int width = 0; foreach (Book book in Books)
                {
                    width += book.Width;
                }
        return width;
            } }
        public Shelf(string name , int height, Library library)
        {
            Name = name;
            Height = height;
            Library = library;
            Books = new List<Book>();   
        }
        public Shelf() { Books = new List<Book>(); }
        
    }
}

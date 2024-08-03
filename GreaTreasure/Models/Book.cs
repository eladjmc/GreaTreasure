using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreaTreasure.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "שם הספר")]
        public string Title { get; set; } =string.Empty;
        [Display(Name = "תוכן")] public string Description { get; set; } = string.Empty;
        [Display(Name = "גובה")] public int Height { get; set; }
        [Display(Name = "עובי")] public int Width { get; set; }
        public Shelf shelf { get; set; }
        [NotMapped] public int ShelfHeight { get { if (shelf == null) return 0; return shelf.Height; } }
        [NotMapped] public int ShelfWidth {  get { if (shelf == null) return 0; return shelf.Width; } }
        [NotMapped] public string ShelfName { get { if (shelf == null) return "";return shelf.Name; } }
        public Book() { }
        public Book(string Title, string Description, Shelf shelf, int width, int height)
        {
            this.Title = Title;
            this.Description = Description;
            this.shelf = shelf;
            Width = width;
            Height = height;
        }
    }
}

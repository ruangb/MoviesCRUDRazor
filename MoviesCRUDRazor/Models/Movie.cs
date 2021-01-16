using System;

namespace MoviesCRUDRazor.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate{ get; set; }

        public int Gender { get; set; }

        public decimal Price { get; set; }
    }
}

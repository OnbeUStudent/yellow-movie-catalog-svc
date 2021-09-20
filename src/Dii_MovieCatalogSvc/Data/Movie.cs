using System;
using System.ComponentModel.DataAnnotations;

namespace dii_MovieCatalogSvc.Data
{
    public class Movie
    {
        public Guid MovieId { get; set; }

        public MovieMetadata MovieMetadata { get; set; }

        [Required]
        public string Title { get; set; }
    }
}

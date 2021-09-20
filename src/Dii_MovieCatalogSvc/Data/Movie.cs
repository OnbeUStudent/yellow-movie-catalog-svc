using System.ComponentModel.DataAnnotations;

namespace dii_MovieCatalogSvc.Data
{
    public class Movie
    {
        public long MovieId { get; set; }

        public MovieMetadata MovieMetadata { get; set; }

        [Required]
        public string Title { get; set; }
    }
}

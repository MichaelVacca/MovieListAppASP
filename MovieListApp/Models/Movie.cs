using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MovieListApp.Models
{
	public class Movie
	{
		// EF Core will configure the database to generate this valuepublic int MovieId { get; set; }
		public int MovieId { get; set; }
		[Required(ErrorMessage = "Please enter a name.")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter a year.")]
		[Range(1889, 2999, ErrorMessage = "Year must be after 1889.")] 
		public int? Year { get; set; }

		[Required(ErrorMessage = "Please enter a rating.")]
		[Range(1, 5, ErrorMessage =
			"Rating must be between 1 and 5.")]
		public int? Rating { get; set; }

		[Required(ErrorMessage = "Please enter a genre")]
		public string GenreId { get; set; } = String.Empty;

		[ValidateNever]
		public Genre Genre
		{
			get;
			set;
		} = null!;

		public string Slug => Name?.Replace(' ', '-').ToLower() + "-" + Year?.ToString();
	}
}

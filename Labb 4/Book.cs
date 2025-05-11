using System.ComponentModel.DataAnnotations;

namespace Labb_4;

public class Book
{
    [Required(ErrorMessage = "Title is required.")]
    [RegularExpression(@"^[a-zA-Z\s.]+$", ErrorMessage = "Title can only contain letters, dots and spaces.")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Author is required.")]
    [RegularExpression(@"^[a-zA-Z\s.]+$", ErrorMessage = "Author name can only contain letters, dots and spaces.")]
    public string Author { get; set; }
    [Required(ErrorMessage = "ISBN is required.")]
    [RegularExpression(@"^\d{10,13}$", ErrorMessage = "ISBN must be 10 or 13 digits long.")]
    public string ISBN { get; set; }
    [Range(1000, 9999, ErrorMessage = "Publication year must be between 1000 and 9999.")]
    public int PublicationYear { get; set; }
    public bool IsBorrowed { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? BorrowDate { get; set; }

    public Book(string title, string author, string isbn, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
        IsBorrowed = false;
        BorrowDate = null;
    }
}
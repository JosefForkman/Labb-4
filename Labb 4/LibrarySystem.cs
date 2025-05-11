using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Labb_4;

public class LibrarySystem
{
    private List<Book> books;

    public LibrarySystem()
    {
        books = new List<Book>();
        // Add some initial books
        books.Add(new Book("1984", "George Orwell", "9780451524935", 1949));
        books.Add(new Book("To Kill a Mockingbird", "Harper Lee", "9780061120084", 1960));
        books.Add(new Book("The Great Gatsby", "F. Scott Fitzgerald", "9780743273565", 1925));
        books.Add(new Book("The Hobbit", "J.R.R. Tolkien", "9780547928227", 1937));
        books.Add(new Book("Pride and Prejudice", "Jane Austen", "9780141439518", 1813));
        books.Add(new Book("The Catcher in the Rye", "J.D. Salinger", "9780316769488", 1951));
        books.Add(new Book("Lord of the Flies", "William Golding", "9780399501487", 1954));
        books.Add(new Book("Brave New World", "Aldous Huxley", "9780060850524", 1932));
    }

    public bool AddBook(Book book)
    {
        var existingBook = SearchByISBN(book.ISBN);
        
        var results = new List<ValidationResult>();
        var isValidBook = Validator.TryValidateObject(book, new ValidationContext(book), results, true);
        
        if (!isValidBook || existingBook != null)
        {
            return false;
        }

        books.Add(book);

        return true;
    }

    public bool RemoveBook(string isbn)
    {
        Book? book = SearchByISBN(isbn);
        if (book == null || book.IsBorrowed)
        {
            return false;
        }
        books.Remove(book);
        return true;
    }

    public Book? SearchByISBN(string isbn)
    {
        return books.FirstOrDefault(book => book.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
    }

    public List<Book> SearchByISBNMany(string isbn)
    {
        return books.Where(book => book.ISBN.Contains(isbn, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Book> SearchByTitle(string title)
    {
        return books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Book> SearchByAuthor(string author)
    {
        return books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public bool BorrowBook(string isbn)
    {
        Book? book = SearchByISBN(isbn);
        if (book != null && !book.IsBorrowed)
        {
            book.IsBorrowed = true;
            book.BorrowDate = DateTime.Now;
            return true;
        }
        return false;
    }

    public bool ReturnBook(string isbn)
    {
        Book? book = SearchByISBN(isbn);

        if (book == null || !book.IsBorrowed)
        {
            return false;
        }

        book.IsBorrowed = false;
        book.BorrowDate = null;

        return true;
    }

    public List<Book> GetAllBooks()
    {
        return books;
    }

/// <summary>
/// Calculates the late fee for a book based on the number of days late.
/// </summary>
/// <param name="isbn"></param>
/// <param name="daysLate"></param>
/// <returns>
/// If the book is not found, returns -1.
/// </returns>
    public decimal CalculateLateFee(string isbn, int daysLate)
    {
        Book? book = SearchByISBN(isbn);
        if (book == null)
        {
            return -1;
        }

        if (daysLate <= 0)
        {
            return 0;
        }

        decimal feePerDay = 0.5m;
        return daysLate * feePerDay;
    }

    /// <summary>
    /// Checks if a book is overdue based on the loan period.
    /// </summary>
    /// <param name="isbn"></param>
    /// <param name="loanPeriodDays"></param>
    /// <returns>
    /// If the book is overdue returns true, otherwise false.
    /// </returns>
    public bool IsBookOverdue(string isbn, int loanPeriodDays)
    {
        Book? book = SearchByISBN(isbn);

        if (book == null || !book.IsBorrowed || book.BorrowDate == null)
        {
            return false;
        }

        double borrowedFor = (DateTime.Now - book.BorrowDate.Value).Days;

        return borrowedFor > loanPeriodDays;
    }
}
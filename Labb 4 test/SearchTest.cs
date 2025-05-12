using Labb_4;

namespace Labb_4_test;

[TestClass]
public class SearchTest
{
    [TestMethod]
    [DataRow("1234567899")]
    public void SearchByISBN_CaseSensitiveSearchOnISBN_reternsFoundBook(string ISBN)
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", ISBN, 2023);
        library.AddBook(book);

        // act
        var result = library.SearchByISBN(book.ISBN);

        // assert
        Assert.IsNotNull(result);
    }
    [TestMethod]
    [DataRow("9780060850550")] // 13 digits
    [DataRow("97800608505501")] // 14 digits
    [DataRow("978006085")] // 9 digits
    [DataRow("978006085055+")] // 12 digits and a +
    [DataRow("978006085055!")] // 12 digits and a !
    [DataRow("978006085055a")] // 12 digits and a letter
    [DataRow("")]
    public void SearchByISBN_IsNotValidISBN_ReturnsNull(string isbn)
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", isbn, 2023);

        // act
        var result = library.SearchByISBN(book.ISBN);

        // assert
        Assert.IsNull(result);
    }
    [TestMethod]
    [DataRow("New Book")] // Normal 
    [DataRow("New book")] // First word has forst letter uppercase
    [DataRow("new book")] // Lowercase
    [DataRow("NEW BOOK")] // Uppercase
    public void SearchByTitle_CaseSensitiveSearchOnTitle_reternsFoundBook(string title)
    {
        // arrange
        var library = new LibrarySystem();
        var bookToCompereTo = new Book("New Book", "New Author", "1234567899", 2023);
        var book = new Book(title, "New Author", "1234567899", 2023);
        library.AddBook(bookToCompereTo);
        library.AddBook(book);

        // act
        var bookToCompereToResult = library.SearchByTitle(bookToCompereTo.Title);
        var result = library.SearchByTitle(book.Title);

        // assert
        CollectionAssert.AreEqual(bookToCompereToResult, result);
    }
    [TestMethod]
    [DataRow("New Author")] // Normal 
    [DataRow("New author")] // First word has forst letter uppercase
    [DataRow("new author")] // Lowercase
    [DataRow("NEW AUTHOR")] // Uppercase
    public void SearchByAuthor_CaseSensitiveSearchOnAuther_reternsFoundBook(string auther)
    {
        // arrange
        var library = new LibrarySystem();
        var bookToCompereTo = new Book("New Book", "New Author", "1234567899", 2023);
        var book = new Book("New Book", auther, "1234567899", 2023);
        library.AddBook(bookToCompereTo);
        library.AddBook(book);

        // act
        var bookToCompereToResult = library.SearchByAuthor(bookToCompereTo.Author);
        var result = library.SearchByAuthor(book.Author);

        // assert
        CollectionAssert.AreEqual(bookToCompereToResult, result);
    }
    [TestMethod]
    [DataRow("9780060850550")]
    [DataRow("60850550")]
    [DataRow("978006085")]
    public void SearchByISBN_PartialMatchesSearchOnISBN_reternsFoundBook(string ISBN)
    {
        // arrange
        var library = new LibrarySystem();
        var expected = new Book("New Book", "New Author", "9780060850550", 2023);
        var actualBook = new Book("New Book", "New Author", ISBN, 2023);
        library.AddBook(expected);

        // act
        var actual = library.SearchByISBNMany(actualBook.ISBN);

        // assert
        Assert.IsTrue(actual.Count > 0);
    }
    [TestMethod]
    [DataRow("New Book")]
    [DataRow(" Book")]
    [DataRow("New Bo")]
    [DataRow("New ")]
    public void SearchByTitle_PartialMatchesSearchOnTitle_reternsFoundBooks(string title)
    {
        // arrange
        var library = new LibrarySystem();
        var expected = new Book("New Book", "New Author", "9780060850550", 2023);
        var actualBook = new Book(title, "New Author", "9780060850550", 2023);
        library.AddBook(expected);
        // act
        var actual = library.SearchByTitle(actualBook.Title);

        // assert
        Assert.IsTrue(actual.Count > 0);
    }
    [TestMethod]
    [DataRow("New Author")]
    [DataRow(" Author")]
    [DataRow("New Au")]
    [DataRow("New ")]
    public void SearchByTitle_PartialMatchesSearchOnAuthor_reternsFoundBooks(string author)
    {
        // arrange
        var library = new LibrarySystem();
        var expected = new Book("New Book", "New Author", "9780060850550", 2023);
        var actualBook = new Book("New Book", author, "9780060850550", 2023);
        library.AddBook(expected);
        // act
        var actual = library.SearchByAuthor(actualBook.Author);

        // assert
        Assert.IsTrue(actual.Count > 0);
    }

}

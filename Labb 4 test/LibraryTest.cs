using Labb_4;

namespace Labb_4_test;

[TestClass]
public sealed class LibraryTest
{
    [TestMethod]
    public void AddBook_AddNewBookToLibrary_unikBook()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "1234567890", 2023);
        // act
        var result = library.AddBook(book);
        // assert
        Assert.IsTrue(result, "Failed to add a new book to the library.");
    }

    [TestMethod]
    public void AddBook_AddBookWithoutISBN_Book()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "", 2023);
        library.AddBook(book);
        // act
        var result = library.AddBook(book);
        // assert
        Assert.IsFalse(result, "Failed to prevent adding a book without an ISBN.");
    }
    [TestMethod]
    public void AddBook_AddBookWithExistingISBN_Book()
    {
        // arrange
        var library = new LibrarySystem();
        var book1 = new Book("New Book", "New Author", "1234567890", 2023);
        var book2 = new Book("Another Book", "Another Author", "1234567890", 2023);
        // act
        library.AddBook(book1);
        var result = library.AddBook(book2);
        // assert
        Assert.IsFalse(result, "Failed to prevent adding a book with an existing ISBN.");
    }

    [TestMethod]
    public void RemoveBook_bookShouldBeAbleToRemove_reternTrue()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "1234567899", 2023);
        library.AddBook(book);
        // act
        var result = library.RemoveBook(book.ISBN);
        // assert
        Assert.IsTrue(result, "Could not delete book");
    }

    [TestMethod]
    public void RemoveBook_booksThatAreOnLoanShouldNotBeAbleToBeRemoved_reternFalse()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "1234567899", 2023);
        library.AddBook(book);
        // act
        library.BorrowBook(book.ISBN);
        var result = library.RemoveBook(book.ISBN);
        // assert
        Assert.IsFalse(result);
    }

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

    [TestMethod]
    public void BorrowBook_borrowedBookShouldContainBorrowedProperties_True()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "9780060850550", 2023);
        library.AddBook(book);
        // act
        library.BorrowBook(book.ISBN);
        var actual = library.SearchByISBN(book.ISBN);
        var expected = true;
        // assert
        Assert.AreEqual(actual.IsBorrowed, expected);
        Assert.IsNotNull(actual.BorrowDate);
    }

    [TestMethod]
    public void BorrowBook_borrowedBookCantBeBorrowedAgen_True()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "9780060850550", 2023);
        library.AddBook(book);
        library.BorrowBook(book.ISBN);
        // act
        var actual = library.BorrowBook(book.ISBN);

        // assert
        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void BorrowBook_BorrowBookGetCorrectDate_True()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "9780060850550", 2023);
        library.AddBook(book);
        library.BorrowBook(book.ISBN);

        // act
        var actual = library.SearchByISBN(book.ISBN);
        var expected = DateTime.Now;

        // assert
        Assert.IsTrue(expected.CompareTo(actual.BorrowDate) >= 0);
    }

    [TestMethod]
    public void ReturnBook_BorrowBookGetRest_True()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "9780060850550", 2023);
        library.AddBook(book);
        library.BorrowBook(book.ISBN);

        // act
        var actual = library.ReturnBook(book.ISBN);

        // assert 
        Assert.IsTrue(actual);
    }
}
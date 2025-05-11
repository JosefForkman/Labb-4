using Labb_4;

namespace Labb_4_test;

[TestClass]
public sealed class BookTest
{
    [TestMethod]
    public void GetAllBooks_BooksInLibrary_ReturnsAllBooks()
    {
        // arrange
        var library = new LibrarySystem();
        var book1 = new Book("New Book", "New Author", "9780060850550", 2023);
        var book2 = new Book("Another Book", "Another Author", "9780060850551", 2023);
        library.AddBook(book1);
        library.AddBook(book2);
        // act
        var result = library.GetAllBooks();
        // assert
        Assert.IsTrue(result.Count > 0, "Failed to retrieve books from the library.");
    }
    [TestMethod]
    public void AddBook_AddNewBookToLibrary_unikBook()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "9780060850550", 2023);
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
        var book1 = new Book("New Book", "New Author", "9780060850550", 2023);
        var book2 = new Book("Another Book", "Another Author", "9780060850550", 2023);
        // act
        library.AddBook(book1);
        var result = library.AddBook(book2);
        // assert
        Assert.IsFalse(result, "Failed to prevent adding a book with an existing ISBN.");
    }

    [TestMethod]
    [DataRow("97800608505501")] // 1 digit too long
    [DataRow("978006085")] // 1 digit too short
    [DataRow("97800608505501a")] // contains a letter
    [DataRow("97800608505501!")] // contains a special character (!)
    [DataRow("97800608505501-")] // contains a special character (-)
    public void AddBook_AddBookIncorrectLength_ISBN_Book(string isbn)
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", isbn, 2023);
        // act
        var result = library.AddBook(book);
        // assert
        Assert.IsFalse(result, "Failed to prevent adding a book with an incorrect ISBN length.");
    }
    [TestMethod]
    public void AddBook_BookHaveNoDate_False()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "9780060850550", 0);
        // act
        var result = library.AddBook(book);
        // assert
        Assert.IsFalse(result, "Failed to prevent adding a book with an invalid date.");
    }
    [TestMethod]
    public void RemoveBook_bookShouldBeAbleToRemove_reternTrue()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "9780060850555", 2023);
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
        var book = new Book("New Book", "New Author", "9780060850555", 2023);
        library.AddBook(book);
        // act
        library.BorrowBook(book.ISBN);
        var result = library.RemoveBook(book.ISBN);
        // assert
        Assert.IsFalse(result);
    }

}

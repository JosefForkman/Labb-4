using Labb_4;

namespace Labb_4_test;

[TestClass]
public sealed class BookTest
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

}

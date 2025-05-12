using Microsoft.VisualStudio.TestTools.UnitTesting;

using Labb_4;

namespace Labb_4_test;

[TestClass]
public class ReturnTest
{
    [TestMethod]
    public void ReturnBook_BorrowBookGetRest_True()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "9780060850550", 2023);
        library.AddBook(book);
        library.BorrowBook(book.ISBN);

        // act
        library.ReturnBook(book.ISBN);
        var actual = library.SearchByISBN(book.ISBN);
        var expected = false;

        // assert 
        Assert.IsTrue(actual.IsBorrowed == expected);
        Assert.IsNull(actual.BorrowDate);
    }

    [TestMethod]
    public void ReturnBook_BorrowBookBeReturned_True()
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

    [TestMethod]
    public void ReturnBook_BookNotInLibrary_False()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "9780060850550", 2023);
        library.AddBook(book);
        library.BorrowBook(book.ISBN);

        // act
        var actual = library.ReturnBook("9780060850551");

        // assert
        Assert.IsFalse(actual);
    }
}

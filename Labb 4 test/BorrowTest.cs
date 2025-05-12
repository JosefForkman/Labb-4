using Labb_4;

namespace Labb_4_test;

[TestClass]
public class BorrowTest
{
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
    public void BorrowBook_BookNotInLibrary_False()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "9780060850550", 2023);
        // act
        var actual = library.BorrowBook(book.ISBN);

        // assert
        Assert.IsFalse(actual);
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Labb_4;

namespace Labb_4_test;

[TestClass]
public class DelayTest
{
    [TestMethod]
    [DataRow(-4)]
    [DataRow(-3)] 
    public void IsBookOverdue_BookIsOverdueIfOlderThenTwoDays_True(int loanPeriodDays)
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "9780060850550", 2023);
        library.AddBook(book);
        library.BorrowBook(book.ISBN);

        book.BorrowDate = DateTime.Now.AddDays(loanPeriodDays);

        // act
        var actual = library.IsBookOverdue(book.ISBN, 2);

        // assert
        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void IsBookOverdue_BookWithIncorrectISBN_False()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "", 2023);

        // act
        var actual = library.IsBookOverdue(book.ISBN, 2);

        // assert
        Assert.IsFalse(actual);
    }


    [TestMethod]
    [DataRow(1, 0.5)]
    [DataRow(2, 1)]
    [DataRow(3, 1.5)]
    public void CalculateLateFee_CalculateLateFeeTimesNumberOfDaysLate_FeeIsSameAsExpected(int daysLate, double expected)
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "9780060850550", 2023);

        library.AddBook(book);
        library.BorrowBook(book.ISBN);

        book.BorrowDate = DateTime.Now.AddDays(-daysLate);
        // act
        var actual = library.CalculateLateFee(book.ISBN, daysLate);

        // assert 
        Assert.AreEqual((decimal)expected, actual);
    }

    [TestMethod]
    public void CalculateLateFee_BookWithIncorrectISBN_False()
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "", 2023);

        // act
        var actual = library.CalculateLateFee(book.ISBN, 2);
        var expected = -1;
        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    [DataRow(-2)]
    public void CalculateLateFee_NegativeDaysLate_0(int daysLate)
    {
        // arrange
        var library = new LibrarySystem();
        var book = new Book("New Book", "New Author", "9780060850550", 2023);
        library.AddBook(book);
        library.BorrowBook(book.ISBN);

        // act
        var actual = library.CalculateLateFee(book.ISBN, daysLate);
        var expected = 0;
        // assert
        Assert.AreEqual(expected, actual);
    }
}

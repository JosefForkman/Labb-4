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
    [DataRow(1, 0.5)]
    [DataRow(2, 1)]
    [DataRow(3, 1.5)]
    public void CalculateLateFee_CaleculateLateFeeTimesNumberOfDaysLate_FeeIsSameAsExpected(int daysLate, double expected)
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
}

namespace LibraryApp.Tests;

using LibraryApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// MemberTests - All Unit Tests associated with the Member.cs:
///		CheckOut_AddsBookToCheckedOutBooks,
///		CheckOut_ThrowsException_WhenNoAvailableCopies,
///		CheckOut_ThrowsException_WhenMaxBooksCheckedOut,
///		Return_RemovesBookFromCheckedOutBooks,
///		Return_ThrowsException_WhenBookNotCheckedOut,
///		Return_ThrowsException_WhenBookNotCheckedOutByMember,
/// </summary>

[TestClass]
public class MemberTests
{
    [TestMethod]
    public void CheckOut_AddsBookToCheckedOutBooks()
    {
        // Arrange
        var member = new Member("John Doe", 1);
        var book = new Book("Sample Title", "Sample Author", "0000000000000", 3);
        // Act
        member.CheckOut(book);
        // Assert
        Assert.IsTrue(member.CheckedOutBooks.Contains(book));
    }
    [TestMethod]
    public void CheckOut_ThrowsException_WhenNoAvailableCopies()
    {
        // Arrange
        var member = new Member("John Doe", 1);
        var book = new Book("Sample Title", "Sample Author", "0000000000000", 0);
        // Act
        try
        {
            member.CheckOut(book);
        }
        catch (InvalidOperationException ex)
        {
            // Assert
            Assert.AreEqual($"No available copies of '{book.Title}' to check out.", ex.Message);
            return;
        }
    }
    [TestMethod]
    public void CheckOut_ThrowsException_WhenMaxBooksCheckedOut()
    {
        // Arrange
        var member = new Member("John Doe", 1);
        for (int i = 0; i < 5; i++)
        {
            var book = new Book($"Sample Title {i}", "Sample Author", $"000000000000{i}", 3);
            member.CheckOut(book);
        }
        var extraBook = new Book("Extra Title", "Extra Author", "0000000000005", 3);
        // Act
        try
        {
            member.CheckOut(extraBook);
        }
        // Assert
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual($"Member '{member.Name}' has checked out the max amount of books", ex.Message);
            return;
        }
    }
    [TestMethod]
    public void Return_RemovesBookFromCheckedOutBooks()
    {
        // Arrange
        var member = new Member("John Doe", 1);
        var book = new Book("Sample Title", "Sample Author", "0000000000000", 3);
        member.CheckOut(book);
        // Act
        member.Return(book);
        // Assert
        Assert.IsFalse(member.CheckedOutBooks.Contains(book));
    }
    [TestMethod]
    public void Return_ThrowsException_WhenBookNotCheckedOut()
    {
        // Arrange
        var member = new Member("John Doe", 1);
        var book = new Book("Sample Title", "Sample Author", "0000000000000", 3);
        // Act
        try
        {
            member.Return(book);
        }
        catch (InvalidOperationException ex)
        {
            // Assert
            Assert.AreEqual($"Member '{member.Name}' has not checked out the book '{book.Title}'.", ex.Message);
            return;
        }
    }
    [TestMethod]
    public void Return_ThrowsException_WhenBookNotCheckedOutByMember()
    {
        // Arrange
        var member1 = new Member("John Doe", 1);
        var member2 = new Member("Jane Smith", 2);
        var book = new Book("Sample Title", "Sample Author", "0000000000000", 3);
        member1.CheckOut(book);
        // Act
        try
        {
            member2.Return(book);
        }
        catch (InvalidOperationException ex)
        {
            // Assert
            Assert.AreEqual($"Member '{member2.Name}' has not checked out the book '{book.Title}'.", ex.Message);
            return;
        }
    }
	[TestMethod]
	public void TemporaryFailure()
	{
		Assert.AreEqual(1, 2);  // deliberately wrong
	}
}
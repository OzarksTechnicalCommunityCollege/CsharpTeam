namespace LibraryApp.Tests;

/// <summary>
/// Member-Catalog Tests - All Tests associated with the Catalog.cs that have to do with Member operations:
///     AddMember_AddsMemberToCatalog,
///     AddMember_ThrowsException_WhenMemberAlreadyExists,
///     FindMemberById_ReturnsMember_WhenExists,
///     FindMemberById_ReturnsNull_WhenNotExists,
///     FindMemberById_ThrowsException_WhenInvalidId,
///     SearchMembersByName_ReturnsMatchingMembers_WhenExists,
///     SearchMembersByName_ReturnsEmptyList_WhenNotExists,
///     CheckOutBook_AddsBookToMemberCheckedOutBooks_WhenAvailable,
///     CheckOutBook_ThrowsException_WhenMemberHasMaxBooksCheckedOut,
///     ReturnBook_RemovesBookFromMemberCheckedOutBooks_WhenCheckedOut,
///     ReturnBook_ThrowsException_WhenBookNotCheckedOutByMember,
/// </summary>

[TestClass]
public class MemberCatalogTests
{
    [TestMethod]
    public void AddMember_AddsMemberToCatalog()
    {
        // Arrange
        var catalog = new Catalog();
        var member = new Member("TestMember", 1);

        // Act
        catalog.AddMember(member);

        // Assert
        Assert.IsTrue(catalog.FindMemberById(1) != null);
    }
    [TestMethod]
    public void AddMember_ThrowsException_WhenMemberAlreadyExists()
    {
        // Arrange
        var catalog = new Catalog();
        var member = new Member("TestMember", 1);
        catalog.AddMember(member);
        // Act
        try
        {
            catalog.AddMember(member);
        }
        // Assert 
        catch (InvalidOperationException ex)
        {
            // Assert
            Assert.AreEqual($"A member with ID '1' already exists in the catalog.", ex.Message);
            return;
        }
    }
    [TestMethod]
    public void FindMemberById_ReturnsMember_WhenExists()
    {
        // Arrange
        var catalog = new Catalog();
        var member = new Member("TestMember", 1);
        catalog.AddMember(member);
        // Act
        var foundMember = catalog.FindMemberById(1);
        // Assert
        Assert.IsNotNull(foundMember);
        Assert.AreEqual("TestMember", foundMember.Name);
    }
    [TestMethod]
    public void FindMemberById_ReturnsNull_WhenNotExists()
    {
        // Arrange
        var catalog = new Catalog();
        // Act
        //var foundMember = catalog.FindMemberById(1);

        // Assert
        Assert.Throws<InvalidOperationException>(() =>
        {
            var foundMember = catalog.FindMemberById(1);
        });
    }
    [TestMethod]
    public void FindMemberById_ThrowsException_WhenInvalidId()
    {
        // Arrange
        var catalog = new Catalog();
        // Act and Assert
        Assert.Throws<InvalidOperationException>(() => catalog.FindMemberById(-1));
    }
    [TestMethod]
    public void SearchMembersByName_ReturnsMatchingMembers_WhenExists()
    {
        // Arrange
        var catalog = new Catalog();
        var member1 = new Member("John Doe", 1);
        var member2 = new Member("Jane Smith", 2);
        catalog.AddMember(member1);
        catalog.AddMember(member2);
        // Act
        var results = catalog.SearchMembersByName("John");
        // Assert
        Assert.AreEqual(1, results.Count);
        Assert.AreEqual("John Doe", results[0].Name);
    }
    [TestMethod]
    public void SearchMembersByName_ReturnsEmptyList_WhenNotExists()
    {
        // Arrange
        var catalog = new Catalog();
        var member1 = new Member("John Doe", 1);
        var member2 = new Member("Jane Smith", 2);
        catalog.AddMember(member1);
        catalog.AddMember(member2);
        // Act
        var results = catalog.SearchMembersByName("Alice");
        // Assert
        Assert.AreEqual(0, results.Count);
    }
    [TestMethod]
    public void CheckOutBook_AddsBookToMemberCheckedOutBooks_WhenAvailable()
    {
        // Arrange
        var catalog = new Catalog();
        var member = new Member("John Doe", 1);
        var book = new Book("Sample Title", "Sample Author", "0000000000000", 3);
        catalog.AddMember(member);
        catalog.AddBook(book);
        // Act
        catalog.CheckOutBook("0000000000000", 1);
        // Assert
        Assert.IsTrue(member.CheckedOutBooks.Contains(book));
    }
    [TestMethod]
    public void CheckOutBook_ThrowsException_WhenMemberHasMaxBooksCheckedOut()
    {
        // Arrange
        var catalog = new Catalog();
        var member = new Member("John Doe", 1);
        catalog.AddMember(member);
        for (int i = 0; i < 5; i++)
        {
            var book = new Book($"Sample Title {i}", "Sample Author", $"000000000000{i}", 3);
            catalog.AddBook(book);
            catalog.CheckOutBook($"000000000000{i}", 1);
        }
        var extraBook = new Book("Extra Title", "Extra Author", "0000000000005", 3);
        catalog.AddBook(extraBook);
        // Act and Assert
        Assert.Throws<InvalidOperationException>(() => catalog.CheckOutBook("0000000000005", 1));
    }
    [TestMethod]
    public void ReturnBook_RemovesBookFromMemberCheckedOutBooks_WhenCheckedOut()
    {
        //Test method LibraryApp.Tests.MemberCatalogTests.ReturnBook_RemovesBookFromMemberCheckedOutBooks_WhenCheckedOut threw exception:
        //System.InvalidOperationException: John Doe does not currently have Sample Title checked out.
        // Arrange
        var catalog = new Catalog();
        var member = new Member("John Doe", 1);
        var book = new Book("Sample Title", "Sample Author", "0000000000000", 3);
        catalog.AddMember(member);
        catalog.AddBook(book);
        catalog.CheckOutBook("0000000000000", 1);
        Assert.IsTrue(member.CheckedOutBooks.Contains(book));
        // Act
        catalog.ReturnBook("0000000000000", 1);
        // Assert
        Assert.IsFalse(member.CheckedOutBooks.Contains(book));
    }
    [TestMethod]
    public void ReturnBook_ThrowsException_WhenBookNotCheckedOutByMember()
    {
        // Arrange
        var catalog = new Catalog();
        var member = new Member("John Doe", 1);
        var book = new Book("Sample Title", "Sample Author", "0000000000000", 3);
        catalog.AddMember(member);
        catalog.AddBook(book);
        // Act
        // Assert
        Assert.Throws<InvalidOperationException>(() => catalog.ReturnBook("0000000000000", 1));
    }
}
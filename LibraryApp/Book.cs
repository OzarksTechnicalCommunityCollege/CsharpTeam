namespace LibraryApp;

/// <summary>
/// Represents a single book title in the library catalog.
/// One Book instance represents all copies of that title.
/// </summary>
public class Book
{
    public string Title { get; }
    public string Author { get; }
    public string Isbn { get; }
    public int TotalCopies { get; }
    public int AvailableCopies { get; private set; }
    public string Genre { get; }

    // add audits to this mutable list
    private readonly List<CheckoutRecord> _checkoutHistory = new();
    // readonly version of history for external access, just a readonly copy of _checkoutHistory
    public IReadOnlyList<CheckoutRecord> CheckoutHistory => _checkoutHistory.AsReadOnly();

    public Book(string title, string author, string isbn, int totalCopies, string genre = "General")
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty.", nameof(author));
        if (string.IsNullOrWhiteSpace(isbn))
            throw new ArgumentException("ISBN cannot be empty.", nameof(isbn));
        if (totalCopies < 0)
            throw new ArgumentException("Total copies cannot be negative.", nameof(totalCopies));

        Title = title;
        Author = author;
        Isbn = isbn;
        TotalCopies = totalCopies;
        AvailableCopies = totalCopies;
        // if genre is null or empty, give it genre of general, else assign value
        Genre = string.IsNullOrWhiteSpace(genre) ? "General" : genre;

        // record creation in audit
        _checkoutHistory.Add(new CheckoutRecord(DateTime.UtcNow, $"Added {totalCopies} copies to catalog."));
    }

    public bool IsAvailable => AvailableCopies > 0;

    public void CheckOut()
    {
        if (!IsAvailable)
            throw new InvalidOperationException($"No available copies of '{Title}' to check out.");

        AvailableCopies--;
        // audit checkout
        _checkoutHistory.Add(new CheckoutRecord(DateTime.UtcNow, "Checked out"));
    }

    public void Return()
    {
        if (AvailableCopies >= TotalCopies)
            throw new InvalidOperationException($"All copies of '{Title}' are already returned.");

        AvailableCopies++;
        // audit return
        _checkoutHistory.Add(new CheckoutRecord(DateTime.UtcNow, "Returned"));
    }

    public void AddCopy()
    {
        AvailableCopies++;
        // audit add copy
        _checkoutHistory.Add(new CheckoutRecord(DateTime.UtcNow, "Added a copy"));
    }
}

// record to hold checkout data, cannot be changed after creation, immutable
public record CheckoutRecord(DateTime Timestamp, string Action);

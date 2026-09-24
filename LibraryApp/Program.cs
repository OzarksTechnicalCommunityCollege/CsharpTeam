namespace LibraryApp;

public class Program
{
    public static void Main(string[] args)
    {
        var catalog = new Catalog();

        catalog.AddBook(new Book("The Pragmatic Programmer", "David Thomas", "9780135957059", 3));
        catalog.AddBook(new Book("Clean Code", "Robert C. Martin", "9780132350884", 2));
        catalog.AddBook(new Book("The DevOps Handbook", "Gene Kim", "9781942788003", 1));

        Console.WriteLine("=== Library Catalog ===");
        foreach (var book in catalog.Books)
        {
            Console.WriteLine($"{book.Title} by {book.Author} \u2014 {book.AvailableCopies}/{book.TotalCopies} available");
        }




        Console.WriteLine();
        Console.WriteLine("Checking out 'Clean Code'...");
        catalog.CheckOutBook("9780132350884");

        var cleanCode = catalog.FindByIsbn("9780132350884");
        Console.WriteLine($"'{cleanCode!.Title}' now has {cleanCode.AvailableCopies}/{cleanCode.TotalCopies} available.");

        Console.WriteLine();
        Console.WriteLine($"Total copies available across catalog: {catalog.TotalAvailableCopies()}");
    }
}

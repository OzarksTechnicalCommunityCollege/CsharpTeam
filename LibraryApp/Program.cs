namespace LibraryApp;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var catalog = new Catalog();

            catalog.AddBook(new Book("The Pragmatic Programmer", "David Thomas", "9780135957059", 3, "Programming"));
            catalog.AddBook(new Book("Clean Code", "Robert C. Martin", "9780132350884", 2, "Software Engineering"));
            catalog.AddBook(new Book("The DevOps Handbook", "Gene Kim", "9781942788003", 1, "DevOps"));

            Console.WriteLine("=== Library Catalog ===");
            foreach (var book in catalog.Books)
            {
                Console.WriteLine($"{book.Title} by {book.Author} ({book.Genre}) — {book.AvailableCopies}/{book.TotalCopies} available");
            }

            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Checkout book");
                Console.WriteLine("2. Return Book");
                Console.WriteLine("3. Exit");

                string userChoice = Console.ReadLine();

                if (userChoice == "1")
                {
                    Console.WriteLine("\nEnter the title of the book you would like to check out:");
                    foreach (var book in catalog.Books)
                    {
                        Console.WriteLine(book.Title);
                    }

                    string bookTitle = Console.ReadLine();

                    foreach (var book in catalog.Books)
                    {
                        if (bookTitle == book.Title)
                        {
                            book.CheckOut();
                            Console.WriteLine($"Book has been checked out: {book.AvailableCopies} copies left");
                        }
                    }

                    Console.ReadLine();
                    Console.Clear();
                }
                else if (userChoice == "2")
                {
                    Console.WriteLine("\nEnter the title of the book you would like to return:");
                    foreach (var book in catalog.Books)
                    {
                        Console.WriteLine(book.Title);
                    }

                    string bookReturned = Console.ReadLine();

                    foreach (var book in catalog.Books)
                    {
                        if (bookReturned == book.Title)
                        {
                            book.Return();
                            Console.WriteLine($"\nBook returned: {book.AvailableCopies} copies");
                        }
                    }

                    Console.ReadLine();
                    Console.Clear();
                }
                else if (userChoice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Invalid Input");
                }
            }

            var cleanCode = catalog.FindByIsbn("9780132350884");
            if (cleanCode != null)
            {
                Console.WriteLine();
                Console.WriteLine($"'{cleanCode.Title}' now has {cleanCode.AvailableCopies}/{cleanCode.TotalCopies} available.");

                Console.WriteLine();
                Console.WriteLine("Checkout history for 'Clean Code':");
                foreach (var rec in cleanCode.CheckoutHistory)
                {
                    Console.WriteLine($"- {rec.Timestamp:u} : {rec.Action}");
                }
            }

            // Console.WriteLine();
            // Console.WriteLine($"Total copies available across catalog: {catalog.TotalAvailableCopies()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
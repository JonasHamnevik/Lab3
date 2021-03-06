using Bookstore;
using Bookstore.Data;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

using (var context = new BookstoreContext())
{
    //GetBooks();
    //AddBook();
    //RemoveBook();
    //GetStock();
    GetInventory();

    //Visar alla böcker
    void GetBooks()
    {

        Console.WriteLine("{0, -30} {1, -68} {2, 5}\n", "ISBN13", "Title", "Price");

        foreach (var b in context.Books)
        {
            Console.WriteLine("{0, -10} - {1, -80} - {2, 5}Kr\n", b.Isbn13, b.Title, Math.Round(b.Price, 2));
        }
        Console.WriteLine();
        Console.Write("----------------------------------------------------------");
    }

    //Lägger till en bok
    void AddBook()
    {
        var stock = new Stock { StoreId = 3, Isbn13 = 9781435167902, Quantity = 1 };
        context.Stocks.Add(stock);
        context.SaveChanges();
    }

    //Tar bort boken som lades till
    void RemoveBook()
    {
        var stock = context.Stocks.Single(x => x.StoreId == 3 && x.Isbn13 == 9781435167902);

        context.Stocks.Remove(stock);
        context.SaveChanges();

        Console.WriteLine("Press any key to exit..");
        Console.ReadKey();
    }

    //Visa totala värdet hos varje bokhandel
    void GetInventory()
    {

        var inventory =
            from s in context.Stocks
            join st in context.Stores on s.StoreId equals st.Id
            join b in context.Books on s.Isbn13 equals b.Isbn13
            group new { s.Quantity, b.Price } 
            by new { st.Name } into g
            select new
            {
                g.Key.Name,
                inventory = g.Sum(x => x.Quantity * x.Price) 
            };

        Console.WriteLine("{0, -10} {1, -20}\n", "Store", "Stock balance");

        foreach (var s in inventory)
        {
            Console.WriteLine("{0, -10} {1}kr\n", s.Name, s.inventory);
        }
        Console.WriteLine();
        Console.Write("----------------------------------------------------------");
    }

    //Visa totala antalet böcker
    void GetStock()
    {

        var stockBalance =
            from s in context.Stocks
            join st in context.Stores on s.StoreId equals st.Id
            join b in context.Books on s.Isbn13 equals b.Isbn13
            select new
            {
                st.Name,
                b.Title,
                s.Quantity
            };

        Console.WriteLine("{0} - {1} - {2}\n", "Store", "Title", "Quantity");

        foreach (var s in stockBalance)
        {
            Console.WriteLine("{0} - {1} - {2}\n", s.Name, s.Title, s.Quantity);
        }
        Console.WriteLine();
        Console.Write("----------------------------------------------------------");
    }
}
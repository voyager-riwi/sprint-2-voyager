namespace Sprint_02_Voyager.Services;
using System;
using System.Linq;
using Sprint_02_Voyager.Models;

public class ConsultsAdvances
{
    public static void ConcertByCity(string city, System.Collections.Generic.List<Concerts> concerts)
    {
        var query = concerts.Where(c => c.City == city);
        foreach (var c in query)
            Console.WriteLine($"{c.Name} - {c.City} - {c.Date}");
    }

    public static void ConcertByDateRange(DateTime start, DateTime end, System.Collections.Generic.List<Concerts> concerts)
    {
        var query = concerts.Where(c => c.Date >= start && c.Date <= end);
        foreach (var c in query)
            Console.WriteLine($"{c.Name} - {c.Date}");
    }

    public static void ConcertWithTheMostTicketsSold(System.Collections.Generic.List<Concerts> concerts)
    {
        var top = concerts.OrderByDescending(c => c.TicketsSold).FirstOrDefault();
        if (top != null)
            Console.WriteLine($"{top.Name} vendió {top.TicketsSold} tickets.");
    }

    public static void TotalConcertIncome(int concertId, System.Collections.Generic.List<Tickets> tickets)
    {
        var total = tickets.Where(t => t.ConcertId == concertId).Sum(t => t.Total);
        Console.WriteLine($"Ingresos totales: {total:C}");
    }

    public static void CustomerMorePurchases(System.Collections.Generic.List<Tickets> tickets)
    {
        var top = tickets.GroupBy(t => t.ClientId)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault();

        if (top != null)
            Console.WriteLine($"Cliente {top.Key} hizo {top.Count()} compras.");
    }
}
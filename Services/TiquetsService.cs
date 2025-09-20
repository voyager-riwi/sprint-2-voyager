namespace Sprint_02_Voyager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Sprint_02_Voyager.Models;

public class TicketsService
{
    private List<Tickets> tickets = new();
    private int nextId = 1;

    public void RegisterPurchase(int clientId, int concertId, int quantity, decimal price)
    {
        var total = price * quantity;
        tickets.Add(new Tickets
        {
            Id = nextId++,
            ClientId = clientId,
            ConcertId = concertId,
            Quantity = quantity,
            Total = total,
            PurchaseDate = DateTime.Now
        });

        Console.WriteLine("Compra registrada exitosamente.");
    }

    public void ShowPurchases()
    {
        if (tickets.Count == 0)
        {
            Console.WriteLine("No hay compras registradas.");
            return;
        }

        foreach (var t in tickets)
        {
            Console.WriteLine($"Compra {t.Id}: Cliente {t.ClientId}, Concierto {t.ConcertId}, Cantidad {t.Quantity}, Total {t.Total:C}");
        }
    }

    public List<Tickets> GetAll() => tickets;
}
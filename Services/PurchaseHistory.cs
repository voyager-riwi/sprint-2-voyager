namespace Sprint_02_Voyager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Sprint_02_Voyager.Models;

public class PurchaseHistory
{
    private List<Tickets> tickets;
    private List<Clients> clients;

    public PurchaseHistory(List<Tickets> tickets, List<Clients> clients)
    {
        this.tickets = tickets;
        this.clients = clients;
    }

    // Mostrar todas las compras realizadas por un usuario
    public void ShowPurchasesPerCustomer(int clientId)
    {
        var client = clients.FirstOrDefault(c => c.Id == clientId);

        if (client == null)
        {
            Console.WriteLine("Cliente no encontrado.");
            return;
        }

        var shopping = tickets.Where(t => t.ClientId == clientId).ToList();

        if (shopping.Count == 0)
        {
            Console.WriteLine($"El cliente {client.Name} no tiene compras registradas.");
            return;
        }

        Console.WriteLine($"\nHistorial de compras de {client.Name}:");
        foreach (var t in shopping)
        {
            Console.WriteLine($"Ticket #{t.Id} | Concierto: {t.ConcertId} | Precio: {t.Total:C}");
        }
    }
}
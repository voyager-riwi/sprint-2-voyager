namespace Sprint_02_Voyager.Models;
using System;

public class Tickets
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int ConcertId { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public DateTime PurchaseDate { get; set; }
}
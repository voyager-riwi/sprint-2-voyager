namespace Sprint_02_Voyager.Models;
using System;

public class Concerts
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public int Capacity { get; set; }
    public int TicketsSold { get; set; } = 0;
}
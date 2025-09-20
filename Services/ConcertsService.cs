namespace Sprint_02_Voyager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Sprint_02_Voyager.Models;

public class ConcertsService
{
    private List<Concerts> concerts = new();
    private int nextId = 1;

    public void RegisterConcert()
    {
        try
        {
            Console.Write("Nombre del concierto: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: El nombre del concierto no puede estar vacío.");
                return;
            }

            Console.Write("Ciudad: ");
            string city = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(city))
            {
                Console.WriteLine("Error: La ciudad no puede estar vacía.");
                return;
            }

            Console.Write("Fecha (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Error: Formato de fecha inválido.");
                return;
            }

            if (date <= DateTime.Now)
            {
                Console.WriteLine("Error: La fecha del concierto debe ser futura.");
                return;
            }

            Console.Write("Precio: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
            {
                Console.WriteLine("Error: El precio debe ser un número válido mayor a 0.");
                return;
            }

            Console.Write("Capacidad: ");
            if (!int.TryParse(Console.ReadLine(), out int capacity) || capacity <= 0)
            {
                Console.WriteLine("Error: La capacidad debe ser un número entero mayor a 0.");
                return;
            }

            concerts.Add(new Concerts()
            {
                Id = nextId++,
                Name = name,
                City = city,
                Date = date,
                Price = price,
                Capacity = capacity
            });

            Console.WriteLine("Concierto registrado con éxito.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al registrar concierto: {ex.Message}");
        }
    }

    public void ShowConcerts()
    {
        foreach (var c in concerts)
        {
            Console.WriteLine($"{c.Id} - {c.Name} ({c.City}, {c.Date.ToShortDateString()}) - Capacidad: {c.Capacity}, Precio: {c.Price}, Vendidos: {c.TicketsSold}");
        }
    }

    public Concerts ObtenerPorId(int id) => concerts.FirstOrDefault(c => c.Id == id);

    public void EditConcert(int id)
    {
        try
        {
            var c = ObtenerPorId(id);
            if (c == null) { Console.WriteLine("Concierto no encontrado."); return; }

            Console.Write("Nuevo nombre: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: El nombre del concierto no puede estar vacío.");
                return;
            }

            Console.Write("Nueva ciudad: ");
            string city = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(city))
            {
                Console.WriteLine("Error: La ciudad no puede estar vacía.");
                return;
            }

            Console.Write("Nueva fecha (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Error: Formato de fecha inválido.");
                return;
            }

            if (date <= DateTime.Now)
            {
                Console.WriteLine("Error: La fecha del concierto debe ser futura.");
                return;
            }

            Console.Write("Nuevo precio: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
            {
                Console.WriteLine("Error: El precio debe ser un número válido mayor a 0.");
                return;
            }

            Console.Write("Nueva capacidad: ");
            if (!int.TryParse(Console.ReadLine(), out int capacity) || capacity <= 0)
            {
                Console.WriteLine("Error: La capacidad debe ser un número entero mayor a 0.");
                return;
            }

            c.Name = name;
            c.City = city;
            c.Date = date;
            c.Price = price;
            c.Capacity = capacity;

            Console.WriteLine("Concierto editado con éxito.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al editar concierto: {ex.Message}");
        }
    }

    public void DeleteConcert(int id)
    {
        var c = ObtenerPorId(id);
        if (c != null)
        {
            concerts.Remove(c);
            Console.WriteLine("Concierto eliminado.");
        }
        else Console.WriteLine("No se encontró el concierto.");
    }

    public List<Concerts> GetAll() => concerts;
}
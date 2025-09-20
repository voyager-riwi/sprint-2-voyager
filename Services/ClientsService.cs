namespace Sprint_02_Voyager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Sprint_02_Voyager.Models;

public class ClientsService
{
    private List<Clients> clients = new();
    private int nextId = 1;

    public void RegisterClient()
    {
        try
        {
            Console.Write("Nombre: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: El nombre no puede estar vacío.");
                return;
            }

            Console.Write("Correo: ");
            string email = Console.ReadLine();
            if (!IsValidEmail(email))
            {
                Console.WriteLine("Error: Formato de email inválido.");
                return;
            }

            Console.Write("Teléfono: ");
            string phone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("Error: El teléfono no puede estar vacío.");
                return;
            }

            clients.Add(new Clients { Id = nextId++, Name = name, Email = email, Phone = phone });
            Console.WriteLine("Cliente registrado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al registrar cliente: {ex.Message}");
        }
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    public void ShowClients()
    {
        foreach (var c in clients)
        {
            Console.WriteLine($"{c.Id} - {c.Name} ({c.Email} - {c.Phone})");
        }
    }

    public Clients ObtenerPorId(int id) => clients.FirstOrDefault(c => c.Id == id);

    public void EditClient(int id)
    {
        try
        {
            var c = ObtenerPorId(id);
            if (c == null) { Console.WriteLine("Cliente no encontrado."); return; }

            Console.Write("Nuevo nombre: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: El nombre no puede estar vacío.");
                return;
            }

            Console.Write("Nuevo correo: ");
            string email = Console.ReadLine();
            if (!IsValidEmail(email))
            {
                Console.WriteLine("Error: Formato de email inválido.");
                return;
            }

            Console.Write("Nuevo teléfono: ");
            string phone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("Error: El teléfono no puede estar vacío.");
                return;
            }

            c.Name = name;
            c.Email = email;
            c.Phone = phone;

            Console.WriteLine("Cliente editado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al editar cliente: {ex.Message}");
        }
    }

    public void DeleteClient(int id)
    {
        var c = ObtenerPorId(id);
        if (c != null)
        {
            clients.Remove(c);
            Console.WriteLine("Cliente eliminado.");
        }
        else Console.WriteLine("Cliente no encontrado.");
    }

    public List<Clients> GetAll() => clients;
}

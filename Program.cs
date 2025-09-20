using System;
using Sprint_02_Voyager.Services;

var clientService = new ClientsService();
var concertService = new ConcertsService();
var ticketsService = new TicketsService();
var purchaseHistory = new PurchaseHistory(ticketsService.GetAll(), clientService.GetAll());

bool outSystem = false;
while (!outSystem)
{
    Console.WriteLine("\nBienvenido a RiwiMusic");
    Console.WriteLine("1. Gestión de Conciertos");
    Console.WriteLine("2. Gestión de Clientes");
    Console.WriteLine("3. Gestión de Tickets");
    Console.WriteLine("4. Historial de Compras");
    Console.WriteLine("5. Consultas Avanzadas");
    Console.WriteLine("6. Salir");
    Console.Write("Elige una opción: ");

    switch (Console.ReadLine())
    {
        case "1":
            Console.WriteLine("1. Registrar concierto\n2. Listar conciertos\n3. Editar concierto\n4. Eliminar concierto");
            var opC = Console.ReadLine();
            if (opC == "1") concertService.RegisterConcert();
            if (opC == "2") concertService.ShowConcerts();
            if (opC == "3") 
            { 
                Console.Write("ID: "); 
                if (int.TryParse(Console.ReadLine(), out int editId))
                    concertService.EditConcert(editId);
                else
                    Console.WriteLine("Error: ID inválido.");
            }
            if (opC == "4") 
            { 
                Console.Write("ID: "); 
                if (int.TryParse(Console.ReadLine(), out int deleteId))
                    concertService.DeleteConcert(deleteId);
                else
                    Console.WriteLine("Error: ID inválido.");
            }
            break;

        case "2":
            Console.WriteLine("1. Registrar cliente\n2. Listar clientes\n3. Editar cliente\n4. Eliminar cliente");
            var opCl = Console.ReadLine();
            if (opCl == "1") clientService.RegisterClient();
            if (opCl == "2") clientService.ShowClients();
            if (opCl == "3") 
            { 
                Console.Write("ID: "); 
                if (int.TryParse(Console.ReadLine(), out int editClientId))
                    clientService.EditClient(editClientId);
                else
                    Console.WriteLine("Error: ID inválido.");
            }
            if (opCl == "4") 
            { 
                Console.Write("ID: "); 
                if (int.TryParse(Console.ReadLine(), out int deleteClientId))
                    clientService.DeleteClient(deleteClientId);
                else
                    Console.WriteLine("Error: ID inválido.");
            }
            break;

        case "3":
            Console.WriteLine("1. Registrar compra\n2. Listar compras");
            var opT = Console.ReadLine();
            if (opT == "1")
            {
                try
                {
                    Console.Write("ID Cliente: ");
                    if (!int.TryParse(Console.ReadLine(), out int clientId))
                    {
                        Console.WriteLine("Error: ID de cliente inválido.");
                        break;
                    }

                    Console.Write("ID Concierto: ");
                    if (!int.TryParse(Console.ReadLine(), out int concertId))
                    {
                        Console.WriteLine("Error: ID de concierto inválido.");
                        break;
                    }

                    Console.Write("Cantidad: ");
                    if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Error: La cantidad debe ser un número entero mayor a 0.");
                        break;
                    }

                    var client = clientService.ObtenerPorId(clientId);
                    if (client == null)
                    {
                        Console.WriteLine("Error: Cliente no encontrado.");
                        break;
                    }

                    var concert = concertService.ObtenerPorId(concertId);
                    if (concert == null)
                    {
                        Console.WriteLine("Error: Concierto no encontrado.");
                        break;
                    }

                    if (concert.TicketsSold + quantity > concert.Capacity)
                    {
                        Console.WriteLine($"Error: No hay suficientes tiquetes disponibles. Disponibles: {concert.Capacity - concert.TicketsSold}");
                        break;
                    }

                    ticketsService.RegisterPurchase(clientId, concertId, quantity, concert.Price);
                    concert.TicketsSold += quantity;
                    Console.WriteLine("Compra registrada exitosamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al registrar compra: {ex.Message}");
                }
            }
            if (opT == "2") ticketsService.ShowPurchases();
            break;

        case "4":
            Console.WriteLine("Ingrese ID del cliente:");
            if (int.TryParse(Console.ReadLine(), out int idClient))
            {
                purchaseHistory.ShowPurchasesPerCustomer(idClient);
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            break;
        case "5":
            Console.WriteLine("1. Conciertos por ciudad\n2. Conciertos por rango fechas\n3. Concierto con más tickets vendidos\n4. Ingresos de un concierto\n5. Cliente con más compras");
            var opQ = Console.ReadLine();
            if (opQ == "1") 
            { 
                Console.Write("Ciudad: "); 
                string city = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(city))
                    ConsultsAdvances.ConcertByCity(city, concertService.GetAll());
                else
                    Console.WriteLine("Error: La ciudad no puede estar vacía.");
            }
            if (opQ == "2") 
            { 
                try
                {
                    Console.Write("Fecha inicio: "); 
                    if (DateTime.TryParse(Console.ReadLine(), out var f1))
                    {
                        Console.Write("Fecha fin: "); 
                        if (DateTime.TryParse(Console.ReadLine(), out var f2))
                        {
                            if (f1 <= f2)
                                ConsultsAdvances.ConcertByDateRange(f1, f2, concertService.GetAll());
                            else
                                Console.WriteLine("Error: La fecha de inicio debe ser menor o igual a la fecha fin.");
                        }
                        else
                            Console.WriteLine("Error: Formato de fecha fin inválido.");
                    }
                    else
                        Console.WriteLine("Error: Formato de fecha inicio inválido.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en consulta por rango de fechas: {ex.Message}");
                }
            }
            if (opQ == "3") ConsultsAdvances.ConcertWithTheMostTicketsSold(concertService.GetAll());
            if (opQ == "4") 
            { 
                Console.Write("ID Concierto: "); 
                if (int.TryParse(Console.ReadLine(), out int concertId))
                    ConsultsAdvances.TotalConcertIncome(concertId, ticketsService.GetAll());
                else
                    Console.WriteLine("Error: ID de concierto inválido.");
            }
            if (opQ == "5") ConsultsAdvances.CustomerMorePurchases(ticketsService.GetAll());
            break;

        case "6":
            outSystem = true;
            break;
        
        default:
            Console.WriteLine("Opción no válida, intente de nuevo.");
            break;  
    }
}
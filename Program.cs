using System.Runtime.CompilerServices;

class Program 
{
   static Hotel hotel = new Hotel();
   static Guest guest = new Guest();

    public static void Main(string[] args)
    {      
       
        Console.WriteLine("Välkommen till hotellbokningssytemet");
        
        bool isRunning = true;

        while (isRunning)
        {
            System.Console.WriteLine("Gör ett val: ");
            System.Console.WriteLine("1. Se tillgänliga rum");
            System.Console.WriteLine("2. Checka in gäst");
            System.Console.WriteLine("3. Checka ut gäst");
            System.Console.WriteLine("4. Skriv ut faktura");
            System.Console.WriteLine("5. Skriv ut info om rum");
            System.Console.WriteLine("6. Avsluta programmet");
            int input = int.Parse(Console.ReadLine());
            

            switch (input)
            {
                case 1:
                    hotel.ShowAvailableRooms();
                    break;
                case 2:
                    hotel.CheckInGuest();
                    break;
                case 3:
                    System.Console.WriteLine("Det här är val nr 3");
                    break;
                case 4:
                    System.Console.WriteLine("Det här är val 4");
                    break;
                case 5:
                    System.Console.WriteLine("Det här är val 5");
                    break;
                case 6:
                    System.Console.WriteLine("Avslutar programmet. Välkommen åter!");
                    isRunning = false;
                    break;

            }
        }

    }    
}


class Hotel
{
    private List<Room> rooms = new List<Room>();
    
    public Hotel()
    {
        rooms.Add(new Room {RoomNr = 101, Price = 1000, NumOfBeds = 2});
        rooms.Add(new Room {RoomNr = 102, Price = 1250, NumOfBeds = 3});
        rooms.Add(new Room {RoomNr = 103, Price = 1500, NumOfBeds = 4});
        rooms.Add(new Room {RoomNr = 104, Price = 1000, NumOfBeds = 2});
        rooms.Add(new Room {RoomNr = 105, Price = 1250, NumOfBeds = 3});
        rooms.Add(new Room {RoomNr = 106, Price = 1500, NumOfBeds = 4});
    }    

    public void ShowAvailableRooms()
    {
        foreach(var room in rooms)
        {
            if (!room.IsOccupied)
            {
                System.Console.WriteLine($" Rum {room.RoomNr} Pris {room.Price} med {room.NumOfBeds} antal bäddar är ledigt.");
            }
        }
    }

    public void CheckInGuest()
    {
        System.Console.Write("Ange rumsnummer för incheckning: ");
        int roomNr = int.Parse(Console.ReadLine());
        var room = rooms.Find(r => r.RoomNr == roomNr); 
        if (room !=null)
        {
            System.Console.Write("Ange vilket datum gäst vill checka in: ");
            DateTime checkInDate;
            while (!DateTime.TryParse(Console.ReadLine(), out checkInDate))
            {
                System.Console.WriteLine("Ogiltigt datum. Försök igen (format YYYY-MM-DD)");
            }
            System.Console.Write("Ange datum för utcheckning: ");
            DateTime checkOutDate;
            while (!DateTime.TryParse(Console.ReadLine(), out checkOutDate) || checkOutDate <= checkInDate)
            {
                System.Console.WriteLine("Utchekning behöver vara ett datum efter incheckning. ");
            }
            if (room.IsOccupied && (checkInDate < room.Guest.CheckOutDate && checkOutDate > room.Guest.CheckInDate))
            {
                Console.WriteLine($"Rum {room.RoomNr} är redan bokat från {room.Guest.CheckInDate:yyyy-MM-dd} till {room.Guest.CheckOutDate:yyyy-MM-dd}. Bokning överlappar.");
            }
                               
        }       


       


        if (room != null && !room.IsOccupied)
        {
            System.Console.Write("Skriv in gästens förnamn: ");
            string firstName = Console.ReadLine();
            System.Console.Write("Skriv in gästens efternamn: ");
            string lastName = Console.ReadLine();
            room.Guest = new Guest{FirstName = firstName, Lastname = lastName};
            room.IsOccupied = true;
            System.Console.WriteLine($"Gästen {firstName} {lastName} är nu incheckad i rum {room.RoomNr}");
        }
        else if (room != null && room.IsOccupied)
        {
            System.Console.WriteLine($"Rum {room.RoomNr} är tyvärr redan upptaget");
        }
        else
        {
            System.Console.WriteLine("Inget rum med det rumsnumret");
        }
    }
}

class Room
{
    public int RoomNr {get; set;}
    public int Price {get; set;}
    public int NumOfBeds {get; set;}
    public bool IsOccupied {get; set; } = false;
    public Guest Guest {get; set;}    
}
class Guest
{
    public string FirstName { get; set;}
    public string Lastname {get; set;}
    public DateTime CheckInDate { get; set; } 
    public DateTime CheckOutDate { get; set; } 

}
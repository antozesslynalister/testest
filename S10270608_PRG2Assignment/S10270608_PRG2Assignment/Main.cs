//==========================================================
// Student Number : S10270608A
// Student Name	  : Antozesslyn Alister
// Partner Number : S10267773D 
// Partner Name	  : Joely Lim Kei Cin
//==========================================================




// Basic feature 1 : Load files (airlines and boarding gates)
// added methods onto Terminal class for easyness  
using S10270608_PRG2Assignment;
using System.Net;

Terminal terminal = new Terminal();
terminal.LoadAirlinesFromCSV("airlines.csv");
terminal.LoadBoardingGatesFromCSV("boardinggates.csv");

// Basic feature 4 : List all boarding gates
void DisplayBoardingGates(Terminal terminal)
{
    Console.WriteLine("{0, -15}{1, -10}{2, -10}{3, -10}", "Boarding Gate", "DDJB", "CFFT", "LWTT");

    // iterate thru boarding gates to get value
    foreach (var boardingGate in terminal.BoardingGates.Values)
    {
        Console.WriteLine("{0, -15}{1, -10}{2, -10}{3, -10}",
            boardingGate.GateName,
            boardingGate.SupportsDDJB ? "TRUE" : "FALSE",
            boardingGate.SupportsCFFT ? "TRUE" : "FALSE",
            boardingGate.SupportsLWTT ? "TRUE" : "FALSE");
    }
}
DisplayBoardingGates(terminal);

// Basic Feature 7 : Display full flight details from an airline

// Basic Feature 8 : Modify flight details







// DO NOT TOUCH JOELY LIM 

// Basic feature 2 : Load files (flights)
Dictionary<string, Flight> flightlist = new Dictionary<string, Flight>();
void Load_Flight()
{
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        string? s;
        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(",");
            string flight_num = data[0];
            string origin = data[1];
            string destination = data[2];
            DateTime expected_time = DateTime.Parse(data[3]);
            string status = data[4];
            Flight flight1 = new NORMFlight(flight_num, origin, destination, expected_time, status);
            flightlist.Add(flight_num, flight1);
        }
    }
}
Load_Flight();

// Basic feature 3 : List all flights with their basic information
void Display_Flights()
{
    Console.WriteLine("{0, -15}{1, -15}{2, -15}{3, -15}{4, -15}", "Flight Number: ", "Origin: ", "Destination", "Expected Time: ", "Status: ");
    foreach (KeyValuePair<string, Flight> flight in flightlist)
    {
        Console.WriteLine("{ 0, -15}, { 1, -15}, { 2, -15}, { 3, -15}, { 4, -15}", (flight.Key), (flight.Value.Origin), (flight.Value.Destination), (flight.Value.ExpectedTime), (flight.Value.Status));
    }
}
Display_Flights();

// Basic Feature 5 : Assign a boarding gate to a flight
void BoardingGateToFlight()
{
    Console.Write("Enter your flight number: ");
    string flightnum = Console.ReadLine();
    foreach (KeyValuePair<string, Flight> flight in flightlist)
    {
        if (flight.Key == flightnum)
        {
            Console.WriteLine("{ 0, -15}, { 1, -15}, { 2, -15}, { 3, -15}, { 4, -15}", (flight.Key), (flight.Value.Origin), (flight.Value.Destination), (flight.Value.ExpectedTime), (flight.Value.Status));
        }
        else
        { continue; }
    }
    Console.WriteLine("Enter the boarding gate: ");
    string boarding_gate = Console.ReadLine();
    
}

// Basic Feature 6 : Create a new flight





// Basic Feature 9 : Display scheduled flights in chronological order, with boarding gates assignments where applicable

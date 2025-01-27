//==========================================================
// Student Number : S10270608A
// Student Name	  : Antozesslyn Alister
// Partner Number : S10267773D 
// Partner Name	  : Joely Lim Kei Cin
//==========================================================

using S10270608_PRG2Assignment;
using System.Net;


// Basic feature 1 : Load files (airlines and boarding gates) 

// dictionary to store airlines
Dictionary<string, Airline> airlineList = new Dictionary<string, Airline>();

// to load airlines from the CSV file
void Load_Airlines()
{
    using (StreamReader sr = new StreamReader("airlines.csv"))
    {
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            string[] data = line.Split(",");
            string airlineCode = data[0].Trim();
            string airlineName = data[1].Trim();

            // create an Airline object
            Airline airline = new Airline(airlineCode, airlineName);

            // add the Airline object to the dictionary
            airlineList.Add(airlineCode, airline);
        }
    }
    Console.WriteLine("Loading Airlines...");
}
Console.WriteLine("Loading Airlines...");


// dictionary to store boarding gates
Dictionary<string, BoardingGate> boardingGateList = new Dictionary<string, BoardingGate>();

// method to load boarding gates from the CSV file
void Load_BoardingGates()
{
    using (StreamReader sr = new StreamReader("boardinggates.csv"))
    {
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            string[] data = line.Split(",");
            string gateName = data[0].Trim();
            bool supportsCFFT = bool.Parse(data[1].Trim());
            bool supportsDDJB = bool.Parse(data[2].Trim());
            bool supportsLWTT = bool.Parse(data[3].Trim());

            // create a BoardingGate object
            BoardingGate gate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT, null);

            // add the BoardingGate object to the dictionary
            boardingGateList.Add(gateName, gate);
        }
    }
    Console.WriteLine("Loading Boarding Gates ...");
}




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

// Basic Feature 7 : Display full flight details from an airline

// Basic Feature 8 : Modify flight details






/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// DO NOT TOUCH JOELY LIM 

// Basic feature 2 : Load files (flights)
Dictionary<string, Flight> flightlist = new Dictionary<string, Flight>();
void Load_Flight()
{
    Console.WriteLine("Loading Flights...");
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
            string code = data[4];
            if (code == "DDJB")
            {
                Flight ddjb_flight = new DDJBFlight(flight_num, origin, destination, expected_time);
                flightlist.Add(flight_num, ddjb_flight);
            }
            else if (code =="LWTT")
            {
                Flight lwtt_flight = new LWTTFlight(flight_num, origin, destination, expected_time);
                flightlist.Add(flight_num, lwtt_flight);
            }
            else if (code =="CFFT")
            {
                Flight cfft_flight = new CFFTFlight(flight_num, origin, destination, expected_time);
                flightlist.Add(flight_num, cfft_flight);
            }
        }
    }
    Console.WriteLine($"{flightlist.Count} flights loaded!");
}


// Basic feature 3 : List all flights with their basic information
void Display_Flights()
{
    Console.WriteLine("{0, -15}{1, -15}{2, -15}{3, -15}{4, -15}", "Flight Number: ", "Origin: ", "Destination", "Expected Time: ");
    foreach (KeyValuePair<string, Flight> flight in flightlist)
    {
        Console.WriteLine("{ 0, -15}, { 1, -15}, { 2, -15}, { 3, -15}, { 4, -15}", (flight.Key), (flight.Value.Origin), (flight.Value.Destination), (flight.Value.ExpectedTime));
    }
}


// Basic Feature 5 : Assign a boarding gate to a flight
void BoardingGateToFlight()
{

    Console.Write("Enter Flight Number: ");
    string flightnum = Console.ReadLine();
    Console.Write("Enter Boarding Gate Name: ");
    string boarding_gate = Console.ReadLine();
    foreach (KeyValuePair<string, Flight> flight in flightlist)
    {
        if (flightnum == flight.Key)
        {
            string origin = flight.Value.Origin;
            string destination = flight.Value.Destination;
            DateTime expected_time = flight.Value.ExpectedTime;
            Console.WriteLine($"Flight number: {flightnum}");
            Console.WriteLine($"Origin: {origin}");
            Console.WriteLine($"Destination: {destination}");
            Console.WriteLine($"Expected Time: {expected_time}");
            if ()
            {
                Console.WriteLine($"Special Request Code: None");
            }
            else
            {
                Console.WriteLine($"Special Request Code: {}");
            }
        }
        else 
            { continue; }
    }
    foreach (KeyValuePair<string, BoardingGate> gate in boardingGateList)
    {
        if (gate.Key == boarding_gate)
        {
            bool supportDDJB = gate.Value.SupportsDDJB;
            bool supportCFFT = gate.Value.SupportsCFFT;
            bool supportLWTT = gate.Value.SupportsLWTT;
            Console.WriteLine($"Boarding Gate Name: {boarding_gate}");
            Console.WriteLine($"Support DDJB: {supportDDJB}");
            Console.WriteLine($"Supports CFFT: {supportCFFT}");
            Console.WriteLine($"Supports LWTT: {supportLWTT}");
        }
        else 
            { continue; }
        Console.Write("Would you like to update the status of the flight? (Y/N)")
        string choice = Console.ReadLine();
        if (choice == "Y")
        {
            Console.WriteLine("1. Delayed");
            Console.WriteLine("2. Boarding");
            Console.WriteLine("3. On Time");
            Console.Write("Please select the new status of the flight: ");
            string statusChoice = Console.ReadLine();

    }
}

// Basic Feature 6 : Create a new flight





// Basic Feature 9 : Display scheduled flights in chronological order, with boarding gates assignments where applicable

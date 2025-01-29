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
    int airlineCount = 0;
    try
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

                // increment count
                airlineCount++;
            }
        }
        Console.WriteLine("Loading Airlines...");
        Console.WriteLine($"{airlineCount} Airlines Loaded!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error Loading Airlines {ex.Message}");
    }
}


// dictionary to store boarding gates
Dictionary<string, BoardingGate> boardingGateList = new Dictionary<string, BoardingGate>();

// method to load boarding gates from the CSV file
void Load_BoardingGates()
{
    int gateCount = 0;
    try
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
                BoardingGate gate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT);

                // add the BoardingGate object to the dictionary
                boardingGateList.Add(gateName, gate);

                // increment count
                gateCount++;
            }
        }
        Console.WriteLine("Loading Boarding Gates ...");
        Console.WriteLine($"{gateCount} Boarding Gates Loaded!");
    }
    catch (FileNotFoundException ex)
    {
        Console.WriteLine($"Error File not found. {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error Loading Boarding Gates {ex.Message}");

    }
}




// Basic feature 4 : List all boarding gates (opt 2)
void DisplayBoardingGates(Terminal terminal)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Gate Name",-15} {"DDJB", -10} {"CFFT", -10} {"LWTT", -10}");

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

// Basic Feature 7 : Display full flight details from an airline (
void DisplayFullFlightDetails(Terminal terminal)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Code",-15}{"Airline Name"}");

    // prompt user to enter a 2-letter airline code
    Console.Write("\nEnter the 2-letter Airline Code (e.g., SQ or MH): ");
    string airlineCode = Console.ReadLine()?.ToUpper().Trim();

    // retrieve the Airline object
    if (terminal.Airlines.TryGetValue(airlineCode, out Airline selectedAirline))
    {
        // list all flights for the selected airline
        foreach (var flight in selectedAirline.Flights.Values)
        {
            Console.WriteLine($"Flight Number: {flight.FlightNumber}, Origin: {flight.Origin}, Destination: {flight.Destination}");
        }

        //pPrompt user to select a flight number
        Console.Write("\nEnter the Flight Number: ");
        string flightNumber = Console.ReadLine();

        // retrieve the Flight object
        if (selectedAirline.Flights.TryGetValue(flightNumber, out Flight selectedFlight))
        {
            // display full flight details
            Console.WriteLine("\nFlight Details:");
            Console.WriteLine($"Flight Number: {selectedFlight.FlightNumber}");
            Console.WriteLine($"Airline Name: {selectedAirline.Name}");
            Console.WriteLine($"Origin: {selectedFlight.Origin}");
            Console.WriteLine($"Destination: {selectedFlight.Destination}");
            Console.WriteLine($"Expected Departure/Arrival Time: {selectedFlight.ExpectedTime}");
            Console.WriteLine($"Special Request Code: {selectedFlight.Status ?? "None"}");
            Console.WriteLine($"Boarding Gate: {selectedFlight.boardingGate?.GateName ?? "Unassigned"}");
        }
        else
        {
            Console.WriteLine("Invalid Flight Number. Please try again.");
        }
    }
    else
    {
        Console.WriteLine("Invalid Airline Code. Please try again.");
    }
}




// Basic Feature 8 : Modify flight details (opt 6)
void DisplayAirlinesAndPromptSelection(Terminal terminal)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Code", -15}{"Airline Name", -25}");

    // display all the airlines
    foreach (var airline in terminal.Airlines.Values)
    {
        Console.WriteLine($"{airline.Code, -15}{airline.Name, -25}");
    }

    // prompt user to select an airline by its code
    Console.Write("\nEnter Airline Code: ");
    string selectedCode = Console.ReadLine()?.ToUpper();

    // retrieve the Airline object based on the selected code
    if (terminal.Airlines.ContainsKey(selectedCode))
    {
        Airline selectedAirline = terminal.Airlines[selectedCode];
        DisplayFlightsForAirline(selectedAirline);
    }
    else
    {
        Console.WriteLine("Invalid Airline Code.");
    }
}

void DisplayFlightsForAirline(Airline selectedAirline)
{
    Console.WriteLine($"\nList of Flights for {selectedAirline.Name}");
    Console.WriteLine($"{"Flight Number", -15}{"Airline Name", -25}{"Origin", -25}{"Destination", -25}{"Expected Departure/Arrival Time"}");

    // display each flight's details (Airline Number, Origin, and Destination)
    foreach (var flight in selectedAirline.Flights.Values)
    {
        Console.WriteLine($"{flight.FlightNumber,-20}{selectedAirline.Name,-20}{flight.Origin,-25}{flight.Destination,-25}{flight.ExpectedTime:dd/MM/yyyy hh:mm tt}");
    }

    // ask the user to either modify or delete a flight
    Console.Write("\nChoose an existing Flight to modify or delete:\nEnter Flight Number: ");
    string flightNumber = Console.ReadLine()?.ToUpper();

    if (selectedAirline.Flights.ContainsKey(flightNumber))
    {
        Flight selectedFlight = selectedAirline.Flights[flightNumber];
        Console.WriteLine("\n1. Modify Flight");
        Console.WriteLine("2. Delete Flight");
        Console.Write("Choose an option: ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            ModifyFlight(selectedAirline, selectedFlight);
        }
        else if (choice == "2")
        {
            DeleteFlight(selectedAirline, selectedFlight);
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }
    else
    {
        Console.WriteLine("Flight not found.");
    }
}

// modify the flight details for choice 1
void ModifyFlight(Airline selectedAirline, Flight selectedFlight)
{
    Console.WriteLine("\n1. Modify Basic Information");
    Console.WriteLine("2. Modify Status");
    Console.WriteLine("3. Modify Special Request Code");
    Console.WriteLine("4. Modify Boarding Gate");
    Console.Write("Choose an option: ");
    string modifyChoice = Console.ReadLine();

    if (modifyChoice == "1")
    {
        ModifyBasicInformation(selectedFlight);
    }
    else if (modifyChoice == "2")
    {
        ModifyStatus(selectedFlight);
    }
    else if (modifyChoice == "3")
    {
        ModifySpecialRequestCode(selectedFlight);
    }
    else if (modifyChoice == "4")
    {
        ModifyBoardingGate(selectedFlight);
    }
    else
    {
        Console.WriteLine("Invalid Choice!");
    }

    Console.WriteLine("Flight updated!");
    DisplayFlightDetails(selectedFlight, selectedAirline, boardingGateList); // Pass the updated values

}

// for modification choice 1 
void ModifyBasicInformation(Flight selectedFlight)
{
    Console.Write("Enter new Origin: ");
    string origin = Console.ReadLine();
    Console.Write("Enter new Destination: ");
    string destination = Console.ReadLine();
    Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy hh:mm): ");
    string timeInput = Console.ReadLine();

    if (DateTime.TryParse(timeInput, out DateTime newTime) && !string.IsNullOrWhiteSpace(origin) && !string.IsNullOrWhiteSpace(destination))
    {
        selectedFlight.Origin = origin;
        selectedFlight.Destination = destination;
        selectedFlight.ExpectedTime = newTime;
        Console.WriteLine("Flight updated!");
    }
    else
    {
        Console.WriteLine("Flight not updated!");
    }
}

// for modification choice 2
void ModifyStatus(Flight selectedFlight)
{
    Console.Write("Enter new Status: ");
    string status = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(status))
    {
        selectedFlight.Status = status;
        Console.WriteLine("Status updated successfully!");
    }
    else
    {
        Console.WriteLine("Status not updated successfully!");
    }
}

// for modification choice 3
void ModifySpecialRequestCode(Flight selectedFlight)
{
    string SpecialRequestCode = "";
    Console.Write("Enter new Special Request Code: ");
    string requestCode = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(requestCode))
    {
        selectedFlight.specialRequestCode = requestCode;
        Console.WriteLine("Special Request Code updated successfully!");
    }
    else
    {
        Console.WriteLine("Special Request Code cannot be empty. Update canceled.");
    }
}

// for modification choice 4
void ModifyBoardingGate(Flight selectedFlight)
{
    Console.Write("Enter new Boarding Gate: ");
    string gate = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(gate))
    {
        selectedFlight.boardingGate = new BoardingGate(gate, false, false, false);
        Console.WriteLine("Boarding Gate updated successfully!");
    }
    else
    {
        Console.WriteLine("Boarding Gate not updated successfully!");
    }
}




// delete the flight for choice 2
void DeleteFlight(Airline selectedAirline, Flight selectedFlight)
{
    Console.WriteLine($"\nAre you sure you want to delete Flight {selectedFlight.FlightNumber}? (Y/N): ");
    string confirmation = Console.ReadLine()?.ToUpper();

    if (confirmation == "Y")
    {
        selectedAirline.Flights.Remove(selectedFlight.FlightNumber);
        Console.WriteLine("Flight deleted successfully.");
    }
    else
    {
        Console.WriteLine("Flight deletion canceled.");
    }
}

void DisplayFlightDetails(Flight flight, Airline selectedAirline, Dictionary<string, BoardingGate> boardingGateList)
{
    Console.WriteLine($"\nFlight Number: {flight.FlightNumber}");
    Console.WriteLine($"Airline Name: {selectedAirline.Name}");
    Console.WriteLine($"Origin: {flight.Origin}");
    Console.WriteLine($"Destination: {flight.Destination}");
    Console.WriteLine($"Expected Departure/Arrival Time: {flight.ExpectedTime:dd/MM/yyyy hh:mm tt}");
    Console.WriteLine($"Status: {flight.Status}");

    // Handle Special Request Code
    Console.WriteLine($"Special Request Code: {flight.specialRequestCode ?? "None"}");

    // Handle Boarding Gate (Check if it exists in the boardingGateList)
    string boardingGate = boardingGateList.ContainsKey(flight.FlightNumber) ? boardingGateList[flight.FlightNumber].GateName : "Unassigned";
    Console.WriteLine($"Boarding Gate: {boardingGate}");
}

// Advanced Feature (b) : Display the total fee per airline for the day
// Method to calculate and display total fee per airline for the day
void CalculateTotalFeePerAirline(Terminal terminal)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Total Fees Per Airline for the Day");
    Console.WriteLine("=============================================");

    // Check if all flights have assigned boarding gates
    foreach (var airline in terminal.Airlines.Values)
    {
        foreach (var flight in airline.Flights.Values)
        {
            if (flight.boardingGate == null)
            {
                Console.WriteLine("Error: Not all flights have assigned boarding gates.");
                Console.WriteLine("Please ensure all flights have boarding gates assigned.");
                return;
            }
        }
    }

    decimal totalFees = 0;
    decimal totalDiscounts = 0;

    foreach (var airline in terminal.Airlines.Values)
    {
        decimal airlineSubtotal = 0;
        decimal airlineDiscounts = 0;

        // Retrieve all flights for the airline
        foreach (var flight in airline.Flights.Values)
        {
            airlineSubtotal += (decimal)flight.CalculateFees();
        }

        // airline-specific discounts based on promotional conditions
        airlineDiscounts = ComputeDiscounts(airline);

        totalFees += airlineSubtotal;
        totalDiscounts += airlineDiscounts;

        Console.WriteLine($"{airline.Name}: Subtotal: ${airlineSubtotal}, Discounts: -${airlineDiscounts}, Final Total: ${airlineSubtotal - airlineDiscounts}");
    }

    // Display the overall total fees and discounts
    Console.WriteLine("=============================================");
    Console.WriteLine($"Total Airline Fees: ${totalFees}");
    Console.WriteLine($"Total Airline Discounts: -${totalDiscounts}");
    Console.WriteLine($"Final Total Fees Collected: ${totalFees - totalDiscounts}");
    Console.WriteLine($"Discount Percentage: {((totalFees > 0) ? (totalDiscounts / totalFees) * 100 : 0):F2}%");
}

// Helper method to compute discounts for an airline
decimal ComputeDiscounts(Airline airline)
{
    return airline.Flights.Count > 10 ? airline.Flights.Count * 50 : 0; // Example: $50 discount per flight if more than 10 flights
}





////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Main Program : display menu and runs the entire program
void DisplayMenu()
{
    Console.WriteLine("=============================================Welcome to Changi Airport Terminal 5=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");
}
void Main()
{
    string option = "";
    while (option != "0")
    {
        DisplayMenu();
        Console.Write("Enter your option: ");
        option = Console.ReadLine();
        if (option == "1")
        {

        }
        else if (option == "2")
        {

        }
        else if (option == "3")
        {

        }
        else if (option == "4")
        {

        }
        else if (option == "5")
        {

        }
        else if (option == "6")
        {
            NewFlight();
            Console.Write("Would you like to add another flight? (Y/N)");
            string anotherFlight = Console.ReadLine().ToUpper();
            if (anotherFlight == "Y")
            {
                NewFlight();
            }
            else if (anotherFlight == "N")
            {
                { break; }
            }
            else
            {
                Console.WriteLine("Invalid Option!");
            }
        }
        else if (option == "7")
        {

        }
        else if (option == "0")
        {
            { break; }
        }
        else
        { Console.WriteLine("Invalid option! Choose an option from 0 - 7"); }
    }
}



////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// DO NOT TOUCH JOELY LIM 

// Basic feature 2 : Load files (flights)
Dictionary<string, Flight> flightdict = new Dictionary<string, Flight>();
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
                flightdict.Add(flight_num, ddjb_flight);
            }
            else if (code =="LWTT")
            {
                Flight lwtt_flight = new LWTTFlight(flight_num, origin, destination, expected_time);
                flightdict.Add(flight_num, lwtt_flight);
            }
            else if (code =="CFFT")
            {
                Flight cfft_flight = new CFFTFlight(flight_num, origin, destination, expected_time);
                flightdict.Add(flight_num, cfft_flight);
            }
        }
    }
    Console.WriteLine($"{flightdict.Count} flights loaded!");
}


// Basic feature 3 : List all flights with their basic information
void Display_Flights()
{
    Console.WriteLine("{0, -15}{1, -15}{2, -15}{3, -15}{4, -15}", "Flight Number: ", "Origin: ", "Destination", "Expected Time: ");
    foreach (KeyValuePair<string, Flight> flight in flightdict)
    {
        Console.WriteLine("{ 0, -15}, { 1, -15}, { 2, -15}, { 3, -15}, { 4, -15}", (flight.Key), (flight.Value.Origin), (flight.Value.Destination), (flight.Value.ExpectedTime));
    }
}


// Basic Feature 5 : Assign a boarding gate to a flight
void BoardingGateToFlight()
{
    Console.Write("Enter Flight Number: ");
    string flightnum = Console.ReadLine();
    if (!flightdict.ContainsKey(flightnum))
    {
        Console.WriteLine("Flight number does not exist!");
        return;
    }

    Console.Write("Enter Boarding Gate Name: ");
    string boarding_gate = Console.ReadLine();
    if (!boardingGateList.ContainsKey(boarding_gate))            //check if boarding gate typed in exists
    {
        Console.WriteLine("Boarding Gate does not exist!");
        return;
    }
    if (boardingGateList[boarding_gate].Flight != null)        //check if boarding gate has been assigned before
    {
        Console.WriteLine("This boarding gate has already been assigned to another flight!");
        return;
    }
    //ALL THE DISPLAYING
    string origin = flightdict[flightnum].Origin;                 // displaying the flight details 
    string destination = flightdict[flightnum].Destination;
    DateTime expected_time = flightdict[flightnum].ExpectedTime;
    Console.WriteLine($"Flight number: {flightnum}");
    Console.WriteLine($"Origin: {origin}");
    Console.WriteLine($"Destination: {destination}");
    Console.WriteLine($"Expected Time: {expected_time}");
    if (flightdict[flightnum] is NORMFlight)
    {
        Console.WriteLine("Special Request Code: None");
    }
    else if (flightdict[flightnum] is DDJBFlight)
    {
        Console.WriteLine("Special Request Code: DDJB");
    }
    else if (flightdict[flightnum] is LWTTFlight)
    {
        Console.WriteLine("Special Request Code: LWTT");
    }
    else if (flightdict[flightnum] is CFFTFlight)
    {
        Console.WriteLine("Special Request Code: CFFT");
    }
    bool supportDDJB = boardingGateList[boarding_gate].SupportsDDJB;             // displaying of the boarding gate
    bool supportCFFT = boardingGateList[boarding_gate].SupportsCFFT;
    bool supportLWTT = boardingGateList[boarding_gate].SupportsLWTT;
    Console.WriteLine($"Boarding Gate Name: {boarding_gate}");
    Console.WriteLine($"Support DDJB: {supportDDJB}");
    Console.WriteLine($"Supports CFFT: {supportCFFT}");
    Console.WriteLine($"Supports LWTT: {supportLWTT}");


    Console.Write("Would you like to update the status of the flight? (Y/N)");            // changing the status of the flight
    string choice = Console.ReadLine();
    if (choice == "Y")
    {
        Console.WriteLine("1. Delayed");
        Console.WriteLine("2. Boarding");
        Console.WriteLine("3. On Time");
        Console.Write("Please select the new status of the flight: ");
        string statusChoice = Console.ReadLine();
        if (statusChoice == "1")
        { flightdict[flightnum].Status = "Delayed"; }
        else if (statusChoice == "2")
        { flightdict[flightnum].Status = "Boarding"; }
        else if (statusChoice == "3")
        { flightdict[flightnum].Status = "On Time"; }
        else
        { Console.WriteLine("Invalid choice. Status is not updated."); }
        Console.WriteLine($"Flight {flightnum} has been assigned to Boarding Gate {boarding_gate}!");
    }

    else if (choice == "N")
    {
        Console.WriteLine($"Flight {flightnum} has been assigned to Boarding Gate {boarding_gate}!");
    }
    boardingGateList[boarding_gate].Flight = flightdict[flightnum];     //WHY THO?
}

// Basic Feature 6 : Create a new flight
void NewFlight()
{
    Console.Write("Enter Flight Number: ");
    string flightNum = Console.ReadLine();
    Console.Write("Enter Origin: ");
    string flightOrigin = Console.ReadLine();
    Console.Write("Enter Destination: ");
    string flightDestination = Console.ReadLine();
    Console.Write("Enter Expected Departure/Arrival Time (dd:mm:yyyy hh:mm): ");
    DateTime flightTime = DateTime.Parse(Console.ReadLine());
    Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
    string code = Console.ReadLine().ToUpper();
    Flight newFlight = null;
    if (code == "DDJB")
    {
        newFlight = new DDJBFlight(flightNum, flightOrigin, flightDestination, flightTime);
    }
    else if (code == "CFFT")
    {
        newFlight = new CFFTFlight(flightNum, flightOrigin, flightDestination, flightTime);
    }
    else if (code == "LWTT")
    {
        newFlight = new LWTTFlight(flightNum, flightOrigin, flightDestination, flightTime);
    }
    else if (code == "NONE")
    {
        newFlight = new NORMFlight(flightNum, flightOrigin, flightDestination, flightTime);
    }
    else
    { Console.WriteLine("Invalid option!"); }
    if (newFlight != null)
    {
        flightdict.Add(flightNum, newFlight);
        Console.WriteLine($"Flight {flightNum} has been added!");
    }
    using (StreamWriter sw = new StreamWriter("flights.csv"))
    {
        if (code == "NONE")
        {
            string addFlight = flightNum + "," + flightOrigin + "," + flightDestination + "," + flightTime + ",";
            sw.WriteLine(addFlight);
        }
        else
        {
            string addFlight = flightNum + "," + flightOrigin + "," + flightDestination + "," + flightTime + "," + code;
            sw.WriteLine(addFlight);
        }
    }
}


// Basic Feature 9 : Display scheduled flights in chronological order, with boarding gates assignments where applicable
void SortedFlights()
{
    List <Flight> flights = new List <Flight>();
    foreach (KeyValuePair<string, Flight> flight in flightdict)
    {
        flights.Add(flight.Value);
    }
    flights.Sort();
}
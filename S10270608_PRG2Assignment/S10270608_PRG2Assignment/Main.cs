//==========================================================
// Student Number : S10270608A
// Student Name	  : Antozesslyn Alister
// Partner Number : S10267773D 
// Partner Name	  : Joely Lim Kei Cin
//==========================================================

using S10270608_PRG2Assignment;
using System.Net;
using System.Runtime.InteropServices;

// To create terminal 5 dictioanry: stores airlines and boarding gates at terminal 5 and the gate fees as a dictionary
Terminal terminal5 = new Terminal("Terminal 5", new Dictionary<string, Airline>(), new Dictionary<string, BoardingGate>(), new Dictionary<string, double>());

// Basic feature 1 : Load files (airlines and boarding gates) 

// dictionary to store airlines
Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
// method to load airlines from the CSV file
void Load_Airlines()
{
    Console.WriteLine("Loading Airlines...");

    if (!File.Exists("airlines.csv")) // check if file exists
    {
        Console.WriteLine("Error: Airlines CSV file does not exist.");
        return;
    }

    try
    {
        string[] airlinesLines = File.ReadAllLines("airlines.csv");

        // iterate through each line in the airlines CSV 
        foreach (string line in airlinesLines)
        {
            // skip the header
            if (line == airlinesLines[0])
            {
                continue;
            }
            else
            {
                // split each line by commas to get the airline details
                string[] airlinesDetails = line.Split(',');

                // create Airline object
                Airline airline = new Airline(airlinesDetails[0], airlinesDetails[1]);
                airline.Flights = new Dictionary<string, Flight>(); // intialize empty dict to store flights for the airline

                // add the new airline object to the dictionary using airline code as key
                airlineDict.Add(airlinesDetails[1], airline);
                terminal5.AddAirline(airline); // add the airline obj to terminal5 obj for later use
            }
        }
        Console.WriteLine($"{airlineDict.Count} Airlines Loaded!");
    }
    catch (FileNotFoundException ex)
    { // handle exception if the file is not found
        Console.WriteLine($"Error: File not found. {ex.Message}");
    }
    catch (Exception ex)
    { // handle other exceptions that may occur
        Console.WriteLine($"Error Loading Airlines: {ex.Message}");
    }
}

// dictionary to store boarding gates
Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();

// method to load boarding gates from the CSV file
void Load_BoardingGates()
{
    Console.WriteLine("Loading Boarding Gates...");

    if (!File.Exists("boardinggates.csv")) // check if file exists
    {
        Console.WriteLine("Error: Boarding Gates CSV file does not exist.");
        return;
    }

    try
    {
        string[] boardingGatesLines = File.ReadAllLines("boardinggates.csv");

        // iterate through each line in the boarding gates CSV file
        foreach (string line in boardingGatesLines)
        {
            // skip the header 
            if (line == boardingGatesLines[0])
            {
                continue;
            }
            else
            {
                // split each line by commas to get boarding gate details
                string[] boardingGatesDetails = line.Split(',');

                // create boarding gate object 
                BoardingGate boardingGate = new BoardingGate(
                    boardingGatesDetails[0],  // Gate ID
                    Convert.ToBoolean(boardingGatesDetails[1]),   // DDJB
                    Convert.ToBoolean(boardingGatesDetails[2]),  // CFFT
                    Convert.ToBoolean(boardingGatesDetails[3])  // LWTT
                );

                // add the new boarding gate object to the dict
                boardingGateDict.Add(boardingGatesDetails[0], boardingGate);
                terminal5.AddBoardingGate(boardingGate); // add boarding gate obj to terminal5 obj for later
            }
        }
        Console.WriteLine($"{boardingGateDict.Count} Boarding Gates Loaded!");
    }
    catch (FileNotFoundException ex)
    { // handle exception if the file is not found
        Console.WriteLine($"Error: File not found. {ex.Message}");
    }
    catch (Exception ex)
    { // handle other exceptions that may occur
        Console.WriteLine($"Error Loading Boarding Gates: {ex.Message}");
    }
}


// Basic feature 2 : Load files (flights)
Dictionary<string, Flight> flightdict = new Dictionary<string, Flight>();
void Load_Flight()
{
    try
    {
        Console.WriteLine("Loading Flights...");
        using (StreamReader sr = new StreamReader("flights.csv"))
        {
            sr.ReadLine();                                              // skip header 
            string? s;
            while ((s = sr.ReadLine()) != null)
            {
                if ((string.IsNullOrEmpty(s)))              // to prevent any any error caused by empty new lines
                {
                    break;
                }
                string[] data = s.Split(",");
                string flight_num = data[0];
                string origin = data[1];
                string destination = data[2];
                DateTime expected_time = DateTime.Parse(data[3]);
                string code = data[4];
                string airlineCode = flight_num.Split(" ")[0];                     // retrieving the airline code ("SQ" from "SQ 512")
                Flight flight;

                if (code == "DDJB")
                {
                    flight = new DDJBFlight(flight_num, origin, destination, expected_time);
                }
                else if (code == "LWTT")
                {
                    flight = new LWTTFlight(flight_num, origin, destination, expected_time);
                }
                else if (code == "CFFT")
                {
                    flight = new CFFTFlight(flight_num, origin, destination, expected_time);
                }
                else
                {
                    flight = new NORMFlight(flight_num, origin, destination, expected_time);
                }
                flightdict.Add(flight_num, flight);
                if (airlineDict.ContainsKey(airlineCode))
                {
                    airlineDict[airlineCode].Flights.Add(flight_num, flight);                    // adding the flights to the airline's flight dictionary
                }
                else
                {
                    Console.WriteLine($"{airlineCode} is not found!");                          // lets user know the airline code is not found
                }
            }
        }
        Console.WriteLine($"{flightdict.Count} flights loaded!");
    }
    catch (FileNotFoundException ex)
    {
        Console.WriteLine($"File was not found. {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading the flight file. {ex.Message}");
    }
}

// Basic feature 3 : List all flights with their basic information
void Display_Flights()
{
    Console.WriteLine("{0, -15}{1, -23}{2, -23}{3, -15}", "Flight Number", "Origin", "Destination", "Expected Time");
    foreach (KeyValuePair<string, Flight> flight in flightdict)
    {
        Console.WriteLine("{0, -15}{1, -23}{2, -23}{3, -15}", flight.Key, flight.Value.Origin, flight.Value.Destination, flight.Value.ExpectedTime);
    }
}

// Basic feature 4 : List all boarding gates 
void DisplayBoardingGates()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("\nList of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Gate Name",-15} {"DDJB",-10} {"CFFT",-10} {"LWTT",-10}");

    // iterate through all boarding gates in the dict
    foreach (BoardingGate boardinggate in boardingGateDict.Values)
    {
        Console.WriteLine($"{boardinggate.GateName,-16}{boardinggate.SupportsDDJB,-11}{boardinggate.SupportsCFFT,-11}{boardinggate.SupportsLWTT,-11}");
    }
}

// Basic Feature 5 : Assign a boarding gate to a flight
void BoardingGateToFlight()
{
    try
    {
        string flightnum;
        string boarding_gate;
        while (true)                                                                   // makes sure they enter an acceptable flight number, if not try again
        {
            Console.Write("Enter Flight Number: ");
            flightnum = Console.ReadLine().ToUpper();
            if (!flightdict.ContainsKey(flightnum))
            {
                Console.WriteLine("Flight number does not exist! Please try again.");
            }
            else
            {
                break;
            }
        }

        while (true)                                                                             // data validation for invalid input
        {
            Console.Write("Enter Boarding Gate Name: ");
            boarding_gate = Console.ReadLine().ToUpper();
            if (!boardingGateDict.ContainsKey(boarding_gate))            //check if boarding gate typed in exists
            {
                Console.WriteLine("Boarding Gate does not exist!");
            }
            else if (boardingGateDict[boarding_gate].Flight != null)        //check if boarding gate has been assigned before
            {
                Console.WriteLine("This boarding gate has already been assigned to another flight!");
            }
            else
            {
                break;
            }
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
        bool supportDDJB = boardingGateDict[boarding_gate].SupportsDDJB;             // displaying of the boarding gate
        bool supportCFFT = boardingGateDict[boarding_gate].SupportsCFFT;
        bool supportLWTT = boardingGateDict[boarding_gate].SupportsLWTT;
        Console.WriteLine($"Boarding Gate Name: {boarding_gate}");
        Console.WriteLine($"Support DDJB: {supportDDJB}");
        Console.WriteLine($"Supports CFFT: {supportCFFT}");
        Console.WriteLine($"Supports LWTT: {supportLWTT}");

        while (true)                                                                             // data validation for invalid input
        {
            Console.Write("Would you like to update the status of the flight? (Y/N) ");            // changing the status of the flight
            string choice = Console.ReadLine().ToUpper();
            if (choice == "Y")                                                                  //if they would like to update the status of the flight
            {
                Console.WriteLine("1. Delayed");
                Console.WriteLine("2. Boarding");
                Console.WriteLine("3. On Time");
                while (true)
                {
                    Console.Write("Please select the new status of the flight: ");                    // options to update
                    string statusChoice = Console.ReadLine();
                    if (statusChoice == "1")                                                          // updating the status 
                    {
                        flightdict[flightnum].Status = "Delayed";
                        break;
                    }
                    else if (statusChoice == "2")
                    {
                        flightdict[flightnum].Status = "Boarding";
                        break;
                    }
                    else if (statusChoice == "3")
                    {
                        flightdict[flightnum].Status = "On Time";
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Status is not updated.");
                    }
                }
                break;
            }

            else if (choice == "N")                                                            // if they choose no, status set to on time
            {
                Console.WriteLine("Status is set to 'On Time'");
                flightdict[flightnum].Status = "On Time";
                break;

            }
            else
            {
                Console.WriteLine("Invalid option. Type in Y or N.");                              //if they did not choose an option from 1 - 3
            }
        }
        Console.WriteLine($"Flight {flightnum} has been assigned to Boarding Gate {boarding_gate}!");        // display that it has been updated

        boardingGateDict[boarding_gate].Flight = flightdict[flightnum];     // assigning the boarding gate to the flight
    }
    catch (KeyNotFoundException kex)
    {
        Console.WriteLine($"A key was not found. {kex.Message}");
    }
    catch (FormatException fex)
    {
        Console.WriteLine($"Invalid format. Details: {fex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }
}

// Basic Feature 6 : Create a new flight
void NewFlight()
{
    try
    {
        string flightNum;
        string flightOrigin;
        string flightDestination;
        DateTime flightTime;
        string code;

        // all the inputs and saving the inputs to a variable
        while (true)                                                                             // data validation for invalid input 
        {
            Console.Write("Enter Flight Number: ");
            flightNum = Console.ReadLine();
            if (flightdict.ContainsKey(flightNum))
            {
                Console.WriteLine("Flight number already exists! Please enter a unique flight number.");
            }
            else if (string.IsNullOrWhiteSpace(flightNum))
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
            else
            {
                break;
            }
        }
        while (true)                                                                             // data validation for invalid input
        {
            Console.Write("Enter Origin: ");
            flightOrigin = Console.ReadLine();
            if (!string.IsNullOrEmpty(flightOrigin))
            {
                break;
            }
            else
            {
                Console.WriteLine("Origin could be empty. Please enter a valid origin.");
            }
        }
        while (true)                                                                             // data validation for invalid input
        {
            Console.Write("Enter Destination: ");
            flightDestination = Console.ReadLine();
            if (!string.IsNullOrEmpty(flightDestination))
            {
                break;
            }
            else
            {
                Console.WriteLine("Destination could be empty. Please enter a valid destination.");
            }
        }
        while (true)                                                                             // data validation for invalid input
        {
            Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
            try
            {
                flightTime = DateTime.Parse(Console.ReadLine());
                break;
            }
            catch
            {
                Console.WriteLine("Invalid data format! Please type it in the dd/MM/yyy hh:mm format.");
            }
        }
        while (true)                                                                             // data validation for invalid input
        {
            Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            code = Console.ReadLine().ToUpper();                               // making the code input all uppercase 
            if (code == "DDJB" || code == "CFFT" || code == "LWTT" || code == "NONE")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid option! Please enter CFFT, DDJB, LWTT or None");
            }
        }
        Flight newFlight = null;                                                  // setting the flight to null first as the new flight has not been added yet 

        //assigning the flights to the correct class based on their special request code
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
        if (newFlight != null)                                  // the flight has been successfully added
        {
            flightdict.Add(flightNum, newFlight);
            Console.WriteLine($"Flight {flightNum} has been added!");
        }

        using (StreamWriter sw = new StreamWriter("flights.csv", true))                  // appending the new flight into the csv file 
        {
            if (code == "NONE")                                         // if there is no special request code, the format has no code at the end
            {
                string addFlight = flightNum + "," + flightOrigin + "," + flightDestination + "," + flightTime + ",";
                sw.WriteLine(addFlight);
            }
            else                                                     // if there is a special request code, the format includes the code at the end 
            {
                string addFlight = flightNum + "," + flightOrigin + "," + flightDestination + "," + flightTime + "," + code;
                sw.WriteLine(addFlight);
            }
        }

        //Adding a new flight again
        while (true)                                                            // data validation for invalid input
        {
            Console.Write("Would you like to add another flight? (Y/N) ");
            string anotherFlight = Console.ReadLine().ToUpper();
            if (anotherFlight == "N")
            {
                break;                                 // break out of the method
            }
            else if (anotherFlight == "Y")
            {
                NewFlight();                                    // rerun the method again
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");               // allows user to input again if entered wrongly 
            }
        }
    }
    catch (Exception ex)
    { 
        Console.WriteLine($"Unexpected error occurred. {ex.Message}");
    }
}

// Basic Feature 7 : Display full flight details from an airline
void DisplayFullFlightDetails()
{
    try
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine($"{"Airline Code",-15}{"Airline Name"}");

        // iterate through all airlines in the dict
        foreach (Airline airline in airlineDict.Values)
        {
            Console.WriteLine($"{airline.Code,-15}{airline.Name,-18}");
        }

        // prompt to enter the airline code
        string airlineCode;
        while (true)  // data input validation loop                                                           
        {
            Console.Write("Enter Airline Code: ");
            airlineCode = Console.ReadLine()?.ToUpper().Trim(); // convert input to uppercase and remove whitespace
            // check if airline code exists in dict
            // [ensure airline code is not null or whitespace or empty & that it exists in the dict]
            if (string.IsNullOrWhiteSpace(airlineCode) || !airlineDict.ContainsKey(airlineCode))
            {
                Console.WriteLine("Invalid option, enter the airline codes given.");
            }
            else
            {
                break; // loop is exited if code is valid
            }
        }

        // get the airline obj based on the code inputted
        Airline selectedAirline = airlineDict[airlineCode];

        // iterate through all airlines in the dict
        foreach (Airline airline in airlineDict.Values)
        {
            if (airline.Code == airlineCode) // check if airline code matches input
            {
                selectedAirline = airline; // assign the matching airline to the variable
                break; // loop is exited once matched
            }
        }

        // if no matching airline is found, error message is displayed and exit
        if (selectedAirline == null)
        {
            Console.WriteLine("Invalid Airline Code. Please try again.");
            return;
        }

        Console.WriteLine("=============================================");
        Console.WriteLine($"List of Flights for {selectedAirline.Name}");
        Console.WriteLine("=============================================");
        Console.WriteLine($"{"Flight Number",-18}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}{"Expected Departure/Arrival Time"}");

        // list to store flight numbers for validation
        List<string> selecteedAirlineList = new List<string>();
        // iterate through flights to show of the chosen airline
        foreach (Flight flight in selectedAirline.Flights.Values)
        {
            Console.WriteLine($"{flight.FlightNumber,-18}{selectedAirline.Name,-23}{flight.Origin,-23}{flight.Destination,-23}{flight.ExpectedTime}");
            selecteedAirlineList.Add(flight.FlightNumber); // add flight number to list for future validation
        }

        // prompt to select a flight number
        string flightNumber;
        while (true)  // data input validation loop
        {
            Console.Write("Enter the Flight Number: ");
            flightNumber = Console.ReadLine()?.ToUpper().Trim(); // convert input to uppercase and remove whitespace
            // check if flight number exists in dict
            // [ensure flight number is not null or whitespace or empty & that it exists in the list of valid flight numbers]
            if (string.IsNullOrWhiteSpace(flightNumber) || !selecteedAirlineList.Contains(flightNumber))
            {
                Console.WriteLine("Invalid Flight Number. Please try again.");
            }
            else
            {
                break; // loop is exited for valid flight number
            }
        }

        // retrieve the selected flight based on flight number
        if (selectedAirline.Flights.TryGetValue(flightNumber, out Flight selectedFlight))
        {
            Console.WriteLine("Flight Details:");
            Console.WriteLine($"Flight Number: {selectedFlight.FlightNumber}");
            Console.WriteLine($"Airline Name: {selectedAirline.Name}");
            Console.WriteLine($"Origin: {selectedFlight.Origin}");
            Console.WriteLine($"Destination: {selectedFlight.Destination}");
            Console.WriteLine($"Expected Departure/Arrival Time: {selectedFlight.ExpectedTime}");
            // check for the special requets code associated
            if (selectedFlight is CFFTFlight)
                Console.WriteLine("Special Request Code: CFFT");
            else if (selectedFlight is DDJBFlight)
                Console.WriteLine("Special Request Code: DDJB");
            else if (selectedFlight is LWTTFlight)
                Console.WriteLine("Special Request Code: LWTT");
            else if (selectedFlight is NORMFlight)
                Console.WriteLine("Special Request Code: None");
            bool hasGate = false;
            foreach (BoardingGate gate in boardingGateDict.Values)
            {
                if (gate.Flight == selectedFlight)
                {
                    Console.WriteLine($"Boarding Gate: {gate.GateName}");
                    hasGate = true;
                    break;
                }
            }
            if (!hasGate)
                Console.WriteLine("Boarding Gate: Unassigned");
        }
        else
        { // error message for invalid flight no
            Console.WriteLine("Invalid Flight Number. Please try again.");
        }
    }
    catch (Exception ex)
    { // handle any other exceptions that may occur
        Console.WriteLine($"Error : {ex.Message}");
    }
}


// Basic Feature 8 : Modify flight details (opt 6)
void DisplayAirlinesAndPromptSelection()
{
    try
    {
        Console.WriteLine("\n=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine($"{"Airline Code",-15}{"Airline Name",-25}");

        // iterate through all airlines in dict for display
        foreach (Airline airline in airlineDict.Values)
        {
            Console.WriteLine($"{airline.Code,-15}{airline.Name,-25}");
        }

        string selectedCode;
        while (true)  // data input validation loop
        {
            // prompt user to select an airline by its code
            Console.Write("Enter Airline Code: ");
            selectedCode = Console.ReadLine()?.ToUpper();
            // check if selected code exists in dict
            // [ensure selected code is not null or whitespace or empty & that it exists in the dict]
            if (string.IsNullOrWhiteSpace(selectedCode) || !airlineDict.ContainsKey(selectedCode))
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
            else
            {
                // get the airline obj based on the selected code
                Airline selectedAirline = airlineDict[selectedCode];
                DisplayFlightsForAirline(selectedAirline);
                break;
            }
        }
    }
    catch (Exception ex)
    {  // handle any other exceptions that may occur
        Console.WriteLine($"Error: {ex.Message}");
    }
}

void DisplayFlightsForAirline(Airline selectedAirline)
{
    try
    {
        Console.WriteLine("=============================================");
        Console.WriteLine($"List of Flights for {selectedAirline.Name}");
        Console.WriteLine("=============================================");
        Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-25}{"Origin",-25}{"Destination",-25}{"Expected Departure/Arrival Time"}");

        // iterate and display each flight
        foreach (Flight flight in selectedAirline.Flights.Values)
        {
            Console.WriteLine($"{flight.FlightNumber,-20}{selectedAirline.Name,-20}{flight.Origin,-25}{flight.Destination,-25}{flight.ExpectedTime:dd/MM/yyyy hh:mm tt}");
        }

        // prompt user to either modify or delete
        while (true) // data input validation loop
        {
            Console.Write("Choose an existing Flight to modify or delete:");

            string flightNumber = Console.ReadLine()?.ToUpper();

            // check if flight exists
            if (selectedAirline.Flights.ContainsKey(flightNumber))
            {
                Flight selectedFlight = selectedAirline.Flights[flightNumber];
                while (true) // data input validation loop
                {
                    Console.WriteLine("1. Modify Flight");
                    Console.WriteLine("2. Delete Flight");
                    Console.Write("Choose an option: ");
                    string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        ModifyFlight(selectedAirline, selectedFlight);
                        break;
                    }
                    else if (choice == "2")
                    {
                        DeleteFlight(selectedAirline, selectedFlight);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                }
                break;
            }
            else
            {
                Console.WriteLine("Flight not found.");
            }
        }
    }
    catch (Exception ex)
    {  // handle any other exceptions that may occur
        Console.WriteLine($"Error: {ex.Message}");
    }
}

// modify the flight details for choice 1
void ModifyFlight(Airline selectedAirline, Flight selectedFlight)
{
    try
    {
        while (true)
        {
            Console.WriteLine("1. Modify Basic Information");
            Console.WriteLine("2. Modify Status");
            Console.WriteLine("3. Modify Special Request Code");
            Console.WriteLine("4. Modify Boarding Gate");
            Console.Write("Choose an option: ");
            string modifyChoice = Console.ReadLine();

            if (modifyChoice == "1")
            {
                ModifyBasicInformation(selectedFlight);
                break;
            }
            else if (modifyChoice == "2")
            {
                ModifyStatus(selectedFlight);
                break;
            }
            else if (modifyChoice == "3")
            {
                ModifySpecialRequestCode(selectedFlight);
                break;
            }
            else if (modifyChoice == "4")
            {
                ModifyBoardingGate(selectedFlight);
                break;
            }
            else
            {
                Console.WriteLine("Invalid Choice!");
            }
        }

        //Console.WriteLine("Flight updated!");
        DisplayFlightDetails(selectedFlight, selectedAirline);
    }
    catch (Exception ex)
    { // handle any other exceptions that may occur
        Console.WriteLine($"Error: {ex.Message}");
    }
}

// for modification choice 1 
void ModifyBasicInformation(Flight selectedFlight)
{
    try
    {
        while (true) // data input validation loop 
        {
            Console.Write("Enter new Origin: ");
            string origin = Console.ReadLine();
            Console.Write("Enter new Destination: ");
            string destination = Console.ReadLine();
            Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy hh:mm): ");
            string timeInput = Console.ReadLine();

            // ensure time formate is correct & that origin/destination are not empty
            if (DateTime.TryParse(timeInput, out DateTime newTime) && !string.IsNullOrWhiteSpace(origin) && !string.IsNullOrWhiteSpace(destination))
            {
                selectedFlight.Origin = origin;
                selectedFlight.Destination = destination;
                selectedFlight.ExpectedTime = newTime;
                break; // loop is exited once valid 
            }
            else
            {
                Console.WriteLine("Flight not updated!"); // invalid input
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

// for modification choice 2
void ModifyStatus(Flight selectedFlight)
{
    try
    {
        while (true)  // data input validation loop
        {
            Console.Write("Enter new Status: ");
            string status = Console.ReadLine().ToUpper();
            // checks if input is not empty or whitespace and if it matches valid status 
            if (!(string.IsNullOrWhiteSpace(status)) && (status == "ON TIME" || status == "DELAYED" || status == "BOARDING"))
            {
                // valid input = updates status
                selectedFlight.Status = status;
                Console.WriteLine("Status updated successfully!");
                break; // loop is exited
            }
            else
            {
                Console.WriteLine("Invalid input. Type either 'delayed', 'on time' or 'boarding'.");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

// for modification choice 3
void ModifySpecialRequestCode(Flight selectedFlight)
{
    try
    {
        //Flight newFlight = selectedFlight; // new obj based on input
        while (true) // data input validation loop
        {
            Console.Write("Enter new Special Request Code: ");
            string requestCode = Console.ReadLine().ToUpper();

            // checks if input is not empty or whitespace and if it matches valid special request code 
            if (!(string.IsNullOrWhiteSpace(requestCode)) && (requestCode == "CFFT" || requestCode == "DDJB" || requestCode == "LWTT" || requestCode == "NONE"))
            { // valid input = updates the special request code
                selectedFlight.SpecialRequestCode = requestCode; // assign the input to this obj
                Console.WriteLine("Special Request Code updated successfully!");
                break; // loop is exited
            }
            else
            { // invalid input 
                Console.WriteLine("Special Request Code must be either CFFT, DDJB, LWTT OR None. Try again.");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

// for modification choice 4
void ModifyBoardingGate(Flight selectedFlight)
{
    try
    {
        while (true) // data input validation loop
        {
            Console.Write("Enter new Boarding Gate: ");
            string gate = Console.ReadLine().ToUpper();

            // checks if input is not empty or whitespace and if it matches valid gate 
            if (!(string.IsNullOrWhiteSpace(gate)) && boardingGateDict.ContainsKey(gate))
            { // valid input = updates gate
                BoardingGate newGate = boardingGateDict[gate];
                selectedFlight.boardingGate = newGate;
                newGate.Flight = selectedFlight;
                Console.WriteLine("Boarding Gate updated successfully!");
                break; // loop is exited
            }
            else
            { // invalid input 
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}


// delete the flight for choice 2
void DeleteFlight(Airline selectedAirline, Flight selectedFlight)
{
    try
    {
        while (true)  // data input validation loop
        {
            Console.WriteLine($"Are you sure you want to delete Flight {selectedFlight.FlightNumber}? (Y/N): ");
            string confirmation = Console.ReadLine().ToUpper();

            if (confirmation == "Y")
            { // remove flight using flight number as key 
                selectedAirline.Flights.Remove(selectedFlight.FlightNumber);
                Console.WriteLine("Flight deleted successfully.");
                break;
            }
            else if (confirmation == "N")
            {
                Console.WriteLine("Flight deletion cancelled.");
                break;
            }
            else
            { // invalid input
                Console.WriteLine("Choose Y or N.");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

void DisplayFlightDetails(Flight flight, Airline selectedAirline)
{
    try
    {
        Console.WriteLine($"\nFlight Number: {flight.FlightNumber}");
        Console.WriteLine($"Airline Name: {selectedAirline.Name}");
        Console.WriteLine($"Origin: {flight.Origin}");
        Console.WriteLine($"Destination: {flight.Destination}");
        Console.WriteLine($"Expected Departure/Arrival Time: {flight.ExpectedTime:dd/MM/yyyy hh:mm tt}");
        Console.WriteLine($"Status: {flight.Status}");

        if (flight.SpecialRequestCode == "CFFT")
            Console.WriteLine("Special Request Code: CFFT");
        else if (flight.SpecialRequestCode == "DDJB")
            Console.WriteLine("Special Request Code: DDJB");
        else if (flight.SpecialRequestCode == "LWTT")
            Console.WriteLine("Special Request Code: LWTT");
        else if (flight.SpecialRequestCode == "NONE")
            Console.WriteLine("Special Request Code: None");

        bool hasGate = false; // check if flight has boarding gate assigned
        // iterate through all bgs in dict
        foreach (BoardingGate gate in boardingGateDict.Values)
        {
            if (gate.Flight == flight) // compare associated and selected flight
            {
                Console.WriteLine($"Boarding Gate: {gate.GateName}");
                hasGate = true; // to indicate gate has been assigned
                break;
            }
        }

        // if no gate is found
        if (!hasGate)
            Console.WriteLine("Boarding Gate: Unassigned");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}


// Basic Feature 9 : Display scheduled flights in chronological order, with boarding gates assignments where applicable
void SortedFlights()
{
    try
    {
        List<Flight> flightlist = new List<Flight>(flightdict.Values);                     // making a list to Sort() it

        flightlist.Sort();                                                         // sorting the flight list by the scheduled time

        Console.WriteLine("=============================================");
        Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("{0, -18}{1, -23}{2, -23}{3, -23}{4, -35}{5, -15}{6, -15}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time", "Status", "Boarding Gate");

        foreach (Flight flight in flightlist)
        {
            //string status;
            string gate = "Unassigned";
            string flightname = "Unknown airline";

            foreach (Airline airline in airlineDict.Values)
            {
                if (airline.Flights.ContainsKey(flight.FlightNumber))                    // checking if the airline exists
                {                                                                    // if it does, the flight name will change
                    flightname = airline.Name;                                   // to retrieve the flight name and display it later
                    break;
                }
            }

            foreach (BoardingGate gates in boardingGateDict.Values)
            {

                if (gates.Flight == flight)                                     // if the gate has been assigned 
                {
                    gate = gates.GateName;                                      // retrieve the gate name for displaying later 
                    break;
                }
            }
            Console.WriteLine("{0, -18}{1, -23}{2, -23}{3, -23}{4, -35}{5, -15}{6, -15}", (flight.FlightNumber), (flightname), (flight.Origin), (flight.Destination), (flight.ExpectedTime), (flight.Status), (gate)); // display the details if flight has status 
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error occurred. {ex.Message}");
    }
}


// Advanced feature (a) : process all unassigned flights to boarding gates in bulk
void UnassignedFlights()
{
    try
    {
        if (flightdict.Count == 0)
        {
            Console.WriteLine("There are no flights to process.");                                 // if there are no flights in the dictionary
            return;
        }
        if (boardingGateDict.Count == 0)
        {
            Console.WriteLine("There are no boarding gates to process.");                          // if there are no boarding gates in the dictionary
            return;
        }

        Queue<Flight> unassignedFlights = new Queue<Flight>();  // queue for unassigned flights
        List<BoardingGate> unassignedGates = new List<BoardingGate>();  // list for unassigned gates

        // identify unassigned flights
        foreach (Flight flight in flightdict.Values)
        {
            bool hasGate = false;
            foreach (BoardingGate gate in boardingGateDict.Values)
            {
                if (gate.Flight == flight)
                {
                    hasGate = true;
                    break;
                }
            }
            if (!hasGate)
            {
                unassignedFlights.Enqueue(flight);
            }
        }
        Console.WriteLine($"Number of unassigned flights: {unassignedFlights.Count}");

        // identify unassigned gates
        foreach (BoardingGate gate in boardingGateDict.Values)
        {
            if (gate.Flight == null)
            {
                unassignedGates.Add(gate);
            }
        }
        Console.WriteLine($"Number of unassigned gates: {unassignedGates.Count}");

        double assignedFlightCount = 0;
        double assignedGateCount = 0;

        while (unassignedFlights.Count > 0 && unassignedGates.Count > 0)
        {
            Flight assignedFlight = unassignedFlights.Dequeue();                       // select the next flight
            BoardingGate assignedGate = null;                                         // set the gate to null first 

            foreach (BoardingGate gate in unassignedGates)
            {
                if (assignedFlight is CFFTFlight && gate.SupportsCFFT)                      // if the gate supports CFFT and the flight's code is CFFT
                {
                    assignedGate = gate;                                                   // they would be assigned to each other
                }
                else if (assignedFlight is DDJBFlight && gate.SupportsDDJB)                 // if the gate supports DDJB and the flight's code is DDJB
                {
                    assignedGate = gate;                                                   // they would be assigned to each other
                }
                else if (assignedFlight is LWTTFlight && gate.SupportsLWTT)                // if the gate supports LWTT and the flight's code is LWTT
                {
                    assignedGate = gate;                                                   // they would be assigned to each other
                }
                else if (assignedFlight is NORMFlight && !gate.SupportsCFFT && !gate.SupportsDDJB && !gate.SupportsLWTT)     // if the gate does not support any code and the flight does not have any code
                {
                    assignedGate = gate;                                                   // they would be assigned to each other
                }
            }
            if (assignedGate != null)                                                      // if the gate was assigned a flight
            {
                assignedGate.Flight = assignedFlight;                                     // assigning the respective ly flight ot the gate 
                unassignedGates.Remove(assignedGate);                                     // remove this gate from the list
                                                                                          // increasing the number of process and assigned gates and flights by 1
                assignedFlightCount++;
                assignedGateCount++;
            }
        }
        Console.WriteLine($"Number of flights processed and assigned: {assignedFlightCount}");
        Console.WriteLine($"Number of gates processed and assigned: {assignedGateCount}");
        double percentageFlights = (assignedFlightCount / flightdict.Count) * 100;
        double percentageGates = (assignedGateCount / boardingGateDict.Count) * 100;
        Console.WriteLine($"The percentage of flights processed and assigned: {percentageFlights:0.00}%");
        Console.WriteLine($"The percentage of gates processed and assigned: {percentageGates:0.00}%");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error occurred. {ex.Message}");
    }
}

// Advanced Feature (b) : Display the total fee per airline for the day
// Method to calculate and display total fee per airline for the day
void CalculateTotalFeePerAirline()
{
    try
    {
        Console.WriteLine("\n=============================================");
        Console.WriteLine("Total Fees Per Airline for the Day");
        Console.WriteLine("=============================================");

        decimal totalFees = 0; // track fees for all airlines 
        decimal totalDiscounts = 0; // track total discounts for all airlines

        // check if airline data is available
        if (airlineDict == null || airlineDict.Count == 0)
        {
            Console.WriteLine("Error: No airline data available.");
            return;
        }

        // create a list to store unassigned flights & test if each flight is assigned to a gate
        List<Flight> unassignedFlights = new List<Flight>();
        foreach (Flight flight in flightdict.Values)
        {
            bool hasGate = false; // checking whether the flight has a gate assigned to it or not 
            // iterate through boarding gates to check if the flight has a gate assigned
            foreach (BoardingGate gate in boardingGateDict.Values)
            {
                if (gate.Flight == flight) // if the gate has a flight assigned to it 
                {
                    hasGate = true;
                    break; // loop is exited when there is flight assigned
                }
            }
            if (!hasGate) // if it does not have a gate
            {
                unassignedFlights.Add(flight); // add it to the unassigned flights Queue
            }
        }

        //if list is not empty, not all flights have been assigned boarding gates
        if (unassignedFlights.Count != 0)
        {
            Console.WriteLine("Error: Not all flights have assigned boarding gates.");
            Console.WriteLine("Please ensure all flights have boarding gates assigned.");
            return;
        }

        // iterate over airlines in the airline dict 
        foreach (var airline in airlineDict.Values)
        {
            decimal airlineTotal = 0; // variable to track subtotal fees for each airline
            decimal airlineDiscounts = 0; // variable to track discounts for each airline

            Console.WriteLine("\n=============================================");
            Console.WriteLine($"{airline.Name}'s Total Amount");
            Console.WriteLine("=============================================");

            try
            {
                // get all flights for the airline
                foreach (var flight in airline.Flights.Values)
                {
                    decimal flightFees = 0; // variable to track fees for each flight

                    // check if the origin or destination is sg
                    if (flight.Origin == "SIN")
                    {
                        flightFees += 800; // departing flight fee
                    }
                    if (flight.Destination == "SIN")
                    {
                        flightFees += 500; // arriving flight fee
                    }

                    // check if flight has a special request code and apply its respective fees
                    if (!string.IsNullOrEmpty(flight.SpecialRequestCode))
                    {
                        if (flight.SpecialRequestCode == "DDJB")
                        {
                            flightFees += 300;
                        }
                        else if (flight.SpecialRequestCode == "CFFT")
                        {
                            flightFees += 150;
                        }
                        else if (flight.SpecialRequestCode == "LWTT")
                        {
                            flightFees += 500;
                        }
                    }

                    // boarding gate base fee
                    flightFees += 300;

                    // discounts for fl
                    decimal flightDiscount = ComputeDiscountsForFlights(flight);

                    // add flight fees to the airline's subtotal
                    airlineTotal += flightFees;

                    Console.WriteLine("\n=============================================");
                    Console.WriteLine($"Flight : {flight.FlightNumber}");
                    Console.WriteLine($"Airline: {airline.Name}");
                    Console.WriteLine($"Origin : {flight.Origin}");
                    Console.WriteLine($"Destination : {flight.Destination}");
                    Console.WriteLine($"Expected Time : {flight.ExpectedTime}");

                    if (flight is CFFTFlight)
                    {
                        Console.WriteLine("Special Request Code: CFFT");
                    }
                    else if (flight is DDJBFlight)
                    {
                        Console.WriteLine("Special Request Code: DDJB");
                    }
                    else if (flight is LWTTFlight)
                    {
                        Console.WriteLine("Special Request Code: LWTT");
                    }
                    else
                    {
                        Console.WriteLine("Special Request Code: None");
                    }

                    Console.WriteLine($"Initial Amount : ${flightFees}");
                    Console.WriteLine($"Total Discount : -${flightDiscount}");
                    Console.WriteLine($"Final Amount : ${flightFees - flightDiscount}");
                    Console.WriteLine("=============================================");

                    // add fees to airline total 
                    airlineTotal += flightFees;
                }


                // airline specific discounts based on promotional conditions
                airlineDiscounts = ComputeDiscountsForAirline(airline, airlineTotal);

                // add the airlines's subtotal and discounts to the overall totals
                totalFees += airlineTotal;
                totalDiscounts += airlineDiscounts;

                Console.WriteLine($"{airline.Name}: ");
                Console.WriteLine($"Subtotal: ${airlineTotal}");
                Console.WriteLine($"Discounts: -${airlineDiscounts}");
                Console.WriteLine($"Final Total: ${airlineTotal - airlineDiscounts}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing fees for airline {airline.Name}: {ex.Message}");
            }

        }

        // display overall total fees and discounts
        Console.WriteLine("=============================================");
        Console.WriteLine($"Total Airline Fees : ${totalFees}");
        Console.WriteLine($"Total Airline Discounts : -${totalDiscounts}");
        Console.WriteLine($"Final Total Amount : ${totalFees - totalDiscounts}");
        Console.WriteLine($"Discount Percentage : {((totalFees > 0) ? (totalDiscounts / totalFees) * 100 : 0):F2}%");
        // checks of total fees is greater than 0, calculates the discount % and formats in 2dp
        // if total fees is 0, discount% is 0
        Console.WriteLine("=============================================");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }
}

// helper method to compute discounts for an airline
decimal ComputeDiscountsForAirline(Airline airline, decimal airlineTotal)
{
    decimal totalDiscounts = 0; // variable to track total discount for airline
    int flightCount = airline.Flights.Count; // get the number of flights for airline

    // for every 3 flights, a $350 discount
    totalDiscounts += (flightCount / 3) * 350;

    if (flightCount > 5)
    {
        totalDiscounts += airlineTotal * 0.03m;
    }

    return totalDiscounts;
}


// helper method to compute discounts for flight
decimal ComputeDiscountsForFlights(Flight flight)
{
    decimal discount = 0; // variable to track the discount for flight

    // check if the flight qualifies for a time-based discount
    if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour > 21)
    {
        discount += 110;
    }

    // check if the flight's origin qualifies for a discount
    if (flight.Origin == "DXB" || flight.Origin == "BKK" || flight.Origin == "NRT")
    {
        discount += 25;
    }

    // apply special request code discounts
    if (!string.IsNullOrEmpty(flight.SpecialRequestCode))
    {
        if (flight.SpecialRequestCode == "DDJB")
        {
            discount += 300;
        }
        else if (flight.SpecialRequestCode == "CFFT")
        {
            discount += 150;
        }
        else if (flight.SpecialRequestCode == "LWTT")
        {
            discount += 500;
        }
    }
    else
    {
        // add $50 discount if there is no special request code
        discount += 50;
    }

    return discount;
}



// Main Program : display menu and runs the entire program
void DisplayMenu()
{
    Console.WriteLine("\n=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");                                                   // feature 3
    Console.WriteLine("2. List Boarding Gates");                                               // feature 4
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");                               // feature 5
    Console.WriteLine("4. Create Flight");                                                   // feature 6
    Console.WriteLine("5. Display Airline Flights");                                        // feature 7
    Console.WriteLine("6. Modify Flight Details");                                         // feature 8
    Console.WriteLine("7. Display Flight Schedule");                                      // feature 9
    Console.WriteLine("8. Assign Flights to an Available Boarding Gate");                // advanced feature (a)
    Console.WriteLine("9. Calculate the Total Amount of Airline Fees to be Charged");   // advanced feature (b)
    Console.WriteLine("0. Exit");
}
void Main(Dictionary<string, Flight> flightdict, Dictionary<string, BoardingGate> boardingGateList, Dictionary<string, Airline> airlineList)
{
    //Loading the files
    Load_Airlines();
    Load_BoardingGates();
    Load_Flight();

    string option = "";
    while (option != "0")
    {
        DisplayMenu();
        Console.Write("Please select your option: ");
        option = Console.ReadLine();
        if (option == "1")
        {
            Display_Flights();
        }
        else if (option == "2")
        {
            DisplayBoardingGates();
        }
        else if (option == "3")
        {
            BoardingGateToFlight();
        }
        else if (option == "4")
        {
            NewFlight();
        }
        else if (option == "5")
        {
            DisplayFullFlightDetails();
        }
        else if (option == "6")
        {
            DisplayAirlinesAndPromptSelection();
        }
        else if (option == "7")
        {
            SortedFlights();
        }
        else if (option == "8")
        {
            UnassignedFlights();
        }
        else if (option == "9")
        {
            CalculateTotalFeePerAirline();
        }
        else if (option == "0")
        {
            Console.WriteLine("Goodbye!");
            break;
        }
        else
        { Console.WriteLine("Invalid option! Choose an option from 0 - 9"); }
    }
}
Main(flightdict, boardingGateDict, airlineDict);
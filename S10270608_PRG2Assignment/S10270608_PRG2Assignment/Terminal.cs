//==========================================================
// Student Number : S10270608
// Student Name	  : Antozesslyn Alister
// Partner Name	  : Joely Lim Kei Cin
//==========================================================



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10270608_PRG2Assignment
{
    public class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();

        public Terminal() { }
        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
            Airlines = new Dictionary<string, Airline>(); 
            Flights = new Dictionary<string, Flight>();
            BoardingGates = new Dictionary<string, BoardingGate>();
            GateFees = new Dictionary<string, double>();
        }


        public bool AddAirline(Airline airline)
        { // add new airline to terminal
            // only add if airline is not in dictionary
            if (!Airlines.ContainsKey(airline.Code))
            {
                Airlines[airline.Code] = airline;
                return true;
            }
            return false;
        }

        public bool AddBoardingGate(BoardingGate gate)
        { // add boarding gate to terminal
            // only add if gate is not in dictionary
            if (!BoardingGates.ContainsKey(gate.GateName))
            {
                BoardingGates[gate.GateName] = gate;
                return true;
            }
            return false;
        }

        public Airline GetAirlineFromFlight(Flight flight)
        { // get airline associated with the flight
            foreach (var airline in Airlines.Values)
            { // iterate through the airlines to find the one with the flight
                if (airline.Flights.ContainsKey(flight.FlightNumber))
                    return airline;
            }
            return null;
        }

        public void PrintAirlineFees()
        { // prints total fees for each airline
            foreach (var airline in Airlines.Values)
            {
                Console.WriteLine($"Airline: {airline.Name}, Fees: {airline.CalculateFees()}");
            }
        }

        public override string ToString()
        {
            return $"Terminal: {TerminalName}, Airlines: {Airlines.Count}, Flights: {Flights.Count}, Boarding Gates: {BoardingGates.Count}";
        }

        //public void LoadAirlinesFromCSV(string fileName)
        //{
        //    string[] lines = File.ReadAllLines("airlines.csv");
        //    foreach (var line in lines)
        //    {
        //        var columns = line.Split(',');
        //        if (columns.Length == 2)
        //        {
        //            var airlineCode = columns[0].Trim();
        //            var airlineName = columns[1].Trim();
        //            var airline = new Airline(airlineCode, airlineName);
        //            AddAirline(airline); // add  airline to airlines dictionary
        //        }
        //    }
        //    Console.WriteLine("Airlines loaded successfully.");
        //}

        //public void LoadBoardingGatesFromCSV(string fileName)
        //{
        //    string[] lines = File.ReadAllLines("boardinggates.csv");

        //    foreach (var line in lines)
        //    {
        //        var columns = line.Split(',');
        //        if (columns.Length == 4)
        //        {
        //            var gateName = columns[0].Trim();
        //            var supportsCFFT = bool.Parse(columns[1].Trim());
        //            var supportsDDJB = bool.Parse(columns[2].Trim());
        //            var supportsLWTT = bool.Parse(columns[3].Trim());

        //            var boardingGate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT, Flight);
        //                //Flight = Flights.ContainsKey(FlightNumber) ? Flights[FlightNumber] : null // assign flight if exists
        //           AddBoardingGate(boardingGate); // add boarding gate to BoardingGates dictionary
        //        }
        //    }
        //    Console.WriteLine("Boarding gates loaded successfully.");
        //}
    }
}

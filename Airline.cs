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

namespace PRG2_ASSIGNMENT
{
    public class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        // to be indentified by flight no
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

        public bool AddFlight(Flight flight)
        { // add new flight to airline
            // only add if flight is not in dict
            if (!Flights.ContainsKey(flight.FlightNumber))
            { 
                Flights[flight.FlightNumber] = flight;
                return true;
            }
            return false;
        }

        public bool RemoveFlight(Flight flight)
        { // remove flight from terminal
            // to be removed by flight no
            return Flights.Remove(flight.FlightNumber);
        }

        public double CalculateFees()
        { // total fees for all flights of an airline
            double totalFees = 0;
            foreach (var flight in Flights.Values)
            {
                totalFees += flight.CalculateFees();
            }
            return totalFees;
        }

        public override string ToString()
        {
            return $"Airline: {Name}, Code: {Code}, Flights: {Flights.Count}";
        }
    }

}

// committed on 26.01.25 2.10 pm
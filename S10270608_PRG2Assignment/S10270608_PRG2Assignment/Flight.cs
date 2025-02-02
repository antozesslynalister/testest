//==========================================================
// Student Number : S10267773
// Student Name	  : Joely Lim Kei Cin 
// Partner Name	  : Antozesslyn Alister
//==========================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10270608_PRG2Assignment
{
    public abstract class Flight : IComparable<Flight>
    {
        private string flightNumber;
        private string origin;
        private string destination;
        private DateTime expectedTime;
        private string status;

        public string FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }
        public string Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        public DateTime ExpectedTime
        {
            get { return expectedTime; }
            set { expectedTime = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        //for features 7 and 8
        public string SpecialRequestCode { get; set; }
        public BoardingGate boardingGate { get; set; } // may ne unnesscary

        public Flight() { }
        public Flight(string fn, string o, string d, DateTime et)
        {
            FlightNumber = fn;
            Origin = o;
            Destination = d;
            ExpectedTime = et;
            Status = "Scheduled";

            //for features 7 and 8
            SpecialRequestCode = "";
            boardingGate = null;
        }
        public abstract double CalculateFees();
        public override string ToString()
        {
            return $"{FlightNumber}\t{Origin}\t{Destination}\t{ExpectedTime}\t{Status}\t";
        }
        public int CompareTo(Flight other)
        {
            return ExpectedTime.CompareTo(other.ExpectedTime);
        }
    }
}

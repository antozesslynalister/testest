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
    abstract class Flight
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
        public Flight() { }
        public Flight(string fn, string o, string d, DateTime et, string s)
        {
            FlightNumber = fn;
            Origin = o;
            Destination = d;
            ExpectedTime = et;
            Status = s;
        }
        public abstract double CalculateFee();
        public override string ToString()
        {
            return $"{FlightNumber}\t{Origin}\t{Destination}\t{ExpectedTime}\t{Status}\t";
        }
    }
}

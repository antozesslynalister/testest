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
    public class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public double CalculateFees()
        {
            // if a flight is assigned, calculate fees. if not, 0
            return Flight != null ? Flight.CalculateFee() : 0;
        }

        public override string ToString()
        {
            return $"Gate: {GateName}, Supports CFFT: {SupportsCFFT}, Supports DDJB: {SupportsDDJB}, Supports LWTT: {SupportsLWTT}, Flight: {(Flight != null ? Flight.FlightNumber : "None")}";
        }
    }
}

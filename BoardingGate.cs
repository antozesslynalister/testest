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

// committed on 26.01.25 2.10 pm
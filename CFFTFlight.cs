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

namespace PRG2_ASSIGNMENT
{
    class CFFTFlight : Flight
    {
        private double requestFee;
        public double RequestFee
            { get { return requestFee; } 
            set { requestFee = value; } }
        public CFFTFlight() { }
        public CFFTFlight(string fn, string o, string d, DateTime et, string s, double rf) : base(fn,o,d,et,s) 
        { 
            RequestFee = rf;
        }
        public override double CalculateFee()
        {
           RequestFee = 150;
           return RequestFee;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}

// committed on 26.01.25 7 pm with name header
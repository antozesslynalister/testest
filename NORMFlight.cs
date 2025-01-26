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
    class NORMFlight : Flight
    {
        public NORMFlight() { }
        public NORMFlight(string fn, string o, string d, DateTime et, string s) : base(fn, o, d, et, s) { }
        public override double CalculateFee()
        {
            double fee = 0;
            return fee;
        }
            public override string ToString()
        {
            return base.ToString();
        }
    }
}


// committed on 26.01.25 7 pm with name header
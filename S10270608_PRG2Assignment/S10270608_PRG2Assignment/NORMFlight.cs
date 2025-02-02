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
    class NORMFlight : Flight
    {
        public NORMFlight() { }

        public string SpecialRequestCode { get; set; }
        public NORMFlight(string fn, string o, string d, DateTime et) : base(fn, o, d, et) {
            SpecialRequestCode = "NONE";
        }
        public override double CalculateFees()
        {
            double Base = 300;
            double fee = 0;
            if (Destination == "Singapore (SIN)")
                { fee = 500; }
            else
                { fee = 800; }
            return Base + fee;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
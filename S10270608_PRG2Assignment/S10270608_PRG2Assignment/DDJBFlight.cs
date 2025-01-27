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
    class DDJBFlight : Flight
    {
        private double requestFee;
        public double RequestFee
        {
            get { return requestFee; }
            set { requestFee = value; }
        }
        public DDJBFlight() { }
        public DDJBFlight(string fn, string o, string d, DateTime et, string s, double rf) : base(fn, o, d, et, s)
        { RequestFee = rf; }
        public override double CalculateFee()
        {
            RequestFee = 300;
            return RequestFee;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}



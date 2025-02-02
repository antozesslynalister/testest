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
    class LWTTFlight : Flight
    {
        private double requestFee;
        public double RequestFee
        {
            get { return requestFee; }
            set { requestFee = value; }
        }

        public string SpecialRequestCode { get; set; }
        public LWTTFlight() { }
        public LWTTFlight(string fn, string o, string d, DateTime et) : base(fn, o, d, et)
        {
            RequestFee = 500;
            SpecialRequestCode = "LWTTF";
        }
        public override double CalculateFees()
        {
            double Base = 300;
            double fee = 0;
            if (Destination == "Singapore (SIN)")
            {
                fee= Base + 500 + RequestFee;
            }
            else if (Destination != "Singapore (SIN)")
            {
                fee = Base + 800 + RequestFee;
            }
            return fee;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}


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
    class CFFTFlight : Flight
    {
        private double requestFee;
        public double RequestFee
        {
            get { return requestFee; }
            set { requestFee = value; }
        }

        public string SpecialRequestCode { get; set; }
        public CFFTFlight() { }
        public CFFTFlight(string fn, string o, string d, DateTime et) : base(fn, o, d, et)
        {
            RequestFee = 150;
            SpecialRequestCode = "CFFT";
        }
        public override double CalculateFees()
        {
            double Base = 300;
            double fee = 0;
            if (Destination == "SINGAPORE (SIN)")
            {
                fee = 500 + Base + RequestFee;
            }
            else
            {
                fee = 800 + Base + RequestFee;
            }
            return fee;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASSIGNMENT
{
    class LWTTFlight : Flight
    {
        private double requestFee;
        public double RequestFee 
        { get { return requestFee; } 
          set { requestFee = value; } }
        public LWTTFlight() { }
        public LWTTFlight(string fn, string o, string d, DateTime et, string s, double rf) : base(fn,o,d,et,s)
        {
            RequestFee = rf;
        }
        public override double CalculateFee()
        {
            RequestFee = 500;
            return RequestFee;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}

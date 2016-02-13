using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magi_calc
{
    class Program
    {
        private static float _m = 100.0f;
        private static float _dspR; //Dispertion Rate.

        private static float _mP = 1.0f; //Magic Potency
        private static float _lvl = 1.0f; //Current Level
        private static float _chg; //Current Charge amount.

        static void Main(string[] args)
        {
            Console.WriteLine("Magi Calc:");
            Console.WriteLine("Magic calculation system for Project Home.\n");

            //Show Charge Rate
            var chargeRate = CalculateChargeRate();
            Console.WriteLine($"Magic Charge Rate: {chargeRate}");

            //Calculate and display charge time.
            var chargeTime = CalculateChargeTime(chargeRate);
            Console.WriteLine($"Charge Time: {chargeTime}");

            //Set charge to max.
            _chg = 5.0f;

            //Do Dispertion stuff.

            Console.WriteLine("Finished.");
            Console.ReadKey();

        }

        private static float CalculateChargeRate()
        {
            return _mP*_lvl/_m;
        }

        private static float CalculateChargeTime(float chargeRate)
        {
            return 5.0f/chargeRate;
        }

        private float CalculateDispertionRate()
        {
            return ((_chg*_mP)*_lvl)/_m;
        }

        private float ApplyDispertion(float dispertion)
        {
            return _chg - dispertion;
        }
    }
}

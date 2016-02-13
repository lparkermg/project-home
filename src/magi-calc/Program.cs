using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace magi_calc
{
    class Program
    {
        private static float _m = 10.0f;
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
            Console.WriteLine("Simulating and Saving Dispertion.");
            DispertionSim();
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

        private static float CalculateDispertionRate()
        {
            return ((_chg*_mP)*_lvl)/_m;
        }

        private static float ApplyDispertion(float dispertion)
        {
            return _chg - dispertion;
        }

        private static void DispertionSim()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "\\dispertion.txt";
            //Create/Open file then loop through untill the charge rate <= to 0.
            using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    //Set to 0.1 so we dont hit weird stuff at 0.0f
                    while (_chg >= 0.1f)
                    {
                        _dspR = CalculateDispertionRate();
                        _chg = ApplyDispertion(_dspR);
                        Console.WriteLine($"{_dspR}, {_chg}.");
                        sw.Write($"{_dspR}, {_chg}, ");
                    }
                    sw.Flush();
                }
            }
        }
    }
}

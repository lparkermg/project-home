﻿using System;
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
        //Constants through the entire sim.
        private const float IMPACT_DIVIDER = 4.0f;
        private const float MAGICAL_POTENCY = 1.0f;
        private const float MULTIPLIER = 10.0f;
        private const float LEVEL = 1.0f;
        private const float MAX_CHARGE = 5.0f;

        //Changable variables
        private static float _dspR; //Dispertion Rate.
        private static float _chg; //Current Charge amount.


        static void Main(string[] args)
        {
            //TODO: May need to change the lot to fit more with the actual objects and code for the game.
            Console.WriteLine("Magi Calc:");
            Console.WriteLine("Magic calculation system for Project Home.\n");

            //Show Charge Rate
            var chargeRate = CalculateChargeRate();
            Console.WriteLine($"Magic Charge Rate: {chargeRate}");

            //Calculate and display charge time.
            var chargeTime = CalculateChargeTime(chargeRate);
            Console.WriteLine($"Charge Time: {chargeTime}\n");

            //Set charge to max.
            _chg = MAX_CHARGE;
            
            //Calculate and display Impact Absorbtion and Resonence on incrementing absorbtion rates up to 5.
            for(var i = 0;i <= 5;i++)
                Console.WriteLine(GetImapctAndResonenceFromCharge(_chg,(float)i));

            //Do Dispertion stuff.
            Console.WriteLine("Simulating and Saving Dispertion.");
            DispertionSim();
            Console.WriteLine("Finished.");
            Console.ReadKey();

        }

        private static float CalculateChargeRate()
        {
            return MAGICAL_POTENCY*LEVEL/(MAX_CHARGE/2.0f);
        }

        private static float CalculateChargeTime(float chargeRate)
        {
            return MAX_CHARGE/chargeRate;
        }

        private static float CalculateDispertionRate()
        {
            return ((_chg*MAGICAL_POTENCY)*LEVEL)/MULTIPLIER;
        }

        private static float ApplyDispertion(float dispertion)
        {
            return _chg - dispertion;
        }

        private static string GetImapctAndResonenceFromCharge(float charge,float absorbtion)
        {
            var imp = charge - absorbtion;
            var rr = charge - imp;

            return $"Impact Absorbtion: {imp}\nMax Resonence Radius: {rr}";
        }

        private static void DispertionSim()
        {
            var file = $"{AppDomain.CurrentDomain.BaseDirectory}\\dispertion-{LEVEL.ToString("0")}.txt";
            //Create/Open file then loop through untill the charge rate <= to 0.05.
            using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    //Set to 0.05 so we dont hit weird stuff at 0.0f
                    sw.Write("{\"Dispertion Rate\" : \"Current Charge\"}");
                    while (_chg >= 0.05f)
                    {
                        _dspR = CalculateDispertionRate();
                        _chg = ApplyDispertion(_dspR);
                        sw.Write($"{{{_dspR} : {_chg}}},");
                    }
                    sw.Flush();
                }
            }
        }
    }
}

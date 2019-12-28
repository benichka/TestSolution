using DesignPatternAdapter.Adapters;
using System;

namespace DesignPatternAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Simple case.
            // Instanciate a legacy car and make it do some stuff.
            ILegacyCar legacyCar = new LegacyCar();
            legacyCar.ChangeGearNumber(1);
            legacyCar.SpeedUp();

            Console.WriteLine($"Legacy car info: gear {legacyCar.GearNumber}, speed is {legacyCar.Mph}");

            // Instanciate a new car based on the existing legacy car.
            // As the legacy car is a reference type (class), everything made to this
            // new car will also be made in the correct way in the legacy car.
            INewCar newCar = new LegacyCarToNewCarAdapter(legacyCar);

            // Do some stuff with the new car.
            newCar.ChangeGear(2);
            newCar.Accelerate();

            // Display the states of both car: as expected, the legacy and the new car contain the same
            // information, only adapted for the new car.
            Console.WriteLine($"New car info: gear {newCar.Gear}, speed is {newCar.Speed}");
            Console.WriteLine($"Legacy car info: gear {legacyCar.GearNumber}, speed is {legacyCar.Mph}");
            #endregion Simple case.
        }
    }
}

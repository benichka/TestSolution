using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternAdapter
{
    class NewCar : INewCar
    {
        private int _power;
        public int Power { get => _power; }

        private int _gear;
        public int Gear { get => _gear; }

        private int _speed;
        public int Speed { get => _speed; }

        public NewCar()
        {
            _speed = 0;
            _gear = 0;
            _power = 150;
        }

        // This is a really simple use case so we don't bother with all the checks
        // (negative gear or speed, etc.).
        public void Accelerate()
        {
            _speed += 5;
        }

        public void Break()
        {
            _speed -= 5;
        }

        public void ChangeGear(int gearNumber)
        {
            _gear = gearNumber;
        }
    }
}

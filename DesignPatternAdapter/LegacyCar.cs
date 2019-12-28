namespace DesignPatternAdapter
{
    class LegacyCar : ILegacyCar
    {
        private int _horsePower;
        public int HorsePower { get => _horsePower; }

        private int _gearNumber;
        public int GearNumber { get => _gearNumber; }

        private int _mph;
        public int Mph { get => _mph; }

        public LegacyCar()
        {
            _mph = 0;
            _gearNumber = 0;
            _horsePower = 150;
        }

        // This is a really simple use case so we don't bother with all the checks
        // (negative gear or speed, etc.).
        public void ChangeGearNumber(int gearNumber)
        {
            _gearNumber = gearNumber;
        }

        public void SpeedDown()
        {
            _mph -= 5;
        }

        public void SpeedUp()
        {
            _mph += 5;
        }
    }
}

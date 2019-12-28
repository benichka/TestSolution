namespace DesignPatternAdapter.Adapters
{
    /// <summary>
    /// Takes a legacy car and adapt it to be a new car; hence, implements the INewCar interface.
    /// </summary>
    class LegacyCarToNewCarAdapter : INewCar
    {
        /// <summary>The legacy car to adapt. It must me set in the constructor.</summary>
        private ILegacyCar _legacyCar;

        // All the fields for INewCar will in fact target the equivalent in the legacyCar.
        public int Power => _legacyCar.HorsePower;

        public int Gear => _legacyCar.GearNumber;

        public int Speed => _legacyCar.Mph;

        /// <summary>
        /// The constructor simply assign the legacy car.
        /// </summary>
        /// <param name="legacyCar">The legacy car that we want to adapt.</param>
        public LegacyCarToNewCarAdapter(ILegacyCar legacyCar)
        {
            _legacyCar = legacyCar;
        }

        // As for the fields, all methods simply target the corresponding method in the legacy car.
        public void Accelerate()
        {
            _legacyCar.SpeedUp();
        }

        public void Break()
        {
            _legacyCar.SpeedDown();
        }

        public void ChangeGear(int gearNumber)
        {
            _legacyCar.ChangeGearNumber(gearNumber);
        }
    }
}

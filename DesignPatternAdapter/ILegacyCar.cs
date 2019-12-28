namespace DesignPatternAdapter
{
    interface ILegacyCar
    {
        int HorsePower { get; }
        int GearNumber { get; }
        int Mph { get; }

        void ChangeGearNumber(int gearNumber);

        void SpeedUp();

        void SpeedDown();
    }
}

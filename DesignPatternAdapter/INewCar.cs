namespace DesignPatternAdapter
{
    interface INewCar
    {
        int Power { get; }
        int Gear { get; }
        int Speed { get; }

        void ChangeGear(int gearNumber);

        void Accelerate();

        void Break();
    }
}

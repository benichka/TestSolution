using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Interface
{
    public interface IBeverage
    {
        int GetServingTemperature(bool includesMilk);
        bool IsFairTrade { get; set; }
    }

    public class ClassWithInterface : IBeverage
    {
        private int servingTempWithoutMilk { get; set; }
        private int servingTempWithMilk { get; set; }

        // Pas possible de déclarer des modifiers : private par défaut
        // lorsqu'une interface est implémentée de manière explicite
        //public int IBeverage.GetServingTemperature(bool includesMilk)
        //{
        //    if (includesMilk)
        //    {
        //        return servingTempWithMilk;
        //    }
        //    else
        //    {
        //        return servingTempWithoutMilk;
        //    }
        //}
        bool IBeverage.IsFairTrade { get; set; }

        int IBeverage.GetServingTemperature(bool includesMilk)
        {
            if (includesMilk)
            {
                return servingTempWithMilk;
            }
            else
            {
                return servingTempWithoutMilk;
            }
        }
        // Other non-interface members.
    }
}

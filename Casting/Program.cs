using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casting
{
    class Program
    {
        static void Main(string[] args)
        {
            var money = new Money(42.42M);

            decimal moneyAsDecimal = money;

            int moneyAsInt = (int)money;
        }
    }

    public class Money
    {
        /// <summary>Amount to store</summary>
        public decimal Amount { get; set; }

        /// <summary>Constructor</summary>
        public Money(decimal amount)
        {
            Amount = amount;
        }

        /// <summary>
        /// Implicit conversion from this class to a decimal
        /// </summary>
        /// <param name="money">The Money object to convert to a decimal</param>
        public static implicit operator decimal(Money money)
        {
            return money.Amount;
        }

        /// <summary>
        /// Explicit conversion from this class to an int
        /// </summary>
        /// <param name="money">The money object to convert to an int</param>
        public static explicit operator int(Money money)
        {
            return (int)money.Amount;
        }
    }
}

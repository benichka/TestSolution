using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Interface
{
    interface TestInterface
    {
        // Possible de définir des propriétés car ce sont des méthodes
        int MyProperty { get; set; }

        // Pas possible de mettre un champ dans une interface
        // int x;

        // Pas possible de définir un access modifier : pas de sens
        // car pas de code. En fait toutes les propriétés sont par défaut
        // public car les interfaces sont destinées à être totalement
        // implémentées
        int MyMethod();
    }
}

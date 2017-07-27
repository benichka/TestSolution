using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessModifier
{
    class NestedType
    {
        [Obsolete("c'est obsolète")]
        private void Toto()
        {

        }
        class NestedClassDefault
        {
            // Default accessibility for a nested type: private! It behave like a member

            private void Tata()
            {
                var topLevel = new NestedType();
                topLevel.Toto();
            }

        }

        internal class NestedClassInternal
        {

        }
    }
}

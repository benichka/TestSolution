using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessModifier
{
    class TopLevelClass
    {
        // Default access modifier for a top-level type: internal
        // Top-level type : can be internal or public and that's all

        // Default access modifier for a member: private
        int MyProperty { get; set; }
    }
}

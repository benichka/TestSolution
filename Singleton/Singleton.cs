using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    class Singleton
    {
        private static readonly Singleton _Instance;
        public Singleton Instance
        {
            get
            {
                return _Instance;
            }
        }
        public string ID { get; set; }
    }
}

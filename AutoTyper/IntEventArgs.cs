using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoTyper
{
    class IntEventArgs : EventArgs
    {
        public IntEventArgs(int val)
        {
            this.Value = val;
        }

        public int Value { get; private set; }
    }
}

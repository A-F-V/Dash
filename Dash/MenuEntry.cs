using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dash
{
    struct MenuEntry
    {
        public string name;
        public Action action;
        public ConsoleMenu next;

        public MenuEntry(string name, Action action, ConsoleMenu next)
        {
            this.name = name;
            this.action = action;
            this.next = next;
        }
    }
}

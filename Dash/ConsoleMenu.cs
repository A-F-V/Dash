using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dash
{
    public class ConsoleMenu
    {
        private MenuEntry[] options;

        public ConsoleMenu()
        {
            options = new MenuEntry[0];
        }

        public void SetDetails(string[] stringOptions, Action[] menuAction, ConsoleMenu[] nextMenu)
        {
            this.options = new MenuEntry[stringOptions.Length];
            for (int pos = 0; pos < stringOptions.Length; pos++)
            {
                this.options[pos] = new MenuEntry(stringOptions[pos], menuAction[pos], nextMenu[pos]);
            }
        }

        public int optionsCount() => options.Length;

        internal void PerformOption(int response)
        {
            options[response - 1].action();
        }

        public ConsoleMenu Next(int response)
        {
            return options[response - 1].next;
        }

        public string OptionTitle(int response)
        {
            return options[response - 1].name;
        }

        public bool IsEnd()
        {
            return optionsCount() == 0;
        }
    }
}

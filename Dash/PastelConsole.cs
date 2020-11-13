using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pastel;

namespace Dash
{
    public class PastelConsole
    {
        private ColourPalette palette;
        private ConsoleMenu current;

        public PastelConsole(ColourPalette palette, ConsoleMenu cm)
        {
            this.palette = palette;
            current = cm;
        }

        public string Format(string literal, params object[] bindings)
        {
            string output = "";
            string[] temp = literal.Split('{', '}');
            for (int pos = 0; pos < temp.Length; pos++)
            {
                if (pos % 2 == 0)
                {
                    output += temp[pos].Pastel(palette.Body);
                }
                else
                {
                    output += bindings[(pos - 1) / 2].ToString().Pastel(palette[Int32.Parse(temp[pos])]);
                }
            }

            return output;
        }

        public void FormatWriteLine(string literal, params object[] bindings)
        {
            Console.WriteLine(Format(literal, bindings));
        }

        public void FormatWrite(string literal, params object[] bindings)
        {
            Console.Write(Format(literal, bindings));
        }

        public void WriteLine(string literal)
        {
            Console.WriteLine(literal.Pastel(palette.Body));
        }

        public void WriteQuestion(string literal)
        {
            WriteLine(literal);
        }

        public void WriteError(string literal)
        {
            FormatWriteLine("{-3}",literal);
        }

        public void Run()
        {
            if (current.IsEnd())
            {
                return;
            }
            int response = -1;
            if (current.optionsCount() == 1)
            {
                response = 1;
            }
            while (response <= 0 || response > current.optionsCount())
            {
                DisplayMenu();
                WriteQuestion("Please select an option.");
                if (!Int32.TryParse(Console.ReadLine(), out response) ||response<=0||response> current.optionsCount())
                {
                    WriteError("That is not a valid option. Please try again...");
                }
            }
            current.PerformOption(response);
            current = current.Next(response);
            Run();
        }

        private void DisplayMenu()
        {
            for (int choice = 1; choice <= current.optionsCount(); choice++)
            {
                FormatWriteLine($"{{0}}. {{{choice%2+1}}}",choice,current.OptionTitle(choice));
            }
        }
    }
}

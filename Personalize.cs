using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stf
{
    public class Personalize
    {
        private List<Command> _personalizedCommands;

        public Personalize() { }
        public Personalize(string[] args) 
        {
            if (args == null || args.Count() == 0)
            {
                showMenu();    
            }
        }

        private void getPersonalizedCommands()
        {
#warning implementare
        }

        private void showMenu()
        {
            Console.WriteLine();
        }
    }
}

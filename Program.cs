using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace stf
{
    internal class Program
    {
        private static List<Command> _commands;

        public static void Main(string[] args)
        {
            getCommandsList();

            if (args == null || args.Count() == 0)
            {
                showMainMenu();
                return;
            }

            if (int.TryParse(args[0], out int i))
            {
                Console.WriteLine("invalid command...");
                return;
            }

            var cmd = commandByName(args[0]);
            if (cmd != null)
            {
                if (cmd != null)
                {
                    var cleanedArgs = args.Skip(1);
                    cmd.Action(cleanedArgs);

                    return;
                }
            }

            Console.WriteLine("Command not found...");
        }

        private static void showMainMenu()
        {
            if (_commands == null || _commands.Count == 0)
            {
                Console.WriteLine("No commands implemented...");
            }
            else
            {
                string logMenu = _commands.ConvertAll(e =>
                {

                    string sh = "";
                    if (e.Shorts != null && e.Shorts.Count != 0)
                    {
                        sh = e.Shorts.ToList().ConvertAll(s => $"-{s}")
                           .Aggregate((a, b) => $"{a}|{b}");
                    }

                    if (sh != "")
                    {
                        return $"   {e.Name}\t\t{e.Description}{Environment.NewLine}" +
                                $"  [{sh}]";
                    }
                    else
                    {
                        return $"   {e.Name}\t\t{e.Description}";
                    }

                })
                    .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");
                Console.WriteLine(logMenu);
            }
        }
        //
        private static void getCommandsList()
        {
            _commands = new List<Command>();
            addCommand("version", version, "print the version", new[] { "s" });
            addCommand("personalize", personalize, "enter in personalize mode", new[] { "prs" });
        }
        private static Command commandByName(string name)
        {
            if (_commands == null) { return null; }

            if (name[0] == '-')
            {
                return _commands.Find(c => c.Shorts.Find(s => s == name.Substring(1)) != null);
            }
            else
            {
                return _commands.Find(c => c.Name == name);
            }
        }
        private static void addCommand(string cmd, Action<IEnumerable<string>> action, string description, IEnumerable<string> shortCommands = null)
        {
            if (_commands == null) { return; }
            var command = new Command(cmd, action, description);
            if (shortCommands != null)
            {
                foreach (string sh in shortCommands) 
                {
                    command.AddShortCommand(sh);
                }
            }
            _commands.Add(command);
        }

        #region commands
        private static void version(IEnumerable<string> args)
        {
            string logVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Console.WriteLine(logVersion);
        }
        private static void personalize(IEnumerable<string> args)
        {

        }
        #endregion
    }
}

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
        private static List<Tuple<commands, Action<IEnumerable<string>>, string>> _commands;
        private static Dictionary<shortCommands, commands> _dctShortCommands;

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

            if (Enum.TryParse(args[0], out commands c))
            {
                var cmd = _commands.Find(cm => cm.Item1 == c);
                if (cmd != null)
                {
                    var cleanedArgs = args.Skip(1);
                    cmd.Item2(cleanedArgs);

                    return;
                }
            }
            else if (args[0][0] == '-' && Enum.TryParse(args[0].Substring(1), out shortCommands sc))
            {
                if (_dctShortCommands.ContainsKey(sc))
                {
                    args[0] = _dctShortCommands[sc].ToString();
                    Main(args);
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
                    string sh = _dctShortCommands.ToList().FindAll(d => d.Value == e.Item1)
                        .ConvertAll(s => $"-{s.Key.ToString()}").Aggregate((a, b) => $"{a}|{b}");
                    return $"   {e.Item1}[{sh}]\t\t{e.Item3}";
                })
                    .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");
                Console.WriteLine(logMenu);
            }
        }
        //
        private static void getCommandsList()
        {
            _commands = new List<Tuple<commands, Action<IEnumerable<string>>, string>>();
            addCommand(commands.version, version, "print the version");

            _dctShortCommands = new Dictionary<shortCommands, commands>();
            _dctShortCommands.Add(shortCommands.v, commands.version);
        }
        private static void addCommand(commands cmd, Action<IEnumerable<string>> action, string description)
        {
            if (_commands == null) { return; }
            _commands.Add(new Tuple<commands, Action<IEnumerable<string>>, string>(cmd, action, description));
        }

        #region commands
        private static void version(IEnumerable<string> args)
        {
            string logVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Console.WriteLine(logVersion);
        }
        #endregion

        private enum commands
        {
            version
        }
        private enum shortCommands
        {
            v
        }
    }
}

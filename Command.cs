namespace stf
{
    public class Command
    {
        private List<string> _shorts;

        public Command(string name, Action<IEnumerable<string>> action, string description)
        {
            Name = name;
            Action = action;
            Description = description;

            _shorts = new List<string>();
        }

        public string Name { get; set; }
        public Action<IEnumerable<string>> Action { get; set; }
        public List<string> Shorts { get => _shorts; set => _shorts = value; }
        public string Description { get; set; }

        public void AddShortCommand(string sh)
        {
            _shorts.Add(sh);
        }
    }
}

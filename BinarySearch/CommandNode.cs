using System.Collections.Generic;

namespace BinarySearch
{
    public class CommandNode
    {
        public CommandNode LeftNode { get; set; }
        public CommandNode RightNode { get; set; }
        public int Priority { get; set; } = int.MinValue;
        public List<CommandImpl> Commands { get; set; } = new List<CommandImpl>();

    }
    public class CommandImpl
    {
    }
}

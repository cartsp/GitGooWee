using System;
namespace GitGooWee
{
    public class Branch
    {
        public string Name { get; set; }
        public bool Current { get; set; }

        public override string ToString() => 
            Current ? $"{Name} (Active)" : Name;
    }
 }

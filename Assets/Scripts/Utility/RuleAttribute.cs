using System;

namespace Utility
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RuleAttribute : Attribute
    {
        public string Name;

        public RuleAttribute(string name)
        {
            Name = name;
        }
    }
}
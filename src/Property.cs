using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ReaperParser
{
    class Property
    {
        public Property(string name, IEnumerable<string> values)
        {
            Name = name;
            Values = values;
        }

        public string Name { get; }

        public IEnumerable<string> Values { get; }

        public override string ToString()
        {
            return $"{Name} {string.Join(" ", Values.Select(s => string.IsNullOrWhiteSpace(s) ? @"""" : s))}";
        }
        
        public sealed class PropertyCollection : Collection<Property>
        {
            public IEnumerable<Property> this[string name]
            {
                get { return Items.Where(p => p.Name == name); }
            }
        }
    }
}

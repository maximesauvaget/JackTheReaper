using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static ReaperParser.Property;

namespace ReaperParser
{
    class Element
    {
        protected List<string> _attributes;

        protected Element(string name)
        {
            Name = name;
            Properties = new PropertyCollection();
            Elements = new ElementCollection();
            _attributes = new List<string>();
        }

        public string Name { get; }

        public IReadOnlyList<string> Attributes => _attributes;

        public PropertyCollection Properties { get; }

        public ElementCollection Elements { get; }

        public Element AddElement(string name, string[] attributes)
        {
            var element = new Element(name);
            Elements.Add(element);
            if(_attributes != null)
                element._attributes.AddRange(attributes);
                
            return element;
        }

        protected void InternalToString(StringBuilder sb, int level)
        {
            string makeIndent(int l) => string.Empty.PadLeft(l * 2, ' ');

            sb.AppendLine($"{makeIndent(level)}{Constants.START}{Name} {string.Join(" ", Attributes)}");
            foreach(var property in Properties)
            {
                sb.AppendLine($"{makeIndent(level + 1)}{property}");
            }

            foreach (var e in Elements)
            {
                e.InternalToString(sb, level + 1);
            }

            sb.AppendLine($"{makeIndent(level)}{Constants.END}");
        }

        public sealed class ElementCollection : Collection<Element>
        {
            internal ElementCollection()
            {

            }

            public IEnumerable<Element> this[string name]
            {
                get { return Items.Where(p => p.Name == name); }
            }
        }
    }
}

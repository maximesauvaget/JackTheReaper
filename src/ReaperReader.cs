using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReaperParser
{
    class ReaperReader
    {
        private ReaperReader()
        {
        }

        public static async Task<ReaperProject> Read(string filename)
        {
            ReaperProject reaperProjet = null;

            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(stream))
            {
                Stack<Element> elementStack = new Stack<Element>();

                string line = await reader.ReadLineAsync();
                var root = ParseElement(line);
                reaperProjet = ReaperProject.New(root.name, root.attributes);
                elementStack.Push(reaperProjet);

                while (line != null)
                {
                    Element currentElement = elementStack.Peek();

                    line = line.Trim();
                    if (line.StartsWith(Constants.START))
                    {
                        var (name, attributes) = ParseElement(line);
                        if (name != ReaperProject.REAPER_PROJECT)
                        {
                            var newElement = currentElement.AddElement(name, attributes);
                            elementStack.Push(newElement);
                        }
                    }
                    else if (line.StartsWith(Constants.END))
                    {
                        if (elementStack.Count > 0)
                            elementStack.Pop();
                    }
                    else
                    {
                        var property = ParseProperty(line);
                        currentElement.Properties.Add(property);
                    }

                    line = await reader.ReadLineAsync();
                }

                if (elementStack.Any())
                    throw new FormatException();
            }

            return reaperProjet;
        }
        
        private static (string name, string[] attributes)  ParseElement(string s)
        {
            s = new string(s.SkipWhile(c => !Equals(c, Constants.START)).Skip(1).ToArray());
            var split = s.Split(' ');

            return (split[0], split.Skip(1).ToArray());
        }

        private static Property ParseProperty(string s)
        {
            var split = s.Split(' ');
            var propertyName = split[0];
            var propertyValues = split.Skip(1).ToArray();

            return new Property(propertyName, propertyValues);
        }
    }
}

using System;
using System.Text;

namespace ReaperParser
{
    class ReaperProject : Element
    {
        public const string REAPER_PROJECT = "REAPER_PROJECT";

        private ReaperProject() : base(REAPER_PROJECT)
        {

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            InternalToString(sb, 0);

            return sb.ToString();
        }

        public static ReaperProject New(string name, string[] attributes)
        {
            if (name != REAPER_PROJECT)
                throw new ArgumentException(nameof(name));

            var project = new ReaperProject();
            if(attributes != null)
                project._attributes.AddRange(attributes);

            return project;
        }
    }
}

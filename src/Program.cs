using ReaperParser;
using System;
using System.Linq;

namespace MyProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var project = ReaperReader.Read(@"sample.rpp").GetAwaiter().GetResult();
            
            Console.WriteLine(project.Properties["VIDEO_CONFIG"].FirstOrDefault());

            var tracks = project.Elements["TRACK"];
            foreach(var track in tracks)
            {
                Console.WriteLine(track.Properties["TRACKID"].FirstOrDefault());
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine(project);

            Console.ReadKey();
        }
    }
}

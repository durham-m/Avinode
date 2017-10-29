using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Avinode
{
    public class PrintMenu
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Incorrect number of arguments. Usage: PrintMenu xmlFilePath pathToMatch");
                return;
            }

            var xmlFilePath = args[0];
            var pathToMatch = args[1];

            if (!File.Exists(xmlFilePath))
            {
                Console.WriteLine(string.Format("Error: File {0} cannot be found.", xmlFilePath));
                return;
            }

            var xdoc = XDocument.Load(xmlFilePath);
            foreach (var element in xdoc.Descendants("item"))
            {
                var indentCount = 0;
                var isActive = IsActive(element, pathToMatch);
                indentCount = element.Ancestors().Count(x => x.Name == "subMenu");
                var displayName = element.Element("displayName");
                var path = element.Element("path");
                Console.WriteLine(new String('\t', indentCount) + displayName.Value + ", " + path.Attribute("value").Value + (isActive ? " ACTIVE" : ""));
            }

        }

        private static bool IsActive(XElement element, string pathToMatch)
        {
            if (element.DescendantsAndSelf().Any(x => x.Name == "path" && x.Attribute("value").Value.Equals(pathToMatch, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
            return false;
        }
    }
}

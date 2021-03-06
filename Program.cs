﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RomDeDupe
{
    class Program
    {
        static void Main(string[] args)
        {
                try
                {
                    DeDupe();
                }
                catch{

                }
        }

        public static void DeDupe()
        {
            var romDirectories = new DirectoryInfo("C:\\roms\\").GetDirectories();
            foreach(var romDirectory in romDirectories)
            {
                var fileNames = romDirectory.GetFiles().Select(x => x.Name);
                foreach(var fileName in fileNames)
                {
                    var name = fileName.Split("(")[0];
                    var similarNames = fileNames.Where(x => x.Contains(name));
                    var i=0;
                    foreach(var item in similarNames)
                    {
                        if(Regex.IsMatch(name,"1|2|3|4|5|6|7|8|9|\\sI\\s|\\sII\\s|\\sIII\\s"))
                            continue;
                        if(i > similarNames.Count())
                            break;

                            if(i==0)
                            {
                                i++;
                                continue;
                            }
                            else{
                                Console.WriteLine("Keeping " + similarNames.ElementAt(0) + ", Removing " +  similarNames.ElementAt(i));
                                File.Delete(Path.Combine(romDirectory.FullName, item));
                                DeDupe();
                                throw new Exception("Completed");
                            }
                    }
                }
            }
        }
    }
}

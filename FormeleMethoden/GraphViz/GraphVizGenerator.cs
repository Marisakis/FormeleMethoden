using Automata;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace GraphViz
{
    public class GraphVizGenerator
    {

        public static void generateFiles(Automata<String> automata)
        {
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //TODO: apply automata name to file name
            var gvfile = Path.Combine(projectFolder, @"Automata.gv");
            var pngfile = Path.Combine(projectFolder, @"Automata.png");
            System.Diagnostics.Debug.WriteLine("Creating files in " + projectFolder);

            GenerateGVFile(automata, gvfile);
            generatePNGFromGV(gvfile, pngfile);
        }
        private static void GenerateGVFile(Automata<String> automata, string gvFilepath)
        {
            StreamWriter writer = new StreamWriter(gvFilepath);
            writer.WriteLine("digraph graphname {");

            foreach(Transition<String> t in automata.getTransitions())
            {
                var line = t.getFromState() + " -> " + t.getToState() + " [label=" + t.getSymbol() + "];";
                writer.WriteLine(line);
            }

            foreach (string t in automata.getFinalStates())
            {
                writer.WriteLine(t + " [shape=doublecircle]");
            }

            writer.WriteLine("}");
            writer.Close();
            
        }

        private static void generatePNGFromGV(string gvFilepath, string pngFilepath)
        {

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C dot -Tpng " + gvFilepath + " > " + pngFilepath;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            
        }
    }
}

    

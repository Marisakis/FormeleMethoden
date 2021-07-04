using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Formele_Methoden_Eindopdracht.GraphViz
{
    public class GraphVizGenerator
    {

        public static String generateFiles(Automata automata, string filename) //May not contain spaces!
        {
            /*var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;*/
            var savePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var gvFilePath = Path.Combine(savePath, @"" + filename + ".gv");
            var pngFilePath = Path.Combine(savePath, @"" + filename + ".png");
            System.Diagnostics.Debug.WriteLine("Creating files in " + savePath);

            GenerateGVFile(automata, gvFilePath);
            generatePNGFromGV(gvFilePath, pngFilePath);
            return pngFilePath;
        }

        private static void GenerateGVFile(Automata automata, string gvFilepath)
        {
            StreamWriter writer = new StreamWriter(gvFilepath);
            writer.WriteLine("digraph graphname {");

            /*foreach (Transition t in automata.getTransitions())
            {
                var line = t.getFromState() + " -> " + t.getToState() + " [label=" + t.getSymbol() + "];";
                writer.WriteLine(line);
            }

            foreach (string t in automata.getFinalStates())
            {
                writer.WriteLine(t + " [shape=doublecircle]");
            }*///old

            foreach(State s in automata.GetStates())
            {
                foreach(Transition t in s.GetTransitions())
                {
                    var line = t.PreviousState.Name + " -> " + t.NextState.Name + " [label=" + t.Character + "];";
                    writer.WriteLine(line);

                }
                if(s.stateType == State.StateType.END_STATE || s.stateType == State.StateType.START_AND_END_STATE)
                    writer.WriteLine(s.Name + " [shape=doublecircle]");

            }

            writer.WriteLine("}");
            writer.Close();

        }

        private static void generatePNGFromGV(string gvFilepath, string pngFilepath) // Does not overwrite files! 
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

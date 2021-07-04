using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Formele_Methoden_Eindopdracht
{
    public partial class MainWindow : Window
    {
        public enum AutomataSelection
        {
            STARTS_WITH,
            ENDS_WITH,
            CONTAINS,
            EVEN_CHARACTERS,
            UNEVEN_CHARACTERS
        }

        public enum AutomataType
        { 
            DFA,
            NDFA
        }

        private Dictionary<string, Automata> createdAutomata;

        public MainWindow()
        {
            InitializeComponent();

            this.createdAutomata = new Dictionary<string, Automata>();

            this.createdAutomata.Add("Contains abba", AutomataBuilder.ContainsDFA("abba", new List<char>() { 'a', 'b' }));
            this.createdAutomata["Contains abba"].Validate();
            //this.createdAutomata.Add("aaa",TestAutomata.RegexAAA());
            //this.createdAutomata.Add("aab", TestAutomata.RegexAAB());
            this.createdAutomata.Add("abc", TestAutomata.RegexABC());
            this.createdAutomata.Add("aorb", TestAutomata.RegexAorB());
            this.createdAutomata.Add("aloop", TestAutomata.RegexAloop());

            TestAutomata.RegexABC().GenerateWordsNotInLanguage(10,25);

            UpdateComboBoxes();

            cmb_Automata_DropDownClosed(cmb_Automata, null);
            cmb_FirstAutomata_DropDownClosed(cmb_FirstAutomata, null);
            cmb_SecondAutomata_DropDownClosed(cmb_SecondAutomata, null);

            cmb_CreateAutomataTypes.ItemsSource = new List<string>() { "StartsWith", "EndsWith", "Contains", "EvenCharacters", "UnevenCharacters" };
            cmb_CreateAutomataTypes.SelectedIndex = 0;

            cmb_Operator.ItemsSource = new List<string>() { "AND", "OR", "NOT" };
            cmb_Operator.SelectedIndex = 0;

            cmb_Conversion.ItemsSource = new List<string>() { "ToDFA", "Minimize_Table", "Minimize_Reverse" };
            cmb_Conversion.SelectedIndex = 0;


            //Automata regexAutomata = RegexTranslator.TranslateRegex("ab*");
            ////regexAutomata.Minimized();

            //Automata dfaRegex = regexAutomata.ConvertedToDFA();

            //bool testResult1 = dfaRegex.Evaluate("ab");
            //bool testResult2 = dfaRegex.Evaluate("abb");
            //bool testResult3 = dfaRegex.Evaluate("abbb");
            //bool testResult4 = dfaRegex.Evaluate("abbbb");

            //bool testResult5 = dfaRegex.Evaluate("ba");
            //bool testResult6 = dfaRegex.Evaluate("baa");
            //bool testResult7 = dfaRegex.Evaluate("bbaa");
            //bool testResult8 = dfaRegex.Evaluate("bbba");

            //int test = 0;

           /* List<char> symbols1 = new List<char>() { 'a', 'b' };
            Automata startsWith = AutomataBuilder.StartsWithDFA("ab", symbols1);
            startsWith.Minimized();
            int test = 0;*/


            //List<char> symbols = new List<char>() { 'a', 'b' };
            //Automata evenAutomata = TestAutomata.EvenNumberOfCharacters('a', symbols);
            //Automata unevenAutomata = TestAutomata.UnevenNumberOfCharacters('a', symbols);

            //bool test11 = evenAutomata.Evaluate("a");
            //bool test12 = evenAutomata.Evaluate("aa");
            //bool test13 = evenAutomata.Evaluate("aaa");
            //bool test14 = evenAutomata.Evaluate("aaaa");

            //bool test21 = unevenAutomata.Evaluate("a");
            //bool test22 = unevenAutomata.Evaluate("aa");
            //bool test23 = unevenAutomata.Evaluate("aaa");
            //bool test24 = unevenAutomata.Evaluate("aaaa");

            //Automata even2Automata = Automata.Not(evenAutomata);
            //Automata uneven2Automata = Automata.Not(unevenAutomata);

            //bool test31 = even2Automata.Evaluate("a");
            //bool test32 = even2Automata.Evaluate("aa");
            //bool test33 = even2Automata.Evaluate("aaa");
            //bool test34 = even2Automata.Evaluate("aaaa");

            //bool test41 = uneven2Automata.Evaluate("a");
            //bool test42 = uneven2Automata.Evaluate("aa");
            //bool test43 = uneven2Automata.Evaluate("aaa");
            //bool test44 = uneven2Automata.Evaluate("aaaa");

            //Automata andAutomata = Automata.And(evenAutomata, unevenAutomata);

            //bool test51 = andAutomata.Evaluate("a");
            //bool test52 = andAutomata.Evaluate("aa");
            //bool test53 = andAutomata.Evaluate("aaa");
            //bool test54 = andAutomata.Evaluate("aaaa");

            //Automata orAutomata = Automata.Or(evenAutomata, unevenAutomata);

            //bool test61 = orAutomata.Evaluate("a");
            //bool test62 = orAutomata.Evaluate("aa");
            //bool test63 = orAutomata.Evaluate("aaa");
            //bool test64 = orAutomata.Evaluate("aaaa");

            //Console.WriteLine("Automata even:");
            //Console.WriteLine("--------------------------------");
            //Console.WriteLine(evenAutomata.ToString());
            //Console.WriteLine("--------------------------------");

            //Console.WriteLine("Automata even (not):");
            //Console.WriteLine("--------------------------------");
            //Console.WriteLine(even2Automata.ToString());
            //Console.WriteLine("--------------------------------");

            //Console.WriteLine("Automata uneven:");
            //Console.WriteLine("--------------------------------");
            //Console.WriteLine(unevenAutomata.ToString());
            //Console.WriteLine("--------------------------------");

            //Console.WriteLine("Automata uneven (not):");
            //Console.WriteLine("--------------------------------");
            //Console.WriteLine(uneven2Automata.ToString());
            //Console.WriteLine("--------------------------------");





            //List<char> symbols = new List<char>() { 'a', 'b' };
            //Automata startsWithAutomataA = new Automata(symbols);
            //startsWithAutomataA.AddStartState("1");
            //startsWithAutomataA.AddIntermediateState("2");
            //startsWithAutomataA.AddEndState("3");
            //startsWithAutomataA.AddIntermediateState("fuik");

            //startsWithAutomataA.AddTransition('a', "1", "2");
            //startsWithAutomataA.AddTransition('b', "2", "3");
            //startsWithAutomataA.AddMissingSymbolTransitions("1", "fuik");
            //startsWithAutomataA.AddMissingSymbolTransitions("2", "fuik");
            //startsWithAutomataA.AddMissingSymbolTransitions("3", "3");
            //startsWithAutomataA.AddMissingSymbolTransitions("fuik", "fuik");

            //Automata startsWithAutomataB = new Automata(symbols);
            //startsWithAutomataB.AddStartState("1");
            //startsWithAutomataB.AddEndState("2");
            //startsWithAutomataB.AddIntermediateState("fuik");

            //startsWithAutomataB.AddTransition('a', "1", "2");
            //startsWithAutomataB.AddMissingSymbolTransitions("1", "fuik");
            //startsWithAutomataB.AddMissingSymbolTransitions("2", "2");
            //startsWithAutomataB.AddMissingSymbolTransitions("fuik", "fuik");

            //bool resultA1 = startsWithAutomataA.Evaluate("abb");
            //bool resultA2 = startsWithAutomataA.Evaluate("bab");

            //bool resultB1 = startsWithAutomataA.Evaluate("bab");
            //bool resultB2 = startsWithAutomataA.Evaluate("abb");

            //Automata andAutomata = Automata.And(startsWithAutomataA, startsWithAutomataB);

            //bool resultC1 = andAutomata.Evaluate("ab");
            //bool resultC2 = andAutomata.Evaluate("a");
            //bool resultC3 = andAutomata.Evaluate("ba");
            //bool resultC4 = andAutomata.Evaluate("b");

            //Automata orAutomata = Automata.Or(startsWithAutomataA, startsWithAutomataB);

            //bool resultD1 = orAutomata.Evaluate("ab");
            //bool resultD2 = orAutomata.Evaluate("a");
            //bool resultD3 = orAutomata.Evaluate("ba");
            //bool resultD4 = orAutomata.Evaluate("b");



            //List<char> symbols = new List<char>() { 'a', 'b' };

            //Automata startsWithDFA = AutomataBuilder.StartsWithDFA("abba", symbols);

            //bool testA1 = startsWithDFA.Evaluate("abbababa");
            //bool testA2 = startsWithDFA.Evaluate("baabbaba");

            //Automata endsWithDFA = AutomataBuilder.EndsWithDFA("abba", symbols);

            //bool testB1 = endsWithDFA.Evaluate("abbabbaa");
            //bool testB2 = endsWithDFA.Evaluate("baababba");

            //Automata containsDFA = AutomataBuilder.ContainsDFA("abba", symbols);

            //bool testC1 = containsDFA.Evaluate("abbabbaa");
            //bool testC2 = containsDFA.Evaluate("baababba");
            //bool testC3 = containsDFA.Evaluate("baabbaab");
            //bool testC4 = containsDFA.Evaluate("abababab");

            //Automata testAutomata = Automata.And(startsWithDFA, endsWithDFA);

            //Console.WriteLine(testAutomata.ToString());

            //bool testResult1 = testAutomata.Evaluate("abbaabababba");
            //bool testResult2 = testAutomata.Evaluate("babaabbababa");
            //bool testResult3 = testAutomata.Evaluate("abbababababa");
            //bool testResult4 = testAutomata.Evaluate("babababaabba");

            //lbl_Output.Content = "";

            //GetSymbolsFromString("a,,b");





            List<char> symbols = new List<char>() { 'a', 'b' };

            //Automata a = new Automata(symbols);

            //a.AddStartState("0");
            //a.AddIntermediateState("1");
            //a.AddEndState("2");

            //a.AddTransition('a', "0", "1");
            //a.AddTransition('b', "1", "2");

            //Automata aDFA = a.ConvertedToDFA();

            //// Should accept
            //bool aResult1A = aDFA.Evaluate("ab");

            //// Should decline
            //bool aResult1D = aDFA.Evaluate("ba");
            //bool aResult2D = aDFA.Evaluate("bbba");
            //bool aResult3D = aDFA.Evaluate("baaa");
            //bool aResult4D = aDFA.Evaluate("bbaa");

            //Console.WriteLine(a.ToString());
            //Console.WriteLine(aDFA.ToString());

            //Automata b = new Automata(symbols);

            //b.AddStartState("0");
            //b.AddIntermediateState("1");
            //b.AddIntermediateState("3");
            //b.AddEndState("2");
            //b.AddEndState("4");

            //b.AddTransition('b', "0", "1");
            //b.AddTransition('a', "1", "2");
            //b.AddTransition('b', "1", "3");
            //b.AddTransition('a', "3", "4");

            //Automata bDFA = b.ConvertedToDFA();

            //// Should accept
            //bool bResult1A = bDFA.Evaluate("ba");
            //bool bResult2A = bDFA.Evaluate("bba");

            //// Should decline
            //bool bResult1D = bDFA.Evaluate("ab");
            //bool bResult2D = bDFA.Evaluate("aaab");
            //bool bResult3D = bDFA.Evaluate("abbb");
            //bool bResult4D = bDFA.Evaluate("aabb");

            //Console.WriteLine(b.ToString());
            //Console.WriteLine(bDFA.ToString());

            Automata c = new Automata(symbols);

            c.AddStartState("0");
            c.AddIntermediateState("1");
            c.AddIntermediateState("2");
            c.AddIntermediateState("4");
            c.AddIntermediateState("5");
            c.AddIntermediateState("7");
            c.AddIntermediateState("9");
            c.AddEndState("3");
            c.AddEndState("6");
            c.AddEndState("8");

            c.AddTransition('a', "0", "1");
            c.AddTransition("1", "2");
            c.AddTransition('b', "2", "3");

            c.AddTransition("1", "4");
            c.AddTransition('a', "4", "5");
            c.AddTransition('b', "5", "6");

            c.AddTransition('a', "5", "7");
            c.AddTransition("7", "8");

            Automata cDFA = c.ConvertedToDFA();

            // Should accept
            bool cResult1A = cDFA.Evaluate("ab");
            bool cResult2A = cDFA.Evaluate("aab");
            bool cResult3A = cDFA.Evaluate("aaa");

            // Should decline
            bool cResult1D = cDFA.Evaluate("ab");
            bool cResult2D = cDFA.Evaluate("aaab");
            bool cResult3D = cDFA.Evaluate("abbb");
            bool cResult4D = cDFA.Evaluate("aabb");

            //Console.WriteLine(c.ToString());
            //Console.WriteLine(cDFA.ToString());

            //Automata d = new Automata(symbols);

            //d.AddStartState("0");
            //d.AddIntermediateState("1");
            //d.AddIntermediateState("3");
            //d.AddEndState("2");
            //d.AddEndState("4");

            //d.AddTransition('b', "0", "1");
            //d.AddTransition("1", "2");
            //d.AddTransition('b', "1", "3");
            //d.AddTransition('a', "3", "4");

            //Automata dDFA = d.ConvertedToDFA();

            //// Should accept
            //bool dResult1A = dDFA.Evaluate("b");
            //bool dResult2A = dDFA.Evaluate("bba");

            //// Should decline
            //bool dResult1D = dDFA.Evaluate("ab");
            //bool dResult2D = dDFA.Evaluate("aaab");
            //bool dResult3D = dDFA.Evaluate("bbaa");
            //bool dResult4D = dDFA.Evaluate("aabb");

            //Console.WriteLine(d.ToString());
            //Console.WriteLine(dDFA.ToString());
        }

        private void UpdateComboBoxes()
        {
            List<string> automataNames = this.createdAutomata.Keys.ToList();

            cmb_Automata.ItemsSource = automataNames;
            cmb_FirstAutomata.ItemsSource = automataNames;
            cmb_SecondAutomata.ItemsSource = automataNames;
            cmb_ConversionAutomata.ItemsSource = automataNames;
            cmb_VisualizeAutomata.ItemsSource = automataNames;

            cmb_Automata.SelectedIndex = 0;
            cmb_FirstAutomata.SelectedIndex = 0;
            cmb_SecondAutomata.SelectedIndex = 0;
            cmb_ConversionAutomata.SelectedIndex = 0;
            cmb_VisualizeAutomata.SelectedIndex = 0;
        }

        #region MENU_BUTTONS

        private void btn_AutomataMenu_Click(object sender, RoutedEventArgs e)
        {
            stk_CreateAutomataPanel.Visibility = Visibility.Hidden;
            stk_OperatorsPanel.Visibility = Visibility.Hidden;
            stk_ConversionPanel.Visibility = Visibility.Hidden;
            stk_RegularExpressionsPanel.Visibility = Visibility.Hidden;
            stk_GraphvizPanel.Visibility = Visibility.Hidden;

            stk_AutomataPanel.Visibility = Visibility.Visible;
        }

        private void btn_CreateAutomataMenu_Click(object sender, RoutedEventArgs e)
        {
            stk_AutomataPanel.Visibility = Visibility.Hidden;
            stk_OperatorsPanel.Visibility = Visibility.Hidden;
            stk_ConversionPanel.Visibility = Visibility.Hidden;
            stk_RegularExpressionsPanel.Visibility = Visibility.Hidden;
            stk_GraphvizPanel.Visibility = Visibility.Hidden;

            stk_CreateAutomataPanel.Visibility = Visibility.Visible;
        }

        private void btn_OperatorsMenu_Click(object sender, RoutedEventArgs e)
        {
            stk_AutomataPanel.Visibility = Visibility.Hidden;
            stk_CreateAutomataPanel.Visibility = Visibility.Hidden;
            stk_ConversionPanel.Visibility = Visibility.Hidden;
            stk_RegularExpressionsPanel.Visibility = Visibility.Hidden;
            stk_GraphvizPanel.Visibility = Visibility.Hidden;

            stk_OperatorsPanel.Visibility = Visibility.Visible;
        }

        private void btn_Conversion_Click(object sender, RoutedEventArgs e)
        {
            stk_AutomataPanel.Visibility = Visibility.Hidden;
            stk_CreateAutomataPanel.Visibility = Visibility.Hidden;
            stk_OperatorsPanel.Visibility = Visibility.Hidden;
            stk_RegularExpressionsPanel.Visibility = Visibility.Hidden;
            stk_GraphvizPanel.Visibility = Visibility.Hidden;

            stk_ConversionPanel.Visibility = Visibility.Visible;
        }

        private void btn_RegularExpressionsMenu_Click(object sender, RoutedEventArgs e)
        {
            stk_AutomataPanel.Visibility = Visibility.Hidden;
            stk_CreateAutomataPanel.Visibility = Visibility.Hidden;
            stk_OperatorsPanel.Visibility = Visibility.Hidden;
            stk_ConversionPanel.Visibility = Visibility.Hidden;
            stk_GraphvizPanel.Visibility = Visibility.Hidden;

            stk_RegularExpressionsPanel.Visibility = Visibility.Visible;
        }

        private void btn_Graphviz_Click(object sender, RoutedEventArgs e)
        {
            stk_AutomataPanel.Visibility = Visibility.Hidden;
            stk_CreateAutomataPanel.Visibility = Visibility.Hidden;
            stk_OperatorsPanel.Visibility = Visibility.Hidden;
            stk_ConversionPanel.Visibility = Visibility.Hidden;
            stk_RegularExpressionsPanel.Visibility = Visibility.Hidden;

            stk_GraphvizPanel.Visibility = Visibility.Visible;
        }

        #endregion

        private void btn_Evaluate_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txb_EvaluateInput.Text))
            {
                lbl_ResultMessage.Content = "Input field is empty!";
                lbl_ResultMessage.Foreground = Brushes.Red;
            }
            else
            {
                Automata selectedAutomata = this.createdAutomata[cmb_Automata.Text];

                if (selectedAutomata.IsDFA)
                {
                    if (selectedAutomata.Evaluate(txb_EvaluateInput.Text))
                    {
                        lbl_ResultMessage.Content = "Accepted!";
                        lbl_ResultMessage.Foreground = Brushes.Green;
                    }
                    else
                    {
                        lbl_ResultMessage.Content = "Not accepted!";
                        lbl_ResultMessage.Foreground = Brushes.Orange;
                    }
                }
                else
                {
                    lbl_ResultMessage.Content = "Selected automata is not a DFA! (Convert to DFA to evaluate)";
                    lbl_ResultMessage.Foreground = Brushes.Red;
                }
            }

            lbl_ResultMessage.Visibility = Visibility.Visible;
        }

        private void btn_CreateAutomata_Click(object sender, RoutedEventArgs e)
        {
            lbl_CreateAutomataMessage.Visibility = Visibility.Hidden;
            lbl_CreateAutomataMessage.Content = "";

            bool errors = false;
            if (String.IsNullOrEmpty(txb_CreateAutomataName.Text))
            {
                lbl_CreateAutomataMessage.Content += "Name field is empty!\n";
                errors = true;
            }
            else if(this.createdAutomata.ContainsKey(txb_CreateAutomataName.Text))
            {
                lbl_CreateAutomataMessage.Content += "Name already used!\n";
                errors = true;
            }

            if (String.IsNullOrEmpty(txb_CreateAutomataInput.Text))
            {
                lbl_CreateAutomataMessage.Content += "Input field is empty!\n";
                errors = true;
            }
            if (String.IsNullOrEmpty(txb_CreateAutomataSymbols.Text))
            {
                lbl_CreateAutomataMessage.Content += "Symbols field is empty!\n";
                errors = true;
            }

            if(!String.IsNullOrEmpty(txb_CreateAutomataInput.Text) && !String.IsNullOrEmpty(txb_CreateAutomataSymbols.Text))
            {
                foreach(char character in txb_CreateAutomataInput.Text)
                {
                    if(!txb_CreateAutomataSymbols.Text.Contains(character))
                    {
                        lbl_CreateAutomataMessage.Content += "Input contains invalid symbols!\n";
                        errors = true;
                        break;
                    }
                }
            }

            if (!errors)
            {
                Tuple<bool, List<char>> symbolsResult = GetSymbolsFromString(txb_CreateAutomataSymbols.Text);

                if (!symbolsResult.Item1 || (symbolsResult.Item2.Distinct().Count() != symbolsResult.Item2.Count))
                {
                    lbl_CreateAutomataMessage.Content += "Symbols definition invalid!\n";
                    lbl_CreateAutomataMessage.Foreground = Brushes.Red;
                    lbl_CreateAutomataMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    Tuple<string, Automata> createdAutomataResult = GetAutomataFromTypeString(txb_CreateAutomataInput.Text, symbolsResult.Item2);
                    if(createdAutomataResult.Item2 == null)
                    {
                        lbl_CreateAutomataMessage.Content += (createdAutomataResult.Item1 + "\n");
                        lbl_CreateAutomataMessage.Foreground = Brushes.Red;
                        lbl_CreateAutomataMessage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.createdAutomata.Add(txb_CreateAutomataName.Text, createdAutomataResult.Item2);
                        this.createdAutomata[txb_CreateAutomataName.Text].Validate();

                        txb_CreateAutomataName.Text = "";
                        txb_CreateAutomataInput.Text = "";
                        txb_CreateAutomataSymbols.Text = "";
                        cmb_CreateAutomataTypes.SelectedIndex = 0;

                        lbl_CreateAutomataMessage.Content = "Automata added!\n";
                        lbl_CreateAutomataMessage.Foreground = Brushes.Green;
                        lbl_CreateAutomataMessage.Visibility = Visibility.Visible;

                        UpdateComboBoxes();
                    }
                }
            }
            else
            {
                lbl_CreateAutomataMessage.Foreground = Brushes.Red;
                lbl_CreateAutomataMessage.Visibility = Visibility.Visible;
            }
        }

        private void btn_OperatorCreateAutomata_Click(object sender, RoutedEventArgs e)
        {
            lbl_OperatorCreateAutomataMessage.Visibility = Visibility.Hidden;
            lbl_OperatorCreateAutomataMessage.Content = "";

            bool errors = false;
            if (String.IsNullOrEmpty(txb_OperatorsCreateAutomataName.Text))
            {
                lbl_OperatorCreateAutomataMessage.Content += "Name field is empty!\n";
                errors = true;
            }
            else if (this.createdAutomata.ContainsKey(txb_OperatorsCreateAutomataName.Text))
            {
                lbl_OperatorCreateAutomataMessage.Content += "Name already used!\n";
                errors = true;
            }

            if (String.IsNullOrEmpty(cmb_FirstAutomata.Text))
            {
                lbl_OperatorCreateAutomataMessage.Content += "No first automata selected!\n";
                errors = true;
            }
            if (String.IsNullOrEmpty(cmb_SecondAutomata.Text) && (cmb_Operator.Text.ToUpper() != "NOT"))
            {
                lbl_OperatorCreateAutomataMessage.Content += "No second automata selected!\n";
                errors = true;
            }

            if(!String.IsNullOrEmpty(cmb_FirstAutomata.Text) && (!String.IsNullOrEmpty(cmb_SecondAutomata.Text) && (cmb_Operator.Text.ToUpper() != "NOT")))
            {
                if(cmb_FirstAutomata.Text == cmb_SecondAutomata.Text)
                {
                    lbl_OperatorCreateAutomataMessage.Content += "Can't execute operation on the same automata!\n";
                    errors = true;
                }

                Automata firstAutomata = this.createdAutomata[cmb_FirstAutomata.Text];
                Automata secondAutomata = this.createdAutomata[cmb_SecondAutomata.Text];

                if(!firstAutomata.IsDFA || !secondAutomata.IsDFA)
                {
                    lbl_OperatorCreateAutomataMessage.Content += "Can't execute operation on NDFA automata!\n";
                    errors = true;
                }
            }

            if (!errors)
            {
                switch (cmb_Operator.Text.ToUpper())
                {
                    case "AND":
                        {
                            cmb_SecondAutomata.Visibility = Visibility.Visible;
                            lbl_SecondAutomata.Visibility = Visibility.Visible;

                            Automata firstAutomata = this.createdAutomata[cmb_FirstAutomata.Text];
                            Automata secondAutomata = this.createdAutomata[cmb_SecondAutomata.Text];

                            Automata andAutomata = Automata.And(firstAutomata, secondAutomata);

                            this.createdAutomata.Add(txb_OperatorsCreateAutomataName.Text, andAutomata);
                            this.createdAutomata[txb_OperatorsCreateAutomataName.Text].Validate();

                            break;
                        }
                    case "OR":
                        {
                            cmb_SecondAutomata.Visibility = Visibility.Visible;
                            lbl_SecondAutomata.Visibility = Visibility.Visible;

                            Automata firstAutomata = this.createdAutomata[cmb_FirstAutomata.Text];
                            Automata secondAutomata = this.createdAutomata[cmb_SecondAutomata.Text];

                            Automata orAutomata = Automata.Or(firstAutomata, secondAutomata);

                            this.createdAutomata.Add(txb_OperatorsCreateAutomataName.Text, orAutomata);
                            this.createdAutomata[txb_OperatorsCreateAutomataName.Text].Validate();

                            break;
                        }
                    case "NOT":
                        {
                            cmb_SecondAutomata.Visibility = Visibility.Hidden;
                            lbl_SecondAutomata.Visibility = Visibility.Hidden;

                            Automata automata = this.createdAutomata[cmb_FirstAutomata.Text];

                            Automata notAutomata = Automata.Not(automata);

                            this.createdAutomata.Add(txb_OperatorsCreateAutomataName.Text, notAutomata);
                            this.createdAutomata[txb_OperatorsCreateAutomataName.Text].Validate();

                            break;
                        }
                }

                txb_OperatorsCreateAutomataName.Text = "";
                cmb_Operator.SelectedIndex = 0;

                lbl_OperatorCreateAutomataMessage.Content = "Automata added!\n";
                lbl_OperatorCreateAutomataMessage.Foreground = Brushes.Green;
                lbl_OperatorCreateAutomataMessage.Visibility = Visibility.Visible;

                UpdateComboBoxes();
            }
            else
            {
                lbl_OperatorCreateAutomataMessage.Foreground = Brushes.Red;
                lbl_OperatorCreateAutomataMessage.Visibility = Visibility.Visible;
            }
        }

        private void btn_ConversionCreateAutomata_Click(object sender, RoutedEventArgs e)
        {
            lbl_ConversionCreateAutomataMessage.Visibility = Visibility.Hidden;
            lbl_ConversionCreateAutomataMessage.Content = "";

            bool errors = false;
            if (String.IsNullOrEmpty(txb_ConversionName.Text))
            {
                lbl_ConversionCreateAutomataMessage.Content += "Name field is empty!\n";
                errors = true;
            }
            else if (this.createdAutomata.ContainsKey(txb_ConversionName.Text))
            {
                lbl_ConversionCreateAutomataMessage.Content += "Name already used!\n";
                errors = true;
            }

            if (!errors)
            {
                Automata automata = this.createdAutomata[cmb_ConversionAutomata.Text];
                if(!automata.IsDFA && cmb_Conversion.Text.ToUpper().StartsWith("MINIMIZE"))
                {
                    lbl_ConversionCreateAutomataMessage.Content += "Automata is not a DFA and cannot be minimized! (Convert to DFA first)\n";
                    lbl_ConversionCreateAutomataMessage.Foreground = Brushes.Red;
                    lbl_ConversionCreateAutomataMessage.Visibility = Visibility.Visible;
                }
                else if(automata.IsDFA && cmb_Conversion.Text.ToUpper() == "TODFA")
                {
                    lbl_ConversionCreateAutomataMessage.Content += "Automata is already a DFA!\n";
                    lbl_ConversionCreateAutomataMessage.Foreground = Brushes.Red;
                    lbl_ConversionCreateAutomataMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    if (cmb_Conversion.Text.ToUpper() == "TODFA")
                    {
                        Automata toDFAAutomata = automata.ConvertedToDFA();

                        this.createdAutomata.Add(txb_ConversionName.Text, toDFAAutomata);
                        this.createdAutomata[txb_ConversionName.Text].Validate();
                    }
                    else if(cmb_Conversion.Text.ToUpper() == "MINIMIZE_TABLE")
                    {
                        Automata minimizedAutomata = automata.Minimized();

                        this.createdAutomata.Add(txb_ConversionName.Text, minimizedAutomata);
                        this.createdAutomata[txb_ConversionName.Text].Validate();
                    }
                    else if (cmb_Conversion.Text.ToUpper() == "MINIMIZE_REVERSE")
                    {
                        Automata minimizedAutomata = automata.Minimized2();

                        this.createdAutomata.Add(txb_ConversionName.Text, minimizedAutomata);
                        this.createdAutomata[txb_ConversionName.Text].Validate();
                    }

                    txb_ConversionName.Text = "";
                    cmb_Conversion.SelectedIndex = 0;
                    cmb_ConversionAutomata.SelectedIndex = 0;

                    lbl_ConversionCreateAutomataMessage.Content = "Automata added!\n";
                    lbl_ConversionCreateAutomataMessage.Foreground = Brushes.Green;
                    lbl_ConversionCreateAutomataMessage.Visibility = Visibility.Visible;

                    UpdateComboBoxes();
                }
            }
            else
            {
                lbl_ConversionCreateAutomataMessage.Foreground = Brushes.Red;
                lbl_ConversionCreateAutomataMessage.Visibility = Visibility.Visible;
            }
        }

        private void btn_ParseRegex_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Attempting to parse regex");
            Automata automata = RegexTranslator.TranslateRegex(txb_ParseRegexInput.Text);
            createdAutomata.Add(txb_ParseRegexName.Text, automata);
            UpdateComboBoxes();
        }

        private static int imagecounter = 0;
        private void btn_VisualizeAutomata_Click(object sender, RoutedEventArgs e) 
        {
            Automata automata = this.createdAutomata[cmb_VisualizeAutomata.Text];
            var imageblock = stk_GraphvizPanel.Children.OfType<Image>().FirstOrDefault();
            var uriSource = new Uri(GraphViz.GraphVizGenerator.generateFiles(automata, "automata" + imagecounter++.ToString())); //To improve: using automata names, but no spaces
            var image = new BitmapImage(uriSource);
            imageblock.Source = image;

        }

        private Tuple<bool, List<char>> GetSymbolsFromString(string input)
        {
            if(input.Length == 0 || (input.Length % 2) == 0)
                return new Tuple<bool, List<char>>(false, new List<char>());

            List<char> symbols = new List<char>();
            for (int i = 0; i < input.Length; i++)
            {
                bool isEven = ((i % 2) == 0);

                if(isEven)
                {
                    if (input[i] == ',')
                        return new Tuple<bool, List<char>>(false, new List<char>());
                    else
                        symbols.Add(input[i]);
                }
                else if(input[i] != ',')
                    return new Tuple<bool, List<char>>(false, new List<char>());
            }

            return new Tuple<bool, List<char>>(true, symbols);
        }

        private Tuple<string, Automata> GetAutomataFromTypeString(string input, List<char> symbols)
        {
            switch(cmb_CreateAutomataTypes.Text.ToUpper())
            {
                case "STARTSWITH":
                    {
                        return new Tuple<string, Automata>("", AutomataBuilder.StartsWithDFA(input, symbols));
                    }
                case "ENDSWITH":
                    {
                        return new Tuple<string, Automata>("", AutomataBuilder.EndsWithDFA(input, symbols));
                    }
                case "CONTAINS":
                    {
                        return new Tuple<string, Automata>("", AutomataBuilder.ContainsDFA(input, symbols));
                    }
                case "EVENCHARACTERS":
                    {
                        if (input.Length > 1)
                            return new Tuple<string, Automata>("Only one character can be used as input for this automata type!", null);
                        return new Tuple<string, Automata>("", TestAutomata.EvenNumberOfCharacters(input[0], symbols));
                    }
                case "UNEVENCHARACTERS":
                    {
                        if (input.Length > 1)
                            return new Tuple<string, Automata>("Only one character can be used as input for this automata type!", null);
                        return new Tuple<string, Automata>("", TestAutomata.UnevenNumberOfCharacters(input[0], symbols));
                    }
            }

            return null;
        }

        #region DROPDOWNCLOSED_EVENTS

        private void cmb_Automata_DropDownClosed(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmb_Automata.Text))
            {
                Automata selectedAutomata = this.createdAutomata[cmb_Automata.Text];
                if (selectedAutomata.IsDFA)
                {
                    lbl_AutomataTypeMessage.Content = "DFA";
                    lbl_AutomataTypeMessage.Foreground = Brushes.Green;
                }
                else
                {
                    lbl_AutomataTypeMessage.Content = "NDFA";
                    lbl_AutomataTypeMessage.Foreground = Brushes.Red;
                }
            }
        }

        private void cmb_FirstAutomata_DropDownClosed(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmb_FirstAutomata.Text))
            {
                Automata selectedAutomata = this.createdAutomata[cmb_FirstAutomata.Text];
                if (selectedAutomata.IsDFA)
                {
                    lbl_FirstAutomataTypeMessage.Content = "DFA";
                    lbl_FirstAutomataTypeMessage.Foreground = Brushes.Green;
                }
                else
                {
                    lbl_FirstAutomataTypeMessage.Content = "NDFA";
                    lbl_FirstAutomataTypeMessage.Foreground = Brushes.Red;
                }
            }
        }

        private void cmb_SecondAutomata_DropDownClosed(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmb_FirstAutomata.Text))
            {
                Automata selectedAutomata = this.createdAutomata[cmb_FirstAutomata.Text];
                if (selectedAutomata.IsDFA)
                {
                    lbl_SecondAutomataTypeMessage.Content = "DFA";
                    lbl_SecondAutomataTypeMessage.Foreground = Brushes.Green;
                }
                else
                {
                    lbl_SecondAutomataTypeMessage.Content = "NDFA";
                    lbl_SecondAutomataTypeMessage.Foreground = Brushes.Red;
                }
            }
        }

        private void cmb_Operator_DropDownClosed(object sender, EventArgs e)
        {
            switch (cmb_Operator.Text.ToUpper())
            {
                case "AND":
                    {
                        cmb_SecondAutomata.Visibility = Visibility.Visible;
                        lbl_SecondAutomata.Visibility = Visibility.Visible;

                        break;
                    }
                case "OR":
                    {
                        cmb_SecondAutomata.Visibility = Visibility.Visible;
                        lbl_SecondAutomata.Visibility = Visibility.Visible;

                        break;
                    }
                case "NOT":
                    {
                        cmb_SecondAutomata.Visibility = Visibility.Hidden;
                        lbl_SecondAutomata.Visibility = Visibility.Hidden;

                        break;
                    }
            }
        }

        private void cmb_VisualizeAutomata_DropDownClosed(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmb_VisualizeAutomata.Text))
            {
                Automata selectedAutomata = this.createdAutomata[cmb_VisualizeAutomata.Text];
                if (selectedAutomata.IsDFA)
                {
                    lbl_VisualizeAutomataMessage.Content = "DFA";
                    lbl_VisualizeAutomataMessage.Foreground = Brushes.Green;
                }
                else
                {
                    lbl_VisualizeAutomataMessage.Content = "NDFA";
                    lbl_VisualizeAutomataMessage.Foreground = Brushes.Red;
                }
            }
        }

        #endregion
    }
}

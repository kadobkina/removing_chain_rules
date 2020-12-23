using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;

namespace ta13
{
    class Program
    {
 
        const string path = "grammar.txt";
        static string[] lines = new string[count_of_row(path)]; // строки грамматики из файла
        static string[] nonterm = new string[count_of_row(path)]; // список нетерминалов
        static string[] chain = new string[count_of_row(path)]; // список цепей
        static string[] result_grammar = new string[count_of_row(path)]; //грамматика без цепных продукций


        public static int count_of_row(string p)
        {
            int count = 0;
            using (StreamReader sr = new StreamReader(p))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    count++;
                }
            }
            return count;
        }

        public static void add_chain(int i)
        {
            string line = lines[i];
            string[] split_line = line.Split(' ');
            //result_grammar[i] = split_line[0] + " ";
            foreach (string c in split_line)
            {
                if (nonterm.Contains(c))
                {
                    chain[i] += c + " ";
                    for (int j = 0; j < lines.Length; j++)
                    {
                        string line2 = lines[j];
                        if ((""+line2[0]).CompareTo(c) == 0)
                        {
                            add_chain(j);
                            chain[i] += chain[j];
                        }

                    }
                }
                else
                {
                    result_grammar[i] += c + " ";
                }
            }

            string[] split_chain = chain[i].Split(' ');
            chain[i] = null;
            var new_split_chain = split_chain.Distinct();
            foreach (var c in new_split_chain)
                chain[i] += c + " ";
        }


        static void Main(string[] args)
        {

            WriteLine("Исходная грамматика в файле ..bin/Debug/grammar.txt  >>>");

            // считываем файл построчно
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    int ind = 0;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        WriteLine(line);
                        lines[ind] = line;
                        nonterm[ind] = ""+line[0];
                        chain[ind] = line[0] + " ";
                        ind++;
                    }
                }
            } catch (Exception e)
            {
                WriteLine(e.Message);
            }

            WriteLine("Грамматика после удаления цепных продукций  >>>");

            add_chain(0);

            /*            foreach (var c in chain)      // проверка циклов
                            WriteLine(c);*/


          /*    for (int i = 0; i < chain.GetLength(0); i++)
            {
                for (int j = 1; j < chain.GetLength(1); j++)
                {
                    string el = chain[i, j];
                    foreach (var gram in result_grammar)
                    {
                        if ((gram[0] + "").CompareTo(el) == 0)
                        {
                            var gr = gram.Split(' ');
                            for (int k = 1; k < gr.Length; k++)
                            {
                                result_grammar[i] += gr[k] + " ";
                            }
                        }
                    }

                }
            }*/

            for (int i = 0; i < chain.Length; i++)
            {
                var split_chain = chain[i].Split(' ');
                for (int j = 1; j < split_chain.Length; j++)
                {
                    string el = split_chain[j];
                    foreach (var gram in result_grammar)
                    {
                        if ((gram[0] + "").CompareTo(el) == 0)
                        {
                            var gr = gram.Split(' ');
                            for (int k = 1; k < gr.Length; k++)
                            {
                                result_grammar[i] += gr[k] + " ";
                            }
                        }
                    }
                }
            }


            foreach (var g in result_grammar)
            {
                WriteLine(g);
            }

        }
    }
}

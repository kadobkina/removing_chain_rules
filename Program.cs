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
        static void Main(string[] args)
        {
            string path = "grammar.txt";
            int ind = 0;
            bool flag = true;

            WriteLine("Исходная грамматика в файле ..bin/Debug/grammar.txt  >>>");

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ind++;
                }
            }
            string[] lines = new string[ind]; // строки грамматики из файла
            string[] nonterm = new string[ind]; // список нетерминалов
            string[,] chain = new string[ind, ind]; // список цепей

            string[] result_grammar = new string[ind]; //грамматика без цепных продукций

            // считываем файл построчно
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    ind = 0;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        WriteLine(line);
                        lines[ind] = line;
                        nonterm[ind] = ""+line[0];
                        chain[ind, 0] = "" + line[0];
                        ind++;
                    }
                }
            } catch (Exception e)
            {
                WriteLine(e.Message);
            }

            WriteLine("Грамматика после удаления цепных продукций  >>>");


            for (int i = 0; i < lines.Length; i++)
            {
                string[] split_line = null;
                string line = lines[i];
                split_line = line.Split(' ');
                //result_grammar[i] = split_line[0] + " ";
                int ii = 1;
                foreach (string c in split_line)
                {
                    if (nonterm.Contains(c))
                    {
                        chain[i, ii] = c;
                        ii++;
                        flag = false;
                    }
                    else
                    {
                        result_grammar[i] += c + " ";
                    }
                }
            }

            for (int i = 0; i < chain.GetLength(0); i++)
            {
                for (int j = 1; j < chain.GetLength(1); j++)
                {
                    string el = chain[i, j];
                    foreach (var gram in result_grammar)
                    {
                        if ((gram[0]+"").CompareTo(el) == 0)
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

            /*            for (int i = 1; i < chain.GetLength(0); i++)
                        {
                            for (int j = 1; j < chain.GetLength(1); j++)
                            {
                                result_grammar[i] += chain[i, j] + " ";
                            }

                        }*/

            foreach (var g in result_grammar)
            {
                WriteLine(g);
            }

        }
    }
}

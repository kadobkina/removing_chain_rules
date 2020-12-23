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
        static int count_gr = count_of_row(path);
        static string[] lines = new string[count_gr]; // грамматика с цепными продукциями из файла
        static string[] nonterm = new string[count_gr]; // список нетерминалов
        static string[] chain = new string[count_gr]; // список цепных продукций
        static string[] result_grammar = new string[count_gr]; //грамматика без цепных продукций

        // количество строк в грамматике
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

        // рекурсивный поиск цепных продукций
        public static void add_chain(int i)
        {
            string line = lines[i];
            string[] split_line = line.Split(' ');
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

            // считываем файл построчно, выписываем нетерминалы
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


            // находим цепные продукции, начиная с начальной
            add_chain(0);

            /*            foreach (var c in chain)      // проверка циклов
                            WriteLine(c);*/

            // удаляем цепные продукции из итоговой грамматики, добавляя варианты переходов
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


            // вывод итоговой грамматики без цепных продукций
            WriteLine("\nГрамматика после удаления цепных продукций  >>>");
            foreach (var g in result_grammar)
            {
                WriteLine(g);
            }

        }
    }
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;

namespace Workbook_Problem
{
    class Result
    {

        public static int workbook(int n, int k, List<int> arr)
        {
            int special_count = 0;
            int page = 1;
            int problems_per_page = 0;
            bool newChapter = false;
            List<ProblemList> problem_list = new List<ProblemList>();

            // assembles problem_list
            for (int i = 0; i < arr.Count; i++)
            {
                for (int j = 0; j < arr.ElementAt(i); j++)
                {
                    problem_list.Add(new ProblemList(i + 1, j + 1));
                }
            }

            for (int x = 0; x < arr.Count; x++)
            {
                newChapter = false;
                for (int y = 0; y < problem_list.Count; y++)
                {
                    if ((problem_list.ElementAt(y).chapter > (x + 1)))
                    {
                        problems_per_page = 0;
                        page++;
                        newChapter = true;
                        break;
                    }
                    else if ((problem_list.ElementAt(y).chapter == (x + 1))
                    && (problems_per_page < k))
                    {
                        problem_list.ElementAt(y).page = page;
                        problems_per_page++;
                    }
                    else if ((problem_list.ElementAt(y).chapter == (x + 1))
                    && (problems_per_page >= k))
                    {
                        page++;
                        problems_per_page = 1;
                        problem_list.ElementAt(y).page = page;
                    }
                }
                if (newChapter)
                {
                    continue;
                }
            }

            foreach (ProblemList pl in problem_list)
            {
                if ((pl.problem == pl.page))
                {
                    special_count++;
                }
            }

            return special_count;
        }
    }

    class ProblemList
    {
        public int chapter { get; set; }
        public int problem { get; set; }
        public int page { get; set; }

        public ProblemList(int c, int p)
        {
            chapter = c;
            problem = p;
        }
    }

    class Solution
    {
        public static void Main(string[] args)
        {
//            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int n = Convert.ToInt32(firstMultipleInput[0]);

            int k = Convert.ToInt32(firstMultipleInput[1]);

            List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

            int result = Result.workbook(n, k, arr);
            Console.WriteLine(result);

            //            textWriter.WriteLine(result);

            //            textWriter.Flush();
            //            textWriter.Close();
        }
    }

}

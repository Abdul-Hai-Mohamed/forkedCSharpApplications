// C# program to illustrate the
// Capacity Property of List<T>
using System;
using System.Collections.Generic;

class Geeks
{

	        static void Main(string[] args)
        {

            /// <summary>
            /// Write a program and ask the user to supply a list of comma separated numbers (e.g 5, 1, 9, 2, 10). If the list is 
            /// empty or includes less than 5 numbers, display "Invalid List" and ask the user to re-try; otherwise, display 
            /// the 3 smallest numbers in the list.
            /// 
            /// </summary>
            /// 
            ///


            //My solution

            Console.WriteLine("please enter string of many numbers sperated");
            x();
            void x()
            {

                string str_input = Console.ReadLine();
                Console.WriteLine("You have entered {0}",str_input);
                /////////////////
                string str_pattern = @"[0-9]";
                MatchCollection matches = Regex.Matches(str_input, str_pattern);
                List<int> matched_list = new List<int>();
                //////////////////
                for (int i = 0; i <= matches.Count-1; i++)
                {
                    matched_list.Add(Int32.Parse(matches[i].ToString()));
                }
                if (matched_list.Count ==0) 
                {
                    Console.WriteLine("You have not entered any number , please enter 5 unique numbers"); 
                    x();
                }
                else
                {
                    Console.WriteLine("Your matche List elements");
                    foreach (int i in matched_list) { Console.WriteLine(i); }

                    //////////////////
                    List<int> Unique_matched_list = new List<int>();
                    for (int i = 0; i <= matched_list.Count - 1; i++)
                    {
                        if (Unique_matched_list.Contains(matched_list[i]) == false)
                        {
                            Unique_matched_list.Add(matched_list[i]);
                        }
                    }
                    Console.WriteLine("Your Unique matche List elements");
                    foreach (int i in Unique_matched_list) { Console.WriteLine(i); }
                    //////////////
                    if (Unique_matched_list.Count < 5) 
                    {
                        Console.WriteLine("unique numbers less than 5 , please enter 5 unique numbers");
                        x();
                    }
                    else if (Unique_matched_list.Count > 5)
                    {
                        Console.WriteLine("unique numbers greater than 5 , So here is the first unique 5 numbers");
                        for (int i = 0; i <= 4; i++) { Console.WriteLine(Unique_matched_list[i]); }
                    }
                }

            }
            







            ////////////////////
            ///Mosh solution
                
            
            /*
             
            string[] elements;
                while (true)
                {
                    Console.Write("Enter a list of comma-separated numbers: ");
                    var input = Console.ReadLine();

                    if (!String.IsNullOrWhiteSpace(input))
                    {
                        elements = input.Split(',');
                        if (elements.Length >= 5)
                            break;
                    }

                    Console.WriteLine("Invalid List");
                }


                
                var numbers = new List<int>();
                foreach (var number in elements)
                    numbers.Add(Convert.ToInt32(number));

                var smallests = new List<int>();
                while (smallests.Count < 3)
                {
                    // Assume the first number is the smallest
                    var min = numbers[0];
                    foreach (var number in numbers)
                    {
                        if (number < min)
                            min = number;
                    }
                    smallests.Add(min);

                    numbers.Remove(min);
                }

                Console.WriteLine("The 3 smallest numbers are: ");
                foreach (var number in smallests)
                    Console.WriteLine(number);
            

            */

        }

}

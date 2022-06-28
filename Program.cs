using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace test1
{
    class Class_Program

    {

        string brand;
        int price;
        Class_Program(string theBrand, int thePrice)
        {
            brand = theBrand;
            price = thePrice;
        }
        Class_Program(Class_Program S)
        {
            brand = S.brand;
            price = S.price;
        }
        static void Main(string[] args)
        {

            Class_Program p = new Class_Program("bm", 100);
            Console.WriteLine(p);//test1.Class_Program
            Console.WriteLine(p.brand);//bm
            Console.WriteLine(p.price);//100

            // equal p2 by p using copy constructor 
            Class_Program p2 = new Class_Program(p);
            Console.WriteLine(p2.brand);//bm
            Console.WriteLine(p2.price);//100

            Class_Program p3 = p;
            Console.WriteLine(p3.brand);//bm
            Console.WriteLine(p3.price);//100


            p.brand = "mercids";
            Console.WriteLine(p.brand);//mercids
            Console.WriteLine(p.price);//100

            Console.WriteLine(p2.brand);//bm
            Console.WriteLine(p2.price);//100


            Console.WriteLine(p3.brand);//mercids
            Console.WriteLine(p3.price);//100

        }
    }

    struct Struct_Program

    {

        string brand;
        int price;
        Struct_Program(string theBrand, int thePrice)
        {
            brand = theBrand;
            price = thePrice;
        }
        Struct_Program(Struct_Program S)
        {
            brand = S.brand;
            price = S.price;
        }
        static void Main(string[] args)
        {


            Struct_Program p = new Struct_Program("bm",100);
            Console.WriteLine(p);//test1.Struct_Program
            Console.WriteLine(p.brand);//bm
            Console.WriteLine(p.price);//100

            // equal p2 by p using copy constructor 
            Struct_Program p2 = new Struct_Program(p);
            Console.WriteLine(p2.brand);//bm
            Console.WriteLine(p2.price);//100

            Struct_Program p3 = p;
            Console.WriteLine(p3.brand);//bm
            Console.WriteLine(p3.price);//100


            p.brand = "mercids";
            Console.WriteLine(p.brand);//mercids
            Console.WriteLine(p.price);//100

            Console.WriteLine(p2.brand);//bm
            Console.WriteLine(p2.price);//100


            Console.WriteLine(p3.brand);//bm
            Console.WriteLine(p3.price);//100

        }
    }
}

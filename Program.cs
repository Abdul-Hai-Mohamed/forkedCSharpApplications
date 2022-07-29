using System;
namespace MyNameSpace
{
    class A
    {


    }
    class B : A
    {
     
        static public B StaticMethodOfB()
        {
            B bb = new B();
            return bb;
        }
    }
    class Program
    {
        static void Main()
        {
            Console.WriteLine(typeof(Int32));                        //System.Int32
            Console.WriteLine(10.GetType());                         //System.Int32
            Console.WriteLine(  (  typeof(Int32)  ).GetType());      //System.RuntimeType
            Console.WriteLine(  (  10.GetType()   ).GetType());      //System.RuntimeType


            Console.WriteLine();

            B b = new B();
            Console.WriteLine(typeof(B));       //MyNameSpace.B
            Console.WriteLine(b);               //MyNameSpace.B
            Console.WriteLine(b.GetType());     //MyNameSpace.B

            Console.WriteLine(  (  typeof(B)      ).GetType());     //System.RuntimeType
            Console.WriteLine(  (  b.GetType()    ).GetType());      //System.RuntimeType



            Console.WriteLine();



            A a1 = new A();
            Console.WriteLine(typeof(A));       //MyNameSpace.A
            Console.WriteLine(a1);              //MyNameSpace.A
            Console.WriteLine(a1.GetType());    //MyNameSpace.A
            Console.WriteLine(  (  typeof(A)      ).GetType());     //System.RuntimeType
            Console.WriteLine(  (  a1.GetType()   ).GetType());      //System.RuntimeType


            Console.WriteLine();

            //انشاء اوبجكتس من البيز بالرفرنس لاوبجكتس الموروث
            //create object from base that reference to object of inheritor  


            //inheritor object created by the constructor
            A a2 = new B();
            Console.WriteLine(typeof(A));       //MyNameSpace.A
            Console.WriteLine(a2);              //MyNameSpace.B
            Console.WriteLine(a2.GetType());    //MyNameSpace.B
            Console.WriteLine(  (  typeof(A)     ).GetType());     //System.RuntimeType
            Console.WriteLine(  (  a2.GetType()  ).GetType());      //System.RuntimeType
            Console.WriteLine();
            
            //inheritor object created by the returned value of method
            A a3 = B.StaticMethodOfB();
            Console.WriteLine(typeof(A));       //MyNameSpace.A
            Console.WriteLine(a3);              //MyNameSpace.B
            Console.WriteLine(a3.GetType());    //MyNameSpace.B
            Console.WriteLine(  (  typeof(A)     ).GetType());     //System.RuntimeType
            Console.WriteLine(  (  a3.GetType()  ).GetType());      //System.RuntimeType





        }
    }
}

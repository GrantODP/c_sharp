using System;

namespace  HelloWorld.Foo
{
   interface IMyFoo {

    public void  PrintFoo();
   }

    class FooImpl : IMyFoo
    {
        public FooImpl()
        {
        }

        public void PrintFoo() {
            Console.WriteLine("I printed Foo");
        }
    }
}
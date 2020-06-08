using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5WithCoreDI.Services
{
    public class HelloService : IHelloService
    {
        public string SayHello()
        {
            return "Hello dongsu.dev";
        }
    }
}
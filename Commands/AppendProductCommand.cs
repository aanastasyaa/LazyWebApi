using LazyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazyWebApi.Commands
{
    public class AppendProductCommand
    {
        public Product Product { get; set; }
    }
}

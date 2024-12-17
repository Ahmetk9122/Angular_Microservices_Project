using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todos.WebAPI.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Work { get; set; } = default!;
    }
}
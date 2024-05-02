using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ToDoList
    {
        public int Id {get; set;}

        public string? Description {get; set;}

        public bool IsTaskDone {get; set;}
    }
}
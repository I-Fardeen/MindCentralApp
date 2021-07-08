using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesWebApp.Models
{
    public class Riddles
    {
        public int Id { get; set; }
        public string RiddleQuestion { get; set; }
        public string RiddleAnswer { get; set; }
        public Riddles()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TP2_scrutin
{
    public class Candidat
    {
        public Candidat(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string DisplayFirstAndLastName()
        {
            return FirstName + ' ' + LastName;
        }
    }
}

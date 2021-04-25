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

        // Redéfinition de Equals et GetHashCode pour effectuer des comparaison
        public override bool Equals(object obj)
        {
            var key = obj as Candidat;

            if (key == null)
                return false;

            return LastName.Equals(key.LastName) &&
                   FirstName.Equals(key.FirstName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = hash * 16777619 ^ LastName.GetHashCode();
                hash = hash * 16777619 ^ FirstName.GetHashCode();
                return hash;
            }
        }
    }
}

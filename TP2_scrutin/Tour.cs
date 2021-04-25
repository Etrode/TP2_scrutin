using System;
using System.Collections.Generic;
using System.Text;

namespace TP2_scrutin
{
    public class Tour
    {
        public Tour()
        {
            this.closure = false;
            this.CandidatVotes = new Dictionary<Candidat, int>();
        }
        // Nombre de voix par candidat
        public Dictionary<Candidat, int> CandidatVotes { get; }

        // Cloture de ce tour de scrutin
        private bool closure;
        public bool Closure
        { 
            get => closure; 
            set
            {
                closure = value;
                if(closure)
                {
                    // Si le scrutin est fermé, alors on affiche le nombre de votes pour chaque candidat et le pourcentage correspondant.
                    double total = 0;
                    foreach (KeyValuePair<Candidat, int> currentCandidatVotes in this.CandidatVotes)
                    {
                        total += currentCandidatVotes.Value;
                    }

                    foreach (KeyValuePair<Candidat, int> currentCandidatVotes in CandidatVotes)
                    {
                        double percent = 0.0;
                        if (total != 0)
                        {
                            percent = (currentCandidatVotes.Value / total) * 100;
                        }
                        else
                        {
                            percent = 0.0;
                        }
                        Console.WriteLine("Candidat = " + currentCandidatVotes.Key + " | Votes = " + currentCandidatVotes.Value + " | pourcentage = " + Math.Round(percent, 2));                  
                    }
                }
                    
            }
        }
    }
}


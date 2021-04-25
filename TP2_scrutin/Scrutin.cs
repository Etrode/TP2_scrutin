using System;
using System.Collections.Generic;
using System.Linq;

namespace TP2_scrutin
{
    public class Scrutin
    {
        public Scrutin()
        {
            this.Tour1 = new Tour();
        }

        // Tours 1 du scrutin avec les voix par candidat et clôture du tour du scrutin
        public Tour Tour1 { get; set; }

        // Tours 2 du scrutin avec les voix par candidat et clôture du tour du scrutin
        public Tour Tour2 { get; set; }

        public string Calculate()
        {
            if (Tour1.Closure)
            {
                double total = 0;
                // Candidat avec son pourcentage de votes sur le total de votes
                Dictionary<Candidat, double> candidatPercentVotes = new Dictionary<Candidat, double>();
                // Candidat gagnant
                Candidat winner = null;

                foreach (KeyValuePair<Candidat, int> currentCandidatVotes in Tour1.CandidatVotes)
                {
                    total += currentCandidatVotes.Value;
                }

                foreach (KeyValuePair<Candidat, int> currentCandidatVotes in Tour1.CandidatVotes)
                {
                    if(total != 0)
                    {
                        double percent = (currentCandidatVotes.Value / total) * 100;
                        candidatPercentVotes.Add(currentCandidatVotes.Key, Math.Round(percent,2));

                        if (! (percent <= 50.0))
                        {
                            // Gagnant
                            winner = currentCandidatVotes.Key;
                        }

                    } else
                    {
                        candidatPercentVotes.Add(currentCandidatVotes.Key, 0.0);
                    }
                }

                if (winner != null)
                {
                    return winner.DisplayFirstAndLastName();
                }
                // Pas de gagnant, donc second tour avec les 2 meilleurs candidats
                Candidat candidat1 = null;
                Candidat candidat2 = null;
                // Tri des votes décroissant
                foreach (KeyValuePair<Candidat, int> currentCandidatVotes in Tour1.CandidatVotes.OrderByDescending(key => key.Value))
                {
                    if(candidat1 == null)
                    {
                        candidat1 = currentCandidatVotes.Key;
                    }
                    else if(candidat2 == null)
                    {
                        candidat2 = currentCandidatVotes.Key;
                    }
                    else if(candidat1 != null && candidat2 != null)
                    {
                        break;
                    }
                }

                if(candidat1 == null || candidat2 == null)
                {
                    return "There must be at least 2 candidates";
                }

                // Second tour
                if (Tour2 != null && Tour2.Closure)
                {
                    // return "à coder";
                }
                else
                {
                    return "No winner yet, the second tour is not over";
                }
            }
            else
            {
                return "The first tour is not closed";
            }
            return "No winner";
        }

    }
}

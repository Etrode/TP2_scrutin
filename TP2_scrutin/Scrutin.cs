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
                Dictionary<Candidat, double> candidatPercentVotesTour1 = new Dictionary<Candidat, double>();
                // Candidat gagnant
                Candidat winner = null;

                foreach (KeyValuePair<Candidat, int> currentCandidatVotes in Tour1.CandidatVotes)
                {
                    // Les bulletins blancs n’entrent pas en compte pour la détermination des suffrages
                    if ( !currentCandidatVotes.Key.DisplayFirstAndLastName().Equals("Blank Votes"))
                        total += currentCandidatVotes.Value;
                }

                foreach (KeyValuePair<Candidat, int> currentCandidatVotes in Tour1.CandidatVotes)
                {
                    if (!currentCandidatVotes.Key.DisplayFirstAndLastName().Equals("Blank Votes"))
                    {
                        if (total != 0)
                        {
                            double percent = (currentCandidatVotes.Value / total) * 100;
                            candidatPercentVotesTour1.Add(currentCandidatVotes.Key, Math.Round(percent, 2));

                            if (!(percent <= 50.0))
                            {
                                // Gagnant
                                winner = currentCandidatVotes.Key;
                            }

                        }
                        else
                        {
                            candidatPercentVotesTour1.Add(currentCandidatVotes.Key, 0.0);
                        }
                    }
                }

                if (winner != null)
                {
                    return winner.DisplayFirstAndLastName();
                }
                // Pas de gagnant, donc second tour avec les 2 meilleurs candidats
                Candidat candidat1 = null;
                Candidat candidat2 = null;
                Candidat tempCandidat2 = null;
                // Tri des votes décroissant
                foreach (KeyValuePair<Candidat, int> currentCandidatVotes in Tour1.CandidatVotes.OrderByDescending(key => key.Value))
                {
                    if (!currentCandidatVotes.Key.DisplayFirstAndLastName().Equals("Blank Votes"))
                    {
                        if (candidat1 == null)
                        {
                            candidat1 = currentCandidatVotes.Key;
                        }
                        else if (candidat2 == null)
                        {
                            // Gestion 2ème et 3ème candidat égaux
                            if (tempCandidat2 == null)
                            {
                                tempCandidat2 = currentCandidatVotes.Key;
                            }
                            else if (Tour1.CandidatVotes[tempCandidat2] == currentCandidatVotes.Value)
                            {
                                // 3ème candidat existant étant égal en votes avec le deuxième meilleurs candidat
                                // Le premier tour est alors annulé, et doit être rejoué.
                                return "The first tour must be replayed because the second and the 3rd best candidate have the same number of votes";
                            }
                            else
                            {
                                // Pas d'égalité entre le deuxième et le troisième meilleurs candidat
                                candidat2 = tempCandidat2;
                                break;
                            }
                        }
                        else if (candidat1 != null && candidat2 != null)
                        {
                            break;
                        }
                    }
                }

                if (candidat2 == null && tempCandidat2 != null)
                    candidat2 = tempCandidat2;

                if (candidat1 == null || candidat2 == null)
                    return "There must be at least 2 candidates appart from Blank Votes";

                // Second tour
                if (Tour2 != null && Tour2.Closure)
                {
                    double total2 = 0;
                    // Candidat avec son pourcentage de votes sur le total de votes
                    Dictionary<Candidat, double> candidatPercentVotesTour2 = new Dictionary<Candidat, double>();

                    // Vérifications candidats
                    if(!(Tour2.CandidatVotes.ContainsKey(candidat1)) || !(Tour2.CandidatVotes.ContainsKey(candidat2)))
                        return "A candidate from the second tour is not part of the top 2 of the first tour";

                    int cpt = 0;
                    foreach (KeyValuePair<Candidat, int> currentCandidatVotes in Tour2.CandidatVotes)
                    {
                        if (!currentCandidatVotes.Key.DisplayFirstAndLastName().Equals("Blank Votes"))
                        {
                            cpt++;
                            total2 += currentCandidatVotes.Value;
                        }
                    }
                    if(cpt != 2)
                        return "The second tour can only have 2 candidates";

                    foreach (KeyValuePair<Candidat, int> currentCandidatVotes in Tour2.CandidatVotes)
                    {
                        if (!currentCandidatVotes.Key.DisplayFirstAndLastName().Equals("Blank Votes"))
                        {
                            if (total2 != 0)
                            {
                                double percent = (currentCandidatVotes.Value / total2) * 100;
                                candidatPercentVotesTour2.Add(currentCandidatVotes.Key, Math.Round(percent, 2));
                            }
                            else
                            {
                                candidatPercentVotesTour2.Add(currentCandidatVotes.Key, 0.0);
                            }
                        }
                    }

                    bool first = true;
                    double max = 0.0;
                    foreach (KeyValuePair<Candidat, double> currentCandidatPercentVotes in candidatPercentVotesTour2.OrderByDescending(key => key.Value))
                    {
                        if (!currentCandidatPercentVotes.Key.DisplayFirstAndLastName().Equals("Blank Votes"))
                        {
                            if (first)
                            {
                                winner = currentCandidatPercentVotes.Key;
                                max = currentCandidatPercentVotes.Value;
                                first = false;

                            }
                            else if (max.Equals(currentCandidatPercentVotes.Value))
                            {
                                // Egalité
                                return "No winner";
                            }
                        }
                    }

                    if (winner != null)
                    {
                        return winner.DisplayFirstAndLastName();
                    }
                    else
                    {
                        return "No winner";
                    }
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
        }

    }
}

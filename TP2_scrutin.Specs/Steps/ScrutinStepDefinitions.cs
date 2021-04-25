using TechTalk.SpecFlow;
using FluentAssertions;

namespace TP2_scrutin.Specs.Steps
{
    [Binding]
    public sealed class ScrutinStepDefinitions
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        private readonly Scrutin _scrutin = new Scrutin();

        private string _result;

        public ScrutinStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the following votes on the first tour")]
        public void GivenTheFollowingVotes(Table table)
        {
            Tour tour = new Tour(); // Nouveau tour
            foreach (TableRow row in table.Rows)
            {
                // FirstName
                string firstName = row[0];

                // LastName
                string lastName = row[1];

                // Votes
                int votes = int.Parse(row[2]);

                Candidat candidat = new Candidat(firstName, lastName);
                tour.CandidatVotes.Add(candidat, votes);
            }
            this._scrutin.Tour1 = tour;
        }

        [Given(@"the closing of the first tour is (.*)")]
        public void GivenTheClosingOfTheFirstTourIsTrue(bool closure)
        {
            // Si closure = true, alors affichage des candidats + votes + pourcentage > voir Console.WriteLine dans la classe Tour
            this._scrutin.Tour1.Closure = closure;
        }

        [When(@"results are calculated")]
        public void WhenResultsAreCalculated()
        {
            this._result = this._scrutin.Calculate();
        }



        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(string result)
        {
            this._result.Should().Be(result);
        }
    }
}

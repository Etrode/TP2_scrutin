Feature: Scrutin

@nominal
Scenario: Two candidates and one vote
	Given the following votes on the first tour
    | FirstName | LastName | votes |
    | Monica    | Geller   | 1     |
    | Rachel    | Green    | 0     |
	And the closing of the first tour is true
	When results are calculated
	Then the result should be Monica Geller
	 
@tourUnclosed
Scenario: First tour is unclosed
	Given the following votes on the first tour
    | FirstName | LastName | votes |
    | Monica    | Geller   | 1     |
    | Rachel    | Green    | 0     |
	And the closing of the first tour is false
	When results are calculated
	Then the result should be The first tour is not closed

@noWinnerOnFirstTour
Scenario: No winner on first tour
	Given the following votes on the first tour
    | FirstName | LastName | votes |
    | Monica    | Geller   | 1     |
    | Rachel    | Green    | 0     |
    | Ross      | Geller   | 1     |
	And the closing of the first tour is true
	When results are calculated
	Then the result should be No winner yet, the second tour is not over

@secondTour
Scenario: First and second tour WITH a winner
	Given the following votes on the first tour
    | FirstName | LastName | votes |
    | Monica    | Geller   | 1     |
    | Rachel    | Green    | 0     |
    | Ross      | Geller   | 1     |
	And the closing of the first tour is true
	And the following votes on the second tour
	| FirstName | LastName | votes |
    | Monica    | Geller   | 0     |
    | Ross      | Geller   | 2     |
	And the closing of the second tour is true
	When results are calculated
	Then the result should be Ross Geller

@secondTourWihoutWinner
Scenario: First and second tour WITHOUT a winner
	Given the following votes on the first tour
    | FirstName | LastName | votes |
    | Monica    | Geller   | 1     |
    | Rachel    | Green    | 0     |
    | Ross      | Geller   | 1     |
	And the closing of the first tour is true
	And the following votes on the second tour
	| FirstName | LastName | votes |
    | Monica    | Geller   | 1     |
    | Ross      | Geller   | 1     |
	And the closing of the second tour is true
	When results are calculated
	Then the result should be No winner


@SecondAndThirdCandidateAreEquals
Scenario: The second and third candidate are equals in votes
	Given the following votes on the first tour
    | FirstName | LastName | votes |
    | Monica    | Geller   | 2     |
    | Rachel    | Green    | 1     |
    | Ross      | Geller   | 1     |
	And the closing of the first tour is true
	When results are calculated
	Then the result should be The first tour must be replayed because the second and the 3rd best candidate have the same number of votes


@blankVotes
Scenario: First and second tour with blank votes
	Given the following votes on the first tour
    | FirstName | LastName | votes |
    | Monica    | Geller   | 1     |
    | Rachel    | Green    | 0     |
    | Ross      | Geller   | 1     |
    | Blank     | Votes    | 5     |
	And the closing of the first tour is true
	And the following votes on the second tour
	| FirstName | LastName | votes |
    | Monica    | Geller   | 2     |
    | Ross      | Geller   | 1     |
    | Blank     | Votes    | 4     |
	And the closing of the second tour is true
	When results are calculated
	Then the result should be Monica Geller
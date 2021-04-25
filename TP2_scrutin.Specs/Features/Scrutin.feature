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

#@secondTour
#Scenario: Two candidates and one vote
#	Given the following votes on the first tour
#    | FirstName | LastName | votes |
#    | Monica    | Geller   | 1     |
#    | Rachel    | Green    | 0     |
#    | Ross      | Geller   | 1     |
#	And the closing of the first tour is true
#	And the following votes on the second tour
#	| FirstName | LastName | votes |
#    | Monica    | Geller   | 1     |
#    | Rachel    | Green    | 0     |
#    | Ross      | Geller   | 1     |
#	When results are calculated
#	Then the result should be Monica Geller


### PROJECT EXERCISE 2
Your Product Team has just started the next sprint, working on a new Pharmacy management system.
You pull the next feature story from your team’s backlog, assign it to yourself, and get to work on the following criteria:
*	There is a new React web site
*	The user can select any one pharmacy to update, from a list of 5 existing pharmacies in the database.
*	After editing a pharmacy record the user can save or cancel the changes which then displays the edited record in its standard mode again.
*	Saving persists the changes in the database. 
*	Cancelling ignores and reverts the changes. 
 

 Each Pharmacy record has the following fields:

*	Name
* Address
*	City
*	State
*	Zip
*	Number of Filled Prescriptions (current month to date)
*	Created Date (date pharmacy was added to the system)
*	Updated Date (date pharmacy was last updated in the system)

### SOLUTION
You must create a new React/Typescript Web App solution that contains the following:
*	Leverages existing Pharmacy API Controller that allows the following:
  *	View a Pharmacy By ID
  *	View the entire Pharmacy List – all 5 pharmacies
  *	Update a Pharmacy By ID
*	Leverages your API exercise solution as the API
*	Leverages a reactive state management engine / react hooks (Redux or Effector) and events to trigger and manage data communication between UI components. 
*	Has a series of code commits to a branch for each major component you develop. 
 
BONUS OBJECTIVES
*	Make Wireframes before coding. 
*	Use a dependency injection pattern – leveraging services and initial configuration injection
*	Leverage environment detection and clearly display if in dev or production environment in the view. 
*	Use standard authentication, authorization and/or routing attributes
*	Unit Test coverage (ex: using a Jasmine-like test harness).


### Running the project

You will need to make sure the WebAPI project is already running and you'll want to verify that the port is the same as what is in the `.env.development` file. 

Install the required packages by running `npm install`

To start the project you can run `npm run vite`




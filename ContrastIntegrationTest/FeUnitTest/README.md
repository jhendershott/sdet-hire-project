# How to setup and run
Setup is very simple and easy to run
- npm i
- npm run cypress:open
- click on integration-specs/vulnerability-stats-spec.js

# Project Structure
 * fixtures
    This is meant to contain any static elements, such as locators and response stubs
    - pages will contain
     - const containing object of locators
     - class for any page interactions
    - response-mocks
     - will contain a base json response from requests which can be edited in the tests
  * integration-specs will contain test files themselves
    - Most requests will use environments url unless components are tested individually
 **If possible to move this into the app code and run as unit tests n specific modules thats preferable**
  * plugins is not currently used
  * support
    Currenly used specifically for commands
    - any frequently used code should be put into commands 
  
    

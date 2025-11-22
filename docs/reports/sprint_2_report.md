# Sprint 2 Report
Video Link: https://youtu.be/GM4-06SH5_8
## What's New
* Docker container for backend
* Backend API deployed to web
* Image hosting via Google Cloud and corresponding API
* User authentication via Firebase and corresponding API
* Shopping cart and corresponding API
* Updates to Menu class allowing for editing

## Work Summary (Developer Facing)
We managed to resolve most of the project dependency issues a couple of our team members have been having since Sprint 1 by containerizing our project with all (if not most) of the necessary dependencies using Docker. We were fairly new to working with Docker, but we managed to successfully containerize most of the dependencies needed for our project by just following the documentation on Docker's website and online resources. Additionally, we managed to successfully set up the backend necessary for the user authentication tool that we are using (Firebase). Google does not have any official mobile APIs for integrating Firebase into applications like ours. So instead, we built the backend to be a web API which allows the frontend to successfully connect. This in turn allowed us to get more familiar with working with APIs.

## Unfinished Work
During this sprint, our group made progress towards developing the front-end user authentication system. The core UI components for login and registration screens were implemented; this includes the form fields, basic validation, and routing to the authentication pages.  Also, the initial API request structures were created to support user login and sign-up functionality. 

However, we could not fully complete full integration with the backend authentication service. While the UI framework and request logic are in place we still need to do the following:
* Completing API connection to the backend authentication endpoints
* Implementing error-handling and secure token storage
* Finishing redirect logic after successful login
* Finalizing user session persistence
This issue remained open in this sprint with progress being tracked, and a comment was added explaining that the task required more time than we originally thought. This user authentication task will be moved to the next sprint for completion


## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:
* Image API - https://github.com/JadenBryant/kitchen-compass/issues/36
* Kitchen-facing backend - https://github.com/JadenBryant/kitchen-compass/issues/22
* Shopping cart backend - https://github.com/JadenBryant/kitchen-compass/issues/16
* Menu editing - https://github.com/JadenBryant/kitchen-compass/issues/8
* User authentication API - https://github.com/JadenBryant/kitchen-compass/issues/9
* Deployment of API via Railway - https://github.com/JadenBryant/kitchen-compass/issues/39
* Backend containerization with Docker - https://github.com/JadenBryant/kitchen-compass/issues/38

## Incomplete Issues/User Stories
Here are links to issues we worked on but did not complete in this sprint:
* https://github.com/JadenBryant/kitchen-compass/issues/35 - implementation of the authentication API took longer than expected, thus preventing us from hooking it up to the front end and completing the login feature.

## Code Files for Review
Please review the following code files, which were actively developed during this
sprint, for quality:
* [AuthController.cs](https://github.com/JadenBryant/kitchen-compass/blob/main/kitchen-compass/CustomerBackend/Controllers/AuthController.cs)
* [ImageController.cs](https://github.com/JadenBryant/kitchen-compass/blob/main/kitchen-compass/CustomerBackend/Controllers/ImageController.cs)
* [CartController.cs](https://github.com/JadenBryant/kitchen-compass/blob/main/kitchen-compass/CustomerBackend/Controllers/CartController.cs)
* [MenuController.cs](https://github.com/JadenBryant/kitchen-compass/blob/main/kitchen-compass/CustomerBackend/Controllers/MenuController.cs)

## Retrospective Summary
Here's what went well:
* We were able to finally containerize the appropriate pieces of our project, which improved development conditions several times over because everyone was able to easily spin up a local container of the project rather than having to worry about building it on their own device.
* We were able to convert our backend into a web API and to develop several endpoints, allowing us to more easily interface the backend with their corresponding frontend pieces.
Here's what we'd like to improve:
* Complete the implementation for the backend of the kitchen.
* Implement better error handling in our projects.
Here are changes we plan to implement in the next sprint:
* Implement the front end for user authentication.
* Finish user session persistence.


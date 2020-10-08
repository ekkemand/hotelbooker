# HomeWork

All course projects (there will be several) in single git repo.
https://gitlab.cs.ttu.ee  
Git repo has to be named: **icd0009-2019s**  
Every project in its own directory.  
Do not commit binaries and platform specific metafiles to git. Use correct .gitignore file.  
Combine these as a starter:  
https://raw.githubusercontent.com/github/gitignore/master/VisualStudio.gitignore  
https://raw.githubusercontent.com/JetBrains/resharper-rider-samples/master/.gitignore

## Project requirements for successful defence

DeadLine **When you defend your home project**

### New stuff

Move base projects from your solution to separate solution.
Solution has to be named ee.itcollege.<your-uni-id>. Include new solution in your git project.  
Rename namespaces and project names.  
Publish all the packages to nuget.org  
Use your packages in your homework project.  

Hosting
Deploy your apps into docker containers and deploy/run them from cloud (azure student for example).  

### HW Final requirements (initial)  

Min 10 functional entities - not including trivial m:m in-between tables, identity tables and language string / translation tables.
Layered clean architecture with:

Domain  
DAL  
BLL  
Rest/Web  

and dto mapping between every layer. DAL is using Entity Framework.  
Swagger with XML docs  
API versioning support (and public versioned DTO-s).
Identity support, with RESTful API implementations for registering and login-in (JWT creation).  
Base projects splitted into separate solution - and base packages hosted in Nuget.org  
App hosted in docker somewhere (azure for example).  
Support for i18n - if you also participate in ASP.NET Web Apps course (including in the client).  

And of course - fully functional client app with usable UI/UX. Client app technology is free choice - js, android, etc.  

API endpoints have to be correctly secured - if resources are user specific then only correct user can access and modify these.  

Update your documentation to analyze what/why you did do and what did you finally achieved.  
Explain your app architecture. All the documentation has to be up-to-date with final project (erd schema).  

MVC web controllers and Razor pages for Identity are optional.  
Or move them to separate "Developer" area and protect them with [Authorize[Roles="Developer"]].  

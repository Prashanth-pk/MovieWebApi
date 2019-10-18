# moviewebapi
## Technology Stack Used
    .NetCore 3.0, C# 8.0,EF Core, WebApi 
## Functionalities Yet To Be Added
    More Changes could be added to the current solution like as stated below.
	1: More UnitTesting needs to be added. Created a skeleton now.
	2: Swagger for Api Documentation.
        3: Segregation of Model and Data is currently added with in the Web solution. Model/Entities can be added as a seperate project and 
	   could reference those .dlls to the web so that each layer is independent and segregrated.
        4: Thinking about the architecture,if I had time,I would have created a  sql database and hosted/deployed it on Azure so that
	   it could  be scalable and microservice clustercan horizontally scale rather than using inmemory database.
	5: Currently I have implemented inbuilt logging functionality using Logger but could also add third party logging
	   modules and inject it to the service/controller.

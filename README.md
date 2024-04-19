# EComFunctionAppAPI 

Dominique's Notes

For this project I have added diagrams to show the bigger picture of how this project could look for an e-commerce specfication. 
The idea is there would be one main project which would talk to different projects such as Orders, Customer, Wallet which would contain the logic inside there and we would just call the client projects which include a http call 
as shown in my .client project. This way we could isolate code making it easier to manage and deploy. 
With the database design I have set this up to use one azure located database. However, the design would need more thought on if we would like alternative database for the project or all in one. 

--------------------


Technical Task

We’d like to find out a bit more about how you approach problem solving by completing a technical exercise. You should aim to spend around 2-3 hours on this technical task, but are free to spend as much or little of those 2-3 hours as you wish. Please upload your code sample to GitHub and submit the link. Don’t worry if you don’t complete the task, can’t get the code to compile or have any other issues. We’re most interested in seeing how it is you tackle problem solving, and we’re looking to understand why you’ve made the decisions you have when it comes to your solution.

Your solution will provide the basis for your technical interview where we will explore extending your solution given certain business challenges. If you don’t finish then don’t worry just send over what you’ve got

To be clear that while we practise TDD in some of our teams, unit tests are not required for this task – we will not look at them and they will not be considered when evaluating your task

We recommend watching at least part of this video to get an idea of how we work - https://www.youtube.com/watch?v=UYmTUw5LXwQ 
If you get a chance it would be good to watch it, we don’t expect you to watch the whole thing but there’s a useful section between 15-25 mins that gives an example of the way we work and if you can demonstrate that in your task we would recommend doing so


E-Commerce Endpoint Task

Please create a basic .Net API endpoint that can receive some JSON representing a typical e-commerce order and save it into a SQL database.

This should be achievable within a single API endpoint.

Don’t just place the data transfer object (dto) direct into the database – show how you would expect this to work in an enterprise environment
Consider extra information you might store that you would not trust coming from the front-end client.

Use some basic validation.

We use a guest checkout so part of the order should include some basic customer details

Don’t make this too complicated, a placeholder such as a name will do for the customer details in your database but we’ll leave it up to you how to organise it.

The order should link a customer to their order and the products they have ordered including quantities and cost.

Don’t take into account the need for prices to change over time or anything, just a simple representation of a product and cost will do.

We want to see how you structure a solution in Visual Studio and what projects you use in your solution.

We want to see how you layout your code.

We want to see how you design your database and business logic layers.

Please build the service as you would choose to work in an ideal situation but consider carefully how you encapsulate your business logic.  



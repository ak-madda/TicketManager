CORS Setup: Ensure the backend allows requests from the Angular app. In Program.cs, CORS is already set to allow requests from http://localhost:4200.

Service Calls: The TicketService in Angular uses HttpClient to make API calls to the .NET 8 API. Ensure the API URL is correct ( https://localhost:5001/api/tickets).


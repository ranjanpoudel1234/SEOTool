This is an ASP.Net Core MVC Project. This project renders a UI that allows users to search for keyword and url against google search.

# Project Layout
When debugging, a UI will be rendered that will ask user to enter keywords and url. After clicking search, the backend will take that input and query google search URL and scrap the web search results from google.

# Backend Architecture

Backend uses Onion Architecture that is differentitated into multiple smaller projects within same solution. Presentation Layer, Domain Layer and Infrastructure Layer differentiate the 3 layers.
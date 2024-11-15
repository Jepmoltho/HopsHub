# HopsHub

Add and rate beers to keep track your favourite brews

# Instructions

Github Repository: https://github.com/Jepmoltho/HopsHub
Docker Repository: https://hub.docker.com/repository/docker/jepmoltho/hopshub/general

1. Clone the github repository: git clone https://github.com/Jepmoltho/HopsHub.git
2. Initiliase a container for the DB image: docker run --name hopshub-db -e SA_PASSWORD=YOURSTRONGPASSWORD --network hopshub-network -p 1433:1433 -d jepmoltho/hopshub:sqlserver-latest
3. Change to backend directory: cd HopsHub.Api
4. Create the DB: dotnet database update
5. Initialise the backend: docker run --name hopshub-backend -e DB_PASSWORD=YOURSTRONGPASSWORD --network hopshub-network -p 8080:8080 jepmoltho/hopshub:backend-latest
6. Go to: http://localhost:8080/swagger/index.html

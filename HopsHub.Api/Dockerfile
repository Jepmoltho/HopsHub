# Use the official Microsoft .NET SDK image as a base image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the .csproj and restore any dependencies (via NuGet)
COPY HopsHub.Api.csproj ./
RUN dotnet restore HopsHub.Api.csproj

# Copy the rest of the application source code
COPY . ./

# Publish the app to the /out directory in the container, specifying the .csproj
RUN dotnet publish HopsHub.Api.csproj -c Release -o /out

# Use the official Microsoft .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory in the runtime container
WORKDIR /app

# Copy the published app from the build container
COPY --from=build /out .

# Set default environment variables
# ENV ASPNETCORE_ENVIRONMENT=Development
# ENV ASPNETCORE_HTTP_PORTS=8080
# ENV DB_HOST=hopshub-sqlserver-v.1.1
# ENV DB_USER=sa
# ENV TESTUSER_PASSWORD=userPassw0rd!
# ENV SMTP_SERVER=localhost
# ENV SMTP_PORT=1025
# ENV SENDER_EMAIL=hopshub@dev.com
# ENV SENDER_NAME="HopsHub DEV Server"

# Expose the port the app will run on
EXPOSE 8080

# Define the entry point to run the application
ENTRYPOINT ["dotnet", "HopsHub.Api.dll"]

# Todo: Run dotnet database update from dockerfile (which is a runtime command, so it might be tricky)
# Todo: Can you setup running docker network for both containers (yaml file, to start DB and Backend in right order)
# Todo: CI/CD pipelines
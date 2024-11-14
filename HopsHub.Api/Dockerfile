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

# Expose the port the app will run on
EXPOSE 80

# Define the entry point to run the application
ENTRYPOINT ["dotnet", "HopsHub.Api.dll"]

# Todo: Fix the dotnet port 80 breaking change
# Todo: Setup env variables in docker file so the run command is smaller
# Todo: Add a readme
# Todo: Can you setup running docker network for both containers (DB and Backend)

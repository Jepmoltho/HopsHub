# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy .csproj and restore any dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build the app
COPY . ./
RUN dotnet publish -c Release -o out

# Use a smaller runtime image for the final app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Install the globalization libraries
RUN apt-get update \
    && apt-get install -y locales \
    && locale-gen en_US.UTF-8 \
    && dpkg-reconfigure locales \
    && apt-get clean

# Set the locale
ENV LANG en_US.UTF-8  
ENV LC_ALL en_US.UTF-8  
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

COPY --from=build-env /app/out .

# Expose port 80 to the host
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "HopsHub.Api.dll"]

# Use the official Microsoft SQL Server image as the base image
FROM mcr.microsoft.com/mssql/server:2022-latest

# Set environment variables
ENV ACCEPT_EULA=Y
ENV ENVIRONMENT=Development

# Optional: set default networking and configuration settings

# Expose the default SQL Server port
EXPOSE 1433

# Run SQL Server as the container's main process
CMD [ "/opt/mssql/bin/sqlservr" ]

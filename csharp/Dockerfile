# Run the demo without installing .NET locally:
#   docker build -t desiarchitect-patterns .
#   docker run -it --rm desiarchitect-patterns
#
# Multi-stage: build with the SDK, ship only the runtime.

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restore first (cached unless the csproj changes)
COPY DesiArchitect.DesignPatterns/*.csproj ./DesiArchitect.DesignPatterns/
RUN dotnet restore ./DesiArchitect.DesignPatterns/DesiArchitect.DesignPatterns.csproj

# Then build + publish
COPY DesiArchitect.DesignPatterns/ ./DesiArchitect.DesignPatterns/
RUN dotnet publish ./DesiArchitect.DesignPatterns/DesiArchitect.DesignPatterns.csproj \
    -c Release -o /app

FROM mcr.microsoft.com/dotnet/runtime:10.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# The menu reads from stdin, so run the container with -it.
ENTRYPOINT ["dotnet", "DesiArchitect.DesignPatterns.dll"]

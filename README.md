# Semanix Microservices

This repository contains the source code for Semanix microservices:

- **AccessManagement**: Authentication and authorization service
- **RequestManagement**: Request handling and processing service

## Repository Structure

```
.
├── SemanixBackend/
│   ├── src/
│   │   ├── Services/
│   │   │   ├── AccessManagement/
│   │   │   └── RequestManagement/
│   │   └── intent/
│   ├── docker-compose.yml
│   └── SemanixBackend.sln
└── .github/
    └── workflows/
        ├── microservice-ci-template.yaml
        ├── accessmanagement-cicd.yaml
        └── requestmanagement-cicd.yaml
```

## CI/CD Pipeline

Each microservice has its own CI/CD pipeline that:

1. Builds the Docker image
2. Tags it with the short commit SHA and 'latest'
3. Pushes it to Azure Container Registry

The deployments are handled via the separate deployment repository.

## Development Workflow

1. Clone the repository
2. Build and run locally using docker-compose:
   ```bash
   cd SemanixBackend
   docker-compose up
   ```

3. Make changes to the service code
4. Commit and push to trigger the CI pipeline

## Building Docker Images Locally

To build a specific microservice:

```bash
cd SemanixBackend
docker build -f src/Services/AccessManagement/AccessManagement.Api/Dockerfile -t accessmanagementapi:local .
```

## Testing

Run tests for a specific microservice:

```bash
cd SemanixBackend/src/Services/AccessManagement
dotnet test
```

## Dependencies

- .NET 8.0
- Docker
- Dapr

## Microservice Design Principles

- **Modular**: Each microservice follows Domain-Driven Design
- **Independent**: Services can be developed, deployed, and scaled independently
- **Resilient**: Services handle failures gracefully
- **Observable**: Logging and telemetry are built-in
- **Stateless**: Services don't maintain state between requests

## Adding a New Microservice

1. Create a new directory in `SemanixBackend/src/Services/`
2. Follow the existing structure (Api, Application, Domain, Infrastructure)
3. Create a GitHub workflow file based on the template
4. Add the service to docker-compose.yml for local development 
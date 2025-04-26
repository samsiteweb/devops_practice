# SemanixBackend Kubernetes Deployment

This project contains the Kubernetes deployment configuration for the SemanixBackend microservices, following the architecture shown in the diagram.

## Architecture Overview

### Continuous Integration (Top Green Box)
- **Code Repository**: GitHub stores the microservices code
- **Unit/Integration Tests**: Automated tests run on code push
- **Code Quality**: SonarQube performs static code analysis
- **Docker Build**: Creates container images for microservices
- **Security Scanning**: Container images are scanned for vulnerabilities
- **Notifications**: Microsoft Teams receives build notifications

### Continuous Delivery (Bottom Red Box)
- **Container Registry**: Docker Hub/Azure Container Registry stores images
- **Deployment**: Argo CD handles GitOps-style deployments
- **Configuration**: Consul provides service discovery and config management
- **Secrets**: HashiCorp Vault manages secrets and injects them into services
- **Microservices**: Running in Kubernetes with proper networking and scaling
- **Data Services**: Redis for caching, PostgreSQL for persistence
- **Messaging**: RabbitMQ for asynchronous communication
- **Observability**: Prometheus and Grafana for metrics, OpenTelemetry and Zipkin for tracing

## Implementation Plan

1. **Docker Images**:
   - Multi-stage Dockerfiles for AccessManagement and RequestManagement
   - Optimized for size and security

2. **Helm Charts**:
   - Separate charts for each microservice with appropriate values files
   - Common dependencies extracted for reusability

3. **Kubernetes Base Resources**:
   - Infrastructure components (Redis, RabbitMQ)
   - Monitoring stack (Prometheus, Grafana)
   - Tracing infrastructure (OpenTelemetry, Zipkin)
   - Service discovery (Consul, Vault)

4. **Environment Overlays**:
   - Dev: Lower resource limits, single replicas
   - Prod: Higher resource limits, multiple replicas, proper affinity

5. **CI/CD Pipeline**:
   - GitHub Actions workflow for testing, building, and pushing images
   - Argo CD application definitions for automated deployment

## Directory Structure

```
k8s-deployment/
├── helm/
│   └── charts/
│       ├── accessmanagement/
│       └── requestmanagement/
├── k8s/
│   ├── base/
│   │   ├── infrastructure.yaml
│   │   ├── monitoring.yaml
│   │   ├── tracing.yaml
│   │   ├── service-discovery.yaml
│   │   └── kustomization.yaml
│   ├── overlays/
│   │   ├── dev/
│   │   └── prod/
│   └── argocd/
│       └── application.yaml
└── .github/
    └── workflows/
        └── ci.yml
```

## Deployment Instructions

### Prerequisites
- Kubernetes cluster (local or cloud)
- kubectl configured to access your cluster
- Helm 3 installed
- Argo CD installed on the cluster

### Setup Infrastructure
1. Apply the Kubernetes base resources:
   ```bash
   kubectl apply -k k8s/base
   ```

2. Install Argo CD:
   ```bash
   kubectl create namespace argocd
   kubectl apply -n argocd -f https://raw.githubusercontent.com/argoproj/argo-cd/stable/manifests/install.yaml
   ```

3. Register the application in Argo CD:
   ```bash
   kubectl apply -f k8s/argocd/application.yaml
   ```

### Deploy Environments
- For development:
  ```bash
  kubectl apply -k k8s/overlays/dev
  ```

- For production:
  ```bash
  kubectl apply -k k8s/overlays/prod
  ```

### Monitoring and Management
- Grafana dashboards: http://grafana.your-domain.com
- Zipkin UI: http://zipkin.your-domain.com
- Consul UI: http://consul.your-domain.com
- Vault UI: http://vault.your-domain.com

## Security Considerations

- All secrets are managed by Vault
- Container images scanned for vulnerabilities
- Network policies control inter-service communication
- Service accounts with minimal permissions
- Regular security patches applied through CI/CD pipeline 
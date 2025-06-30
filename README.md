# Disbursement Provider Contract Tests

This is a sample project for contract testing using PactNet and xUnit with a .NET 8 provider API.

## Overview
- Provider API: .NET 8 (see `src/Disbursement.Api`)
- Contract tests: xUnit, PactNet (see `tests/ContractTests`)
- Pact files are sourced from the [pact-contracts](https://github.com/suryamani-tw/pact-contracts) repository during CI.

## Running Locally

1. **Restore dependencies:**
   ```sh
   dotnet restore
   ```
2. **Build the solution:**
   ```sh
   dotnet build
   ```
3. **Run contract tests:**
   ```sh
   dotnet test tests/ContractTests/Disbursement.ContractTests.csproj
   ```
   > **Note:** The contract tests now automatically start and stop the API server as part of the test process. You do NOT need to start the API manually or set the `API_URL` environment variable.

## CI/CD

See `.github/workflows/contract-tests.yml` for automated contract verification in GitHub Actions.

## Directory Structure
- `src/` - API and supporting projects
- `tests/` - Test projects
- `pacts/` - Pact files (populated in CI)

---

For more details, see the source code and workflow files.

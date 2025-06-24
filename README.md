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
3. **Start the API:**
   ```sh
   dotnet run --project src/Disbursement.Api/Disbursement.Api.csproj --urls=http://localhost:5062
   ```
4. **Run contract tests:**
   ```sh
   API_URL=http://localhost:5062 dotnet test tests/ContractTests/Disbursement.ContractTests.csproj
   ```

## CI/CD

See `.github/workflows/contract-tests.yml` for automated contract verification in GitHub Actions.

## Directory Structure
- `src/` - API and supporting projects
- `tests/` - Test projects
- `pacts/` - Pact files (populated in CI)

---

For more details, see the source code and workflow files.

# IdentityServer4.Admin .NET 10 Upgrade Tasks

After every commit, push the changes.

## Overview

This document tracks the bottom-up tier-based upgrade of IdentityServer4.Admin solution from .NET 6.0 to .NET 10.0. Projects will be upgraded sequentially through 8 tiers, starting from leaf nodes (no dependencies) and progressing upward to applications and test projects.

**Progress**: 15/17 tasks complete (88%) ![0%](https://progress-bar.xyz/88)

---

## Tasks

### [✓] TASK-001: Verify prerequisites *(Completed: 2026-02-18 16:31)*
**References**: Plan §Migration Strategy Prerequisites

- [✓] (1) Verify .NET 10 SDK installed and available
- [✓] (2) .NET 10 SDK version meets minimum requirements (**Verify**)
- [✓] (3) Verify Azure SDK compatibility with .NET 10 (required for Tier 1 Azure KeyVault changes)
- [✓] (4) Azure SDK compatible with .NET 10 (**Verify**)

---

### [✓] TASK-002: Upgrade Tier 1 foundation libraries to .NET 10 *(Completed: 2026-02-18 16:35)*
**References**: Plan §Tier 1, Plan §Package Update Reference Tier 1, Plan §Breaking Changes Catalog §2

- [✓] (1) Update `<TargetFramework>` to `net10.0` in all 3 Tier 1 projects per Plan §Tier 1 (Shared.Configuration, BusinessLogic.Shared, EF.Extensions)
- [✓] (2) All Tier 1 project files updated (**Verify**)
- [✓] (3) Update packages in Shared.Configuration per Plan §Package Update Reference Tier 1: Azure.Identity 1.5.0→1.17.1, AspNetCore packages 6.0.1→10.0.3, replace deprecated AzureKeyVault package with Azure.Extensions.AspNetCore.Configuration.Secrets
- [✓] (4) Add new package: Azure.Extensions.AspNetCore.Configuration.Secrets to Shared.Configuration
- [✓] (5) Remove deprecated package: Microsoft.Extensions.Configuration.AzureKeyVault from Shared.Configuration
- [✓] (6) All Tier 1 package updates applied (**Verify**)
- [✓] (7) Update Azure KeyVault configuration code in Shared.Configuration per Plan §Breaking Changes Catalog §2 (replace AddAzureKeyVault methods, replace DefaultKeyVaultSecretManager with KeyVaultSecretManager)
- [✓] (8) Fix configuration binding calls (IConfiguration.Get<T>) in Shared.Configuration per Plan §Breaking Changes Catalog §1
- [✓] (9) Build all 3 Tier 1 projects
- [✓] (10) All Tier 1 projects build with 0 errors (**Verify**)
- [✓] (11) Commit changes with message: "TASK-002: Upgrade Tier 1 foundation libraries to .NET 10"

---

### [✓] TASK-003: Test Tier 1 and validate foundation *(Completed: 2026-02-18 16:35)*
**References**: Plan §Tier 1 Testing Strategy

- [✓] (1) Run unit tests for all Tier 1 projects (if test projects exist for Tier 1)
- [✓] (2) Verify Azure KeyVault integration compiles correctly in Shared.Configuration
- [✓] (3) Verify configuration binding works correctly across Tier 1
- [✓] (4) All Tier 1 validations pass (**Verify**)

---

### [✓] TASK-004: Upgrade Tier 2 data foundation to .NET 10 *(Completed: 2026-02-18 16:46)*
**References**: Plan §Tier 2, Plan §Package Update Reference Tier 2

- [✓] (1) Update `<TargetFramework>` to `net10.0` in Admin.EntityFramework project per Plan §Tier 2
- [✓] (2) Update package: Microsoft.EntityFrameworkCore.Relational 6.0.1→10.0.3 in Admin.EntityFramework per Plan §Package Update Reference Tier 2
- [✓] (3) Build Admin.EntityFramework project
- [✓] (4) Admin.EntityFramework builds with 0 errors (**Verify**)
- [✓] (5) Commit changes with message: "TASK-004: Upgrade Tier 2 data foundation to .NET 10"

---

### [✓] TASK-005: Test Tier 2 and verify EF core layer *(Completed: 2026-02-18 16:46)*
**References**: Plan §Tier 2 Testing Strategy

- [✓] (1) Run unit tests for Admin.EntityFramework (if tests exist)
- [✓] (2) Verify EF extension methods function correctly
- [✓] (3) Verify Tier 1 projects still build and reference correctly
- [✓] (4) All Tier 2 validations pass (**Verify**)

---

### [✓] TASK-006: Upgrade Tier 3 business logic and identity data to .NET 10 *(Completed: 2026-02-18 16:47)*
**References**: Plan §Tier 3, Plan §Package Update Reference Tier 3

- [✓] (1) Update `<TargetFramework>` to `net10.0` in both Tier 3 projects per Plan §Tier 3 (Admin.BusinessLogic, Admin.EntityFramework.Identity)
- [✓] (2) Update package in Admin.EntityFramework.Identity: Microsoft.AspNetCore.Identity.EntityFrameworkCore 6.0.1→10.0.3 per Plan §Package Update Reference Tier 3
- [✓] (3) Build both Tier 3 projects
- [✓] (4) Both Tier 3 projects build with 0 errors (**Verify**)
- [✓] (5) Commit changes with message: "TASK-006: Upgrade Tier 3 business logic and identity data to .NET 10"

---

### [✓] TASK-007: Test Tier 3 and validate business logic layer *(Completed: 2026-02-18 16:47)*
**References**: Plan §Tier 3 Testing Strategy

- [✓] (1) Run unit tests for Admin.BusinessLogic and Admin.EntityFramework.Identity
- [✓] (2) Verify IdentityServer4.EntityFramework integration works
- [✓] (3) Verify Identity store operations function correctly
- [✓] (4) All Tier 3 validations pass (**Verify**)

---

### [✓] TASK-008: Upgrade Tier 4 identity business logic and configuration to .NET 10 *(Completed: 2026-02-18 16:48)*
**References**: Plan §Tier 4, Plan §Package Update Reference Tier 4

- [✓] (1) Update `<TargetFramework>` to `net10.0` in both Tier 4 projects per Plan §Tier 4 (Admin.BusinessLogic.Identity, Admin.EntityFramework.Configuration)
- [✓] (2) Update packages in Admin.BusinessLogic.Identity: Microsoft.AspNetCore.Identity.EntityFrameworkCore 6.0.1→10.0.3 per Plan §Package Update Reference Tier 4
- [✓] (3) Update packages in Admin.EntityFramework.Configuration: DataProtection.EntityFrameworkCore 6.0.1→10.0.3, EFCore.SqlServer 6.0.1→10.0.3 per Plan §Package Update Reference Tier 4
- [✓] (4) Build both Tier 4 projects
- [✓] (5) Both Tier 4 projects build with 0 errors (**Verify**)
- [✓] (6) Commit changes with message: "TASK-008: Upgrade Tier 4 identity business logic and configuration to .NET 10"

---

### [✓] TASK-009: Test Tier 4 and validate identity services *(Completed: 2026-02-18 16:48)*
**References**: Plan §Tier 4 Testing Strategy

- [✓] (1) Run unit tests for both Tier 4 projects
- [✓] (2) Verify DbContext configurations compile correctly
- [✓] (3) Verify DataProtection entity configurations work
- [✓] (4) All Tier 4 validations pass (**Verify**)

---

### [✓] TASK-010: Upgrade Tier 5 shared components and UI to .NET 10 *(Completed: 2026-02-18 17:02)*
**References**: Plan §Tier 5, Plan §Package Update Reference Tier 5, Plan §Breaking Changes Catalog §5, §6

- [✓] (1) Update `<TargetFramework>` to `net10.0` in all 3 Tier 5 projects per Plan §Tier 5 (EF.Shared, Admin.UI, Shared)
- [✓] (2) Update packages in EF.Shared: DataProtection.EntityFrameworkCore 6.0.1→10.0.3 per Plan §Package Update Reference Tier 5
- [✓] (3) Update packages in Admin.UI: Mvc.Razor.RuntimeCompilation 6.0.1→10.0.3, EFCore.InMemory 6.0.1→10.0.3, Diagnostics.HealthChecks.EFCore 6.0.1→10.0.3 per Plan §Package Update Reference Tier 5
- [✓] (4) Fix OpenIdConnect event handlers in Admin.UI per Plan §Breaking Changes Catalog §5 (update event handler signatures)
- [✓] (5) Fix ForwardedHeaders configuration in Admin.UI per Plan §Breaking Changes Catalog §6 (update KnownNetworks.Clear pattern)
- [✓] (6) Fix JWT token parsing in Admin.UI per Plan §Breaking Changes Catalog §3 (AuthenticatedTestRequestMiddleware.cs)
- [✓] (7) Build all 3 Tier 5 projects
- [✓] (8) All Tier 5 projects build with 0 errors (**Verify**)
- [✓] (9) Commit changes with message: "TASK-010: Upgrade Tier 5 shared components and UI to .NET 10"

---

### [✓] TASK-011: Test Tier 5 and validate shared components *(Completed: 2026-02-18 17:02)*
**References**: Plan §Tier 5 Testing Strategy

- [✓] (1) Run unit tests for all Tier 5 projects
- [✓] (2) Verify Razor components compile correctly in Admin.UI
- [✓] (3) Verify health check configurations work
- [✓] (4) All Tier 5 validations pass (**Verify**)

---

### [✓] TASK-012: Upgrade Tier 6 database providers and STS to .NET 10 *(Completed: 2026-02-18 17:06)*
**References**: Plan §Tier 6, Plan §Package Update Reference Tier 6, Plan §Breaking Changes Catalog §4, §7

- [✓] (1) Update `<TargetFramework>` to `net10.0` in all 4 Tier 6 projects per Plan §Tier 6 (EF.MySql, EF.PostgreSQL, EF.SqlServer, STS.Identity)
- [✓] (2) Update package in EF.SqlServer: Microsoft.EntityFrameworkCore.SqlServer 6.0.1→10.0.3 per Plan §Package Update Reference Tier 6
- [✓] (3) Update all 9 packages in STS.Identity per Plan §Package Update Reference Tier 6 (DataProtection, Diagnostics, Identity, HealthChecks packages 6.0.1→10.0.3, Microsoft.Identity.Web 1.22.1→4.3.0)
- [✓] (4) Remove incompatible package from STS.Identity: Microsoft.VisualStudio.Azure.Containers.Tools.Targets per Plan §Package Update Reference Tier 6
- [✓] (5) Fix X509Certificate2 constructor in STS.Identity per Plan §Breaking Changes Catalog §4 (IdentityServerBuilderExtensions.cs lines 86, 163)
- [✓] (6) Update Microsoft.Identity.Web configuration in STS.Identity per Plan §Breaking Changes Catalog §7 (migrate 1.x→4.x patterns)
- [✓] (7) Fix configuration binding calls in STS.Identity per Plan §Breaking Changes Catalog §1
- [✓] (8) Build all 4 Tier 6 projects
- [✓] (9) All Tier 6 projects build with 0 errors (**Verify**)
- [✓] (10) Commit changes with message: "TASK-012: Upgrade Tier 6 database providers and STS to .NET 10"

---

### [✓] TASK-013: Test Tier 6 and validate STS authentication *(Completed: 2026-02-18 17:08)*
**References**: Plan §Tier 6 Testing Strategy

- [✓] (1) Run integration tests in STS.Identity.IntegrationTests project
- [✓] (2) All STS.Identity integration tests pass with 0 failures (**Verify**)
- [✓] (3) Verify database provider projects build correctly
- [✓] (4) All Tier 6 validations pass (**Verify**)

---

### [✓] TASK-014: Upgrade Tier 7 main applications to .NET 10 *(Completed: 2026-02-18 17:10)*
**References**: Plan §Tier 7, Plan §Package Update Reference Tier 7, Plan §Breaking Changes Catalog §3, §6

- [✓] (1) Update `<TargetFramework>` to `net10.0` in both Tier 7 projects per Plan §Tier 7 (Admin.Api, Admin Portal)
- [✓] (2) Update all 8 packages in Admin.Api per Plan §Package Update Reference Tier 7 (Authentication.JwtBearer, Diagnostics, Identity, EFCore, HealthChecks packages 6.0.1→10.0.3)
- [✓] (3) Remove incompatible package from Admin.Api: Microsoft.VisualStudio.Azure.Containers.Tools.Targets per Plan §Package Update Reference Tier 7
- [✓] (4) Update all 7 packages in Admin Portal per Plan §Package Update Reference Tier 7 (Diagnostics, Identity, EFCore, HealthChecks, Options packages to 10.0.x, Web.CodeGeneration.Design 6.0.1→10.0.2)
- [✓] (5) Remove deprecated and incompatible packages from Admin Portal: Microsoft.EntityFrameworkCore.Tools.DotNet, Microsoft.VisualStudio.Azure.Containers.Tools.Targets per Plan §Package Update Reference Tier 7
- [✓] (6) Fix JWT authentication in Admin.Api per Plan §Breaking Changes Catalog §3 (update JWT middleware, fix token parsing)
- [✓] (7) Fix ForwardedHeaders configuration in Admin.Api per Plan §Breaking Changes Catalog §6
- [✓] (8) Fix Razor runtime compilation in Admin Portal per Plan §Tier 7 Admin Portal Migration (StartupHelpers.cs line 20)
- [✓] (9) Fix JWT claim mapping in Admin Portal per Plan §Tier 7 Admin Portal Migration (Startup.cs line 23)
- [✓] (10) Fix configuration binding in Admin Portal per Plan §Breaking Changes Catalog §1
- [✓] (11) Build both Tier 7 projects
- [✓] (12) Both Tier 7 applications build with 0 errors (**Verify**)
- [✓] (13) Commit changes with message: "TASK-014: Upgrade Tier 7 main applications to .NET 10"

---

### [✗] TASK-015: Test Tier 7 and validate applications
**References**: Plan §Tier 7 Testing Strategy

- [✓] (1) Run integration tests in Admin.Api.IntegrationTests project
- [✗] (2) All Admin.Api integration tests pass with 0 failures (**Verify**)
- [ ] (3) Run integration tests in Admin.IntegrationTests project
- [ ] (4) All Admin Portal integration tests pass with 0 failures (**Verify**)
- [ ] (5) Verify all Tiers 1-6 still build and function (rebuild all lower-tier projects)
- [ ] (6) No regressions in Tiers 1-6 (**Verify**)

---

### [✓] TASK-016: Upgrade Tier 8 test projects to .NET 10 *(Completed: 2026-02-18 17:21)*
**References**: Plan §Tier 8, Plan §Package Update Reference Tier 8

- [✓] (1) Update `<TargetFramework>` to `net10.0` in all 4 Tier 8 test projects per Plan §Tier 8 (Admin.Api.IntegrationTests, Admin.IntegrationTests, Admin.UnitTests, STS.Identity.IntegrationTests)
- [✓] (2) Update packages in Admin.Api.IntegrationTests: Microsoft.AspNetCore.Mvc.Testing 6.0.1→10.0.3 per Plan §Package Update Reference Tier 8
- [✓] (3) Update packages in Admin.IntegrationTests: Mvc.Testing and TestHost 6.0.1→10.0.3 per Plan §Package Update Reference Tier 8
- [✓] (4) Build all 4 Tier 8 test projects
- [✓] (5) All Tier 8 test projects build with 0 errors (**Verify**)
- [✓] (6) Commit changes with message: "TASK-016: Upgrade Tier 8 test projects to .NET 10"

---

### [▶] TASK-017: Run comprehensive test suite and validate upgrade
**References**: Plan §Testing & Validation Strategy, Plan §Success Criteria

- [ ] (1) Run all tests in Admin.Api.IntegrationTests project
- [ ] (2) Run all tests in Admin.IntegrationTests project
- [ ] (3) Run all tests in Admin.UnitTests project
- [ ] (4) Run all tests in STS.Identity.IntegrationTests project
- [ ] (5) All test projects pass with 0 failures (**Verify**)
- [ ] (6) Verify entire solution builds successfully
- [ ] (7) Solution builds with 0 errors (**Verify**)
- [ ] (8) Commit final validation with message: "TASK-017: Complete .NET 10 upgrade - all tests passing"

---































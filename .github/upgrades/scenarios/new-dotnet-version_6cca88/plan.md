# .NET 10 Upgrade Plan

## Table of Contents
- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Plans](#project-by-project-plans)
- [Package Update Reference](#package-update-reference)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Risk Management](#risk-management)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

---

## Executive Summary

### Scenario
Upgrade IdentityServer4.Admin solution from **.NET 6.0** to **.NET 10.0 (Long Term Support)**.

### Scope
- **21 active projects** (22 including docker-compose placeholder)
- **3 ASP.NET Core applications** (Admin Portal, API, STS Identity)
- **14 class libraries** (Business Logic, EntityFramework layers, UI components)
- **4 test projects** (integration and unit tests)

### Current State
- All projects target `net6.0`
- 256 compatibility issues identified (79 mandatory, 169 potential, 8 optional)
- 61 files affected across the solution
- 60 NuGet packages in use (17 require updates, 4 incompatible, 3 deprecated)
- 1 security vulnerability: `Azure.Identity` 1.5.0 → 1.17.1

### Target State
- All projects targeting `net10.0`
- All Microsoft packages aligned to .NET 10 versions
- Security vulnerability resolved
- Deprecated packages replaced or removed
- All breaking changes addressed

### Complexity Assessment
**Classification: Complex**

**Key Metrics:**
- **Dependency Depth:** 8 levels (Level 0-7)
- **Project Count:** 21 active projects
- **Issue Volume:** 256 total issues
  - Binary incompatibility: 55 occurrences (API changes requiring recompilation)
  - Source incompatibility: 84 occurrences (code changes required)
  - Behavioral changes: 42 occurrences (runtime behavior differences)
- **Package Challenges:**
  - 17 packages need version updates
  - 4 incompatible packages (require removal or replacement)
  - 3 deprecated packages (`Microsoft.EntityFrameworkCore.Tools.DotNet`, `Microsoft.Extensions.Configuration.AzureKeyVault`, `Microsoft.Identity.Web`)
  - 1 security vulnerability (critical to address)

**Complexity Factors:**
- Deep dependency hierarchy requires strict tier-based sequencing
- Multiple EntityFramework database providers (SqlServer, PostgreSQL, MySql)
- IdentityServer4 integration (authentication/authorization complexity)
- Razor Pages project with UI components
- Cross-cutting concerns (logging, health checks, configuration)

### Selected Strategy: Bottom-Up (Tier-Based) Migration

**Rationale:**
- **Deep dependency tree** (8 levels) requires strict ordering to prevent multi-targeting complexity
- **Foundation libraries** (Shared.Configuration, EntityFramework.Extensions) used by 10+ projects - must stabilize these first
- **Risk mitigation:** Each tier validated before proceeding reduces cascading failures
- **Clear validation points:** 8 tiers provide natural checkpoints for testing and review
- **Complexity management:** Incremental approach handles 256 issues systematically without overwhelming changes

**Approach:**
Migrate projects tier-by-tier from Level 0 (leaf nodes) to Level 7 (test projects), with each tier fully upgraded and validated before proceeding to the next.

### Iteration Strategy
Using **Tier-Based Detail Generation** (8 detail iterations - one per dependency level):
- Phase 1: Discovery & Classification (3 iterations) ✓
- Phase 2: Foundation (3 iterations) 
- Phase 3: Dynamic Detail (8 iterations - one per tier)
- **Expected total:** ~14 iterations

---

## Migration Strategy

### Selected Approach: Bottom-Up Incremental Migration

**Decision Rationale:**

**Why Bottom-Up:**
- **Deep dependency hierarchy** (8 levels) makes all-at-once approach impractical
- **Foundation-first principle:** Upgrading leaf nodes first ensures dependencies are always on same or newer framework
- **No multi-targeting needed:** Each project upgrades once, cleanly
- **Risk isolation:** Issues contained within current tier, don't cascade to upgraded tiers
- **Clear validation:** Each tier provides a natural checkpoint for testing
- **Learning curve:** Early tiers inform approach for more complex later tiers

**Why Incremental (Tier-by-Tier):**
- **256 issues** distributed across solution - too complex for simultaneous change
- **3 incompatible packages** + **3 deprecated packages** require careful replacement strategy
- **Security vulnerability** in foundational package (Azure.Identity) affects configuration layer
- **84 source incompatibility issues** will require code changes discovered during compilation
- **Testing confidence:** Each tier can be validated independently before moving up

**Strategy Advantages for This Solution:**
1. **Minimizes blast radius:** Tier 1 issues won't compound with Tier 7 issues
2. **Enables progressive testing:** Can validate each tier's changes thoroughly
3. **Supports rollback:** Can revert a tier without affecting completed tiers
4. **Predictable progress:** 8 tiers provide clear milestones (12.5% per tier)
5. **Resource flexibility:** Team can work tier-by-tier with review gates

**Alternative Rejected:**
- **All-at-once migration:** Rejected due to complexity, depth, and volume of issues
- **Top-down migration:** Rejected - would require complex multi-targeting and break dependency principles

---

### Execution Sequence

Strict tier-by-tier sequence, no tier skipping:

**Phase 1: Foundation (Tiers 1-2)** - Establish stable base
- Tier 1: Upgrade 3 leaf libraries (Configuration, Shared, Extensions)
- Tier 2: Upgrade EntityFramework core

**Phase 2: Core Services (Tiers 3-4)** - Build on stable foundation
- Tier 3: Upgrade BusinessLogic and EF.Identity
- Tier 4: Upgrade Identity BusinessLogic, EF.Configuration, Shared components

**Phase 3: Data & UI (Tiers 5-6)** - Complete infrastructure
- Tier 5: Upgrade EF.Shared, Admin.UI, Shared library
- Tier 6: Upgrade database providers (MySql, PostgreSQL, SqlServer) and STS.Identity

**Phase 4: Applications (Tier 7)** - Main entry points
- Tier 7: Upgrade Admin.Api and Admin Portal (Razor Pages)

**Phase 5: Validation (Tier 8)** - Final verification
- Tier 8: Upgrade test projects, run comprehensive test suite

---

### Tier Completion Criteria

Each tier must meet these criteria before proceeding:

**Mandatory:**
- ✅ All projects in tier build successfully without errors
- ✅ All projects in tier build without warnings (or warnings documented and accepted)
- ✅ All unit tests for tier projects pass
- ✅ No new package dependency conflicts introduced
- ✅ Previously upgraded tiers still build and function

**Recommended:**
- 🔍 Code review of changes completed
- 🔍 Integration tests with dependent tiers pass (if available)
- 🔍 Breaking changes documented

---

### Parallel vs Sequential Execution

**Within Each Tier:**

**Tiers 1, 3, 5, 6, 8:** Projects can be upgraded in parallel
- Same dependency level
- Independent codebases
- Parallel execution acceptable with adequate testing

**Tiers 2, 4:** Single project per tier
- Sequential by definition

**Tier 7 (Applications):** Sequential execution recommended
- Most complex projects (69 and 17 issues)
- Razor Pages application requires careful validation
- API project has 16 mandatory issues
- Benefits from lessons learned in earlier tiers

**Recommendation:** For initial execution, proceed sequentially within all tiers to identify patterns and validate approach. Future similar upgrades could parallelize low-risk tiers.

---

### Bottom-Up Strategy Principles Applied

1. **No Multi-Targeting:** Each project upgraded once to net10.0, no intermediate TFMs
2. **Dependency-First:** Lower tiers always completed before higher tiers
3. **Stable Foundation:** Tier N builds on fully-upgraded Tier N-1
4. **Tier-Scoped Operations:** Framework updates, package updates, and fixes batched per tier
5. **Cumulative Testing:** Each tier tested with all lower tiers
6. **Learning Application:** Patterns from early tiers applied to later tiers

---

## Detailed Dependency Analysis

### Dependency Graph Overview

The solution exhibits a clear 8-tier hierarchical structure with no circular dependencies:

```
Level 7 (Test Projects):
  └─ Admin.Api.IntegrationTests, Admin.IntegrationTests, Admin.UnitTests
       ↓
Level 6 (Applications):
  └─ Admin.Api, Admin (Portal), STS.Identity.IntegrationTests
       ↓
Level 5 (Database Providers):
  └─ EF.MySql, EF.PostgreSQL, EF.SqlServer, STS.Identity
       ↓
Level 4 (Shared Components):
  └─ EF.Shared, Admin.UI, Shared
       ↓
Level 3 (Business/EF Configuration):
  └─ BusinessLogic.Identity, EF.Configuration
       ↓
Level 2 (Core Business/Data):
  └─ BusinessLogic, EF.Identity
       ↓
Level 1 (Data Foundation):
  └─ EntityFramework
       ↓
Level 0 (Leaf Nodes):
  └─ Shared.Configuration, BusinessLogic.Shared, EF.Extensions, docker-compose
```

### Migration Tiers (Bottom-Up)

#### Tier 1: Foundation Libraries (Level 0 - No Dependencies)
**Projects (4):**
1. `Skoruba.IdentityServer4.Shared.Configuration`
2. `Skoruba.IdentityServer4.Admin.BusinessLogic.Shared`
3. `Skoruba.IdentityServer4.Admin.EntityFramework.Extensions`
4. `docker-compose.dcproj` (excluded - orchestration only)

**Why First:** Zero internal dependencies, used by 2-10+ projects each. Upgrading these establishes stable foundation.

**Impact Radius:** High - Configuration used by all 3 applications, Extensions/Shared used across entire stack

---

#### Tier 2: Data Foundation (Level 1)
**Projects (1):**
1. `Skoruba.IdentityServer4.Admin.EntityFramework`

**Dependencies:** EF.Extensions (Tier 1)

**Why Next:** Core EntityFramework abstractions used by all database-specific implementations

**Impact Radius:** High - Used by BusinessLogic and all EF.Identity layers

---

#### Tier 3: Business Logic & Identity Data (Level 2)
**Projects (2):**
1. `Skoruba.IdentityServer4.Admin.BusinessLogic`
2. `Skoruba.IdentityServer4.Admin.EntityFramework.Identity`

**Dependencies:** EntityFramework (Tier 2), BusinessLogic.Shared (Tier 1)

**Why Next:** Core business rules and identity data layer - needed by higher-level services

**Impact Radius:** High - BusinessLogic used by all applications, EF.Identity used by configuration layer

---

#### Tier 4: Identity Business Logic & Configuration (Level 3)
**Projects (2):**
1. `Skoruba.IdentityServer4.Admin.BusinessLogic.Identity`
2. `Skoruba.IdentityServer4.Admin.EntityFramework.Configuration`

**Dependencies:** EF.Identity (Tier 3), BusinessLogic.Shared (Tier 1)

**Why Next:** Identity-specific business logic and EF configuration needed by shared components

**Impact Radius:** Medium-High - Used by applications and shared libraries

---

#### Tier 5: Shared Components & UI (Level 4)
**Projects (3):**
1. `Skoruba.IdentityServer4.Admin.EntityFramework.Shared`
2. `Skoruba.IdentityServer4.Admin.UI` (Razor Pages components)
3. `Skoruba.IdentityServer4.Shared`

**Dependencies:** EF.Configuration, BusinessLogic.Identity, Shared.Configuration (Tiers 1-4)

**Why Next:** Shared libraries and UI components needed by database providers and applications

**Impact Radius:** High - EF.Shared used by 5 projects (all DB providers + STS), UI used by Admin portal, Shared used by all applications

---

#### Tier 6: Database Providers & STS Application (Level 5)
**Projects (4):**
1. `Skoruba.IdentityServer4.Admin.EntityFramework.MySql`
2. `Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL`
3. `Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer`
4. `Skoruba.IdentityServer4.STS.Identity` (Authentication Server)

**Dependencies:** EF.Shared, Shared.Configuration, Shared (Tiers 1-5)

**Why Next:** Database implementations needed by API/Admin, STS application can run independently

**Impact Radius:** Medium - DB providers used by Admin.Api and Admin portal, STS has one test project dependent

---

#### Tier 7: Main Applications (Level 6)
**Projects (2):**
1. `Skoruba.IdentityServer4.Admin.Api` (Web API)
2. `Skoruba.IdentityServer4.Admin` (Admin Portal - Razor Pages)

**Dependencies:** All DB providers, BusinessLogic, Shared, UI (Tiers 1-6)

**Why Next:** Entry-point applications that consume entire stack

**Impact Radius:** Low internally, High for tests - Used only by their respective test projects

---

#### Tier 8: Test Projects (Level 7)
**Projects (4):**
1. `Skoruba.IdentityServer4.Admin.Api.IntegrationTests`
2. `Skoruba.IdentityServer4.Admin.IntegrationTests`
3. `Skoruba.IdentityServer4.Admin.UnitTests`
4. `Skoruba.IdentityServer4.STS.Identity.IntegrationTests` (Level 6)

**Dependencies:** Applications (Tier 7) and STS.Identity (Tier 6)

**Why Last:** Tests validate the applications - must migrate after the code they test

**Impact Radius:** None - Top-level nodes with no dependents

---

### Critical Path

**Longest dependency chain:** 
`Shared.Configuration` (L0) → `BusinessLogic` (L2) → `BusinessLogic.Identity` (L3) → `Shared` (L4) → `STS.Identity` (L5) → `STS.IntegrationTests` (L6)

**Bottleneck projects** (block multiple dependents):
- `Shared.Configuration` (Level 0): 3 applications depend on it
- `EntityFramework` (Level 1): All EF layers depend on it
- `EF.Shared` (Level 4): All 3 database providers depend on it
- `Admin.UI` (Level 4): Admin portal depends on it

### Risk Considerations

**High-Risk Tiers:**
- **Tier 1** (Shared.Configuration): 24 issues including security vulnerability, impacts all applications
- **Tier 7** (Admin.Api): 69 issues (16 mandatory) - most complex project
- **Tier 7** (Admin Portal): Razor Pages application, 17 issues

**Circular Dependencies:** None detected

**Parallel Opportunities:** Projects within same tier can potentially be upgraded in parallel, but sequential execution recommended for complex tiers (Tier 7).

---

## Project-by-Project Plans

### Tier 1: Foundation Libraries

#### Project: Skoruba.IdentityServer4.Shared.Configuration

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary
- **Files:** 19 total, 7 with issues
- **Dependencies:** None (Level 0)
- **Dependents:** Admin.Api, Admin Portal, STS.Identity (all 3 applications)
- **Packages:** 7 packages
- **Lines of Code:** ~500-1000 (estimated)
- **Issues:** 24 total (11 mandatory, 10 potential, 3 optional)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** 2 packages upgraded, 1 security fix, 1 deprecated package replaced

---

**Migration Steps:**

**1. Prerequisites**
- ✅ No project dependencies to upgrade first
- ✅ Verify Azure SDK compatibility with .NET 10
- ✅ Review Azure KeyVault integration patterns for .NET 10

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| **Azure.Identity** | 1.5.0 | 1.17.1 | ⚠️ **CRITICAL:** Security vulnerability (MODERATE severity) |
| Microsoft.AspNetCore.DataProtection.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.AspNetCore.Identity.UI | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| **Microsoft.Extensions.Configuration.AzureKeyVault** | 3.1.22 | **REPLACE** | ⚠️ Deprecated - Replace with `Azure.Extensions.AspNetCore.Configuration.Secrets` |

**Compatible Packages (No Change):**
- Azure.Extensions.AspNetCore.DataProtection.Keys 1.1.0
- Azure.Security.KeyVault.Certificates 4.2.0
- Sendgrid 9.25.2

**4. Expected Breaking Changes**

**Azure KeyVault Configuration (HIGH IMPACT):**

**File:** `Helpers/StartupHelpers.cs`

**Issue:** `AddAzureKeyVault` extension methods removed from deprecated `Microsoft.Extensions.Configuration.AzureKeyVault` package

**Breaking Change Details:**
- Line 89: `AddAzureKeyVault(endpoint, clientId, clientSecret)` - Method signature removed
- Line 98: `AddAzureKeyVault(endpoint, keyVaultClient, DefaultKeyVaultSecretManager)` - Method signature removed
- `DefaultKeyVaultSecretManager` type no longer exists

**Required Code Changes:**
```csharp
// OLD (.NET 6 - Deprecated package):
using Microsoft.Extensions.Configuration.AzureKeyVault;
configurationBuilder.AddAzureKeyVault(
    azureKeyVaultConfiguration.AzureKeyVaultEndpoint,
    azureKeyVaultConfiguration.ClientId, 
    azureKeyVaultConfiguration.ClientSecret);

// NEW (.NET 10 - Modern pattern):
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;

var credential = new ClientSecretCredential(
    azureKeyVaultConfiguration.TenantId,
    azureKeyVaultConfiguration.ClientId, 
    azureKeyVaultConfiguration.ClientSecret);

var secretClient = new SecretClient(
    new Uri(azureKeyVaultConfiguration.AzureKeyVaultEndpoint), 
    credential);

configurationBuilder.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
```

**Configuration Binding (MEDIUM IMPACT):**

**File:** `Helpers/DockerHelpers.cs` (Line 14), `Helpers/StartupHelpers.cs` (Line 50, 83)

**Issue:** `IConfiguration.Get<T>()` method has binary incompatibility

**Breaking Change:** Method signature changed, requires recompilation and potential code adjustments

**Required Code Changes:**
- Recompilation will likely resolve most cases
- May need to add null-checking or provide binding options
- Consider using `IConfiguration.Get<T>(options => ...)` with explicit binding configuration if defaults insufficient

**5. Code Modifications**

**High Priority:**
1. Replace deprecated AzureKeyVault configuration (StartupHelpers.cs, lines 67-98)
2. Add new package: `Azure.Extensions.AspNetCore.Configuration.Secrets`
3. Update credential initialization pattern
4. Replace `DefaultKeyVaultSecretManager` with `KeyVaultSecretManager`

**Medium Priority:**
5. Review all `configuration.Get<T>()` calls (3 occurrences)
6. Verify DataProtection KeyVault integration still works with new APIs
7. Test URI behavioral changes (System.Uri - lines 67, 74)

**Testing Focus:**
- Azure KeyVault connectivity and secret retrieval
- DataProtection key storage in KeyVault
- Configuration binding for all config sections
- Docker configuration helper functionality

**6. Testing Strategy**

**Unit Tests:**
- Configuration loading tests
- DockerHelpers functionality
- DataProtection configuration

**Integration Tests:**
- Azure KeyVault connection and secret retrieval (requires Azure resources or mocks)
- Configuration binding across all applications
- DataProtection encryption/decryption flows

**Manual Verification:**
- Run each application (Admin, Api, STS) and verify startup
- Check application logs for configuration warnings
- Verify Azure KeyVault integration in development/staging environment

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] Azure.Identity upgraded to 1.17.1 (security fix)
- [ ] Deprecated package replaced with modern alternative
- [ ] All package updates applied
- [ ] AzureKeyVault configuration code compiles
- [ ] Configuration.Get<T>() calls compile
- [ ] All 3 applications (Admin, Api, STS) start successfully
- [ ] Azure KeyVault secrets accessible (if configured)
- [ ] DataProtection keys stored/retrieved from KeyVault
- [ ] No new security vulnerabilities introduced
- [ ] Dependent projects (Admin, Api, STS) still reference this project correctly

---

#### Project: Skoruba.IdentityServer4.Admin.BusinessLogic.Shared

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary
- **Files:** 6 total, 1 with issues
- **Dependencies:** None (Level 0)
- **Dependents:** Admin.BusinessLogic, Admin.BusinessLogic.Identity
- **Packages:** 0 packages
- **Issues:** 1 total (1 mandatory)

**Target State:**
- **Target Framework:** net10.0
- **No package updates required**

---

**Migration Steps:**

**1. Prerequisites**
- ✅ No project dependencies
- ✅ No package dependencies

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**
None required - project has no NuGet packages.

**4. Expected Breaking Changes**
None expected - framework-only update with no external dependencies.

**5. Code Modifications**
- Recompilation only
- No code changes anticipated

**6. Testing Strategy**

**Unit Tests:**
- Verify any shared models, DTOs, or contracts still compile
- Validate any shared business logic interfaces

**Integration Tests:**
- Build dependent projects (BusinessLogic, BusinessLogic.Identity) against upgraded version
- Ensure no breaking changes in shared contracts

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] Dependent projects (BusinessLogic, BusinessLogic.Identity) still reference correctly
- [ ] No regressions in shared models or contracts

---

#### Project: Skoruba.IdentityServer4.Admin.EntityFramework.Extensions

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary
- **Files:** 5 total, 1 with issues
- **Dependencies:** None (Level 0)
- **Dependents:** Admin.EntityFramework (core EF layer)
- **Packages:** 0 packages
- **Issues:** 1 total (1 mandatory)

**Target State:**
- **Target Framework:** net10.0
- **No package updates required**

---

**Migration Steps:**

**1. Prerequisites**
- ✅ No project dependencies
- ✅ No package dependencies

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**
None required - project has no NuGet packages.

**4. Expected Breaking Changes**
None expected - framework-only update with no external dependencies.

**5. Code Modifications**
- Recompilation only
- Likely contains EntityFramework extension methods or helpers
- No code changes anticipated

**6. Testing Strategy**

**Unit Tests:**
- Verify extension methods still compile and function
- Test any EF-specific helpers

**Integration Tests:**
- Build dependent project (Admin.EntityFramework) against upgraded version
- Verify EF extensions still work in context

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] Extension methods still accessible from dependent projects
- [ ] Dependent project (Admin.EntityFramework) references correctly

---

### Tier 2: Data Foundation

#### Project: Skoruba.IdentityServer4.Admin.EntityFramework

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary
- **Dependencies:** Admin.EntityFramework.Extensions (Tier 1)
- **Dependents:** Admin.BusinessLogic, Admin.EntityFramework.Identity (foundation for all EF layers)
- **Packages:** 1 package (Microsoft.EntityFrameworkCore.Relational)
- **Issues:** 2 total (1 mandatory, 1 potential)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** 1 package upgraded (EF Core Relational)

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 1 complete (EntityFramework.Extensions upgraded and validated)
- ✅ Verify EntityFramework.Extensions reference resolves correctly

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| Microsoft.EntityFrameworkCore.Relational | 6.0.1 | 10.0.3 | Alignment with .NET 10 - Core EF abstractions update |

**4. Expected Breaking Changes**

**Entity Framework Core 6 → 10 Changes:**
- **Breaking changes across EF Core 7, 8, 9, 10** - Multiple versions skipped
- **Common areas of impact:**
  - Value generation changes
  - Query behavior modifications
  - Model building conventions
  - Migration scaffolding differences
  - Interceptor API changes

**Mitigation:**
- Most breaking changes will surface at higher tiers (Identity, Configuration, Database providers)
- This layer contains abstractions, less likely to be directly affected
- Validate during Tier 3-6 migrations where concrete implementations exist

**5. Code Modifications**
- Recompilation required
- No immediate code changes expected (abstractions/interfaces)
- Monitor for obsolete warnings during build

**6. Testing Strategy**

**Unit Tests:**
- Verify EF extension methods still function
- Test any repository base classes or abstractions

**Integration Tests:**
- Build dependent projects against upgraded version
- Defer comprehensive EF testing to Tier 3+ (where DbContext implementations exist)

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] EF Core 10.0.3 package restored successfully
- [ ] Dependent projects (BusinessLogic, EF.Identity) still reference correctly
- [ ] No package version conflicts with Tier 1 projects

---

### Tier 3: Business Logic & Identity Data

#### Project: Skoruba.IdentityServer4.Admin.BusinessLogic

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary
- **Files:** 143 total, 1 with issues
- **Dependencies:** Admin.EntityFramework (Tier 2), Admin.BusinessLogic.Shared (Tier 1)
- **Dependents:** Admin Portal, Admin.Api, Admin.UI (all applications and UI layer)
- **Packages:** 1 package (IdentityServer4.EntityFramework)
- **Issues:** 1 total (1 mandatory)

**Target State:**
- **Target Framework:** net10.0
- **No package updates required**

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 1 complete (BusinessLogic.Shared upgraded)
- ✅ Tier 2 complete (Admin.EntityFramework upgraded)
- ✅ Verify project references resolve correctly

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

**Compatible Packages (No Update Needed):**
- IdentityServer4.EntityFramework 4.1.2 (compatible with .NET 10)

**4. Expected Breaking Changes**
- Minimal expected - large codebase (143 files) but only framework-level change
- IdentityServer4.EntityFramework compatible with .NET 10
- May encounter indirect breaking changes from EF Core 10 in data access patterns

**5. Code Modifications**
- Recompilation required for 143 files
- Monitor for obsolete API warnings
- Verify IdentityServer4 integration points
- Review any EF Core query patterns for behavioral changes

**6. Testing Strategy**

**Unit Tests:**
- Run all business logic unit tests
- Verify service layer functionality
- Test any IdentityServer4-specific business rules

**Integration Tests:**
- Verify data access through upgraded EntityFramework layer
- Test IdentityServer4 entity operations (clients, resources, grants)
- Validate business logic with upgraded dependencies

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings (or warnings documented)
- [ ] All 143 files recompile successfully
- [ ] IdentityServer4.EntityFramework integration intact
- [ ] Unit tests pass
- [ ] Dependent projects (Admin, Api, UI) still reference correctly

---

#### Project: Skoruba.IdentityServer4.Admin.EntityFramework.Identity

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary
- **Dependencies:** Admin.EntityFramework (Tier 2)
- **Dependents:** Admin.BusinessLogic.Identity, Admin.EntityFramework.Configuration
- **Packages:** 1 package (Microsoft.AspNetCore.Identity.EntityFrameworkCore)
- **Issues:** 2 total (1 mandatory, 1 potential)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** 1 package upgraded (ASP.NET Core Identity.EntityFrameworkCore)

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 2 complete (Admin.EntityFramework upgraded with EF Core 10)
- ✅ Verify EntityFramework reference resolves correctly

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 and EF Core 10 |

**4. Expected Breaking Changes**

**ASP.NET Core Identity 6 → 10:**
- Identity schema changes (unlikely - backward compatible)
- User/Role store interface changes (possible)
- Password hashing algorithm improvements (behavioral)
- Token provider changes (rare)

**EF Core 10 Integration:**
- DbContext configuration for Identity
- Migration compatibility
- Query translation changes for Identity queries

**5. Code Modifications**
- Recompilation required
- Review Identity DbContext configuration
- Verify custom user/role stores (if any) match new interfaces
- Check for obsolete Identity API usage

**6. Testing Strategy**

**Unit Tests:**
- Identity store operations (user/role CRUD)
- Custom Identity extensions (if any)

**Integration Tests:**
- Full Identity data access scenarios
- User registration/authentication data flows
- Role and claims management

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] Microsoft.AspNetCore.Identity.EntityFrameworkCore 10.0.3 restored
- [ ] Identity DbContext still functional
- [ ] No conflicts between EF Core 10 and Identity 10 packages
- [ ] Dependent projects (BusinessLogic.Identity, EF.Configuration) reference correctly

---

### Tier 4: Identity Business Logic & Configuration

#### Project: Skoruba.IdentityServer4.Admin.BusinessLogic.Identity

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary
- **Dependencies:** Admin.BusinessLogic.Shared (Tier 1), Admin.EntityFramework.Identity (Tier 3)
- **Dependents:** Admin.Api, Admin.UI, Shared
- **Packages:** 1 package (Microsoft.AspNetCore.Identity.EntityFrameworkCore)
- **Issues:** 2 total (1 mandatory, 1 potential)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** 1 package upgraded (Identity.EntityFrameworkCore)

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 1 complete (BusinessLogic.Shared upgraded)
- ✅ Tier 3 complete (EntityFramework.Identity upgraded with Identity.EFCore 10)
- ✅ Verify both dependency references resolve

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 and EF Core 10 |

**4. Expected Breaking Changes**

**Identity Business Logic Layer:**
- UserManager/RoleManager API changes (rare but possible)
- SignInManager behavioral changes
- Identity validators or token providers updates
- Password hasher changes (behavioral - more secure algorithms)

**5. Code Modifications**
- Recompilation required
- Review any custom identity services or managers
- Verify Identity-specific business rules
- Check for obsolete Identity API usage

**6. Testing Strategy**

**Unit Tests:**
- Identity business logic unit tests
- User/role management services
- Custom identity validators or token providers

**Integration Tests:**
- Full identity workflows with upgraded EF layer
- User registration, login, role assignment flows

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] Microsoft.AspNetCore.Identity.EntityFrameworkCore 10.0.3 restored
- [ ] Identity services compile and function
- [ ] Dependent projects (Api, UI, Shared) reference correctly

---

#### Project: Skoruba.IdentityServer4.Admin.EntityFramework.Configuration

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary
- **Dependencies:** Admin.EntityFramework.Identity (Tier 3)
- **Dependents:** EF.Shared (Tier 5), STS.Identity (Tier 6), Admin.UI (Tier 5)
- **Packages:** 2 packages (DataProtection.EFCore, EFCore.SqlServer)
- **Issues:** 3 total (1 mandatory, 2 potential)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** 2 packages upgraded (DataProtection, SqlServer provider)

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 3 complete (EntityFramework.Identity upgraded)
- ✅ Verify dependency reference resolves

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| Microsoft.AspNetCore.DataProtection.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.EntityFrameworkCore.SqlServer | 6.0.1 | 10.0.3 | Alignment with EF Core 10 |

**4. Expected Breaking Changes**

**DataProtection Configuration:**
- DbContext configuration for DataProtection keys
- Key storage/retrieval patterns may have changed

**EF Configuration Layer:**
- May contain IdentityServer4 configuration DbContext
- Migration files may need regeneration for EF Core 10
- OnModelCreating configurations compatibility

**5. Code Modifications**
- Recompilation required
- Review DbContext configurations
- Verify DataProtection entity configurations
- Check IdentityServer4 configuration entities (if present)
- May need to regenerate EF migrations (document current migrations first)

**6. Testing Strategy**

**Unit Tests:**
- DbContext configuration validity
- Entity configuration tests

**Integration Tests:**
- DataProtection key persistence
- IdentityServer4 configuration persistence
- Database operations against SqlServer

**Migration Verification:**
- Document current migration state before changes
- Test migrations can still be applied
- Verify database schema compatibility

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] Both packages upgraded to 10.0.3
- [ ] DbContext configurations compile
- [ ] No migration conflicts detected
- [ ] Dependent projects (EF.Shared, STS.Identity, Admin.UI) reference correctly

---

### Tier 5: Shared Components & UI

#### Project: Skoruba.IdentityServer4.Admin.EntityFramework.Shared

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary
- **Dependencies:** Admin.EntityFramework.Configuration (Tier 4)
- **Dependents:** EF.SqlServer, EF.PostgreSQL, EF.MySql (all 3 DB providers), STS.Identity
- **Packages:** 1 package (DataProtection.EntityFrameworkCore)
- **Issues:** 2 total (1 mandatory, 1 potential)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** 1 package upgraded

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 4 complete (EF.Configuration upgraded)
- ✅ Verify dependency reference resolves

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| Microsoft.AspNetCore.DataProtection.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 |

**4. Expected Breaking Changes**
- DataProtection EF integration changes (minor)
- Shared DbContext patterns may need adjustment
- Database provider abstraction compatibility

**5. Code Modifications**
- Recompilation required
- Review shared database configuration helpers
- Verify DataProtection entity model configurations

**6. Testing Strategy**

**Unit Tests:**
- Shared DbContext configuration tests
- Database provider abstractions

**Integration Tests:**
- Build against all 3 database providers (SqlServer, PostgreSQL, MySql)
- Verify DataProtection persistence

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] DataProtection.EntityFrameworkCore 10.0.3 restored
- [ ] All 4 dependent projects (3 DB providers + STS) reference correctly

---

#### Project: Skoruba.IdentityServer4.Admin.UI

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary (Razor Components)
- **Dependencies:** Shared.Configuration (Tier 1), BusinessLogic.Identity (Tier 4), BusinessLogic (Tier 3), EF.Configuration (Tier 4)
- **Dependents:** Admin Portal (Tier 7)
- **Packages:** 3 packages requiring updates
- **Issues:** 39 total (4 mandatory, 32 potential, 3 optional)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** 3 packages upgraded

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tiers 1-4 complete (all dependencies upgraded)
- ✅ Verify all dependency references resolve

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation | 6.0.1 | 10.0.3 | Alignment with .NET 10 - Razor development-time compilation |
| Microsoft.EntityFrameworkCore.InMemory | 6.0.1 | 10.0.3 | Alignment with EF Core 10 - In-memory testing |
| Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 - Health check integrations |

**Compatible Packages (No Change):**
- AutoMapper 10.1.1
- HtmlAgilityPack 1.11.40

**4. Expected Breaking Changes**

**JWT Security Token (MEDIUM IMPACT):**

**File:** `Middlewares/AuthenticatedTestRequestMiddleware.cs` (Lines 22-23)

**Issue:** `JwtSecurityToken` constructor and `Claims` property binary incompatible

**Breaking Change Details:**
- `new JwtSecurityToken(token)` - Binary incompatibility
- `jwt.Claims` property - Binary incompatibility

**Required Code Changes:**
- Recompile and test - likely compatible after recompilation
- If issues persist, may need to adjust token parsing logic

**OpenIdConnect Events (HIGH IMPACT):**

**File:** `Helpers/StartupHelpers.cs` (Lines 400-423)

**Issue:** `OpenIdConnectEvents` and related context types source incompatible

**Breaking Change Details:**
- `OpenIdConnectEvents` constructor/properties changed
- `RedirectContext` and `MessageReceivedContext` types modified
- `OnRedirectToIdentityProvider` and `OnMessageReceived` event signatures

**Required Code Changes:**
- Update event handler signatures to match .NET 10 patterns
- Verify `ProtocolMessage.RedirectUri` property still available
- Test authentication flow thoroughly

**ForwardedHeaders (MEDIUM IMPACT):**

**File:** `Helpers/StartupHelpers.cs` (Line 529)

**Issue:** `ForwardedHeadersOptions.KnownNetworks` property source incompatible

**Breaking Change Details:**
- `KnownNetworks.Clear()` API changed

**Required Code Changes:**
- Update forwarded headers configuration pattern
- Verify proxy/reverse proxy configuration still works

**Behavioral Changes:**
- `UseExceptionHandler()` behavioral changes (Line 595)
- `UseForwardedHeaders()` behavioral changes (Line 532)
- `System.Uri` constructor behavioral changes (Line 449)
- Health check integration behavioral differences

**5. Code Modifications**

**High Priority:**
1. Update OpenIdConnect event handlers (StartupHelpers.cs, lines 400-423)
2. Fix ForwardedHeadersOptions configuration (StartupHelpers.cs, line 529)
3. Review JWT token parsing (AuthenticatedTestRequestMiddleware.cs, lines 22-23)

**Medium Priority:**
4. Verify exception handler middleware (StartupHelpers.cs, line 595)
5. Test health checks with EF Core 10 (StartupHelpers.cs, line 449)
6. Review Razor runtime compilation behavior

**Testing Focus:**
- All Razor components render correctly
- OpenIdConnect authentication flow works
- Health checks report accurately
- Exception handling behaves correctly
- Reverse proxy scenarios (if applicable)

**6. Testing Strategy**

**Unit Tests:**
- UI helper methods
- Middleware logic
- Component models

**Integration Tests:**
- Render Razor components in Admin Portal context
- Full authentication flow with OpenIdConnect
- Health check endpoints
- Exception handling middleware

**Manual Verification:**
- Visual testing of all Razor components
- Authentication flow through UI
- Health check UI functionality

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] All 3 packages upgraded to 10.0.3
- [ ] Razor components compile
- [ ] OpenIdConnect events compile
- [ ] JWT middleware compiles
- [ ] Health checks compile
- [ ] Admin Portal (Tier 7) can reference upgraded UI library
- [ ] No breaking changes in shared UI components

---

#### Project: Skoruba.IdentityServer4.Shared

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary
- **Files:** 12 total
- **Dependencies:** Admin.BusinessLogic.Identity (Tier 4)
- **Dependents:** Admin Portal, Admin.Api, STS.Identity (all 3 applications)
- **Packages:** 7 packages (same as Shared.Configuration - appears to be project reference)
- **Issues:** 6 total (1 mandatory, 2 potential, 3 optional)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** Same as Shared.Configuration (inherits dependencies)

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 1 complete (Shared.Configuration upgraded - security fix applied)
- ✅ Tier 4 complete (BusinessLogic.Identity upgraded)
- ✅ Verify dependency references resolve

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

**Note:** This project likely inherits packages from Shared.Configuration reference. If explicit packages exist:

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| Azure.Identity | 1.5.0 | 1.17.1 | Security vulnerability (inherited or direct) |
| Microsoft.AspNetCore.Identity.UI | 6.0.1 | 10.0.3 | Alignment with .NET 10 (if explicit) |
| Microsoft.AspNetCore.DataProtection.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 (if explicit) |

**Verify during migration:** Check if packages are direct dependencies or inherited.

**4. Expected Breaking Changes**
- Similar to Shared.Configuration if packages are explicit
- Otherwise, minimal changes expected (inherits from dependencies)
- May contain shared models, services, or helpers

**5. Code Modifications**
- Recompilation required
- Review shared services or utilities
- Verify any Identity-specific shared code

**6. Testing Strategy**

**Unit Tests:**
- Shared service tests
- Shared model validation
- Common utility methods

**Integration Tests:**
- Build against all 3 applications (Admin, Api, STS)
- Verify shared functionality accessible

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] Security vulnerability addressed (if direct dependency)
- [ ] All 3 applications (Admin, Api, STS) reference correctly
- [ ] Shared functionality intact

---

### Tier 6: Database Providers & STS Application

#### Project: Skoruba.IdentityServer4.Admin.EntityFramework.MySql

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary
- **Dependencies:** Admin.EntityFramework.Shared (Tier 5)
- **Dependents:** Admin Portal, Admin.Api
- **Packages:** 1 package (Pomelo.EntityFrameworkCore.MySql)
- **Issues:** 1 total (1 mandatory)

**Target State:**
- **Target Framework:** net10.0
- **No package updates required**

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 5 complete (EF.Shared upgraded)
- ✅ Verify dependency reference resolves

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

**Compatible Packages (No Update Needed):**
- Pomelo.EntityFrameworkCore.MySql 6.0.1 (compatible with .NET 10 and EF Core 10)
- Pomelo.EntityFrameworkCore.MySql.Design 1.1.2

**4. Expected Breaking Changes**
- Minimal - MySQL provider maintained separately, 6.x compatible with EF Core 10
- DbContext registration patterns should remain compatible

**5. Code Modifications**
- Recompilation only
- No code changes expected

**6. Testing Strategy**

**Integration Tests:**
- Build against applications (Admin, Api)
- Verify MySQL connection and queries (if MySQL environment available)

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] Pomelo MySql provider remains compatible
- [ ] Dependent projects (Admin, Api) reference correctly

---

#### Project: Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary
- **Dependencies:** Admin.EntityFramework.Shared (Tier 5)
- **Dependents:** Admin Portal, Admin.Api
- **Packages:** 1 package (Npgsql.EntityFrameworkCore.PostgreSQL)
- **Issues:** 1 total (1 mandatory)

**Target State:**
- **Target Framework:** net10.0
- **No package updates required**

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 5 complete (EF.Shared upgraded)
- ✅ Verify dependency reference resolves

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

**Compatible Packages (No Update Needed):**
- Npgsql.EntityFrameworkCore.PostgreSQL 6.0.2 (compatible with .NET 10 and EF Core 10)

**4. Expected Breaking Changes**
- Minimal - Npgsql provider maintained separately, 6.x compatible with EF Core 10
- DbContext registration patterns should remain compatible

**5. Code Modifications**
- Recompilation only
- No code changes expected

**6. Testing Strategy**

**Integration Tests:**
- Build against applications (Admin, Api)
- Verify PostgreSQL connection and queries (if PostgreSQL environment available)

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] Npgsql provider remains compatible
- [ ] Dependent projects (Admin, Api) reference correctly

---

#### Project: Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** ClassLibrary
- **Dependencies:** Admin.EntityFramework.Shared (Tier 5)
- **Dependents:** Admin Portal, Admin.Api
- **Packages:** 1 package (Microsoft.EntityFrameworkCore.SqlServer)
- **Issues:** 2 total (1 mandatory, 1 potential)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** 1 package upgraded

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 5 complete (EF.Shared upgraded)
- ✅ Verify dependency reference resolves

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| Microsoft.EntityFrameworkCore.SqlServer | 6.0.1 | 10.0.3 | Alignment with EF Core 10 |

**4. Expected Breaking Changes**
- EF Core 10 SQL Server provider changes (query translation, features)
- DbContext registration patterns minimal changes expected

**5. Code Modifications**
- Recompilation required
- No code changes expected

**6. Testing Strategy**

**Integration Tests:**
- Build against applications (Admin, Api)
- Verify SQL Server connection and queries

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] EF Core SqlServer 10.0.3 restored
- [ ] Dependent projects (Admin, Api) reference correctly

---

#### Project: Skoruba.IdentityServer4.STS.Identity

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** AspNetCore (Authentication/Token Server)
- **Dependencies:** EF.Shared, Shared.Configuration, EF.Configuration, Shared (Tiers 1-5)
- **Dependents:** STS.Identity.IntegrationTests (Tier 6)
- **Packages:** 9+ packages requiring updates, 1 incompatible, 1 deprecated
- **Issues:** 33 total (14 mandatory, 16 potential, 3 optional)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** 9 packages upgraded, 1 deprecated package replaced, 1 incompatible package removed/updated

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tiers 1-5 complete (all dependencies upgraded)
- ✅ Security vulnerability addressed (Azure.Identity via Shared.Configuration)
- ✅ Deprecated AzureKeyVault package replaced (via Shared.Configuration)
- ✅ Verify all dependency references resolve

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| Microsoft.AspNetCore.DataProtection.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.AspNetCore.Identity.UI | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.EntityFrameworkCore.InMemory | 6.0.1 | 10.0.3 | Alignment with EF Core 10 |
| Microsoft.EntityFrameworkCore.SqlServer | 6.0.1 | 10.0.3 | Alignment with EF Core 10 |
| Microsoft.EntityFrameworkCore.Tools | 6.0.1 | 10.0.3 | Alignment with EF Core 10 |
| Microsoft.Extensions.Diagnostics.HealthChecks | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| **Microsoft.Identity.Web** | 1.22.1 | 4.3.0 | ⚠️ Deprecated - Update to latest LTS version |
| **Microsoft.VisualStudio.Azure.Containers.Tools.Targets** | 1.14.0 | **REMOVE or UPDATE** | ⚠️ Incompatible - Check for updated version or remove |

**Compatible Packages (No Change):**
- AspNet.Security.OAuth.GitHub 6.0.3
- IdentityServer4.AspNetIdentity 4.1.2
- IdentityServer4.EntityFramework 4.1.2
- Serilog (all packages)
- NWebsec.AspNetCore.Middleware 3.0.0

**4. Expected Breaking Changes**

**X509Certificate2 Constructor (HIGH IMPACT):**

**File:** `Helpers/IdentityServerBuilderExtensions.cs` (Lines 86, 163)

**Issue:** `X509Certificate2(pfxPath, password)` constructor source incompatible

**Breaking Change Details:**
- Constructor signature may have changed
- Security improvements in certificate loading
- May require different overload or pattern

**Required Code Changes:**
```csharp
// May need to adjust to:
new X509Certificate2(pfxPath, password, X509KeyStorageFlags.MachineKeySet)
// Or use alternative loading pattern for .NET 10
```

**Configuration Binding (MEDIUM IMPACT):**

**Files:** `Helpers/IdentityServerBuilderExtensions.cs` (Lines 27, 28, 119, 120), `Helpers/StartupHelpers.cs` (Line 455)

**Issue:** `IConfiguration.Get<T>()` binary incompatible (same as Tier 1)

**Required Code Changes:**
- Recompilation should resolve most cases
- Add null-checking or explicit binding options if needed

**OpenIdConnect Configuration (MEDIUM IMPACT):**

**File:** `Helpers/StartupHelpers.cs` (Line 395)

**Issue:** `OpenIdConnectOptions.ClientId` property source incompatible

**Required Code Changes:**
- Verify property still exists in .NET 10
- May need to adjust external provider configuration pattern

**Microsoft.Identity.Web Migration (HIGH IMPACT):**

**Breaking Change:** Version 1.22.1 → 4.3.0 (major version jump)

**Potential Changes:**
- API surface changes in Identity.Web
- Authentication configuration patterns
- Token validation changes
- Integration with ASP.NET Core Identity

**Required Investigation:**
- Review Microsoft.Identity.Web 4.x migration guide
- Identify breaking changes between 1.x and 4.x
- Update authentication configuration accordingly

**5. Code Modifications**

**High Priority:**
1. Fix X509Certificate2 constructor calls (IdentityServerBuilderExtensions.cs, lines 86, 163)
2. Update Microsoft.Identity.Web 1.x → 4.x patterns (entire authentication configuration)
3. Remove or update Microsoft.VisualStudio.Azure.Containers.Tools.Targets
4. Update configuration binding calls (5+ occurrences)

**Medium Priority:**
5. Review OpenIdConnect options configuration (StartupHelpers.cs, line 395)
6. Verify IdentityServer4 integration with .NET 10
7. Test health checks with upgraded packages
8. Review diagnostics EntityFrameworkCore integration

**Testing Focus:**
- **CRITICAL:** Full authentication and token issuance flows
- IdentityServer4 discovery endpoint
- Login/logout flows
- Token validation
- External authentication providers (AzureAD, GitHub)
- Certificate-based signing/validation

**6. Testing Strategy**

**Unit Tests:**
- IdentityServer configuration tests
- Certificate loading tests
- Configuration binding tests

**Integration Tests:**
- Full authentication workflows
- Token issuance and validation
- External provider authentication
- IdentityServer4 endpoints (/.well-known/openid-configuration)
- Database connectivity (all 3 providers if using provider-switching)

**Manual Verification:**
- Start STS.Identity application
- Access IdentityServer discovery endpoint
- Perform complete login flow
- Validate token generation
- Test external provider login (if configured)

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] All 9 packages upgraded to 10.0.3
- [ ] Microsoft.Identity.Web updated to 4.3.0
- [ ] Incompatible package removed or updated
- [ ] X509Certificate2 code compiles
- [ ] Configuration.Get<T>() calls compile
- [ ] OpenIdConnect configuration compiles
- [ ] Application starts successfully
- [ ] IdentityServer4 discovery endpoint accessible
- [ ] Login flow works end-to-end
- [ ] Token generation successful
- [ ] No authentication errors in logs

---

### Tier 7: Main Applications

#### Project: Skoruba.IdentityServer4.Admin.Api

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** AspNetCore (Web API)
- **Dependencies:** Shared.Configuration, EF.SqlServer, EF.PostgreSQL, EF.MySql, BusinessLogic.Identity, BusinessLogic, Shared (Tiers 1-6)
- **Dependents:** Admin.Api.IntegrationTests (Tier 8)
- **Packages:** 8 packages requiring updates, 1 incompatible
- **Issues:** 69 total (16 mandatory, 50 potential, 3 optional)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** 8 packages upgraded, 1 incompatible package removed/updated

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tiers 1-6 complete (all dependencies upgraded and validated)
- ✅ All database providers upgraded (SqlServer, PostgreSQL, MySql)
- ✅ STS.Identity authentication server upgraded and functional
- ✅ Verify all 7 project references resolve

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| Microsoft.AspNetCore.Authentication.JwtBearer | 6.0.1 | 10.0.3 | Alignment with .NET 10 - JWT authentication |
| Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.EntityFrameworkCore.InMemory | 6.0.1 | 10.0.3 | Alignment with EF Core 10 |
| Microsoft.EntityFrameworkCore.SqlServer | 6.0.1 | 10.0.3 | Alignment with EF Core 10 |
| Microsoft.EntityFrameworkCore.Tools | 6.0.1 | 10.0.3 | Alignment with EF Core 10 |
| Microsoft.Extensions.Diagnostics.HealthChecks | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| **Microsoft.VisualStudio.Azure.Containers.Tools.Targets** | 1.14.0 | **REMOVE** | ⚠️ Incompatible - No .NET 10 version, Docker tooling package |

**Compatible Packages (No Change):**
- Swashbuckle.AspNetCore 6.2.3
- Swashbuckle.AspNetCore.Swagger 6.2.3
- IdentityModel 6.0.0
- Serilog (all packages)
- AspNetCore.HealthChecks.* (all packages)

**4. Expected Breaking Changes**

**JwtBearer Authentication (HIGH IMPACT):**

**Files:** `Middlewares/AuthenticatedTestRequestMiddleware.cs` (Lines 24-25), Multiple API controllers

**Issue:** JWT token handling APIs binary/source incompatible

**Breaking Change Details:**
- `JwtSecurityToken` constructor and properties changed
- `JwtBearerDefaults.AuthenticationScheme` field source incompatible
- JWT validation configuration may need updates

**Required Code Changes:**
- Update JWT middleware configuration
- Fix token parsing in test middleware
- Verify JWT validation parameters
- Test bearer token authentication flows

**ForwardedHeaders Configuration (MEDIUM IMPACT):**

**File:** `Helpers/StartupHelpers.cs` (Line 350, 353)

**Issue:** `ForwardedHeadersOptions.KnownNetworks.Clear()` source incompatible

**Required Code Changes:**
- Update forwarded headers configuration pattern for .NET 10
- Verify reverse proxy scenarios work correctly

**Configuration Binding (MEDIUM IMPACT):**

**File:** `Helpers/StartupHelpers.cs` (Line 295)

**Issue:** `IConfiguration.Get<T>()` binary incompatible

**Required Code Changes:**
- Recompilation, potentially add explicit binding options

**Health Checks (LOW-MEDIUM IMPACT):**

**File:** `Helpers/StartupHelpers.cs` (Line 275)

**Issue:** System.Uri behavioral changes in health check configuration

**Required Code Changes:**
- Verify health check endpoints still register correctly
- Test health check responses

**Swagger/OpenAPI (MONITORING REQUIRED):**
- Swashbuckle.AspNetCore 6.2.3 may have compatibility considerations with .NET 10
- API documentation generation may need verification
- OpenAPI spec generation should be tested

**5. Code Modifications**

**High Priority:**
1. Update JWT authentication configuration (Startup configuration)
2. Fix JWT token parsing (AuthenticatedTestRequestMiddleware.cs, lines 24-25)
3. Remove incompatible Azure Container Tools package
4. Update ForwardedHeaders configuration (StartupHelpers.cs, line 350)
5. Fix configuration binding calls (StartupHelpers.cs, line 295)

**Medium Priority:**
6. Verify all API controllers compile
7. Test Swagger/OpenAPI generation
8. Review health check configuration
9. Verify authentication middleware pipeline

**Testing Focus:**
- **CRITICAL:** All API endpoints functional
- JWT bearer authentication works
- Swagger UI accessible and accurate
- Health check endpoints respond
- Database operations through all 3 providers
- API authorization policies enforce correctly

**6. Testing Strategy**

**Unit Tests:**
- API controller logic tests
- Service layer tests
- Middleware tests

**Integration Tests:**
- Full API endpoint testing
- JWT authentication flows
- Database operations (all providers)
- Health checks
- Swagger endpoint generation

**Manual Verification:**
- Start Admin.Api application
- Access Swagger UI (/swagger)
- Test API endpoints with/without authentication
- Verify JWT token validation
- Check health check endpoint (/health)
- Test database switching (SqlServer, PostgreSQL, MySql)

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] All 8 packages upgraded to 10.0.3
- [ ] Incompatible package removed
- [ ] JWT authentication compiles
- [ ] All API controllers compile
- [ ] Application starts successfully
- [ ] Swagger UI accessible
- [ ] API endpoints respond correctly
- [ ] JWT authentication works
- [ ] Health checks report correctly
- [ ] No authentication/authorization errors
- [ ] Integration tests project can reference upgraded API

---

#### Project: Skoruba.IdentityServer4.Admin (Admin Portal)

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** AspNetCore (Razor Pages Application)
- **Dependencies:** EF.SqlServer, EF.PostgreSQL, EF.MySql, Admin.UI, BusinessLogic, Shared (Tiers 1-6)
- **Dependents:** Admin.IntegrationTests, Admin.UnitTests (Tier 8)
- **Packages:** 7 packages requiring updates, 1 incompatible, 1 deprecated
- **Issues:** 17 total (8 mandatory, 7 potential, 2 optional)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** 7 packages upgraded, 1 deprecated package removed, 1 incompatible package removed/updated

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tiers 1-6 complete (all dependencies upgraded)
- ✅ Admin.UI library upgraded (Tier 5) - Razor components available
- ✅ All database providers upgraded
- ✅ Verify all 6 project references resolve

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.EntityFrameworkCore.SqlServer | 6.0.1 | 10.0.3 | Alignment with EF Core 10 |
| Microsoft.EntityFrameworkCore.Tools | 6.0.1 | 10.0.3 | Alignment with EF Core 10 |
| Microsoft.Extensions.Diagnostics.HealthChecks | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.Extensions.Options | 6.0.0 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.VisualStudio.Web.CodeGeneration.Design | 6.0.1 | 10.0.2 | Alignment with .NET 10 - Razor scaffolding |
| **Microsoft.EntityFrameworkCore.Tools.DotNet** | 2.0.3 | **REMOVE** | ⚠️ Deprecated - Functionality in SDK |
| **Microsoft.VisualStudio.Azure.Containers.Tools.Targets** | 1.14.0 | **REMOVE** | ⚠️ Incompatible - Docker tooling |

**Compatible Packages (No Change):**
- Serilog (all packages)
- AspNetCore.HealthChecks.* (all packages)

**4. Expected Breaking Changes**

**Razor Runtime Compilation (MEDIUM IMPACT):**

**File:** `Helpers/StartupHelpers.cs` (Line 20)

**Issue:** `AddRazorRuntimeCompilation()` method source incompatible

**Breaking Change Details:**
- Method signature changed
- PhysicalFileProvider configuration may need adjustment

**Required Code Changes:**
- Update AddRazorRuntimeCompilation call to match .NET 10 signature
- Verify file provider configuration for Admin.UI library references

**JWT Token Handler (MEDIUM IMPACT):**

**File:** `Startup.cs` (Line 23)

**Issue:** `JwtSecurityTokenHandler.DefaultInboundClaimTypeMap` binary incompatible

**Breaking Change Details:**
- Static property or its usage pattern changed
- Affects claim type mapping for JWT tokens

**Required Code Changes:**
```csharp
// May need to update pattern from:
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// To alternative approach or new API in .NET 10
```

**Configuration Binding (MEDIUM IMPACT):**

**File:** `Program.cs` (Lines 62-63)

**Issue:** `IConfiguration.Get<T>()` binary incompatible (consistent with other projects)

**Required Code Changes:**
- Recompilation, add null-checking or binding options

**Razor Pages (HIGH IMPACT - Prioritized per workspace context):**

**Expected Changes:**
- Razor syntax compatibility (should be backward compatible)
- Tag helpers compatibility
- ViewComponent patterns
- Layout and partial view rendering
- Model binding in Razor Pages

**Areas to Verify:**
- All `.cshtml` files compile
- Razor components from Admin.UI library render correctly
- Page routing works
- Model validation
- Form submission and post-back handling

**5. Code Modifications**

**High Priority:**
1. Update Razor runtime compilation configuration (StartupHelpers.cs, line 20)
2. Fix JwtSecurityTokenHandler claim mapping (Startup.cs, line 23)
3. Remove deprecated EF Tools.DotNet package
4. Remove incompatible Azure Container Tools package
5. Update configuration binding (Program.cs, lines 62-63)

**Medium Priority:**
6. Verify all Razor Pages compile and render
7. Test authentication integration with STS.Identity
8. Review database migration seeding (Program.cs)
9. Verify health checks
10. Test all admin portal features

**Testing Focus:**
- **CRITICAL (Razor Pages):** All portal pages render correctly
- Authentication and authorization in portal
- CRUD operations through UI
- Navigation and routing
- Form validation and submission
- Integration with Admin.UI components

**6. Testing Strategy**

**Unit Tests:**
- Page model logic tests
- Helper method tests
- Configuration tests

**Integration Tests:**
- Full page rendering tests
- Authentication flows through portal
- Database operations through UI
- Navigation scenarios

**Manual Verification:**
- Start Admin Portal application
- Login through STS.Identity
- Navigate all major sections
- Test CRUD operations
- Verify Razor components render
- Test forms and validation
- Check responsive design/layout

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] All 7 packages upgraded
- [ ] Deprecated package removed
- [ ] Incompatible package removed
- [ ] All Razor Pages compile
- [ ] JWT claim mapping compiles
- [ ] Razor runtime compilation configured correctly
- [ ] Application starts successfully
- [ ] All portal pages accessible
- [ ] Authentication works with STS.Identity
- [ ] All Razor components from Admin.UI render correctly
- [ ] Database operations functional (all 3 providers)
- [ ] Forms and validation work
- [ ] Test projects (Integration, Unit) can reference upgraded portal

---

### Tier 8: Test Projects

#### Project: Skoruba.IdentityServer4.Admin.Api.IntegrationTests

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** DotNetCoreApp (Test Project)
- **Files:** 13 files
- **Dependencies:** Admin.Api (Tier 7)
- **Dependents:** None (top-level)
- **Packages:** 1 package requiring update
- **Issues:** 14 total (6 mandatory, 8 potential)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** 1 package upgraded

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 7 complete (Admin.Api upgraded and validated)
- ✅ Admin.Api application functional on net10.0
- ✅ Verify project reference resolves

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| Microsoft.AspNetCore.Mvc.Testing | 6.0.1 | 10.0.3 | Alignment with .NET 10 - WebApplicationFactory support |

**Compatible Packages (No Change):**
- coverlet.collector 3.1.0
- FluentAssertions 6.4.0
- Microsoft.NET.Test.Sdk 17.0.0
- xunit 2.4.1
- xunit.runner.visualstudio 2.4.3

**4. Expected Breaking Changes**

**IdentityModel & Claims (MEDIUM IMPACT):**
- 5 issues related to claims-based security
- JWT token handling in tests
- Identity model API changes

**Mvc.Testing Framework (LOW-MEDIUM IMPACT):**
- WebApplicationFactory configuration may have changes
- TestServer setup patterns
- Integration test host configuration

**5. Code Modifications**
- Recompilation required
- Update test fixtures using WebApplicationFactory
- Fix any JWT token creation/validation in test helpers
- Review test authentication setup

**6. Testing Strategy**

**Execution:**
- Run all integration tests against upgraded Admin.Api
- Verify test infrastructure (WebApplicationFactory, TestServer) works
- Check all API endpoint tests pass
- Validate test authentication/authorization

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] Microsoft.AspNetCore.Mvc.Testing 10.0.3 restored
- [ ] All integration tests compile
- [ ] All integration tests run successfully
- [ ] WebApplicationFactory initializes correctly
- [ ] Test authentication works
- [ ] API endpoint tests pass

---

#### Project: Skoruba.IdentityServer4.Admin.IntegrationTests

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** DotNetCoreApp (Test Project)
- **Files:** 14 files
- **Dependencies:** Admin Portal (Tier 7)
- **Dependents:** None (top-level)
- **Packages:** 2 packages requiring update
- **Issues:** 17 total (6 mandatory, 11 potential)

**Target State:**
- **Target Framework:** net10.0
- **Updated Packages:** 2 packages upgraded

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 7 complete (Admin Portal upgraded and validated)
- ✅ Admin Portal application functional on net10.0
- ✅ All Razor Pages rendering correctly
- ✅ Verify project reference resolves

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

| Package | Current | Target | Reason |
|---------|---------|--------|--------|
| Microsoft.AspNetCore.Mvc.Testing | 6.0.1 | 10.0.3 | Alignment with .NET 10 |
| Microsoft.AspNetCore.TestHost | 6.0.1 | 10.0.3 | Alignment with .NET 10 |

**Compatible Packages (No Change):**
- coverlet.msbuild 3.1.0
- FluentAssertions 6.4.0
- Microsoft.NET.Test.Sdk 17.0.0
- xunit 2.4.1
- xunit.runner.visualstudio 2.4.3

**4. Expected Breaking Changes**

**IdentityModel & Claims (MEDIUM IMPACT):**
- Similar to Admin.Api tests - JWT/claims handling
- Authentication setup in tests

**Mvc.Testing & TestHost (MEDIUM IMPACT):**
- WebApplicationFactory for Razor Pages
- TestServer configuration
- Razor Pages rendering in tests

**Behavioral Changes (HIGH COUNT):**
- 7 behavioral change issues
- May affect test assertions
- Runtime behavior differences need verification

**5. Code Modifications**
- Recompilation required
- Update WebApplicationFactory configuration
- Fix Razor Pages test rendering
- Update authentication setup for tests
- Review test assertions for behavioral changes

**6. Testing Strategy**

**Execution:**
- Run all integration tests against upgraded Admin Portal
- Verify Razor Pages render correctly in test context
- Check authentication flows in tests
- Validate database operations through UI in tests

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] Both packages upgraded to 10.0.3
- [ ] All integration tests compile
- [ ] All integration tests run successfully
- [ ] WebApplicationFactory initializes correctly
- [ ] Razor Pages render in test context
- [ ] Test authentication works
- [ ] Portal integration tests pass

---

#### Project: Skoruba.IdentityServer4.Admin.UnitTests

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** DotNetCoreApp (Test Project)
- **Files:** 39 files
- **Dependencies:** Admin Portal (Tier 7)
- **Dependents:** None (top-level)
- **Packages:** 0 packages requiring update
- **Issues:** 3 total (1 mandatory, 2 potential)

**Target State:**
- **Target Framework:** net10.0
- **No package updates required**

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 7 complete (Admin Portal upgraded)
- ✅ Verify project reference resolves

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

**Compatible Packages (No Change):**
- Bogus 34.0.1
- coverlet.msbuild 3.1.0
- FluentAssertions 6.4.0
- Microsoft.NET.Test.Sdk 17.0.0
- Moq 4.16.1
- xunit 2.4.1
- xunit.runner.visualstudio 2.4.3

**4. Expected Breaking Changes**
- 2 source incompatibility issues (minor)
- Mostly recompilation required
- Minimal code changes expected

**5. Code Modifications**
- Recompilation required for 39 test files
- Fix any source incompatibility issues discovered during build
- Update test mocks if necessary

**6. Testing Strategy**

**Execution:**
- Run all 39 unit test files
- Verify all tests pass
- Check test coverage maintained

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] All unit tests compile
- [ ] All unit tests run successfully
- [ ] Test coverage maintained
- [ ] Moq/Bogus frameworks compatible with test code

---

#### Project: Skoruba.IdentityServer4.STS.Identity.IntegrationTests

**Current State:**
- **Target Framework:** net6.0
- **Project Type:** DotNetCoreApp (Test Project)
- **Files:** 17 files
- **Dependencies:** STS.Identity (Tier 6)
- **Dependents:** None (top-level)
- **Packages:** 0 packages requiring update (assessment shows compatible)
- **Issues:** 16 total (1 mandatory, 15 potential)

**Target State:**
- **Target Framework:** net10.0
- **Package updates:** Assessment shows Mvc.Testing and TestHost as compatible (need verification)

---

**Migration Steps:**

**1. Prerequisites**
- ✅ Tier 6 complete (STS.Identity upgraded and validated)
- ✅ STS.Identity authentication flows functional
- ✅ Verify project reference resolves

**2. Framework Update**
- Update `<TargetFramework>` from `net6.0` to `net10.0` in `.csproj`

**3. Package Updates**

**Compatible Packages (assessment indicates no updates needed, verify during execution):**
- FluentAssertions 6.4.0
- HtmlAgilityPack 1.11.40
- IdentityModel 6.0.0
- Microsoft.AspNetCore.Mvc.Testing 6.0.1 (may need update - verify)
- Microsoft.AspNetCore.TestHost 6.0.1 (may need update - verify)
- Microsoft.NET.Test.Sdk 17.0.0
- xunit 2.4.1
- xunit.runner.visualstudio 2.4.3

**Note:** Assessment shows compatible, but other test projects upgrade Mvc.Testing/TestHost. Verify if updates needed during execution.

**4. Expected Breaking Changes**

**Behavioral Changes (HIGH COUNT):**
- 11 behavioral change issues
- Authentication flow behavioral differences
- Token validation behavioral changes
- IdentityServer endpoints behavioral changes

**Source Incompatibilities:**
- 2 source incompatibility issues
- May affect test setup or assertions

**5. Code Modifications**
- Recompilation required
- Fix source incompatibility issues
- Update test assertions affected by behavioral changes
- Review authentication flow tests thoroughly
- Verify token issuance tests

**6. Testing Strategy**

**Execution:**
- Run all STS.Identity integration tests
- Verify authentication workflows
- Check token issuance tests
- Validate IdentityServer4 endpoint tests
- Test external provider authentication (if covered)

**Critical Focus:**
- Login flow tests
- Token generation tests
- Logout tests
- Discovery endpoint tests
- External authentication provider tests

**7. Validation Checklist**

- [ ] Project builds without errors
- [ ] Project builds without warnings
- [ ] All integration tests compile
- [ ] All integration tests run successfully
- [ ] Authentication flow tests pass
- [ ] Token issuance tests pass
- [ ] IdentityServer endpoint tests pass
- [ ] No behavioral regressions detected

---

## Package Update Reference

### Consolidated Package Updates by Tier

#### Tier 1: Foundation Libraries

**Shared.Configuration (CRITICAL - Security Fix):**
| Package | Current | Target | Severity | Notes |
|---------|---------|--------|----------|-------|
| **Azure.Identity** | 1.5.0 | 1.17.1 | **SECURITY** | Addresses MODERATE severity vulnerability |
| Microsoft.AspNetCore.DataProtection.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | Alignment |
| Microsoft.AspNetCore.Identity.UI | 6.0.1 | 10.0.3 | Update | Alignment |
| **Microsoft.Extensions.Configuration.AzureKeyVault** | 3.1.22 | **REPLACE** | **Deprecated** | Use Azure.Extensions.AspNetCore.Configuration.Secrets |

**BusinessLogic.Shared & EF.Extensions:**
- No package updates required

---

#### Tier 2: Data Foundation

**Admin.EntityFramework:**
| Package | Current | Target | Severity | Notes |
|---------|---------|--------|----------|-------|
| Microsoft.EntityFrameworkCore.Relational | 6.0.1 | 10.0.3 | Update | Core EF abstractions |

---

#### Tier 3: Business Logic & Identity Data

**Admin.EntityFramework.Identity:**
| Package | Current | Target | Severity | Notes |
|---------|---------|--------|----------|-------|
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | Identity data layer |

**Admin.BusinessLogic:**
- No package updates required (IdentityServer4.EntityFramework 4.1.2 compatible)

---

#### Tier 4: Identity Business Logic & Configuration

**Admin.BusinessLogic.Identity:**
| Package | Current | Target | Severity | Notes |
|---------|---------|--------|----------|-------|
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | Identity services |

**Admin.EntityFramework.Configuration:**
| Package | Current | Target | Severity | Notes |
|---------|---------|--------|----------|-------|
| Microsoft.AspNetCore.DataProtection.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | DataProtection persistence |
| Microsoft.EntityFrameworkCore.SqlServer | 6.0.1 | 10.0.3 | Update | Configuration DbContext |

---

#### Tier 5: Shared Components & UI

**Admin.EntityFramework.Shared:**
| Package | Current | Target | Severity | Notes |
|---------|---------|--------|----------|-------|
| Microsoft.AspNetCore.DataProtection.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | Shared DataProtection |

**Admin.UI (Razor Components):**
| Package | Current | Target | Severity | Notes |
|---------|---------|--------|----------|-------|
| Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation | 6.0.1 | 10.0.3 | Update | Razor development support |
| Microsoft.EntityFrameworkCore.InMemory | 6.0.1 | 10.0.3 | Update | In-memory testing |
| Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | Health checks |

**Shared:**
- Inherits packages from Shared.Configuration (security fix applied in Tier 1)

---

#### Tier 6: Database Providers & STS Application

**EF.SqlServer:**
| Package | Current | Target | Severity | Notes |
|---------|---------|--------|----------|-------|
| Microsoft.EntityFrameworkCore.SqlServer | 6.0.1 | 10.0.3 | Update | SqlServer provider |

**EF.PostgreSQL & EF.MySql:**
- No package updates required (Npgsql 6.0.2 and Pomelo 6.0.1 compatible with EF Core 10)

**STS.Identity (Authentication Server):**
| Package | Current | Target | Severity | Notes |
|---------|---------|--------|----------|-------|
| Microsoft.AspNetCore.DataProtection.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | DataProtection |
| Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | Diagnostics |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | Identity integration |
| Microsoft.AspNetCore.Identity.UI | 6.0.1 | 10.0.3 | Update | Identity UI |
| Microsoft.EntityFrameworkCore.InMemory | 6.0.1 | 10.0.3 | Update | Testing |
| Microsoft.EntityFrameworkCore.SqlServer | 6.0.1 | 10.0.3 | Update | Database |
| Microsoft.EntityFrameworkCore.Tools | 6.0.1 | 10.0.3 | Update | Migrations |
| Microsoft.Extensions.Diagnostics.HealthChecks | 6.0.1 | 10.0.3 | Update | Health checks |
| Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | EF health checks |
| **Microsoft.Identity.Web** | 1.22.1 | 4.3.0 | **Deprecated** | Major version update - review migration guide |
| **Microsoft.VisualStudio.Azure.Containers.Tools.Targets** | 1.14.0 | **REMOVE** | **Incompatible** | Docker tooling - remove or find alternative |

---

#### Tier 7: Main Applications

**Admin.Api (Web API):**
| Package | Current | Target | Severity | Notes |
|---------|---------|--------|----------|-------|
| Microsoft.AspNetCore.Authentication.JwtBearer | 6.0.1 | 10.0.3 | Update | JWT authentication |
| Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | Diagnostics |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | Identity |
| Microsoft.EntityFrameworkCore.InMemory | 6.0.1 | 10.0.3 | Update | Testing |
| Microsoft.EntityFrameworkCore.SqlServer | 6.0.1 | 10.0.3 | Update | Database |
| Microsoft.EntityFrameworkCore.Tools | 6.0.1 | 10.0.3 | Update | Migrations |
| Microsoft.Extensions.Diagnostics.HealthChecks | 6.0.1 | 10.0.3 | Update | Health checks |
| Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | EF health checks |
| **Microsoft.VisualStudio.Azure.Containers.Tools.Targets** | 1.14.0 | **REMOVE** | **Incompatible** | Docker tooling |

**Admin Portal (Razor Pages):**
| Package | Current | Target | Severity | Notes |
|---------|---------|--------|----------|-------|
| Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | Diagnostics |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 6.0.1 | 10.0.3 | Update | Identity |
| Microsoft.EntityFrameworkCore.SqlServer | 6.0.1 | 10.0.3 | Update | Database |
| Microsoft.EntityFrameworkCore.Tools | 6.0.1 | 10.0.3 | Update | Migrations |
| Microsoft.Extensions.Diagnostics.HealthChecks | 6.0.1 | 10.0.3 | Update | Health checks |
| Microsoft.Extensions.Options | 6.0.0 | 10.0.3 | Update | Configuration |
| Microsoft.VisualStudio.Web.CodeGeneration.Design | 6.0.1 | 10.0.2 | Update | Razor scaffolding |
| **Microsoft.EntityFrameworkCore.Tools.DotNet** | 2.0.3 | **REMOVE** | **Deprecated** | Functionality in SDK |
| **Microsoft.VisualStudio.Azure.Containers.Tools.Targets** | 1.14.0 | **REMOVE** | **Incompatible** | Docker tooling |

---

#### Tier 8: Test Projects

**Admin.Api.IntegrationTests:**
| Package | Current | Target | Severity | Notes |
|---------|---------|--------|----------|-------|
| Microsoft.AspNetCore.Mvc.Testing | 6.0.1 | 10.0.3 | Update | WebApplicationFactory |

**Admin.IntegrationTests:**
| Package | Current | Target | Severity | Notes |
|---------|---------|--------|----------|-------|
| Microsoft.AspNetCore.Mvc.Testing | 6.0.1 | 10.0.3 | Update | WebApplicationFactory |
| Microsoft.AspNetCore.TestHost | 6.0.1 | 10.0.3 | Update | Test hosting |

**Admin.UnitTests & STS.Identity.IntegrationTests:**
- No package updates required

---

### Package Update Summary

**Total Unique Packages Requiring Updates:** 17

**By Category:**
- **EF Core packages:** 5 (Relational, SqlServer, InMemory, Tools, Diagnostics.HealthChecks.EFCore)
- **ASP.NET Core packages:** 8 (Identity.EFCore, Identity.UI, DataProtection.EFCore, Diagnostics.EFCore, Mvc.Razor.RuntimeCompilation, Authentication.JwtBearer, Mvc.Testing, TestHost)
- **Extensions packages:** 3 (Diagnostics.HealthChecks, Options, Web.CodeGeneration.Design)
- **Azure packages:** 1 (Azure.Identity - security fix)

**Critical Actions:**
- **Security Fix:** Azure.Identity 1.5.0 → 1.17.1 (Tier 1)
- **Deprecated Replacements:**
  - Microsoft.Extensions.Configuration.AzureKeyVault → Azure.Extensions.AspNetCore.Configuration.Secrets (Tier 1)
  - Microsoft.Identity.Web 1.22.1 → 4.3.0 (Tier 6)
  - Microsoft.EntityFrameworkCore.Tools.DotNet → Remove (Tier 7)
- **Incompatible Removals:**
  - Microsoft.VisualStudio.Azure.Containers.Tools.Targets (Tiers 6-7, 3 projects)

**Compatible Packages (No Updates):**
- IdentityServer4 packages (AspNetIdentity 4.1.2, EntityFramework 4.1.2)
- Database providers (Npgsql 6.0.2, Pomelo 6.0.1)
- Logging (all Serilog packages)
- Health Checks (AspNetCore.HealthChecks.* packages)
- Testing frameworks (xUnit, FluentAssertions, Moq, Bogus)
- Utilities (AutoMapper, HtmlAgilityPack, Sendgrid, NWebsec)

---

## Breaking Changes Catalog

### Expected Breaking Changes by Category

#### 1. Configuration Binding (55 occurrences - HIGH IMPACT)

**Issue:** `IConfiguration.Get<T>()` method binary incompatibility

**Affected Files (sample):**
- Shared.Configuration: `Helpers/DockerHelpers.cs` (Line 14), `Helpers/StartupHelpers.cs` (Line 50, 83)
- STS.Identity: `Helpers/IdentityServerBuilderExtensions.cs` (Lines 27-28, 119-120), `Helpers/StartupHelpers.cs` (Line 455)
- Admin.Api: `Helpers/StartupHelpers.cs` (Line 295)
- Admin Portal: `Program.cs` (Lines 62-63)

**Breaking Change:** Method signature changed in .NET 10, requires recompilation and potentially code adjustments

**Migration Strategy:**
```csharp
// .NET 6 pattern (still works but may need adjustment):
var config = configuration.GetSection("SectionName").Get<ConfigType>();

// .NET 10 pattern (if binding issues occur):
var config = configuration.GetSection("SectionName").Get<ConfigType>(options => {
    options.BindNonPublicProperties = true; // if needed
});

// Or with null safety:
var config = configuration.GetSection("SectionName").Get<ConfigType>() 
    ?? throw new InvalidOperationException("Configuration section missing");
```

**Validation:** Test all configuration loading at startup, verify no null configuration objects

---

#### 2. Azure KeyVault Integration (24 occurrences - CRITICAL IMPACT)

**Issue:** Deprecated `Microsoft.Extensions.Configuration.AzureKeyVault` package APIs removed

**Affected Files:**
- Shared.Configuration: `Helpers/StartupHelpers.cs` (Lines 67-98)

**Breaking Change:** `AddAzureKeyVault()` extension methods and `DefaultKeyVaultSecretManager` no longer exist

**Migration Strategy:**

**OLD Pattern (.NET 6):**
```csharp
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Azure.KeyVault;

var keyVaultClient = new KeyVaultClient(...);
configurationBuilder.AddAzureKeyVault(
    endpoint, 
    keyVaultClient, 
    new DefaultKeyVaultSecretManager());

// OR
configurationBuilder.AddAzureKeyVault(endpoint, clientId, clientSecret);
```

**NEW Pattern (.NET 10):**
```csharp
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;

// Option 1: DefaultAzureCredential (recommended)
var secretClient = new SecretClient(
    new Uri(azureKeyVaultEndpoint), 
    new DefaultAzureCredential());
configurationBuilder.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());

// Option 2: ClientSecretCredential
var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
var secretClient = new SecretClient(new Uri(azureKeyVaultEndpoint), credential);
configurationBuilder.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
```

**Required Actions:**
1. Add package: `Azure.Extensions.AspNetCore.Configuration.Secrets`
2. Remove package: `Microsoft.Extensions.Configuration.AzureKeyVault`
3. Update using statements
4. Replace `AddAzureKeyVault` calls (2 occurrences - lines 89, 98)
5. Replace `DefaultKeyVaultSecretManager` with `KeyVaultSecretManager`

**Validation:** Test Azure KeyVault connectivity in all 3 applications, verify secrets loaded correctly

---

#### 3. JWT Security Token Handling (55 occurrences - HIGH IMPACT)

**Issue:** `JwtSecurityToken` and `JwtSecurityTokenHandler` APIs binary incompatible

**Affected Files:**
- Admin.UI: `Middlewares/AuthenticatedTestRequestMiddleware.cs` (Lines 22-23)
- Admin.Api: `Middlewares/AuthenticatedTestRequestMiddleware.cs` (Lines 24-25)
- Admin Portal: `Startup.cs` (Line 23)

**Breaking Changes:**

**A. JwtSecurityToken Constructor & Claims Property:**
```csharp
// Pattern (likely still works after recompilation):
var jwt = new JwtSecurityToken(token);
var claims = jwt.Claims; // Binary incompatibility
```

**B. JwtSecurityTokenHandler.DefaultInboundClaimTypeMap:**
```csharp
// .NET 6:
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// .NET 10: May need adjustment if API changed
// Verify property still exists or use alternative approach
```

**C. JwtBearerDefaults.AuthenticationScheme:**
```csharp
// Used in: Admin.Api/Middlewares/AuthenticatedTestRequestMiddleware.cs (Line 25)
// May have source incompatibility - verify field still available
```

**Migration Strategy:**
- Recompile and test
- If issues persist, consult .NET 10 JWT documentation for updated patterns
- May need to adjust claim type mapping approach
- Verify JWT validation configuration in authentication middleware

**Validation:** Full JWT authentication flows in Admin.Api, test middleware functionality

---

#### 4. X509Certificate2 Constructor (MEDIUM IMPACT)

**Issue:** `X509Certificate2(pfxPath, password)` constructor source incompatible

**Affected Files:**
- STS.Identity: `Helpers/IdentityServerBuilderExtensions.cs` (Lines 86, 163)

**Breaking Change:** Constructor signature or behavior changed for security improvements

**Migration Strategy:**
```csharp
// .NET 6:
new X509Certificate2(pfxFilePath, password)

// .NET 10 (may need explicit flags):
new X509Certificate2(pfxFilePath, password, X509KeyStorageFlags.MachineKeySet)

// Or use more secure loading pattern:
X509Certificate2.CreateFromPemFile(certPath, keyPath)
```

**Validation:** Test IdentityServer signing credential loading, verify token signing works

---

#### 5. OpenIdConnect Events & Options (MEDIUM-HIGH IMPACT)

**Issue:** OpenIdConnect event handler types and properties source incompatible

**Affected Files:**
- Admin.UI: `Helpers/StartupHelpers.cs` (Lines 400-423)
- STS.Identity: `Helpers/StartupHelpers.cs` (Line 395)

**Breaking Changes:**
- `OpenIdConnectEvents` constructor/properties
- `RedirectContext` and `MessageReceivedContext` types
- `OpenIdConnectOptions.ClientId` property
- Event handler signatures (OnRedirectToIdentityProvider, OnMessageReceived)

**Migration Strategy:**
```csharp
// Verify current pattern still works:
options.Events = new OpenIdConnectEvents
{
    OnMessageReceived = context => OnMessageReceived(context, config),
    OnRedirectToIdentityProvider = context => OnRedirectToIdentityProvider(context, config)
};

// If issues, update to .NET 10 event handler signatures
// Check: context.ProtocolMessage property availability
```

**Validation:** Test OpenIdConnect authentication, verify redirect flows, test external providers

---

#### 6. ForwardedHeaders Configuration (MEDIUM IMPACT)

**Issue:** `ForwardedHeadersOptions.KnownNetworks.Clear()` source incompatible

**Affected Files:**
- Admin.UI: `Helpers/StartupHelpers.cs` (Line 529)
- Admin.Api: `Helpers/StartupHelpers.cs` (Line 350)

**Breaking Change:** `KnownNetworks` property API changed

**Migration Strategy:**
```csharp
// .NET 6:
forwardingOptions.KnownNetworks.Clear();

// .NET 10: Property may be read-only or collection type changed
// Check documentation for correct pattern, possibly:
forwardingOptions.KnownNetworks = new List<IPNetwork>();
// Or property may be removed if defaults changed
```

**Validation:** Test reverse proxy scenarios, verify forwarded headers respected

---

#### 7. Microsoft.Identity.Web 1.x → 4.x (HIGH IMPACT)

**Issue:** Major version upgrade of deprecated package with breaking changes

**Affected Files:**
- STS.Identity: Authentication configuration throughout

**Breaking Changes (Major Version Jump):**
- Configuration API changes
- Authentication builder extension changes
- Token validation parameter changes
- Integration patterns with ASP.NET Core Identity

**Migration Strategy:**
1. Review Microsoft.Identity.Web 4.0 migration guide
2. Update authentication configuration patterns
3. Replace deprecated APIs with 4.x equivalents
4. Test authentication flows thoroughly

**Validation:** Full authentication testing, token validation, external provider integration

---

#### 8. Behavioral Changes (42 occurrences - TESTING CRITICAL)

**Issue:** Runtime behavior differences without compilation errors

**Categories:**

**A. System.Uri Behavioral Changes:**
- Admin.UI: `Helpers/StartupHelpers.cs` (Line 449) - Health check URI
- Admin.Api: `Helpers/StartupHelpers.cs` (Line 275) - Health check URI
- Shared.Configuration: `Helpers/StartupHelpers.cs` (Lines 67, 74) - KeyVault URIs

**Impact:** URI parsing, validation, or normalization may behave differently

**B. Middleware Behavioral Changes:**
- `UseExceptionHandler()` - Admin.UI (Line 595)
- `UseForwardedHeaders()` - Admin.UI (Line 532), Admin.Api (Line 353)

**Impact:** Exception handling flow or header forwarding logic may differ

**Migration Strategy:**
- Extensive testing required - behavioral changes don't cause compilation errors
- Compare runtime behavior before/after upgrade
- Review .NET 10 behavioral change documentation
- Test edge cases and error scenarios

**Validation:** Integration testing, error scenario testing, proxy configuration testing

---

### Breaking Change Resolution Order

**Phase 1 (Tier 1):**
1. Azure KeyVault deprecated package replacement (CRITICAL)
2. Azure.Identity security vulnerability fix (CRITICAL)
3. Configuration.Get<T>() recompilation

**Phase 2 (Tiers 2-5):**
4. Package version updates (EF Core, ASP.NET Core)
5. Configuration binding adjustments
6. OpenIdConnect events (Admin.UI)

**Phase 3 (Tier 6):**
7. X509Certificate2 constructor fixes (STS.Identity)
8. Microsoft.Identity.Web 1.x → 4.x migration (STS.Identity)
9. OpenIdConnect options (STS.Identity)

**Phase 4 (Tier 7):**
10. JWT token handling (Admin.Api, Admin Portal)
11. ForwardedHeaders configuration (Admin.Api, Admin Portal)
12. Razor runtime compilation (Admin Portal)

**Phase 5 (Tier 8):**
13. Test framework compatibility
14. Behavioral change testing and adjustments

---

## Testing & Validation Strategy
[To be filled]

---

## Risk Management

### High-Risk Changes

| Project | Risk Level | Description | Mitigation |
|---------|-----------|-------------|------------|
| Shared.Configuration | High | Security vulnerability (Azure.Identity 1.5.0), 2 deprecated packages, 24 total issues, affects all applications | Address security fix first, test Azure KeyVault integration thoroughly, verify all configuration paths |
| Admin.Api | High | 69 issues (16 mandatory), incompatible package (VisualStudio.Azure.Containers.Tools.Targets), most complex codebase | Incremental compilation fixes, extensive API endpoint testing, verify OpenAPI/Swagger compatibility |
| Admin Portal | High | Razor Pages application, 17 issues (8 mandatory), incompatible package, deprecated packages | Test all Razor Pages rendering, verify authentication flows, validate UI/UX consistency |
| STS.Identity | High | 33 issues (14 mandatory), authentication server, incompatible package, deprecated packages | Extensive authentication/token flow testing, verify IdentityServer4 compatibility with .NET 10 |
| Admin.UI | Medium-High | 39 issues (4 mandatory), Razor components library, API changes | Component-level testing, verify all shared UI elements render correctly |

### Security Vulnerabilities

| Package | Current Version | Secure Version | Projects Affected | CVE/Vulnerability | Remediation |
|---------|----------------|----------------|-------------------|-------------------|-------------|
| Azure.Identity | 1.5.0 | 1.17.1 | Shared.Configuration, Shared | Known security vulnerabilities in older versions | Upgrade to 1.17.1 during Tier 1 migration |

**Priority:** **CRITICAL** - Address during Tier 1 upgrade (Shared.Configuration)

### Deprecated Packages

| Package | Current Version | Projects Affected | Replacement Strategy |
|---------|----------------|-------------------|---------------------|
| Microsoft.EntityFrameworkCore.Tools.DotNet | 2.0.3 | (Unknown - needs verification) | Remove - functionality integrated into SDK |
| Microsoft.Extensions.Configuration.AzureKeyVault | 3.1.22 | Shared.Configuration | Replace with `Azure.Extensions.AspNetCore.Configuration.Secrets` pattern or modern KeyVault integration |
| Microsoft.Identity.Web | 1.22.1 | Admin Portal, STS.Identity | Evaluate if still needed - may be superseded by direct Azure.Identity usage |

### Incompatible Packages

| Package | Current Version | Projects Affected | Resolution Strategy |
|---------|----------------|-------------------|---------------------|
| Microsoft.VisualStudio.Azure.Containers.Tools.Targets | 1.14.0 | Admin.Api, Admin Portal, STS.Identity | Update to compatible version or remove if only used for Docker tooling (check actual usage) |

### Breaking Change Categories

**Binary Incompatibility (55 occurrences):**
- API changes requiring recompilation
- Affects multiple projects in Tiers 6-7
- **Mitigation:** Systematic recompilation tier-by-tier ensures all references updated

**Source Incompatibility (84 occurrences):**
- Code changes required for compilation
- Spread across Admin.Api (highest concentration), UI, STS.Identity
- **Mitigation:** Address during compilation phase, leverage IDE refactoring tools, apply patterns learned in early tiers

**Behavioral Changes (42 occurrences):**
- Runtime behavior differences (no compilation errors)
- Requires thorough testing to detect
- **Mitigation:** Comprehensive testing at each tier, integration tests for cross-tier interactions

### Contingency Plans

**If Tier Upgrade Fails:**
1. **Compilation Errors Unresolvable:**
   - Document blocking issue
   - Investigate alternative packages/approaches
   - Consider skipping tier temporarily and addressing in next iteration
   - Rollback tier changes and reassess

2. **Test Failures in Tier:**
   - Isolate failing tests
   - Determine if tier-specific or regression in lower tier
   - Fix tier-specific issues before proceeding
   - Re-validate lower tiers if regression suspected

3. **Incompatible Package Has No Replacement:**
   - Research .NET 10 alternatives
   - Evaluate functionality necessity
   - Consider feature removal if non-critical
   - Consult package maintainer/community

4. **Performance Degradation:**
   - Profile before/after comparison
   - Identify .NET 10-specific behavior changes
   - Apply performance optimizations from .NET 10 migration guides
   - Consider selective rollback if critical

**Rollback Strategy:**
- Branch-based approach allows clean revert: `git reset --hard` to pre-tier commit
- Each tier committed separately with clear commit messages
- Can rollback entire upgrade: switch back to `master` branch

---

## Complexity & Effort Assessment

### Per-Tier Complexity

| Tier | Projects | Total Issues | Mandatory | Complexity | Dependencies | Risk | Rationale |
|------|----------|--------------|-----------|------------|--------------|------|-----------|
| **Tier 1** | 3 | 26 | 13 | **High** | None | High | Security vulnerability, deprecated packages, foundational impact |
| **Tier 2** | 1 | 2 | 1 | **Low** | Tier 1 | Medium | Single project, minimal issues, but core EF abstractions |
| **Tier 3** | 2 | 3 | 2 | **Low** | Tiers 1-2 | Medium | Framework updates only, established dependencies |
| **Tier 4** | 2 | 5 | 2 | **Medium** | Tiers 1-3 | Medium | Package updates, configuration layer |
| **Tier 5** | 3 | 47 | 6 | **Medium-High** | Tiers 1-4 | Medium | UI components (39 issues), shared libraries |
| **Tier 6** | 4 | 37 | 16 | **Medium** | Tiers 1-5 | High | STS.Identity critical (33 issues), DB providers straightforward |
| **Tier 7** | 2 | 86 | 24 | **High** | Tiers 1-6 | High | Admin.Api most complex (69 issues), Razor Pages portal |
| **Tier 8** | 4 | 50 | 8 | **Medium** | Tiers 1-7 | Medium | Test projects, integration concerns |

### Phase Complexity Assessment

**Phase 1 (Tiers 1-2): Foundation**
- **Overall Complexity:** High
- **Critical Success Factor:** Resolving security vulnerability and deprecated packages in Tier 1
- **Dependency Chain:** Establishes base for all 19 dependent projects
- **Estimated Relative Effort:** 20% of total migration effort

**Phase 2 (Tiers 3-4): Core Services**
- **Overall Complexity:** Medium
- **Critical Success Factor:** Maintaining business logic integrity and EF configuration compatibility
- **Dependency Chain:** Links foundation to shared components
- **Estimated Relative Effort:** 15% of total migration effort

**Phase 3 (Tiers 5-6): Data & UI**
- **Overall Complexity:** Medium-High
- **Critical Success Factor:** Admin.UI component compatibility (39 issues), STS.Identity authentication flows (33 issues)
- **Dependency Chain:** Completes infrastructure needed by applications
- **Estimated Relative Effort:** 30% of total migration effort

**Phase 4 (Tier 7): Applications**
- **Overall Complexity:** High
- **Critical Success Factor:** Admin.Api endpoint compatibility (69 issues), Razor Pages functionality (17 issues)
- **Dependency Chain:** Final application-level integration
- **Estimated Relative Effort:** 25% of total migration effort

**Phase 5 (Tier 8): Validation**
- **Overall Complexity:** Medium
- **Critical Success Factor:** All integration and unit tests passing
- **Dependency Chain:** Validates entire upgrade
- **Estimated Relative Effort:** 10% of total migration effort

### Resource Requirements

**Skills Needed:**
- **Essential:**
  - .NET 6 → .NET 10 migration experience
  - Entity Framework Core expertise
  - ASP.NET Core knowledge (middleware, authentication, Razor Pages)
  - IdentityServer4 understanding

- **Beneficial:**
  - Azure SDK experience (for KeyVault/Identity changes)
  - Razor Pages development
  - Multi-database support (SqlServer, PostgreSQL, MySql)

**Team Capacity:**
- **Sequential execution:** 1-2 developers can execute tier-by-tier
- **Parallel opportunities:** Tiers 1, 5, 6, 8 could use 2-3 developers if parallelized
- **Review capacity:** At least 1 additional person for tier validation

### Issue Distribution Insights

**Concentration Areas:**
1. **Admin.Api** (69 issues): Largest effort, plan for multiple compilation/fix cycles
2. **Admin.UI** (39 issues): Razor components may need template updates
3. **STS.Identity** (33 issues): Authentication flows require extensive testing
4. **Shared.Configuration** (24 issues): Security-critical, affects all applications

**Lower-Impact Projects:**
- 9 projects with ≤3 issues (primarily framework updates)
- Database provider projects (MySql, PostgreSQL) have minimal issues
- BusinessLogic projects relatively clean

---

## Source Control Strategy
[To be filled]

---

## Success Criteria
[To be filled]

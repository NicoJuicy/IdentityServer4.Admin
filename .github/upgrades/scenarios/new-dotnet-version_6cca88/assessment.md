# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v10.0.

## Table of Contents

- [Executive Summary](#executive-Summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
- [Aggregate NuGet packages details](#aggregate-nuget-packages-details)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Projects Relationship Graph](#projects-relationship-graph)
- [Project Details](#project-details)

  - [docker-compose.dcproj](#docker-composedcproj)
  - [src\Skoruba.IdentityServer4.Admin.Api\Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)
  - [src\Skoruba.IdentityServer4.Admin.BusinessLogic.Identity\Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj](#srcskorubaidentityserver4adminbusinesslogicidentityskorubaidentityserver4adminbusinesslogicidentitycsproj)
  - [src\Skoruba.IdentityServer4.Admin.BusinessLogic.Shared\Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.csproj](#srcskorubaidentityserver4adminbusinesslogicsharedskorubaidentityserver4adminbusinesslogicsharedcsproj)
  - [src\Skoruba.IdentityServer4.Admin.BusinessLogic\Skoruba.IdentityServer4.Admin.BusinessLogic.csproj](#srcskorubaidentityserver4adminbusinesslogicskorubaidentityserver4adminbusinesslogiccsproj)
  - [src\Skoruba.IdentityServer4.Admin.EntityFramework.Configuration\Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj](#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj)
  - [src\Skoruba.IdentityServer4.Admin.EntityFramework.Extensions\Skoruba.IdentityServer4.Admin.EntityFramework.Extensions.csproj](#srcskorubaidentityserver4adminentityframeworkextensionsskorubaidentityserver4adminentityframeworkextensionscsproj)
  - [src\Skoruba.IdentityServer4.Admin.EntityFramework.Identity\Skoruba.IdentityServer4.Admin.EntityFramework.Identity.csproj](#srcskorubaidentityserver4adminentityframeworkidentityskorubaidentityserver4adminentityframeworkidentitycsproj)
  - [src\Skoruba.IdentityServer4.Admin.EntityFramework.MySql\Skoruba.IdentityServer4.Admin.EntityFramework.MySql.csproj](#srcskorubaidentityserver4adminentityframeworkmysqlskorubaidentityserver4adminentityframeworkmysqlcsproj)
  - [src\Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL\Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL.csproj](#srcskorubaidentityserver4adminentityframeworkpostgresqlskorubaidentityserver4adminentityframeworkpostgresqlcsproj)
  - [src\Skoruba.IdentityServer4.Admin.EntityFramework.Shared\Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj](#srcskorubaidentityserver4adminentityframeworksharedskorubaidentityserver4adminentityframeworksharedcsproj)
  - [src\Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer\Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer.csproj](#srcskorubaidentityserver4adminentityframeworksqlserverskorubaidentityserver4adminentityframeworksqlservercsproj)
  - [src\Skoruba.IdentityServer4.Admin.EntityFramework\Skoruba.IdentityServer4.Admin.EntityFramework.csproj](#srcskorubaidentityserver4adminentityframeworkskorubaidentityserver4adminentityframeworkcsproj)
  - [src\Skoruba.IdentityServer4.Admin.UI\Skoruba.IdentityServer4.Admin.UI.csproj](#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj)
  - [src\Skoruba.IdentityServer4.Admin\Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)
  - [src\Skoruba.IdentityServer4.Shared.Configuration\Skoruba.IdentityServer4.Shared.Configuration.csproj](#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj)
  - [src\Skoruba.IdentityServer4.Shared\Skoruba.IdentityServer4.Shared.csproj](#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj)
  - [src\Skoruba.IdentityServer4.STS.Identity\Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj)
  - [tests\Skoruba.IdentityServer4.Admin.Api.IntegrationTests\Skoruba.IdentityServer4.Admin.Api.IntegrationTests.csproj](#testsskorubaidentityserver4adminapiintegrationtestsskorubaidentityserver4adminapiintegrationtestscsproj)
  - [tests\Skoruba.IdentityServer4.Admin.IntegrationTests\Skoruba.IdentityServer4.Admin.IntegrationTests.csproj](#testsskorubaidentityserver4adminintegrationtestsskorubaidentityserver4adminintegrationtestscsproj)
  - [tests\Skoruba.IdentityServer4.Admin.UnitTests\Skoruba.IdentityServer4.Admin.UnitTests.csproj](#testsskorubaidentityserver4adminunittestsskorubaidentityserver4adminunittestscsproj)
  - [tests\Skoruba.IdentityServer4.STS.Identity.IntegrationTests\Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj](#testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 22 | 21 require upgrade |
| Total NuGet Packages | 60 | 21 need upgrade |
| Total Code Files | 796 |  |
| Total Code Files with Incidents | 61 |  |
| Total Lines of Code | 59077 |  |
| Total Number of Issues | 256 |  |
| Estimated LOC to modify | 181+ | at least 0,3% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| [docker-compose.dcproj](#docker-composedcproj) |  | ✅ None | 0 | 0 |  | DotNetCoreApp, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin.Api\Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj) | net6.0 | 🟢 Low | 9 | 59 | 59+ | AspNetCore, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin.BusinessLogic.Identity\Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj](#srcskorubaidentityserver4adminbusinesslogicidentityskorubaidentityserver4adminbusinesslogicidentitycsproj) | net6.0 | 🟢 Low | 1 | 0 |  | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin.BusinessLogic.Shared\Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.csproj](#srcskorubaidentityserver4adminbusinesslogicsharedskorubaidentityserver4adminbusinesslogicsharedcsproj) | net6.0 | 🟢 Low | 0 | 0 |  | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin.BusinessLogic\Skoruba.IdentityServer4.Admin.BusinessLogic.csproj](#srcskorubaidentityserver4adminbusinesslogicskorubaidentityserver4adminbusinesslogiccsproj) | net6.0 | 🟢 Low | 0 | 0 |  | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin.EntityFramework.Configuration\Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj](#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj) | net6.0 | 🟢 Low | 2 | 0 |  | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin.EntityFramework.Extensions\Skoruba.IdentityServer4.Admin.EntityFramework.Extensions.csproj](#srcskorubaidentityserver4adminentityframeworkextensionsskorubaidentityserver4adminentityframeworkextensionscsproj) | net6.0 | 🟢 Low | 0 | 0 |  | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin.EntityFramework.Identity\Skoruba.IdentityServer4.Admin.EntityFramework.Identity.csproj](#srcskorubaidentityserver4adminentityframeworkidentityskorubaidentityserver4adminentityframeworkidentitycsproj) | net6.0 | 🟢 Low | 1 | 0 |  | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin.EntityFramework.MySql\Skoruba.IdentityServer4.Admin.EntityFramework.MySql.csproj](#srcskorubaidentityserver4adminentityframeworkmysqlskorubaidentityserver4adminentityframeworkmysqlcsproj) | net6.0 | 🟢 Low | 0 | 0 |  | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL\Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL.csproj](#srcskorubaidentityserver4adminentityframeworkpostgresqlskorubaidentityserver4adminentityframeworkpostgresqlcsproj) | net6.0 | 🟢 Low | 0 | 0 |  | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin.EntityFramework.Shared\Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj](#srcskorubaidentityserver4adminentityframeworksharedskorubaidentityserver4adminentityframeworksharedcsproj) | net6.0 | 🟢 Low | 1 | 0 |  | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer\Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer.csproj](#srcskorubaidentityserver4adminentityframeworksqlserverskorubaidentityserver4adminentityframeworksqlservercsproj) | net6.0 | 🟢 Low | 1 | 0 |  | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin.EntityFramework\Skoruba.IdentityServer4.Admin.EntityFramework.csproj](#srcskorubaidentityserver4adminentityframeworkskorubaidentityserver4adminentityframeworkcsproj) | net6.0 | 🟢 Low | 1 | 0 |  | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin.UI\Skoruba.IdentityServer4.Admin.UI.csproj](#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj) | net6.0 | 🟢 Low | 3 | 35 | 35+ | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Admin\Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj) | net6.0 | 🟢 Low | 9 | 7 | 7+ | AspNetCore, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Shared.Configuration\Skoruba.IdentityServer4.Shared.Configuration.csproj](#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj) | net6.0 | 🟢 Low | 5 | 18 | 18+ | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.Shared\Skoruba.IdentityServer4.Shared.csproj](#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj) | net6.0 | 🟢 Low | 5 | 0 |  | ClassLibrary, Sdk Style = True |
| [src\Skoruba.IdentityServer4.STS.Identity\Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | net6.0 | 🟢 Low | 11 | 21 | 21+ | AspNetCore, Sdk Style = True |
| [tests\Skoruba.IdentityServer4.Admin.Api.IntegrationTests\Skoruba.IdentityServer4.Admin.Api.IntegrationTests.csproj](#testsskorubaidentityserver4adminapiintegrationtestsskorubaidentityserver4adminapiintegrationtestscsproj) | net6.0 | 🟢 Low | 1 | 12 | 12+ | DotNetCoreApp, Sdk Style = True |
| [tests\Skoruba.IdentityServer4.Admin.IntegrationTests\Skoruba.IdentityServer4.Admin.IntegrationTests.csproj](#testsskorubaidentityserver4adminintegrationtestsskorubaidentityserver4adminintegrationtestscsproj) | net6.0 | 🟢 Low | 2 | 14 | 14+ | DotNetCoreApp, Sdk Style = True |
| [tests\Skoruba.IdentityServer4.Admin.UnitTests\Skoruba.IdentityServer4.Admin.UnitTests.csproj](#testsskorubaidentityserver4adminunittestsskorubaidentityserver4adminunittestscsproj) | net6.0 | 🟢 Low | 0 | 2 | 2+ | DotNetCoreApp, Sdk Style = True |
| [tests\Skoruba.IdentityServer4.STS.Identity.IntegrationTests\Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj](#testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj) | net6.0 | 🟢 Low | 2 | 13 | 13+ | DotNetCoreApp, Sdk Style = True |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 39 | 65,0% |
| ⚠️ Incompatible | 4 | 6,7% |
| 🔄 Upgrade Recommended | 17 | 28,3% |
| ***Total NuGet Packages*** | ***60*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 55 | High - Require code changes |
| 🟡 Source Incompatible | 84 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 42 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 151535 |  |
| ***Total APIs Analyzed*** | ***151716*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| AspNet.Security.OAuth.GitHub | 6.0.3 |  | [Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| AspNetCore.HealthChecks.MySql | 6.0.1 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.UI.csproj](#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| AspNetCore.HealthChecks.NpgSql | 6.0.1 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.UI.csproj](#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| AspNetCore.HealthChecks.OpenIdConnectServer | 6.0.1 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.UI.csproj](#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj) | ✅Compatible |
| AspNetCore.HealthChecks.SqlServer | 6.0.1 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.UI.csproj](#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| AspNetCore.HealthChecks.UI | 6.0.2 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| AspNetCore.HealthChecks.UI.Client | 6.0.2 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.UI.csproj](#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| AutoMapper | 10.1.1 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj) | ✅Compatible |
| Azure.Extensions.AspNetCore.DataProtection.Keys | 1.1.0 |  | [Skoruba.IdentityServer4.Shared.Configuration.csproj](#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj)<br/>[Skoruba.IdentityServer4.Shared.csproj](#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj) | ✅Compatible |
| Azure.Identity | 1.5.0 | 1.17.1 | [Skoruba.IdentityServer4.Shared.Configuration.csproj](#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj)<br/>[Skoruba.IdentityServer4.Shared.csproj](#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj) | NuGet package contains security vulnerability |
| Azure.Security.KeyVault.Certificates | 4.2.0 |  | [Skoruba.IdentityServer4.Shared.Configuration.csproj](#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj)<br/>[Skoruba.IdentityServer4.Shared.csproj](#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj) | ✅Compatible |
| Bogus | 34.0.1 |  | [Skoruba.IdentityServer4.Admin.UnitTests.csproj](#testsskorubaidentityserver4adminunittestsskorubaidentityserver4adminunittestscsproj) | ✅Compatible |
| coverlet.collector | 3.1.0 |  | [Skoruba.IdentityServer4.Admin.Api.IntegrationTests.csproj](#testsskorubaidentityserver4adminapiintegrationtestsskorubaidentityserver4adminapiintegrationtestscsproj) | ✅Compatible |
| coverlet.msbuild | 3.1.0 |  | [Skoruba.IdentityServer4.Admin.IntegrationTests.csproj](#testsskorubaidentityserver4adminintegrationtestsskorubaidentityserver4adminintegrationtestscsproj)<br/>[Skoruba.IdentityServer4.Admin.UnitTests.csproj](#testsskorubaidentityserver4adminunittestsskorubaidentityserver4adminunittestscsproj) | ✅Compatible |
| FluentAssertions | 6.4.0 |  | [Skoruba.IdentityServer4.Admin.Api.IntegrationTests.csproj](#testsskorubaidentityserver4adminapiintegrationtestsskorubaidentityserver4adminapiintegrationtestscsproj)<br/>[Skoruba.IdentityServer4.Admin.IntegrationTests.csproj](#testsskorubaidentityserver4adminintegrationtestsskorubaidentityserver4adminintegrationtestscsproj)<br/>[Skoruba.IdentityServer4.Admin.UnitTests.csproj](#testsskorubaidentityserver4adminunittestsskorubaidentityserver4adminunittestscsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj](#testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj) | ✅Compatible |
| HtmlAgilityPack | 1.11.40 |  | [Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj](#testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj) | ✅Compatible |
| IdentityModel | 6.0.0 |  | [Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj](#testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj) | ✅Compatible |
| IdentityServer4.AspNetIdentity | 4.1.2 |  | [Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| IdentityServer4.EntityFramework | 4.1.2 |  | [Skoruba.IdentityServer4.Admin.BusinessLogic.csproj](#srcskorubaidentityserver4adminbusinesslogicskorubaidentityserver4adminbusinesslogiccsproj)<br/>[Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj](#srcskorubaidentityserver4adminbusinesslogicidentityskorubaidentityserver4adminbusinesslogicidentitycsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.Admin.EntityFramework.csproj](#srcskorubaidentityserver4adminentityframeworkskorubaidentityserver4adminentityframeworkcsproj)<br/>[Skoruba.IdentityServer4.Admin.EntityFramework.Identity.csproj](#srcskorubaidentityserver4adminentityframeworkidentityskorubaidentityserver4adminentityframeworkidentitycsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| Microsoft.AspNetCore.Authentication.JwtBearer | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj) | NuGet package upgrade is recommended |
| Microsoft.AspNetCore.DataProtection.EntityFrameworkCore | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj](#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj)<br/>[Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj](#srcskorubaidentityserver4adminentityframeworksharedskorubaidentityserver4adminentityframeworksharedcsproj)<br/>[Skoruba.IdentityServer4.Shared.Configuration.csproj](#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj)<br/>[Skoruba.IdentityServer4.Shared.csproj](#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | NuGet package upgrade is recommended |
| Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | NuGet package upgrade is recommended |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj](#srcskorubaidentityserver4adminbusinesslogicidentityskorubaidentityserver4adminbusinesslogicidentitycsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.Admin.EntityFramework.Identity.csproj](#srcskorubaidentityserver4adminentityframeworkidentityskorubaidentityserver4adminentityframeworkidentitycsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | NuGet package upgrade is recommended |
| Microsoft.AspNetCore.Identity.UI | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Shared.Configuration.csproj](#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj)<br/>[Skoruba.IdentityServer4.Shared.csproj](#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | NuGet package upgrade is recommended |
| Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Admin.UI.csproj](#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj) | NuGet package upgrade is recommended |
| Microsoft.AspNetCore.Mvc.Testing | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Admin.Api.IntegrationTests.csproj](#testsskorubaidentityserver4adminapiintegrationtestsskorubaidentityserver4adminapiintegrationtestscsproj)<br/>[Skoruba.IdentityServer4.Admin.IntegrationTests.csproj](#testsskorubaidentityserver4adminintegrationtestsskorubaidentityserver4adminintegrationtestscsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj](#testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj) | NuGet package upgrade is recommended |
| Microsoft.AspNetCore.TestHost | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Admin.IntegrationTests.csproj](#testsskorubaidentityserver4adminintegrationtestsskorubaidentityserver4adminintegrationtestscsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj](#testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj) | NuGet package upgrade is recommended |
| Microsoft.EntityFrameworkCore.InMemory | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.UI.csproj](#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | NuGet package upgrade is recommended |
| Microsoft.EntityFrameworkCore.Relational | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Admin.EntityFramework.csproj](#srcskorubaidentityserver4adminentityframeworkskorubaidentityserver4adminentityframeworkcsproj) | NuGet package upgrade is recommended |
| Microsoft.EntityFrameworkCore.SqlServer | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj](#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj)<br/>[Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer.csproj](#srcskorubaidentityserver4adminentityframeworksqlserverskorubaidentityserver4adminentityframeworksqlservercsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | NuGet package upgrade is recommended |
| Microsoft.EntityFrameworkCore.Tools | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | NuGet package upgrade is recommended |
| Microsoft.EntityFrameworkCore.Tools.DotNet | 2.0.3 |  | [Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj) | ⚠️NuGet package is deprecated |
| Microsoft.Extensions.Configuration.AzureKeyVault | 3.1.22 |  | [Skoruba.IdentityServer4.Shared.Configuration.csproj](#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj)<br/>[Skoruba.IdentityServer4.Shared.csproj](#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj) | ⚠️NuGet package is deprecated |
| Microsoft.Extensions.Diagnostics.HealthChecks | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | NuGet package upgrade is recommended |
| Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore | 6.0.1 | 10.0.3 | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.UI.csproj](#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | NuGet package upgrade is recommended |
| Microsoft.Extensions.Options | 6.0.0 | 10.0.3 | [Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj) | NuGet package upgrade is recommended |
| Microsoft.Identity.Web | 1.22.1 |  | [Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ⚠️NuGet package is deprecated |
| Microsoft.NET.Test.Sdk | 17.0.0 |  | [Skoruba.IdentityServer4.Admin.Api.IntegrationTests.csproj](#testsskorubaidentityserver4adminapiintegrationtestsskorubaidentityserver4adminapiintegrationtestscsproj)<br/>[Skoruba.IdentityServer4.Admin.IntegrationTests.csproj](#testsskorubaidentityserver4adminintegrationtestsskorubaidentityserver4adminintegrationtestscsproj)<br/>[Skoruba.IdentityServer4.Admin.UnitTests.csproj](#testsskorubaidentityserver4adminunittestsskorubaidentityserver4adminunittestscsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj](#testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj) | ✅Compatible |
| Microsoft.VisualStudio.Azure.Containers.Tools.Targets | 1.14.0 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ⚠️NuGet package is incompatible |
| Microsoft.VisualStudio.Web.CodeGeneration.Design | 6.0.1 | 10.0.2 | [Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj) | NuGet package upgrade is recommended |
| Moq | 4.16.1 |  | [Skoruba.IdentityServer4.Admin.UnitTests.csproj](#testsskorubaidentityserver4adminunittestsskorubaidentityserver4adminunittestscsproj) | ✅Compatible |
| Npgsql.EntityFrameworkCore.PostgreSQL | 6.0.2 |  | [Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj](#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj)<br/>[Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL.csproj](#srcskorubaidentityserver4adminentityframeworkpostgresqlskorubaidentityserver4adminentityframeworkpostgresqlcsproj) | ✅Compatible |
| NWebsec.AspNetCore.Middleware | 3.0.0 |  | [Skoruba.IdentityServer4.Admin.UI.csproj](#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| Pomelo.EntityFrameworkCore.MySql | 6.0.1 |  | [Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj](#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj)<br/>[Skoruba.IdentityServer4.Admin.EntityFramework.MySql.csproj](#srcskorubaidentityserver4adminentityframeworkmysqlskorubaidentityserver4adminentityframeworkmysqlcsproj) | ✅Compatible |
| Pomelo.EntityFrameworkCore.MySql.Design | 1.1.2 |  | [Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj](#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj)<br/>[Skoruba.IdentityServer4.Admin.EntityFramework.MySql.csproj](#srcskorubaidentityserver4adminentityframeworkmysqlskorubaidentityserver4adminentityframeworkmysqlcsproj) | ✅Compatible |
| Sendgrid | 9.25.2 |  | [Skoruba.IdentityServer4.Shared.Configuration.csproj](#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj)<br/>[Skoruba.IdentityServer4.Shared.csproj](#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj) | ✅Compatible |
| Serilog | 2.10.0 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| Serilog.Enrichers.Environment | 2.2.0 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| Serilog.Enrichers.Thread | 3.1.0 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| Serilog.Extensions.Hosting | 4.2.0 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| Serilog.Settings.Configuration | 3.3.0 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| Serilog.Sinks.Console | 4.0.1 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| Serilog.Sinks.File | 5.0.0 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| Serilog.Sinks.MSSqlServer | 5.6.1 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| Serilog.Sinks.Seq | 5.1.1 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj)<br/>[Skoruba.IdentityServer4.Admin.csproj](#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| Skoruba.AuditLogging.EntityFramework | 1.0.0 |  | [Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj](#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj)<br/>[Skoruba.IdentityServer4.Admin.EntityFramework.csproj](#srcskorubaidentityserver4adminentityframeworkskorubaidentityserver4adminentityframeworkcsproj)<br/>[Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj](#srcskorubaidentityserver4adminentityframeworksharedskorubaidentityserver4adminentityframeworksharedcsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.csproj](#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj) | ✅Compatible |
| Swashbuckle.AspNetCore | 6.2.3 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj) | ✅Compatible |
| Swashbuckle.AspNetCore.Swagger | 6.2.3 |  | [Skoruba.IdentityServer4.Admin.Api.csproj](#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj) | ✅Compatible |
| xunit | 2.4.1 |  | [Skoruba.IdentityServer4.Admin.Api.IntegrationTests.csproj](#testsskorubaidentityserver4adminapiintegrationtestsskorubaidentityserver4adminapiintegrationtestscsproj)<br/>[Skoruba.IdentityServer4.Admin.IntegrationTests.csproj](#testsskorubaidentityserver4adminintegrationtestsskorubaidentityserver4adminintegrationtestscsproj)<br/>[Skoruba.IdentityServer4.Admin.UnitTests.csproj](#testsskorubaidentityserver4adminunittestsskorubaidentityserver4adminunittestscsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj](#testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj) | ✅Compatible |
| xunit.runner.visualstudio | 2.4.3 |  | [Skoruba.IdentityServer4.Admin.Api.IntegrationTests.csproj](#testsskorubaidentityserver4adminapiintegrationtestsskorubaidentityserver4adminapiintegrationtestscsproj)<br/>[Skoruba.IdentityServer4.Admin.IntegrationTests.csproj](#testsskorubaidentityserver4adminintegrationtestsskorubaidentityserver4adminintegrationtestscsproj)<br/>[Skoruba.IdentityServer4.Admin.UnitTests.csproj](#testsskorubaidentityserver4adminunittestsskorubaidentityserver4adminunittestscsproj)<br/>[Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj](#testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj) | ✅Compatible |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| IdentityModel & Claims-based Security | 24 | 13,3% | Windows Identity Foundation (WIF), SAML, and claims-based authentication APIs that have been replaced by modern identity libraries. WIF was the original identity framework for .NET Framework. Migrate to Microsoft.IdentityModel.* packages (modern identity stack). |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |
| M:Microsoft.Extensions.Configuration.ConfigurationBinder.Get''1(Microsoft.Extensions.Configuration.IConfiguration) | 26 | 14,4% | Binary Incompatible |
| T:System.Uri | 26 | 14,4% | Behavioral Change |
| T:Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults | 13 | 7,2% | Source Incompatible |
| F:Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme | 13 | 7,2% | Source Incompatible |
| M:System.Uri.#ctor(System.String) | 6 | 3,3% | Behavioral Change |
| T:Microsoft.Extensions.DependencyInjection.IdentityEntityFrameworkBuilderExtensions | 6 | 3,3% | Source Incompatible |
| M:Microsoft.Extensions.DependencyInjection.IdentityEntityFrameworkBuilderExtensions.AddEntityFrameworkStores''1(Microsoft.AspNetCore.Identity.IdentityBuilder) | 6 | 3,3% | Source Incompatible |
| T:System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler | 6 | 3,3% | Binary Incompatible |
| T:Microsoft.AspNetCore.Hosting.WebHostBuilder | 6 | 3,3% | Source Incompatible |
| T:System.Net.Http.HttpContent | 6 | 3,3% | Behavioral Change |
| T:System.IdentityModel.Tokens.Jwt.JwtSecurityToken | 4 | 2,2% | Binary Incompatible |
| F:System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultInboundClaimTypeMap | 4 | 2,2% | Binary Incompatible |
| M:Microsoft.AspNetCore.Builder.ForwardedHeadersExtensions.UseForwardedHeaders(Microsoft.AspNetCore.Builder.IApplicationBuilder,Microsoft.AspNetCore.Builder.ForwardedHeadersOptions) | 3 | 1,7% | Behavioral Change |
| P:Microsoft.AspNetCore.Builder.ForwardedHeadersOptions.KnownNetworks | 3 | 1,7% | Source Incompatible |
| P:System.IdentityModel.Tokens.Jwt.JwtSecurityToken.Claims | 2 | 1,1% | Binary Incompatible |
| M:System.IdentityModel.Tokens.Jwt.JwtSecurityToken.#ctor(System.String) | 2 | 1,1% | Binary Incompatible |
| M:System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.#ctor | 2 | 1,1% | Binary Incompatible |
| M:System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.WriteToken(Microsoft.IdentityModel.Tokens.SecurityToken) | 2 | 1,1% | Binary Incompatible |
| M:System.IdentityModel.Tokens.Jwt.JwtSecurityToken.#ctor(System.String,System.String,System.Collections.Generic.IEnumerable{System.Security.Claims.Claim},System.Nullable{System.DateTime},System.Nullable{System.DateTime},Microsoft.IdentityModel.Tokens.SigningCredentials) | 2 | 1,1% | Binary Incompatible |
| T:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents | 2 | 1,1% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectOptions.Scope | 2 | 1,1% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectOptions.ClientSecret | 2 | 1,1% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectOptions.ClientId | 2 | 1,1% | Source Incompatible |
| M:System.TimeSpan.FromDays(System.Double) | 2 | 1,1% | Source Incompatible |
| T:Microsoft.Extensions.Configuration.AzureKeyVaultConfigurationExtensions | 2 | 1,1% | Source Incompatible |
| M:System.Security.Cryptography.X509Certificates.X509Certificate2.#ctor(System.String,System.String) | 2 | 1,1% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerOptions.Audience | 1 | 0,6% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerOptions.RequireHttpsMetadata | 1 | 0,6% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerOptions.Authority | 1 | 0,6% | Source Incompatible |
| T:Microsoft.Extensions.DependencyInjection.JwtBearerExtensions | 1 | 0,6% | Source Incompatible |
| M:Microsoft.Extensions.DependencyInjection.JwtBearerExtensions.AddJwtBearer(Microsoft.AspNetCore.Authentication.AuthenticationBuilder,System.String,System.Action{Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerOptions}) | 1 | 0,6% | Source Incompatible |
| M:Microsoft.Extensions.DependencyInjection.RazorRuntimeCompilationMvcBuilderExtensions.AddRazorRuntimeCompilation(Microsoft.Extensions.DependencyInjection.IMvcBuilder,System.Action{Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation.MvcRazorRuntimeCompilationOptions}) | 1 | 0,6% | Source Incompatible |
| M:Microsoft.AspNetCore.Builder.ExceptionHandlerExtensions.UseExceptionHandler(Microsoft.AspNetCore.Builder.IApplicationBuilder,System.String) | 1 | 0,6% | Behavioral Change |
| T:Microsoft.AspNetCore.Authentication.OpenIdConnect.RedirectContext | 1 | 0,6% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.OpenIdConnect.RedirectContext.ProtocolMessage | 1 | 0,6% | Source Incompatible |
| T:Microsoft.AspNetCore.Authentication.OpenIdConnect.MessageReceivedContext | 1 | 0,6% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents.OnRedirectToIdentityProvider | 1 | 0,6% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents.OnMessageReceived | 1 | 0,6% | Source Incompatible |
| M:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents.#ctor | 1 | 0,6% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectOptions.Events | 1 | 0,6% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectOptions.TokenValidationParameters | 1 | 0,6% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectOptions.GetClaimsFromUserInfoEndpoint | 1 | 0,6% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectOptions.ClaimActions | 1 | 0,6% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectOptions.ResponseType | 1 | 0,6% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectOptions.RequireHttpsMetadata | 1 | 0,6% | Source Incompatible |
| P:Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectOptions.Authority | 1 | 0,6% | Source Incompatible |
| T:Microsoft.Extensions.DependencyInjection.OpenIdConnectExtensions | 1 | 0,6% | Source Incompatible |
| M:Microsoft.Extensions.DependencyInjection.OpenIdConnectExtensions.AddOpenIdConnect(Microsoft.AspNetCore.Authentication.AuthenticationBuilder,System.String,System.Action{Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectOptions}) | 1 | 0,6% | Source Incompatible |
| M:Microsoft.Extensions.DependencyInjection.RazorRuntimeCompilationMvcBuilderExtensions.AddRazorRuntimeCompilation(Microsoft.Extensions.DependencyInjection.IMvcBuilder) | 1 | 0,6% | Source Incompatible |
| T:Microsoft.Extensions.Configuration.AzureKeyVault.DefaultKeyVaultSecretManager | 1 | 0,6% | Binary Incompatible |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.csproj</b><br/><small>net6.0</small>"]
    P2["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.IntegrationTests.csproj</b><br/><small>net6.0</small>"]
    P3["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.UnitTests.csproj</b><br/><small>net6.0</small>"]
    P4["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.csproj</b><br/><small>net6.0</small>"]
    P5["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.csproj</b><br/><small>net6.0</small>"]
    P6["<b>📦&nbsp;Skoruba.IdentityServer4.STS.Identity.csproj</b><br/><small>net6.0</small>"]
    P7["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Identity.csproj</b><br/><small>net6.0</small>"]
    P8["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.csproj</b><br/><small>net6.0</small>"]
    P9["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj</b><br/><small>net6.0</small>"]
    P10["<b>📦&nbsp;Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj</b><br/><small>net6.0</small>"]
    P11["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.Api.csproj</b><br/><small>net6.0</small>"]
    P12["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj</b><br/><small>net6.0</small>"]
    P13["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Extensions.csproj</b><br/><small>net6.0</small>"]
    P14["<b>📦&nbsp;docker-compose.dcproj</b><br/><small></small>"]
    P15["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer.csproj</b><br/><small>net6.0</small>"]
    P16["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL.csproj</b><br/><small>net6.0</small>"]
    P17["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.MySql.csproj</b><br/><small>net6.0</small>"]
    P18["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.Api.IntegrationTests.csproj</b><br/><small>net6.0</small>"]
    P19["<b>📦&nbsp;Skoruba.IdentityServer4.Shared.csproj</b><br/><small>net6.0</small>"]
    P20["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.UI.csproj</b><br/><small>net6.0</small>"]
    P21["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj</b><br/><small>net6.0</small>"]
    P22["<b>📦&nbsp;Skoruba.IdentityServer4.Shared.Configuration.csproj</b><br/><small>net6.0</small>"]
    P1 --> P15
    P1 --> P20
    P1 --> P5
    P1 --> P19
    P1 --> P16
    P1 --> P17
    P2 --> P1
    P3 --> P1
    P4 --> P13
    P5 --> P4
    P5 --> P8
    P6 --> P12
    P6 --> P22
    P6 --> P21
    P6 --> P19
    P7 --> P4
    P9 --> P8
    P9 --> P7
    P10 --> P6
    P11 --> P22
    P11 --> P15
    P11 --> P9
    P11 --> P5
    P11 --> P19
    P11 --> P16
    P11 --> P17
    P12 --> P21
    P15 --> P12
    P16 --> P12
    P17 --> P12
    P18 --> P11
    P19 --> P9
    P20 --> P22
    P20 --> P9
    P20 --> P5
    P20 --> P21
    P21 --> P7
    click P1 "#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj"
    click P2 "#testsskorubaidentityserver4adminintegrationtestsskorubaidentityserver4adminintegrationtestscsproj"
    click P3 "#testsskorubaidentityserver4adminunittestsskorubaidentityserver4adminunittestscsproj"
    click P4 "#srcskorubaidentityserver4adminentityframeworkskorubaidentityserver4adminentityframeworkcsproj"
    click P5 "#srcskorubaidentityserver4adminbusinesslogicskorubaidentityserver4adminbusinesslogiccsproj"
    click P6 "#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj"
    click P7 "#srcskorubaidentityserver4adminentityframeworkidentityskorubaidentityserver4adminentityframeworkidentitycsproj"
    click P8 "#srcskorubaidentityserver4adminbusinesslogicsharedskorubaidentityserver4adminbusinesslogicsharedcsproj"
    click P9 "#srcskorubaidentityserver4adminbusinesslogicidentityskorubaidentityserver4adminbusinesslogicidentitycsproj"
    click P10 "#testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj"
    click P11 "#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj"
    click P12 "#srcskorubaidentityserver4adminentityframeworksharedskorubaidentityserver4adminentityframeworksharedcsproj"
    click P13 "#srcskorubaidentityserver4adminentityframeworkextensionsskorubaidentityserver4adminentityframeworkextensionscsproj"
    click P14 "#docker-composedcproj"
    click P15 "#srcskorubaidentityserver4adminentityframeworksqlserverskorubaidentityserver4adminentityframeworksqlservercsproj"
    click P16 "#srcskorubaidentityserver4adminentityframeworkpostgresqlskorubaidentityserver4adminentityframeworkpostgresqlcsproj"
    click P17 "#srcskorubaidentityserver4adminentityframeworkmysqlskorubaidentityserver4adminentityframeworkmysqlcsproj"
    click P18 "#testsskorubaidentityserver4adminapiintegrationtestsskorubaidentityserver4adminapiintegrationtestscsproj"
    click P19 "#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj"
    click P20 "#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj"
    click P21 "#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj"
    click P22 "#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj"

```

## Project Details

<a id="docker-composedcproj"></a>
### docker-compose.dcproj

#### Project Info

- **Current Target Framework:** ✅
- **SDK-style**: True
- **Project Kind:** DotNetCoreApp
- **Dependencies**: 0
- **Dependants**: 0
- **Number of Files**: 0
- **Lines of Code**: 0
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["docker-compose.dcproj"]
        MAIN["<b>📦&nbsp;docker-compose.dcproj</b><br/><small></small>"]
        click MAIN "#docker-composedcproj"
    end

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

<a id="srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj"></a>
### src\Skoruba.IdentityServer4.Admin.Api\Skoruba.IdentityServer4.Admin.Api.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** AspNetCore
- **Dependencies**: 7
- **Dependants**: 1
- **Number of Files**: 81
- **Number of Files with Incidents**: 5
- **Lines of Code**: 3546
- **Estimated LOC to modify**: 59+ (at least 1,7% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P18["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.Api.IntegrationTests.csproj</b><br/><small>net6.0</small>"]
        click P18 "#testsskorubaidentityserver4adminapiintegrationtestsskorubaidentityserver4adminapiintegrationtestscsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.Api.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.Api.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj"
    end
    subgraph downstream["Dependencies (7"]
        P22["<b>📦&nbsp;Skoruba.IdentityServer4.Shared.Configuration.csproj</b><br/><small>net6.0</small>"]
        P15["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer.csproj</b><br/><small>net6.0</small>"]
        P9["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj</b><br/><small>net6.0</small>"]
        P5["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.csproj</b><br/><small>net6.0</small>"]
        P19["<b>📦&nbsp;Skoruba.IdentityServer4.Shared.csproj</b><br/><small>net6.0</small>"]
        P16["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL.csproj</b><br/><small>net6.0</small>"]
        P17["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.MySql.csproj</b><br/><small>net6.0</small>"]
        click P22 "#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj"
        click P15 "#srcskorubaidentityserver4adminentityframeworksqlserverskorubaidentityserver4adminentityframeworksqlservercsproj"
        click P9 "#srcskorubaidentityserver4adminbusinesslogicidentityskorubaidentityserver4adminbusinesslogicidentitycsproj"
        click P5 "#srcskorubaidentityserver4adminbusinesslogicskorubaidentityserver4adminbusinesslogiccsproj"
        click P19 "#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj"
        click P16 "#srcskorubaidentityserver4adminentityframeworkpostgresqlskorubaidentityserver4adminentityframeworkpostgresqlcsproj"
        click P17 "#srcskorubaidentityserver4adminentityframeworkmysqlskorubaidentityserver4adminentityframeworkmysqlcsproj"
    end
    P18 --> MAIN
    MAIN --> P22
    MAIN --> P15
    MAIN --> P9
    MAIN --> P5
    MAIN --> P19
    MAIN --> P16
    MAIN --> P17

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 14 | High - Require code changes |
| 🟡 Source Incompatible | 36 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 9 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 4703 |  |
| ***Total APIs Analyzed*** | ***4762*** |  |

#### Project Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| IdentityModel & Claims-based Security | 7 | 11,9% | Windows Identity Foundation (WIF), SAML, and claims-based authentication APIs that have been replaced by modern identity libraries. WIF was the original identity framework for .NET Framework. Migrate to Microsoft.IdentityModel.* packages (modern identity stack). |

<a id="srcskorubaidentityserver4adminbusinesslogicidentityskorubaidentityserver4adminbusinesslogicidentitycsproj"></a>
### src\Skoruba.IdentityServer4.Admin.BusinessLogic.Identity\Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 2
- **Dependants**: 3
- **Number of Files**: 94
- **Number of Files with Incidents**: 1
- **Lines of Code**: 2863
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (3)"]
        P11["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.Api.csproj</b><br/><small>net6.0</small>"]
        P19["<b>📦&nbsp;Skoruba.IdentityServer4.Shared.csproj</b><br/><small>net6.0</small>"]
        P20["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.UI.csproj</b><br/><small>net6.0</small>"]
        click P11 "#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj"
        click P19 "#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj"
        click P20 "#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminbusinesslogicidentityskorubaidentityserver4adminbusinesslogicidentitycsproj"
    end
    subgraph downstream["Dependencies (2"]
        P8["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.csproj</b><br/><small>net6.0</small>"]
        P7["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Identity.csproj</b><br/><small>net6.0</small>"]
        click P8 "#srcskorubaidentityserver4adminbusinesslogicsharedskorubaidentityserver4adminbusinesslogicsharedcsproj"
        click P7 "#srcskorubaidentityserver4adminentityframeworkidentityskorubaidentityserver4adminentityframeworkidentitycsproj"
    end
    P11 --> MAIN
    P19 --> MAIN
    P20 --> MAIN
    MAIN --> P8
    MAIN --> P7

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 2472 |  |
| ***Total APIs Analyzed*** | ***2472*** |  |

<a id="srcskorubaidentityserver4adminbusinesslogicsharedskorubaidentityserver4adminbusinesslogicsharedcsproj"></a>
### src\Skoruba.IdentityServer4.Admin.BusinessLogic.Shared\Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 0
- **Dependants**: 2
- **Number of Files**: 6
- **Number of Files with Incidents**: 1
- **Lines of Code**: 105
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (2)"]
        P5["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.csproj</b><br/><small>net6.0</small>"]
        P9["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj</b><br/><small>net6.0</small>"]
        click P5 "#srcskorubaidentityserver4adminbusinesslogicskorubaidentityserver4adminbusinesslogiccsproj"
        click P9 "#srcskorubaidentityserver4adminbusinesslogicidentityskorubaidentityserver4adminbusinesslogicidentitycsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminbusinesslogicsharedskorubaidentityserver4adminbusinesslogicsharedcsproj"
    end
    P5 --> MAIN
    P9 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 164 |  |
| ***Total APIs Analyzed*** | ***164*** |  |

<a id="srcskorubaidentityserver4adminbusinesslogicskorubaidentityserver4adminbusinesslogiccsproj"></a>
### src\Skoruba.IdentityServer4.Admin.BusinessLogic\Skoruba.IdentityServer4.Admin.BusinessLogic.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 2
- **Dependants**: 3
- **Number of Files**: 143
- **Number of Files with Incidents**: 1
- **Lines of Code**: 5104
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (3)"]
        P1["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.csproj</b><br/><small>net6.0</small>"]
        P11["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.Api.csproj</b><br/><small>net6.0</small>"]
        P20["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.UI.csproj</b><br/><small>net6.0</small>"]
        click P1 "#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj"
        click P11 "#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj"
        click P20 "#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.BusinessLogic.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminbusinesslogicskorubaidentityserver4adminbusinesslogiccsproj"
    end
    subgraph downstream["Dependencies (2"]
        P4["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.csproj</b><br/><small>net6.0</small>"]
        P8["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.csproj</b><br/><small>net6.0</small>"]
        click P4 "#srcskorubaidentityserver4adminentityframeworkskorubaidentityserver4adminentityframeworkcsproj"
        click P8 "#srcskorubaidentityserver4adminbusinesslogicsharedskorubaidentityserver4adminbusinesslogicsharedcsproj"
    end
    P1 --> MAIN
    P11 --> MAIN
    P20 --> MAIN
    MAIN --> P4
    MAIN --> P8

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 5897 |  |
| ***Total APIs Analyzed*** | ***5897*** |  |

<a id="srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj"></a>
### src\Skoruba.IdentityServer4.Admin.EntityFramework.Configuration\Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 1
- **Dependants**: 3
- **Number of Files**: 14
- **Number of Files with Incidents**: 1
- **Lines of Code**: 458
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (3)"]
        P6["<b>📦&nbsp;Skoruba.IdentityServer4.STS.Identity.csproj</b><br/><small>net6.0</small>"]
        P12["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj</b><br/><small>net6.0</small>"]
        P20["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.UI.csproj</b><br/><small>net6.0</small>"]
        click P6 "#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj"
        click P12 "#srcskorubaidentityserver4adminentityframeworksharedskorubaidentityserver4adminentityframeworksharedcsproj"
        click P20 "#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj"
    end
    subgraph downstream["Dependencies (1"]
        P7["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Identity.csproj</b><br/><small>net6.0</small>"]
        click P7 "#srcskorubaidentityserver4adminentityframeworkidentityskorubaidentityserver4adminentityframeworkidentitycsproj"
    end
    P6 --> MAIN
    P12 --> MAIN
    P20 --> MAIN
    MAIN --> P7

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 565 |  |
| ***Total APIs Analyzed*** | ***565*** |  |

<a id="srcskorubaidentityserver4adminentityframeworkextensionsskorubaidentityserver4adminentityframeworkextensionscsproj"></a>
### src\Skoruba.IdentityServer4.Admin.EntityFramework.Extensions\Skoruba.IdentityServer4.Admin.EntityFramework.Extensions.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 0
- **Dependants**: 1
- **Number of Files**: 5
- **Number of Files with Incidents**: 1
- **Lines of Code**: 113
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P4["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.csproj</b><br/><small>net6.0</small>"]
        click P4 "#srcskorubaidentityserver4adminentityframeworkskorubaidentityserver4adminentityframeworkcsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.EntityFramework.Extensions.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Extensions.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminentityframeworkextensionsskorubaidentityserver4adminentityframeworkextensionscsproj"
    end
    P4 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 108 |  |
| ***Total APIs Analyzed*** | ***108*** |  |

<a id="srcskorubaidentityserver4adminentityframeworkidentityskorubaidentityserver4adminentityframeworkidentitycsproj"></a>
### src\Skoruba.IdentityServer4.Admin.EntityFramework.Identity\Skoruba.IdentityServer4.Admin.EntityFramework.Identity.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 1
- **Dependants**: 2
- **Number of Files**: 4
- **Number of Files with Incidents**: 1
- **Lines of Code**: 657
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (2)"]
        P9["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj</b><br/><small>net6.0</small>"]
        P21["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj</b><br/><small>net6.0</small>"]
        click P9 "#srcskorubaidentityserver4adminbusinesslogicidentityskorubaidentityserver4adminbusinesslogicidentitycsproj"
        click P21 "#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.EntityFramework.Identity.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Identity.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminentityframeworkidentityskorubaidentityserver4adminentityframeworkidentitycsproj"
    end
    subgraph downstream["Dependencies (1"]
        P4["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.csproj</b><br/><small>net6.0</small>"]
        click P4 "#srcskorubaidentityserver4adminentityframeworkskorubaidentityserver4adminentityframeworkcsproj"
    end
    P9 --> MAIN
    P21 --> MAIN
    MAIN --> P4

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 1166 |  |
| ***Total APIs Analyzed*** | ***1166*** |  |

<a id="srcskorubaidentityserver4adminentityframeworkmysqlskorubaidentityserver4adminentityframeworkmysqlcsproj"></a>
### src\Skoruba.IdentityServer4.Admin.EntityFramework.MySql\Skoruba.IdentityServer4.Admin.EntityFramework.MySql.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 1
- **Dependants**: 2
- **Number of Files**: 25
- **Number of Files with Incidents**: 1
- **Lines of Code**: 5533
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (2)"]
        P1["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.csproj</b><br/><small>net6.0</small>"]
        P11["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.Api.csproj</b><br/><small>net6.0</small>"]
        click P1 "#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj"
        click P11 "#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.EntityFramework.MySql.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.MySql.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminentityframeworkmysqlskorubaidentityserver4adminentityframeworkmysqlcsproj"
    end
    subgraph downstream["Dependencies (1"]
        P12["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj</b><br/><small>net6.0</small>"]
        click P12 "#srcskorubaidentityserver4adminentityframeworksharedskorubaidentityserver4adminentityframeworksharedcsproj"
    end
    P1 --> MAIN
    P11 --> MAIN
    MAIN --> P12

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 7222 |  |
| ***Total APIs Analyzed*** | ***7222*** |  |

<a id="srcskorubaidentityserver4adminentityframeworkpostgresqlskorubaidentityserver4adminentityframeworkpostgresqlcsproj"></a>
### src\Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL\Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 1
- **Dependants**: 2
- **Number of Files**: 25
- **Number of Files with Incidents**: 1
- **Lines of Code**: 5643
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (2)"]
        P1["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.csproj</b><br/><small>net6.0</small>"]
        P11["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.Api.csproj</b><br/><small>net6.0</small>"]
        click P1 "#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj"
        click P11 "#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminentityframeworkpostgresqlskorubaidentityserver4adminentityframeworkpostgresqlcsproj"
    end
    subgraph downstream["Dependencies (1"]
        P12["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj</b><br/><small>net6.0</small>"]
        click P12 "#srcskorubaidentityserver4adminentityframeworksharedskorubaidentityserver4adminentityframeworksharedcsproj"
    end
    P1 --> MAIN
    P11 --> MAIN
    MAIN --> P12

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 7665 |  |
| ***Total APIs Analyzed*** | ***7665*** |  |

<a id="srcskorubaidentityserver4adminentityframeworksharedskorubaidentityserver4adminentityframeworksharedcsproj"></a>
### src\Skoruba.IdentityServer4.Admin.EntityFramework.Shared\Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 1
- **Dependants**: 4
- **Number of Files**: 15
- **Number of Files with Incidents**: 1
- **Lines of Code**: 522
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (4)"]
        P6["<b>📦&nbsp;Skoruba.IdentityServer4.STS.Identity.csproj</b><br/><small>net6.0</small>"]
        P15["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer.csproj</b><br/><small>net6.0</small>"]
        P16["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL.csproj</b><br/><small>net6.0</small>"]
        P17["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.MySql.csproj</b><br/><small>net6.0</small>"]
        click P6 "#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj"
        click P15 "#srcskorubaidentityserver4adminentityframeworksqlserverskorubaidentityserver4adminentityframeworksqlservercsproj"
        click P16 "#srcskorubaidentityserver4adminentityframeworkpostgresqlskorubaidentityserver4adminentityframeworkpostgresqlcsproj"
        click P17 "#srcskorubaidentityserver4adminentityframeworkmysqlskorubaidentityserver4adminentityframeworkmysqlcsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminentityframeworksharedskorubaidentityserver4adminentityframeworksharedcsproj"
    end
    subgraph downstream["Dependencies (1"]
        P21["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj</b><br/><small>net6.0</small>"]
        click P21 "#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj"
    end
    P6 --> MAIN
    P15 --> MAIN
    P16 --> MAIN
    P17 --> MAIN
    MAIN --> P21

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 536 |  |
| ***Total APIs Analyzed*** | ***536*** |  |

<a id="srcskorubaidentityserver4adminentityframeworksqlserverskorubaidentityserver4adminentityframeworksqlservercsproj"></a>
### src\Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer\Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 1
- **Dependants**: 2
- **Number of Files**: 25
- **Number of Files with Incidents**: 1
- **Lines of Code**: 5670
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (2)"]
        P1["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.csproj</b><br/><small>net6.0</small>"]
        P11["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.Api.csproj</b><br/><small>net6.0</small>"]
        click P1 "#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj"
        click P11 "#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminentityframeworksqlserverskorubaidentityserver4adminentityframeworksqlservercsproj"
    end
    subgraph downstream["Dependencies (1"]
        P12["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj</b><br/><small>net6.0</small>"]
        click P12 "#srcskorubaidentityserver4adminentityframeworksharedskorubaidentityserver4adminentityframeworksharedcsproj"
    end
    P1 --> MAIN
    P11 --> MAIN
    MAIN --> P12

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 7575 |  |
| ***Total APIs Analyzed*** | ***7575*** |  |

<a id="srcskorubaidentityserver4adminentityframeworkskorubaidentityserver4adminentityframeworkcsproj"></a>
### src\Skoruba.IdentityServer4.Admin.EntityFramework\Skoruba.IdentityServer4.Admin.EntityFramework.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 1
- **Dependants**: 2
- **Number of Files**: 24
- **Number of Files with Incidents**: 1
- **Lines of Code**: 1798
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (2)"]
        P5["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.csproj</b><br/><small>net6.0</small>"]
        P7["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Identity.csproj</b><br/><small>net6.0</small>"]
        click P5 "#srcskorubaidentityserver4adminbusinesslogicskorubaidentityserver4adminbusinesslogiccsproj"
        click P7 "#srcskorubaidentityserver4adminentityframeworkidentityskorubaidentityserver4adminentityframeworkidentitycsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.EntityFramework.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminentityframeworkskorubaidentityserver4adminentityframeworkcsproj"
    end
    subgraph downstream["Dependencies (1"]
        P13["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Extensions.csproj</b><br/><small>net6.0</small>"]
        click P13 "#srcskorubaidentityserver4adminentityframeworkextensionsskorubaidentityserver4adminentityframeworkextensionscsproj"
    end
    P5 --> MAIN
    P7 --> MAIN
    MAIN --> P13

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 2738 |  |
| ***Total APIs Analyzed*** | ***2738*** |  |

<a id="srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj"></a>
### src\Skoruba.IdentityServer4.Admin.UI\Skoruba.IdentityServer4.Admin.UI.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 4
- **Dependants**: 1
- **Number of Files**: 1102
- **Number of Files with Incidents**: 4
- **Lines of Code**: 9798
- **Estimated LOC to modify**: 35+ (at least 0,4% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P1["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.csproj</b><br/><small>net6.0</small>"]
        click P1 "#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.UI.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.UI.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj"
    end
    subgraph downstream["Dependencies (4"]
        P22["<b>📦&nbsp;Skoruba.IdentityServer4.Shared.Configuration.csproj</b><br/><small>net6.0</small>"]
        P9["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj</b><br/><small>net6.0</small>"]
        P5["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.csproj</b><br/><small>net6.0</small>"]
        P21["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj</b><br/><small>net6.0</small>"]
        click P22 "#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj"
        click P9 "#srcskorubaidentityserver4adminbusinesslogicidentityskorubaidentityserver4adminbusinesslogicidentitycsproj"
        click P5 "#srcskorubaidentityserver4adminbusinesslogicskorubaidentityserver4adminbusinesslogiccsproj"
        click P21 "#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj"
    end
    P1 --> MAIN
    MAIN --> P22
    MAIN --> P9
    MAIN --> P5
    MAIN --> P21

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 3 | High - Require code changes |
| 🟡 Source Incompatible | 28 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 4 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 68374 |  |
| ***Total APIs Analyzed*** | ***68409*** |  |

#### Project Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| IdentityModel & Claims-based Security | 3 | 8,6% | Windows Identity Foundation (WIF), SAML, and claims-based authentication APIs that have been replaced by modern identity libraries. WIF was the original identity framework for .NET Framework. Migrate to Microsoft.IdentityModel.* packages (modern identity stack). |

<a id="srcskorubaidentityserver4adminskorubaidentityserver4admincsproj"></a>
### src\Skoruba.IdentityServer4.Admin\Skoruba.IdentityServer4.Admin.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** AspNetCore
- **Dependencies**: 6
- **Dependants**: 2
- **Number of Files**: 9
- **Number of Files with Incidents**: 4
- **Lines of Code**: 304
- **Estimated LOC to modify**: 7+ (at least 2,3% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (2)"]
        P2["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.IntegrationTests.csproj</b><br/><small>net6.0</small>"]
        P3["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.UnitTests.csproj</b><br/><small>net6.0</small>"]
        click P2 "#testsskorubaidentityserver4adminintegrationtestsskorubaidentityserver4adminintegrationtestscsproj"
        click P3 "#testsskorubaidentityserver4adminunittestsskorubaidentityserver4adminunittestscsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Admin.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj"
    end
    subgraph downstream["Dependencies (6"]
        P15["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.SqlServer.csproj</b><br/><small>net6.0</small>"]
        P20["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.UI.csproj</b><br/><small>net6.0</small>"]
        P5["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.csproj</b><br/><small>net6.0</small>"]
        P19["<b>📦&nbsp;Skoruba.IdentityServer4.Shared.csproj</b><br/><small>net6.0</small>"]
        P16["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.PostgreSQL.csproj</b><br/><small>net6.0</small>"]
        P17["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.MySql.csproj</b><br/><small>net6.0</small>"]
        click P15 "#srcskorubaidentityserver4adminentityframeworksqlserverskorubaidentityserver4adminentityframeworksqlservercsproj"
        click P20 "#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj"
        click P5 "#srcskorubaidentityserver4adminbusinesslogicskorubaidentityserver4adminbusinesslogiccsproj"
        click P19 "#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj"
        click P16 "#srcskorubaidentityserver4adminentityframeworkpostgresqlskorubaidentityserver4adminentityframeworkpostgresqlcsproj"
        click P17 "#srcskorubaidentityserver4adminentityframeworkmysqlskorubaidentityserver4adminentityframeworkmysqlcsproj"
    end
    P2 --> MAIN
    P3 --> MAIN
    MAIN --> P15
    MAIN --> P20
    MAIN --> P5
    MAIN --> P19
    MAIN --> P16
    MAIN --> P17

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 6 | High - Require code changes |
| 🟡 Source Incompatible | 1 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 337 |  |
| ***Total APIs Analyzed*** | ***344*** |  |

#### Project Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| IdentityModel & Claims-based Security | 4 | 57,1% | Windows Identity Foundation (WIF), SAML, and claims-based authentication APIs that have been replaced by modern identity libraries. WIF was the original identity framework for .NET Framework. Migrate to Microsoft.IdentityModel.* packages (modern identity stack). |

<a id="srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj"></a>
### src\Skoruba.IdentityServer4.Shared.Configuration\Skoruba.IdentityServer4.Shared.Configuration.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 0
- **Dependants**: 3
- **Number of Files**: 19
- **Number of Files with Incidents**: 5
- **Lines of Code**: 618
- **Estimated LOC to modify**: 18+ (at least 2,9% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (3)"]
        P6["<b>📦&nbsp;Skoruba.IdentityServer4.STS.Identity.csproj</b><br/><small>net6.0</small>"]
        P11["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.Api.csproj</b><br/><small>net6.0</small>"]
        P20["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.UI.csproj</b><br/><small>net6.0</small>"]
        click P6 "#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj"
        click P11 "#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj"
        click P20 "#srcskorubaidentityserver4adminuiskorubaidentityserver4adminuicsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Shared.Configuration.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Shared.Configuration.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj"
    end
    P6 --> MAIN
    P11 --> MAIN
    P20 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 10 | High - Require code changes |
| 🟡 Source Incompatible | 3 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 5 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 703 |  |
| ***Total APIs Analyzed*** | ***721*** |  |

<a id="srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj"></a>
### src\Skoruba.IdentityServer4.Shared\Skoruba.IdentityServer4.Shared.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 1
- **Dependants**: 3
- **Number of Files**: 12
- **Number of Files with Incidents**: 1
- **Lines of Code**: 108
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (3)"]
        P1["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.csproj</b><br/><small>net6.0</small>"]
        P6["<b>📦&nbsp;Skoruba.IdentityServer4.STS.Identity.csproj</b><br/><small>net6.0</small>"]
        P11["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.Api.csproj</b><br/><small>net6.0</small>"]
        click P1 "#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj"
        click P6 "#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj"
        click P11 "#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj"
    end
    subgraph current["Skoruba.IdentityServer4.Shared.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Shared.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj"
    end
    subgraph downstream["Dependencies (1"]
        P9["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj</b><br/><small>net6.0</small>"]
        click P9 "#srcskorubaidentityserver4adminbusinesslogicidentityskorubaidentityserver4adminbusinesslogicidentitycsproj"
    end
    P1 --> MAIN
    P6 --> MAIN
    P11 --> MAIN
    MAIN --> P9

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

<a id="srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj"></a>
### src\Skoruba.IdentityServer4.STS.Identity\Skoruba.IdentityServer4.STS.Identity.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** AspNetCore
- **Dependencies**: 4
- **Dependants**: 1
- **Number of Files**: 731
- **Number of Files with Incidents**: 4
- **Lines of Code**: 6615
- **Estimated LOC to modify**: 21+ (at least 0,3% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P10["<b>📦&nbsp;Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj</b><br/><small>net6.0</small>"]
        click P10 "#testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj"
    end
    subgraph current["Skoruba.IdentityServer4.STS.Identity.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.STS.Identity.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj"
    end
    subgraph downstream["Dependencies (4"]
        P12["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj</b><br/><small>net6.0</small>"]
        P22["<b>📦&nbsp;Skoruba.IdentityServer4.Shared.Configuration.csproj</b><br/><small>net6.0</small>"]
        P21["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.csproj</b><br/><small>net6.0</small>"]
        P19["<b>📦&nbsp;Skoruba.IdentityServer4.Shared.csproj</b><br/><small>net6.0</small>"]
        click P12 "#srcskorubaidentityserver4adminentityframeworksharedskorubaidentityserver4adminentityframeworksharedcsproj"
        click P22 "#srcskorubaidentityserver4sharedconfigurationskorubaidentityserver4sharedconfigurationcsproj"
        click P21 "#srcskorubaidentityserver4adminentityframeworkconfigurationskorubaidentityserver4adminentityframeworkconfigurationcsproj"
        click P19 "#srcskorubaidentityserver4sharedskorubaidentityserver4sharedcsproj"
    end
    P10 --> MAIN
    MAIN --> P12
    MAIN --> P22
    MAIN --> P21
    MAIN --> P19

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 12 | High - Require code changes |
| 🟡 Source Incompatible | 8 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 1 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 24429 |  |
| ***Total APIs Analyzed*** | ***24450*** |  |

<a id="testsskorubaidentityserver4adminapiintegrationtestsskorubaidentityserver4adminapiintegrationtestscsproj"></a>
### tests\Skoruba.IdentityServer4.Admin.Api.IntegrationTests\Skoruba.IdentityServer4.Admin.Api.IntegrationTests.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** DotNetCoreApp
- **Dependencies**: 1
- **Dependants**: 0
- **Number of Files**: 13
- **Number of Files with Incidents**: 8
- **Lines of Code**: 353
- **Estimated LOC to modify**: 12+ (at least 3,4% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["Skoruba.IdentityServer4.Admin.Api.IntegrationTests.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.Api.IntegrationTests.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#testsskorubaidentityserver4adminapiintegrationtestsskorubaidentityserver4adminapiintegrationtestscsproj"
    end
    subgraph downstream["Dependencies (1"]
        P11["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.Api.csproj</b><br/><small>net6.0</small>"]
        click P11 "#srcskorubaidentityserver4adminapiskorubaidentityserver4adminapicsproj"
    end
    MAIN --> P11

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 5 | High - Require code changes |
| 🟡 Source Incompatible | 2 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 5 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 441 |  |
| ***Total APIs Analyzed*** | ***453*** |  |

#### Project Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| IdentityModel & Claims-based Security | 5 | 41,7% | Windows Identity Foundation (WIF), SAML, and claims-based authentication APIs that have been replaced by modern identity libraries. WIF was the original identity framework for .NET Framework. Migrate to Microsoft.IdentityModel.* packages (modern identity stack). |

<a id="testsskorubaidentityserver4adminintegrationtestsskorubaidentityserver4adminintegrationtestscsproj"></a>
### tests\Skoruba.IdentityServer4.Admin.IntegrationTests\Skoruba.IdentityServer4.Admin.IntegrationTests.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** DotNetCoreApp
- **Dependencies**: 1
- **Dependants**: 0
- **Number of Files**: 14
- **Number of Files with Incidents**: 9
- **Lines of Code**: 518
- **Estimated LOC to modify**: 14+ (at least 2,7% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["Skoruba.IdentityServer4.Admin.IntegrationTests.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.IntegrationTests.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#testsskorubaidentityserver4adminintegrationtestsskorubaidentityserver4adminintegrationtestscsproj"
    end
    subgraph downstream["Dependencies (1"]
        P1["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.csproj</b><br/><small>net6.0</small>"]
        click P1 "#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj"
    end
    MAIN --> P1

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 5 | High - Require code changes |
| 🟡 Source Incompatible | 2 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 7 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 647 |  |
| ***Total APIs Analyzed*** | ***661*** |  |

#### Project Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| IdentityModel & Claims-based Security | 5 | 35,7% | Windows Identity Foundation (WIF), SAML, and claims-based authentication APIs that have been replaced by modern identity libraries. WIF was the original identity framework for .NET Framework. Migrate to Microsoft.IdentityModel.* packages (modern identity stack). |

<a id="testsskorubaidentityserver4adminunittestsskorubaidentityserver4adminunittestscsproj"></a>
### tests\Skoruba.IdentityServer4.Admin.UnitTests\Skoruba.IdentityServer4.Admin.UnitTests.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** DotNetCoreApp
- **Dependencies**: 1
- **Dependants**: 0
- **Number of Files**: 39
- **Number of Files with Incidents**: 2
- **Lines of Code**: 8013
- **Estimated LOC to modify**: 2+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["Skoruba.IdentityServer4.Admin.UnitTests.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.UnitTests.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#testsskorubaidentityserver4adminunittestsskorubaidentityserver4adminunittestscsproj"
    end
    subgraph downstream["Dependencies (1"]
        P1["<b>📦&nbsp;Skoruba.IdentityServer4.Admin.csproj</b><br/><small>net6.0</small>"]
        click P1 "#srcskorubaidentityserver4adminskorubaidentityserver4admincsproj"
    end
    MAIN --> P1

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 2 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 14907 |  |
| ***Total APIs Analyzed*** | ***14909*** |  |

<a id="testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj"></a>
### tests\Skoruba.IdentityServer4.STS.Identity.IntegrationTests\Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj

#### Project Info

- **Current Target Framework:** net6.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** DotNetCoreApp
- **Dependencies**: 1
- **Dependants**: 0
- **Number of Files**: 17
- **Number of Files with Incidents**: 8
- **Lines of Code**: 738
- **Estimated LOC to modify**: 13+ (at least 1,8% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj"]
        MAIN["<b>📦&nbsp;Skoruba.IdentityServer4.STS.Identity.IntegrationTests.csproj</b><br/><small>net6.0</small>"]
        click MAIN "#testsskorubaidentityserver4stsidentityintegrationtestsskorubaidentityserver4stsidentityintegrationtestscsproj"
    end
    subgraph downstream["Dependencies (1"]
        P6["<b>📦&nbsp;Skoruba.IdentityServer4.STS.Identity.csproj</b><br/><small>net6.0</small>"]
        click P6 "#srcskorubaidentityserver4stsidentityskorubaidentityserver4stsidentitycsproj"
    end
    MAIN --> P6

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 2 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 11 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 886 |  |
| ***Total APIs Analyzed*** | ***899*** |  |


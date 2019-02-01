#Identity Server 4 Information:
	1. http://localhost:5000/.well-known/openid-configuration

#Development Cert Guide for test:
	1. https://cmatskas.com/enforcing-https-only-traffic-with-asp-net-core-and-kestrel/

#Generate Dummy Cert in PowerShell:
	Run the scripts in the Docker Compose section

#Generic:
	﻿dotnet ef migrations add [name] --context [DbContextName]
	dotnet ef database update --context [DbContextName]

#For Identity 2.1:
	﻿dotnet ef migrations add SecurityDatabase --context SecurityDbContext
	dotnet ef database update --context SecurityDbContext

#For IdentityServer4:
	dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext
	dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext

	dotnet ef database update --context PersistedGrantDbContext
	dotnet ef database update --context ConfigurationDbContext
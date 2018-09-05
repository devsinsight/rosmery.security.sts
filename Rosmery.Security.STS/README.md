#Identity Server 4 Information:
	1. http://localhost:5000/.well-known/openid-configuration

#Development Cert Guide for test:
	1. https://cmatskas.com/enforcing-https-only-traffic-with-asp-net-core-and-kestrel/

#Generate Dummy Cert in PowerShell:
	New-SelfSignedCertificate -NotBefore (Get-Date) -NotAfter (Get-Date).AddYears(1) -Subject "development-dummy-cert" -KeyA
	lgorithm "RSA" -KeyLength 2048 -HashAlgorithm "SHA256" -CertStoreLocation "Cert:\CurrentUser\My" -KeyUsage KeyEncipherment -FriendlyName "HTTPS
	 development certificate" -TextExtension @("2.5.29.19={critical}{text}","2.5.29.37={critical}{text}1.3.6.1.5.5.7.3.1","2.5.29.17={critical}{tex
	t}DNS=localhost")

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
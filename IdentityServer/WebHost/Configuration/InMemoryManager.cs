using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;

namespace WebHost.Configuration
{
    public class InMemoryManager
    {
        public List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "admin",
                    Username = "admin",
                    Password = "password",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.Name, "Admin Admin")
                    }
                }
            };
        }

        public IEnumerable<Scope> GetScopes()
        {
            return new[]
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,
                new Scope
                {
                    Name = "user/*.*",
                    DisplayName = "Full Access to all User Data",
                    Type = ScopeType.Resource
                },
                new Scope
                {
                    Name = "user/*.read",
                    DisplayName = "read Access to all User Data"
                },
                new Scope
                {
                    Name = "user/*.write",
                    DisplayName = "write Access to all User Data"
                },
                new Scope
                {
                    Name = "user/Person.*",
                    DisplayName = "Full Access to Person resource"
                },
                new Scope
                {
                    Name = "user/Person.read",
                    DisplayName = "read Access to all User Data"
                },
                new Scope
                {
                    Name = "user/Person.write",
                    DisplayName = "write Access to all User Data"
                },
                new Scope
                {
                    Name = "launch/patient",
                    DisplayName = "launch patient"
                },
                new Scope
                {
                    Name = "patient/*.read",
                    Description = "read all patient resources"
                }
            };
        }

        public IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "practiceXYZ",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "PracticeXYZ",
                    Flow = Flows.ResourceOwner,
                    AllowAccessToAllScopes = true,
                    Enabled = true,
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://10.37.129.2:9000"
                    },
                    RedirectUris = new List<string>
                    {
                        "http://10.37.129.2:9000/",
                        "http://10.37.129.2:9000",
                        "http://10.37.129.2:9000/launch.html?fhirServiceUrl=http://10.211.55.33:8080/FhirStu3&patientId=1482713",
                        "http://10.37.129.2:9000/launch.html?patientId=1482713"
                    }
                },
                new Client
                {
                    ClientId = "practiceXYZ_implicit",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "Growth Charts Smart on FHIR Application",
                    Flow = Flows.AuthorizationCode,
                    AllowAccessToAllScopes = true,
                    RedirectUris = new List<string>
                    {
                        "http://10.37.129.2:9000/",
                        "http://10.37.129.2:9000",
                        "http://10.37.129.2:9000/launch.html?fhirServiceUrl=http://10.211.55.33:8080/FhirStu3&patientId=1482713",
                        "http://10.37.129.2:9000/launch.html?patientId=1482713",
                        "http://192.168.86.38:9000/"
                    },
                    Enabled = true,
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://10.37.129.2:9000",
                        "http://192.168.86.38:9000"
                    }
                },
                new Client
                {
                    ClientId = "growth_chart",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "Growth Charts Smart on FHIR Application",
                    Flow = Flows.AuthorizationCode,
                    AllowAccessToAllScopes = true,
                    RedirectUris = new List<string>
                    {
                        "http://10.37.129.2:9000/",
                        "http://10.37.129.2:9000",
                        "http://10.37.129.2:9000/launch.html?fhirServiceUrl=http://10.211.55.33:8080/FhirStu3&patientId=1482713",
                        "http://10.37.129.2:9000/launch.html?patientId=1482713"
                    },
                    Enabled = true,
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://10.37.129.2:9000"
                    }
                },

                new Client
                {
                    ClientId = "ellkay-himss-client-id",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientName = "Ellkay Smart on FHIR Application",
                    Flow = Flows.AuthorizationCode,
                    AllowAccessToAllScopes = true,
                    RedirectUris = new List<string>
                    {
                        "https://services.lkstaging.com/SSORedirector"
                    },
                    Enabled = true,
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://10.37.129.2:9000",
                        "http://192.168.86.38:9000"
                    }
                },
            };
        }
    }
}
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace KevinParkinson.IDP
{
    public static class MockupStore
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "d860efca-22d9-47fd-8249-791ba61b07c7",
                    Username = "Kevin",
                    Password = "%0MyG0d101TDP%",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Kevin"),
                        new Claim("family_name", "Parkinson"),
                        new Claim("address", "5203 185 Street NW"),
                        new Claim("role", "AdminSuperUser"),
                        new Claim("subscriptionlevel", "AdminSuperUser"),
                        new Claim("country", "CA")
                    }
                },
                new TestUser
                {
                    SubjectId = "b7539694-97e7-4dfe-84da-b4256e1ff5c7",
                    Username = "Claudia",
                    Password = "5401Love",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Claudia"),
                        new Claim("family_name", "Parkinson"),
                        new Claim("address", "5203 185 Street NW"),
                        new Claim("role", "SuperUser"),
                        new Claim("subscriptionlevel", "SuperUser"),
                        new Claim("country", "CA")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "Your role(s)", new List<string>() { "role"}),
                new IdentityResource("country", "The country you're living in",
                    new List<string> { "country" }),
                new IdentityResource("subscriptionlevel", "Your subscription level",
                    new List<string> { "subscriptionlevel" })
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("Top30MoviesAPI", "Top 30 Movies API",
                new List<string>() { "role" })
                {
                    ApiSecrets = { new Secret("apisecret".Sha256()) }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientName = "Top 30 Movies Client",
                    ClientId = "Top30MoviesClient",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    AccessTokenType = AccessTokenType.Reference,

                    RequireConsent = false,
                   // IdentityTokenLifetime = 300,
                  //  AuthorizationCodeLifetime = 300
                    AccessTokenLifetime = 120, 
                   // AbsoluteRefreshTokenLifetime 
                    //RefreshTokenExpiration = TokenExpiration.Sliding,
                    //SlidingRefreshTokenLifetime = ...  
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AllowOfflineAccess = true,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:44355/signin-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "imagegalleryapi",
                        "country",
                        "subscriptionlevel"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    PostLogoutRedirectUris =
                    {
                        "https://localhost:44355/signout-callback-oidc"
                    },
                   // AlwaysIncludeUserClaimsInIdToken = true
                
                }
             };
        }
    }
}

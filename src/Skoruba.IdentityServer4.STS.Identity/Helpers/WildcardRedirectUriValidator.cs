using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Skoruba.IdentityServer4.STS.Identity.Helpers
{
    /// <summary>
    /// Redirect URI validator that supports wildcard patterns (* and ?) in registered client redirect URIs.
    /// This enables preview/deployment URLs to work, e.g. https://myapp-*.azurewebsites.net/callback
    /// Falls back to strict (exact match) validation when no wildcards are present.
    /// </summary>
    public class WildcardRedirectUriValidator : StrictRedirectUriValidator
    {
        public override Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
        {
            if (client.RedirectUris != null &&
                client.RedirectUris.Any(uri => HasWildcard(uri) && MatchesWildcardPattern(uri, requestedUri)))
            {
                return Task.FromResult(true);
            }

            return base.IsRedirectUriValidAsync(requestedUri, client);
        }

        public override Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
        {
            if (client.PostLogoutRedirectUris != null &&
                client.PostLogoutRedirectUris.Any(uri => HasWildcard(uri) && MatchesWildcardPattern(uri, requestedUri)))
            {
                return Task.FromResult(true);
            }

            return base.IsPostLogoutRedirectUriValidAsync(requestedUri, client);
        }

        private static bool HasWildcard(string uri)
        {
            return uri.Contains('*') || uri.Contains('?');
        }

        private static bool MatchesWildcardPattern(string pattern, string input)
        {
            // Convert the wildcard pattern to a regex:
            // * matches zero or more characters, ? matches exactly one character.
            var regexPattern = "^" + Regex.Escape(pattern)
                .Replace("\\*", ".*")
                .Replace("\\?", ".") + "$";

            return Regex.IsMatch(input, regexPattern, RegexOptions.IgnoreCase);
        }
    }
}

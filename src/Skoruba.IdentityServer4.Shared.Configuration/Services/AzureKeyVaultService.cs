// Original file comes from: https://github.com/damienbod/IdentityServer4AspNetCoreIdentityTemplate
// Modified by Jan Škoruba
// Updated for Azure SDK v12+ (.NET 10 compatibility)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using Azure.Security.KeyVault.Secrets;
using Skoruba.IdentityServer4.Shared.Configuration.Configuration.Common;

namespace Skoruba.IdentityServer4.Shared.Configuration.Services
{
    public class AzureKeyVaultService
    {
        private readonly AzureKeyVaultConfiguration _azureKeyVaultConfiguration;

        public AzureKeyVaultService(AzureKeyVaultConfiguration azureKeyVaultConfiguration)
        {
            if (azureKeyVaultConfiguration == null)
            {
                throw new ArgumentException("missing azureKeyVaultConfiguration");
            }

            if (string.IsNullOrEmpty(azureKeyVaultConfiguration.AzureKeyVaultEndpoint))
            {
                throw new ArgumentException("missing keyVaultEndpoint");
            }

            _azureKeyVaultConfiguration = azureKeyVaultConfiguration;
        }

        public async Task<(X509Certificate2 ActiveCertificate, X509Certificate2 SecondaryCertificate)> GetCertificatesFromKeyVault()
        {
            (X509Certificate2 ActiveCertificate, X509Certificate2 SecondaryCertificate) certs = (null, null);

            var certificateClient = BuildCertificateClient();
            var secretClient = BuildSecretClient();

            var certificateItems = await GetAllEnabledCertificateVersionsAsync(certificateClient);
            var item = certificateItems.FirstOrDefault();
            if (item != null)
            {
                certs.ActiveCertificate = await GetCertificateAsync(item.Name, item.Version, certificateClient, secretClient);
            }

            if (certificateItems.Count > 1)
            {
                certs.SecondaryCertificate = await GetCertificateAsync(certificateItems[1].Name, certificateItems[1].Version, certificateClient, secretClient);
            }

            return certs;
        }

        /// <summary>
        /// Build CertificateClient according to authentication method
        /// </summary>
        /// <returns></returns>
        public CertificateClient BuildCertificateClient()
        {
            var vaultUri = new Uri(_azureKeyVaultConfiguration.AzureKeyVaultEndpoint);

            if (_azureKeyVaultConfiguration.UseClientCredentials)
            {
                var credential = new ClientSecretCredential(
                    _azureKeyVaultConfiguration.TenantId,
                    _azureKeyVaultConfiguration.ClientId,
                    _azureKeyVaultConfiguration.ClientSecret);
                return new CertificateClient(vaultUri, credential);
            }
            else
            {
                return new CertificateClient(vaultUri, new DefaultAzureCredential());
            }
        }

        /// <summary>
        /// Build SecretClient according to authentication method (needed to get private key)
        /// </summary>
        /// <returns></returns>
        public SecretClient BuildSecretClient()
        {
            var vaultUri = new Uri(_azureKeyVaultConfiguration.AzureKeyVaultEndpoint);

            if (_azureKeyVaultConfiguration.UseClientCredentials)
            {
                var credential = new ClientSecretCredential(
                    _azureKeyVaultConfiguration.TenantId,
                    _azureKeyVaultConfiguration.ClientId,
                    _azureKeyVaultConfiguration.ClientSecret);
                return new SecretClient(vaultUri, credential);
            }
            else
            {
                return new SecretClient(vaultUri, new DefaultAzureCredential());
            }
        }

        private async Task<List<CertificateProperties>> GetAllEnabledCertificateVersionsAsync(CertificateClient certificateClient)
        {
            // Get all the certificate versions (this will also get the current active version)
            var certificateVersions = certificateClient.GetPropertiesOfCertificateVersionsAsync(_azureKeyVaultConfiguration.IdentityServerCertificateName);

            var enabledVersions = new List<CertificateProperties>();
            await foreach (var certVersion in certificateVersions)
            {
                if (certVersion.Enabled.HasValue && certVersion.Enabled.Value)
                {
                    enabledVersions.Add(certVersion);
                }
            }

            // Sort by creation date in descending order
            return enabledVersions
                .OrderByDescending(certVersion => certVersion.CreatedOn)
                .ToList();
        }

        private async Task<X509Certificate2> GetCertificateAsync(string certificateName, string version, CertificateClient certificateClient, SecretClient secretClient)
        {
            // Get the certificate (contains public key)
            var certificateResponse = await certificateClient.GetCertificateVersionAsync(certificateName, version);
            var certificate = certificateResponse.Value;

            // Get the secret (contains private key)
            var secretResponse = await secretClient.GetSecretAsync(certificateName, version);
            var privateKeyBytes = Convert.FromBase64String(secretResponse.Value.Value);

            // Use X509CertificateLoader instead of deprecated constructor
            var certificateWithPrivateKey = X509CertificateLoader.LoadPkcs12(
                privateKeyBytes, 
                password: null, 
                keyStorageFlags: X509KeyStorageFlags.MachineKeySet);

            return certificateWithPrivateKey;
        }
    }
}

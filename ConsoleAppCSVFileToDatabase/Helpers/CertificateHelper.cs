using System.Security.Cryptography.X509Certificates;

namespace ConsoleAppCSVFileToDatabase.Helpers;

public static class CertificateHelper
{
    static CertificateHelper()
    {
        var x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        x509Store.Open(OpenFlags.ReadOnly);
        var ravedDbCertificate = x509Store.Certificates.FirstOrDefault(c => c.Subject == "CN=free.dotnet");
        if (ravedDbCertificate != null)
        {
            Certificate = ravedDbCertificate;

        }
        x509Store.Close();
    }

    public static X509Certificate2? Certificate { get; set; }
}
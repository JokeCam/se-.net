using System.Security.Cryptography.X509Certificates;

namespace ConsoleAppCSVFileToDatabase.Helpers;

public class Certificate
{
    private string CertificateSubject;
    public Certificate(string certificateSubject)
    {
        CertificateSubject = certificateSubject;
    }

    public async Task<X509Certificate2> GetCertificate()
    {
        var certTask = new Task<X509Certificate2>(() =>
        {
            var x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            x509Store.Open(OpenFlags.ReadOnly);
            var ravedDbCertificate = x509Store.Certificates.FirstOrDefault(c => c.Subject == CertificateSubject);
            x509Store.Close();
            return ravedDbCertificate;
        });
        return await Task.Run(() => certTask);
    }
}
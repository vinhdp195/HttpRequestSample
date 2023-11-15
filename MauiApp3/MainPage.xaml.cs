using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void SocketClickListener(object sender, EventArgs e)
    {
        string username = userName.Text;
        string password = passWord.Text;
        string server_url = serverUrl.Text;

        if (password != null || username != null || server_url != null)
        {
            resultText.Text = "Send Request";
            _ = CheckSocketAsync(username, password, server_url);
        }

    }

    private void HttpClickListener(object sender, EventArgs e)
    {
        string username = userName.Text;
        string password = passWord.Text;
        string server_url = serverUrl.Text;

        if (password != null || username != null || server_url != null)
        {
            resultText.Text = "Send Request";
            _ = CheckHttpAsync(username, password, server_url);
        }
    }

    public async Task CheckHttpAsync(string username, string password, string server_url)
    {
        string result;

        try
        {
            string room = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:t=\"http://schemas.microsoft.com/exchange/services/2006/types\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n<soap:Header>\r\n<t:RequestServerVersion Version=\"Exchange2007_SP1\" />\r\n</soap:Header>\r\n<soap:Body>\r\n<ResolveNames xmlns=\"http://schemas.microsoft.com/exchange/services/2006/messages\" ReturnFullContactData=\"true\">\r\n<UnresolvedEntry>u</UnresolvedEntry>\r\n</ResolveNames>\r\n</soap:Body>\r\n</soap:Envelope>\r\n";
            var dataContent = new StringContent(room, Encoding.UTF8, "text/xml");

            NSUrlSessionHandler socket = new NSUrlSessionHandler();

            NetworkCredential userCredentials = new NetworkCredential(username, password);
            CredentialCache credentialsCache = new CredentialCache();
            Uri uri = new Uri(server_url);
            credentialsCache.Add(uri, "NTLM", userCredentials);
            socket.Credentials = credentialsCache;

            HttpClient http = new HttpClient(socket);
            http.Timeout = TimeSpan.FromMilliseconds(20000);
            HttpResponseMessage res = await http.PostAsync(server_url, dataContent);
            result = res.StatusCode.GetHashCode().ToString();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            result = ex.Message;
        }

        resultText.Text = "Result: " + result;
    }

    public async Task CheckSocketAsync(string username, string password, string server_url)
    {
        string result;
        try
        {
            string room = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:t=\"http://schemas.microsoft.com/exchange/services/2006/types\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n<soap:Header>\r\n<t:RequestServerVersion Version=\"Exchange2007_SP1\" />\r\n</soap:Header>\r\n<soap:Body>\r\n<ResolveNames xmlns=\"http://schemas.microsoft.com/exchange/services/2006/messages\" ReturnFullContactData=\"true\">\r\n<UnresolvedEntry>u</UnresolvedEntry>\r\n</ResolveNames>\r\n</soap:Body>\r\n</soap:Envelope>\r\n";
            var dataContent = new StringContent(room, Encoding.UTF8, "text/xml");

            SocketsHttpHandler socket = new SocketsHttpHandler();

            NetworkCredential userCredentials = new NetworkCredential(username, password);
            CredentialCache credentialsCache = new CredentialCache();
            Uri uri = new Uri(server_url);
            credentialsCache.Add(uri, "NTLM", userCredentials);
            socket.Credentials = credentialsCache;

            HttpClient http = new HttpClient(socket);
            http.Timeout = TimeSpan.FromMilliseconds(20000);
            HttpResponseMessage res = await http.PostAsync(server_url, dataContent);
            result = res.StatusCode.GetHashCode().ToString();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            result = ex.Message;
        }

        resultText.Text = "Result: " + result;
    }
}
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Windows;

namespace Procajas
{
    public partial class MainWindow : Window
    {
        #region Config Values

        //
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        // The Tenant is the name of the Azure AD tenant in which this application is registered.
        // The AAD Instance is the instance of Azure, for example public Azure or Azure China.
        // The Redirect URI is the URI where Azure AD will return OAuth responses.
        // The Authority is the sign-in URL of the tenant.
        //
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        Uri redirectUri = new Uri(ConfigurationManager.AppSettings["ida:RedirectUri"]);
        private static string authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

        private static string procajasResourceId = ConfigurationManager.AppSettings["ida:ProcajasResourceId"];
        private static string procajasApiEndpoint = ConfigurationManager.AppSettings["ida:ProcajasApiEndpoint"];

        #endregion

        private HttpClient httpClient = new HttpClient();
        private AuthenticationContext authContext = null;

        public MainWindow()
        {
            InitializeComponent();

            authContext = new AuthenticationContext(authority, new FileCache());

            CheckForCachedToken();
        }

        public async void CheckForCachedToken()
        {
            // As the application starts, try to get an access token without prompting the user.  If one exists, show the user as signed in.
            AuthenticationResult result = null;
            try
            {
                result = await authContext.AcquireTokenAsync(procajasResourceId, clientId, redirectUri, new PlatformParameters(PromptBehavior.Always));
            }
            catch (AdalException ex)
            {
                if (ex.ErrorCode != "user_interaction_required")
                {
                    // An unexpected error occurred.
                    MessageBox.Show(ex.Message);
                }

                // If user interaction is required, proceed to main page without singing the user in.
                return;
            }

            // A valid token is in the cache
            SignOutButton.Visibility = Visibility.Visible;
            SignInButton.Visibility = Visibility.Hidden;
            UserNameLabel.Content = result.UserInfo.DisplayableId;
        }

        private void SignOut(object sender = null, RoutedEventArgs args = null)
        {
            // Clear the token cache
            authContext.TokenCache.Clear();

            // Clear cookies from the browser control.
            ClearCookies();

            // Reset the UI
            SignOutButton.Visibility = Visibility.Hidden;
            SignInButton.Visibility = Visibility.Visible;
            UserNameLabel.Content = string.Empty;
        }

        private async void SignIn(object sender = null, RoutedEventArgs args = null)
        {
            // Get an Access Token for the Graph API
            AuthenticationResult result = null;
            try
            {
                result = await authContext.AcquireTokenAsync(procajasResourceId, clientId, redirectUri, new PlatformParameters(PromptBehavior.Auto));
                UserNameLabel.Content = result.UserInfo.DisplayableId;
                SignOutButton.Visibility = Visibility.Visible;
                SignInButton.Visibility = Visibility.Hidden;
            }
            catch (AdalException ex)
            {
                // An unexpected error occurred, or user canceled the sign in.
                if (ex.ErrorCode != "access_denied")
                    MessageBox.Show(ex.Message);

                return;
            }
        }

        #region Cookie Management

        // This function clears cookies from the browser control used by ADAL.
        private void ClearCookies()
        {
            const int INTERNET_OPTION_END_BROWSER_SESSION = 42;
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);
        }

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

        #endregion
    }
}

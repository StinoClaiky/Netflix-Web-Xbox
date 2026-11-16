using System;
using Microsoft.Web.WebView2.Core;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace NetflixWebXbox {
    public sealed partial class MainPage : Page {
        private const string Home = "https://www.netflix.com/";
        private CoreWebView2 core;
        public MainPage() {
            InitializeComponent();
            Web.RequiresPointer = RequiresPointer.WhenFocused;
            Loaded += Start;
            PreviewKeyDown += Keys;
            SystemNavigationManager.GetForCurrentView().BackRequested += SystemBack;
        }
        private static bool Allowed(string url) {
            Uri u;
            return Uri.TryCreate(url, UriKind.Absolute, out u) && u.Scheme == "https" &&
                (u.IdnHost.Equals("netflix.com", StringComparison.OrdinalIgnoreCase) ||
                 u.IdnHost.EndsWith(".netflix.com", StringComparison.OrdinalIgnoreCase));
        }
        private async void Start(object sender, RoutedEventArgs e) {
            Loaded -= Start;
            try {
                await Web.EnsureCoreWebView2Async();
                core = Web.CoreWebView2;
                core.Settings.AreDevToolsEnabled = false;
                core.Settings.IsStatusBarEnabled = false;
                core.NavigationStarting += (s,a) => {
                    if (!Allowed(a.Uri)) { a.Cancel = true; StatusText.Text = "Navigatie buiten Netflix geblokkeerd."; }
                };
                core.NewWindowRequested += (s,a) => {
                    a.Handled = true;
                    if (Allowed(a.Uri)) core.Navigate(a.Uri);
                };
                core.NavigationCompleted += (s,a) => {
                    StatusText.Text = a.IsSuccess ? "Website geladen. Videoweergave en DRM nog te testen. Menu = werkbalk." :
                        "Website niet geladen: " + a.WebErrorStatus.ToString() + ". Probeer Vernieuw.";
                };
                core.ProcessFailed += (s,a) => { StatusText.Text = "Browser gestopt: " + a.ProcessFailedKind.ToString(); ShowToolbar(true); };
                core.Navigate(Home);
                Web.Focus(FocusState.Programmatic);
            } catch (Exception ex) {
                StatusText.Text = "WebView2 kon niet starten (" + ex.HResult.ToString("X8") + "). Open browser als alternatief.";
            }
        }
        private void ShowToolbar(bool show) {
            Toolbar.Visibility = StatusText.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
            if (!show) Web.Focus(FocusState.Programmatic);
        }
        private void Keys(object sender, KeyRoutedEventArgs e) {
            if (e.Key == VirtualKey.GamepadMenu || e.Key == VirtualKey.F11) {
                ShowToolbar(Toolbar.Visibility != Visibility.Visible); e.Handled = true;
            }
        }
        private void SystemBack(object sender, BackRequestedEventArgs e) {
            if (Toolbar.Visibility != Visibility.Visible) { ShowToolbar(true); e.Handled = true; }
            else if (core != null && core.CanGoBack) { core.GoBack(); e.Handled = true; }
        }
        private void Home_Click(object s, RoutedEventArgs e) { if (core != null) core.Navigate(Home); }
        private void Back_Click(object s, RoutedEventArgs e) { if (core != null && core.CanGoBack) core.GoBack(); }
        private void Reload_Click(object s, RoutedEventArgs e) { if (core != null) core.Reload(); }
        private void Full_Click(object s, RoutedEventArgs e) { ShowToolbar(false); }
        private async void Browser_Click(object s, RoutedEventArgs e) {
            try { if (!await Launcher.LaunchUriAsync(new Uri(Home))) StatusText.Text = "Geen externe browser beschikbaar."; }
            catch { StatusText.Text = "Externe browser niet beschikbaar."; }
        }
    }
}
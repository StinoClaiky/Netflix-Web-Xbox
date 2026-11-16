using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
namespace NetflixWebXbox {
    sealed partial class App : Application {
        public App() { InitializeComponent(); RequiresPointerMode = ApplicationRequiresPointerMode.WhenRequested; }
        protected override void OnLaunched(LaunchActivatedEventArgs e) {
            if (e.PrelaunchActivated) return;
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            if (Window.Current.Content == null) Window.Current.Content = new MainPage();
            Window.Current.Activate();
        }
    }
}
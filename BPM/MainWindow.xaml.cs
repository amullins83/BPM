using System.Windows;

namespace BPM {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    /// <summary>
    ///  The view model for the BPM view.
    /// </summary>
    private BpmViewModel bpmViewModel = new BpmViewModel();

    /// <summary>
    ///  Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow() {
      DataContext = bpmViewModel;
      InitializeComponent();
    }

    /// <summary>
    ///  Update the BPM Text
    /// </summary>
    /// <param name="sender">The object that raised the event (ignored).</param>
    /// <param name="e">The event arguments (ignored).</param>
    private void TapButton_Click(object sender, RoutedEventArgs e) {
      bpmViewModel.Tap();
    }
  }
}

using System;
using System.ComponentModel;
using System.Windows;

namespace BPM {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window, INotifyPropertyChanged {
    /// <summary>
    ///  The milliseconds in one minute.
    /// </summary>
    private const double OneMinute = 60000.0;

    /// <summary>
    ///  The highest BPM displayed.
    /// </summary>
    private const double MaxBPM = 999.9;
    
    /// <summary>
    ///  Captures the tempo tapped.
    /// </summary>
    private double bpm = 0.0;

    /// <summary>
    ///  Captures the times when the tempo button was tapped.
    /// </summary>
    private DateTime[] tapTimes = new DateTime[3];

    /// <summary>
    ///  Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow() {
      DataContext = this;
      InitializeComponent();
    }

    /// <summary>
    ///  Notifies listeners of changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    ///  Gets the text representation of the tempo.
    /// </summary>
    public string Bpm {
      get {
        return string.Format("{0:F2} BPM", bpm);
      }
    }

    /// <summary>
    ///  Update the BPM Text
    /// </summary>
    /// <param name="sender">The object that raised the event (ignored).</param>
    /// <param name="e">The event arguments (ignored).</param>
    private void TapButton_Click(object sender, RoutedEventArgs e) {
      var newTapTimes = new DateTime[3];
      
      Array.Copy(tapTimes, 0, newTapTimes, 1, 2);
      tapTimes = newTapTimes;
      tapTimes[0] = DateTime.Now;
      if (tapTimes[1] != default(DateTime)) {
        var lastInterval = (tapTimes[0] - tapTimes[1]).TotalMilliseconds;
        if (tapTimes[2] != default(DateTime)) {
          var previousInterval = (tapTimes[1] - tapTimes[2]).TotalMilliseconds;
          lastInterval = (lastInterval + previousInterval) / 2;
        }

        if (lastInterval > OneMinute / MaxBPM) {
          bpm = OneMinute / lastInterval;
        } else {
          bpm = MaxBPM;
        }
        OnPropertyChanged("Bpm");
      }
    }

    /// <summary>
    ///  Raises the PropertyChanged event.
    /// </summary>
    /// <param name="name"></param>
    private void OnPropertyChanged(string name) {
      var propChanged = PropertyChanged;
      if (propChanged != null) {
        PropertyChanged(this, new PropertyChangedEventArgs(name));
      }
    }
  }
}

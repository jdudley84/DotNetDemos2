using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwaitSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancelSource;

        public MainWindow()
        {
            InitializeComponent();
            this.CalculateButton.Click += CalculateButton_ClickAsyncWithCancel;
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            this.CalculationProgress.Visibility = Visibility.Visible;
            this.ResultBox.Text = CalculateAnswer();
            this.CalculationProgress.Visibility = Visibility.Hidden;
        }

        private async void CalculateButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            this.CalculationProgress.Visibility = Visibility.Visible;
            this.ResultBox.Text = await CalculateAnswerAsync();
            this.CalculationProgress.Visibility = Visibility.Hidden;
        }

        private async void CalculateButton_ClickAsyncWithCancel(object sender, RoutedEventArgs e)
        {
            this.CalculationProgress.Visibility = Visibility.Visible;
            _cancelSource = new CancellationTokenSource();
            try
            {
                this.ResultBox.Text = await CalculateAnswerAsyncWithCancel(_cancelSource.Token);
            }
            catch (TaskCanceledException)
            {
                this.ResultBox.Text = "Cancelled";
            }
            this.CalculationProgress.Visibility = Visibility.Hidden;
        }

        private void CalculateButton_ClickAsyncDeadlock(object sender, RoutedEventArgs e)
        {
            this.CalculationProgress.Visibility = Visibility.Visible;
            this.ResultBox.Text = CalculateAnswerAsync().Result;
            this.CalculationProgress.Visibility = Visibility.Hidden;
        }

        private void CalculateButton_ClickTPL(object sender, RoutedEventArgs e)
        {
            this.CalculationProgress.Visibility = Visibility.Visible;

            CalculateAnswerAsync().ContinueWith(t =>
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    this.ResultBox.Text = t.Result;
                    this.CalculationProgress.Visibility = Visibility.Hidden;
                });
            });
        }

        private async void CalculateButton_ClickCrash(object sender, RoutedEventArgs e)
        {
            this.CalculationProgress.Visibility = Visibility.Visible;
            try
            {
                UnhandleableCrashAsync();
            }
            catch (Exception ex)
            {
                this.ResultBox.Text = $"Calculation failed: {ex.Message}";
            }
            this.CalculationProgress.Visibility = Visibility.Hidden;
        }

        private string CalculateAnswer()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));//Perform complicated calculation
            return "42";
        }

        private async Task<string> CalculateAnswerAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));//Perform complicated calculation            
            return "42";
        }

        private async Task<string> CalculateAnswerAsyncWithCancel(CancellationToken token)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), token);//Perform complicated calculation            
            return "42";
        }

        private Task<string> CalculateAnswerTPL()
        {
            return Task.Run(() => 
            {
                Task.Delay(TimeSpan.FromSeconds(5));//Perform complicated calculation
                return "42";
            });
        }

        private async void UnhandleableCrashAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            throw new Exception("Crash");
        }

        private async Task HandleableCrashAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            throw new Exception("Crash");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _cancelSource.Cancel();
        }
    }
}

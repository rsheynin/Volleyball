using System.Windows;
using System.Windows.Controls;

namespace VB.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged_PlayerAge(object sender, SelectionChangedEventArgs e)
        {
            ComboBox playerAge = new ComboBox();

            for (var i = 16; i < 56; i++)
            {
                playerAge.Items.Add(i);
            }

        }

        private void ComboBox_SelectionChanged_PlayerHeight(object sender, SelectionChangedEventArgs e)
        {
            ComboBox playerHeight = new ComboBox();

            for (var i = 150; i < 221; i++)
            {
                playerHeight.Items.Add(i);
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


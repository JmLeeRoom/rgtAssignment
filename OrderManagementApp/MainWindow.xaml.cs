using OrderManagementApp.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrderManagementApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        // 버튼 클릭 이벤트 핸들러 정의
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Order Details 버튼이 클릭되었습니다!");
        }
    }
}

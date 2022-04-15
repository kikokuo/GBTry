using GbTry.Machine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GbTry
{
    /// <summary>
    /// DebugView.xaml 的互動邏輯
    /// </summary>
    public partial class DebugView : Window
    {
        private GbCPU gbCPU;
        public DebugView()
        {
            InitializeComponent();
        }

        public void GetGbCPU(GbCPU gb)
        {
            gbCPU = gb;
        }
        public void UpdateInfo(String s)
        {
            if (!gbCPU.Running)
            {
                ListView1.Dispatcher.Invoke(() =>
                {
                    PCLabel.Content = gbCPU.PC.word.ToString("X4");
                    SPLabel.Content = gbCPU.SP.word.ToString("X4");
                    AFLabel.Content = gbCPU.AF.word.ToString("X4");
                    BCLabel.Content = gbCPU.BC.word.ToString("X4");
                    DELabel.Content = gbCPU.DE.word.ToString("X4");
                    HLLabel.Content = gbCPU.HL.word.ToString("X4");
                    CntLabel.Content = gbCPU.Cycle.ToString("X4");
                    LcdcLabel.Content = gbCPU.GetValueFromMemory(0xFF40).ToString("X4");
                    StatLabel.Content = gbCPU.GetValueFromMemory(0xFF41).ToString("X4");
                    LYLabel.Content = gbCPU.GetValueFromMemory(0xFF44).ToString("X4");
                    IELabel.Content = gbCPU.GetValueFromMemory(0xFFFF).ToString("X4");
                    IFLabel.Content = gbCPU.GetValueFromMemory(0xFF0F).ToString("X4");
                    //if (ListView1.Items.Count >= 10000)
                    //    ListView1.Items.Clear();
                    //if(gbCPU.Running)
                    //ListView1.Items.Add(s);
                }, System.Windows.Threading.DispatcherPriority.Background);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gbCPU.SetGotoPC(GotoPC.Text);
        }

        private void Single_Click(object sender, RoutedEventArgs e)
        {
            gbCPU.SetSingle();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gbCPU.debugflag = false;
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}

using GbTry.Machine;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GbTry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GbCPU gbCPU;
        private UInt32[] g_bg_data = new UInt32[160 * 144+160];
        private Image render = new Image();
        private bool isRunning = true;
        private WriteableBitmap backgroundBMP =
            new WriteableBitmap(160, 144, 96, 96, PixelFormats.Bgr32, null);
        private DebugView debugView = new DebugView();
        private Task Rungame;
        private int speed = 1;
        public float updateInterval = 1.0f;  //每幾秒算一次
        private DateTime lastInterval;
        private int frames = 0;
        private int fps;
        private TextBlock textBlock = new TextBlock();
        // 預設按鍵對映
        public Dictionary<Key, int> button_key_map = new Dictionary<Key, int>();

        public MainWindow()
        {
            InitializeComponent();
            render.Source = backgroundBMP;
            RenderOptions.SetBitmapScalingMode(render, BitmapScalingMode.NearestNeighbor);
            GameArea.Children.Add(render);
            textBlock.FontSize = 8;
            textBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            Canvas.SetLeft(textBlock, 0);
            Canvas.SetTop(textBlock, 10);
            GameArea.Children.Add(textBlock);
            lastInterval = DateTime.Now;  //自遊戲開始時間
            frames = 0;  //初始frames =0
            button_key_map.Add(Key.Z, 0x04); // a
            button_key_map.Add(Key.X, 0x08); // b
            button_key_map.Add(Key.Space, 0x01); // select
            button_key_map.Add(Key.Enter, 0x02); // start
            button_key_map.Add(Key.Up, 0x80); //up
            button_key_map.Add(Key.Down, 0x40); // down
            button_key_map.Add(Key.Left, 0x10); // left
            button_key_map.Add(Key.Right, 0x20); // right

            gbCPU = new GbCPU();
            gbCPU.Init();
            debugView.GetGbCPU(this.gbCPU);
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            int index;
            if (button_key_map.TryGetValue(e.Key, out index))
            {
                gbCPU.User_Input(index, 0);
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int index;
            if (button_key_map.TryGetValue(e.Key, out index))
            {
                gbCPU.User_Input(index, 1);
            }
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            System.Environment.Exit(0);
        }
        [DllImport("kernel32", EntryPoint = "RtlMoveMemory", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern void CopyMemory(IntPtr destination, IntPtr source, uint length);
        private unsafe void CompositionTarget_Rendering(object sender, EventArgs e)
        {
             gbCPU.Tick(speed);
            if (gbCPU.debugflag)
                debugView.UpdateInfo(gbCPU.commandstring);
            //if (gbCPU.ppu.blink())
            {
                backgroundBMP.Lock();
                Parallel.Invoke(() =>
                {
                    fixed (UInt32* ptr = g_bg_data)
                    {
                        var p = new IntPtr(ptr);
                        CopyMemory(backgroundBMP.BackBuffer, new IntPtr(ptr), (uint)160 * 144 * 4);
                    }
                });
                backgroundBMP.AddDirtyRect(new Int32Rect(0, 0, backgroundBMP.PixelWidth, backgroundBMP.PixelHeight));
                backgroundBMP.Unlock();
            }
        }

        private unsafe void UpdateGame()
        {
            Rungame = Task.Run(() => 
            {
                isRunning = true;
                gbCPU.stop = false;
                while (isRunning)
                {
                    gbCPU.Tick(speed);
                    if (gbCPU.debugflag)
                        debugView.UpdateInfo(gbCPU.commandstring);
                    if (gbCPU.ppu.blink()) {
                        GameArea.Dispatcher.Invoke(() =>
                        {
                            backgroundBMP.Lock();
                            fixed (UInt32* ptr = g_bg_data)
                            {
                                var p = new IntPtr(ptr);
                                CopyMemory(backgroundBMP.BackBuffer, new IntPtr(ptr), (uint)160 * 144 * 4);
                            }
                            backgroundBMP.AddDirtyRect(new Int32Rect(0, 0, backgroundBMP.PixelWidth, backgroundBMP.PixelHeight));
                            backgroundBMP.Unlock();
                            frames++;
                            if ((DateTime.Now - lastInterval).TotalSeconds >= updateInterval)  //每1秒更新一次
                            {
                                fps = ((int)(frames / (DateTime.Now - lastInterval).TotalSeconds)); //幀數= 每幀/每幀間隔毫秒 
                                frames = 0;
                                lastInterval = DateTime.Now;
                                textBlock.Text = new StringBuilder("FPS:" + fps.ToString()).ToString();
                            }
                        }, System.Windows.Threading.DispatcherPriority.Normal);
                    }
                    //_ = SpinWait.SpinUntil(() => !gbCPU.stop, 1);
                }
                isRunning = true;
            });
        }
        private void mnuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Zip files (*.zip)|*.zip|Nes files (*.gb)|*.gb|All files (*.*)|*.*";
            if (Rungame != null)
               gbCPU.stop = true;
            if (openFileDialog.ShowDialog() == true)
            {

                using (FileStream s = File.OpenRead(openFileDialog.FileName))
                {
                    // and BinareReader to read values
                    using (BinaryReader r = new BinaryReader(s))
                    {
                        var filearray = r.ReadBytes((int)s.Length);
                        if (Rungame != null)
                        {
                            isRunning = false;
                            Rungame.Wait();
                            Rungame = null;
                        }
                        gbCPU.Init_Emu(ref filearray);
                        gbCPU.SetBootImage(openFileDialog.FileName.Contains(".bin") == true);
                        gbCPU.PowerOn(ref g_bg_data);
                        UpdateGame();
                        //CompositionTarget.Rendering += CompositionTarget_Rendering;
                    }
                }
             
            }
        }

        private void mnuOpen_Debug(object sender, RoutedEventArgs e)
        {
            gbCPU.debugflag = true;
            debugView.Show();
        }
        private void mnuOpen_Reset(object sender, RoutedEventArgs e)
        {
            gbCPU.PowerOn(ref g_bg_data);
        }
        
    }
}

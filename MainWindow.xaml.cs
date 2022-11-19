using System;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace launchsound
{
    public partial class MainWindow : Window
    {

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private const int HOTKEY_ID = 8496;

        private IntPtr _windowHandle;
        private HwndSource _source;

        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            _windowHandle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            //RegisterHotKey(_windowHandle, HOTKEY_ID, 0x0000, 0x71); //F2
            //RegisterHotKey(_windowHandle, HOTKEY_ID, 0x0000, 0x72); //F3
            //RegisterHotKey(_windowHandle, HOTKEY_ID, 0x0000, 0x73); //F4

            RegisterHotKey(_windowHandle, HOTKEY_ID, 0x0000, 0x7C); //F13
            RegisterHotKey(_windowHandle, HOTKEY_ID, 0x0000, 0x7D); //F14
            RegisterHotKey(_windowHandle, HOTKEY_ID, 0x0000, 0x7E); //F15
            RegisterHotKey(_windowHandle, HOTKEY_ID, 0x0000, 0x7F); //F16
            RegisterHotKey(_windowHandle, HOTKEY_ID, 0x0000, 0x80); //F17
            RegisterHotKey(_windowHandle, HOTKEY_ID, 0x0000, 0x81); //F18
            RegisterHotKey(_windowHandle, HOTKEY_ID, 0x0000, 0x82); //F18
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            int lintParam = (int)((int)lParam * Math.Pow(16, -4));

            switch (msg)
            {
                case 0x0312:        //Match pour les hotkeys
                    this.playsound(lintParam);
                    handled = true;
                    break;
            }
            return IntPtr.Zero;
        }

        private void playsound(int lintParam)
        {
            SoundPlayer player = new SoundPlayer(Sound.GetSoundPath(lintParam));
            try
            {
                player.Load();
                player.Play();
            }
            catch (FileNotFoundException fnfe)
            {
                MessageBox.Show($"Le son {Sound.GetSoundName(lintParam)} n'existe pas");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            UnregisterHotKey(_windowHandle, HOTKEY_ID);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.Visibility = Visibility.Hidden;
        }
    }
}

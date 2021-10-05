using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
using System.Windows.Threading;

namespace TaskManager {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private MainWIndowViewModell ViewModell { get; }
        private DispatcherTimer Timer { get; }

        public MainWindow() {
            InitializeComponent();
            ViewModell = new MainWIndowViewModell();
            DataContext = ViewModell;

            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 2);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            if (ViewModell.SelectedProcess is null) {
                ViewModell.UpdatingProcesses();
            }
            else {
                int processId = ViewModell.SelectedProcess.Id;
                ViewModell.UpdatingProcesses();
                dataGrid.SelectedItem = ViewModell.Processes.FirstOrDefault(process => process.Id == processId);
                dataGrid.Focus();
            }
        }

    }
}

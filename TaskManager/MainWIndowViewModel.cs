using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace TaskManager {
    class MainWIndowViewModell : INotifyPropertyChanged{
        private readonly ObservableCollection<Process> _processes;

        public Process _selectedProcess;
        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand KillCommand { get; }

        public IReadOnlyList<Process> Processes => _processes;
        public Process SelectedProcess {
            get => _selectedProcess;
            set {
                if (_selectedProcess == value) return;
                _selectedProcess = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_selectedProcess)));
            }
        }

        public MainWIndowViewModell() {
            _processes = new ObservableCollection<Process>();
            FillСollection();
            SelectedProcess = null;
            KillCommand = new DelegateCommand(KillProcess);
        }

        private void FillСollection() {
            foreach (Process process in Process.GetProcesses())
                _processes.Add(process);
        }

        public void UpdatingProcesses() {
            _processes.Clear();
            FillСollection();
        }

        private void KillProcess() {
            if (SelectedProcess is Process process) {
                try {
                    process.Kill();
                    _processes.Remove(process);
                }
                catch (Win32Exception) {
                    MessageBox.Show(
                        "Операция недопустима для этого процесса",
                        "Не удалось завершить процесс",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                }
            }
        }
    }
}

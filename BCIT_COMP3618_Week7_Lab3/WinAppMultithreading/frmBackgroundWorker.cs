using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinAppMultithreading
{
    /// <summary>
    /// BCIT COMP 3618 
    /// Krzysztof Szczurowski Week 7 Lab 3
    /// Repo: https://github.com/kriss3/BCIT_COMP3618_Week7_Lab3.git
    /// </summary>
    public partial class frmBackgroundWorker : Form
    {
        public frmBackgroundWorker()
        {
            InitializeComponent();
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            int arg = (int)e.Argument;

            // Call time-consuming operation.
            e.Result = Helper.TimeConsumingOperation(bw, arg);

            // If the operation was canceled by the user, 
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //Cancel operation
                MessageBox.Show("Operation was canceled", "Cancelled Operation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.Error != null)
            {
                //Error occured during operation.
                MessageBox.Show($"An error occurec: {e.Error.Message}", "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Operation completed normally.
                MessageBox.Show($"Result = {e.Result}", "Operation Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnStart_Click(object sender, EventArgs e) =>  bgw.RunWorkerAsync(20000);

        private void btnStop_Click(object sender, EventArgs e)
        {
            bgw.WorkerSupportsCancellation = true;
            bgw.CancelAsync();
        }
    }
}

using System;
using System.Windows.Forms;
// Ensure Microsoft.PowerShell.SDK NuGet Package is installed for System.Management namespace
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace STIG_Tool_Beta;

public partial class MainForm : Form
{
    // Tell the compiler that this variable can be null until it's set in MainForm_Load
    private Runspace? _runspace;
    // To handle PowerShell errors related to connection state
    private bool _isConnected = false;

    public MainForm()
    {
        InitializeComponent();
    }

    // When the main windows loads, do this stuff
    private void MainForm_Load(object sender, EventArgs e)
    {
        try
        {
            toolStripStatusLabelMainInfo.Text = "Initializing...";

            // Create Default Runspace Initial Session State
            InitialSessionState initial = InitialSessionState.CreateDefault();

            // Temporarily bypass execution policy
            initial.ExecutionPolicy = Microsoft.PowerShell.ExecutionPolicy.Bypass;

            // Construct an empty PowerShell instance to add commands to
            using (PowerShell psCheck = PowerShell.Create())
            {
                // Create DefaultHost PowerShell runspace
                psCheck.Runspace = RunspaceFactory.CreateRunspace(initial);
                psCheck.Runspace.Open();

                // Construct Get-Module cmdlet
                psCheck.AddCommand("Get-Module")
                       .AddParameter("ListAvailable")
                       .AddParameter("Name", "VMware.PowerCLI");

                // Check PowerCLI install state
                var checkResults = psCheck.Invoke();
                // If not installed...
                if (checkResults.Count == 0)
                {
                    // Ask user to install PowerCLI
                    toolStripStatusLabelMainInfo.Text = "PowerCLI not found!";
                    var dr = MessageBox.Show("VMware.PowerCLI is not installed. Install now?", "Missing PowerCLI", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        // Clear Get-Module from variable
                        psCheck.Commands.Clear();
                        // Construct Install-Module cmdlet
                        psCheck.AddCommand("Install-Module")
                               .AddParameter("Name", "VMware.PowerCLI")
                               .AddParameter("Scope", "CurrentUser")
                               .AddParameter("Force", true);
                        // Attempt PowerCLI install
                        toolStripStatusLabelMainInfo.Text = "Installing PowerCLI...";
                        psCheck.Invoke();
                    }
                }
                // Clean up and close DefaultHost PowerShell runspace
                psCheck.Runspace.Close();
                psCheck.Runspace.Dispose();
            }

            // Now that it's installed or was already there, proceed with PowerCLI import into Initial Session State
            initial.ImportPSModule(new[] { "VMware.PowerCLI" });

            // Create persistent PowerShell runspace
            _runspace = RunspaceFactory.CreateRunspace(initial);
            _runspace.Open();

            toolStripStatusLabelMainInfo.Text = "Ready!";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error initializing PowerCLI: {ex.Message}");
            toolStripStatusLabelMainInfo.Text = "Failed to initialize PowerCLI.";
        }
    }

    // When you click the 'Connect' button
    private void buttonConnect_Click(object sender, EventArgs e)
    {
        string computerName = textBoxComputerName.Text;
        string user = textBoxUser.Text;
        string pass = maskedTextBoxPassword.Text;

        try
        {
            using (PowerShell ps = PowerShell.Create())
            {
                // Use the existing runspace
                ps.Runspace = _runspace;

                ps.AddCommand("Connect-VIServer")
                  .AddParameter("Server", computerName)
                  .AddParameter("User", user)
                  .AddParameter("Password", pass);

                toolStripStatusLabelMainInfo.Text = $"Connecting to {computerName}...";

                var results = ps.Invoke();
                if (ps.HadErrors)
                {
                    var errorMsg = ps.Streams.Error[0].ToString();
                    MessageBox.Show("Connect-VIServer failed: " + errorMsg);
                    toolStripStatusLabelMainInfo.Text = "Connect failed.";
                }
                else
                {
                    // Set connection state
                    _isConnected = true;
                    toolStripStatusLabelMainInfo.Text = $"Connected to {computerName} successfully.";
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error connecting: " + ex.Message);
        }
    }

    // When you click the 'Disconnect' button
    private void buttonDisconnect_Click(object sender, EventArgs e)
    {
        // Only run disconnect logic if we're connected
        if (!_isConnected)
        {
            toolStripStatusLabelMainInfo.Text = "No active connection to disconnect.";
            return;
        }

        try
        {
            using (PowerShell ps = PowerShell.Create())
            {
                // Use persistent PowerShell runspace19
                ps.Runspace = _runspace;

                // Construct Disconnect-VIServer cmdlet
                ps.AddCommand("Disconnect-VIServer")
                  .AddParameter("Confirm", false);

                // Invoke cmdlet and capture any errors
                toolStripStatusLabelMainInfo.Text = "Disconnecting...";
                var results = ps.Invoke();
                if (ps.HadErrors)
                {
                    var errorMsg = ps.Streams.Error[0].ToString();
                    MessageBox.Show("Disconnect-VIServer failed: " + errorMsg);
                }
                else
                {
                    // Set connection state
                    _isConnected = false;
                    toolStripStatusLabelMainInfo.Text = "Disconnected successfully.";
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error disconnecting: " + ex.Message);
        }
    }

    // When the form closes, do this stuff
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Disconnect from the server if still connected
        if (_isConnected && _runspace != null && _runspace.RunspaceStateInfo.State == RunspaceState.Opened)
        {
            using (PowerShell ps = PowerShell.Create())
            {
                // Use persistent PowerShell runspace
                ps.Runspace = _runspace;

                // Check if there is an active connection
                // Construct Get-VIServer cmdlet
                ps.AddCommand("Get-VIServer");

                // Invoke cmdlet and capture any errors
                toolStripStatusLabelMainInfo.Text = "Checking active connections...";
                var checkResults = ps.Invoke();
                // If there's any error or no servers, skip disconnect
                if (!ps.HadErrors && checkResults.Count > 0)
                {
                    // Clear Get-VIServer from variable
                    ps.Commands.Clear();
                    // Construct Disconnect-VIServer cmdlet
                    ps.AddCommand("Disconnect-VIServer")
                      .AddParameter("Confirm", false);

                    // Invoke cmdlet and capture any errors
                    toolStripStatusLabelMainInfo.Text = "Disconnecting...";
                    var results = ps.Invoke();
                    if (ps.HadErrors)
                    {
                        var errorMsg = ps.Streams.Error[0].ToString();
                        MessageBox.Show("Disconnect-VIServer failed: " + errorMsg);
                    }
                    toolStripStatusLabelMainInfo.Text = "Disconnected successfully.";
                }
            }

            // Clean up and close persistent PowerShell runspace
            toolStripStatusLabelMainInfo.Text = "Closing...";
            _runspace.Close();
            _runspace.Dispose();
        }
    }
}
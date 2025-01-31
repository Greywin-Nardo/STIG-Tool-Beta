namespace STIG_Tool_Beta;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        panelCredentials = new Panel();
        buttonDisconnect = new Button();
        buttonConnect = new Button();
        labelPassword = new Label();
        maskedTextBoxPassword = new MaskedTextBox();
        textBoxUser = new TextBox();
        labelUser = new Label();
        textBoxComputerName = new TextBox();
        labelComputerName = new Label();
        statusStripMain = new StatusStrip();
        toolStripStatusLabelMainInfo = new ToolStripStatusLabel();
        panelCredentials.SuspendLayout();
        statusStripMain.SuspendLayout();
        SuspendLayout();
        // 
        // panelCredentials
        // 
        panelCredentials.Controls.Add(buttonDisconnect);
        panelCredentials.Controls.Add(buttonConnect);
        panelCredentials.Controls.Add(labelPassword);
        panelCredentials.Controls.Add(maskedTextBoxPassword);
        panelCredentials.Controls.Add(textBoxUser);
        panelCredentials.Controls.Add(labelUser);
        panelCredentials.Controls.Add(textBoxComputerName);
        panelCredentials.Controls.Add(labelComputerName);
        panelCredentials.Dock = DockStyle.Top;
        panelCredentials.Location = new Point(0, 0);
        panelCredentials.Name = "panelCredentials";
        panelCredentials.Size = new Size(800, 100);
        panelCredentials.TabIndex = 0;
        // 
        // buttonDisconnect
        // 
        buttonDisconnect.Location = new Point(220, 35);
        buttonDisconnect.Name = "buttonDisconnect";
        buttonDisconnect.Size = new Size(75, 23);
        buttonDisconnect.TabIndex = 7;
        buttonDisconnect.Text = "Disconnect";
        buttonDisconnect.UseVisualStyleBackColor = true;
        buttonDisconnect.Click += buttonDisconnect_Click;
        // 
        // buttonConnect
        // 
        buttonConnect.Location = new Point(220, 6);
        buttonConnect.Name = "buttonConnect";
        buttonConnect.Size = new Size(75, 23);
        buttonConnect.TabIndex = 6;
        buttonConnect.Text = "Connect";
        buttonConnect.UseVisualStyleBackColor = true;
        buttonConnect.Click += buttonConnect_Click;
        // 
        // labelPassword
        // 
        labelPassword.AutoSize = true;
        labelPassword.Location = new Point(51, 67);
        labelPassword.Name = "labelPassword";
        labelPassword.Size = new Size(60, 15);
        labelPassword.TabIndex = 5;
        labelPassword.Text = "Password:";
        // 
        // maskedTextBoxPassword
        // 
        maskedTextBoxPassword.Location = new Point(114, 64);
        maskedTextBoxPassword.Name = "maskedTextBoxPassword";
        maskedTextBoxPassword.Size = new Size(100, 23);
        maskedTextBoxPassword.TabIndex = 4;
        maskedTextBoxPassword.UseSystemPasswordChar = true;
        // 
        // textBoxUser
        // 
        textBoxUser.Location = new Point(114, 35);
        textBoxUser.Name = "textBoxUser";
        textBoxUser.Size = new Size(100, 23);
        textBoxUser.TabIndex = 3;
        // 
        // labelUser
        // 
        labelUser.AutoSize = true;
        labelUser.Location = new Point(78, 38);
        labelUser.Name = "labelUser";
        labelUser.Size = new Size(33, 15);
        labelUser.TabIndex = 2;
        labelUser.Text = "User:";
        // 
        // textBoxComputerName
        // 
        textBoxComputerName.Location = new Point(114, 6);
        textBoxComputerName.Name = "textBoxComputerName";
        textBoxComputerName.Size = new Size(100, 23);
        textBoxComputerName.TabIndex = 1;
        // 
        // labelComputerName
        // 
        labelComputerName.AutoSize = true;
        labelComputerName.Location = new Point(12, 9);
        labelComputerName.Name = "labelComputerName";
        labelComputerName.Size = new Size(99, 15);
        labelComputerName.TabIndex = 0;
        labelComputerName.Text = "Computer Name:";
        // 
        // statusStripMain
        // 
        statusStripMain.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelMainInfo });
        statusStripMain.Location = new Point(0, 428);
        statusStripMain.Name = "statusStripMain";
        statusStripMain.Size = new Size(800, 22);
        statusStripMain.TabIndex = 1;
        // 
        // toolStripStatusLabelMainInfo
        // 
        toolStripStatusLabelMainInfo.Name = "toolStripStatusLabelMainInfo";
        toolStripStatusLabelMainInfo.Size = new Size(60, 17);
        toolStripStatusLabelMainInfo.Text = "Welcome!";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(statusStripMain);
        Controls.Add(panelCredentials);
        Name = "MainForm";
        Text = "ESXi STIG Compliance Tool";
        FormClosing += MainForm_FormClosing;
        Load += MainForm_Load;
        panelCredentials.ResumeLayout(false);
        panelCredentials.PerformLayout();
        statusStripMain.ResumeLayout(false);
        statusStripMain.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Panel panelCredentials;
    private Label labelComputerName;
    private TextBox textBoxUser;
    private Label labelUser;
    private TextBox textBoxComputerName;
    private Button buttonConnect;
    private Label labelPassword;
    private MaskedTextBox maskedTextBoxPassword;
    private StatusStrip statusStripMain;
    private Button buttonDisconnect;
    private ToolStripStatusLabel toolStripStatusLabelMainInfo;
}

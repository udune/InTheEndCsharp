namespace AsyncUI;

partial class Form1
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        btnAsync = new System.Windows.Forms.Button();
        lbLog = new System.Windows.Forms.ListBox();
        progressBar = new System.Windows.Forms.ProgressBar();
        btnSync = new System.Windows.Forms.Button();
        button1 = new System.Windows.Forms.Button();
        btnAsyncStream = new System.Windows.Forms.Button();
        btnStop = new System.Windows.Forms.Button();
        btnLongRunning = new System.Windows.Forms.Button();
        btnAttached = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // btnAsync
        // 
        btnAsync.Location = new System.Drawing.Point(26, 25);
        btnAsync.Name = "btnAsync";
        btnAsync.Size = new System.Drawing.Size(140, 68);
        btnAsync.TabIndex = 0;
        btnAsync.Text = "비동기 테스트";
        btnAsync.UseVisualStyleBackColor = true;
        btnAsync.Click += btnAsync_Click;
        // 
        // lbLog
        // 
        lbLog.FormattingEnabled = true;
        lbLog.Location = new System.Drawing.Point(187, 25);
        lbLog.Name = "lbLog";
        lbLog.Size = new System.Drawing.Size(225, 304);
        lbLog.TabIndex = 1;
        // 
        // progressBar
        // 
        progressBar.Location = new System.Drawing.Point(187, 341);
        progressBar.Name = "progressBar";
        progressBar.Size = new System.Drawing.Size(224, 32);
        progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
        progressBar.TabIndex = 2;
        // 
        // btnSync
        // 
        btnSync.Location = new System.Drawing.Point(26, 117);
        btnSync.Name = "btnSync";
        btnSync.Size = new System.Drawing.Size(140, 68);
        btnSync.TabIndex = 3;
        btnSync.Text = "동기 테스트";
        btnSync.UseVisualStyleBackColor = true;
        btnSync.Click += btnSync_Click;
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(26, 216);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(140, 68);
        button1.TabIndex = 4;
        button1.Text = "데드락";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // btnAsyncStream
        // 
        btnAsyncStream.Location = new System.Drawing.Point(438, 25);
        btnAsyncStream.Name = "btnAsyncStream";
        btnAsyncStream.Size = new System.Drawing.Size(140, 68);
        btnAsyncStream.TabIndex = 5;
        btnAsyncStream.Text = "비동기 스트림";
        btnAsyncStream.UseVisualStyleBackColor = true;
        btnAsyncStream.Click += btnAsyncStream_Click;
        // 
        // btnStop
        // 
        btnStop.Location = new System.Drawing.Point(438, 117);
        btnStop.Name = "btnStop";
        btnStop.Size = new System.Drawing.Size(140, 68);
        btnStop.TabIndex = 6;
        btnStop.Text = "중지";
        btnStop.UseVisualStyleBackColor = true;
        btnStop.Click += btnStop_Click;
        // 
        // btnLongRunning
        // 
        btnLongRunning.Location = new System.Drawing.Point(438, 216);
        btnLongRunning.Name = "btnLongRunning";
        btnLongRunning.Size = new System.Drawing.Size(140, 68);
        btnLongRunning.TabIndex = 7;
        btnLongRunning.Text = "Long Running";
        btnLongRunning.UseVisualStyleBackColor = true;
        btnLongRunning.Click += btnLongRunning_Click;
        // 
        // btnAttached
        // 
        btnAttached.Location = new System.Drawing.Point(605, 216);
        btnAttached.Name = "btnAttached";
        btnAttached.Size = new System.Drawing.Size(140, 68);
        btnAttached.TabIndex = 8;
        btnAttached.Text = "Attached To Parent";
        btnAttached.UseVisualStyleBackColor = true;
        btnAttached.Click += btnAttached_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(btnAttached);
        Controls.Add(btnLongRunning);
        Controls.Add(btnStop);
        Controls.Add(btnAsyncStream);
        Controls.Add(button1);
        Controls.Add(btnSync);
        Controls.Add(progressBar);
        Controls.Add(lbLog);
        Controls.Add(btnAsync);
        Text = "Form1";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button btnAttached;

    private System.Windows.Forms.Button btnLongRunning;

    private System.Windows.Forms.Button btnStop;

    private System.Windows.Forms.Button btnAsyncStream;

    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.Button btnSync;

    private System.Windows.Forms.ProgressBar progressBar;

    private System.Windows.Forms.ListBox lbLog;

    private System.Windows.Forms.Button btnAsync;

    #endregion
}
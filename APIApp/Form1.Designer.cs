namespace APIApp;

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
        btnGetUser = new System.Windows.Forms.Button();
        dgv = new System.Windows.Forms.DataGridView();
        btnGetUsers = new System.Windows.Forms.Button();
        btnPostUser = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
        SuspendLayout();
        // 
        // btnGetUser
        // 
        btnGetUser.Location = new System.Drawing.Point(11, 11);
        btnGetUser.Name = "btnGetUser";
        btnGetUser.Size = new System.Drawing.Size(224, 49);
        btnGetUser.TabIndex = 0;
        btnGetUser.Text = "Get User";
        btnGetUser.UseVisualStyleBackColor = true;
        btnGetUser.Click += btnGetUser_Click;
        // 
        // dgv
        // 
        dgv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
        dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgv.Location = new System.Drawing.Point(12, 66);
        dgv.Name = "dgv";
        dgv.Size = new System.Drawing.Size(776, 372);
        dgv.TabIndex = 1;
        dgv.Text = "dataGridView1";
        // 
        // btnGetUsers
        // 
        btnGetUsers.Location = new System.Drawing.Point(241, 12);
        btnGetUsers.Name = "btnGetUsers";
        btnGetUsers.Size = new System.Drawing.Size(224, 49);
        btnGetUsers.TabIndex = 2;
        btnGetUsers.Text = "Get Users";
        btnGetUsers.UseVisualStyleBackColor = true;
        btnGetUsers.Click += btnGetUsers_Click;
        // 
        // btnPostUser
        // 
        btnPostUser.Location = new System.Drawing.Point(471, 11);
        btnPostUser.Name = "btnPostUser";
        btnPostUser.Size = new System.Drawing.Size(224, 49);
        btnPostUser.TabIndex = 3;
        btnPostUser.Text = "Post User";
        btnPostUser.UseVisualStyleBackColor = true;
        btnPostUser.Click += btnPostUser_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(btnPostUser);
        Controls.Add(btnGetUsers);
        Controls.Add(dgv);
        Controls.Add(btnGetUser);
        Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button btnPostUser;

    private System.Windows.Forms.Button btnGetUsers;

    private System.Windows.Forms.DataGridView dgv;

    private System.Windows.Forms.Button btnGetUser;

    #endregion
}
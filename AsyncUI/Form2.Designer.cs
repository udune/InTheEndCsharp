namespace AsyncUI
{
    partial class Form2
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
      btnWriteDummyFile = new Button();
      btnCopyFile = new Button();
      lblWrite = new Label();
      lblWriteProgress = new Label();
      btnOpenFolder = new Button();
      txtDummyFileName = new TextBox();
      label1 = new Label();
      label2 = new Label();
      txtSource = new TextBox();
      label3 = new Label();
      txtDestination = new TextBox();
      lblCopyProgress = new Label();
      label5 = new Label();
      lblWriteError = new Label();
      lblCopyError = new Label();
      SuspendLayout();
      // 
      // btnWriteDummyFile
      // 
      btnWriteDummyFile.Location = new Point(284, 80);
      btnWriteDummyFile.Name = "btnWriteDummyFile";
      btnWriteDummyFile.Size = new Size(168, 55);
      btnWriteDummyFile.TabIndex = 0;
      btnWriteDummyFile.Text = "5GB 더미 파일 만들기";
      btnWriteDummyFile.UseVisualStyleBackColor = true;
      btnWriteDummyFile.Click += btnWriteDummyFile_Click;
      // 
      // btnCopyFile
      // 
      btnCopyFile.Location = new Point(284, 191);
      btnCopyFile.Name = "btnCopyFile";
      btnCopyFile.Size = new Size(168, 52);
      btnCopyFile.TabIndex = 2;
      btnCopyFile.Text = "더미 파일 복사하기";
      btnCopyFile.UseVisualStyleBackColor = true;
      btnCopyFile.Click += btnCopyFile_Click;
      // 
      // lblWrite
      // 
      lblWrite.AutoSize = true;
      lblWrite.Location = new Point(12, 144);
      lblWrite.Name = "lblWrite";
      lblWrite.Size = new Size(94, 15);
      lblWrite.TabIndex = 5;
      lblWrite.Text = "파일 생성 진행: ";
      // 
      // lblWriteProgress
      // 
      lblWriteProgress.AutoSize = true;
      lblWriteProgress.Location = new Point(112, 144);
      lblWriteProgress.Name = "lblWriteProgress";
      lblWriteProgress.Size = new Size(26, 15);
      lblWriteProgress.TabIndex = 6;
      lblWriteProgress.Text = "0/0";
      // 
      // btnOpenFolder
      // 
      btnOpenFolder.Location = new Point(12, 5);
      btnOpenFolder.Name = "btnOpenFolder";
      btnOpenFolder.Size = new Size(440, 69);
      btnOpenFolder.TabIndex = 7;
      btnOpenFolder.Text = "실행 폴더 열기";
      btnOpenFolder.UseVisualStyleBackColor = true;
      btnOpenFolder.Click += btnOpenFolder_Click;
      // 
      // txtDummyFileName
      // 
      txtDummyFileName.Location = new Point(112, 80);
      txtDummyFileName.Name = "txtDummyFileName";
      txtDummyFileName.Size = new Size(166, 23);
      txtDummyFileName.TabIndex = 8;
      txtDummyFileName.Text = "dummy_5GB_file.bin";
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new Point(12, 84);
      label1.Name = "label1";
      label1.Size = new Size(94, 15);
      label1.TabIndex = 9;
      label1.Text = "더미 파일 이름: ";
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Location = new Point(12, 195);
      label2.Name = "label2";
      label2.Size = new Size(90, 15);
      label2.TabIndex = 11;
      label2.Text = "복사할 파일명: ";
      // 
      // txtSource
      // 
      txtSource.Location = new Point(112, 191);
      txtSource.Name = "txtSource";
      txtSource.Size = new Size(166, 23);
      txtSource.TabIndex = 10;
      txtSource.Text = "dummy_5GB_file.bin";
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Location = new Point(12, 224);
      label3.Name = "label3";
      label3.Size = new Size(90, 15);
      label3.TabIndex = 13;
      label3.Text = "복사될 파일명: ";
      // 
      // txtDestination
      // 
      txtDestination.Location = new Point(112, 220);
      txtDestination.Name = "txtDestination";
      txtDestination.Size = new Size(166, 23);
      txtDestination.TabIndex = 12;
      txtDestination.Text = "dummy_5GB_file_copy.bin";
      // 
      // lblCopyProgress
      // 
      lblCopyProgress.AutoSize = true;
      lblCopyProgress.Location = new Point(112, 252);
      lblCopyProgress.Name = "lblCopyProgress";
      lblCopyProgress.Size = new Size(26, 15);
      lblCopyProgress.TabIndex = 15;
      lblCopyProgress.Text = "0/0";
      // 
      // label5
      // 
      label5.AutoSize = true;
      label5.Location = new Point(12, 252);
      label5.Name = "label5";
      label5.Size = new Size(94, 15);
      label5.TabIndex = 14;
      label5.Text = "파일 복사 진행: ";
      // 
      // lblWriteError
      // 
      lblWriteError.AutoSize = true;
      lblWriteError.ForeColor = Color.Red;
      lblWriteError.Location = new Point(12, 168);
      lblWriteError.Name = "lblWriteError";
      lblWriteError.Size = new Size(0, 15);
      lblWriteError.TabIndex = 16;
      // 
      // lblCopyError
      // 
      lblCopyError.AutoSize = true;
      lblCopyError.ForeColor = Color.Red;
      lblCopyError.Location = new Point(12, 277);
      lblCopyError.Name = "lblCopyError";
      lblCopyError.Size = new Size(0, 15);
      lblCopyError.TabIndex = 17;
      // 
      // Form2
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(533, 340);
      Controls.Add(lblCopyError);
      Controls.Add(lblWriteError);
      Controls.Add(lblCopyProgress);
      Controls.Add(label5);
      Controls.Add(label3);
      Controls.Add(txtDestination);
      Controls.Add(label2);
      Controls.Add(txtSource);
      Controls.Add(label1);
      Controls.Add(txtDummyFileName);
      Controls.Add(btnOpenFolder);
      Controls.Add(lblWriteProgress);
      Controls.Add(lblWrite);
      Controls.Add(btnCopyFile);
      Controls.Add(btnWriteDummyFile);
      Name = "Form2";
      Text = "Form2";
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private Button btnWriteDummyFile;
    private Button btnCopyFile;
    private Label lblWrite;
    private Label lblWriteProgress;
    private Button btnOpenFolder;
    private TextBox txtDummyFileName;
    private Label label1;
    private Label label2;
    private TextBox txtSource;
    private Label label3;
    private TextBox txtDestination;
    private Label lblCopyProgress;
    private Label label5;
    private Label lblWriteError;
    private Label lblCopyError;
  }
}

using System.Diagnostics;

namespace AsyncUI
{
  public partial class Form2 : Form
  {
    public Form2()
    {
      InitializeComponent();
    }

    // 더미파일 생성
    private async Task WriteDummyFile(string filePath, long fileSizeInBytes, Action<long, long> progressCallback)
    {
      // 버퍼 크기 (쓰기 작업 시 사용하는 크기)
      int bufferSize = 1024 * 1024; // 1MB
      byte[] buffer = new byte[bufferSize];

      // 랜덤 데이터 생성
      Random random = new Random();
      random.NextBytes(buffer);

      try
      {
        // 파일 스트림 생성 및 쓰기
        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
        {
          long bytesWritten = 0;

          while (bytesWritten < fileSizeInBytes)
          {
            long bytesToWrite = Math.Min(bufferSize, fileSizeInBytes - bytesWritten);
            await fs.WriteAsync(buffer, 0, (int)bytesToWrite);
            bytesWritten += bytesToWrite;

            // 진행 상황 콜백 호출
            progressCallback?.Invoke(bytesWritten, fileSizeInBytes);
          }
        }

        Console.WriteLine($"더미 파일 생성 완료: {filePath}");
      }
      catch (Exception ex)
      {
        lblWriteError.Text = $"파일 생성 중 오류 발생: {ex.Message}";
      }
    }

    // 파일 복사
    private async Task CopyFileAsync(string sourceFilePath, string destinationFilePath, Action<long, long> progressCallback)
    {
      const int bufferSize = 1024 * 1024; // 1MB

      try
      {
        using (FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
        using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
        {
          long totalBytes = sourceStream.Length;
          long bytesCopied = 0;

          byte[] buffer = new byte[bufferSize];
          int bytesRead;

          while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
          {
            await destinationStream.WriteAsync(buffer, 0, bytesRead);
            bytesCopied += bytesRead;

            progressCallback?.Invoke(bytesCopied, totalBytes);
          }
        }
      }
      catch (Exception ex)
      {
        lblCopyError.Text = $"파일 복사 중 오류 발생: {ex.Message}";
      }
    }

    private double BytesToMegabytes(long bytes)
    {
      return bytes / (1024.0 * 1024.0);
    }

    // 폴더 열기
    private static void OpenFolder(string folderPath)
    {
      if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
      {
        Console.WriteLine("폴더 경로가 유효하지 않습니다.");
        return;
      }

      try
      {
        Process.Start(new ProcessStartInfo
        {
          FileName = folderPath,
          UseShellExecute = true,
          Verb = "open"
        });

        Console.WriteLine($"폴더가 열렸습니다: {folderPath}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"폴더 열기 중 오류 발생: {ex.Message}");
      }
    }

    private async void btnWriteDummyFile_Click(object sender, EventArgs e)
    {
      lblWriteError.Text = "";

      string dummyFileName = txtDummyFileName.Text.Trim();

      await WriteDummyFile(dummyFileName, 5L * 1024 * 1024 * 1024, (current, total) =>
      {
        lblWriteProgress.Text = $"{BytesToMegabytes(current)}/{BytesToMegabytes(total)} MB ({(double)current / total:P})";
      });
    }

    private async void btnCopyFile_Click(object sender, EventArgs e)
    {
      lblCopyError.Text = "";

      string sourceFilePath = txtSource.Text.Trim();
      string destinationFilePath = txtDestination.Text.Trim();

      await CopyFileAsync(sourceFilePath, destinationFilePath, (current, total) =>
      {
        lblCopyProgress.Text = $"{BytesToMegabytes(current)}MB / {BytesToMegabytes(total)}MB ({(double)current / total:P})";
      });
    }

    private void btnOpenFolder_Click(object sender, EventArgs e)
    {
      OpenFolder(Environment.CurrentDirectory);
    }
  }
}

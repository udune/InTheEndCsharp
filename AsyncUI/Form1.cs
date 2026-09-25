using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AsyncUI;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private async void btnAsync_Click(object sender, EventArgs e)
    {
        //Task task1 = TaskAsync1();
        //Task task2 = TaskAsync2();
        
        //await Task.WhenAll(task1, task2);
        
        Task<string> task3 = TaskAsync3();
        Task<string> task4 = TaskAsync4();

        List<Task<string>> tasks = [task3, task4];
        while (tasks.Count > 0)
        {
            var completedTask = await Task.WhenAny(tasks);
            lbLog.Items.Add(await completedTask);
            tasks.Remove(completedTask);
        }
    }

    async Task TaskAsync1()
    {
        lbLog.Items.Add("TaskAsync1 Started");
        await Task.Delay(3000);
        lbLog.Items.Add("TaskAsync1 Finished");
    }
    
    async Task TaskAsync2()
    {
        lbLog.Items.Add("TaskAsync2 Started");
        await Task.Delay(1500);
        lbLog.Items.Add("TaskAsync2 Finished");
    }

    async Task<string> TaskAsync3()
    {
        await Task.Delay(3000);
        return "TaskAsync3 Finished";
    }
    
    async Task<string> TaskAsync4()
    {
        await Task.Delay(1500);
        return "TaskAsync4 Finished";
    }

    private void btnSync_Click(object sender, EventArgs e)
    {
        TaskSync1();
        TaskSync2();
    }
    
    void TaskSync1()
    {
        lbLog.Items.Add("TaskSync1 Started");
        Thread.Sleep(3000);
        lbLog.Items.Add("TaskSync1 Finished");
    }
    
    void TaskSync2()
    {
        lbLog.Items.Add("TaskSync2 Started");
        Thread.Sleep(1500);
        lbLog.Items.Add("TaskSync2 Finished");
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        // TaskAsync_Deadlock().Wait();
        
        // string test = TaskAsync_Deadlock2().Result;
        // MessageBox.Show(test);
        
        string test = await TaskAsync_Deadlock2();
        MessageBox.Show(test);
    }

    private async Task TaskAsync_Deadlock()
    {
        await Task.Delay(1000).ConfigureAwait(false);
    }

    private async Task<string> TaskAsync_Deadlock2()
    {
        await Task.Delay(1000).ConfigureAwait(false);
        return "test";
    }

    private async void btnAsyncStream_Click(object sender, EventArgs e)
    {
        if (cts != null)
        {
            MessageBox.Show("작업이 진행중입니다.");
            return;
        }
        
        cts = new CancellationTokenSource();
        
        try
        {
            double result = await Task.Run(GetBigCalculateData);
            
            lbLog.Items.Add(result);
            
            await foreach (int i in GetIntsAsync(cts.Token))
            {
                lbLog.Items.Add(i);
            }
        }
        catch (TaskCanceledException exception)
        {
            MessageBox.Show("작업이 취소되었습니다");
        }
        catch (OperationCanceledException exception)
        {
            MessageBox.Show("작업이 취소되었습니다");
        }
        finally
        {
            cts.Dispose();
            cts = null;
        }
    }

    async IAsyncEnumerable<int> GetIntsAsync([EnumeratorCancellation] CancellationToken token)
    {
        for (int i = 0; i < 10; i++)
        {
            // if (token.IsCancellationRequested)
            // {
            //     throw new OperationCanceledException();
            // }
            token.ThrowIfCancellationRequested();
            
            await Task.Delay(1000, token);
            yield return i;
        }
    }

    private CancellationTokenSource? cts;

    private void btnStop_Click(object sender, EventArgs e)
    {
        cts?.Cancel();
    }

    private double GetBigCalculateData()
    {
        double result = 0;
        for (long i = 0; i < 2000000000; i++)
        {
            result += Math.Sqrt(i);
        }

        return result;
    }

    private async void btnLongRunning_Click(object sender, EventArgs e)
    {
        string result = await Task.Factory.StartNew(() =>
        {
            Task.Delay(5000).Wait();
            return "LongRunning 작업 완료";
        }, TaskCreationOptions.LongRunning);

        lbLog.Items.Add(result);
    }

    private async void btnAttached_Click(object sender, EventArgs e)
    {
        var parentTask = Task.Factory.StartNew(() =>
        {
            var childTask = Task.Factory.StartNew(() =>
            {
                Task.Delay(5000).Wait();
                this.Invoke(() =>
                {
                    lbLog.Items.Add("AttachedToParent 자식 작업 완료!");
                });
            }, TaskCreationOptions.AttachedToParent);

            Task.Delay(1000).Wait();
            return "부모 직업 완료!";
        });

        string result = await parentTask;
        lbLog.Items.Add(result);
    }
}
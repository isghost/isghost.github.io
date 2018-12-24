using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cdemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string name="UNITY";//name就是进程的名称
            int pid=-1;
            Process[] pp = System.Diagnostics.Process.GetProcessesByName(name); 
            for (int i = 0; i < pp.Length; i++)
            {
                if (pp[i].ProcessName == name)
                {
                    pid = pp[i].Id;//这个就是进程的ID 
                }
            }
            if (pid == -1)
            {
                Console.WriteLine("未找到unity.exe进程，请确保Unity已经打开");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("UNITY.exe pid = "+pid);

            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe ";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine("netstat -ano|findstr " + pid);
            p.StandardInput.WriteLine("exit");
            System.Threading.Thread.Sleep(1000);
            string Re = p.StandardOutput.ReadToEnd();
            p.StandardOutput.Close();
            p.WaitForExit();
            p.Close();
            string targetStr = "TCP    0.0.0.0:56";
            int strBegin = Re.IndexOf(targetStr);
            string portStr = Re.Substring(strBegin + targetStr.Length-2,5);
            Console.WriteLine("Unity debug port = "+portStr);
            Console.WriteLine("1. open vs 2015");
            Console.WriteLine("2. Debug -->  Attach Unity Debugger");
            Console.WriteLine("3. Input IP and Run");
            Console.ReadLine();
        }
    }

}

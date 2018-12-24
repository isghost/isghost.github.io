---
layout: post
title: VS2015无法Attach To Unity解决方案
published: true
categories:
tags:
---

## 问题描述

Unity版本```5.2.2f1```  
Visual Studio 2015 Tools for Unity 	版本```2.1.0.0```  
点击Attach To Unity后，VS会弹出一个框让你选择一个Unity的Instance,结果列表为空。在VS官网，几天前也有人提到了这个问题，官方人员回复如下  

	We'll continue offline, and will provide you a debug version to see what is the possible issue.

等更新估计要一段时间。

## 解决方法
经过长时间努力和尝试，找到一个方法。如下  

1. 查看Unity.exe的PID
2. 通过PID和netstat得到Unity的调试端口号
3. 在VS中手动输入端口进行连接

每次查找端口号也是麻烦，写了一个小程序读取Unity.exe的端口，具体连接流程也写在代码里面了。

获取端口的程序[点击下载](images/otherRes/GetPort.exe)

## C#代码
{% codeblock C# %}
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
            string name="UNITY";//name¾ÍÊÇ½ø³ÌµÄÃû³Æ
            int pid=-1;
            Process[] pp = System.Diagnostics.Process.GetProcessesByName(name); 
            for (int i = 0; i < pp.Length; i++)
            {
                if (pp[i].ProcessName == name)
                {
                    pid = pp[i].Id;//Õâ¸ö¾ÍÊÇ½ø³ÌµÄID 
                }
            }
            if (pid == -1)
            {
                Console.WriteLine("Î´ÕÒµ½unity.exe½ø³Ì£¬ÇëÈ·±£UnityÒÑ¾­´ò¿ª");
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
{% endcodeblock %}
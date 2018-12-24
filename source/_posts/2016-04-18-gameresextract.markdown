---
layout: post
title: 游戏资源提取方法合集
published: true
categories:
tags:
---

前言
===
这篇文章主要介绍一些我提取过的美术资源的方法

1.饥荒
===

![dontsarve](/images/resextract/dontstarve_2.jpg)

饥荒的美术资源是没有加密过，但是是```tex```后缀，网上没找到这种格式的相关信息。我找到一个工具，可以
将这种格式的资源转化为png，工具名称```ktool```.[百度网盘ktool下载地址](http://pan.baidu.com/s/1dEEN7dn)
解压后可以在目录下看到```ktech.exe```文件，通过下面的方法进行转换

	ktech.exe srcFileName targetFileName

为了遍历所有的图片，我又写了一个批处理

	cd C:\Users\isghost\Desktop\ktools-4.0-win32\ktools\image
	for /r   %%i in (*.tex)  do (
	   ..\ktech.exe %%i %%~ni.png
	)

这个批处理会遍历所有的tex结尾的文件，将其转化为png。
![dontsarve](/images/resextract/dontstarve.jpg)

[百度网盘下载地址](http://pan.baidu.com/s/1dEEN7dn)，包含饥荒和饥荒dlc的资源

---

2.拳皇98终极之战ol
===
![拳皇98](/images/resextract/ko98.jpg)

由于PNG图片的开头都是一样的。将拳皇的图片资源与普通图片对比，可以发现，除了开头二进制部分有一些不一样，替换成正常头部，就可以读取了。

![拳皇98](/images/resextract/ko98_2.jpg)
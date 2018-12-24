---
layout: post
title: 在windows环境下安装ursa正确方法
published: true
categories:
tags:
date: 2016-05-21 00:00:00
---

## 前言
* ```crypto```内置加密库居然没有rsa加密(有签名验证)
* 先找了一个不依赖openssl的加密库```node-ras```，日本人写的，加解密都用私钥.....一定是我没有理解作者的意图。
* ```ursa```比较热门，但是依赖```openssl```,windows下还依懒```Python```和```vc```，还有依懒nodejs库```node-gyp```，安装openssl还要依懒perl...

## 步骤
1. 安装node-gyp,```npm install -g node-gyp```
2. 安装perl，由于有msi,下载安装即可（虽然安装时提示是否添加到Path,选了是还是不会添加，手动添加即可）
3. 安装python,下载安装即可
4. 安装VC，windows机子怎么可能会没有VC,略
5. 安装openssl. 
* [openssl下载地址](http://www.openssl.org/source/)，选最新，包体最大准没错(^ ^)
* 解压后，将文件夹名字改为```OpenSSL-Win64```，放到C盘根目录(ursa安装限定死路径，当然可以设置VC的全局INCLUDE,你会发现，这样子更累)
* 配置编译环境，为下面的编译源码做准备，打开```cmd```,运行```C:\Program Files (x86)\Microsoft Visual Studio 11.0\VC\bin\amd64\vcvars64.bat```(win7_64)，不要半闭cmd
* 编译源码，生成lib，步骤详情查看```openssl```根目录下的```INSTALL.W64```
* 编译完成后的文件会输出到```out32dll```，将文件夹名字修改为```lib```
* 检查```C:\OpenSSL-Win64\include\openssl```里的头文件，是否是正确的C文件，如果不是，删除所有，将```C:\OpenSSL-Win64\inc32\openssl```里的文件复制过来(不明白那个错误的C头文件是怎么回事)
6. 最后一步，```npm i --save ursa```,have fun ~ _ ~   

## 编译openssl错误
1. ```目标模块x86与目标计算机x64不匹配```，这个情况可能是不小心运行过vcvars32.bat。这时需要删除根目录下的```tmp32dll```文件夹
2. ```找不到windows.h```，VC一般情况下都会有，我的一台机子居然不见了。在```C:\Promgram File(x86)```环境下搜索```windows.h```，将所在的文件夹目录添加到C++的```include```
3. 添加C++的全局include，~~Tool -> Options -> VC++ Directory，这个方式在新版本vs中已经废弃~~。打开```C:\Program Files (x86)\Microsoft Visual Studio 11.0\VC\VCWizards\default.vcxproj```，这个工程的配置会被转化成全局配置。  

## 附录

* [OpenSSL生成私钥和公钥的方法]http://blog.csdn.net/lvxiangan/article/details/45318443

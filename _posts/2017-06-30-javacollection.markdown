---
layout: post
permalink: /:categories/:collections.html
title: Java容器简介
published: true
categories:
tags:
---

需求
========
一共有N个公会，每个公会有(M1,M2...MN)个人。要进行公会战。
1. 玩法是一对一随机混战。
2. 尽量不和自己公会的人对站

解决思路
=============
假设参赛人数为K人，比舞台数K/2个(先不考虑临界值问题)。遍历所有公会，从可以上场的舞台(人数少于二人)中，随机获取公会人数数量的舞台，将整个公会的人分配进去。
0. 舞台是假想，实际就是一场战斗
1. 优先取空舞台
2. 舞台数不够时，需要处理公会自己人打自己人的情况

问题转换
=========
从空舞台中随机获取M个舞台，这个问题转换为，从一个数组中，随机选取M个数。

数组的乱序
================
允许改变数组的最快算法
{% highlight C++ %}
void rand_array(int * array, int len) {
	for (int i = len; i > 0; i--) {
		int k = rand() % i;
		int tmp = array[i - 1];
		array[i - 1] = array[k];
		array[k] = tmp;
	}
}
{% endhighlight %}

不允许改变数组的最快算法
{% highlight C++ %}
{% endhighlight %}

从数组中随机获取M个数
==============
实际就是乱序算法的阉割。乱序算法，N个数，随机N-1次。取M个数，只需要随机M次。

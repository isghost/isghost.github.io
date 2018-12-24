---
layout: post
permalink: /:categories/:title.html
title: 游戏限时抽奖策略
published: true
categories: math
tags: normal distribution
---

前言
====
> 游戏抽奖几乎成了现在卡牌类游戏的标准配置，单抽，十连抽，限时抽...。合理的游戏抽奖算法，将会提高玩家的游戏体
验，影响游戏的主要收入。单抽和十连抽大部分游戏都差不多，这篇文章主要讲一下限时抽奖，因为单抽和十连抽主要集中在
开服前期，给点保底奖励就行了，一般也不会在这个上搞活动。限时抽则是游戏运营一段时间后出现，长期的收入要靠他。活
动花样多。

---

实现目标
====
游戏出新英雄了，做一个限时卡活动，希望玩家平均20次能抽到一张整卡。    
**需求**    
1. 平均二十次抽到一张整卡

---

平均方法
====
一次抽中的概率为0.04，每次都进行随机。我们来画一下玩家第一次抽到整卡时抽奖次数的分布    
公式：```P(k)=0.96^(k-1)*0.04```，其中k为第一次抽到整卡的抽奖次数
![平均随机抽奖](/images/math/avg.png)    
从图表上可以看出来，大部分玩家第一次抽中整卡是在第一次的时候，而小部分玩家抽了50次还没中(抽50次基本都是大R，这
时有很大概率反馈说遇到BUG。这显然不是我们想要的结果

---

正态分布随机数
====
虽然需求是平均20次抽到一个整卡， 上面的方法满足了需求，但暴露出来两个问题。    

1. 第一次就抽中整卡的玩家最多(就靠这个赚钱了，怎么可以那么轻松拿到)
2. 50次抽中以后的玩家还有很多(这是一个BUG)

所以更改需求为：
  
1. 平均二十次抽到一张整卡。
2. 大部分玩家是在抽二十次左右第一次抽到整卡。
3. 抽到整卡时，避免抽卡次数过多或者过少。

**如果玩家每次抽奖都独立，那么就无法避免抽很多次也抽不到整卡的情况**。所以，在玩家开始抽奖前，先随机一个整数
n，玩家会在第n次抽奖时抽到整卡。这个n符合上面的需求。```没想出来怎么过度到正态分布，反正用正态分布就可以```。
现在只要保证大量生成的随机数n符合正态分布，那么基本满足需求。

> 生成符合正态分布随机数，这里是传送门:[知乎](http://www.zhihu.com/question/29971598),
[Wiki](https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform)

Lua实现的代码

{% codeblock lua %}
-- @author isghost
-- @date 2016/3/25
-- @param mu 期望值
-- @param sigma 标准差
-- @description 通过Box–Muller方法生成符合正态分布的随机数
function generateGaussianNoise(mu,sigma)
    -- u1,u2 > (0,1]
    local u1,u2
    repeat
        u1 = math.random()
        u2 = math.random()
    until(u1 > 1e-200)
    local z0 = math.sqrt(-2.0 * math.log(u1)) * math.cos(2 * math.pi * u2)
    return z0 * sigma + mu
end
{% endcodeblock %}

测试用例

{% codeblock lua %}
-- 期望值 20，标准差 5
for i=1,10000 do
    local tmp = generateGaussianNoise(20,5)
    print(tmp - tmp%1)
end
{% endcodeblock %}

将这些数绘制成图表

![平均随机抽奖](/images/math/normaldis.png)    

生成的随机数符合正态分布后，对两个极端进行删除(比如小于1和大于50的数)

就是这样。
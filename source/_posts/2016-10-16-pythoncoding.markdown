---
layout: post
title: python编码解析
published: true
categories:
tags:
date: 2016-10-16 00:00:00
---

基本概念
===========
文件常用编码`utf-8`,`gbk`。

python里的编码
===========
python存在三种编码`utf-8`,`gbk`,`unicode`
python读取文件，文件是什么编码，得到的就是什么编码

编码转换
========
`str.decode(coding="utf8")`函数，decode之后得到的是unicode编码。`utf8`或`gbk` ==> `unicode`

`str.encode(coding="utf8")`函数，encode之后得到的是参数类型的编码。 `unicode` ==> `utf8`或`gbk`

无法直接`utf8 <==> gbk`

文件读取
=========


{% codeblock python %}
# 读取GBK编码的文件，文件内容为 你好
f = open("hello.txt","rb")
gbkHello = f.readline()# gbk
unicodeHello = gbkHello.decode("gbk")# unicode
utf8Hello = gbkHello.decode("gbk").encode("utf-8") # utf8
f1 = open("world1.txt","wb")
f2 = open("world2.txt","wb")
f3 = open("world3.txt","wb")
f1.write(gbkHello)
f1.close()
f3.write(utf8Hello)
f3.close()
# 会报错,unicode算是python内置编码，文件是没有unicode编码，所以报错
# UnicodeEncodeError: 'ascii' codec can't encode characters in position 7-8: ordinal not in range(128)
# f2.write(unicodeHello)
# f2.close()
{% endcodeblock %}

`str函数`
===============
这个函数是个巨坑。python的编码应该是比较简单，即使什么都不懂，瞎转一般也能转出个结果。但是，如果将一
个对象用str函数进行，这时你会发现，不管用什么编码转，都得不到正确结果，只能看到\u123或者\x123之类的。

问题原因
------------

{% codeblock python %}
d = {"hello":"你好"}
print(str(d))
{% endcodeblock %}

上面代码显示的结果是 `"{'hello': '\\xc4\\xe3\\xba\\xc3'}"`，第一反应，*中文显示不出来*。陷阱就是在这
里，这个时候已经*不存在中文*，*不存在除ascii以外的字符*。str会将非ascii字符转化为它的书写形式(表示不
太准确)。

解决
----------
python提供了`str.decode("unicode_escape")`这个解码方式。如果原来对象中存在gbk编码的字符串，先将其转换
为unicode编码，再`str(d).decode("unicode_escape")`就能得到正常的字符串。

还有一个小问题，字符串的前面会有一个u字符，不利于数据的保存。str函数可以修改为json.dumps(d)。这个函数
会去掉字符u，其它一样。

{% codeblock python %}
d = {"hello":u"你好"}
#d = {"hello":"你好".decode("gbk")}
print(json.dumps(d).decode("unicode_escape"))
{% endcodeblock %}

总结 
===============
先将对象的字符串全部修改为unicode==>再json.dumps(obj)==>str.decode("unicode_escape") ==> str.encode("utf8")
保存

---
layout: post
permalink: /:categories/:title.html
title: Java容器简介
published: true
categories:
tags:
---

Java容器简介
===
描述顺序按版本来，如有遗漏，欢迎补充

使用到的数据结构
---
1. 数组
2. 链表
3. 哈希表
4. Map
5. 红黑树
6. 跳表
7. 堆

如果对以上数据结构不了解，建议翻看《数据结构》，都是基础知识点，学完之后，再看容器，就会感觉一目了然。
建议把二叉搜索树的整个系列都学习下，包括`B+, B-,B*, AVL，红黑树`。（虽然我看完忘的干干净净，但我觉得可以影响你的思考方式)

依赖的技术
---
1. CAS(Compare and Swap) 如果与预期值相同，那么就交换，否则什么都不干。Java中的主要函数有:
- `UNSAFE.compareAndSwapObject(this, nextOffset, cmp, val);`
- `UNSAFE.putObject(this, itemOffset, item);`
2. volatile 不使用CPU缓存。
3. AQS(AbstractQueuedSynchronized) 基于前面两种技术，主要用于加锁，例如'ReentrantLock`

这里简单描述，细节自行网上查找，每一个都是一个长篇文章。

Java 1.0
---
1. Vector
2. HashTable

已不推荐使用，记住是线程安全，效率低下即可。

Java 1.2
---
由于线程安全容器的效率低下，所以推出线程不安全，但效率高的容器，同步由用户控制。

1. `ArrayList` 使用数据结构`数组`，有自动扩容的功能。
2. `LinkedList` 使用数据结构`链表`，是一个双向`链表`，所以拥有队列和栈的功能
3. `HashMap` 使用数据结构`哈希表`，要点是如何取哈希值，哈希冲突如何解决。

	 * This map usually acts as a binned (bucketed) hash table, but
     * when bins get too large, they are transformed into bins of
     * TreeNodes, each structured similarly to those in
     * java.util.TreeMap
     未细看源码，我的理解是，有哈希冲突时，数据放在一个桶里，当这个桶过大时，将其转化为类似TreeMap的树。

4. `TreeMap` 使用数据结构`红黑树`，由于能根据大小保持顺序，所以key需要继承自`Comparable`或者创建时提供`Comparator`。
5. `LinkedHashMap` 继承自HashMap，通用实现`LinkedHashIterator`等内部类，拥有了按插入顺序访问和LRU功能。
6. `HashSet` 使用HashMap实现(组合形式，非继承),所有的`value`为`private static final Object PRESENT = new Object();`
7. `TreeSet` 默认使用TreeMap实现，创建时，也可以指定实现`NavigableMap`接口的类。同样`value`也为`PRESENT`


Java 1.4
---
1. `LinkedHashSet` 继承自`HashSet`,好像没有添加什么新功能，源码里基本都是构造函数?_?。

Java 1.5
---
在1.5版本里，推出线程安全，阻塞的容器(Blocking系列)，使用1.5中的新锁。还有线程安全，非阻塞容器(Concurrent系列)，使用CAS技术。分别对应于悲观锁和乐观锁思想。

1. `PriorityQueue` 优先队列，使用了数据结构`堆`(非内存中的堆)
2. `ArrayBlockingQueue` 使用数据结构`数组`，固定大小，线程安全，循环使用，不存在移动元素的情况，修改时，使用`ReentrantLock`进行加锁。
3. `LinkedBlockingQueue` 使用数据结构`单向链表`，使用`双锁队列`算法，固定大小，线程安全。`offer`和`take`使用不同的锁。使用到`ReetrantLock`和`Condition`。
4. `LinkedBlockingDeque` 使用数据结构`双向链表`，固定大小，线程安全。单锁，使用`ReentrantLock`加锁。
5. `PriorityBlockingQueue` 使用数据结构`堆`，固定大小，线程安全。单锁，使用`ReentrantLock`加锁。
6. `ConcurrentLinkedQueue` 使用数据结构`链表`，应该是基于这篇论文[《Simple, Fast, and Practical Non-Blocking and Blocking Concurrent Queue Algorithms》](https://www.research.ibm.com/people/m/michael/podc-1996.pdf)修改而来。依赖`CAS`，线程安全。
7. `ConcurrentHashMap` 使用数据结构`哈希表`，依赖`CAS`，线程安全。有点不同的时，发生冲突时，使用`synchronized(f)`同步代码块，估计是树的调整比较麻烦。


Java 1.6
---
1.5中并没有提供`TreeMap`对应的并发版本，如果像`ConcurrentHashMap`一样，直接同步整个代码块，还不如将这个操作交给用户。
`红黑树`在并发中，有一个问题，发生变动时，需要修改的值较多，情况也变多。用无锁设计，情况将变的复杂起来。`跳表`功能与`红黑树`相似，修改相对较少，方便实现无锁设计(源码中的不同情况还是有点多...)，跳表存在的问题是，有多级索引，占用空间过多(空间换时间)。
1. `ConcurrentSkipListMap` 使用数据结构`跳表`,依赖`CAS`，线程安全。
2. `ConcurrentSkipListSet` 跟之前一样`Set`又使用对应版本的Map。默认使用`ConcurrentSkipListMap`或者指定实现`ConcurrentNavigableMap`接口的类。线程安全。


Java 1.7
---
1. `ConcurrentLinkedDeque` 使用数据结构`双向链表`，与`ConcurrentLinkedQueue`类似。


# QUBombPlane

###### 1、设想了飞机拖拽的情况，计划实现一个如同植物大战僵尸或明日方舟那样的拖拽植物或干员到指定位置，然后会显示放置位置的功能，设想了很多功能，最后采用了最麻烦也是最扯淡的方法——触发器（答应我，以后别碰触发器）来做。

在战场上生成了10*10的格子，每个格子上都带有触发器，飞机上也有触发器，当触发器碰撞后，将战场上的格子的按钮可点击选项关掉。

1.1 解决了飞机拖拽的问题，实现了刚开始是飞机图标，鼠标点击后图标变大，然后拖拽飞机。

1.2 在自己的战场生成格子，敌方的战场先不生成格子，等游戏开始再生成格子。



###### 2、飞机在战场中也能旋转，但是飞机在一些特定位置旋转时机翼会突出战场，鼠标点击飞机选择也需要在飞机上新做一个按钮。

2.1 解决了飞机鼠标点击飞机，然后飞机旋转的问题，在飞机上添加了一个按钮来解决此问题。

2.2 优化背景表格，显示数字。

2.3 实现了飞机吸附在表格，，用触发器来完成此项功能。确定了飞机的中心点，希望以此来计算出飞机所占的10个格子是否都在表格内或者是否被占用，并判断飞机能否顺利部署。

2.4在飞机上绑定了十个触发器，找出了飞机所占用的全部格子，并且把格子的状态进行了同步。

2.5 确定了放飞机的范围，让飞机只能在战场范围内部署，超出范围会将飞机退回到上一个部署的位置

2.6 获取了飞机上的Trigger9触发器，如果这个触发器占用的格子为：

【2，0】，【3，0】，【4，0】，【5，0】，【6，0】，【7，0】，

【9，2】，【9，3】，【9，4】，【9，5】，【9，6】，【9，7】，

【2，9】，【3，9】，【4，9】，【5，9】，【6，9】，【7，9】，

【0，2】，【0，3】，【0，4】，【0，5】，【0，6】，【0，7】

则飞机不能旋转，旋转则会让飞机的一个机翼超出战场范围，超出的机翼上的子物体就是Trigger2.

2.7 修复了三架飞机可以重复放置在同一位置的问题（未完全修复）

采用了触发器上的isUse来判定，但还是做不到提前判定，所以决定采用UI提醒的方式来提醒玩家放飞机的方式是错误的。



###### 3、重新设计了UI界面，打算在《孤独摇滚》中取材，比较谁会讨厌可爱的后藤小姐呢！

3.1 优化了主界面的字体显示，按钮UI也找了适合背景图片的绿色UI。

3.2 主界面放置了交互按钮，联机对战和人机对战，人机对战属于是整活，不排除后期开发的可能。



###### 4、开始设计服务器端，服务器端主要用来中转协议，将协议发送个每个参与的玩家，从而实现UI的差异化显示。

4.1 Enter协议，当进入游戏界面时，发送此协议，然后进入游戏，UI界面开始布局，开始生成战场和飞机。

4.2 GameStart协议，布置完成飞机后，获取战场1的每个格子信息，只发送每个格子的名字。

4.3 Play协议，格子生成完成后，每点击一次格子，根据每个格子的状态，显示对应的子物体，子物体主要是图片UI，用来显示是否炸到飞机。

4.4 SetPlane协议，用来发送战场1的格子信息，只发送格子状态为飞机头和飞机身的格子名字。

4.5 Result协议，用来发送胜者的名字给服务器。

4.6 Leave协议，如果对手掉线，发送此协议。


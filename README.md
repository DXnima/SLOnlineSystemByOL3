# 水利信息在线分析服务系统

#### 介绍
水利信息在线分析服务系统
WebGIS

#### 软件架构
OpenLayers3
ASP .NET 4.5
SQL Server

#### 安装教程

1.  将database文件下的数据库文件，都附加到SQL Server
2.  数据库连接修改web.config文件中的IP、用户名、密码
3.  测试环境Visual Studio打开项目运行
4.  部署环境，IIS部署

#### 报错解决
1. HTTP Error 404.3 - Not Found 由于扩展配置问题而无法提供您请求的页面。如果该页面是脚本，请添加处理程序。如果应下载文件，请添加 MIME 映射。
   在IIS，MIME 映射中添加 .geojson、.kml、.gpx后缀文件的映射
   或者调试在.vs/config/applicationhost.config文件下，mimeMap节点下添加下面代码：

   ```   
   <mimeMap fileExtension=".geojson" mimeType="application/geojson" />
   <mimeMap fileExtension=".kml" mimeType="application/kml" />
   <mimeMap fileExtension=".gpx" mimeType="application/gpx" />
   ```
2. .ashx报错： HTTP 错误 404.17 - Not Found 请求的内容似乎是脚本，因而将无法由静态文件处理程序来处理
   IIS添加对ashx文件的支持：https://www.cnblogs.com/szytwo/archive/2012/09/04/2670493.html

3. 编译器错误消息: CS0246: 未能找到类型或命名空间名称“Newtonsoft”(是否缺少 using 指令或程序集引用?)
   源错误:
   ```
	行 14: using Newtonsoft.Json;
   ```
   由于Newtonsoft.Json版本问题，下载本机IIS对应版本的，然后在VS中重新引入。

#### 项目图片
##### 1. 水情与雨情
![输入图片说明](https://images.gitee.com/uploads/images/2021/0525/104445_cd4fc06a_4939108.png "屏幕截图.png")
##### 2. 台风路径
![输入图片说明](https://images.gitee.com/uploads/images/2021/0525/104508_00a8cb74_4939108.png "屏幕截图.png")
##### 3. 云图播放
![输入图片说明](https://images.gitee.com/uploads/images/2021/0525/104531_791598ed_4939108.png "屏幕截图.png")

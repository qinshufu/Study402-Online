# Study402-Online

## 项目简介

一个在线教育项目的实现，采用微服务架构，同时没有太多的业务规则，浏览一下代码会发现，输入都没有做验证，
业务逻辑也不完整。这是因为一个完整的在线教育项目的微服务架构实现不是我编写该项目的目标。

该项目的目的对微服务相关技术架构进行一个展示，目标适用的相关技术架构为：

1. 架构模式适用微服务以及 CQRS，但并没有适用领域驱动设计
2. 适用 Redis 以及 RabbitMQ
3. ...

还有一个比较严重的问题就是没有测试...所以只能看看代码，但是正确运行确实不一定的😀

### 项目结构
```
.
├── sql
└── src
    ├── ContentService
    │   ├── ContentService.Api
    │   │   ├── Application
    │   │   │   ├── MapperProfiles
    │   │   │   ├── RequestHandlers
    │   │   │   └── Requests
    │   │   ├── Controllers
    │   │   ├── Infrastructure
    │   │   ├── Migrations
    │   │   └── Properties
    │   └── ContentService.Model
    │       ├── DataModel
    │       └── ViewModel
    ├── DictionaryService
    │   ├── DictionaryService.Api
    │   │   ├── Application
    │   │   │   ├── RequestHandlers
    │   │   │   └── Requests
    │   │   ├── Controllers
    │   │   ├── Instructure
    │   │   ├── Migrations
    │   │   └── Properties
    │   └── DictionaryService.Model
    │       └── DataModel
    └── StudyOnline.Common
        ├── Configurations
        ├── Expression
        ├── Helpers
        ├── Linq
        └── ViewModel
```

- sql 目录下为填充虚假内容的 sql
- ContentService DictionaryService 分别为对应微服务的目录
- StudyOneline.Common 项目为公用的类库
- RequestHandlers 保存对应请求的处理器 Requests 目录保存对应请求
- Infrastucture 保存基础设施
- ContentService.Model DictionaryService.Model 保存试图模型以及数据库模型
- ....

## 需要注意的问题

### 请求输入的验证

首先，该项目的 Web API 没有对输入的请求进行验证，这是一个很严重的问题，在实际的公司项目中不要学习这里不做验证而必须应该进行验证。

关于输入数据的验证主要有两个方面：

1. 一个是数据格式上面的与业务逻辑无关的
2. 另外一个是与业务逻辑相关的，比如验证操作的课程是否存在等

关于这两类验证应该放在那里是一个值得讨论的问题，它们是应该放在一起还是应该拆分在不同的位置呢？个人看法是这两类验证应该拆分。

## 开发环境

VS2022 + .NET7 + windows10 + Docker Desktop
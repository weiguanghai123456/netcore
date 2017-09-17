旅游网站接口1.0版本
使用.netcore 2.0.0
使用sqlite数据库
2017-09-07 17:00:00 韦广海

一,接口说明:
1,所有接口都可以get或者post
2,所有接口只要是200代码都有个通用的json结构(如下)
{
    "code": -1,
    "msg": "通用错误",
    "result": null
}
注释:
code等于0表示此次业务请求成功  result根据业务有可能有，也有可能没有
code等于非0表示此次业务请求有误 result=null
result是一个json字符串

二,接口详情:
1.1,登录接口
http://47.52.63.221/user/login?name=hai&pwd=123
参数:
name=admin pwd=admin
返回结果:
{
    "code": 0,
    "msg": "",
    "result": {
        "id": "0",
        "name": "admin",
        "pwd": "admin",
        "alias": "管理员",
        "power": "1",
        "photo": "头像base64字符",
        "enable": "1",
        "createTime": "2017-09-17 11:13:37",
        "updateTime": "2017-09-17 11:13:39"
    }
}

2.1,文章添加接口（增）
http://47.52.63.221/article/insert?title=文章1&content=内容1&photo=base64string&type=type1
参数:
title=文章1 content=内容1 photo=base64string type=type1   各数据可以为空字符串
返回结果:
{
    "code": 0,
    "msg": "",
    "result": true
}

2.2,文章删除接口(删)
http://47.52.63.221/article/delete?id=6bde3712-c0a4-4ec7-96a7-f1ee1ee77738
参数:
id=6bde3712-c0a4-4ec7-96a7-f1ee1ee77738   数据的id(guid由后台生成)
返回结果:
{
    "code": 0,
    "msg": "",
    "result": true
}

2.3,文章查询接口(分页)(查)
http://47.52.63.221/article/getList?limit=0&offset=2
参数:
limit=0 offset=2   (表示从0下标开始,往下读2条数据)
返回结果:
{
    "code": 0,
    "msg": "",
    "result": {
        "orderby": null,
        "limit": 0,
        "offset": 0,
        "total": 2,
        "rows": [
            {
                "id": "1",
                "title": "大事件",
                "content": "啦啦啦",
                "photo": "",
                "type": "",
                "createTime": "",
                "updateTime": ""
            },
            {
                "id": "43434",
                "title": "大事件1",
                "content": "哈哈",
                "photo": "34",
                "type": "2",
                "createTime": "",
                "updateTime": ""
            }
        ]
    }
}

2.4,文章更新接口(改)
http://47.52.63.221/article/delete?id=6bde3712-c0a4-4ec7-96a7-f1ee1ee77738&title=文章1&content=内容1&photo=base64string&type=type1
参数:
id=6bde3712-c0a4-4ec7-96a7-f1ee1ee77738&title=文章1&content=内容1&photo=base64string&type=type1
根据数据的id 修改成后面参数的内容
返回结果:
{
    "code": 0,
    "msg": "",
    "result": true
}

3,新闻增删改查接口与文章接口一样
比如增加接口
http://47.52.63.221/article/insert
只需要把article替换成new
http://47.52.63.221/new/insert
参数目前两个都是一样的,以此类推


# de:code 2017 TL07 デモ

## 環境

* Visual Studio 2017
* SQL Server 2016 Express
    * aspnetdb  
      Membership(aspnet_regsql.exe)
    * decodedon  
      デモ用
```
CREATE TABLE [dbo].[Toots]
(
  [Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
  [Name] NVARCHAR(256) NOT NULL, 
  [Text] NTEXT NULL, 
  [CreateAt] DATETIME NOT NULL
)
```

## ソリューション

* decodedon.v35  
    .NET3.5で作成したソリューション    
* decodedon.v35_v46_complete  
    .NET3.5でを.NET4.6.2に移行したソリューション    
* decodedon.v46  
    .NET4.6.2で作成したソリューション    
* decodedon.complete  
    スタイルを適用した完成版

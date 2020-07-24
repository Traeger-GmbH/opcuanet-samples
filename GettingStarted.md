# Getting started

## 1. Create Solution

```bash
dotnet new sln

mkdir ClientApp
cd ClientApp
dotnet new console
dotnet add package Opc.UaFx.Client

cd ..
dotnet sln add ./ClientApp/ClientApp.csproj

mkdir ServerApp
cd ServerApp
dotnet new console
dotnet add package Opc.UaFx.Advanced

cd ..
dotnet sln add ./ServerApp/ServerApp.csproj

```

## 2. Implement Server

```csharp
var node = new OpcDataVariableNode<string>("Hello", value: "Hello World!");

using (var server = new OpcServer(node)) {
    server.Start();

    Console.Write("Started.");
    Console.ReadLine();
}
```

## 3. Implement Client

```csharp
using (var client = new OpcClient("opc.tcp://localhost:4840")) {
    client.Connect();

    Console.Write(client.ReadNode("ns=2;s=Hello"));
    Console.ReadLine();
}
```

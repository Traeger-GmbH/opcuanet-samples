# Client.Unity Sample

This sample project demonstrates how the OPC UA .NET SDK can be used in Unity to interact with an OPC UA Server.

## Requirements

To get started with the sample you have to assure the following requirements are fulfilled:

* at least Unity Hub v2.4.5 is installed
* at least Unity v2020.3.15f2 is installed

## Getting started

1. Clone this repository

2. Start the Playground Server (./Scenarios/Server.Playground) using

```shell
dotnet run --framework netcoreapp3.1
```

3. Open the 'Client.Unity' project

4. Add the [Opc.UaFx.Client Unity Package](https://docs.traeger.de/en/software/sdk/opc-ua/net/start#download)
   1. Choose the menu entry 'Assets > Import Package > Custom Package...'
   2. Browse to the *.unitypackage file and click 'Open'
   3. Keep every item in the package checked and click 'Import'

5. Open the 'SampleScene' in Unity (Assets/Scenes)
   1. Select the UI element 'statusText'
   2. Assign the script 'OpcUaClientBehaviour' in the inspector

6. Start the Unity Player

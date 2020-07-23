// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

using namespace System;
using namespace Opc::UaFx::Client;

void CommunicateWithServer(OpcClient% client);
void HandleAppStarted(Object^ sender, EventArgs^ e);

/// <summary>
/// This sample demonstrates how to implement a primitive OPC UA client.
/// </summary>
int main(array<String^>^ args)
{
    //// If the server domain name does not match localhost just replace it
    //// e.g. with the IP address or name of the server machine.

    #pragma region 1st Way : Use the OpcClient class.
    {
        // The OpcClient class interacts with one OPC UA server. While this class
        // provides session based access to the different OPC UA services of the
        // server, it does not implement a main loop.
        OpcClient client(L"opc.tcp://localhost:4840/SampleServer");

        client.Connect();
        CommunicateWithServer(client);
        client.Disconnect();
    }
    #pragma endregion

    #pragma region 2nd Way : Use the OpcClientApplication class.
    {
        //// The OpcClientApplication class uses a single OpcClient instance which is
        //// wrapped within a main loop.
        ////
        //// Remarks
        //// - The app instance starts a main loop when the session to the server has
        ////   been established.
        //// - Custom client/session dependent code have to be implemented within the event
        ////   handler of the Started event.
        //OpcClientApplication app(L"opc.tcp://localhost:4840/SampleServer");
        //app.Started += gcnew EventHandler(HandleAppStarted);

        //app.Run();
    }
    #pragma endregion
}

void CommunicateWithServer(OpcClient %client)
{
    Console::WriteLine(L"ReadNode: {0}", client.ReadNode("ns=2;s=Machine_1/IsActive"));
    client.WriteNode(L"ns=2;s=Machine_1/IsActive", false);
    Console::WriteLine(L"ReadNode: {0}", client.ReadNode("ns=2;s=Machine_1/IsActive"));
}

void HandleAppStarted(Object^ sender, EventArgs^ e)
{
    CommunicateWithServer(*(static_cast<OpcClientApplication^>(sender))->Client);
}

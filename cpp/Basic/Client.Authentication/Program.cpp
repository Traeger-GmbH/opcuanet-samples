// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

using namespace System;

using namespace Opc::UaFx;
using namespace Opc::UaFx::Client;


/// <summary>
/// This sample demonstrates how to authenticate at an OPC UA server using user name
/// and password.
/// </summary>
int main(array<String^>^ args)
{
    //// If the server domain name does not match localhost just replace it
    //// e.g. with the IP address or name of the server machine.

    OpcClient client(L"opc.tcp://localhost:4840/SampleServer");

    // Just configure the OpcClient instance with an appropriate user identity with
    // the name of the user and its password to use to authenticate.
    client.Security->UserIdentity = gcnew OpcClientIdentity(L"username", L"password");

    client.Connect();
    Console::WriteLine(L"ReadNode: {0}", client.ReadNode(L"ns=2;s=Machine_1/IsActive"));

    try {
        client.WriteNode(L"ns=2;s=Machine_1/IsActive", false);
    }
    catch (Exception^ ex) {
        Console::WriteLine(ex->Message);
    }

    Console::WriteLine(L"ReadNode: {0}", client.ReadNode(L"ns=2;s=Machine_1/IsActive"));

    client.Disconnect();
    Console::ReadKey(true);
}

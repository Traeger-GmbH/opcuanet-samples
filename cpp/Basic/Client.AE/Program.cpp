// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

#include "Observer.hpp"

using namespace Opc::UaFx;
using namespace Opc::UaFx::Client;

using namespace AE;

/// <summary>
/// This sample demonstrates how to perform handling of Alarm + Events (AE) on the nodes
/// provided by the OPC UA server.
/// </summary>
int main(array<String^>^ args)
{
    //// If the server domain name does not match localhost just replace it
    //// e.g. with the IP address or name of the server machine.

    OpcClient client(L"opc.tcp://localhost:4840/SampleServer");
    client.Connect();

    Observer::Observe(client);
    client.Disconnect();
}

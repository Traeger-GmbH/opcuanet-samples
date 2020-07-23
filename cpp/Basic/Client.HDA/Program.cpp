// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

using namespace System;

using namespace Opc::UaFx;
using namespace Opc::UaFx::Client;

/// <summary>
/// This sample demonstrates how to perform history data access (HDA) on the data provided by
/// the OPC UA server.
/// </summary>
int main(array<String^>^ args)
{
    //// If the server domain name does not match localhost just replace it
    //// e.g. with the IP address or name of the server machine.

    OpcClient client(L"opc.tcp://localhost:4840/SampleServer");
    client.Connect();

    {
        // Read the historical 'Raw' data.
        // - for one specific node.
        // - the whole history in one request.
        auto rawHistory = client.ReadNodeHistory(
            DateTime::UtcNow.Date.AddHours(6),
            DateTime::UtcNow.Date.AddHours(6).AddSeconds(10),
            L"ns=2;s=Machine_1/Position");

        Console::WriteLine(L"Read the historical 'Raw' data...");

        for each (auto item in rawHistory)
            Console::WriteLine(L"{0} - {1}", item->Timestamp, item->Value);
    }

    {
        // Read the historical 'Raw' data page wise.
        // - for one specific node.
        // - the whole history partitioned into multiple requests.
        auto rawHistoryNavigator = client.ReadNodeHistory(
            DateTime::UtcNow.Date.AddHours(6),
            DateTime::UtcNow.Date.AddHours(6).AddSeconds(10),
            2,
            "ns=2;s=Machine_1/Position");

        Console::WriteLine(L"Read the historical 'Raw' data page wise...");

        do {
            for each (auto item in rawHistoryNavigator)
                Console::WriteLine(L"{0} - {1}", item->Timestamp, item->Value);

            Console::Write(L"Press any key to read the next page...");
            Console::ReadKey();
            Console::WriteLine();
        } while (rawHistoryNavigator->MoveNextPage());
    }

    {
        // Read the historical 'ModifiedRaw' data.
        // - for one specific node.
        // - the whole history in one request.
        auto modifiedRawHistory = client.ReadNodeHistoryModified(
            DateTime::UtcNow.Date.AddHours(6),
            DateTime::UtcNow.Date.AddHours(6).AddSeconds(300),
            L"ns=2;s=Machine_1/Position");

        Console::WriteLine(L"Read the historical 'ModifiedRaw' data...");

        for each (auto item in modifiedRawHistory) {
            Console::WriteLine(L"{0} - {1}", item->Timestamp, item->Value);
            Console::WriteLine(L"\t[{0}] by {1}", item->ModificationType, item->ModificationUserName);
        }
    }

    {
        // Read the historical 'ModifiedRaw' data page wise.
        // - for one specific node.
        // - the whole history partitioned into multiple requests.
        auto modifiedRawHistoryNavigator = client.ReadNodeHistoryModified(
            DateTime::UtcNow.Date.AddHours(6),
            DateTime::UtcNow.Date.AddHours(6).AddSeconds(300),
            2,
            L"ns=2;s=Machine_1/Position");

        Console::WriteLine(L"Read the historical 'ModifiedRaw' data page wise...");

        do {
            for each (auto item in modifiedRawHistoryNavigator)
            {
                Console::WriteLine(L"{0} - {1}", item->Timestamp, item->Value);
                Console::WriteLine(L"\t[{0}] by {1}", item->ModificationType, item->ModificationUserName);
            }

            Console::Write(L"Press any key to read the next page...");
            Console::ReadKey();
            Console::WriteLine();
        } while (modifiedRawHistoryNavigator->MoveNextPage());
    }

    {
        // Read the historical data 'at time'.
        // - for one specific node.
        // - the whole history for each time specified.
        auto atTimeHistory = client.ReadNodeHistoryAtTime(
            gcnew array<DateTime>
            {
                DateTime::UtcNow.Date.AddHours(6),
                DateTime::UtcNow.Date.AddHours(6).AddSeconds(10),
                DateTime::UtcNow.Date.AddHours(6).AddSeconds(20),
                DateTime::UtcNow.Date.AddHours(6).AddSeconds(30),
                DateTime::UtcNow.Date.AddHours(6).AddSeconds(40),
                DateTime::UtcNow.Date.AddHours(6).AddSeconds(50),
            },
            L"ns=2;s=Machine_1/Position");

        Console::WriteLine(L"Read the historical data 'at time'...");

        for each (auto item in atTimeHistory)
            Console::WriteLine(L"{0} - {1}", item->Timestamp, item->Value);
    }

    {
        // Read the historical data 'processed'.
        // - for one specific node.
        // - the whole history is processed on the server side.
        auto processedHistory = client.ReadNodeHistoryProcessed(
            DateTime::UtcNow.Date.AddHours(6),
            DateTime::UtcNow.Date.AddHours(6).AddMinutes(30),
            OpcAggregateType::Maximum,
            L"ns=2;s=Machine_1/Position");

        Console::WriteLine(L"Read the historical data 'processed'...");

        for each (auto item in processedHistory)
            Console::WriteLine(L"{0} - {1}", item->Timestamp, item->Value);
    }

    client.Disconnect();
    Console::ReadKey(true);
}

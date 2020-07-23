// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.
#pragma once

namespace AE
{
    #include "ObservedNode.hpp"
    #include "ObservedNodeCollection.hpp"

    using namespace AE;

    using namespace System;
    using namespace System::Threading;

    using namespace Opc::UaFx;
    using namespace Opc::UaFx::Client;

    ref class Observer
    {
        /// <summary>
        /// Stores the semaphore used to determine a ctrl+c input to exit the application.
        /// </summary>
        static SemaphoreSlim^ cancelSemaphore = gcnew SemaphoreSlim(0);

        /// <summary>
        /// Stores a sequence of the simple storage objects used to simplify the sample code.
        /// </summary>
        static ObservedNodeCollection^ nodes = gcnew ObservedNodeCollection();

    public:
        static void Observe(OpcClient% client)
        {
            auto machineId = OpcNodeId::Parse(L"ns=2;s=Machine_1");

            auto positionNode = nodes->Add(client, machineId, L"Position");
            auto temperatureNode = nodes->Add(client, machineId, L"Temperature");
            auto statusNode = nodes->Add(client, machineId, L"Status");

            auto severity = gcnew OpcSimpleAttributeOperand(OpcEventTypes::Event, L"Severity");
            auto sourceName = gcnew OpcSimpleAttributeOperand(OpcEventTypes::Event, L"SourceName");

            auto filter = OpcFilter::Using(% client)
                // Construct the filter to use for event subscriptions...
                ->FromEvents(
                // ... define the types of events to include in the filter
                // this will automatically add all properties defined by
                // the types of events specified ...
                OpcEventTypes::AlarmCondition,
                OpcEventTypes::ExclusiveLimitAlarm,
                OpcEventTypes::DialogCondition)
                ->Where(
                // ... restrict the event information received from the server
                // by specifying the types of events and ...
                OpcFilterOperand::OfType(OpcEventTypes::AlarmCondition)
                | OpcFilterOperand::OfType(OpcEventTypes::ExclusiveLimitAlarm)
                | OpcFilterOperand::OfType(OpcEventTypes::DialogCondition))
                // ... maybe additional conditions to fulfill, like the required severity
                // using the severity operand defined above like follows:
                //// severity > OpcEventSeverity.Medium
                // ... these conditions can then enhanced logical operators like follows:
                //// & sourceName.Like("Limit")
                ->Select(); // ... finally using Select() will query all the necessary event
                           // information required and will create the event filter setup with the
                           // givent event types, constraints and additional selection fields.

            // All in one Subscription
            {
                // Subscribe to data changes (value changes committed using ApplyChanges(...)
                // calls) on the status, position and temperature node. Also subscribe to all
                // events reported through notifiers assigned to the machine node. Each of
                // these subscribe tasks represent a single monitored item instance which is used
                // to define the reporting characteristics to use. The so defined monitored items
                // are then maintained by one single subscription.
                auto subscription = client.SubscribeNodes(
                    gcnew OpcSubscribeDataChange(statusNode->Id, gcnew OpcDataChangeReceivedEventHandler(HandleDataChanges)),
                    gcnew OpcSubscribeDataChange(positionNode->Id, gcnew OpcDataChangeReceivedEventHandler(HandleDataChanges)),
                    gcnew OpcSubscribeDataChange(temperatureNode->Id, gcnew OpcDataChangeReceivedEventHandler(HandleDataChanges)),
                    gcnew OpcSubscribeEvent(machineId, filter, gcnew OpcEventReceivedEventHandler(HandleDataEvents)));

                // In case there the client is interested in the current event information (which
                // is not explicitly re-published through the server when a subscription is
                // created) the client have to query a "ConditionRefresh" to query the latest
                // event information known by the server. In general the server does not need to
                // hold something like a history of events.
                subscription->RefreshConditions();
            }

            // Everyone in its own Subscription
            ////{
            ////    auto eventsSubscription = client.SubscribeEvent(machineId, filter, HandleDataEvents);
            ////    eventsSubscription->RefreshConditions();

            ////    // In case there only following events need to be known, the "ConditionRefresh" can
            ////    // be omitted and the subscription variable can be removed:
            ////    //// client.SubscribeEvent(machineId, filter, gcnew OpcEventReceivedEventHandler(HandleDataEvents));

            ////    // The following created subscription do not need to trigger a "ConditionRefresh",
            ////    // because in general only event information may be important and the current
            ////    // value can be just read, too.
            ////    client.SubscribeDataChange(statusNode->Id, gcnew OpcDataChangeReceivedEventHandler(HandleDataChanges));
            ////    client.SubscribeDataChange(positionNode->Id, gcnew OpcDataChangeReceivedEventHandler(HandleDataChanges));
            ////    client.SubscribeDataChange(positionNode->Id, gcnew OpcDataChangeReceivedEventHandler(HandleDataChanges));
            ////    client.SubscribeDataChange(temperatureNode->Id, gcnew OpcDataChangeReceivedEventHandler(HandleDataChanges));
            ////}

            // Handle global (server-wide) events
            {
                auto conditionName = gcnew OpcSimpleAttributeOperand(OpcEventTypes::Condition, L"ConditionName");

                auto globalFilter = OpcFilter::Using(% client)
                    ->FromEvents(OpcEventTypes::AlarmCondition)
                    ->Where(severity > OpcEventSeverity::Medium & conditionName->Like(L"Temperature"))
                    ->Select();

                client.SubscribeEvent(
                    static_cast<OpcNodeId^>(OpcObjectTypes::Server),
                    globalFilter,
                    gcnew OpcEventReceivedEventHandler(HandleGlobalEvents));
            }

            Console::CancelKeyPress += gcnew ConsoleCancelEventHandler(HandleCancelKeyPress);

            do {
                UpdateConsole(% client);
            } while (!cancelSemaphore->Wait(1000));
        }

    private:
        static void HandleCancelKeyPress(Object^ sender, ConsoleCancelEventArgs^ e)
        {
            cancelSemaphore->Release();
        }

        /// <summary>
        /// Handles the <see cref="OpcMonitoredItem.DataChangeReceived"/> event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        static void HandleDataChanges(Object^ sender, OpcDataChangeReceivedEventArgs^ e)
        {
            // In general change handling can be implemented here, too. The approach of this sample
            // takes use of the nodes collection to summarize the received information to process
            // it all in one run to simplify the handling of the console out- and input.
            auto nodeId = static_cast<OpcMonitoredItem^>(sender)->NodeId->ToString(OpcNodeIdFormat::Foundation);

            if (nodes->Contains(nodeId))
                nodes[nodeId]->Value = e->Item->Value;
        }

        /// <summary>
        /// Handles the <see cref="OpcMonitoredItem.EventReceived"/> event using node events.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        static void HandleDataEvents(Object^ sender, OpcEventReceivedEventArgs^ e)
        {
            // In general event handling can be implemented here, too. The approach of this sample
            // takes use of the nodes collection to summarize the received information to process
            // it all in one run to simplify the handling of the console out- and input.
            auto nodeId = e->Event->SourceNodeId->ToString(OpcNodeIdFormat::Foundation);

            if (nodeId != nullptr && nodes->Contains(nodeId))
                nodes[nodeId]->EventData = e->Event;
        }

        /// <summary>
        /// Handles the <see cref="OpcMonitoredItem.EventReceived"/> event using global events.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        static void HandleGlobalEvents(Object^ sender, OpcEventReceivedEventArgs^ e)
        {
            Console::Title = e->Event->Message;
        }

        /// <summary>
        /// Determines the according <see cref="ConsoleColor"/> to use for the
        /// <paramref name="severity"/> specified.
        /// </summary>
        /// <param name="severity">The numeric expression of the severity for that the according
        /// color is to be determined.</param>
        /// <returns>The <see cref="ConsoleColor"/> to use to represent the output of the
        /// <paramref name="severity"/> specified.</returns>
        static ConsoleColor SeverityToColor(OpcEventSeverity severity)
        {
            if (severity <= OpcEventSeverity::Min)
                return ConsoleColor::White;

            if (severity <= OpcEventSeverity::Low)
                return ConsoleColor::Green;

            if (severity <= OpcEventSeverity::Medium)
                return ConsoleColor::Yellow;

            if (severity <= OpcEventSeverity::MediumHigh)
                return ConsoleColor::DarkYellow;

            if (severity <= OpcEventSeverity::High)
                return ConsoleColor::Red;

            return ConsoleColor::Magenta;
        }

        /// <summary>
        /// Updates the whole console output by first clearing it all out and determining the
        /// necessary information to write and read.
        /// </summary>
        /// <param name="client">The <see cref="OpcClient"/> to use in cases there are user inputs
        /// required for which the client is used to communicate with the server.</param>
        static void UpdateConsole(OpcClient^ client)
        {
            Console::Clear();
            Console::SetCursorPosition(0, 0);
            Console::WriteLine(L"Monitoring Alarm & Events...");

            for each (auto node in nodes) {
                Console::Write(node->Label);
                WriteValue(node);

                Console::Write(L"\t");
                WriteEvent(node);

                Console::WriteLine();
            }

            for each (auto node in nodes) {
                auto ackCondition = dynamic_cast<OpcAcknowledgeableCondition^>(node->EventData);

                if (ackCondition != nullptr)
                    QueryAcknowledgment(client, ackCondition);

                auto dialogCondition = dynamic_cast<OpcDialogCondition^>(node->EventData);

                if (dialogCondition != nullptr)
                    QueryDialog(client, dialogCondition);
            }
        }

        /// <summary>
        /// Queries an acknowledgment by the user of the <paramref name="client"/>.
        /// </summary>
        /// <param name="client">The <see cref="OpcClient"/> used to acknowledge the
        /// <paramref name="condition"/> using the user input as the comment.</param>
        /// <param name="condition">The <see cref="OpcAcknowledgeableCondition"/> identifying the
        /// event information for which the acknowledgement is to be performed.</param>
        static void QueryAcknowledgment(OpcClient^ client, OpcAcknowledgeableCondition^ condition)
        {
            if (condition->IsAcked)
                return;

            Console::WriteLine();
            Console::WriteLine(L"Acknowledgment is required for condtion: {0}", condition->ConditionName);
            Console::WriteLine(L"  -> {0}", condition->Message);
            Console::Write(L"Enter your acknowlegment comment and press Enter to acknowledge: ");

            auto comment = Console::ReadLine();
            OpcAcknowledgeableConditionExtension::Acknowledge(condition, client, comment);
        }

        /// <summary>
        /// Queries a dialog to the user of the <paramref name="client"/>.
        /// </summary>
        /// <param name="client">The <see cref="OpcClient"/> used to forward the respond on
        /// the dialog using the user input as the selected response.</param>
        /// <param name="condition">The <see cref="OpcDialogCondition"/> identifying the event
        /// information required to handle the dialog.</param>
        static void QueryDialog(OpcClient^ client, OpcDialogCondition^ condition)
        {
            if (!condition->IsActive)
                return;

            Console::WriteLine();
            Console::WriteLine(condition->Prompt);

            Console::WriteLine(L"    Options:");

            auto responseOptions = condition->ResponseOptions;

            for (int index = 0; index < responseOptions->Length; index++) {
                Console::Write(L"      [{0}] = {1}", index, responseOptions[index]->Value);

                if (index == condition->DefaultResponse)
                    Console::Write(L" (default)");

                Console::WriteLine();
            }

            auto respond = String::Empty;
            auto respondOption = condition->DefaultResponse;

            do {
                Console::Write(L"Enter the number of the option and press Enter to respond: ");
                respond = Console::ReadLine();

                if (String::IsNullOrEmpty(respond))
                    break;
            } while (!Int32::TryParse(respond, respondOption));

            OpcDialogConditionExtension::Respond(condition, client, respondOption);
        }

        /// <summary>
        /// Writes the <see cref="ObservedNode.Event"/> information of the <paramref name="node"/>
        /// specified to the console.
        /// </summary>
        /// <param name="node">The <see cref="ObservedNode"/> its event information is to
        /// be written.</param>
        static void WriteEvent(ObservedNode^ node)
        {
            auto eventData = node->EventData;

            if (eventData != nullptr) {
                auto conditionData = dynamic_cast<OpcCondition^>(node->EventData);

                if (conditionData == nullptr || conditionData->IsRetained)
                    Console::ForegroundColor = SeverityToColor(eventData->Severity);
                else
                    Console::ForegroundColor = ConsoleColor::DarkGray;

                auto eventTime = eventData->Time.ToLocalTime();
                auto eventText = eventData->Message;

                Console::Write(L"{0} - {1}", eventTime.ToString("HH:mm:ss.fff"), eventText);
                Console::ResetColor();
            }
        }

        /// <summary>
        /// Writes the <see cref="ObservedNode.Value"/> and <see cref="ObservedNode.Unit"/>
        /// information of the <paramref name="node"/> specified to the console.
        /// </summary>
        /// <param name="node">The <see cref="ObservedNode"/> its value and unit information is to
        /// written.</param>
        static void WriteValue(ObservedNode^ node)
        {
            if (node->Value != nullptr) {
                Console::ForegroundColor = ConsoleColor::White;
                Console::Write(node->Value);

                if (node->Unit == nullptr)
                    Console::Write(L"\t");
                else
                    Console::Write(L" {0}\t", node->Unit->DisplayName);

                Console::ResetColor();
            }
        }
    };
}

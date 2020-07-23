// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.
#pragma once

namespace AE
{
    using namespace System;

    using namespace Opc::UaFx;
    using namespace Opc::UaFx::Client;

    ref class ObservedNode
    {
    private:
        OpcNodeId^ id;
        OpcEvent^ eventData;
        String^ label;
        OpcEngineeringUnitInfo^ unit;
        OpcValue^ value;

    public:
        ObservedNode(OpcNodeId^ parentNodeId, String^ name)
        {
            this->id = OpcNodeId::Of(name, parentNodeId);
            this->label = (name + ":")->PadRight(16);
        }

    public:
        property OpcNodeId^ Id
        {
            OpcNodeId^ get()
            {
                return this->id;
            }
        }

        property OpcEvent^ EventData
        {
            OpcEvent^ get()
            {
                return this->eventData;
            }

            void set(OpcEvent^ value)
            {
                this->eventData = value;
            }
        }

        property String^ Label
        {
            String^ get()
            {
                return this->label;
            }
        }

        property OpcEngineeringUnitInfo^ Unit
        {
            OpcEngineeringUnitInfo^ get()
            {
                return this->unit;
            }

            void set(OpcEngineeringUnitInfo^ value)
            {
                this->unit = value;
            }
        }

        property OpcValue^ Value
        {
            OpcValue^ get()
            {
                return this->value;
            }

            void set(OpcValue^ value)
            {
                this->value = value;
            }
        }

    public:
        void Initialize(OpcClient% client)
        {
            auto node = client.BrowseNode(this->id);
            auto analogNode = dynamic_cast<OpcAnalogItemNodeInfo^>(node);

            if (analogNode != nullptr)
                this->unit = analogNode->EngineeringUnit;

            this->value = client.ReadNode(this->id);
        }
    };
}

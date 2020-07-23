// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.
#pragma once

namespace AE
{
    #include "ObservedNode.hpp"
    using namespace AE;

    using namespace System;
    using namespace System::Collections::ObjectModel;

    using namespace Opc::UaFx;
    using namespace Opc::UaFx::Client;

    ref class ObservedNodeCollection : public KeyedCollection<String^, ObservedNode^>
    {
    public:
        ObservedNodeCollection()
            : KeyedCollection()
        {
        }

    public:
        ObservedNode^ Add(OpcClient %client, OpcNodeId^ parentNodeId, String^ name)
        {
            auto item = gcnew ObservedNode(parentNodeId, name);
            item->Initialize(client);

            this->Add(item);
            return item;
        }

    protected:
        virtual String^ GetKeyForItem(ObservedNode^ item) override/*= KeyedCollection<String^, ObservedNode^>::GetKeyForItem*/
        {
            return item->Id->ToString(OpcNodeIdFormat::Foundation);
        }
    };
}

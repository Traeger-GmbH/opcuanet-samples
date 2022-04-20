// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

using System;

using UnityEngine;
using UnityEngine.UI;

using Opc.UaFx;
using Opc.UaFx.Client;

public class OpcUaClientBehaviour : MonoBehaviour
{
    private OpcClient client;
    private Text statusText;

    /// <summary>
    /// This sample demonstrates how to implement a primitive OPC UA client in Unity.
    /// </summary>
    /// <remarks>Start is called before the first frame update.</remarks>
    void Start()
    {
        this.statusText = GameObject.Find("statusText").GetComponent<Text>();
        this.statusText.text = "Connecting...";

        try {
            this.client = new OpcClient("opc.tcp://localhost:48400/");

            this.client.Connect();
            this.statusText.text = "Connected!";

            this.client.SubscribeDataChange("ns=2;s=Nodes/Dynamic/String", HandleDataChanged);
            this.statusText.text = "Subscribed!";
        }
        catch (Exception ex) {
            if (ex is TypeInitializationException tiex)
                ex = tiex.InnerException;

            this.statusText.text += Environment.NewLine
                    + ex.GetType().Name
                    + ": " + ex.Message
                    + Environment.NewLine
                    + ex.StackTrace;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void HandleDataChanged(object sender, OpcDataChangeReceivedEventArgs e)
    {
        this.statusText.text = e.Item.Value.Value?.ToString() ?? "null";
    }
}

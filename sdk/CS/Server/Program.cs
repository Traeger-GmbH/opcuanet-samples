// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Server
{
    using Opc.UaFx.Server;

    /// <summary>
    /// This sample demonstrates how to implement a primitive OPC UA server.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            #region 1st Way: Use the OpcServer class.
            {
                //// The OpcServer class interacts with one or more OPC UA clients using one of
                //// the registered base addresses of the server. While this class provides the
                //// different OPC UA services defined by OPC UA, it does not implement a main loop.
                //var server = new OpcServer("opc.tcp://localhost:4840/SampleServer", new SampleNodeManager());

                //server.Start();
                //server.Stop();
            }
            #endregion

            #region 2nd Way: Use the OpcServerApplication class.
            {
                // The OpcServerApplication class uses a single OpcServer instance which is
                // wrapped within a main loop.
                //
                // Remarks
                // - The app instance does start a main loop when the server has been started.
                // - Custom startup code have to be implemented within the event handler of the
                //   Started event of the app instance.
                new OpcServerApplication("opc.tcp://localhost:4840/SampleServer", new SampleNodeManager()).Run();
            }
            #endregion

            #region 3rd Way: Use the OpcServerServiceApplication class.
            {
                //// The OpcServerServiceApplication class uses a single OpcServer instance which is
                //// wrapped within a main loop when it is started with an interactive user or in
                //// debug mode. Otherwise it will start the process as a windows service which
                //// allows the application can be registered as a service process.
                ////
                //// Remarks
                //// - The app instance does start a main loop when the server has been started.
                //// - Custom startup code have to be implemented within the event handler of the
                ////   Started event of the app instance.
                //new OpcServerServiceApplication("opc.tcp://localhost:4840/SampleServer", new SampleNodeManager()).Run();
            }
            #endregion
        }

        #endregion
    }
}

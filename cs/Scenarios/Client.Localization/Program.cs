// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Localization
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    public class Program
    {
        public static void Main()
        {
            //// #Starting Notes
            //// * The Server always tries to offer localized information using the locales preferred by the client.
            //// * The Server always falls back to its default localization in case a requested local is not supported.
            //// * The locale strings used are the ISO normalized culture names used by the CultureInfo.
            //// * The use of multiple locales results into a prioritization by its order and the Server provides the
            ////   localization for the first locale preferred and supported.

            // 1. Case: Use the current thread culture (see CultureInfo.CurrentCulture).
            using (var client = new OpcClient("opc.tcp://localhost:4840")) {
                client.Connect();

                var displayName = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.DisplayName);
                var description = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.Description);

                Console.WriteLine("{0} - {1}", displayName, description);
            }

            // 2. Case: Use an explict culture e.g. 'fr' for France.
            using (var client = new OpcClient("opc.tcp://localhost:4840")) {
                client.Locales.Add("fr");
                client.Connect();

                var displayName = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.DisplayName);
                var description = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.Description);

                Console.WriteLine("{0} - {1}", displayName, description);
            }

            // 3. Case: Use an explict culture e.g. 'de' for German.
            using (var client = new OpcClient("opc.tcp://localhost:4840")) {
                client.Locales.Add("de");
                client.Connect();

                var displayName = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.DisplayName);
                var description = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.Description);

                Console.WriteLine("{0} - {1}", displayName, description);
            }

            // 4. Case: Use multiple cultures. If a localized information can not offered for the
            //          first the second culture is used instead.
            using (var client = new OpcClient("opc.tcp://localhost:4840")) {
                client.Locales.Add("it"); // 1st we prefer "Italian".
                client.Locales.Add("en"); // 2nd if "Italian" is not supported: Give us "English".
                client.Connect();

                var displayName = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.DisplayName);
                var description = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.Description);

                Console.WriteLine("{0} - {1}", displayName, description);
            }

            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}

' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace Localization
    ''' <summary>
    ''' This use case demonstrates how a client requests localized display names and descriptions by
    ''' specifying its preferred locales.
    ''' </summary>
    Public Class Program
        Public Shared Sub Main()
            '''' #Starting Notes
            '''' * The Server always tries to offer localized information using the locales preferred by the client.
            '''' * The Server always falls back to its default localization in case a requested local is not supported.
            '''' * The locale strings used are the ISO normalized culture names used by the CultureInfo.
            '''' * The use of multiple locales results into a prioritization by its order and the Server provides the
            ''''   localization for the first locale preferred and supported.

            ' 1. Case: Use the current thread culture (see CultureInfo.CurrentCulture).
            Using client As New OpcClient("opc.tcp://localhost:4840")
                client.Connect()

                Dim displayName = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.DisplayName)
                Dim description = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.Description)

                Console.WriteLine("{0} - {1}", displayName, description)
            End Using

            ' 2. Case: Use an explict culture e.g. 'fr' for France.
            Using client As New OpcClient("opc.tcp://localhost:4840")
                client.Locales.Add("fr")
                client.Connect()

                Dim displayName = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.DisplayName)
                Dim description = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.Description)

                Console.WriteLine("{0} - {1}", displayName, description)
            End Using

            ' 3. Case: Use an explict culture e.g. 'de' for German.
            Using client As New OpcClient("opc.tcp://localhost:4840")
                client.Locales.Add("de")
                client.Connect()

                Dim displayName = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.DisplayName)
                Dim description = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.Description)

                Console.WriteLine("{0} - {1}", displayName, description)
            End Using

            ' 4. Case: Use multiple cultures. If a localized information can not offered for the
            '          first the second culture is used instead.
            Using client As New OpcClient("opc.tcp://localhost:4840")
                client.Locales.Add("it") ' 1st we prefer "Italian".
                client.Locales.Add("en") ' 2nd if "Italian" is not supported: Give us "English".
                client.Connect()

                Dim displayName = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.DisplayName)
                Dim description = client.ReadNode("ns=2;s=MyFolder", OpcAttribute.Description)

                Console.WriteLine("{0} - {1}", displayName, description)
            End Using

            Console.Write("Press any key to continue...")
            Console.ReadKey(True)
        End Sub
    End Class
End Namespace

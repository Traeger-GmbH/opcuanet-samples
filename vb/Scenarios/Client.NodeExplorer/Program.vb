' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Windows.Forms

''' <summary>
''' This sample demonstrates how to browse the address space of an OPC UA server and show it as a
''' tree in a Windows Forms application.
''' </summary>
Public Module Program
    ''' <summary>
    '''  The main entry point for the application.
    ''' </summary>
    <STAThread>
    Public Sub Main()
#If NET Then
        Application.SetHighDpiMode(HighDpiMode.SystemAware)
#End If
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New Form1())
    End Sub
End Module

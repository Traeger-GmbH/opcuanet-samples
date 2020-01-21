Imports System
Imports System.Windows.Input

Namespace Client.Wpf
    Public Class DelegateCommand
        Implements ICommand

        '---------- Private readonly fields ----------

        Private ReadOnly canExecute As Func(Of Object, Boolean)
        Private ReadOnly execute As Action(Of Object)

        '---------- Public constructors ----------

        Public Sub New(ByVal execute As Action(Of Object))
            MyBase.New()
            Me.execute = execute
        End Sub

        Public Sub New(
                ByVal execute As Action(Of Object),
                ByVal canExecute As Func(Of Object, Boolean))
            Me.New(execute)
            Me.canExecute = canExecute
        End Sub

        '---------- Public events ----------

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        '---------- Public methods ----------

        Public Sub RaiseCanExecuteChanged()
            RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
        End Sub

        Private Sub ICommand_Execute(parameter As Object) Implements ICommand.Execute
            Me.execute(parameter)
        End Sub

        Private Function ICommand_CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            ICommand_CanExecute = Me.canExecute(parameter)
        End Function
    End Class
End Namespace

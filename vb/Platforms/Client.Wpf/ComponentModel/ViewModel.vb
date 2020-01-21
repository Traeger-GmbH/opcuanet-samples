Imports System.ComponentModel

Namespace Client.Wpf
    Public MustInherit Class ViewModel
        Implements INotifyPropertyChanged, INotifyPropertyChanging

        '---------- Protected constructors ----------

        Protected Sub New()
            MyBase.New()
        End Sub

        '---------- Public events ----------

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Event PropertyChanging As PropertyChangingEventHandler Implements INotifyPropertyChanging.PropertyChanging

        '---------- Protected methods ----------

        Protected Overridable Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
            RaiseEvent PropertyChanged(Me, e)
        End Sub

        Protected Overridable Sub OnPropertyChanging(ByVal e As PropertyChangingEventArgs)
            RaiseEvent PropertyChanging(Me, e)
        End Sub

        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Me.OnPropertyChanged(New PropertyChangedEventArgs(propertyName))
        End Sub

        Protected Sub RaisePropertyChanging(ByVal propertyName As String)
            Me.OnPropertyChanging(New PropertyChangingEventArgs(propertyName))
        End Sub
    End Class
End Namespace

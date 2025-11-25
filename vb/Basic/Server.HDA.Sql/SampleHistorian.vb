' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Linq

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace HDA.Sql
    ''' <summary>
    ''' Represents a sample implementation of a custom IOpcNodeHistoryProvider.
    ''' </summary>
    Friend Class SampleHistorian
        Implements IOpcNodeHistoryProvider

        '---------- Private readonly fields ----------

        Private ReadOnly syncRoot As Object

        '---------- Private fields ----------

        Private _autoUpdateHistory As Boolean
        Private _node As OpcVariableNode

        '---------- Public constructors ----------

        Public Sub New(ByVal owner As OpcNodeManager, ByVal node As OpcVariableNode)
            MyBase.New()
            Me.Owner = owner
            Me._node = node

            Me._node.AccessLevel = Me._node.AccessLevel Or OpcAccessLevel.HistoryReadOrWrite
            Me._node.UserAccessLevel = Me._node.UserAccessLevel Or OpcAccessLevel.HistoryReadOrWrite
            Me._node.IsHistorizing = True

            Me.syncRoot = New Object()

            Me.History = SampleHistory(Of OpcHistoryValue) _
                    .Create(".\History.db", node.Id)
            Me.ModifiedHistory = SampleHistory(Of OpcModifiedHistoryValue) _
                    .Create(".\History.Modified.db", node.Id)
        End Sub

        '---------- Public properties ----------

        Public Property AutoUpdateHistory As Boolean
            Get
                SyncLock Me.syncRoot
                    Return Me._autoUpdateHistory
                End SyncLock
            End Get

            Set(ByVal value As Boolean)
                SyncLock Me.syncRoot
                    If Me._autoUpdateHistory <> value Then
                        Me._autoUpdateHistory = value

                        If Me._autoUpdateHistory Then
                            AddHandler Me._node.BeforeApplyChanges, AddressOf Me.HandleNodeBeforeApplyChanges
                        Else
                            RemoveHandler Me._node.BeforeApplyChanges, AddressOf Me.HandleNodeBeforeApplyChanges
                        End If
                    End If
                End SyncLock
            End Set
        End Property

        Public ReadOnly Property History As SampleHistory(Of OpcHistoryValue)
        Public ReadOnly Property ModifiedHistory As SampleHistory(Of OpcModifiedHistoryValue)

        Public ReadOnly Property Node As OpcVariableNode
            Get
                Return Me._node
            End Get
        End Property

        Public ReadOnly Property Owner As OpcNodeManager

        '---------- Private properties (by IOpcNodeHistoryProvider) ----------

        Private ReadOnly Property IOpcNodeHistoryProvider_Node As OpcVariableNode Implements IOpcNodeHistoryProvider.Node
            Get
                Return Me._node
            End Get
        End Property

        '---------- Private methods (by IOpcNodeHistoryProvider) ----------

        Private Function IOpcNodeHistoryProvider_CreateHistory(
                context As OpcContext,
                modificationInfo As OpcHistoryModificationInfo,
                values As OpcValueCollection) As OpcStatusCollection Implements IOpcNodeHistoryProvider.CreateHistory
            Dim results = OpcStatusCollection.Create(OpcStatusCode.Good, values.Count)

            SyncLock Me.syncRoot
                Dim expectedDataType = Me._node.DataTypeId

                For index As Integer = 0 To values.Count - 1
                    Dim result = results(index)
                    Dim value = OpcHistoryValue.Create(values(index))

                    If value.DataTypeId = expectedDataType Then
                        If Me.History.Contains(value.Timestamp) Then
                            result.Update(OpcStatusCode.BadEntryExists)
                        Else
                            Me.History.Add(value)

                            Dim modifiedValue = value.CreateModified(modificationInfo)
                            Me.ModifiedHistory.Add(modifiedValue)

                            result.Update(OpcStatusCode.GoodEntryInserted)
                        End If
                    Else
                        result.Update(OpcStatusCode.BadTypeMismatch)
                    End If
                Next
            End SyncLock

            Return results
        End Function

        Private Function IOpcNodeHistoryProvider_DeleteHistory(
                context As OpcContext,
                modificationInfo As OpcHistoryModificationInfo,
                times As IEnumerable(Of DateTime)) As OpcStatusCollection Implements IOpcNodeHistoryProvider.DeleteHistory
            Dim results = OpcStatusCollection.Create(OpcStatusCode.Good, times.Count())

            SyncLock Me.syncRoot
                Dim index As Integer = 0

                For Each time In times
                    index += 1
                    Dim result = results(index)

                    If Me.History.Contains(time) Then
                        Dim value = Me.History(time)
                        Me.History.RemoveAt(time)

                        Dim modifiedValue = value.CreateModified(modificationInfo)
                        Me.ModifiedHistory.Add(modifiedValue)
                    Else
                        result.Update(OpcStatusCode.BadNoEntryExists)
                    End If
                Next
            End SyncLock

            Return results
        End Function

        Private Function IOpcNodeHistoryProvider_DeleteHistory(
                context As OpcContext,
                modificationInfo As OpcHistoryModificationInfo,
                values As OpcValueCollection) As OpcStatusCollection Implements IOpcNodeHistoryProvider.DeleteHistory
            Dim results = OpcStatusCollection.Create(OpcStatusCode.Good, values.Count)

            SyncLock Me.syncRoot
                For index As Integer = 0 To values.Count - 1
                    Dim timestamp = OpcHistoryValue.Create(values(index)).Timestamp
                    Dim result = results(index)

                    If Me.History.Contains(timestamp) Then
                        Dim value = Me.History(timestamp)
                        Me.History.RemoveAt(timestamp)

                        Dim modifiedValue = value.CreateModified(modificationInfo)
                        Me.ModifiedHistory.Add(modifiedValue)
                    Else
                        result.Update(OpcStatusCode.BadNoEntryExists)
                    End If
                Next
            End SyncLock

            Return results
        End Function

        Private Function IOpcNodeHistoryProvider_DeleteHistory(
                context As OpcContext,
                modificationInfo As OpcHistoryModificationInfo,
                startTime As DateTime?,
                endTime As DateTime?,
                options As OpcDeleteHistoryOptions) As OpcStatusCollection Implements IOpcNodeHistoryProvider.DeleteHistory
            Dim results = New OpcStatusCollection()

            SyncLock Me.syncRoot
                If options.HasFlag(OpcDeleteHistoryOptions.Modified) Then
                    Me.ModifiedHistory.RemoveRange(startTime, endTime)
                Else
                    Dim values = Me.History.Enumerate(startTime, endTime).ToArray()
                    Me.History.RemoveRange(startTime, endTime)

                    For index As Integer = 0 To values.Length - 1
                        Dim value = values(index)
                        Me.ModifiedHistory.Add(value.CreateModified(modificationInfo))

                        results.Add(OpcStatusCode.Good)
                    Next
                End If
            End SyncLock

            Return results
        End Function

        Private Function IOpcNodeHistoryProvider_ReadHistory(
                context As OpcContext,
                startTime As DateTime?,
                endTime As DateTime?,
                timeFlowsBackward As Boolean,
                maxCount As UInteger?,
                options As OpcReadHistoryOptions) As IEnumerable(Of OpcHistoryValue) Implements IOpcNodeHistoryProvider.ReadHistory
            Dim historyValues As OpcHistoryValue()

            SyncLock Me.syncRoot
                If options.HasFlag(OpcReadHistoryOptions.Modified) Then
                    historyValues = Me.ModifiedHistory _
                            .Enumerate(startTime, endTime) _
                            .Cast(Of OpcHistoryValue)() _
                            .ToArray()
                End If

                historyValues = Me.History _
                        .Enumerate(startTime, endTime) _
                        .ToArray()
            End SyncLock

            ' If timeFlowsBackward Is true, we need to reverse the array in order to
            ' return the values in descending order.
            If timeFlowsBackward Then
                Array.Reverse(historyValues)
            End If

            Return historyValues
        End Function

        Private Function IOpcNodeHistoryProvider_ReplaceHistory(
                context As OpcContext,
                modificationInfo As OpcHistoryModificationInfo,
                values As OpcValueCollection) As OpcStatusCollection Implements IOpcNodeHistoryProvider.ReplaceHistory
            Dim results = OpcStatusCollection.Create(OpcStatusCode.Good, values.Count)

            SyncLock Me.syncRoot
                Dim expectedDataTypeId = Me._node.DataTypeId

                For index As Integer = 0 To values.Count - 1
                    Dim result = results(index)
                    Dim value = OpcHistoryValue.Create(values(index))

                    If value.DataTypeId = expectedDataTypeId Then

                        If Me.History.Contains(value.Timestamp) Then
                            Dim oldValue = Me.History(value.Timestamp)
                            Me.History.Replace(value)

                            Dim modifiedValue = oldValue.CreateModified(modificationInfo)
                            Me.ModifiedHistory.Add(modifiedValue)

                            result.Update(OpcStatusCode.GoodEntryReplaced)
                        Else
                            result.Update(OpcStatusCode.BadNoEntryExists)
                        End If
                    Else
                        result.Update(OpcStatusCode.BadTypeMismatch)
                    End If
                Next
            End SyncLock

            Return results
        End Function

        Private Function IOpcNodeHistoryProvider_UpdateHistory(
                context As OpcContext,
                modificationInfo As OpcHistoryModificationInfo,
                values As OpcValueCollection) As OpcStatusCollection Implements IOpcNodeHistoryProvider.UpdateHistory
            Dim results = OpcStatusCollection.Create(OpcStatusCode.Good, values.Count)

            SyncLock Me.syncRoot
                Dim expectedDataTypeId = Me._node.DataTypeId

                For index As Integer = 0 To values.Count - 1
                    Dim result = results(index)
                    Dim value = OpcHistoryValue.Create(values(index))

                    If value.DataTypeId = expectedDataTypeId Then
                        If Me.History.Contains(value.Timestamp) Then
                            Dim oldValue = Me.History(value.Timestamp)
                            Me.History.Replace(value)

                            Dim modifiedValue = oldValue.CreateModified(modificationInfo)
                            Me.ModifiedHistory.Add(modifiedValue)

                            result.Update(OpcStatusCode.GoodEntryReplaced)
                        Else
                            Me.History.Add(value)

                            Dim modifiedValue = value.CreateModified(modificationInfo)
                            Me.ModifiedHistory.Add(modifiedValue)

                            result.Update(OpcStatusCode.GoodEntryInserted)
                        End If
                    Else
                        result.Update(OpcStatusCode.BadTypeMismatch)
                    End If
                Next
            End SyncLock

            Return results
        End Function

        '---------- Private methods ----------

        Private Sub HandleNodeBeforeApplyChanges(ByVal sender As Object, ByVal e As OpcNodeChangesEventArgs)
            Dim timestamp = Me._node.Timestamp

            If timestamp IsNot Nothing AndAlso e.IsChangeOf(OpcNodeChanges.Value) Then
                Dim value = New OpcHistoryValue(Me._node.Value, timestamp.Value)

                If Me.History.Contains(value.Timestamp) Then
                    Me.History.Replace(value)
                Else
                    Me.History.Add(value)
                End If
            End If
        End Sub
    End Class
End Namespace

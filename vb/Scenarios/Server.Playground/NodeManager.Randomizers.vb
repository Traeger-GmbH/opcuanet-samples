' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Text

Namespace Server.Playground
    Partial Friend Class NodeManager
        '---------- Private methods ----------

        Private Function RandomEnum(Of T)() As T
            Return Me.RandomEnum(Of T)(old:=Nothing, random:=3)
        End Function

        Private Function RandomEnum(Of T)(old As T, random As Integer) As T
            Dim values = [Enum].GetValues(GetType(T))

            If GetType(T).IsDefined(GetType(FlagsAttribute), inherit:=False) Then
                Dim value = Math.Min(random, Math.Pow(2, values.Length - 1))
                Dim baseValue = Convert.ChangeType(value, [Enum].GetUnderlyingType(GetType(T)))

                Return DirectCast([Enum].ToObject(GetType(T), baseValue), T)
            End If

            Dim newValue = DateTime.Now.Second * random Mod values.Length

            If [Enum].IsDefined(GetType(T), newValue) Then
                Return DirectCast([Enum].ToObject(GetType(T), newValue), T)
            End If

            Return old
        End Function

        Public Function RandomString() As String
            Return Me.RandomString(old:=String.Empty, random:=3)
        End Function

        Public Function RandomString(old As String, random As Integer) As String
            Dim minWords As Integer = 1
            Dim maxWords As Integer = random
            Dim minSentences As Integer = 1
            Dim maxSentences As Integer = 1
            Dim paragraphCount As Integer = 1

            Dim words = {
                "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
            }

            Dim sentenceCount As Integer = Me.valueRandom.Next(Math.Max(maxSentences - minSentences, 1)) + minSentences + 1
            Dim wordCount As Integer = Me.valueRandom.Next(Math.Max(maxWords - minWords, 1)) + minWords + 1

            Dim result As New StringBuilder()

            For paragraphIndex = 0 To paragraphCount - 1
                For sentenceIndex = 0 To sentenceCount - 1
                    For wordIndex = 0 To wordCount - 1
                        If wordIndex > 0 Then
                            result.Append(" ")
                        End If

                        Dim word = words(Me.indexRandom.Next(words.Length))

                        If wordIndex = 0 Then
                            word = Char.ToUpper(word(0)) & word.Substring(1)
                        End If

                        result.Append(word)
                    Next

                    result.Append(".")

                    If sentenceIndex + 1 < sentenceCount Then
                        result.Append(" ")
                    End If
                Next

                If paragraphIndex + 1 < paragraphCount Then
                    result.Append(Environment.NewLine)
                End If
            Next

            Return result.ToString()
        End Function
    End Class
End Namespace

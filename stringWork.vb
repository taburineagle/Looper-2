Module stringWork
    Public Function betweenTheLines(theString As String, theStart As String, theEnd As String, defaultValue As String) As String
        Dim offsetLength = theStart.Length ' offset length to start after the found section

        Dim startPos As Integer = Strings.InStr(theString, theStart) ' the start of the string
        Dim endPos As Integer ' the end of the string

        If startPos <> 0 Then
            endPos = Strings.InStr(startPos, theString, theEnd) ' the end of the string (try to find the end character)

            If endPos = 0 And theEnd = vbCrLf Then ' if we're looking for a new line, and there isn't one, then don't crash...
                endPos = theString.Length + 1 ' if there's no next line, then just use the end of the string *as* the next line
            End If

            Dim returnString As String = Mid(theString, startPos + offsetLength, endPos - startPos - offsetLength) ' the returned string

            If defaultValue = "B_False" Or defaultValue = "B_True" Then
                If returnString = "0" Then
                    Return "False" ' if an INI value gives us "0", then we want to return "False" to correctly trigger the boolean value
                ElseIf returnString = "1" Then
                    Return "True" ' if an INI value gives us "1", then we want to return "True" to correctly trigger the boolean value
                Else
                    Return returnString ' if we get some strange result, just return that
                End If
            Else
                Return returnString ' we didn't explicitly ask for a boolean return, so just return the result
            End If
        Else ' if there's an error with this string return
            If defaultValue = "B_False" Then
                Return "False" ' return "False" to trigger boolean response
            ElseIf defaultValue = "B_True" Then
                Return "True" ' return "True" to trigger boolean response
            Else
                Return defaultValue ' just parrot (on the couch?  Biting the cushions?) back the inital default value
            End If
        End If
    End Function

    Public Function removeSection(theString As String, theStart As String, theEnd As String) As String
        Dim startPos As Integer = Strings.InStr(theString, theStart) ' the start of the string

        If startPos <> 0 Then
            Dim endPos As Integer = Strings.InStr(startPos, theString, theEnd) ' the end of the string
            Dim returnString As String = Mid(theString, endPos + 1) ' return everything after that point in the string (remove that parameter)
            Return returnString
        Else
            Return theString
        End If
    End Function

    Public Function removeParams(theString As String) As String
        Dim returnString = removeSection(theString, "<L:", ">") ' remove loop parameter from loop name
        returnString = removeSection(returnString, "<S:", ">") ' remove speed parameter from loop name
        returnString = removeSection(returnString, "<Z:", ">") ' remove custom zoom parameter from loop name

        Return returnString
    End Function
End Module

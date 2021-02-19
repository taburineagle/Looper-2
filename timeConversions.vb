Module TimeConversions
    ' ======================================================================================
    ' ================= TIME STRING CONVERSION TO AND FROM NUMBER ==========================
    ' ======================================================================================

    Public Function NumberToTimeString(ByVal number As Double) As String
        Dim hours, minutes As Integer
        Dim seconds As Double

        Dim timeString As String = Nothing

        hours = Convert.ToInt32(Int(number / 3600))
        minutes = Convert.ToInt32(Int((number - (hours * 3600)) / 60))
        seconds = Math.Round(((number - (hours * 3600) - (minutes * 60))), 3)

        If seconds = 60 Then
            seconds = 0
            minutes += 1
        End If

        If hours > 0 Then
            timeString = hours & ":"

            If minutes < 10 Then
                timeString &= "0"
            End If
        End If

        If minutes >= 0 Then
            timeString = timeString & minutes & ":"

            If seconds < 10 Then
                timeString &= "0"
            End If
        End If

        timeString &= seconds

        Return timeString
    End Function

    Public Function TimeStringToNumber(ByVal timeString As String) As Double
        Dim timeStringArray() As Double = Array.ConvertAll(timeString.Split(":"c), Function(s) Val(s))

        If timeStringArray.Length = 1 Then
            Return timeStringArray(0)
        ElseIf timeStringArray.Length = 2 Then
            Return (timeStringArray(0) * 60) + timeStringArray(1)
        ElseIf timeStringArray.Length = 3 Then
            Return (timeStringArray(0) * 3600) + (timeStringArray(1) * 60) + timeStringArray(2)
        Else
            Return 0
        End If
    End Function
End Module

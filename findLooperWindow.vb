Public Class findLooperWindow
    Private Sub findLooperWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()
        Me.Icon = My.Resources.icon ' Set the Form icon to the main program icon
        Me.Top -= 250 ' move the window up just a little bit

        MPCEXEPathButton.Select() ' makes the "..." button the focused item
        Me.TopMost = True
        Me.Show()
    End Sub

    Private Sub findLooperWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If saveChangesButton.Enabled = False Then
            End ' if we choose not to find MPC-HC's .exe file, then just quit the program completely
        End If
    End Sub

    Private Sub findLooperWindow_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub MPCEXEPathButton_Click(sender As Object, e As EventArgs) Handles MPCEXEPathButton.Click
        Dim x86ProgPath = Environment.GetEnvironmentVariable("ProgramFiles(x86)") ' the x86 (32-bit) Program Files base directory
        Dim x64ProgPath = Environment.GetEnvironmentVariable("ProgramW6432") ' the x64 (64-bit) Program Files base directory

        Dim defaultSearchPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) ' the path to the Desktop folder

        If My.Computer.FileSystem.DirectoryExists(x64ProgPath & "\MPC-HC") Then
            defaultSearchPath = x64ProgPath & "\MPC-HC"
        ElseIf My.Computer.FileSystem.DirectoryExists(x64ProgPath & "\MPC-BE") Then
            defaultSearchPath = x64ProgPath & "\MPC-BE"
        ElseIf My.Computer.FileSystem.DirectoryExists(x64ProgPath & "\MPC-HC x64") Then
            defaultSearchPath = x64ProgPath & "\MPC-HC x64"
        ElseIf My.Computer.FileSystem.DirectoryExists(x64ProgPath & "\MPC-BE x64") Then
            defaultSearchPath = x64ProgPath & "\MPC-BE x64"
        ElseIf My.Computer.FileSystem.DirectoryExists(x86ProgPath & "\MPC-HC") Then
            defaultSearchPath = x86ProgPath & "\MPC-HC"
        ElseIf My.Computer.FileSystem.DirectoryExists(x86ProgPath & "\MPC-BE") Then
            defaultSearchPath = x86ProgPath & "\MPC-BE"
        ElseIf My.Computer.FileSystem.DirectoryExists(x86ProgPath & "\MPC-HC x64") Then
            defaultSearchPath = x86ProgPath & "\MPC-HC x64"
        ElseIf My.Computer.FileSystem.DirectoryExists(x86ProgPath & "\MPC-BE x64") Then
            defaultSearchPath = x86ProgPath & "\MPC-BE x64"
        End If

        Dim findMPCEXE As New OpenFileDialog With {
                .Title = "Find MPC-HC/BE's .exe File",
                .Filter = "Executable files|mpc*.exe",
                .InitialDirectory = defaultSearchPath
        }

        If findMPCEXE.ShowDialog = DialogResult.OK Then
            MPCEXEPathTF.Text = findMPCEXE.FileName ' use this path as MPC-HC/BE's .exe file path
            saveChangesButton.Enabled = True ' we have a value, so turn the "Save" button ON
            saveChangesButton.Select() ' and select it as the focused control
        End If
    End Sub

    Private Sub saveChangesButton_Click(sender As Object, e As EventArgs) Handles saveChangesButton.Click
        Dim writingString As String = "[System]" & vbCrLf & "MPCEXE=" & MPCEXEPathTF.Text & vbCrLf
        System.IO.File.WriteAllText(INIFile, writingString) ' write the above line ^ into a new .ini file
        Me.Close() ' and finally close the "Find MPC-HC .exe" dialog
    End Sub
End Class
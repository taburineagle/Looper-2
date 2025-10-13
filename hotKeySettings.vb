Public Class hotKeySettings
    Private Sub HotKeySettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        hotKeysLV.Items.Clear() ' delete all items in the hotkey list view (if they exist from a previous session) to get things started

        ' set the window position to just right of the options window
        Me.Left = optionsWindow.Right
        Me.Top = optionsWindow.Top

        mainWindow.clearHotKeys() ' turn hotkeys off (this forces them to reload when the Options window closes)

        For currentItem = 0 To UBound(mainWindow.hotKeyList)
            hotKeysLV.Items.Add(New ListViewItem(mainWindow.hotKeyList(currentItem, 2))) ' add hotkey to the list

            If Not mainWindow.hotKeyList(currentItem, 1) = Nothing Then ' if this hotkey has a custom setting, then change the formatting for that hotkey
                hotKeysLV.Items(currentItem).Font = playlistWindow.boldFont
                hotKeysLV.Items(currentItem).BackColor = Color.LightBlue
            End If
        Next
    End Sub

    Private Sub hotKeysLV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles hotKeysLV.SelectedIndexChanged
        If hotKeysLV.SelectedIndices.Count = 1 Then ' sanity check, as the below function will error out if this isn't correct (you can't get index 0 if nothing's selected!)
            Dim hkIDX As Integer = hotKeysLV.SelectedIndices(0) ' get the currently selected item in the list view

            If mainWindow.hotKeyList(hkIDX, 1) = Nothing Then ' if there isn't a custom hotkey, then display the normal look
                currentHKLabel.Text = "CURRENT HOTKEY"
                currentHKTF.BackColor = Color.Gainsboro
                currentHKTF.Text = mainWindow.hotKeyList(hkIDX, 0)
                currentHKDesc.Text = convertKeyCodeToString(mainWindow.hotKeyList(hkIDX, 0))
                setDefaultHKButton.Enabled = False
            Else ' if there IS a custom hotkey, then change the color of the background
                currentHKLabel.Text = "CURRENT CUSTOM HOTKEY"
                currentHKTF.BackColor = Color.LightBlue
                currentHKTF.Text = mainWindow.hotKeyList(hkIDX, 1)
                currentHKDesc.Text = convertKeyCodeToString(mainWindow.hotKeyList(hkIDX, 1))
                setDefaultHKButton.Enabled = True ' if we have a custom hotkey, allow resetting it
            End If

            setNewHKButton.Enabled = False ' turn the change button OFF until you change the "new hotkey" field

            ' clear the "new hotkey" field when loading an existing hotkey
            newHKTF.Text = Nothing
            newHKDesc.Text = Nothing
        End If
    End Sub

    Private Function convertKeyCodeToString(theKeyCode As String) As String
        Dim outputString As String = Nothing
        Dim currentSettings(2) As String

        currentSettings = Split(theKeyCode, "|")

        If currentSettings(0).Contains("C") Then outputString = "Ctrl+"
        If currentSettings(0).Contains("A") Then outputString += "Alt+"
        If currentSettings(0).Contains("S") Then outputString += "Shift+"

        Dim hkKey As Integer = CType(currentSettings(1), Integer)
        Dim keyName As String = [Enum].GetName(GetType(System.Windows.Forms.Keys), CType(hkKey, System.Windows.Forms.Keys))

        If keyName = "Prior" Then
            keyName = "PageUp"
        ElseIf keyName = "Oem4" Then
            keyName = "["
        ElseIf keyName = "Oem6" Then
            keyName = "]"
        ElseIf keyName = "OemSemicolon" Then
            keyName = "Semicolon"
        ElseIf keyName = "OemQuotes" Then
            keyName = "Quotes"
        End If

        Return outputString & keyName
    End Function

    Private Sub newHKTF_KeyDown(sender As Object, e As KeyEventArgs) Handles newHKTF.KeyDown
        If Not (e.KeyCode = 16 Or e.KeyCode = 17 Or e.KeyCode = 18) Then
            Dim newCode As String = Nothing

            If e.Control Then newCode += "C"
            If e.Alt Then newCode += "A"
            If e.Shift Then newCode += "S"

            newCode += "|" & Int(e.KeyCode).ToString()

            newHKTF.Text = newCode
            newHKDesc.Text = convertKeyCodeToString(newCode)

            setNewHKButton.Enabled = True
        Else
            newHKTF.Text = Nothing
            newHKDesc.Text = Nothing
        End If

        e.Handled = True
        e.SuppressKeyPress = True
    End Sub

    Private Sub setDefaultHKButton_Click(sender As Object, e As EventArgs) Handles setDefaultHKButton.Click
        If hotKeysLV.SelectedIndices.Count = 1 Then
            Dim hkIDX As Integer = hotKeysLV.SelectedIndices(0)

            ' clear formatting for this hotkey
            hotKeysLV.Items(hkIDX).Font = playlistWindow.normalFont
            hotKeysLV.Items(hkIDX).BackColor = Color.White

            mainWindow.hotKeyList(hkIDX, 1) = Nothing ' reset the custom hotkey to nothing

            ' Update the text fields
            currentHKLabel.Text = "CURRENT HOTKEY"
            currentHKTF.BackColor = Color.Gainsboro
            currentHKTF.Text = mainWindow.hotKeyList(hkIDX, 0)
            currentHKDesc.Text = convertKeyCodeToString(mainWindow.hotKeyList(hkIDX, 0))

            newHKTF.Text = Nothing
            newHKDesc.Text = Nothing

            setDefaultHKButton.Enabled = False
            setNewHKButton.Enabled = False
        End If
    End Sub

    Private Sub setNewHKButton_Click(sender As Object, e As EventArgs) Handles setNewHKButton.Click
        If hotKeysLV.SelectedIndices.Count = 1 Then
            Dim hkIDX As Integer = hotKeysLV.SelectedIndices(0)

            ' show custom formatting for this hotkey
            hotKeysLV.Items(hkIDX).Font = playlistWindow.boldFont
            hotKeysLV.Items(hkIDX).BackColor = Color.LightBlue

            mainWindow.hotKeyList(hkIDX, 1) = newHKTF.Text ' set the custom hotkey to a new combination

            ' Update the text fields
            currentHKLabel.Text = "CURRENT CUSTOM HOTKEY"
            currentHKTF.BackColor = Color.LightBlue
            currentHKTF.Text = mainWindow.hotKeyList(hkIDX, 1)
            currentHKDesc.Text = convertKeyCodeToString(mainWindow.hotKeyList(hkIDX, 1))

            newHKTF.Text = Nothing
            newHKDesc.Text = Nothing

            setDefaultHKButton.Enabled = True ' if we set a new hotkey, allow resetting immediately
            setNewHKButton.Enabled = False
        End If
    End Sub

    Private Sub resetAllButton_Click(sender As Object, e As EventArgs) Handles resetAllButton.Click
        Dim returnValue = MessageBox.Show("Are you sure you want to set all hotkeys back to their default values?",
                                          "Reset Hotkeys to Defaults?",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question,
                                          MessageBoxDefaultButton.Button1,
                                          MessageBoxOptions.DefaultDesktopOnly)

        If returnValue = DialogResult.Yes Then
            For currentItem = 0 To UBound(mainWindow.hotKeyList)
                mainWindow.hotKeyList(currentItem, 1) = Nothing ' clear any special hotkey from the current index

                ' clear formatting for ALL of the items in the list
                hotKeysLV.Items(currentItem).Font = playlistWindow.normalFont
                hotKeysLV.Items(currentItem).BackColor = Color.White
            Next

            ' reset all the controls on the page to their default states
            currentHKLabel.Text = "CURRENT HOTKEY"
            currentHKTF.BackColor = Color.Gainsboro
            currentHKTF.Text = Nothing
            currentHKDesc.Text = Nothing

            newHKTF.Text = Nothing
            newHKDesc.Text = Nothing

            setDefaultHKButton.Enabled = False
            setNewHKButton.Enabled = False
        End If
    End Sub

    Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub
End Class
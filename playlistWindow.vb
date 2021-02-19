Public Class playlistWindow
    Public currentLooperFile As String = Nothing ' the current .looper file loaded in the playlist

    Public currentPlayingEvent As Integer = -1 ' which event is currently playing
    Public actualPlayingEvent As Integer = -1 ' the actual event that's playing (keeping track for getting out of Shuffle Mode)

    Dim totalNumberOfEvents As Integer = 100 ' the number to start ordering events from

    Dim isModified As Boolean = False ' whether or not we've modified the current looper playlist
    Dim insertItem As Boolean = False ' whether or not to allow inserting items into the events list
    Dim SelectedLSI As ListViewItem.ListViewSubItem ' for modifying event list names and loops

    Public stilLLoading As Boolean = False ' set this to True when doing loadEvent() so the program doesn't ask to load multiple things at once

    Dim inSearchMode As Boolean = False ' if we're in search mode or not
    Dim currentSearchPlayingEvent As Integer = -1 ' the ID# (from (0) of the event in the events list) of the current playing event's position in the playlist
    Dim eventsListArray(0, 0) As Object ' an array to hold the events list items during searching

    ReadOnly normalFont As New Font("Segoe UI", 9) ' the normal "non-playing" font for the events list
    ReadOnly boldFont As New Font("Segoe UI", 9, FontStyle.Bold) ' the bold "playing" font for the events list

    ReadOnly errorColor As Color = Color.FromArgb(228, 162, 162) ' the color red signifies ERROR
    ReadOnly foundColor As Color = Color.FromArgb(162, 228, 179) ' we're good to go!
    ReadOnly relativeColor As Color = Color.FromArgb(215, 226, 239) ' the file exists, but not where we were told...

    Public Declare Function SetForegroundWindow Lib "user32" (ByVal hWnd As IntPtr) As Integer ' set specific window (MPC-HC) as the frontmost window

    ' ======================================================================================
    ' ================= FORM FUNCTIONS =====================================================
    ' ======================================================================================

    Private Sub playlistWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.icon
    End Sub

    Private Sub playlistWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        mainWindow.clearHotKeys() ' turn off hotkeys

        If (e.CloseReason = CloseReason.UserClosing) Then
            Dim confirmSave = unInitialize() ' if we've been asked to quit, check to see if that's OK

            If confirmSave = DialogResult.Cancel Then ' we chose NOT to quit, so restore things
                mainWindow.setHotKeys()
                mainWindow.isQuitting = False

                If mainWindow.autoPlayDialogs = True Then mainWindow.SendMessage(CMD_SEND.CMD_PLAY) ' we cancelled quitting, so start playing again

                e.Cancel = True ' if we cancelled, then don't quit...
            Else
                mainWindow.Close() ' if we're unloading playlistWindow, then unload the main window too
            End If
        End If
    End Sub

    Private Sub playlistWindow_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        ' When leaving the playlist window inactive, turn the delete and modify buttons off
        deleteEventButton.Enabled = False
        modifyEventButton.Enabled = False
    End Sub

    Private Sub playlistWindow_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        ' When entering the playlist window, turn buttons on, checking to see if they're good to turn on or not
        selectedEventsButtonTriggers()
    End Sub

    Private Sub eventsList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles eventsList.SelectedIndexChanged
        selectedEventsButtonTriggers()
    End Sub

    Private Sub selectedEventsButtonTriggers()
        If eventsList.SelectedItems.Count > 0 Then
            saveButton.Enabled = True ' Enable the save button (so you can save the subset of events)

            If mainWindow.getMode() Then ' if we're in OFF or Loop mode
                If eventsList.SelectedItems.Count = 1 Then
                    modifyEventButton.Text = "MODIFY EVENT"
                    modifyEventButton.Enabled = True
                Else ' if we have 2 contiginous events selected, let's merge 'em
                    If eventsList.SelectedItems(0).Index + eventsList.SelectedItems.Count = eventsList.SelectedItems(eventsList.SelectedItems.Count - 1).Index + 1 Then
                        modifyEventButton.Text = "MERGE INTO 1"
                        modifyEventButton.Enabled = True
                    Else ' if we have scattered events, then disable the merging
                        modifyEventButton.Text = "MODIFY EVENT"
                        modifyEventButton.Enabled = False
                    End If
                End If
            Else ' if we're not in OFF or Loop mode
                modifyEventButton.Text = "MODIFY EVENT"
                modifyEventButton.Enabled = False
            End If

            If mainWindow.loopModeButton.Text <> "Shuffle Mode" Then
                deleteEventButton.Enabled = True ' if we're not in Shuffle mode, then allow deleting the selected event
            Else
                deleteEventButton.Enabled = False ' don't allow deleting events in Shuffle mode
            End If
        Else ' if we have nothing selected, change the text for the modify button, but turn it off
            modifyEventButton.Text = "MODIFY EVENT"
            modifyEventButton.Enabled = False
            deleteEventButton.Enabled = False

            If isModified = False Then
                saveButton.Enabled = False ' if we haven't modified anything, then disallow this
            Else
                saveButton.Enabled = True ' otherwise make it enabled
            End If
        End If
    End Sub

    Private Sub eventsList_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles eventsList.MouseDoubleClick
        mainWindow.switchToLoopMode() ' double clicking an item returns to Loop mode ALWUS
        loadEvent(eventsList.SelectedItems(0).Index) ' load the currently selected event
    End Sub

    Public Function unInitialize(Optional forceQuit As Boolean = False) As Integer
        mainWindow.SendMessage(CMD_SEND.CMD_PAUSE) ' pause the player when going to the Quit looper window
        mainWindow.isQuitting = True ' set this to true to cause the Threads to skip checking position/hotkeys for now

        If isModified = True Then ' if isModified is true, then ask to save first
            Dim MsgBoxMsg As String
            Dim MsgBoxButtons As MessageBoxButtons

            If forceQuit = False Then ' if we're just quitting normally (MPC-HC is still active and running)
                MsgBoxButtons = MessageBoxButtons.YesNoCancel

                MsgBoxMsg = "Do you want to save the current playlist before quitting?" _
                        & vbCrLf & vbCrLf & "Click Yes to save and quit" _
                        & vbCrLf & "Click No to quit without saving" _
                        & vbCrLf & "Click Cancel to go back to Looper without quitting"
            Else ' if we're being forced to quit (MPC-HC has closed, and we're quitting regardless)
                MsgBoxButtons = MessageBoxButtons.YesNo

                MsgBoxMsg = "Do you want to save the current playlist before quitting?" _
                        & vbCrLf & vbCrLf & "Click Yes to save and quit" _
                        & vbCrLf & "Click No to quit without saving"
            End If

            Dim returnValue = MessageBox.Show(MsgBoxMsg,
                                              "Save Playlist Before Quitting?",
                                              MsgBoxButtons,
                                              MessageBoxIcon.Question,
                                              MessageBoxDefaultButton.Button1,
                                              MessageBoxOptions.DefaultDesktopOnly)

            If returnValue = DialogResult.Yes Then
                Return saveLooperFile() ' save the looper file                
            Else ' if the result is "No" or "Cancel", then just return that value
                Return returnValue
            End If
        Else ' if we're not set to "isModified", then just quit out
            Return DialogResult.No ' return a value, so we don't get an exception
        End If
    End Function

    Public Sub saveLastPlayedFile()
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(INIFile)

        Dim autoloadLastLooper As String = betweenTheLines(fileReader, "autoloadLastLooper=", vbCrLf, Nothing) ' should we launch the last looper file when closed?

        If autoloadLastLooper <> Nothing Then ' if the preference is "Nothing" then we're not set to enable it
            Dim startOffset = Strings.InStr(fileReader, "autoloadLastLooper=") ' the beginning of the old preference in the INI file
            Dim endOffset = Strings.InStr(startOffset, fileReader, vbCrLf) ' the end of the line above ^^

            Dim writingString = Strings.Left(fileReader, startOffset - 1) ' the INI file before the above preference
            writingString += "autoloadLastLooper=" & currentLooperFile & "|" & currentPlayingEvent & vbCrLf ' the new preference to be set
            writingString += Strings.Mid(fileReader, endOffset + 2) ' the rest of the INI file after that preference

            System.IO.File.WriteAllText(INIFile, writingString) ' write the "new" INI file
        End If
    End Sub

    ' ======================================================================================
    ' ================= FILESYSTEM FUNCTIONS ===============================================
    ' ======================================================================================

    Private Sub setIsModified(ByVal setModified As Integer)
        If setModified = 1 Then ' if we've made changes, then draw a * in the title of the playlist to show that
            isModified = True ' we've modified something
            clearEventsButton.Enabled = True ' and allow the events list to be cleared
            saveButton.Enabled = True

            If currentLooperFile = Nothing Then ' if we have nothing loaded, then we're in "Untitled" mode
                If Me.Text <> "Playlist - Untitled *" Then
                    Me.Text = "Playlist - Untitled *"
                End If
            Else ' if something IS loaded, then show that
                If Me.Text <> "Playlist - " & My.Computer.FileSystem.GetFileInfo(currentLooperFile).Name.ToString & " *" Then
                    Me.Text = "Playlist - " & My.Computer.FileSystem.GetFileInfo(currentLooperFile).Name.ToString & " *"
                End If
            End If
        Else ' if we haven't made changes, then DO NOT draw a * in the title of the playlist to show that
            isModified = False ' we haven't modified anything
            saveButton.Enabled = False

            If currentLooperFile = Nothing Then ' if we have nothing loaded, then we're in "Untitled" mode
                If Me.Text <> "Playlist - Untitled" Then
                    Me.Text = "Playlist - Untitled"
                End If
            Else ' if something IS loaded, then show that
                If Me.Text <> "Playlist - " & My.Computer.FileSystem.GetFileInfo(currentLooperFile).Name.ToString Then
                    Me.Text = "Playlist - " & My.Computer.FileSystem.GetFileInfo(currentLooperFile).Name.ToString
                End If
            End If
        End If
    End Sub

    Public Sub loadLooperFile(Optional clearEventList As Boolean = True, Optional looperFile As String = Nothing, Optional eventToPlay As Integer = -1)
        mainWindow.SendMessage(CMD_SEND.CMD_PAUSE) ' pause the player when going to the Load Looper window

        Dim confirmSave As Integer = DialogResult.No ' this doesn't specifically NEED to be no, it just needs to NOT be cancel

        If clearEventList = True Then ' if we're not loading a brand-new looper file
            If isModified = True Then ' and if the list has been modified
                ' then ask if we want to save this list first before overwriting it
                confirmSave = MessageBox.Show("Do you want to save the current playlist before opening another .looper file?" _
                                 & vbCrLf & vbCrLf & "Click Yes to save" _
                                 & vbCrLf & "No to continue without saving" _
                                 & vbCrLf & "Cancel to go back to Looper without doing anything",
                                              "Save Playlist Before Opening?",
                                              MessageBoxButtons.YesNoCancel,
                                              MessageBoxIcon.Question,
                                              MessageBoxDefaultButton.Button1,
                                              MessageBoxOptions.DefaultDesktopOnly)

                If confirmSave = DialogResult.Yes Then
                    confirmSave = saveLooperFile() ' save the looper file (or cancel the process, either by dialog or by file-choose-dialog)
                End If
            End If
        End If

        If confirmSave <> DialogResult.Cancel Then ' as long as you didn't click 'cancel' above, then...
            If looperFile = Nothing Then ' if we haven't specified a looper file to force opening, then ask for one
                Dim openDialogTitle As String

                If clearEventList = True Then
                    openDialogTitle = "Load a .looper File"
                Else
                    openDialogTitle = "Import a .looper File into the Current Playlist"
                End If

                Using looperFileDialog As New OpenFileDialog With {
                    .Title = openDialogTitle,
                    .Filter = "Looper files|*.looper"
                }

                    If looperFileDialog.ShowDialog = DialogResult.OK Then ' if we cancelled, this value will remain Nothing, and then nothing below will trigger
                        looperFile = looperFileDialog.FileName ' if we didn't cancel, then open the file selected
                    End If
                End Using
            End If

            If looperFile <> Nothing Then ' if we agreed to load a new file, then load it... otherwise, do nothing!
                currentLooperFile = looperFile ' if you choose a new file, then load that name in, but if not, then don't do anything else

                If clearEventList = True Then
                    totalNumberOfEvents = 100 ' reset the first index to 100

                    currentPlayingEvent = -1 ' we're not playing an event
                    actualPlayingEvent = -1 ' we're NOT playing an event

                    inSearchMode = False ' we're not in search mode t'neither
                    cancelSearchButton.Enabled = False ' disable the "cancel search" button
                    currentSearchPlayingEvent = -1 ' WE'RE NOT PLAYING AN EVENT! (and not in search mode)
                    ReDim eventsListArray(0, 0) ' delete the master array if it exists, because we're going to need to recreate it anyway

                    mainWindow.cancelRandomization() ' clear out the randomized list
                    eventsList.Items.Clear() ' remove all of the current events from the list

                    setIsModified(0) ' we're not modified, so sort that all out
                End If

                Dim fileReader As String
                fileReader = My.Computer.FileSystem.ReadAllText(currentLooperFile)

                Dim eventListArray() As String = Split(fileReader, vbCrLf) ' read all of the events into an array, split at line endings

                For a As Integer = 0 To eventListArray.Length - 1
                    If eventListArray(a) <> "" Then ' if the current read isn't a blank space (the extra line after 99% of .looper files)
                        Dim currentEvent() As String = Split(eventListArray(a), "|")
                        addEvent(currentEvent(0), currentEvent(1), currentEvent(2), currentEvent(3), a)
                    End If
                Next

                fileReader = Nothing ' un-initialize the variable
                eventListArray = Nothing ' un-initialize the variable

                If clearEventList = True Then ' if we're regularly loading a file, then do these things
                    If mainWindow.autoplayFirstEvent = True Then
                        If eventsList.Items.Count > 1 Then
                            loadPrevNextEvent(1) ' jump to the first playable event (instead of loadEvent(), do loadPrevNextEvent() to seek to the next "good" event)
                        Else
                            loadEvent(0, True) ' we only have one event to load, so try to load that...
                        End If
                    Else ' if we're not set to autoplay the first event, but we have this option (auto-playing after leaving dialogs) turned on...
                        If mainWindow.autoPlayDialogs = True Then mainWindow.SendMessage(CMD_SEND.CMD_PLAY) ' we're out of the dialog, so start playing
                    End If

                    If mainWindow.dontForceLooperModeonOpen = False Then
                        mainWindow.switchToLoopMode() ' if we're not set to not automatically go to looper mode, then switch it
                    Else
                        If mainWindow.loopModeButton.Text = "Shuffle Mode" Then
                            mainWindow.switchToShuffleMode() ' force the shuffle mode generator to re-generate the randomized list
                        End If
                    End If

                    clearEventsButton.Enabled = True
                Else ' else, if we're merging .looper files, do these things...
                    If Not mainWindow.getMode() Then ' we're in Shuffle or Playlist modes
                        mainWindow.switchToLoopMode() ' force switch to Loop Mode
                        currentPlayingEvent += 1
                    End If

                    currentLooperFile = Nothing ' we're no longer operating out of the same file, so reset this
                    setIsModified(1)

                    If mainWindow.autoPlayDialogs = True Then mainWindow.SendMessage(CMD_SEND.CMD_PLAY) ' we merged a .looper into the list, so start playing again
                End If

                tallyTotalList() ' calculate the time of all events in the events list
            Else
                If mainWindow.autoPlayDialogs = True Then mainWindow.SendMessage(CMD_SEND.CMD_PLAY) ' we cancelled loading, so start playing again
            End If
        Else
            If mainWindow.autoPlayDialogs = True Then mainWindow.SendMessage(CMD_SEND.CMD_PLAY) ' we cancelled loading (and saving), so start playing again
        End If
    End Sub

    Private Function findFile(Optional specificEvent As Integer = -1) As Integer
        Dim previousPath, pathToFind, pathName, pathExt As String
        Dim missingEvents As New List(Of Integer)

        mainWindow.SendMessage(CMD_SEND.CMD_PAUSE) ' force pause when checking to load events

        For a As Integer = 0 To eventsList.Items.Count - 1
            If eventsList.Items(a).BackColor = errorColor Then
                missingEvents.Add(a) ' make a list of events that have missing paths (so we don't need to load the entire events list every time)
            End If
        Next

        Dim findFilesPrompt As Integer = DialogResult.No ' if there's just one event, then we just need to find one, so we don't need the dialog asking

        If missingEvents.Count > 1 Then
            findFilesPrompt = MessageBox.Show("The current playlist has " & missingEvents.Count & " events that point" & vbCrLf &
            "to file paths that can not be located." & vbCrLf & vbCrLf &
            "Would you like to locate all of those missing files," & vbCrLf &
            "just the current event's missing file," & vbCrLf &
            "or skip looking for files for now?" & vbCrLf & vbCrLf &
            "Click Yes to find missing files for the entire playlist" & vbCrLf &
            "Click No to find only this event's missing file" & vbCrLf &
            "Click Cancel to cancel finding missing files for now",
                                  "Missing Files!",
                                  MessageBoxButtons.YesNoCancel,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button1,
                                  MessageBoxOptions.DefaultDesktopOnly)

            If findFilesPrompt = DialogResult.Yes Then
                specificEvent = -1 ' look for all of the files and not just one specific one
            End If
        End If

        If findFilesPrompt <> DialogResult.Cancel Then ' if we didn't cancel out of finding events (above)
            previousPath = Nothing

            For a As Integer = 0 To missingEvents.Count - 1 ' iterate through the empty paths now
                pathToFind = eventsList.Items(missingEvents(a)).SubItems(7).Text ' the original "missing" file path
                pathName = My.Computer.FileSystem.GetFileInfo(pathToFind).Name ' the name of the file (minus the directory)
                pathExt = Mid(My.Computer.FileSystem.GetFileInfo(pathToFind).Extension, 2) ' the extension of the file

                If specificEvent = -1 Or specificEvent = missingEvents(a) Then ' if the event we're requesting is this specific one OR isn't a specific event...
                    If eventsList.Items(missingEvents(a)).BackColor = errorColor Then ' if this event still has an error, then...
                        Using fileFind As New OpenFileDialog With {
                           .Title = "Where is the file originally found at " & pathToFind & " now?",
                           .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                           .Filter = "Matching|" & pathName & "|" & pathExt & " Files|*." & pathExt & "|All Files|*.*"
                            }

                            If fileFind.ShowDialog = DialogResult.OK Then
                                setIsModified(1)
                                previousPath = My.Computer.FileSystem.GetFileInfo(fileFind.FileName).DirectoryName.ToString ' check this against the next file

                                For b As Integer = 0 To missingEvents.Count - 1 ' scan through the rest of the missing items to see if paths are restored
                                    If eventsList.Items(missingEvents(b)).SubItems(7).Text = pathToFind Then ' if this event matches the "missing" file path
                                        eventsList.Items(missingEvents(b)).SubItems(7).Text = fileFind.FileName ' replace it with the newly found file
                                        eventsList.Items(missingEvents(b)).BackColor = foundColor ' re-color that event green to show we've found the correct path
                                    Else ' if this event doesn't match the same file, still check it against the *directory*
                                        Dim otherPathToFind As String = eventsList.Items(missingEvents(b)).SubItems(7).Text ' a comparison to check all files in the directory
                                        Dim otherPathName As String = My.Computer.FileSystem.GetFileInfo(otherPathToFind).Name ' this is the current base file we're looking for

                                        If My.Computer.FileSystem.GetFileInfo(previousPath & "\" & otherPathName).Exists Then ' if the file exists in the .looper file's path
                                            eventsList.Items(missingEvents(a)).SubItems(7).Text = previousPath & "\" & otherPathName ' replace it with the newly found file
                                            eventsList.Items(missingEvents(a)).BackColor = foundColor ' re-color that event green to show we've found the correct path
                                        End If
                                    End If
                                Next
                            Else ' we didn't find a result, or cancelled, so pass that back to the loadEvent() Sub
                                If specificEvent <> -1 Or a = missingEvents.Count - 1 Then ' if we're either looking at a single event or the last event
                                    Return DialogResult.Cancel ' we cancelled out of finding this file
                                End If
                            End If
                        End Using
                    End If
                End If
            Next
        End If

        Return findFilesPrompt ' if we cancelled, let's let the loadEvent() procedure know it
    End Function

    Public Function saveLooperFile(Optional saveSubset As Boolean = False) As Integer
        Dim checkOverwrite As Integer = DialogResult.Ignore

        If eventsList.Items.Count > 0 Then ' if we have nothing in the playlist, there's no reason to save it~!
            mainWindow.SendMessage(CMD_SEND.CMD_PAUSE) ' pause the player when going to the Save Looper window

            Dim savefile As String = Nothing
            Dim savingList As New List(Of Integer)

            If saveSubset = True Then ' if you want to save the entire playlist, then just add all the items to the list to save
                For a As Integer = 0 To eventsList.SelectedIndices.Count - 1
                    savingList.Add(eventsList.SelectedIndices(a))
                Next
            Else ' if we only want to save a few of them, then just add those ones
                For a As Integer = 0 To eventsList.Items.Count - 1
                    savingList.Add(a)
                Next
            End If

            If currentLooperFile = Nothing Or saveSubset = True Then
                savefile = returnSaveFile(saveSubset) ' if saveSubset is true, this will change the title on the Save... prompt
            Else
                checkOverwrite = MessageBox.Show("Are you sure you want to overwrite the current .looper file?" & vbCrLf & vbCrLf &
                                            "Choose Yes to overwrite the current .looper file" & vbCrLf &
                                            "No to save to a New .looper file" & vbCrLf &
                                            "Or Cancel to abort saving completely",
                                              "Overwrite Current File?",
                                              MessageBoxButtons.YesNoCancel,
                                              MessageBoxIcon.Question,
                                              MessageBoxDefaultButton.Button1,
                                              MessageBoxOptions.DefaultDesktopOnly)

                If checkOverwrite = DialogResult.Yes Then
                    savefile = currentLooperFile
                ElseIf checkOverwrite = DialogResult.No Then
                    savefile = returnSaveFile()
                ElseIf checkOverwrite = DialogResult.Cancel Then
                    ' don't do anything, because we chose to cancel, so saveFile = Nothing
                End If
            End If

            If savefile <> Nothing Then
                Dim writeString As String

                ' Write the file using the Streamwriter process, to save to hidden files (if we come across those) -
                ' The other method (the normal, one-code-line method) comes up with an AccessException when we try
                ' to do that, so we need to do it this way (the *proper* way, innit?)
                Using writeFile As IO.FileStream = IO.File.Open(savefile, IO.FileMode.OpenOrCreate, IO.FileAccess.Write)
                    Using writer As IO.StreamWriter = New IO.StreamWriter(writeFile, System.Text.Encoding.Unicode)
                        For a As Integer = 0 To savingList.Count - 1
                            writeString = Nothing

                            If eventsList.Items(savingList(a)).SubItems(2).Text <> "100" Then ' if the speed isn't 100, then add that to the name in the looper file
                                writeString = writeString & "<S:" & eventsList.Items(savingList(a)).SubItems(2).Text & ">"
                            End If

                            If eventsList.Items(savingList(a)).SubItems(3).Text <> "1" Then ' if the repeats are more than 1, then add that to the name in the looper file
                                writeString = writeString & "<L:" & eventsList.Items(savingList(a)).SubItems(3).Text & ">"
                            End If

                            writeString = writeString & eventsList.Items(savingList(a)).SubItems(1).Text & "|" &
                            eventsList.Items(savingList(a)).SubItems(4).Text & "|" &
                            eventsList.Items(savingList(a)).SubItems(5).Text & "|" &
                            eventsList.Items(savingList(a)).SubItems(7).Text

                            writer.WriteLine(writeString) ' write this specific event to the .looper file
                        Next

                        writeFile.SetLength(writeFile.Position) ' set the EOF
                    End Using

                    writeFile.Close() ' close file for writing
                End Using

                If mainWindow.isQuitting = False Then
                    If saveSubset = False Then ' if we are saving or saving a new Looper file, then show it - otherwise, you just saved a subclip
                        currentLooperFile = savefile ' change the current looper file name
                        setIsModified(0) ' we saved a file, so we're no longer in modified state

                        For a As Integer = 0 To eventsList.Items.Count - 1
                            If eventsList.Items(a).BackColor <> errorColor Then ' only if the events aren't red (missing)
                                eventsList.Items(a).BackColor = foundColor ' recolor all of the blue events green to show they come from the .looper's path
                            End If

                            eventsList.Items(a).SubItems(0).Text = CStr(100 + a) ' re-organize the current order as the "correct" order
                            eventsList.Items(a).Selected = False ' de-select this item when re-saving it
                        Next
                    Else
                        Dim loadSubset = MessageBox.Show("You just saved a selection of events to a new .looper file." & vbCrLf &
                                            "Would you like to open that new file?" & vbCrLf & vbCrLf &
                                            "Choose Yes to open the new .looper file" & vbCrLf &
                                            "Choose No to continue using the current file",
                                              "Open Looper file?",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question,
                                              MessageBoxDefaultButton.Button2,
                                              MessageBoxOptions.DefaultDesktopOnly)

                        If loadSubset = DialogResult.Yes Then
                            loadLooperFile(True, savefile)
                        End If
                    End If
                End If

                If mainWindow.autoPlayDialogs = True Then mainWindow.SendMessage(CMD_SEND.CMD_PLAY) ' we finished saving, so start playing again
            Else
                checkOverwrite = DialogResult.Cancel
                If mainWindow.autoPlayDialogs = True Then mainWindow.SendMessage(CMD_SEND.CMD_PLAY) ' we cancelled saving, so start playing again
            End If
        End If

        Return checkOverwrite
    End Function

    Private Function returnSaveFile(Optional saveSubset As Boolean = False) As String
        Dim saveTitle As String

        If saveSubset = True Then
            saveTitle = "Save Current Selection of Events as New Looper File..."
        Else
            saveTitle = "Save New Looper file..."
        End If

        Dim saveFileDialog As New SaveFileDialog With {
            .AddExtension = True,
            .DefaultExt = ".looper",
            .OverwritePrompt = True,
            .Filter = "Looper files|*.looper",
            .Title = saveTitle
        }

        ' ----------------- CHECK TO SAVE SIDECAR FILE -----------------
        Dim filesMatching As Integer = 1
        Dim checkAgainst As String = eventsList.Items(0).SubItems(7).Text ' the file path of the first event

        For a As Integer = 1 To eventsList.Items.Count - 1
            If eventsList.Items(a).SubItems(7).Text = checkAgainst Then filesMatching += 1 ' if the filename is the same in this event, then increment counter
        Next

        If filesMatching = eventsList.Items.Count Then ' if the filenames are the same for every event, then default to saving a sidecar file
            saveFileDialog.FileName = My.Computer.FileSystem.GetFileInfo(checkAgainst).Name & ".looper"
            saveFileDialog.InitialDirectory = My.Computer.FileSystem.GetFileInfo(checkAgainst).Directory.ToString
        End If

        ' ----------------- SHOW THE DIALOG AND RETURN -----------------
        If saveFileDialog.ShowDialog(mainWindow) = DialogResult.OK Then
            saveFileDialog.Dispose()
            Return saveFileDialog.FileName
        Else
            saveFileDialog.Dispose()
            Return Nothing
        End If
    End Function

    Private Function checkAlternateFileExists(ByVal filePath As String) As String
        Dim checkFile = My.Computer.FileSystem.GetFileInfo(filePath) ' check to see if the currently specified file exists

        If checkFile.Exists Then
            Return filePath
        Else ' we need to find the file elsewhere
            Dim looperBase = My.Computer.FileSystem.GetFileInfo(currentLooperFile).Directory.ToString ' get the .looper file's base path

            If My.Computer.FileSystem.GetFileInfo(looperBase & "\" & checkFile.Name.ToString).Exists Then ' if the file exists in the looper directory
                Return looperBase & "\" & checkFile.Name.ToString ' then mark the file as *existing*, but in a different directory
            Else
                Return "Not Found" ' mark the file as not existing in the original *or* the .looper file directory
            End If
        End If
    End Function

    ' ======================================================================================
    ' ================= EVENT FUNCTIONS ====================================================
    ' ======================================================================================

    Private Function returnListViewItem(ByVal eventName As String,
                                        ByVal eventSpeed As String,
                                        ByVal eventRepeats As String,
                                        ByVal eventINPoint As String,
                                        ByVal eventOUTPoint As String,
                                        ByVal eventPath As String,
                                        Optional eventNum As String = Nothing,
                                        Optional newDur As String = Nothing) As ListViewItem

        Dim newItemArray(7) As String

        If eventNum = Nothing Then
            newItemArray(0) = CStr(totalNumberOfEvents) ' find the event number based on the current "new" event number
        Else
            newItemArray(0) = eventNum ' use the specified number as the new event number
        End If

        newItemArray(1) = eventName
        newItemArray(2) = eventSpeed
        newItemArray(3) = eventRepeats
        newItemArray(4) = eventINPoint
        newItemArray(5) = eventOUTPoint

        If newDur = Nothing Then ' if we haven't specified a duration, then let's calculate it
            If eventOUTPoint <> Nothing Then ' if the out point isn't "nothing", then calculate the duration
                newItemArray(6) = NumberToTimeString(TimeStringToNumber(eventOUTPoint) - TimeStringToNumber(eventINPoint))
            Else ' if we don't know what the duration is (there's no out point set), then it's endless, man...
                newItemArray(6) = Nothing
            End If
        Else
            newItemArray(6) = newDur ' we already calculated the duration in an earlier step (for combining events), so just use that value
        End If

        newItemArray(7) = eventPath

        Dim newEvent As ListViewItem = New ListViewItem(newItemArray)
        Return newEvent
    End Function


    Private Sub addEvent(ByVal eventName As String, ByVal eventINPoint As String, ByVal eventOutPoint As String,
                              ByVal eventFilePath As String, Optional newEventCount As Integer = 0)
        ' Color the incoming .looper file as different colors based on file availability
        Dim fileExists As String = checkAlternateFileExists(eventFilePath)
        Dim newEventColor As Color

        If fileExists = "Not Found" Then ' The file does *not* exist at all, so color this row red
            newEventColor = errorColor
        ElseIf fileExists = eventFilePath Then ' The file exists at the exact location the .looper file said it would, and we are green for GO
            newEventColor = foundColor
        Else ' The file exists in an alternate path (the path of the .looper file), so color this row blue
            newEventColor = relativeColor
            eventFilePath = fileExists
        End If

        Dim newEvent As ListViewItem = returnListViewItem(removeParams(eventName), betweenTheLines(eventName, "<S:", ">", "100"),
                                                          betweenTheLines(eventName, "<L:", ">", "1"), eventINPoint, eventOutPoint,
                                                          eventFilePath)

        If insertItem = True Then
            If eventsList.SelectedItems.Count > 0 Then ' if something is actually selected
                Dim newIndex = eventsList.SelectedItems.Item(eventsList.SelectedItems.Count - 1).Index + 1 + newEventCount
                eventsList.Items.Insert(newIndex, newEvent) ' add the item into the playlist instead of the end
                eventsList.Items(newIndex).BackColor = newEventColor
            Else ' if nothing is selected, then just add them as you normally would
                eventsList.Items.Add(newEvent)
                eventsList.Items(eventsList.Items.Count - 1).BackColor = newEventColor
            End If
        Else ' if nothing is selected, and "inserting" is turned off, then just add them to the end of the list
            eventsList.Items.Add(newEvent)
            eventsList.Items(eventsList.Items.Count - 1).BackColor = newEventColor
        End If

        totalNumberOfEvents += 1 ' increment the total number of events counter (to add one at the end)        
    End Sub

    Public Sub addEvent()
        If mainWindow.currentPlayingFile <> Nothing Then ' if MPC-HC isn't playing anything, then don't DO anything
            If inSearchMode = True Then
                restoreFromSearch() 'if we're in Search mode and decide to add an event, get out of Search mode first
            End If

            mainWindow.SendMessage(CMD_SEND.CMD_GETNOWPLAYING) ' force refresh of NOWPLAYING info

            eventsList.ListViewItemSorter = Nothing ' add the event at the very end of the list if the list is sorted

            Dim newEvent = returnListViewItem(mainWindow.newEventString, mainWindow.speedSlider.Value.ToString, "1", mainWindow.inTF.Text, mainWindow.outTF.Text,
                                          mainWindow.currentPlayingFile)
            Dim newIndex As Integer

            If insertItem = True Then
                If eventsList.SelectedItems.Count > 0 Then ' if something is actually selected
                    newIndex = eventsList.SelectedItems.Item(eventsList.SelectedItems.Count - 1).Index + 1 ' the new index will be where the item was inserted, plus one
                    eventsList.Items.Insert(eventsList.SelectedItems.Item(eventsList.SelectedItems.Count - 1).Index + 1, newEvent)
                Else ' if nothing is selected, then just add them as you normally would
                    newIndex = eventsList.Items.Count ' the new index will be the count, minus one - or in this case, literally just the count, before the one added
                    eventsList.Items.Add(newEvent)
                End If
            Else ' if nothing is selected, and "inserting" is turned off, then just add them to the end of the list
                newIndex = eventsList.Items.Count ' the new index will be the count, minus one - or in this case, literally just the count, before the one added
                eventsList.Items.Add(newEvent)
            End If

            currentPlayingEvent = newIndex ' mark the newly inserted (or added) event as the current playing event
            setIsModified(1) ' we've added something, so we're now modified

            For a As Integer = 0 To eventsList.Items.Count - 1
                eventsList.Items(a).Selected = False ' clear all other selections
            Next

            highlightEvent(newIndex) ' mark the new event as the currently playing event by bolding the text

            ' select the new item as the current item
            eventsList.Select()
            eventsList.Items(newIndex).EnsureVisible()
            eventsList.Items(newIndex).Selected = True

            tallyTotalList() ' re-tabulate the event list tally

            editListViewItem(newIndex) ' go into the text editor

            totalNumberOfEvents += 1 ' increment the total number of events counter (to add one at the end)
        End If
    End Sub

    Public Sub deleteEvents()
        If eventsList.Items.Count > 0 Then ' if the events list has more than one event
            mainWindow.SendMessage(CMD_SEND.CMD_PAUSE) ' pause the player when asking if you want to delete files

            Dim selectedItemsCount = eventsList.SelectedItems.Count ' get the count of events we're choosing to delete

            If selectedItemsCount > 0 Then ' if we have *at least* one item selected, then try to delete it
                Dim confirmDelete As DialogResult
                Dim MsgBoxMsg As String ' if we're in Search mode, then write a little description that tells you that

                If selectedItemsCount = eventsList.Items.Count Then ' we're choosing to delete ALL the events (which is basically the same as "Clear List")
                    If inSearchMode = False Then
                        MsgBoxMsg =
                            "You've chosen to delete everything in the current playlist!" & vbCrLf & vbCrLf &
                            "Deleting ALL of the events from the playlist is the same thing" & vbCrLf &
                            "as clicking on the Clear All button.  All of the events will be" & vbCrLf &
                            "removed and Looper will start a brand-new session." & vbCrLf & vbCrLf &
                            "Would you like to remove all of the current events and" & vbCrLf &
                            "start a completely New Looper session?"
                    Else
                        MsgBoxMsg =
                            "NOTE: You're currently in Search Mode and" & vbCrLf &
                            "You've chosen to remove everything in the current playlist!" & vbCrLf & vbCrLf &
                            "Removing all of these events in Search Mode will clear" & vbCrLf &
                            "the search and go back to Normal (non-Search) mode, but will" & vbCrLf &
                            "not actually delete any of the events from the playlist." & vbCrLf & vbCrLf &
                            "Would you like to do that?"
                    End If

                    confirmDelete = MessageBox.Show(MsgBoxMsg,
                                              "Clear Entire Playlist?",
                                              MessageBoxButtons.OKCancel,
                                              MessageBoxIcon.Question,
                                              MessageBoxDefaultButton.Button1,
                                              MessageBoxOptions.DefaultDesktopOnly)

                    If confirmDelete = DialogResult.OK Then
                        If inSearchMode = False Then
                            clearEventsList() ' clear the entire events list
                        Else
                            restoreFromSearch() ' switch from Search mode to normal mode again
                        End If
                    End If
                Else ' we have selected some items, but not ALL of them
                    Dim titleText As String ' change the title if we're in Search mode
                    If inSearchMode = False Then
                        MsgBoxMsg = "Are you sure that you want to delete "
                        titleText = "Delete Events?"
                    Else
                        MsgBoxMsg =
                        "NOTE: You're currently in Search Mode" & vbCrLf & vbCrLf &
                        "In Search Mode, if you remove an event, it won't actually delete it" & vbCrLf &
                        "from the master playlist, just from this list of search results." & vbCrLf & vbCrLf &
                        "If you want to delete the event from the master playlist completely," & vbCrLf &
                        "clear the search (by clicking on the X to the right of the Search button)" & vbCrLf &
                        "before attempting to remove the event." & vbCrLf & vbCrLf &
                        "Are you sure that you want to remove "
                        titleText = "Remove Events?"
                    End If

                    If selectedItemsCount > 1 Then ' we have more than one event selected
                        MsgBoxMsg += "these " & selectedItemsCount & " events?"
                    Else
                        MsgBoxMsg += "this event?"
                    End If

                    confirmDelete = MessageBox.Show(MsgBoxMsg,
                                              titleText,
                                              MessageBoxButtons.OKCancel,
                                              MessageBoxIcon.Question,
                                              MessageBoxDefaultButton.Button1,
                                              MessageBoxOptions.DefaultDesktopOnly)

                    If confirmDelete = DialogResult.OK Then
                        For a As Integer = selectedItemsCount - 1 To 0 Step -1 ' delete items from the end to the back, so the index stays correct
                            eventsList.Items.RemoveAt(eventsList.SelectedItems.Item(a).Index)
                        Next

                        setIsModified(1)
                        tallyTotalList() ' re-tabulate the event list tally
                    End If
                End If
            End If

            If mainWindow.autoPlayDialogs = True Then mainWindow.SendMessage(CMD_SEND.CMD_PLAY) ' we finished deleting (or cancelling), so start playing again
        End If
    End Sub

    Public Sub modifyEvent(Optional theEvent As Integer = -1)
        If eventsList.Items.Count > 0 Then ' if we have an event in the playlist
            If theEvent = -1 Then ' if we're not set to modify a specific event
                theEvent = eventsList.SelectedItems(0).Index ' set the event to the currently selected event
            End If

            eventsList.Items(theEvent).SubItems(2).Text = mainWindow.speedSlider.Value.ToString ' the new speed for this event
            eventsList.Items(theEvent).SubItems(4).Text = mainWindow.inTF.Text ' the new IN point for this event
            eventsList.Items(theEvent).SubItems(5).Text = mainWindow.outTF.Text ' the new OUT point for this event
            eventsList.Items(theEvent).SubItems(6).Text = NumberToTimeString(TimeStringToNumber(mainWindow.outTF.Text) - TimeStringToNumber(mainWindow.inTF.Text)) ' the new duration for this event

            setIsModified(1) ' we've modified the event
            tallyTotalList() ' re-tabulate the event list tally
        End If
    End Sub

    Private Sub mergeEvents()
        Dim newInTime As String = eventsList.SelectedItems(0).SubItems(4).Text ' the IN time from the first event is the new IN time
        Dim newOutTime As String = eventsList.SelectedItems(eventsList.SelectedItems.Count - 1).SubItems(5).Text ' the OUT time from the last event is the new OUT time
        Dim newDur As String = NumberToTimeString(TimeStringToNumber(newOutTime) - TimeStringToNumber(newInTime))

        Dim mergeEvents = MessageBox.Show("Do you want to merge these " & eventsList.SelectedItems.Count & " events together into one event?" & vbCrLf & vbCrLf &
                                        "The event's new IN time will be: " & newInTime & vbCrLf &
                                        "The event's new OUT time will be: " & newOutTime & vbCrLf &
                                        "The event's new duration will be: " & newDur,
                                              "Merge Events?",
                                              MessageBoxButtons.OKCancel,
                                              MessageBoxIcon.Question,
                                              MessageBoxDefaultButton.Button1,
                                              MessageBoxOptions.DefaultDesktopOnly)

        If mergeEvents = DialogResult.OK Then
            eventsList.BeginUpdate() ' stop updating the events list so we can do the next step without glitching the view out

            ' ----------------- TRY TO GET OLD INDEX FROM NAME (betweenTheLines()) - IF NOT, THEN JUST FALL BACK TO GETTING FROM EVENT # -----------------
            Dim newEventName As String = "Events ["
            newEventName += betweenTheLines(eventsList.SelectedItems(0).SubItems(1).Text, newEventName, "-", (CInt(eventsList.SelectedItems(0).SubItems(0).Text) - 100).ToString)
            newEventName += "-" & (CInt(eventsList.SelectedItems(eventsList.SelectedItems.Count - 1).SubItems(0).Text) - 100) & "]"

            ' ----------------- BUILD NEW EVENT FROM THE OTHER EVENTS -----------------
            Dim combinedEvent = returnListViewItem(newEventName, "100", "1", newInTime, newOutTime,
                                                   eventsList.SelectedItems(0).SubItems(7).Text,
                                                   eventsList.SelectedItems(eventsList.SelectedItems.Count - 1).SubItems(0).Text,
                                                   newDur)
            Dim newEventIdx As Integer = eventsList.SelectedItems(0).Index ' the index to insert the combined event into (get this before deleting below!)

            For a As Integer = eventsList.SelectedItems.Count - 1 To 0 Step -1
                eventsList.Items.RemoveAt(eventsList.SelectedItems.Item(a).Index) ' delete the old events from the events list
            Next

            eventsList.Items.Insert(newEventIdx, combinedEvent) ' insert the combined "new" event in the old event's place
            eventsList.Items(newEventIdx).BackColor = foundColor ' color it gruner

            eventsList.EndUpdate() ' start updating the events list again after we're done modifying it
            setIsModified(1)
        End If
    End Sub

    Private Sub highlightEvent(Optional theEvent As Integer = -1)
        For a As Integer = 0 To eventsList.Items.Count - 1
            eventsList.Items(a).Font = normalFont ' clear all the bold formatting from all events
        Next

        If theEvent <> -1 Then ' if we have a specific event to highlight (otherwise just clear all old highlights)
            eventsList.Items(theEvent).Font = boldFont ' change the currently playing event to bold text
            eventsList.Items(theEvent).EnsureVisible() ' make sure the event is visible on screen
        End If
    End Sub

    Public Sub checkAgainstCurrentPlayingEvent()
        If currentPlayingEvent <> -1 Then
            Dim currentPlayingEvent_In As String = eventsList.Items(currentPlayingEvent).SubItems(4).Text
            Dim currentPlayingEvent_Out As String = eventsList.Items(currentPlayingEvent).SubItems(5).Text

            If currentPlayingEvent_In = mainWindow.inTF.Text And currentPlayingEvent_Out = mainWindow.outTF.Text Then
                highlightEvent(currentPlayingEvent) ' if everything matches the IN and OUT of the current event, then keep the highlighitng
            Else
                highlightEvent() ' otherwise, if something doesn't mesh, then clear the highlighting
            End If
        End If
    End Sub

    Private Function loadEvent(ByVal selecteditem As Integer, Optional checkValid As Boolean = False) As Boolean
        If stilLLoading = False Then
            stilLLoading = True ' hold off on loading for a second, because we're trying to load something already

            currentPlayingEvent = selecteditem

            If mainWindow.loopModeButton.Text = "Shuffle Mode" Then
                actualPlayingEvent = mainWindow.randomNumberList(selecteditem) ' the actual event to load in Shuffle mode (relative to the current events list)
                selecteditem = actualPlayingEvent ' set the selectedItem to the value from ^^^
            End If

            If eventsList.Items(selecteditem).BackColor = errorColor Then ' and the event we're on has an error, then
                If checkValid = True Then ' if we're forcing a "good event check"
                    stilLLoading = False ' we're not going any fu(a)rther
                    Return False ' if we didn't successfully load an event, then return false
                Else ' we're not forcing the "good event check" (in other words, we clicked on the event directly)
                    Dim fileFound = findFile(selecteditem) ' find the file(s) you're missing, and re-connect any other paths that match that file

                    ' TODO: if we cancel on a series of files to find, we still don't do anything.  Maybe check to see if we're fixed here?
                    If fileFound = DialogResult.Cancel Then ' we cancelled finding a file, so don't do anything
                        If mainWindow.autoPlayDialogs = True Then mainWindow.SendMessage(CMD_SEND.CMD_PLAY) ' we cancelled finding an event, so keep on keepin' on
                        stilLLoading = False
                        Return False
                    End If
                End If
            End If

            highlightEvent(selecteditem) ' put currently playing event as bold

            mainWindow.loopsRepeatTF.Text = eventsList.Items(selecteditem).SubItems(3).Text ' how many repeats this loop has in Playlist mode

            mainWindow.inTF.Text = eventsList.Items(selecteditem).SubItems(4).Text
            mainWindow.outTF.Text = eventsList.Items(selecteditem).SubItems(5).Text

            If mainWindow.currentPlayingFile <> eventsList.Items(selecteditem).SubItems(7).Text Then ' we need to load another file
                If eventsList.Items(selecteditem).BackColor <> errorColor Then ' if there's no issue, then load the file
                    mainWindow.SendMessage(CMD_SEND.CMD_OPENFILE, eventsList.Items(selecteditem).SubItems(7).Text)
                    mainWindow.clearINOUTPoint = False

                    mainWindow.loadingEvent = True
                    Int32.TryParse(eventsList.Items(selecteditem).SubItems(2).Text, mainWindow.loadingEvent_Speed)
                Else
                    ' do we need to do anything here?
                End If
            Else ' we're still working with the same file
                mainWindow.SendMessage(CMD_SEND.CMD_SETPOSITION, CStr(TimeStringToNumber(eventsList.Items(selecteditem).SubItems(4).Text) - 0.5))

                Dim theSpeed As Integer
                Integer.TryParse(eventsList.Items(selecteditem).SubItems(2).Text, theSpeed)
                mainWindow.setSpeed(theSpeed)

                stilLLoading = False
            End If

            If pausePlaybackOnLoadEvent And eventsList.Items(selecteditem).BackColor <> errorColor Then
                mainWindow.SendMessage(CMD_SEND.CMD_PAUSE)
            End If

            If mainWindow.getMode() Then ' we're in OFF or Loop mode
                SetForegroundWindow(mainWindow.MPCHandle)
            End If
        End If

        Return True ' if we made it here, we were successful in loading a file
    End Function

    Public Sub loadPrevNextEvent(ByVal nextOrPrev As Integer)
        Dim numOfEvents As Integer = eventsList.Items.Count
        Dim successfullyLoaded As Boolean = False

        If numOfEvents > 1 Then ' if we have no events, or only one event, there's no reason to do anything!
            Dim missedEventTicks As Integer = 0

            While successfullyLoaded = False
                missedEventTicks += 1 ' increment the counter

                Dim nextEventToPlay = currentPlayingEvent + nextOrPrev
                nextEventToPlay = returnEventBounds(nextEventToPlay)

                If mainWindow.loopModeButton.Text = "Playlist Mode" Then ' find out if the next item we have is selected - if it isn't, then skip it
                    If eventsList.SelectedItems.Count > 1 Then
                        While True
                            For a As Integer = 0 To eventsList.SelectedItems.Count - 1
                                If nextEventToPlay = eventsList.SelectedItems(a).Index Then
                                    Exit While
                                Else
                                    ' Do nothing, because we haven't found anything
                                End If
                            Next

                            If nextOrPrev = 1 Then
                                nextEventToPlay = returnEventBounds(nextEventToPlay + 1)
                            Else
                                nextEventToPlay = returnEventBounds(nextEventToPlay - 1)
                            End If

                        End While
                    End If
                End If

                successfullyLoaded = loadEvent(nextEventToPlay, True) ' check to see if we loaded an event successfully or not, otherwise go to the next one

                If missedEventTicks = eventsList.Items.Count Then ' if we've tried to load every single event and failed, then quit out of this attempt
                    Exit While
                End If
            End While
        End If
    End Sub

    Private Function returnEventBounds(ByVal nextEventToPlay As Integer) As Integer
        If nextEventToPlay < 0 Then
            nextEventToPlay = eventsList.Items.Count - 1
        ElseIf nextEventToPlay = eventsList.Items.Count Then
            nextEventToPlay = 0
        End If

        Return nextEventToPlay
    End Function

    Private Sub clearEventsList()
        eventsList.Items.Clear() ' delete all the items in the events list 
        currentLooperFile = Nothing ' we don't have a looper file loaded now
        setIsModified(0) ' and we're not modified

        totalNumberOfEvents = 100 ' start the incrementing timer back to 100
        currentPlayingEvent = -1 ' we're not currently playing an event

        tallyTotalList() ' re-tabulate the event list tally

        deleteEventButton.Enabled = False
        modifyEventButton.Enabled = False
        clearEventsButton.Enabled = False
        saveButton.Enabled = False
    End Sub

    Private Sub tallyTotalList()
        Dim countOfEvents As Integer = eventsList.Items.Count

        If countOfEvents > 0 Then
            Dim timeTally As Double = 0.0

            For a As Integer = 0 To countOfEvents - 1
                timeTally += TimeStringToNumber(eventsList.Items(a).SubItems(6).Text) ' add all the (known) durations together
            Next

            Dim columnTitle As String

            If inSearchMode = True Then
                columnTitle = "Search Results"
            Else
                columnTitle = "Event"
            End If

            If countOfEvents = 1 Then
                eventsList.Columns(1).Text = columnTitle & " (" & countOfEvents & " event, " & NumberToTimeString(timeTally) & " total)"
            Else
                eventsList.Columns(1).Text = columnTitle & " (" & countOfEvents & " events, " & NumberToTimeString(timeTally) & " total)"
            End If
        Else
            eventsList.Columns(1).Text = "Event (0 events, 0:00.000 total)" ' if we don't have any events, then show that
        End If
    End Sub

    ' ======================================================================================
    ' ================= BUTTON / CLICK HANDLERS ============================================
    ' ======================================================================================

    Private Sub prevEventButton_Click(sender As Object, e As EventArgs) Handles prevEventButton.Click
        loadPrevNextEvent(-1)
    End Sub

    Private Sub saveButton_Click(sender As Object, e As EventArgs) Handles saveButton.Click
        If isModified = True Then
            saveLooperFile()
        Else 'if we're not in a modified state, but still click the Save button, if you have something selected (which you'd have to), it'll save a subset of events
            If eventsList.SelectedItems.Count > 0 Then
                menu_Save.Show(MousePosition.X - 20, MousePosition.Y - 10) ' show the context menu to save a subset
            End If
        End If
    End Sub

    Private Sub loadButton_Click(sender As Object, e As EventArgs) Handles loadButton.Click
        loadLooperFile()
    End Sub

    Private Sub deleteEventButton_Click(sender As Object, e As EventArgs) Handles deleteEventButton.Click
        deleteEvents()
    End Sub

    Private Sub modifyEventButton_Click(sender As Object, e As EventArgs) Handles modifyEventButton.Click
        If modifyEventButton.Text = "MODIFY EVENT" Then
            modifyEvent()
        ElseIf modifyEventButton.Text = "MERGE INTO 1" Then
            mergeEvents()
        End If
    End Sub

    Private Sub addEventButton_Click(sender As Object, e As EventArgs) Handles addEventButton.Click
        addEvent()
    End Sub

    Private Sub clearEventsButton_Click(sender As Object, e As EventArgs) Handles clearEventsButton.Click
        clearEventsList()
    End Sub

    Private Sub nextEventButton_Click(sender As Object, e As EventArgs) Handles nextEventButton.Click
        loadPrevNextEvent(1)
    End Sub

    ' ======================================================================================
    ' ================== CONTEXT MENU FUNCTIONS ============================================
    ' ======================================================================================

    Private Sub menu_load_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles menu_load.Opening
        If eventsList.Items.Count = 0 Then
            menuItem_importLooperFile.Enabled = False
        Else
            menuItem_importLooperFile.Enabled = True
        End If
    End Sub

    Private Sub menuItem_importLooperFile_Click(sender As Object, e As EventArgs) Handles menuItem_importLooperFile.Click
        loadLooperFile(False)
    End Sub

    Private Sub menuItem_AddAtEnd_Click(sender As Object, e As EventArgs) Handles menuItem_AddAtEnd.Click
        menuItem_AddAtEnd.Checked = True
        menuItem_InsertEvents.Checked = False

        addEventButton.Text = "ADD EVENT"

        insertItem = False
    End Sub

    Private Sub menuItem_InsertEvents_Click(sender As Object, e As EventArgs) Handles menuItem_InsertEvents.Click
        menuItem_AddAtEnd.Checked = False
        menuItem_InsertEvents.Checked = True

        addEventButton.Text = "INS. EVENT"

        insertItem = True
    End Sub

    Private Sub menu_Save_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles menu_Save.Opening
        If eventsList.SelectedIndices.Count > 0 Then
            menuItem_SaveSelection.Enabled = True
        Else
            menuItem_SaveSelection.Enabled = False
        End If
    End Sub

    Private Sub menuItem_SaveSelection_Click(sender As Object, e As EventArgs) Handles menuItem_SaveSelection.Click
        saveLooperFile(True) ' we're only saving a few events, so set this to True to force that
    End Sub


    ' ======================================================================================
    ' ================== EVENT SEARCHING FUNCTIONS =========================================
    ' ======================================================================================

    Private Sub doSearchButton_Click(sender As Object, e As EventArgs) Handles doSearchButton.Click
        doSearch()
    End Sub

    Private Sub cancelSearchButton_Click(sender As Object, e As EventArgs) Handles cancelSearchButton.Click
        restoreFromSearch()
    End Sub

    Private Sub searchTF_KeyDown(sender As Object, e As KeyEventArgs) Handles searchTF.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Return Then
            doSearch()
            eventsList.Focus() ' make the event list the focused item to restore hotkeys/take selection off of the text box
            e.SuppressKeyPress = True ' stop the ding after hitting these buttons
        End If
    End Sub

    Private Sub searchTF_Enter(sender As Object, e As EventArgs) Handles searchTF.Enter
        mainWindow.clearHotKeys()
        mainWindow.hotkeysActive = True 'tell the hotkey thread that hotkeys are still active, so it doesn't try to clear them (this is for text entry ONLY)
    End Sub

    Private Sub searchTF_Leave(sender As Object, e As EventArgs) Handles searchTF.Leave
        mainWindow.setHotKeys()
    End Sub

    Private Sub doSearch()
        If searchTF.Text <> Nothing And eventsList.Items.Count > 0 Then
            eventsList.SelectedIndices.Clear()

            If UBound(eventsListArray) = 0 Then ' if the master array hasn't been created yet, then create it...
                ReDim eventsListArray(eventsList.Items.Count, 8)

                For a As Integer = 0 To eventsList.Items.Count - 1
                    For b As Integer = 0 To 7
                        eventsListArray(a, b) = eventsList.Items(a).SubItems(b).Text ' get the events into an array
                    Next

                    eventsListArray(a, 8) = eventsList.Items(a).BackColor
                Next
            End If

            Dim foundIndex As New List(Of Integer)

            For a As Integer = 0 To eventsList.Items.Count - 1 ' check the entire list view for the search string
                If Not UCase(eventsList.Items(a).SubItems(1).Text).Contains(UCase(searchTF.Text)) Then
                    foundIndex.Add(a)
                End If
            Next

            If foundIndex.Count <> eventsList.Items.Count Then ' if we found something, then we need to change the list view
                If currentPlayingEvent > -1 Then
                    Integer.TryParse(eventsList.Items(currentPlayingEvent).SubItems(0).Text, currentSearchPlayingEvent) ' the ID number of the current playing event
                End If

                currentPlayingEvent = -1 ' set this to -1 initially to jump to the beginning of the list if the search doesn't contain ^^^

                eventsList.BeginUpdate() ' stop updating the events list so we can do the next step without glitching the view out

                For a As Integer = foundIndex.Count - 1 To 0 Step -1 ' Delete any items in the list that *don't* contain our search phrase
                    eventsList.Items.RemoveAt(foundIndex(a))
                Next

                eventsList.EndUpdate() ' start updating the events list again after we're done modifying it
                cancelSearchButton.Enabled = True

                For a As Integer = 0 To eventsList.Items.Count - 1 ' find the current playing event in the new results
                    If eventsList.Items(a).SubItems(0).Text = CStr(currentSearchPlayingEvent) Then
                        currentPlayingEvent = a ' set the current playing event to the position in the current playlist we're in
                    End If
                Next

                inSearchMode = True
                tallyTotalList() ' re-tabulate the event list tally

                If mainWindow.loopModeButton.Text = "Shuffle Mode" Then
                    mainWindow.switchToShuffleMode() ' re-initialize the random number list and re-shuffle (to account for the new listing)
                End If
            Else
                MessageBox.Show("No events matched your search:" & vbCrLf & " " & searchTF.Text,
                                "No Results Found",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub

    Private Sub restoreFromSearch()
        If UBound(eventsListArray) <> 0 Then ' restore the original events list from the master array
            If currentPlayingEvent > -1 Then
                Integer.TryParse(eventsList.Items(currentPlayingEvent).SubItems(0).Text, currentSearchPlayingEvent) ' the ID number of the current playing event (this time, from search mdoe)
            End If

            eventsList.BeginUpdate() ' stop updating the events list so we can do the next step without glitching the view out
            eventsList.Items.Clear()

            For a As Integer = 0 To UBound(eventsListArray) - 1
                Dim newItemArray(8) As String

                For b As Integer = 0 To 7
                    newItemArray(b) = CStr(eventsListArray(a, b)) ' re-add all of the original events back to the events list
                Next

                Dim newEvent = New ListViewItem(newItemArray)

                eventsList.Items.Add(newEvent)
                eventsList.Items(eventsList.Items.Count - 1).BackColor = CType(eventsListArray(a, 8), Color) ' the last element of the master array is the original back color
            Next

            eventsList.EndUpdate() ' start updating the events list again after we're done modifying it
            cancelSearchButton.Enabled = False ' disable the "clear search" button, because we're already cleared

            For a As Integer = 0 To eventsList.Items.Count - 1 ' find the current playing event in the new results
                If eventsList.Items(a).SubItems(0).Text = CStr(currentSearchPlayingEvent) Then
                    currentPlayingEvent = a ' set the current playing event to the position in the current playlist we're in
                    checkAgainstCurrentPlayingEvent() ' re-highlight the current playing event (if the IN and OUT points match)
                    Exit For
                End If
            Next

            inSearchMode = False
            tallyTotalList() ' re-tabulate the event list tally

            If mainWindow.loopModeButton.Text = "Shuffle Mode" Then
                mainWindow.switchToShuffleMode() ' re-initialize the random number list and re-shuffle (to account for the new listing)
            End If

            ReDim eventsListArray(0, 0) ' delete the master array to clear memory and get it ready for the next search
        End If
    End Sub

    ' ======================================================================================
    ' ================== EVENT NAME EDITING FUNCTIONS ======================================
    ' ======================================================================================

    Private Sub eventsList_MouseDown(sender As Object, e As MouseEventArgs) Handles eventsList.MouseDown
        HideTextEditor()
    End Sub

    Private Sub listViewEditor_Leave(sender As Object, e As EventArgs) Handles listViewEditor.Leave
        HideTextEditor()
    End Sub

    Private Sub HideTextEditor()
        listViewEditor.Visible = False

        If Not (SelectedLSI Is Nothing) Then
            SelectedLSI.Text = listViewEditor.Text
            setIsModified(1) ' if we changed the value of one of the event list's names or loops, then mark it as being modified
        End If

        If mainWindow.autoPlayDialogs = True Then mainWindow.SendMessage(CMD_SEND.CMD_PLAY) ' we cancelled or finished editing text, so resume playback

        SelectedLSI = Nothing
        listViewEditor.Text = Nothing

        mainWindow.setHotKeys()
    End Sub

    Private Sub listViewEditor_KeyDown(sender As Object, e As KeyEventArgs) Handles listViewEditor.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Return Or e.KeyCode = Keys.Escape Then
            e.SuppressKeyPress = True ' stop the ding after hitting these buttons

            If e.KeyCode = Keys.Escape Then
                SelectedLSI = Nothing ' clear the selection, so the edit doesn't take effect
            End If

            HideTextEditor()
            SetForegroundWindow(mainWindow.MPCHandle) ' Make MPC-HC the foreground window
        End If
    End Sub

    Private Sub eventsList_KeyDown(sender As Object, e As KeyEventArgs) Handles eventsList.KeyDown
        If eventsList.Items.Count > 0 Then
            If eventsList.SelectedItems.Count <> 0 Then
                Dim selectedRow = eventsList.SelectedItems(0).Index

                If e.KeyCode = Keys.F2 Then
                    editListViewItem(selectedRow)
                ElseIf e.KeyCode = Keys.F3 Then
                    editListViewItem(selectedRow, 3)
                End If
            End If
        End If
    End Sub

    Private Sub editListViewItem(ByVal selectedItem As Integer, Optional selectedSubItem As Integer = 1)
        mainWindow.SendMessage(CMD_SEND.CMD_PAUSE) ' pause the player when editing text

        If Me.Visible = False Then
            mainWindow.openPlaylistWindow()
        End If

        Me.Activate() ' make the playlist window frontmost

        mainWindow.clearHotKeys()
        mainWindow.hotkeysActive = True 'tell the hotkey thread that hotkeys are still active, so it doesn't try to clear them (this is for text entry ONLY)

        SelectedLSI = eventsList.Items(selectedItem).SubItems(selectedSubItem)

        Dim cellWidth = SelectedLSI.Bounds.Width
        Dim cellHeight = SelectedLSI.Bounds.Height
        Dim cellLeft = eventsList.Left + SelectedLSI.Bounds.Left
        Dim cellTop = eventsList.Top + SelectedLSI.Bounds.Top

        listViewEditor.Location = New Point(cellLeft, cellTop)
        listViewEditor.Size = New Size(cellWidth, cellHeight)
        listViewEditor.Visible = True
        listViewEditor.BringToFront()
        listViewEditor.Text = SelectedLSI.Text
        listViewEditor.Select()
        listViewEditor.SelectAll()
    End Sub

    ' ======================================================================================
    ' ================== DRAG AND DROP AND SORTING FUNCTIONS ===============================
    ' ======================================================================================

    Private Sub eventsList_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles eventsList.DragDrop
        eventsList.ListViewItemSorter = Nothing ' Clear custom sorting to get the drag operation to work

        'Returns the location of the mouse pointer in the ListView control.
        Dim p As Point = eventsList.PointToClient(New Point(e.X, e.Y))

        'Obtain the item that is located at the specified location of the mouse pointer.
        Dim dragToItem As ListViewItem = eventsList.GetItemAt(p.X, p.Y)
        Dim dragIndex As Integer

        If dragToItem IsNot Nothing Then
            'Obtain the index of the item at the mouse pointer.
            dragIndex = dragToItem.Index
        Else
            'Set the index to zero to put it at the very top
            dragIndex = 0
        End If

        If e.Data.GetDataPresent("System.Windows.Forms.ListView+SelectedListViewItemCollection") Then ' we're dragging events around in the listview
            'Return if the items are not selected in the ListView control.
            If eventsList.SelectedItems.Count = 0 Then Return
            Dim i As Integer
            Dim sel(eventsList.SelectedItems.Count) As ListViewItem
            For i = 0 To eventsList.SelectedItems.Count - 1
                sel(i) = eventsList.SelectedItems.Item(i)
            Next

            For i = 0 To eventsList.SelectedItems.Count - 1
                'Obtain the ListViewItem to be dragged to the target location.
                Dim dragItem As ListViewItem = sel(i)

                Dim itemIndex As Integer = dragIndex
                If itemIndex = dragItem.Index Then Return
                If dragItem.Index < itemIndex Then
                    itemIndex += 1
                Else
                    itemIndex = dragIndex + i
                End If
                'Insert the item in the specified location.
                Dim inserteditem As ListViewItem = CType(dragItem.Clone, ListViewItem)

                eventsList.Items.Insert(itemIndex, inserteditem)
                'Removes the item from the initial location while 
                'the item is moved to the new location.
                eventsList.Items.Remove(dragItem)
            Next

            setIsModified(1) ' if we're not already set as isModified, then do it here
        Else ' we dragged something on to the listview that ISN'T a listview item (like a file from Explorer)
            Dim dropFiles() As String = CType(e.Data.GetData("FileDrop", True), String()) ' get the list of files dropped

            If dropFiles.Count > 1 Then
                Array.Sort(dropFiles) ' sort the array of files alphabetically
            End If

            Dim currentInsertedItem As Integer = 0 ' keep track of which item we're currently adding

            For a As Integer = 0 To dropFiles.Count - 1
                If {".mov", ".avi", ".mp4", ".mkv", ".mts", ".m2ts",
                    ".m2t", ".qt", ".ts", ".qt", ".flv", ".wav", ".mp3",
                    ".ogg", ".aif", ".aiff"}.Contains(LCase(My.Computer.FileSystem.GetFileInfo(dropFiles(a)).Extension)) Then

                    Dim draggedEvent = returnListViewItem(My.Computer.FileSystem.GetFileInfo(dropFiles(a)).Name, "100", "1", "0:00", Nothing, dropFiles(a))

                    If insertItem = False Then
                        eventsList.Items.Add(draggedEvent)
                    Else
                        eventsList.Items.Insert(dragIndex + currentInsertedItem, draggedEvent)
                        currentInsertedItem += 1
                    End If

                    totalNumberOfEvents += 1
                    setIsModified(1) ' if we're not already set as isModified, then do it here
                ElseIf My.Computer.FileSystem.GetFileInfo(dropFiles(a)).Extension = ".looper" Then ' we've dragged a looper file into the playlist
                    If insertItem = False Then ' if we're not set to insert .looper events into the current playlist, then just open a new file
                        loadLooperFile(True, dropFiles(a))
                        Exit For ' exit the loop, skipping the rest of the files (this method only opens one file, not a bunch like the above...)
                    Else ' if we're set to insert .looper events
                        If eventsList.Items.Count > 0 Then ' if there's actually something already in the playlist
                            eventsList.SelectedIndices.Clear()

                            If dragIndex <> 0 Then
                                eventsList.SelectedIndices.Add(dragIndex - 1)
                            Else
                                eventsList.SelectedIndices.Add(eventsList.Items.Count - 1)
                            End If

                            loadLooperFile(False, dropFiles(a)) ' load the new looper at the end of the playlist
                        Else ' if there's nothing in the playlist, then just open the one file like the above, and skip the other files
                            loadLooperFile(True, dropFiles(a))
                            Exit For
                        End If
                        ' We can't do anything with this kind of file
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub eventsList_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles eventsList.DragEnter
        If e.Data.GetDataPresent("System.Windows.Forms.ListView+SelectedListViewItemCollection") Then ' we're dragging a listview item to another location in the playlist
            e.Effect = DragDropEffects.Move
        ElseIf e.Data.GetDataPresent(DataFormats.FileDrop) Then ' we're dragging a file to the playlist from Explorer
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub eventsList_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles eventsList.ItemDrag
        eventsList.DoDragDrop(eventsList.SelectedItems, DragDropEffects.Move)
    End Sub

    Private Sub eventsList_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles eventsList.ColumnClick
        If eventsList.Items.Count > 0 Then ' if we have more than one event in the playlist... otherwise do nothing!
            Dim iSortOrder As SortOrder = CType(eventsList.Columns(e.Column).Tag, SortOrder)
            Dim lvcs As New ListViewColumnSorter
            If iSortOrder = SortOrder.Ascending Then
                eventsList.Columns(e.Column).Tag = SortOrder.Descending
                lvcs.SortingOrder = SortOrder.Descending
            Else
                eventsList.Columns(e.Column).Tag = SortOrder.Ascending
                lvcs.SortingOrder = SortOrder.Ascending
            End If
            lvcs.ColumnIndex = e.Column
            eventsList.ListViewItemSorter = lvcs

            setIsModified(1) ' if we re-sorted the events list, then mark it as being modified
        End If
    End Sub
End Class
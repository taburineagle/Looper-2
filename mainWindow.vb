Imports System.Threading
Imports System.Runtime.InteropServices

' MPC-HC Looper VB - Looper re-written in Visual Basic
' © 2014-2021 Zach Glenwright / Gull's Wing Media Productions
' http://www.gullswingmedia.com

' ======================================================================================
' ================= THINGS TO DO =======================================================
' ======================================================================================
' ----------------- MOST IMPORTANT: -----------------
'
' ----------------- THINGS TO LOOK INTO: -----------------
' - Figure out some kind of speed offset for setting the IN point, based on the current speed (slower = less offset)
' - Make "autosave looper" a preference, and also "keep original .looper file" backup stuff
' - See if Pinging a server will speed things up for loading a list (so inactive servers won't slow the thing down)
' - Write a Trimmer sub-window for Looper that lets you trim .looper files down

Public Class mainWindow
    Public newEventString As String = "New Loop Event" ' the name for new events (by default, just "New Loop Event" as it was in older versions of Looper)

    Dim currentPosition As Double ' the current position of MPC-HC
    Public currentDuration As Double ' the current duration of the file in MPC-HC
    Public currentPlayingFile As String ' the current file playing in MPC-HC

    Public clearINOUTPoint As Boolean = True ' whether or not to clear IN and OUT when loading a new file

    Dim currentOrRemaining As Boolean ' whether or not we're looking at current or remaining time

    ReadOnly RNG As New Random()
    Public randomNumberList(0) As Integer ' array of randomized item events

    ' INI file values stored for later use
    Private inPointOffset As Double = 0 ' how much more to add to an IN point to more fine-tune the point
    Private outPointOffset As Double = 0.15 ' how much more to take off of an OUT point to properly loop it (without skipping to the beginning)

    Public loopSlipLength As Double = 0.5 ' how long a loop slips when you click the slip buttons
    Public loopPreviewLength As Double = 0.25 ' how long a loop previews for before going back to the loop
    Public autoPlayDialogs As Boolean = True ' whether to start playing as soon as dialog boxes close (text editing maybe?)

    Public autoloadLastLooper As String ' whether to not to automatically load the last looper from close
    Public autoplayFirstEvent As Boolean = True ' set this to True to allow auto-playing the first event when opening a .looper file
    Public dontForceLooperModeonOpen As Boolean = False ' whether to set Loop in Looper Mode after opening .looper file

    ' Threads for background work
    Public posThread, frontThread As Thread ' threads to check on MPC-HC position and hotkeys

    ' Handles to... handle inside of the threads above ^^
    Public MPCHandle As IntPtr ' MPC-HC's hWnd
    Private activeHwnd As IntPtr ' the currently active window
    Private myHandle, plHandle As IntPtr ' the current hWnd for me, and for the playlist

    ' Working with hotkeys
    Private disableHotkeys As Boolean = False ' whether or not to disable hotkeys
    Public hotkeysActive As Boolean ' whether hotkeys are active currently or not

    Public dockPlaylistWindow As Boolean = False ' whether or not to dock the playlist to the main window (if it's sized differerntly, then no...)
    Public isQuitting As Boolean = False ' if we are in the process of quitting or not

    Dim waitForSecondTick As Integer = -1 ' wait for the 2nd response from MPC-HC to change the speed

    Public loadingEvent As Boolean = False ' if we're in the process of loading an event (for the NOWPLAYING function)
    Public loadingEvent_Speed As Integer = 100

    Private slipAction As Integer = 0 ' the current slip action to undertake - 1/2 - slip IN left/right 3/4 - slip OUT left/right

    ' ======================================================================================
    ' ================= DLL CALLS AND STRUCTURES ===========================================
    ' ======================================================================================

    Public Structure CopyData
        Public dwData As IntPtr
        Public cbData As Integer
        Public lpData As IntPtr
    End Structure

    ' Returns the current foreground window (to check to see whether hotkeys should be enabled or not)
    Private Declare Function GetForegroundWindow Lib "user32" () As IntPtr

    ' Registers a hotkey with the system
    Private Declare Function RegisterHotKey Lib "user32" _
        (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer

    ' Unregisters a hotkey with the system
    Private Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer

    ' Sends a WM_COPYDATA request to MPC-HC to allow controlling the player or to another Looper instance (if we're running a single-instance-only session)
    Private Declare Auto Function SendMessageA Lib "user32" _
        (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As IntPtr, ByRef lParam As CopyData) As Boolean

    ' Find a window with a specific title (for finding old Looper instances for the above ^^^ call to the other instance)
    Private Declare Auto Function FindWindow Lib "user32" _
        (ByVal ClassName As String, ByVal WindowTitle As String) As IntPtr

    Const WM_HOTKEY As Integer = &H312 ' if the message received is a hotkey message
    Const WM_COPYDATA As Integer = &H4A ' if the message received is a WM_COPYDATA message

    Public Function SendMessage(ByVal cmd As Integer, Optional msg As String = "", Optional customHwnd As Boolean = False, Optional toHwnd As IntPtr = Nothing) As Boolean
        Dim DataStruct As New CopyData With {
            .dwData = CType(cmd, IntPtr),
            .cbData = (msg.Length + 1) * Marshal.SystemDefaultCharSize,
            .lpData = Marshal.StringToHGlobalUni(msg)
        }

        Dim returnValue As Boolean

        If customHwnd = True Then
            returnValue = SendMessageA(toHwnd, WM_COPYDATA, myHandle, DataStruct) ' SendMessage to a custom window (another Looper instance)
        Else
            returnValue = SendMessageA(MPCHandle, WM_COPYDATA, myHandle, DataStruct) ' SendMessage to MPC-HC
        End If

        Marshal.FreeHGlobal(DataStruct.lpData)

        DataStruct = Nothing
        Return returnValue
    End Function

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case m.Msg
            Case WM_HOTKEY ' if the message received is a hotkey event
                handleHotKeyEvent(m.WParam.ToInt32)
            Case WM_COPYDATA ' if the message received is from MPC-HC or another Looper session
                Dim CopyDataStruct As CopyData = DirectCast(Marshal.PtrToStructure(m.LParam, GetType(CopyData)), CopyData)
                Dim theMessage As String = Marshal.PtrToStringUni(CopyDataStruct.lpData)

                Select Case CopyDataStruct.dwData
                    Case CType(&H5000F634, IntPtr) ' recieving a message from another Looper instance (so we don't need a temp file like we've been doing)
                        ' TODO (maybe later) - better define this message so we can send other commands - right now, it's just "open file", so we take it at face value
                        ' i.e.: Send it as a "|" delimited string, and - Dim looperControlArray() as String = Split(theMessage, "|")

                        loadLooperFile(theMessage)
                    Case CType(CMD_RECEIVED.CMD_CONNECT, IntPtr) ' received when MPC-HC connects to Looper for the first time
                        MPCHandle = m.WParam ' the current hWnd of the current MPC-HC instance

                        clearInPoint()
                        clearOutPoint()

                        playlistWindow.SetForegroundWindow(MPCHandle) ' make MPC-HC the foreground window

                        If My.Application.CommandLineArgs.Count > 0 Then ' if we gave Looper a .looper file to open, then open it
                            Dim pathToOpen = My.Application.CommandLineArgs(0)

                            If Strings.InStr(pathToOpen, ".looper") <> 0 Then ' if the file we passed to Looper has a .looper extension, then open it as a Looper file
                                loadLooperFile(pathToOpen)
                            End If
                        Else ' if we didn't, but we're set up to open the last file from the INI file, then open that...
                            If autoloadLastLooper <> Nothing Then
                                Dim autoloadArray() As String = Split(autoloadLastLooper, "|")

                                If autoloadArray(0) <> Nothing Then ' if there's a filename in the array instead of Nothing
                                    If autoloadArray(1) <> "-1" Then ' and there's an event number that's not -1
                                        loadLooperFile(autoloadArray(0), CInt(autoloadArray(1))) ' load the looper file, at the event specified
                                    Else
                                        ' TODO: check if we actually need the -1 check - I don't think there's a save function that saves -1
                                        loadLooperFile(autoloadArray(0)) ' load the looper file at the beginning
                                    End If
                                End If
                            End If
                        End If
                    Case CType(CMD_RECEIVED.CMD_CURRENTPOSITION, IntPtr) ' received when MPC-HC gets a request to update the current media position
                        Double.TryParse(theMessage, currentPosition)
                        currentPosition += 0.5 ' the position MPC-HC returns is actually -0.5 (half a second) behind what the actual position is

                        If currentOrRemaining = False Then ' show the current position
                            If currentPositionTF.Text <> NumberToTimeString(currentPosition) Then
                                currentPositionTF.Text = NumberToTimeString(currentPosition)
                            End If
                        Else ' show the time remaining in the current event/loop
                            If TimeStringToNumber(outTF.Text) - currentPosition > 0 Then
                                If currentPositionTF.Text <> "-" & NumberToTimeString(TimeStringToNumber(outTF.Text) - currentPosition) Then
                                    currentPositionTF.Text = "-" & NumberToTimeString(TimeStringToNumber(outTF.Text) - currentPosition)
                                End If
                            Else
                                If currentPositionTF.Text <> "-0:00.000" Then
                                    currentPositionTF.Text = "-0:00.000"
                                End If
                            End If
                        End If

                        ' ----------------- DO THE LOOP DO THE LOOP DO THE LOOP! -----------------
                        Select Case loopModeButton.Text
                            Case "Loop Mode" ' if we're in LOOP MODE
                                If Not {outTF.Text, inTF.Text}.Contains(Nothing) Then ' if both the IN and OUT point have values
                                    If currentPosition > TimeStringToNumber(outTF.Text) Then ' and the current position reported is past the value in the OUT point
                                        SendMessage(CMD_SEND.CMD_SETPOSITION, CStr(TimeStringToNumber(inTF.Text) - 0.5)) ' move MPC-HC back to the IN point
                                    End If
                                End If
                            Case "Playlist Mode", "Shuffle Mode" ' if we're in PLAYLIST or SHUFFLE modes
                                If Not {outTF.Text, inTF.Text}.Contains(Nothing) Then
                                    If outTF.Text <> Nothing Then ' if the OUT point field has a value set in it
                                        If currentPosition > TimeStringToNumber(outTF.Text) Then
                                            Dim repeatCount As Double
                                            Double.TryParse(loopsRepeatTF.Text, repeatCount)

                                            If repeatCount <> 1 Then ' if we still have more loop repetitions to do, then jump back and decrement that counter
                                                SendMessage(CMD_SEND.CMD_SETPOSITION, CStr(TimeStringToNumber(inTF.Text) - 0.5))
                                                loopsRepeatTF.Text = CStr(repeatCount - 1)
                                            Else ' we've reached the last loop repeat, so jump to the next event
                                                If playlistWindow.eventsList.Items.Count > 1 Then
                                                    playlistWindow.loadPrevNextEvent(1) ' load the next event
                                                Else ' if we have only one event, then we're basically in loop mode, so just treat it that way!
                                                    SendMessage(CMD_SEND.CMD_SETPOSITION, CStr(TimeStringToNumber(inTF.Text) - 0.5))
                                                End If
                                            End If
                                        End If
                                    Else ' if the OUT point has no value set in it, then...
                                        SendMessage(CMD_SEND.CMD_GETNOWPLAYING) ' force updating the NOWPLAYING info to get the duration
                                        outTF.Text = NumberToTimeString(currentDuration + 0.5 - outPointOffset) ' set the OUT point to the duration
                                        playlistWindow.modifyEvent(playlistWindow.currentPlayingEvent) ' and modify the current event to reflect that duration
                                    End If
                                End If
                        End Select
                    Case CType(CMD_RECEIVED.CMD_NOWPLAYING, IntPtr) ' received when MPC-HC gets a request to update the current file + duration info
                        Dim nowInfoArray() As String = Split(theMessage, "|")

                        If currentPlayingFile <> nowInfoArray(UBound(nowInfoArray) - 1) Then ' if the filenames don't match, then change the current information
                            currentPlayingFile = nowInfoArray(UBound(nowInfoArray) - 1)
                            Double.TryParse(nowInfoArray(UBound(nowInfoArray)), currentDuration)
                        End If

                        ' if we're loading an event from a file, and it first recieved the NOWPLAYING message, then set the event up NOW
                        If loadingEvent = True Then
                            SendMessage(CMD_SEND.CMD_SETPOSITION, CStr(TimeStringToNumber(inTF.Text) - 0.5))
                            setSpeed(loadingEvent_Speed)

                            If pausePlaybackOnLoadEvent Then
                                SendMessage(CMD_SEND.CMD_PAUSE) ' pause the playback if you have the preference set to do that
                            End If

                            ' if the OUT point is set as "0" or "0:00" - the Cstr() function sets that to 0 - it gets the current duration (this is
                            ' for files that are added to the playlist by dragging and dropping them on to it.)
                            If CStr(TimeStringToNumber(outTF.Text)) = "0" Then
                                outTF.Text = NumberToTimeString(currentDuration + 0.5 - outPointOffset)
                                playlistWindow.modifyEvent(playlistWindow.currentPlayingEvent)
                            End If

                            ' if we're not in OFF or LOOP modes, then make MPC-HC the foreground window to make it easier to do MPC-HC things
                            ' (like pausing once the playback starts, making larger, etc.)
                            If getMode() Then
                                playlistWindow.SetForegroundWindow(MPCHandle)
                            End If

                            ' re-initialize these values to their defaults
                            loadingEvent = False
                            loadingEvent_Speed = 100

                            playlistWindow.stilLLoading = False
                        Else ' we're not loading an event
                            If waitForSecondTick = 0 Then ' if MPC-HC loaded the file, we want to wait for 2 responses from this message before checking speed
                                waitForSecondTick = 1 ' skip this time around and wait for the next one
                            ElseIf waitForSecondTick = 1 Then ' OK, we're there, now check the speed
                                If speedSlider.Value <> 100 Then
                                    setSpeed(100)
                                End If

                                waitForSecondTick = -1 ' reset the variable so we're not checking it again (until the next time we need to)
                            End If
                        End If

                        nowInfoArray = Nothing ' unset the variable to clear it from memory
                    Case CType(CMD_RECEIVED.CMD_STATE, IntPtr)
                        If Convert.ToInt32(theMessage) = LOADSTATE.MLS_LOADED Then ' received when MPC-HC "finishes" loading a file
                            If clearINOUTPoint = True Then ' re-initialize everything to defaults
                                If loadingEvent = False Then waitForSecondTick = 0 ' check the speed to see if we need to change it... the next time around
                                clearInPoint()
                                clearOutPoint()
                            Else
                                clearINOUTPoint = True ' don't do anything, but turn this back off, so the next file (if it's skipped in MPC-HC) does the above ^^^
                            End If
                        End If
                    Case CType(CMD_RECEIVED.CMD_DISCONNECT, IntPtr) ' received when MPC-HC quits on its own - we can't come back from this, so see if we want to save
                        playlistWindow.unInitialize(True)
                        Me.Close()
                End Select

                CopyDataStruct = Nothing ' uninitialize the variable
                theMessage = Nothing ' uninitialize the variable
        End Select

        MyBase.WndProc(m) ' do anything else that needs to be done
    End Sub

    ' ======================================================================================
    ' ================= FORM FUNCTIONS =====================================================
    ' ======================================================================================

    Private Sub mainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide() ' hide the window on boot, so moving it to its final destination is done in the background
        Me.Icon = My.Resources.icon ' Set the Form icon to the main program icon
        Me.MaximizeBox = False ' Disable the Maximize box

        If My.Computer.Keyboard.CtrlKeyDown = True Then ' re-initialize Looper from the beginning
            findLooperWindow.ShowDialog() ' show the "first launch" dialog to find MPC-HC's executable file
        End If

        If My.Computer.Keyboard.ShiftKeyDown = True Then ' load the defaults (Loop Mode, slip/preview, window positions, etc.) for Looper
            loadINIFileForDefaults() ' load only the MPC-HC values, but the rest are default
            openPlaylistWindow() ' open the playlist window by default
            changeAlwaysOnTop() ' change Always on Top to be true, so Looper sits on top of everything else
        Else ' load Looper like it normally would (this is where we'll USUALLY be)
            loadINIFile() ' load the values from the INI file, and set Looper up from those
        End If

        ' Place the current position counter above the background, on the left
        currentPositionTF.Parent = currentPositionBG
        currentPositionTF.BackColor = Color.Transparent
        currentPositionTF.BringToFront()
        currentPositionTF.Location = New Point(0, 0)

        ' Place the current loop repeats counter above the background, on the right
        loopsRepeatTF.Parent = currentPositionBG
        loopsRepeatTF.BackColor = Color.Transparent
        loopsRepeatTF.BringToFront()
        loopsRepeatTF.Location = New Point(178, 0)

        myHandle = Me.Handle ' my hWnd (for hotkey testing)
        plHandle = playlistWindow.Handle ' the playlist window's hWnd (for hotkey testing)

        posThread = New Thread(AddressOf doTheLoop) With {
            .IsBackground = True
        }

        posThread.Start()

        frontThread = New Thread(AddressOf isForeground) With {
            .IsBackground = True
        }

        frontThread.Start()

        setHotKeys() ' initialize the hotkeys

        Me.Text = "Looper 2 by Zach Glenwright [" & My.Resources.BuildDate & " Version]" ' change the title to the actual title (this is done for the "find the other instance" function below)
        Me.Show() ' finally show the window, once initialization is done
    End Sub

    Private Function PrevInstance() As Boolean
        If UBound(Process.GetProcessesByName(Process.GetCurrentProcess.ProcessName)) > 0 Then ' if we're already running a Looper process, and we're not supposed to have new processes
            If My.Application.CommandLineArgs.Count > 0 Then ' if we have a command line (file to launch)
                Dim oldLooperHwnd As IntPtr = FindWindow(Nothing, "Looper 2 by Zach Glenwright [" & My.Resources.BuildDate & " Version]") ' find the old hWnd
                Dim pathToOpen = My.Application.CommandLineArgs(0) ' get the path to the file

                If Strings.InStr(pathToOpen, ".looper") <> 0 Then ' if the path has ".looper" in it
                    SendMessage(&H5000F634, pathToOpen, True, oldLooperHwnd) ' send the path to the old Looper instance
                End If
            End If

            Return True ' we're running another instance of Looper
        Else
            Return False ' this is the first instance that's running
        End If
    End Function

    Private Sub mainWindow_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        If dockPlaylistWindow = True Then ' we're still set to bound the 2 windows together
            If playlistWindow.Left < Me.Left - 250 Or playlistWindow.Left > Me.Left + 250 Then ' we've strayed away from the "magnet" bounds of the main window
                dockPlaylistWindow = False ' so un-dock the playlist window and treat it seperately
            Else ' we're still in bounds, so move the playlist window along with the main window
                playlistWindow.Left = Me.Left
                playlistWindow.Top = Me.Top + Me.Height - 6
            End If
        End If
    End Sub

    Private Sub mainWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If isQuitting = False Then ' we're not already trying to quit from playlistWindow
            clearHotKeys() ' turn off the hokkeys

            If (e.CloseReason = CloseReason.UserClosing) Then
                Dim confirmSave = playlistWindow.unInitialize() ' ask if we want to save, want to quit, etc.

                If confirmSave = DialogResult.Cancel Then ' if we chose NOT to to quit, then turn things back on
                    setHotKeys()
                    isQuitting = False

                    If autoPlayDialogs = True Then SendMessage(CMD_SEND.CMD_PLAY) ' we cancelled quitting, so start playing again

                    e.Cancel = True ' if we cancelled, then don't quit...
                End If
            End If
        End If

        playlistWindow.saveLastPlayedFile() ' save the last played .looper and event to the INI file
    End Sub

    Private Sub loadOptionsWindowButton_MouseEnter(sender As Object, e As EventArgs) Handles loadOptionsWindowButton.MouseEnter
        loadOptionsWindowButton.BackgroundImage = My.Resources.Gear_42px_Blue
    End Sub

    Private Sub loadOptionsWindowButton_MouseLeave(sender As Object, e As EventArgs) Handles loadOptionsWindowButton.MouseLeave
        loadOptionsWindowButton.BackgroundImage = My.Resources.Gear_42px_Grey
    End Sub

    Private Sub showPlayingFileInExplorerButton_MouseEnter(sender As Object, e As EventArgs) Handles showPlayingFileInExplorerButton.MouseEnter
        showPlayingFileInExplorerButton.BackgroundImage = My.Resources.Folder_42px_Blue
    End Sub

    Private Sub showPlayingFileInExplorerButton_MouseLeave(sender As Object, e As EventArgs) Handles showPlayingFileInExplorerButton.MouseLeave
        showPlayingFileInExplorerButton.BackgroundImage = My.Resources.Folder_42px_Grey
    End Sub

    Private Sub mainWindow_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        playlistWindow.WindowState = Me.WindowState ' minimize or show the Playlist window if the main window is minimized/restored
    End Sub

    ' ======================================================================================
    ' ================= THREADS ============================================================
    ' ======================================================================================

    Private Sub doTheLoop()
        While True
            If isQuitting = False Then
                If currentPlayingFile <> Nothing Then
                    SendMessage(CMD_SEND.CMD_GETCURRENTPOSITION) ' get the current position from MPC-HC
                End If

                System.Threading.Thread.Sleep(30)
            Else ' we're quitting, so don't get the current position at this time
                System.Threading.Thread.Sleep(100) ' wait a little bit longer during the thread to see if we cancel quit
            End If
        End While
    End Sub

    Public Sub isForeground()
        While True
            If isQuitting = False Then
                activeHwnd = GetForegroundWindow() ' see which window is currently the foreground window

                If {myHandle, plHandle, MPCHandle}.Contains(activeHwnd) Then ' if the foreground window is either the control panel, playlist or MPC-HC's instance's window
                    If hotkeysActive = False Then
                        Try
                            Me.Invoke(Sub() setHotKeys()) ' turn hotkeys on
                        Catch ex As Exception
                            ' If there's an error, then bow out gracefully
                        End Try
                    End If
                Else ' if there is another window currently active
                    If hotkeysActive = True Then
                        Try
                            Me.Invoke(Sub() clearHotKeys()) ' turn hotkeys off
                        Catch ex As Exception
                            ' If there's an error, then bow out gracefully
                        End Try
                    End If
                End If

                System.Threading.Thread.Sleep(50)
                activeHwnd = Nothing ' uninitialize the variable
            Else ' we're trying to quit, so don't check the hotkey situation at this time
                System.Threading.Thread.Sleep(100) ' and wait a bit longer before checking again
            End If
        End While
    End Sub

    ' ======================================================================================
    ' ================= FILESYSTEM FUNCTIONS ===============================================
    ' ======================================================================================

    Private Sub loadLooperFile(ByVal fileToOpen As String, Optional eventToPlay As Integer = -1)
        If eventToPlay = -1 Then ' technically we don't need to do this, as we do error checking in the loadLooperFile procedure itself, but it's good to reduce the load
            playlistWindow.loadLooperFile(True, fileToOpen) ' if we don't have a specific event to open, then just load the .looper file
        Else
            playlistWindow.loadLooperFile(True, fileToOpen, eventToPlay) ' load the looper file, and load this specific event in it
        End If
    End Sub

    Private Sub loadINIFile()
        While Not My.Computer.FileSystem.GetFileInfo(INIFile).Exists ' if the INI file for Looper doesn't exist
            If Not My.Computer.FileSystem.GetFileInfo(INIFile).Directory.Exists Then ' if the preferences folder doesn't exist
                My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.GetFileInfo(INIFile).Directory.ToString) ' then create it!
            End If

            findLooperWindow.ShowDialog() ' then show the "First Start" dialog
        End While

        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(INIFile)

        ' Load MPC-HC's information first, so if we're only supposed to run one instance, we can bow out here...
        Dim MPCEXE As String = betweenTheLines(fileReader, "MPCEXE=", vbCrLf, "0") ' the path to MPC-HC

        Dim allowMultipleInstances As Boolean
        Boolean.TryParse(betweenTheLines(fileReader, "allowMultipleInstances=", vbCrLf, "B_False"), allowMultipleInstances)

        If allowMultipleInstances = False Then
            If PrevInstance() Then ' we have multiple instances turned off, check to see if we're already running looper
                End ' quit out of the new instance of Looper (if we're already running Looper)
            Else
                If MPCEXE <> "0" Then linkMPC(MPCEXE, allowMultipleInstances) ' launch MPC-HC as either it's own instance, or multiple instances
            End If
        Else
            If MPCEXE <> "0" Then linkMPC(MPCEXE, allowMultipleInstances) ' launch MPC-HC as either it's own instance, or multiple instances
        End If

        ' INI values that need to be stored in variables for later use
        Double.TryParse(betweenTheLines(fileReader, "loopPreviewLength=", vbCrLf, "0.25"), loopPreviewLength)
        Double.TryParse(betweenTheLines(fileReader, "loopSlipLength=", vbCrLf, "0.50"), loopSlipLength)

        Double.TryParse(betweenTheLines(fileReader, "inPointOffset=", vbCrLf, "0"), inPointOffset)
        Double.TryParse(betweenTheLines(fileReader, "outPointOffset=", vbCrLf, "0.15"), outPointOffset)

        ' TODO: Write a "disable hotkeys" preference
        Boolean.TryParse(betweenTheLines(fileReader, "disableHotkeys=", vbCrLf, "B_False"), disableHotkeys)
        Boolean.TryParse(betweenTheLines(fileReader, "autoplayFirstEvent=", vbCrLf, "B_True"), autoplayFirstEvent)
        Boolean.TryParse(betweenTheLines(fileReader, "autoPlayDialogs=", vbCrLf, "B_True"), autoPlayDialogs)
        Boolean.TryParse(betweenTheLines(fileReader, "dontForceLooperModeonOpen=", vbCrLf, "B_False"), dontForceLooperModeonOpen)
        Boolean.TryParse(betweenTheLines(fileReader, "pausePlaybackOnLoadEvent=", vbCrLf, "B_False"), pausePlaybackOnLoadEvent)

        newEventString = betweenTheLines(fileReader, "newEventName=", vbCrLf, "New Loop Event") ' the name for new un-named events on the events list

        ' INI values that don't need to be stored in global variables (these all get used only when Looper first starts up)
        Dim alwaysOnTop As String = betweenTheLines(fileReader, "alwaysOnTop=", vbCrLf, "True") ' whether or not to always be on top from launch
        If alwaysOnTop = "True" Then changeAlwaysOnTop() ' we're set to be in non-Topmost mode on launch, so switch it

        Dim startPositionL As String = betweenTheLines(fileReader, "startPositionL=", vbCrLf, "-1") ' the left-most coordinate to open Looper at
        If startPositionL <> "-1" Then Me.Left = CInt(startPositionL)

        Dim startPositionT As String = betweenTheLines(fileReader, "startPositionT=", vbCrLf, "-1") ' the top-most coordinate to open Looper at
        If startPositionL <> "-1" Then Me.Top = CInt(startPositionT)

        Dim startPLPositionL As String = betweenTheLines(fileReader, "startPLPositionL=", vbCrLf, "-1") ' the left-most coordinate to open Looper's playlist at
        Dim startPLPositionT As String = betweenTheLines(fileReader, "startPLPositionT=", vbCrLf, "-1") ' the top-most coordinate to open Looper playlist at
        Dim startPLPositionW As String = betweenTheLines(fileReader, "startPLPositionW=", vbCrLf, "-1") ' the width of Looper's playlist - check against min size
        Dim startPLPositionH As String = betweenTheLines(fileReader, "startPLPositionH=", vbCrLf, "-1") ' the height of Looper's playlist - check against min size

        Dim hidePlaylistOnLaunch As Boolean
        Boolean.TryParse(betweenTheLines(fileReader, "hidePlaylistOnLaunch=", vbCrLf, "B_False"), hidePlaylistOnLaunch) ' whether or not to show the playlist when Looper opens

        If hidePlaylistOnLaunch = False Then openPlaylistWindow(CInt(startPLPositionL), CInt(startPLPositionT), CInt(startPLPositionW), CInt(startPLPositionH))

        Dim loopButtonMode As String = betweenTheLines(fileReader, "loopButtonMode=", vbCrLf, "Loop Mode") ' what the loop button mode should be on launching
        If loopButtonMode = "Off" Then switchToOffMode() ' we're set to be in Looper Mode by default, so no need to switch *back* to it on startup

        autoloadLastLooper = betweenTheLines(fileReader, "autoloadLastLooper=", vbCrLf, Nothing) ' should we launch the last looper file when closed?

        Dim disableToolTips As Boolean
        Boolean.TryParse(betweenTheLines(fileReader, "disableToolTips=", vbCrLf, "B_False"), disableToolTips)

        If disableToolTips = False Then loadToolTips(True) ' really no need to do the check here, this value is the boolean
    End Sub

    Private Sub loadINIFileForDefaults() ' if we launch defaults at the beginning, then we just need the MPC-HC values, so we can run the program
        While Not My.Computer.FileSystem.GetFileInfo(INIFile).Exists ' if the INI file for Looper doesn't exist
            If Not My.Computer.FileSystem.GetFileInfo(INIFile).Directory.Exists Then ' if the preferences folder doesn't exist
                My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.GetFileInfo(INIFile).Directory.ToString) ' then create it!
            End If

            findLooperWindow.ShowDialog() ' then show the "First Start" dialog
        End While

        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(INIFile)

        Dim MPCEXE As String = betweenTheLines(fileReader, "MPCEXE=", vbCrLf, "0") ' the path to MPC-HC

        Dim allowMultipleInstances As Boolean
        Boolean.TryParse(betweenTheLines(fileReader, "allowMultipleInstances=", vbCrLf, "B_False"), allowMultipleInstances)

        If MPCEXE <> "0" Then
            linkMPC(MPCEXE, allowMultipleInstances) ' launch MPC-HC as either it's own instance, or multiple instances
        Else
            ' TODO: Mirror back to the INI creation process and start over?  Or just display an error, and then quit?
        End If
    End Sub

    Private Sub linkMPC(ByVal MPCEXE As String, ByVal allowMultipleInstances As Boolean)
        Dim MPC_Path As String

        If allowMultipleInstances = False Then ' just open the normal instance of MPC-HC (or tie-in to an already running one)
            MPC_Path = MPCEXE & " /slave " + Me.Handle.ToString
        Else ' open a new instance of MPC-HC
            MPC_Path = MPCEXE & " /new /slave " + Me.Handle.ToString
        End If

        Shell(MPC_Path, AppWinStyle.NormalFocus, False, -1)
    End Sub

    Private Sub showFileInExplorer()
        SendMessage(CMD_SEND.CMD_GETNOWPLAYING) ' get the currently playing file (just in case it may have changed)

        If currentPlayingFile <> Nothing Then
            Process.Start("explorer.exe", "/select,""" & currentPlayingFile & """") ' if MPC-HC is currently playing, then open the file and highlight it in Explorer
        End If
    End Sub

    ' ======================================================================================
    ' ================= HOTKEY HANDLERS ====================================================
    ' ======================================================================================

    Public Sub setHotKeys(Optional hotKeyID As Integer = -1)
        If disableHotkeys = False Then
            'IN and OUT hotkeys
            If hotKeyID = -1 Or hotKeyID = 100 Then RegisterHotKey(Me.Handle, 100, KeyModifier.None, Asc("I"))  ' set IN point
            If hotKeyID = -1 Or hotKeyID = 101 Then RegisterHotKey(Me.Handle, 101, KeyModifier.None, Asc("O"))  ' set OUT point
            If hotKeyID = -1 Or hotKeyID = 102 Then RegisterHotKey(Me.Handle, 102, KeyModifier.Control, Asc("I"))  ' clear IN point
            If hotKeyID = -1 Or hotKeyID = 103 Then RegisterHotKey(Me.Handle, 103, KeyModifier.Control, Asc("O"))  ' clear OUT point
            If hotKeyID = -1 Or hotKeyID = 104 Then RegisterHotKey(Me.Handle, 104, KeyModifier.Control, Asc("X"))  ' clear IN and OUT points

            ' Trimming IN and OUT
            If hotKeyID = -1 Or hotKeyID = 110 Then RegisterHotKey(Me.Handle, 110, KeyModifier.None, Keys.OemOpenBrackets)  ' trim the IN point left
            If hotKeyID = -1 Or hotKeyID = 111 Then RegisterHotKey(Me.Handle, 111, KeyModifier.Shift, Keys.OemOpenBrackets)  ' trim the IN point left by the default value

            If hotKeyID = -1 Or hotKeyID = 112 Then RegisterHotKey(Me.Handle, 112, KeyModifier.None, Keys.OemCloseBrackets)  ' trim the IN point right
            If hotKeyID = -1 Or hotKeyID = 113 Then RegisterHotKey(Me.Handle, 113, KeyModifier.Shift, Keys.OemCloseBrackets)  ' trim the IN point right by the default value

            If hotKeyID = -1 Or hotKeyID = 114 Then RegisterHotKey(Me.Handle, 114, KeyModifier.None, Keys.OemSemicolon)  ' trim the OUT point left
            If hotKeyID = -1 Or hotKeyID = 115 Then RegisterHotKey(Me.Handle, 115, KeyModifier.Shift, Keys.OemSemicolon)  ' trim the OUT point left by the default value

            If hotKeyID = -1 Or hotKeyID = 116 Then RegisterHotKey(Me.Handle, 116, KeyModifier.None, Keys.OemQuotes)  ' trim the OUT point right
            If hotKeyID = -1 Or hotKeyID = 117 Then RegisterHotKey(Me.Handle, 117, KeyModifier.Shift, Keys.OemQuotes)  ' trim the OUT point right by the default value

            ' GUI hotkeys
            If hotKeyID = -1 Or hotKeyID = 120 Then RegisterHotKey(Me.Handle, 120, KeyModifier.Shift, Asc("L"))  ' change loop mode
            If hotKeyID = -1 Or hotKeyID = 121 Then RegisterHotKey(Me.Handle, 121, KeyModifier.Control, Asc("1"))  ' change loop mode to OFF
            If hotKeyID = -1 Or hotKeyID = 122 Then RegisterHotKey(Me.Handle, 122, KeyModifier.Control, Asc("2"))  ' change loop mode to LOOP MODE
            If hotKeyID = -1 Or hotKeyID = 123 Then RegisterHotKey(Me.Handle, 123, KeyModifier.Control, Asc("3"))  ' change loop mode to PLAYLIST MODE
            If hotKeyID = -1 Or hotKeyID = 124 Then RegisterHotKey(Me.Handle, 124, KeyModifier.Control, Asc("4"))  ' change loop mode to SHUFFLE MODE

            If hotKeyID = -1 Or hotKeyID = 125 Then RegisterHotKey(Me.Handle, 125, KeyModifier.Control, Asc("T"))  ' change always on top setting
            If hotKeyID = -1 Or hotKeyID = 126 Then RegisterHotKey(Me.Handle, 126, KeyModifier.Control, Asc("Q"))  ' quit MPC-HC Looper
            If hotKeyID = -1 Or hotKeyID = 127 Then RegisterHotKey(Me.Handle, 127, KeyModifier.Control, Keys.Oemcomma)  ' load options pane
            If hotKeyID = -1 Or hotKeyID = 128 Then RegisterHotKey(Me.Handle, 128, KeyModifier.Alt, Keys.Home)  ' open path to file in Explorer

            ' Working with events
            If hotKeyID = -1 Or hotKeyID = 129 Then RegisterHotKey(Me.Handle, 129, KeyModifier.Control, Asc("N")) ' create new event
            If hotKeyID = -1 Or hotKeyID = 130 Then RegisterHotKey(Me.Handle, 130, KeyModifier.None, Keys.Delete) ' delete currently selected event(s)
            If hotKeyID = -1 Or hotKeyID = 131 Then RegisterHotKey(Me.Handle, 131, KeyModifier.Control, Asc("S")) ' save current events list (overwrite)
            If hotKeyID = -1 Or hotKeyID = 132 Then RegisterHotKey(Me.Handle, 132, KeyModifier.Control + KeyModifier.Shift, Asc("S")) ' save the selected items as a new Looper file
            If hotKeyID = -1 Or hotKeyID = 133 Then RegisterHotKey(Me.Handle, 133, KeyModifier.Control, Asc("L")) ' load new events list
            If hotKeyID = -1 Or hotKeyID = 134 Then RegisterHotKey(Me.Handle, 134, KeyModifier.Control + KeyModifier.Shift, Asc("L")) ' import .looper file into the current events list
            If hotKeyID = -1 Or hotKeyID = 135 Then RegisterHotKey(Me.Handle, 135, KeyModifier.Control, Keys.PageUp) ' go to one event prior in the events list
            If hotKeyID = -1 Or hotKeyID = 136 Then RegisterHotKey(Me.Handle, 136, KeyModifier.Control, Keys.PageDown) ' go to one event next in the events list
            If hotKeyID = -1 Or hotKeyID = 137 Then RegisterHotKey(Me.Handle, 137, KeyModifier.Control, Keys.Up) ' Speed up playback in MPC-HC
            If hotKeyID = -1 Or hotKeyID = 138 Then RegisterHotKey(Me.Handle, 138, KeyModifier.Control, Keys.Down) ' Slow down playback in MPC-HC
            If hotKeyID = -1 Or hotKeyID = 139 Then RegisterHotKey(Me.Handle, 139, KeyModifier.Control, Asc("R")) ' Reset speed to 100% in MPC-HC
            If hotKeyID = -1 Or hotKeyID = 140 Then RegisterHotKey(Me.Handle, 140, KeyModifier.Control, Asc("F")) ' Go to the Search field of the Playlist window

            If hotKeyID = -1 Or hotKeyID = 141 Then RegisterHotKey(Me.Handle, 141, KeyModifier.Control, Asc("A")) ' Select ALL the events in the events list
            If hotKeyID = -1 Or hotKeyID = 142 Then RegisterHotKey(Me.Handle, 142, KeyModifier.Control + KeyModifier.Shift, Asc("D")) ' Select NONE of the events in the events list

            If hotKeyID = -1 Then
                hotkeysTF.Text = "HOTKEYS ON"
                hotkeysTF.ForeColor = Color.Green
                hotkeysActive = True
            End If
        End If
    End Sub

    Public Sub clearHotKeys(Optional hotKeyID As Integer = -1)
        If disableHotkeys = False Then
            If hotKeyID = -1 Or hotKeyID = 100 Then UnregisterHotKey(Me.Handle, 100) ' set IN point
            If hotKeyID = -1 Or hotKeyID = 101 Then UnregisterHotKey(Me.Handle, 101) ' set OUT point
            If hotKeyID = -1 Or hotKeyID = 102 Then UnregisterHotKey(Me.Handle, 102) ' clear IN point
            If hotKeyID = -1 Or hotKeyID = 103 Then UnregisterHotKey(Me.Handle, 103) ' clear OUT point
            If hotKeyID = -1 Or hotKeyID = 104 Then UnregisterHotKey(Me.Handle, 104) ' clear IN and OUT points

            If hotKeyID = -1 Or hotKeyID = 110 Then UnregisterHotKey(Me.Handle, 110) ' trim the IN point left
            If hotKeyID = -1 Or hotKeyID = 111 Then UnregisterHotKey(Me.Handle, 111) ' trim the IN point left by the default value
            If hotKeyID = -1 Or hotKeyID = 112 Then UnregisterHotKey(Me.Handle, 112) ' trim the IN point right
            If hotKeyID = -1 Or hotKeyID = 113 Then UnregisterHotKey(Me.Handle, 113) ' trim the IN point right by the default value
            If hotKeyID = -1 Or hotKeyID = 114 Then UnregisterHotKey(Me.Handle, 114) ' trim the OUT point left
            If hotKeyID = -1 Or hotKeyID = 115 Then UnregisterHotKey(Me.Handle, 115) ' trim the OUT point left by the default value
            If hotKeyID = -1 Or hotKeyID = 116 Then UnregisterHotKey(Me.Handle, 116) ' trim the OUT point right
            If hotKeyID = -1 Or hotKeyID = 117 Then UnregisterHotKey(Me.Handle, 117) ' trim the OUT point right by the default value

            If hotKeyID = -1 Or hotKeyID = 120 Then UnregisterHotKey(Me.Handle, 120) ' change loop mode
            If hotKeyID = -1 Or hotKeyID = 121 Then UnregisterHotKey(Me.Handle, 121) ' change loop mode to OFF
            If hotKeyID = -1 Or hotKeyID = 122 Then UnregisterHotKey(Me.Handle, 122) ' change loop mode to LOOP MODE
            If hotKeyID = -1 Or hotKeyID = 123 Then UnregisterHotKey(Me.Handle, 123) ' change loop mode to PLAYLIST MODE
            If hotKeyID = -1 Or hotKeyID = 124 Then UnregisterHotKey(Me.Handle, 124) ' change loop mode to SHUFFLE MODE

            If hotKeyID = -1 Or hotKeyID = 125 Then UnregisterHotKey(Me.Handle, 125) ' change always on top setting
            If hotKeyID = -1 Or hotKeyID = 126 Then UnregisterHotKey(Me.Handle, 126) ' quit MPC-HC Looper
            If hotKeyID = -1 Or hotKeyID = 127 Then UnregisterHotKey(Me.Handle, 127) ' load options pane
            If hotKeyID = -1 Or hotKeyID = 128 Then UnregisterHotKey(Me.Handle, 128) ' open path to file in Explorer

            If hotKeyID = -1 Or hotKeyID = 129 Then UnregisterHotKey(Me.Handle, 129) ' create new event
            If hotKeyID = -1 Or hotKeyID = 130 Then UnregisterHotKey(Me.Handle, 130) ' delete currently selected event(s)
            If hotKeyID = -1 Or hotKeyID = 131 Then UnregisterHotKey(Me.Handle, 131) ' save current events list
            If hotKeyID = -1 Or hotKeyID = 132 Then UnregisterHotKey(Me.Handle, 132) ' save the selected items as a new Looper file
            If hotKeyID = -1 Or hotKeyID = 133 Then UnregisterHotKey(Me.Handle, 133) ' load new events list
            If hotKeyID = -1 Or hotKeyID = 134 Then UnregisterHotKey(Me.Handle, 134) ' import .looper file into the current events list
            If hotKeyID = -1 Or hotKeyID = 135 Then UnregisterHotKey(Me.Handle, 135) ' go to one event prior in the events list
            If hotKeyID = -1 Or hotKeyID = 136 Then UnregisterHotKey(Me.Handle, 136) ' go to one event next in the events list
            If hotKeyID = -1 Or hotKeyID = 137 Then UnregisterHotKey(Me.Handle, 137) ' Speed up playback in MPC-HC
            If hotKeyID = -1 Or hotKeyID = 138 Then UnregisterHotKey(Me.Handle, 138) ' Slow down playback in MPC-HC
            If hotKeyID = -1 Or hotKeyID = 139 Then UnregisterHotKey(Me.Handle, 139) ' Reset speed to 100% in MPC-HC
            If hotKeyID = -1 Or hotKeyID = 140 Then UnregisterHotKey(Me.Handle, 140) ' Go to the Search field of the Playlist window

            If hotKeyID = -1 Or hotKeyID = 141 Then UnregisterHotKey(Me.Handle, 141) ' Select ALL the events in the events list
            If hotKeyID = -1 Or hotKeyID = 142 Then UnregisterHotKey(Me.Handle, 142) ' Select NONE of the events in the events list

            hotkeysTF.Text = "HOTKEYS OFF"
            hotkeysTF.ForeColor = Color.Red
            hotkeysActive = False
        End If
    End Sub

    Public Function getMode() As Boolean
        If {"Off", "Loop Mode"}.Contains(loopModeButton.Text) Then
            Return True ' if we're in Loop Mode or OFF, then we can do things with IN/OUT points
        Else
            Return False ' otherwise, don't allow it!
        End If
    End Function

    Public Sub handleHotKeyEvent(hotkeyID As Integer)
        Select Case hotkeyID
            Case 100 ' set IN point
                If getMode() Then setInPoint()
            Case 101 ' set OUT point
                If getMode() Then setOutPoint()
            Case 102 ' clear IN point
                If getMode() Then clearInPoint()
            Case 103 ' clear OUT point
                If getMode() Then clearOutPoint()
            Case 104 ' clear IN and OUT points
                If getMode() Then clearInPoint()
                If getMode() Then clearOutPoint()

            Case 110 ' trim the IN point left
                If getMode() Then slipPoint(1)
            Case 111 ' trim the IN point left by the default value
                If getMode() Then slipPoint(1, 0.05)
            Case 112 ' trim the IN point right
                If getMode() Then slipPoint(2)
            Case 113 ' trim the IN point right by the default value
                If getMode() Then slipPoint(2, 0.05)
            Case 114 ' trim the OUT point left
                If getMode() Then slipPoint(3)
            Case 115 ' trim the OUT point left by the default value
                If getMode() Then slipPoint(3, 0.05)
            Case 116 ' trim the OUT point right
                If getMode() Then slipPoint(4)
            Case 117 ' trim the OUT point right by the default value
                If getMode() Then slipPoint(4, 0.05)

            Case 120 ' change loop mode
                switchLoopMode()
            Case 121 ' change loop mode to OFF
                switchToOffMode()
            Case 122 ' change loop mode to LOOP MODE
                switchToLoopMode()
            Case 123 ' change loop mode to PLAYLIST MODE
                switchToPlaylistMode()
            Case 124 ' change loop mode to SHUFFLE MODE
                switchToShuffleMode()

            Case 125 ' change always on top setting
                changeAlwaysOnTop()
            Case 126 ' quit MPC-HC Looper
                Me.Close()
            Case 127 ' load options pane
                optionsWindow.ShowDialog()
            Case 128 ' open path to file in Explorer
                showFileInExplorer()

            Case 129 ' create new event
                If getMode() Then playlistWindow.addEvent()
            Case 130 ' delete currently selected event(s)
                If getMode() Then playlistWindow.deleteEvents()
            Case 131 ' save current events list
                If getMode() Then playlistWindow.saveLooperFile()
            Case 132 ' save the selected items as a new Looper file
                playlistWindow.saveLooperFile(True)
            Case 133 ' load new events list
                playlistWindow.loadLooperFile()
            Case 134 ' import .looper file into the current events list
                playlistWindow.loadLooperFile(False)
            Case 135 ' go to one event prior in the events list
                playlistWindow.loadPrevNextEvent(-1)
            Case 136 ' go to one event next in the events list
                playlistWindow.loadPrevNextEvent(1)
            Case 137 ' Speed up playback in MPC-HC
                If getMode() Then setSpeed(speedSlider.Value + 10)
            Case 138 ' Slow down playback in MPC-HC
                If getMode() Then setSpeed(speedSlider.Value - 10)
            Case 139 ' Reset speed to 100% in MPC-HC
                If getMode() Then setSpeed(100)
            Case 140 ' Go to the Search field of the Playlist window
                playlistWindow.searchTF.Select()

            Case 141 ' Select ALL the events in the events list
                If playlistWindow.eventsList.Items.Count > 0 Then
                    playlistWindow.eventsList.BeginUpdate()

                    For a As Integer = 0 To playlistWindow.eventsList.Items.Count - 1
                        playlistWindow.eventsList.Items(a).Selected = True
                    Next

                    playlistWindow.eventsList.EndUpdate()
                End If
            Case 142 ' Select NONE of the events in the events list
                If playlistWindow.eventsList.Items.Count > 0 Then
                    playlistWindow.eventsList.BeginUpdate()

                    For a As Integer = 0 To playlistWindow.eventsList.Items.Count - 1
                        playlistWindow.eventsList.Items(a).Selected = False
                    Next

                    playlistWindow.eventsList.EndUpdate()
                End If
        End Select
    End Sub

    ' ======================================================================================
    ' ================= BUTTON / CLICK HANDLERS ============================================
    ' ======================================================================================

    Private Sub inPointButton_Click(sender As Object, e As EventArgs) Handles inPointButton.Click
        setInPoint()
    End Sub

    Private Sub outPointButton_Click(sender As Object, e As EventArgs) Handles outPointButton.Click
        setOutPoint()
    End Sub

    Private Sub inPointClearButton_Click(sender As Object, e As EventArgs) Handles inPointClearButton.Click
        clearInPoint()
    End Sub

    Private Sub outPointClearButton_Click(sender As Object, e As EventArgs) Handles outPointClearButton.Click
        clearOutPoint()
    End Sub

    Private Sub clearAllPointsButton_Click(sender As Object, e As EventArgs) Handles clearAllPointsButton.Click
        clearInPoint()
        clearOutPoint()
    End Sub

    Private Sub currentPositionTF_Click(sender As Object, e As EventArgs) Handles currentPositionTF.Click
        switchCurrentAndRemaining()
    End Sub

    Private Sub alwaysOnTopButton_Click(sender As Object, e As EventArgs) Handles alwaysOnTopButton.Click
        changeAlwaysOnTop()
    End Sub

    Private Sub currentRemainingStatus_Click(sender As Object, e As EventArgs) Handles currentRemainingStatus.Click
        switchCurrentAndRemaining()
    End Sub

    Private Sub togglePlaylistButton_Click(sender As Object, e As EventArgs) Handles togglePlaylistButton.Click
        openPlaylistWindow()
    End Sub

    Public Sub openPlaylistWindow(Optional PLWindowL As Integer = -1,
                                  Optional PLWindowT As Integer = -1,
                                  Optional PLWindowW As Integer = -1,
                                  Optional PLWindowH As Integer = -1)
        If playlistWindow.Visible = True Then
            togglePlaylistButton.Text = "SHOW PLAYLIST"
            playlistWindow.Hide()
        Else
            togglePlaylistButton.Text = "HIDE PLAYLIST"

            If PLWindowL <> -1 Then
                playlistWindow.Left = PLWindowL ' if we specify a Left value, then open the playlist at this position
                dockPlaylistWindow = False ' don't dock the main window and playlist windows
            Else
                playlistWindow.Left = Me.Left ' if we don't specify a value for Left, then open it relative to the main window
                dockPlaylistWindow = True ' DO dock the main window and playlist windows
            End If

            If PLWindowT <> -1 Then
                playlistWindow.Top = PLWindowT ' if we specify a Top value, then open the playlist at this position
            Else
                playlistWindow.Top = Me.Top + Me.Height - 6 ' if we don't specify a value for Top, then open it relative to the main window
            End If

            If PLWindowW <> -1 Then
                playlistWindow.Width = PLWindowW ' if there's a value for width, then resize it to this width
            Else
                playlistWindow.Width = Me.Width
            End If

            If PLWindowH <> -1 Then
                playlistWindow.Height = PLWindowH ' if there's a value for height, then resize it to this height
            Else
                playlistWindow.Height = 538
            End If

            playlistWindow.TopMost = Me.TopMost

            playlistWindow.Show()
        End If
    End Sub

    Private Sub loadOptionsWindowButton_Click(sender As Object, e As EventArgs) Handles loadOptionsWindowButton.Click
        optionsWindow.ShowDialog()
    End Sub

    Private Sub showPlayingFileInExplorerButton_Click(sender As Object, e As EventArgs) Handles showPlayingFileInExplorerButton.Click
        showFileInExplorer()
    End Sub

    Private Sub loopModeButton_Click(sender As Object, e As EventArgs) Handles loopModeButton.Click
        switchLoopMode()
    End Sub

    Private Sub switchLoopMode()
        If loopModeButton.Text = "Loop Mode" Then
            If playlistWindow.eventsList.Items.Count > 0 Then
                switchToPlaylistMode()
            Else
                switchToOffMode()
            End If
        ElseIf loopModeButton.Text = "Playlist Mode" Then
            switchToOffMode()
        ElseIf loopModeButton.Text = "Off" Then
            switchToLoopMode()
        End If
    End Sub

    Private Sub switchEditingControls(Optional onOrOff As Boolean = True)
        inPointSlipLeftButton.Enabled = onOrOff
        inPointClearButton.Enabled = onOrOff
        inPointButton.Enabled = onOrOff
        inPointSlipRightButton.Enabled = onOrOff

        clearAllPointsButton.Enabled = onOrOff

        outPointSlipLeftButton.Enabled = onOrOff
        outPointClearButton.Enabled = onOrOff
        outPointButton.Enabled = onOrOff
        outPointSlipRightButton.Enabled = onOrOff

        speedSlider.Enabled = onOrOff
        speed0Button.Enabled = onOrOff
        speed25Button.Enabled = onOrOff
        speed50Button.Enabled = onOrOff
        speed75Button.Enabled = onOrOff
        speed100Button.Enabled = onOrOff
        speed125Button.Enabled = onOrOff
        speed150Button.Enabled = onOrOff
        speed175Button.Enabled = onOrOff
        speed200Button.Enabled = onOrOff

        playlistWindow.clearEventsButton.Enabled = onOrOff
        playlistWindow.addEventButton.Enabled = onOrOff

        If onOrOff = True Then
            If playlistWindow.eventsList.SelectedItems.Count > 0 Then
                playlistWindow.modifyEventButton.Enabled = True ' activate the Modify Event button if we have something selected
            End If
        Else
            playlistWindow.modifyEventButton.Enabled = False ' turn the Modify Event button off
        End If
    End Sub

    Public Sub switchToOffMode()
        cancelRandomization()

        loopModeButton.Text = "Off"
        loopModeButton.BackColor = Color.FromArgb(255, 255, 225)

        switchEditingControls()
    End Sub

    Public Sub switchToLoopMode()
        cancelRandomization()

        loopModeButton.Text = "Loop Mode"
        loopModeButton.BackColor = Color.FromArgb(176, 246, 176)

        switchEditingControls()
    End Sub

    Public Sub switchToPlaylistMode()
        If playlistWindow.eventsList.Items.Count > 0 Then
            cancelRandomization()

            loopModeButton.Text = "Playlist Mode"
            loopModeButton.BackColor = Color.FromArgb(255, 168, 130)

            switchEditingControls(False)
        End If
    End Sub

    Public Sub switchToShuffleMode()
        Dim totalEvents = playlistWindow.eventsList.Items.Count ' get the total number of events currently in the playlist

        If totalEvents > 0 Then
            ReDim randomNumberList(0) ' erase the values currently stored in the array (to ensure it won't have any extra values)

            loopModeButton.Text = "Shuffle Mode"
            loopModeButton.BackColor = Color.FromArgb(133, 231, 240)

            switchEditingControls(False) ' turn off editing controls, so you can't use them in Shuffle mode

            randomNumberList = Enumerable.Range(0, totalEvents).OrderBy(Function(r) RNG.Next()).Take(totalEvents + 1).ToArray ' re-initialize the array

            ' Force the playlist to start at the beginning of the random order
            playlistWindow.currentPlayingEvent = -1
            playlistWindow.loadPrevNextEvent(1)
        End If
    End Sub

    Public Sub cancelRandomization()
        ReDim randomNumberList(0)
        If loopModeButton.Text = "Shuffle Mode" Then playlistWindow.currentPlayingEvent = playlistWindow.actualPlayingEvent ' get the current event from the currently playing random event
    End Sub

    Private Sub switchCurrentAndRemaining()
        If currentOrRemaining = False Then
            currentOrRemaining = True
            currentRemainingStatus.Text = "REMAINING"
        Else
            currentOrRemaining = False
            currentRemainingStatus.Text = "CURRENT"
        End If
    End Sub

    Private Sub changeAlwaysOnTop()
        If Me.TopMost Then
            Me.TopMost = False
            playlistWindow.TopMost = False
            alwaysOnTopButton.Text = "Not Topmost"
            alwaysOnTopButton.BackColor = Color.FromArgb(209, 209, 209)
        Else
            Me.TopMost = True
            playlistWindow.TopMost = True
            alwaysOnTopButton.Text = "Always on Top"
            alwaysOnTopButton.BackColor = Color.FromArgb(183, 186, 243)
        End If
    End Sub

    ' ======================================================================================
    ' ================= SLIP CLICK/HOLD DONW HANDLERS ======================================
    ' ======================================================================================

    Private Sub inPointSlipLeftButton_Click(sender As Object, e As EventArgs) Handles inPointSlipLeftButton.Click
        slipPoint(1)
    End Sub

    Private Sub inPointSlipRightButton_Click(sender As Object, e As EventArgs) Handles inPointSlipRightButton.Click
        slipPoint(2)
    End Sub

    Private Sub outPointSlipLeftButton_Click(sender As Object, e As EventArgs) Handles outPointSlipLeftButton.Click
        slipPoint(3)
    End Sub

    Private Sub outPointSlipRightButton_Click(sender As Object, e As EventArgs) Handles outPointSlipRightButton.Click
        slipPoint(4)
    End Sub

    Private Sub inPointSlipLeftButton_MouseDown(sender As Object, e As MouseEventArgs) Handles inPointSlipLeftButton.MouseDown
        slipAction = 1 ' we're slipping the IN point left
        slipTimer.Start()
    End Sub

    Private Sub inPointSlipLeftButton_MouseUp(sender As Object, e As MouseEventArgs) Handles inPointSlipLeftButton.MouseUp
        slipTimer.Stop()
    End Sub

    Private Sub inPointSlipRightButton_MouseDown(sender As Object, e As MouseEventArgs) Handles inPointSlipRightButton.MouseDown
        slipAction = 2 ' we're slipping the IN point right
        slipTimer.Start()
    End Sub

    Private Sub inPointSlipRightButton_MouseUp(sender As Object, e As MouseEventArgs) Handles inPointSlipRightButton.MouseUp
        slipTimer.Stop()
    End Sub

    Private Sub outPointSlipLeftButton_MouseDown(sender As Object, e As MouseEventArgs) Handles outPointSlipLeftButton.MouseDown
        slipAction = 3 ' we're slipping the OUT point left
        slipTimer.Start()
    End Sub

    Private Sub outPointSlipLeftButton_MouseUp(sender As Object, e As MouseEventArgs) Handles outPointSlipLeftButton.MouseUp
        slipTimer.Stop()
    End Sub

    Private Sub outPointSlipRightButton_MouseDown(sender As Object, e As MouseEventArgs) Handles outPointSlipRightButton.MouseDown
        slipAction = 4 ' we're slipping the OUT point right
        slipTimer.Start()
    End Sub

    Private Sub outPointSlipRightButton_MouseUp(sender As Object, e As MouseEventArgs) Handles outPointSlipRightButton.MouseUp
        slipTimer.Stop()
    End Sub

    Private Sub slipTimer_Tick(sender As Object, e As EventArgs) Handles slipTimer.Tick
        slipPoint(slipAction) ' slip the specified point
    End Sub

    ' ======================================================================================
    ' ================= IN/OUT/TIME HANDLERS ===============================================
    ' ======================================================================================

    Public Sub setInPoint()
        SendMessage(CMD_SEND.CMD_GETCURRENTPOSITION)
        inTF.Text = NumberToTimeString(currentPosition - 0.25 + inPointOffset)

        If TimeStringToNumber(inTF.Text) > TimeStringToNumber(outTF.Text) Then
            clearOutPoint()
        End If

        playlistWindow.checkAgainstCurrentPlayingEvent() ' check to see if the IN and OUT points match the currently playing event (for highlight)
    End Sub

    Public Sub setOutPoint()
        SendMessage(CMD_SEND.CMD_GETCURRENTPOSITION)
        outTF.Text = NumberToTimeString(currentPosition)

        playlistWindow.checkAgainstCurrentPlayingEvent() ' check to see if the IN and OUT points match the currently playing event (for highlight)
    End Sub

    Public Sub clearInPoint()
        inTF.Text = "0:00"

        playlistWindow.checkAgainstCurrentPlayingEvent() ' check to see if the IN and OUT points match the currently playing event (for highlight)
    End Sub

    Public Sub clearOutPoint()
        SendMessage(CMD_SEND.CMD_GETNOWPLAYING)
        outTF.Text = NumberToTimeString(currentDuration + 0.5 - outPointOffset)

        playlistWindow.checkAgainstCurrentPlayingEvent() ' check to see if the IN and OUT points match the currently playing event (for highlight)
    End Sub

    Private Sub slipPoint(ByVal thePoint As Integer, Optional slipAmount As Double = -1)
        If slipAmount = -1 Then slipAmount = loopSlipLength ' set the slipping amount to your preset if we're not slipping by a specific number
        Dim newPoint As Double ' the new point after slipping

        Select Case thePoint
            Case 1 ' we're slipping the IN point back
                newPoint = TimeStringToNumber(inTF.Text) - slipAmount
                If newPoint < 0 Then newPoint = 0 ' if the resulting IN point is less than zero, then just set the IN to 0
            Case 2 ' we're slipping the IN point forwards
                newPoint = TimeStringToNumber(inTF.Text) + slipAmount
            Case 3 ' we're slipping the OUT point back
                newPoint = TimeStringToNumber(outTF.Text) - slipAmount
            Case 4 ' we're slipping the OUT point forwards
                newPoint = TimeStringToNumber(outTF.Text) + slipAmount
        End Select

        If thePoint < 3 Then ' we're slipping the IN point, so we don't need to do a preview slip-back
            inTF.Text = NumberToTimeString(newPoint)
            SendMessage(CMD_SEND.CMD_SETPOSITION, CStr((newPoint - 0.5)))
        Else ' we're slipping the OUT point, so do the slip-back
            outTF.Text = NumberToTimeString(newPoint)
            SendMessage(CMD_SEND.CMD_SETPOSITION, CStr((newPoint - 0.5 - loopPreviewLength)))
        End If

        playlistWindow.checkAgainstCurrentPlayingEvent() ' check to see if the IN and OUT points match the currently playing event (for highlight)
    End Sub

    ' ======================================================================================
    ' ================= MANUALLY SETTING IN AND OUT POINTS =================================
    ' ======================================================================================

    Private Sub inTF_Enter(sender As Object, e As EventArgs) Handles inTF.Enter
        clearHotKeys()
        hotkeysActive = True  'tell the hotkey thread that hotkeys are still active, so it doesn't try to clear them (this is for text entry ONLY)
    End Sub

    Private Sub inTF_KeyPress(sender As Object, e As KeyPressEventArgs) Handles inTF.KeyPress
        ' ----------------- IF THE ENTERED CHARACTER IS NOT A NUMBER, THE CHARACTER . OR THE CHARACTER :, THEN DON'T ALLOW IT! -----------------
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not e.KeyChar = "." AndAlso Not e.KeyChar = ":" Then
            e.Handled = True
        End If
    End Sub

    Private Sub inTF_Leave(sender As Object, e As EventArgs) Handles inTF.Leave
        setHotKeys()

        Dim timeString As Double
        Dim timeStringTest = Double.TryParse(inTF.Text, timeString)

        If timeStringTest = True Then ' if we can coerce the current value to a double, then convert it to a time string when leaving the control - otherwise it's fine!
            inTF.Text = NumberToTimeString(CDbl(inTF.Text))
        End If
    End Sub

    Private Sub outTF_Enter(sender As Object, e As EventArgs) Handles outTF.Enter
        clearHotKeys()
        hotkeysActive = True  'tell the hotkey thread that hotkeys are still active, so it doesn't try to clear them (this is for text entry ONLY)
    End Sub

    Private Sub outTF_KeyPress(sender As Object, e As KeyPressEventArgs) Handles outTF.KeyPress
        ' ----------------- IF THE ENTERED CHARACTER IS NOT A NUMBER, THE CHARACTER . OR THE CHARACTER :, THEN DON'T ALLOW IT! -----------------
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not e.KeyChar = "." AndAlso Not e.KeyChar = ":" Then
            e.Handled = True
        End If
    End Sub

    Private Sub outTF_Leave(sender As Object, e As EventArgs) Handles outTF.Leave
        setHotKeys()

        Dim timeString As Double
        Dim timeStringTest = Double.TryParse(outTF.Text, timeString)

        If timeStringTest = True Then ' if we can coerce the current value to a double, then convert it to a time string when leaving the control - otherwise it's fine!
            outTF.Text = NumberToTimeString(CDbl(outTF.Text))
        End If
    End Sub

    ' ======================================================================================
    ' ================= SPEED HANDLERS =====================================================
    ' ======================================================================================

    Private Sub speedSlider_Scroll(sender As Object, e As EventArgs) Handles speedSlider.Scroll
        setSpeed()
    End Sub

    Public Sub setSpeed(Optional theSpeed As Integer = -1)
        If theSpeed <> -1 Then
            speedSlider.Value = theSpeed
        End If

        speed100Button.Text = speedSlider.Value & "%"
        SendMessage(CMD_SEND.CMD_SETSPEED, Convert.ToString(speedSlider.Value / 100))
    End Sub

    Private Sub speed0Button_Click(sender As Object, e As EventArgs) Handles speed0Button.Click
        setSpeed(0)
    End Sub

    Private Sub speed25Button_Click(sender As Object, e As EventArgs) Handles speed25Button.Click
        setSpeed(25)
    End Sub

    Private Sub speed50Button_Click(sender As Object, e As EventArgs) Handles speed50Button.Click
        setSpeed(50)
    End Sub

    Private Sub speed75Button_Click(sender As Object, e As EventArgs) Handles speed75Button.Click
        setSpeed(75)
    End Sub

    Private Sub speed100Button_Click(sender As Object, e As EventArgs) Handles speed100Button.Click
        setSpeed(100)
    End Sub

    Private Sub speed125Button_Click(sender As Object, e As EventArgs) Handles speed125Button.Click
        setSpeed(125)
    End Sub

    Private Sub speed150Button_Click(sender As Object, e As EventArgs) Handles speed150Button.Click
        setSpeed(150)
    End Sub

    Private Sub speed175Button_Click(sender As Object, e As EventArgs) Handles speed175Button.Click
        setSpeed(175)
    End Sub

    Private Sub speed200Button_Click(sender As Object, e As EventArgs) Handles speed200Button.Click
        setSpeed(200)
    End Sub
End Class
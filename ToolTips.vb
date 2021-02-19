Module ToolTips
    Public Sub loadToolTips(ByVal loadToolTips As Boolean)
        If loadToolTips = True Then
            ' ======================================================================================
            ' ================= MAIN WINDOW TOOLTIPS ===============================================
            ' ======================================================================================

            mainWindow.toolTips.SetToolTip(mainWindow.loopModeButton, "Change the loop mode from Off, Loop, Playlist and Shuffle" &
                               vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                               "Ctrl-L : Switch modes manually" & vbCrLf &
                               "Ctrl-1 : Directly go to OFF Mode" & vbCrLf &
                               "Ctrl-2 : Directly go to Loop Mode" & vbCrLf &
                               "Ctrl-3 : Directly go to Playlist Mode (you must have at least 2 events in the Playlist)" & vbCrLf &
                               "Ctrl-4 : Directly go to Shuffle Mode (you must have at least 2 events in the Playlist)")

            mainWindow.toolTips.SetToolTip(mainWindow.inTF, "Shows the current event's IN point, where the loop starts playing.  Otherwise known as the ""A"" Point of an A/B loop")

            mainWindow.toolTips.SetToolTip(mainWindow.inPointSlipLeftButton, "Slip (move) the current IN point back a small amount to make the loop start earlier" &
                               vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                               "[ : Slip IN Point back " & mainWindow.loopSlipLength & " second(s) - this value is settable in the Options pane" & vbCrLf &
                               "Shift-[ : Slip IN Point back 1/20th of a second")

            mainWindow.toolTips.SetToolTip(mainWindow.inPointClearButton, "Clear the current IN point and set it to 0:00" &
                               vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                               "CTRL-I : Clear the current IN point")

            mainWindow.toolTips.SetToolTip(mainWindow.inPointButton, "Set the current IN point to the location MPC-HC/BE is currently playing" &
                               vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                               "I - Set IN Point")

            mainWindow.toolTips.SetToolTip(mainWindow.inPointSlipRightButton, "Slip (move) the current IN point forward a small amount to make the loop start later" &
                               vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                               "] : Slip IN Point forward " & mainWindow.loopSlipLength & " second(s)  - this value is settable in the Options pane" & vbCrLf &
                               "Shift-] : Slip IN Point forward 1/20th of a second")

            mainWindow.toolTips.SetToolTip(mainWindow.clearAllPointsButton, "Clear both of the currently set IN and OUT points" &
                               vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                               "Ctrl-X : Clear both IN and OUT Points")

            mainWindow.toolTips.SetToolTip(mainWindow.togglePlaylistButton, "Show or hide the events playlist window")
            mainWindow.toolTips.SetToolTip(mainWindow.alwaysOnTopButton, "Toggles whether or not to keep the Looper windows on top of all other windows" &
                               vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                               "Ctrl-T - Toggle Always on Top on/off")

            mainWindow.toolTips.SetToolTip(mainWindow.currentPositionTF, "Click on the display to switch between showing the current playing position" & vbCrLf & "of MPC-HC/BE or the amount of time remaining in the current event's loop")
            mainWindow.toolTips.SetToolTip(mainWindow.loopsRepeatTF, "Shows the amount of loop repeats left in the current event (when playing in Playlist or" & vbCrLf & "Shuffle mode - in Loop mode, the event will keep repeating")
            mainWindow.toolTips.SetToolTip(mainWindow.hotkeysTF, "Shows whether hotkeys (keyboard shortcuts) are currently enabled or disabled -" & vbCrLf & "if neither Looper or MPC-HC/BE are active, hotkeys will be turned off")

            mainWindow.toolTips.SetToolTip(mainWindow.outTF, "Shows the current event's OUT point, where the loop stops playing.  Otherwise known as the ""B"" Point of an A/B loop")

            mainWindow.toolTips.SetToolTip(mainWindow.outPointSlipLeftButton, "Slip (move) the current OUT point back a small amount to make the loop end earlier" &
                               vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                               "; : Slip OUT Point back " & mainWindow.loopSlipLength & " second(s) - this value is settable in the Options pane" & vbCrLf &
                               "Shift-; : Slip OUT Point back 1/20th of a second")

            mainWindow.toolTips.SetToolTip(mainWindow.outPointClearButton, "Clear the current OUT point and set it to the duration of the currently playing file" &
                               vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                               "CTRL-O : Clear the current OUT point")

            mainWindow.toolTips.SetToolTip(mainWindow.outPointButton, "Set the current OUT point to the location MPC-HC/BE is currently playing" &
                               vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                               "O - Set OUT Point")

            mainWindow.toolTips.SetToolTip(mainWindow.outPointSlipRightButton, "Slip (move) the current OUT point forward a small amount to make the loop end later" &
                               vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                               "' : Slip OUT Point forward " & mainWindow.loopSlipLength & " second(s)  - this value is settable in the Options pane" & vbCrLf &
                               "Shift-' : Slip OUT Point forward 1/20th of a second")

            mainWindow.toolTips.SetToolTip(mainWindow.speedSlider, "Controls the playback of the currently playing file in MPC-HC/BE -" & vbCrLf & "slide left to slow playback down and slide right to speed playback up" &
                               vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                               "Ctrl-R : Set playback speed to 100%" & vbCrLf &
                               "Ctrl-Down : Slow playback down by 10%" & vbCrLf &
                               "Ctrl-Up : Speed playback up by 10%")

            mainWindow.toolTips.SetToolTip(mainWindow.speed0Button, "Click to set the current MPC-HC/BE playback to 0% (basically show a still image)")
            mainWindow.toolTips.SetToolTip(mainWindow.speed25Button, "Click to set the current MPC-HC/BE playback to 25%")
            mainWindow.toolTips.SetToolTip(mainWindow.speed50Button, "Click to set the current MPC-HC/BE playback to 50%")
            mainWindow.toolTips.SetToolTip(mainWindow.speed75Button, "Click to set the current MPC-HC/BE playback to 75%")
            mainWindow.toolTips.SetToolTip(mainWindow.speed100Button, "Shows the current speed Looper is set to." & vbCrLf & "Click on this to set the current MPC-HC/BE playback to 100%" &
                            vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                            "Ctrl-R : Set playback speed to 100%")
            mainWindow.toolTips.SetToolTip(mainWindow.speed125Button, "Click to set the current MPC-HC/BE playback to 125%")
            mainWindow.toolTips.SetToolTip(mainWindow.speed150Button, "Click to set the current MPC-HC/BE playback to 150%")
            mainWindow.toolTips.SetToolTip(mainWindow.speed175Button, "Click to set the current MPC-HC/BE playback to 175%")
            mainWindow.toolTips.SetToolTip(mainWindow.speed200Button, "Click to set the current MPC-HC/BE playback to 200%")

            mainWindow.toolTips.SetToolTip(mainWindow.loadOptionsWindowButton, "Shows the Options pane, which allows you to set various Looper preferences" &
                            vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                            "Ctrl-, : Show Options pane")

            mainWindow.toolTips.SetToolTip(mainWindow.showPlayingFileInExplorerButton, "Opens a Windows Explorer window highlighting the currently playing MPC-HC/BE file" &
                            vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                            "Ctrl-Home - Spotlight currently playing file in Windows Explorer")

            ' ======================================================================================
            ' ================= PLAYLIST WINDOW TOOLTIPS ===========================================
            ' ======================================================================================

            playlistWindow.toolTips.SetToolTip(playlistWindow.prevEventButton, "Click this button to load to the previous event in the playlist." &
                            vbCrLf & "If you're in Shuffle mode, click this button to go to the last random event." &
                            vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                            "Ctrl-Page Up - Previous Event")

            playlistWindow.toolTips.SetToolTip(playlistWindow.saveButton, "Click this button to save the current events list to a .looper file" &
                            vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                            "Ctrl-S - Save Current Events List")

            playlistWindow.toolTips.SetToolTip(playlistWindow.loadButton, "Left click this button to load a .looper file into the playlist." &
                            vbCrLf & "Right-click this button to import a .looper file into the playlist." &
                            vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                            "Ctrl-L - Load .looper File into Playlist" & vbCrLf &
                            "Shift-Ctrl-L - Import .looper File into Playlist")

            playlistWindow.toolTips.SetToolTip(playlistWindow.deleteEventButton, "Click this button to delete the currently selected events." &
                            vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                            "Delete - Delete Currently Selected Events")

            playlistWindow.toolTips.SetToolTip(playlistWindow.modifyEventButton, "Click this button to modify the selected event with the" &
                            vbCrLf & "current IN and OUT points and speed setting.")

            playlistWindow.toolTips.SetToolTip(playlistWindow.addEventButton, "Add a new event to the playlist with the current IN, OUT and Speed parameters." &
                            vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                            "Ctrl-N - Add Event to the Playlist")

            playlistWindow.toolTips.SetToolTip(playlistWindow.clearEventsButton, "Unload the current .looper file and clear the playlist completely.")

            playlistWindow.toolTips.SetToolTip(playlistWindow.nextEventButton, "Click this button to load to the next event in the playlist." &
                            vbCrLf & "If you're in Shuffle mode, click this button to go to the next random event." &
                            vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                            "Ctrl-Page Down - Next Event")

            playlistWindow.toolTips.SetToolTip(playlistWindow.searchTF, "Search for events in the playlist by typing a keyword in here.")
            playlistWindow.toolTips.SetToolTip(playlistWindow.doSearchButton, "Click here to start a search for keywords in the playlist.")
            playlistWindow.toolTips.SetToolTip(playlistWindow.cancelSearchButton, "Click here to cancel a search and go back to the main playlist.")

            playlistWindow.toolTips.SetToolTip(playlistWindow.eventsList, "This is the playlist (or events list), a list of individual loops that" & vbCrLf &
                                               "you can jump to.  You can specify different IN and OUT points, the speed" & vbCrLf &
                                               "of each event, and the amount of times each event will play when in Playlist" & vbCrLf &
                                               "and Shuffle modes." &
                                               vbCrLf & vbCrLf & "Shortcut Keys -" & vbCrLf &
                                               "F2 - Edit the name of the currently selected event" & vbCrLf &
                                               "F3 - Edit the repeat count for the currently selected event" & vbCrLf &
                                               "Ctrl-A - Select all the events in the playlist" & vbCrLf &
                                               "Shift-Ctrl-D - Deselect all the events in the playlist")

            mainWindow.toolTips.Active = True
        Else
            mainWindow.toolTips.Active = False
            playlistWindow.toolTips.Active = False
        End If
    End Sub
End Module

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class playlistWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.clearEventsButton = New System.Windows.Forms.Button()
        Me.addEventButton = New System.Windows.Forms.Button()
        Me.menu_Add = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.menuItem_AddAtEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuItem_InsertEvents = New System.Windows.Forms.ToolStripMenuItem()
        Me.modifyEventButton = New System.Windows.Forms.Button()
        Me.deleteEventButton = New System.Windows.Forms.Button()
        Me.loadButton = New System.Windows.Forms.Button()
        Me.menu_load = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.menuItem_importLooperFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.saveButton = New System.Windows.Forms.Button()
        Me.menu_Save = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.menuItem_SaveSelection = New System.Windows.Forms.ToolStripMenuItem()
        Me.listViewEditor = New System.Windows.Forms.TextBox()
        Me.prevEventButton = New System.Windows.Forms.Button()
        Me.nextEventButton = New System.Windows.Forms.Button()
        Me.searchTF = New System.Windows.Forms.TextBox()
        Me.doSearchButton = New System.Windows.Forms.Button()
        Me.cancelSearchButton = New System.Windows.Forms.Button()
        Me.toolTips = New System.Windows.Forms.ToolTip(Me.components)
        Me.eventsList = New MPC_HC_Looper_VB.ListViewDoubleBuffered()
        Me.eventPosHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.eventNameHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.speedColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.loopsColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.eventINPointHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.eventOutPointHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.eventDurHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.xPosHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.yPosHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.xZoomHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.yZoomHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.eventFilenameHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.menu_Add.SuspendLayout()
        Me.menu_load.SuspendLayout()
        Me.menu_Save.SuspendLayout()
        Me.SuspendLayout()
        '
        'clearEventsButton
        '
        Me.clearEventsButton.Enabled = False
        Me.clearEventsButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clearEventsButton.Location = New System.Drawing.Point(457, 6)
        Me.clearEventsButton.Name = "clearEventsButton"
        Me.clearEventsButton.Size = New System.Drawing.Size(68, 23)
        Me.clearEventsButton.TabIndex = 7
        Me.clearEventsButton.Text = "CLEAR ALL"
        Me.clearEventsButton.UseVisualStyleBackColor = True
        '
        'addEventButton
        '
        Me.addEventButton.ContextMenuStrip = Me.menu_Add
        Me.addEventButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.addEventButton.Location = New System.Drawing.Point(373, 6)
        Me.addEventButton.Name = "addEventButton"
        Me.addEventButton.Size = New System.Drawing.Size(78, 23)
        Me.addEventButton.TabIndex = 6
        Me.addEventButton.Text = "ADD EVENT"
        Me.addEventButton.UseVisualStyleBackColor = True
        '
        'menu_Add
        '
        Me.menu_Add.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.menu_Add.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuItem_AddAtEnd, Me.menuItem_InsertEvents})
        Me.menu_Add.Name = "menu_Add"
        Me.menu_Add.Size = New System.Drawing.Size(356, 48)
        '
        'menuItem_AddAtEnd
        '
        Me.menuItem_AddAtEnd.Checked = True
        Me.menuItem_AddAtEnd.CheckState = System.Windows.Forms.CheckState.Checked
        Me.menuItem_AddAtEnd.Name = "menuItem_AddAtEnd"
        Me.menuItem_AddAtEnd.Size = New System.Drawing.Size(355, 22)
        Me.menuItem_AddAtEnd.Text = "Add New Events to the End of the Event List (Normal)"
        '
        'menuItem_InsertEvents
        '
        Me.menuItem_InsertEvents.CheckOnClick = True
        Me.menuItem_InsertEvents.Name = "menuItem_InsertEvents"
        Me.menuItem_InsertEvents.Size = New System.Drawing.Size(355, 22)
        Me.menuItem_InsertEvents.Text = "Insert New Events After Current Selection"
        '
        'modifyEventButton
        '
        Me.modifyEventButton.Enabled = False
        Me.modifyEventButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modifyEventButton.Location = New System.Drawing.Point(275, 6)
        Me.modifyEventButton.Name = "modifyEventButton"
        Me.modifyEventButton.Size = New System.Drawing.Size(92, 23)
        Me.modifyEventButton.TabIndex = 5
        Me.modifyEventButton.Text = "MODIFY EVENT"
        Me.modifyEventButton.UseVisualStyleBackColor = True
        '
        'deleteEventButton
        '
        Me.deleteEventButton.Enabled = False
        Me.deleteEventButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.deleteEventButton.Location = New System.Drawing.Point(181, 6)
        Me.deleteEventButton.Name = "deleteEventButton"
        Me.deleteEventButton.Size = New System.Drawing.Size(88, 23)
        Me.deleteEventButton.TabIndex = 4
        Me.deleteEventButton.Text = "DELETE EVENT"
        Me.deleteEventButton.UseVisualStyleBackColor = True
        '
        'loadButton
        '
        Me.loadButton.ContextMenuStrip = Me.menu_load
        Me.loadButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.loadButton.Location = New System.Drawing.Point(115, 6)
        Me.loadButton.Name = "loadButton"
        Me.loadButton.Size = New System.Drawing.Size(60, 23)
        Me.loadButton.TabIndex = 3
        Me.loadButton.Text = "LOAD"
        Me.loadButton.UseVisualStyleBackColor = True
        '
        'menu_load
        '
        Me.menu_load.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.menu_load.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuItem_importLooperFile})
        Me.menu_load.Name = "ContextMenuStrip1"
        Me.menu_load.ShowImageMargin = False
        Me.menu_load.Size = New System.Drawing.Size(335, 26)
        '
        'menuItem_importLooperFile
        '
        Me.menuItem_importLooperFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.menuItem_importLooperFile.Name = "menuItem_importLooperFile"
        Me.menuItem_importLooperFile.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.menuItem_importLooperFile.Size = New System.Drawing.Size(334, 22)
        Me.menuItem_importLooperFile.Text = "Import Looper File into Current Playlist..."
        '
        'saveButton
        '
        Me.saveButton.ContextMenuStrip = Me.menu_Save
        Me.saveButton.Enabled = False
        Me.saveButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveButton.Location = New System.Drawing.Point(49, 6)
        Me.saveButton.Name = "saveButton"
        Me.saveButton.Size = New System.Drawing.Size(60, 23)
        Me.saveButton.TabIndex = 2
        Me.saveButton.Text = "SAVE"
        Me.saveButton.UseVisualStyleBackColor = True
        '
        'menu_Save
        '
        Me.menu_Save.ImageScalingSize = New System.Drawing.Size(28, 28)
        Me.menu_Save.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuItem_SaveSelection})
        Me.menu_Save.Name = "menu_Save"
        Me.menu_Save.ShowImageMargin = False
        Me.menu_Save.Size = New System.Drawing.Size(250, 26)
        '
        'menuItem_SaveSelection
        '
        Me.menuItem_SaveSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.menuItem_SaveSelection.Name = "menuItem_SaveSelection"
        Me.menuItem_SaveSelection.Size = New System.Drawing.Size(249, 22)
        Me.menuItem_SaveSelection.Text = "Save Current Selection as Looper file..."
        '
        'listViewEditor
        '
        Me.listViewEditor.Location = New System.Drawing.Point(129, 207)
        Me.listViewEditor.Name = "listViewEditor"
        Me.listViewEditor.Size = New System.Drawing.Size(238, 20)
        Me.listViewEditor.TabIndex = 29
        Me.listViewEditor.Visible = False
        '
        'prevEventButton
        '
        Me.prevEventButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.prevEventButton.Location = New System.Drawing.Point(8, 6)
        Me.prevEventButton.Name = "prevEventButton"
        Me.prevEventButton.Size = New System.Drawing.Size(35, 23)
        Me.prevEventButton.TabIndex = 1
        Me.prevEventButton.Text = "<"
        Me.prevEventButton.UseVisualStyleBackColor = True
        '
        'nextEventButton
        '
        Me.nextEventButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nextEventButton.Location = New System.Drawing.Point(531, 6)
        Me.nextEventButton.Name = "nextEventButton"
        Me.nextEventButton.Size = New System.Drawing.Size(35, 23)
        Me.nextEventButton.TabIndex = 8
        Me.nextEventButton.Text = ">"
        Me.nextEventButton.UseVisualStyleBackColor = True
        '
        'searchTF
        '
        Me.searchTF.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.searchTF.Location = New System.Drawing.Point(8, 33)
        Me.searchTF.Name = "searchTF"
        Me.searchTF.Size = New System.Drawing.Size(443, 22)
        Me.searchTF.TabIndex = 30
        '
        'doSearchButton
        '
        Me.doSearchButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.doSearchButton.Location = New System.Drawing.Point(457, 32)
        Me.doSearchButton.Name = "doSearchButton"
        Me.doSearchButton.Size = New System.Drawing.Size(68, 23)
        Me.doSearchButton.TabIndex = 31
        Me.doSearchButton.Text = "SEARCH"
        Me.doSearchButton.UseVisualStyleBackColor = True
        '
        'cancelSearchButton
        '
        Me.cancelSearchButton.Enabled = False
        Me.cancelSearchButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancelSearchButton.Location = New System.Drawing.Point(531, 32)
        Me.cancelSearchButton.Name = "cancelSearchButton"
        Me.cancelSearchButton.Size = New System.Drawing.Size(35, 23)
        Me.cancelSearchButton.TabIndex = 32
        Me.cancelSearchButton.Text = "X"
        Me.cancelSearchButton.UseVisualStyleBackColor = True
        '
        'eventsList
        '
        Me.eventsList.AllowDrop = True
        Me.eventsList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.eventsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.eventPosHeader, Me.eventNameHeader, Me.speedColumn, Me.loopsColumn, Me.eventINPointHeader, Me.eventOutPointHeader, Me.eventDurHeader, Me.xPosHeader, Me.yPosHeader, Me.xZoomHeader, Me.yZoomHeader, Me.eventFilenameHeader})
        Me.eventsList.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.eventsList.FullRowSelect = True
        Me.eventsList.GridLines = True
        Me.eventsList.HideSelection = False
        Me.eventsList.Location = New System.Drawing.Point(0, 60)
        Me.eventsList.Name = "eventsList"
        Me.eventsList.Size = New System.Drawing.Size(572, 440)
        Me.eventsList.TabIndex = 9
        Me.eventsList.UseCompatibleStateImageBehavior = False
        Me.eventsList.View = System.Windows.Forms.View.Details
        '
        'eventPosHeader
        '
        Me.eventPosHeader.Text = "#"
        Me.eventPosHeader.Width = 31
        '
        'eventNameHeader
        '
        Me.eventNameHeader.Text = "Event (0 events, 0:00.000 total)"
        Me.eventNameHeader.Width = 247
        '
        'speedColumn
        '
        Me.speedColumn.Text = "%"
        Me.speedColumn.Width = 34
        '
        'loopsColumn
        '
        Me.loopsColumn.Text = "X"
        Me.loopsColumn.Width = 34
        '
        'eventINPointHeader
        '
        Me.eventINPointHeader.Text = "In Point"
        Me.eventINPointHeader.Width = 75
        '
        'eventOutPointHeader
        '
        Me.eventOutPointHeader.Text = "Out Point"
        Me.eventOutPointHeader.Width = 75
        '
        'eventDurHeader
        '
        Me.eventDurHeader.Text = "Duration"
        Me.eventDurHeader.Width = 75
        '
        'xPosHeader
        '
        Me.xPosHeader.Text = "X Offset"
        Me.xPosHeader.Width = 65
        '
        'yPosHeader
        '
        Me.yPosHeader.Text = "Y Offset"
        Me.yPosHeader.Width = 65
        '
        'xZoomHeader
        '
        Me.xZoomHeader.Text = "X Zoom"
        Me.xZoomHeader.Width = 65
        '
        'yZoomHeader
        '
        Me.yZoomHeader.Text = "Y Zoom"
        Me.yZoomHeader.Width = 65
        '
        'eventFilenameHeader
        '
        Me.eventFilenameHeader.Text = "File Path"
        Me.eventFilenameHeader.Width = 850
        '
        'playlistWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(572, 499)
        Me.ControlBox = False
        Me.Controls.Add(Me.cancelSearchButton)
        Me.Controls.Add(Me.doSearchButton)
        Me.Controls.Add(Me.searchTF)
        Me.Controls.Add(Me.eventsList)
        Me.Controls.Add(Me.nextEventButton)
        Me.Controls.Add(Me.prevEventButton)
        Me.Controls.Add(Me.clearEventsButton)
        Me.Controls.Add(Me.addEventButton)
        Me.Controls.Add(Me.modifyEventButton)
        Me.Controls.Add(Me.deleteEventButton)
        Me.Controls.Add(Me.loadButton)
        Me.Controls.Add(Me.saveButton)
        Me.Controls.Add(Me.listViewEditor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimumSize = New System.Drawing.Size(584, 238)
        Me.Name = "playlistWindow"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Playlist - Untitled"
        Me.menu_Add.ResumeLayout(False)
        Me.menu_load.ResumeLayout(False)
        Me.menu_Save.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents clearEventsButton As Button
    Friend WithEvents addEventButton As Button
    Friend WithEvents modifyEventButton As Button
    Friend WithEvents deleteEventButton As Button
    Friend WithEvents loadButton As Button
    Friend WithEvents saveButton As Button
    Friend WithEvents listViewEditor As TextBox
    Friend WithEvents prevEventButton As Button
    Friend WithEvents nextEventButton As Button
    Friend WithEvents eventsList As ListViewDoubleBuffered
    Friend WithEvents eventPosHeader As ColumnHeader
    Friend WithEvents eventNameHeader As ColumnHeader
    Friend WithEvents speedColumn As ColumnHeader
    Friend WithEvents loopsColumn As ColumnHeader
    Friend WithEvents eventINPointHeader As ColumnHeader
    Friend WithEvents eventOutPointHeader As ColumnHeader
    Friend WithEvents eventDurHeader As ColumnHeader
    Friend WithEvents eventFilenameHeader As ColumnHeader
    Friend WithEvents menu_load As ContextMenuStrip
    Friend WithEvents menuItem_importLooperFile As ToolStripMenuItem
    Friend WithEvents menu_Add As ContextMenuStrip
    Friend WithEvents menuItem_AddAtEnd As ToolStripMenuItem
    Friend WithEvents menuItem_InsertEvents As ToolStripMenuItem
    Friend WithEvents searchTF As TextBox
    Friend WithEvents doSearchButton As Button
    Friend WithEvents cancelSearchButton As Button
    Friend WithEvents toolTips As ToolTip
    Friend WithEvents menu_Save As ContextMenuStrip
    Friend WithEvents menuItem_SaveSelection As ToolStripMenuItem
    Friend WithEvents xZoomHeader As ColumnHeader
    Friend WithEvents yZoomHeader As ColumnHeader
    Friend WithEvents xPosHeader As ColumnHeader
    Friend WithEvents yPosHeader As ColumnHeader
End Class

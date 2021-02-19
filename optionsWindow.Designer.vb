<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class optionsWindow
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
        Me.previewTimeLabel = New System.Windows.Forms.Label()
        Me.previewTimeTF = New System.Windows.Forms.TextBox()
        Me.slipAmountTF = New System.Windows.Forms.TextBox()
        Me.slipAmountLabel = New System.Windows.Forms.Label()
        Me.savePreviewTimeCB = New System.Windows.Forms.CheckBox()
        Me.saveSlipTimeCB = New System.Windows.Forms.CheckBox()
        Me.saveLooperWndPosCB = New System.Windows.Forms.CheckBox()
        Me.saveCurrentLoopButtonCB = New System.Windows.Forms.CheckBox()
        Me.saveAOTCB = New System.Windows.Forms.CheckBox()
        Me.hidePlaylistWndCB = New System.Windows.Forms.CheckBox()
        Me.defaultTextLabel = New System.Windows.Forms.Label()
        Me.defaultTextLabel2 = New System.Windows.Forms.Label()
        Me.defaultTextLabel3 = New System.Windows.Forms.Label()
        Me.extraSettingsLabel = New System.Windows.Forms.Label()
        Me.keepModeCB = New System.Windows.Forms.CheckBox()
        Me.disableTTCB = New System.Windows.Forms.CheckBox()
        Me.autoloadCB = New System.Windows.Forms.CheckBox()
        Me.allowMICB = New System.Windows.Forms.CheckBox()
        Me.disableAutoPlayCB = New System.Windows.Forms.CheckBox()
        Me.disableAutoPlayOnLoadCB = New System.Windows.Forms.CheckBox()
        Me.forcePauseCB = New System.Windows.Forms.CheckBox()
        Me.savePrefsButton = New System.Windows.Forms.Button()
        Me.cancelPrefsButton = New System.Windows.Forms.Button()
        Me.saveNewNameTF = New System.Windows.Forms.TextBox()
        Me.saveNewNameLabel = New System.Windows.Forms.Label()
        Me.saveNewNameCB = New System.Windows.Forms.CheckBox()
        Me.savePLWindowSizeCB = New System.Windows.Forms.CheckBox()
        Me.openingFilesLabel = New System.Windows.Forms.Label()
        Me.showINIFileButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'previewTimeLabel
        '
        Me.previewTimeLabel.AutoSize = True
        Me.previewTimeLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.previewTimeLabel.Location = New System.Drawing.Point(8, 11)
        Me.previewTimeLabel.Name = "previewTimeLabel"
        Me.previewTimeLabel.Size = New System.Drawing.Size(171, 17)
        Me.previewTimeLabel.TabIndex = 0
        Me.previewTimeLabel.Text = "Preview Time (in Seconds)"
        '
        'previewTimeTF
        '
        Me.previewTimeTF.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.previewTimeTF.Location = New System.Drawing.Point(11, 31)
        Me.previewTimeTF.Name = "previewTimeTF"
        Me.previewTimeTF.Size = New System.Drawing.Size(188, 25)
        Me.previewTimeTF.TabIndex = 1
        Me.previewTimeTF.Text = "0.25"
        '
        'slipAmountTF
        '
        Me.slipAmountTF.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.slipAmountTF.Location = New System.Drawing.Point(209, 31)
        Me.slipAmountTF.Name = "slipAmountTF"
        Me.slipAmountTF.Size = New System.Drawing.Size(182, 25)
        Me.slipAmountTF.TabIndex = 3
        Me.slipAmountTF.Text = "1"
        '
        'slipAmountLabel
        '
        Me.slipAmountLabel.AutoSize = True
        Me.slipAmountLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.slipAmountLabel.Location = New System.Drawing.Point(206, 11)
        Me.slipAmountLabel.Name = "slipAmountLabel"
        Me.slipAmountLabel.Size = New System.Drawing.Size(165, 17)
        Me.slipAmountLabel.TabIndex = 2
        Me.slipAmountLabel.Text = "Slip Amount (in Seconds)"
        '
        'savePreviewTimeCB
        '
        Me.savePreviewTimeCB.AutoSize = True
        Me.savePreviewTimeCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.savePreviewTimeCB.Location = New System.Drawing.Point(11, 60)
        Me.savePreviewTimeCB.Name = "savePreviewTimeCB"
        Me.savePreviewTimeCB.Size = New System.Drawing.Size(183, 21)
        Me.savePreviewTimeCB.TabIndex = 4
        Me.savePreviewTimeCB.Text = "Save this setting as Default"
        Me.savePreviewTimeCB.UseVisualStyleBackColor = True
        '
        'saveSlipTimeCB
        '
        Me.saveSlipTimeCB.AutoSize = True
        Me.saveSlipTimeCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveSlipTimeCB.Location = New System.Drawing.Point(209, 60)
        Me.saveSlipTimeCB.Name = "saveSlipTimeCB"
        Me.saveSlipTimeCB.Size = New System.Drawing.Size(183, 21)
        Me.saveSlipTimeCB.TabIndex = 5
        Me.saveSlipTimeCB.Text = "Save this setting as Default"
        Me.saveSlipTimeCB.UseVisualStyleBackColor = True
        '
        'saveLooperWndPosCB
        '
        Me.saveLooperWndPosCB.AutoSize = True
        Me.saveLooperWndPosCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveLooperWndPosCB.Location = New System.Drawing.Point(14, 282)
        Me.saveLooperWndPosCB.Name = "saveLooperWndPosCB"
        Me.saveLooperWndPosCB.Size = New System.Drawing.Size(339, 21)
        Me.saveLooperWndPosCB.TabIndex = 6
        Me.saveLooperWndPosCB.Text = "Save current Looper Control Panel position as default"
        Me.saveLooperWndPosCB.UseVisualStyleBackColor = True
        '
        'saveCurrentLoopButtonCB
        '
        Me.saveCurrentLoopButtonCB.AutoSize = True
        Me.saveCurrentLoopButtonCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveCurrentLoopButtonCB.Location = New System.Drawing.Point(14, 263)
        Me.saveCurrentLoopButtonCB.Name = "saveCurrentLoopButtonCB"
        Me.saveCurrentLoopButtonCB.Size = New System.Drawing.Size(318, 21)
        Me.saveCurrentLoopButtonCB.TabIndex = 7
        Me.saveCurrentLoopButtonCB.Text = "Save current Loop Mode button setting as default"
        Me.saveCurrentLoopButtonCB.UseVisualStyleBackColor = True
        '
        'saveAOTCB
        '
        Me.saveAOTCB.AutoSize = True
        Me.saveAOTCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveAOTCB.Location = New System.Drawing.Point(14, 244)
        Me.saveAOTCB.Name = "saveAOTCB"
        Me.saveAOTCB.Size = New System.Drawing.Size(291, 21)
        Me.saveAOTCB.TabIndex = 8
        Me.saveAOTCB.Text = "Save current Always on Top setting as default"
        Me.saveAOTCB.UseVisualStyleBackColor = True
        '
        'hidePlaylistWndCB
        '
        Me.hidePlaylistWndCB.AutoSize = True
        Me.hidePlaylistWndCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hidePlaylistWndCB.Location = New System.Drawing.Point(14, 225)
        Me.hidePlaylistWndCB.Name = "hidePlaylistWndCB"
        Me.hidePlaylistWndCB.Size = New System.Drawing.Size(238, 21)
        Me.hidePlaylistWndCB.TabIndex = 9
        Me.hidePlaylistWndCB.Text = "Hide Playlist window when launching"
        Me.hidePlaylistWndCB.UseVisualStyleBackColor = True
        '
        'defaultTextLabel
        '
        Me.defaultTextLabel.AutoSize = True
        Me.defaultTextLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.defaultTextLabel.Location = New System.Drawing.Point(8, 166)
        Me.defaultTextLabel.Name = "defaultTextLabel"
        Me.defaultTextLabel.Size = New System.Drawing.Size(261, 17)
        Me.defaultTextLabel.TabIndex = 10
        Me.defaultTextLabel.Text = "Default Settings for new Looper sessions"
        '
        'defaultTextLabel2
        '
        Me.defaultTextLabel2.AutoSize = True
        Me.defaultTextLabel2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.defaultTextLabel2.Location = New System.Drawing.Point(9, 186)
        Me.defaultTextLabel2.Name = "defaultTextLabel2"
        Me.defaultTextLabel2.Size = New System.Drawing.Size(373, 17)
        Me.defaultTextLabel2.TabIndex = 11
        Me.defaultTextLabel2.Text = "NOTE: To restore any setting below to its default, un-check any of"
        '
        'defaultTextLabel3
        '
        Me.defaultTextLabel3.AutoSize = True
        Me.defaultTextLabel3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.defaultTextLabel3.Location = New System.Drawing.Point(9, 204)
        Me.defaultTextLabel3.Name = "defaultTextLabel3"
        Me.defaultTextLabel3.Size = New System.Drawing.Size(364, 17)
        Me.defaultTextLabel3.TabIndex = 12
        Me.defaultTextLabel3.Text = "the preference(s) you want to restore below and click Save Prefs."
        '
        'extraSettingsLabel
        '
        Me.extraSettingsLabel.AutoSize = True
        Me.extraSettingsLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.extraSettingsLabel.Location = New System.Drawing.Point(8, 326)
        Me.extraSettingsLabel.Name = "extraSettingsLabel"
        Me.extraSettingsLabel.Size = New System.Drawing.Size(140, 17)
        Me.extraSettingsLabel.TabIndex = 13
        Me.extraSettingsLabel.Text = "Extra System Options"
        '
        'keepModeCB
        '
        Me.keepModeCB.AutoSize = True
        Me.keepModeCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.keepModeCB.Location = New System.Drawing.Point(14, 469)
        Me.keepModeCB.Name = "keepModeCB"
        Me.keepModeCB.Size = New System.Drawing.Size(345, 21)
        Me.keepModeCB.TabIndex = 14
        Me.keepModeCB.Text = "Keep the current mode setting when opening new files"
        Me.keepModeCB.UseVisualStyleBackColor = True
        '
        'disableTTCB
        '
        Me.disableTTCB.AutoSize = True
        Me.disableTTCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.disableTTCB.Location = New System.Drawing.Point(14, 365)
        Me.disableTTCB.Name = "disableTTCB"
        Me.disableTTCB.Size = New System.Drawing.Size(368, 21)
        Me.disableTTCB.TabIndex = 15
        Me.disableTTCB.Text = "Disable tool tips on the Control Panel and Playlist windows"
        Me.disableTTCB.UseVisualStyleBackColor = True
        '
        'autoloadCB
        '
        Me.autoloadCB.AutoSize = True
        Me.autoloadCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.autoloadCB.Location = New System.Drawing.Point(14, 424)
        Me.autoloadCB.Name = "autoloadCB"
        Me.autoloadCB.Size = New System.Drawing.Size(338, 21)
        Me.autoloadCB.TabIndex = 16
        Me.autoloadCB.Text = "Auto-load the last open .looper file on Looper launch"
        Me.autoloadCB.UseVisualStyleBackColor = True
        '
        'allowMICB
        '
        Me.allowMICB.AutoSize = True
        Me.allowMICB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.allowMICB.Location = New System.Drawing.Point(14, 346)
        Me.allowMICB.Name = "allowMICB"
        Me.allowMICB.Size = New System.Drawing.Size(227, 21)
        Me.allowMICB.TabIndex = 17
        Me.allowMICB.Text = "Allow multiple instances of Looper"
        Me.allowMICB.UseVisualStyleBackColor = True
        '
        'disableAutoPlayCB
        '
        Me.disableAutoPlayCB.AutoSize = True
        Me.disableAutoPlayCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.disableAutoPlayCB.Location = New System.Drawing.Point(14, 404)
        Me.disableAutoPlayCB.Name = "disableAutoPlayCB"
        Me.disableAutoPlayCB.Size = New System.Drawing.Size(313, 21)
        Me.disableAutoPlayCB.TabIndex = 18
        Me.disableAutoPlayCB.Text = "Disable auto-playing after exiting Looper dialogs"
        Me.disableAutoPlayCB.UseVisualStyleBackColor = True
        '
        'disableAutoPlayOnLoadCB
        '
        Me.disableAutoPlayOnLoadCB.AutoSize = True
        Me.disableAutoPlayOnLoadCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.disableAutoPlayOnLoadCB.Location = New System.Drawing.Point(14, 488)
        Me.disableAutoPlayOnLoadCB.Name = "disableAutoPlayOnLoadCB"
        Me.disableAutoPlayOnLoadCB.Size = New System.Drawing.Size(361, 21)
        Me.disableAutoPlayOnLoadCB.TabIndex = 19
        Me.disableAutoPlayOnLoadCB.Text = "Disable auto-playing first event when opening .looper file"
        Me.disableAutoPlayOnLoadCB.UseVisualStyleBackColor = True
        '
        'forcePauseCB
        '
        Me.forcePauseCB.AutoSize = True
        Me.forcePauseCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.forcePauseCB.Location = New System.Drawing.Point(14, 384)
        Me.forcePauseCB.Name = "forcePauseCB"
        Me.forcePauseCB.Size = New System.Drawing.Size(309, 21)
        Me.forcePauseCB.TabIndex = 20
        Me.forcePauseCB.Text = "Force MPC-HC/BE to pause when loading events"
        Me.forcePauseCB.UseVisualStyleBackColor = True
        '
        'savePrefsButton
        '
        Me.savePrefsButton.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.savePrefsButton.Location = New System.Drawing.Point(283, 528)
        Me.savePrefsButton.Name = "savePrefsButton"
        Me.savePrefsButton.Size = New System.Drawing.Size(110, 31)
        Me.savePrefsButton.TabIndex = 22
        Me.savePrefsButton.Text = "Save Prefs"
        Me.savePrefsButton.UseVisualStyleBackColor = True
        '
        'cancelPrefsButton
        '
        Me.cancelPrefsButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelPrefsButton.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cancelPrefsButton.Location = New System.Drawing.Point(167, 528)
        Me.cancelPrefsButton.Name = "cancelPrefsButton"
        Me.cancelPrefsButton.Size = New System.Drawing.Size(110, 31)
        Me.cancelPrefsButton.TabIndex = 23
        Me.cancelPrefsButton.Text = "Cancel"
        Me.cancelPrefsButton.UseVisualStyleBackColor = True
        '
        'saveNewNameTF
        '
        Me.saveNewNameTF.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveNewNameTF.Location = New System.Drawing.Point(11, 107)
        Me.saveNewNameTF.Name = "saveNewNameTF"
        Me.saveNewNameTF.Size = New System.Drawing.Size(379, 25)
        Me.saveNewNameTF.TabIndex = 24
        Me.saveNewNameTF.Text = "New Loop Event"
        '
        'saveNewNameLabel
        '
        Me.saveNewNameLabel.AutoSize = True
        Me.saveNewNameLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveNewNameLabel.Location = New System.Drawing.Point(8, 85)
        Me.saveNewNameLabel.Name = "saveNewNameLabel"
        Me.saveNewNameLabel.Size = New System.Drawing.Size(105, 17)
        Me.saveNewNameLabel.TabIndex = 25
        Me.saveNewNameLabel.Text = "New Event Title"
        '
        'saveNewNameCB
        '
        Me.saveNewNameCB.AutoSize = True
        Me.saveNewNameCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveNewNameCB.Location = New System.Drawing.Point(11, 137)
        Me.saveNewNameCB.Name = "saveNewNameCB"
        Me.saveNewNameCB.Size = New System.Drawing.Size(183, 21)
        Me.saveNewNameCB.TabIndex = 26
        Me.saveNewNameCB.Text = "Save this setting as Default"
        Me.saveNewNameCB.UseVisualStyleBackColor = True
        '
        'savePLWindowSizeCB
        '
        Me.savePLWindowSizeCB.AutoSize = True
        Me.savePLWindowSizeCB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.savePLWindowSizeCB.Location = New System.Drawing.Point(14, 302)
        Me.savePLWindowSizeCB.Name = "savePLWindowSizeCB"
        Me.savePLWindowSizeCB.Size = New System.Drawing.Size(354, 21)
        Me.savePLWindowSizeCB.TabIndex = 27
        Me.savePLWindowSizeCB.Text = "Save current Playlist window position and size as default"
        Me.savePLWindowSizeCB.UseVisualStyleBackColor = True
        '
        'openingFilesLabel
        '
        Me.openingFilesLabel.AutoSize = True
        Me.openingFilesLabel.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.openingFilesLabel.Location = New System.Drawing.Point(8, 448)
        Me.openingFilesLabel.Name = "openingFilesLabel"
        Me.openingFilesLabel.Size = New System.Drawing.Size(141, 17)
        Me.openingFilesLabel.TabIndex = 28
        Me.openingFilesLabel.Text = "Opening .looper Files"
        '
        'showINIFileButton
        '
        Me.showINIFileButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.showINIFileButton.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.showINIFileButton.Location = New System.Drawing.Point(12, 528)
        Me.showINIFileButton.Name = "showINIFileButton"
        Me.showINIFileButton.Size = New System.Drawing.Size(149, 31)
        Me.showINIFileButton.TabIndex = 29
        Me.showINIFileButton.Text = "Show INI file in Explorer"
        Me.showINIFileButton.UseVisualStyleBackColor = True
        '
        'optionsWindow
        '
        Me.AcceptButton = Me.savePrefsButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cancelPrefsButton
        Me.ClientSize = New System.Drawing.Size(401, 569)
        Me.ControlBox = False
        Me.Controls.Add(Me.showINIFileButton)
        Me.Controls.Add(Me.openingFilesLabel)
        Me.Controls.Add(Me.savePLWindowSizeCB)
        Me.Controls.Add(Me.saveNewNameCB)
        Me.Controls.Add(Me.saveNewNameLabel)
        Me.Controls.Add(Me.saveNewNameTF)
        Me.Controls.Add(Me.cancelPrefsButton)
        Me.Controls.Add(Me.savePrefsButton)
        Me.Controls.Add(Me.forcePauseCB)
        Me.Controls.Add(Me.disableAutoPlayOnLoadCB)
        Me.Controls.Add(Me.disableAutoPlayCB)
        Me.Controls.Add(Me.allowMICB)
        Me.Controls.Add(Me.autoloadCB)
        Me.Controls.Add(Me.disableTTCB)
        Me.Controls.Add(Me.keepModeCB)
        Me.Controls.Add(Me.extraSettingsLabel)
        Me.Controls.Add(Me.defaultTextLabel3)
        Me.Controls.Add(Me.defaultTextLabel2)
        Me.Controls.Add(Me.defaultTextLabel)
        Me.Controls.Add(Me.hidePlaylistWndCB)
        Me.Controls.Add(Me.saveAOTCB)
        Me.Controls.Add(Me.saveCurrentLoopButtonCB)
        Me.Controls.Add(Me.saveLooperWndPosCB)
        Me.Controls.Add(Me.saveSlipTimeCB)
        Me.Controls.Add(Me.savePreviewTimeCB)
        Me.Controls.Add(Me.slipAmountTF)
        Me.Controls.Add(Me.slipAmountLabel)
        Me.Controls.Add(Me.previewTimeTF)
        Me.Controls.Add(Me.previewTimeLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "optionsWindow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Looper 2 Preferences"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents previewTimeLabel As Label
    Friend WithEvents previewTimeTF As TextBox
    Friend WithEvents slipAmountTF As TextBox
    Friend WithEvents slipAmountLabel As Label
    Friend WithEvents savePreviewTimeCB As CheckBox
    Friend WithEvents saveSlipTimeCB As CheckBox
    Friend WithEvents saveLooperWndPosCB As CheckBox
    Friend WithEvents saveCurrentLoopButtonCB As CheckBox
    Friend WithEvents saveAOTCB As CheckBox
    Friend WithEvents hidePlaylistWndCB As CheckBox
    Friend WithEvents defaultTextLabel As Label
    Friend WithEvents defaultTextLabel2 As Label
    Friend WithEvents defaultTextLabel3 As Label
    Friend WithEvents extraSettingsLabel As Label
    Friend WithEvents keepModeCB As CheckBox
    Friend WithEvents disableTTCB As CheckBox
    Friend WithEvents autoloadCB As CheckBox
    Friend WithEvents allowMICB As CheckBox
    Friend WithEvents disableAutoPlayCB As CheckBox
    Friend WithEvents disableAutoPlayOnLoadCB As CheckBox
    Friend WithEvents forcePauseCB As CheckBox
    Friend WithEvents savePrefsButton As Button
    Friend WithEvents cancelPrefsButton As Button
    Friend WithEvents saveNewNameTF As TextBox
    Friend WithEvents saveNewNameLabel As Label
    Friend WithEvents saveNewNameCB As CheckBox
    Friend WithEvents savePLWindowSizeCB As CheckBox
    Friend WithEvents openingFilesLabel As Label
    Friend WithEvents showINIFileButton As Button
End Class

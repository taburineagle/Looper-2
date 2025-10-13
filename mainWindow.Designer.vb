<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class mainWindow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mainWindow))
        Me.loopModeButton = New System.Windows.Forms.Button()
        Me.inPointSlipLeftButton = New System.Windows.Forms.Button()
        Me.inPointButton = New System.Windows.Forms.Button()
        Me.inPointClearButton = New System.Windows.Forms.Button()
        Me.inPointSlipRightButton = New System.Windows.Forms.Button()
        Me.inTF = New System.Windows.Forms.TextBox()
        Me.outTF = New System.Windows.Forms.TextBox()
        Me.outPointSlipRightButton = New System.Windows.Forms.Button()
        Me.outPointClearButton = New System.Windows.Forms.Button()
        Me.outPointButton = New System.Windows.Forms.Button()
        Me.outPointSlipLeftButton = New System.Windows.Forms.Button()
        Me.alwaysOnTopButton = New System.Windows.Forms.Button()
        Me.clearAllPointsButton = New System.Windows.Forms.Button()
        Me.currentPositionTF = New System.Windows.Forms.Label()
        Me.currentRemainingStatus = New System.Windows.Forms.Label()
        Me.togglePlaylistButton = New System.Windows.Forms.Button()
        Me.hotkeysTF = New System.Windows.Forms.Label()
        Me.currentPositionBG = New System.Windows.Forms.Label()
        Me.loopsRepeatTF = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.speedSlider = New System.Windows.Forms.TrackBar()
        Me.speed100Button = New System.Windows.Forms.Label()
        Me.speed125Button = New System.Windows.Forms.Label()
        Me.speed150Button = New System.Windows.Forms.Label()
        Me.speed175Button = New System.Windows.Forms.Label()
        Me.speed200Button = New System.Windows.Forms.Label()
        Me.speed75Button = New System.Windows.Forms.Label()
        Me.speed50Button = New System.Windows.Forms.Label()
        Me.speed25Button = New System.Windows.Forms.Label()
        Me.speed0Button = New System.Windows.Forms.Label()
        Me.loadOptionsWindowButton = New System.Windows.Forms.Button()
        Me.showPlayingFileInExplorerButton = New System.Windows.Forms.Button()
        Me.toolTips = New System.Windows.Forms.ToolTip(Me.components)
        Me.slipTimer = New System.Windows.Forms.Timer(Me.components)
        Me.xZoomLabel = New System.Windows.Forms.Label()
        Me.xZoomTF = New System.Windows.Forms.TextBox()
        Me.yZoomTF = New System.Windows.Forms.TextBox()
        Me.yZoomLabel = New System.Windows.Forms.Label()
        Me.yPosTF = New System.Windows.Forms.TextBox()
        Me.yPosLabel = New System.Windows.Forms.Label()
        Me.xPosTF = New System.Windows.Forms.TextBox()
        Me.xPosLabel = New System.Windows.Forms.Label()
        Me.zoomAxesLinkButton = New System.Windows.Forms.CheckBox()
        CType(Me.speedSlider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'loopModeButton
        '
        Me.loopModeButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(176, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.loopModeButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.loopModeButton.Location = New System.Drawing.Point(12, 12)
        Me.loopModeButton.Name = "loopModeButton"
        Me.loopModeButton.Size = New System.Drawing.Size(152, 23)
        Me.loopModeButton.TabIndex = 1
        Me.loopModeButton.Text = "Loop Mode"
        Me.loopModeButton.UseVisualStyleBackColor = False
        '
        'inPointSlipLeftButton
        '
        Me.inPointSlipLeftButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.inPointSlipLeftButton.Location = New System.Drawing.Point(13, 67)
        Me.inPointSlipLeftButton.Name = "inPointSlipLeftButton"
        Me.inPointSlipLeftButton.Size = New System.Drawing.Size(25, 24)
        Me.inPointSlipLeftButton.TabIndex = 3
        Me.inPointSlipLeftButton.Text = "-"
        Me.inPointSlipLeftButton.UseVisualStyleBackColor = True
        '
        'inPointButton
        '
        Me.inPointButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.inPointButton.Location = New System.Drawing.Point(75, 67)
        Me.inPointButton.Name = "inPointButton"
        Me.inPointButton.Size = New System.Drawing.Size(56, 24)
        Me.inPointButton.TabIndex = 5
        Me.inPointButton.Text = "IN"
        Me.inPointButton.UseVisualStyleBackColor = True
        '
        'inPointClearButton
        '
        Me.inPointClearButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.inPointClearButton.Location = New System.Drawing.Point(44, 67)
        Me.inPointClearButton.Name = "inPointClearButton"
        Me.inPointClearButton.Size = New System.Drawing.Size(25, 24)
        Me.inPointClearButton.TabIndex = 4
        Me.inPointClearButton.Text = "X"
        Me.inPointClearButton.UseVisualStyleBackColor = True
        '
        'inPointSlipRightButton
        '
        Me.inPointSlipRightButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.inPointSlipRightButton.Location = New System.Drawing.Point(137, 67)
        Me.inPointSlipRightButton.Name = "inPointSlipRightButton"
        Me.inPointSlipRightButton.Size = New System.Drawing.Size(25, 24)
        Me.inPointSlipRightButton.TabIndex = 6
        Me.inPointSlipRightButton.Text = "+"
        Me.inPointSlipRightButton.UseVisualStyleBackColor = True
        '
        'inTF
        '
        Me.inTF.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.inTF.Location = New System.Drawing.Point(13, 39)
        Me.inTF.Name = "inTF"
        Me.inTF.Size = New System.Drawing.Size(148, 25)
        Me.inTF.TabIndex = 2
        Me.inTF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'outTF
        '
        Me.outTF.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.outTF.Location = New System.Drawing.Point(408, 39)
        Me.outTF.Name = "outTF"
        Me.outTF.Size = New System.Drawing.Size(152, 25)
        Me.outTF.TabIndex = 10
        Me.outTF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'outPointSlipRightButton
        '
        Me.outPointSlipRightButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.outPointSlipRightButton.Location = New System.Drawing.Point(536, 67)
        Me.outPointSlipRightButton.Name = "outPointSlipRightButton"
        Me.outPointSlipRightButton.Size = New System.Drawing.Size(24, 24)
        Me.outPointSlipRightButton.TabIndex = 14
        Me.outPointSlipRightButton.Text = "+"
        Me.outPointSlipRightButton.UseVisualStyleBackColor = True
        '
        'outPointClearButton
        '
        Me.outPointClearButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.outPointClearButton.Location = New System.Drawing.Point(439, 67)
        Me.outPointClearButton.Name = "outPointClearButton"
        Me.outPointClearButton.Size = New System.Drawing.Size(25, 24)
        Me.outPointClearButton.TabIndex = 12
        Me.outPointClearButton.Text = "X"
        Me.outPointClearButton.UseVisualStyleBackColor = True
        '
        'outPointButton
        '
        Me.outPointButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.outPointButton.Location = New System.Drawing.Point(470, 67)
        Me.outPointButton.Name = "outPointButton"
        Me.outPointButton.Size = New System.Drawing.Size(60, 24)
        Me.outPointButton.TabIndex = 13
        Me.outPointButton.Text = "OUT"
        Me.outPointButton.UseVisualStyleBackColor = True
        '
        'outPointSlipLeftButton
        '
        Me.outPointSlipLeftButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.outPointSlipLeftButton.Location = New System.Drawing.Point(408, 67)
        Me.outPointSlipLeftButton.Name = "outPointSlipLeftButton"
        Me.outPointSlipLeftButton.Size = New System.Drawing.Size(25, 24)
        Me.outPointSlipLeftButton.TabIndex = 11
        Me.outPointSlipLeftButton.Text = "-"
        Me.outPointSlipLeftButton.UseVisualStyleBackColor = True
        '
        'alwaysOnTopButton
        '
        Me.alwaysOnTopButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.alwaysOnTopButton.Location = New System.Drawing.Point(408, 12)
        Me.alwaysOnTopButton.Name = "alwaysOnTopButton"
        Me.alwaysOnTopButton.Size = New System.Drawing.Size(152, 23)
        Me.alwaysOnTopButton.TabIndex = 9
        Me.alwaysOnTopButton.Text = "Not Topmost"
        Me.alwaysOnTopButton.UseVisualStyleBackColor = True
        '
        'clearAllPointsButton
        '
        Me.clearAllPointsButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clearAllPointsButton.Location = New System.Drawing.Point(167, 67)
        Me.clearAllPointsButton.Name = "clearAllPointsButton"
        Me.clearAllPointsButton.Size = New System.Drawing.Size(127, 24)
        Me.clearAllPointsButton.TabIndex = 7
        Me.clearAllPointsButton.Text = "CLEAR IN AND OUT"
        Me.clearAllPointsButton.UseVisualStyleBackColor = True
        '
        'currentPositionTF
        '
        Me.currentPositionTF.AutoSize = True
        Me.currentPositionTF.BackColor = System.Drawing.Color.White
        Me.currentPositionTF.Font = New System.Drawing.Font("Segoe UI Semibold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.currentPositionTF.Location = New System.Drawing.Point(174, 13)
        Me.currentPositionTF.MinimumSize = New System.Drawing.Size(160, 26)
        Me.currentPositionTF.Name = "currentPositionTF"
        Me.currentPositionTF.Size = New System.Drawing.Size(160, 28)
        Me.currentPositionTF.TabIndex = 13
        Me.currentPositionTF.Text = "-:--:--.---"
        Me.currentPositionTF.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'currentRemainingStatus
        '
        Me.currentRemainingStatus.AutoSize = True
        Me.currentRemainingStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.currentRemainingStatus.Location = New System.Drawing.Point(171, 48)
        Me.currentRemainingStatus.MaximumSize = New System.Drawing.Size(157, 13)
        Me.currentRemainingStatus.MinimumSize = New System.Drawing.Size(157, 13)
        Me.currentRemainingStatus.Name = "currentRemainingStatus"
        Me.currentRemainingStatus.Size = New System.Drawing.Size(157, 13)
        Me.currentRemainingStatus.TabIndex = 15
        Me.currentRemainingStatus.Text = "CURRENT"
        Me.currentRemainingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'togglePlaylistButton
        '
        Me.togglePlaylistButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.togglePlaylistButton.Location = New System.Drawing.Point(300, 67)
        Me.togglePlaylistButton.Name = "togglePlaylistButton"
        Me.togglePlaylistButton.Size = New System.Drawing.Size(94, 23)
        Me.togglePlaylistButton.TabIndex = 8
        Me.togglePlaylistButton.Text = "SHOW PLAYLIST"
        Me.togglePlaylistButton.UseVisualStyleBackColor = True
        '
        'hotkeysTF
        '
        Me.hotkeysTF.AutoSize = True
        Me.hotkeysTF.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hotkeysTF.Location = New System.Drawing.Point(243, 47)
        Me.hotkeysTF.MaximumSize = New System.Drawing.Size(90, 15)
        Me.hotkeysTF.MinimumSize = New System.Drawing.Size(90, 15)
        Me.hotkeysTF.Name = "hotkeysTF"
        Me.hotkeysTF.Size = New System.Drawing.Size(90, 15)
        Me.hotkeysTF.TabIndex = 17
        Me.hotkeysTF.Text = "HOTKEYS OFF"
        Me.hotkeysTF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'currentPositionBG
        '
        Me.currentPositionBG.AutoSize = True
        Me.currentPositionBG.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.currentPositionBG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.currentPositionBG.Font = New System.Drawing.Font("Segoe UI Semibold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.currentPositionBG.Location = New System.Drawing.Point(174, 13)
        Me.currentPositionBG.MinimumSize = New System.Drawing.Size(220, 26)
        Me.currentPositionBG.Name = "currentPositionBG"
        Me.currentPositionBG.Size = New System.Drawing.Size(220, 30)
        Me.currentPositionBG.TabIndex = 18
        Me.currentPositionBG.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'loopsRepeatTF
        '
        Me.loopsRepeatTF.AutoSize = True
        Me.loopsRepeatTF.BackColor = System.Drawing.Color.White
        Me.loopsRepeatTF.Font = New System.Drawing.Font("Segoe UI Semibold", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.loopsRepeatTF.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.loopsRepeatTF.Location = New System.Drawing.Point(352, 13)
        Me.loopsRepeatTF.MinimumSize = New System.Drawing.Size(40, 26)
        Me.loopsRepeatTF.Name = "loopsRepeatTF"
        Me.loopsRepeatTF.Size = New System.Drawing.Size(40, 28)
        Me.loopsRepeatTF.TabIndex = 19
        Me.loopsRepeatTF.Text = "1"
        Me.loopsRepeatTF.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(338, 48)
        Me.Label1.MaximumSize = New System.Drawing.Size(56, 13)
        Me.Label1.MinimumSize = New System.Drawing.Size(56, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "REPEATS"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'speedSlider
        '
        Me.speedSlider.LargeChange = 10
        Me.speedSlider.Location = New System.Drawing.Point(56, 95)
        Me.speedSlider.Maximum = 200
        Me.speedSlider.Name = "speedSlider"
        Me.speedSlider.Size = New System.Drawing.Size(458, 45)
        Me.speedSlider.SmallChange = 5
        Me.speedSlider.TabIndex = 16
        Me.speedSlider.TickFrequency = 20
        Me.speedSlider.Value = 100
        '
        'speed100Button
        '
        Me.speed100Button.AutoSize = True
        Me.speed100Button.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.speed100Button.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.speed100Button.Location = New System.Drawing.Point(267, 118)
        Me.speed100Button.Name = "speed100Button"
        Me.speed100Button.Size = New System.Drawing.Size(42, 19)
        Me.speed100Button.TabIndex = 21
        Me.speed100Button.Text = "100%"
        Me.speed100Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'speed125Button
        '
        Me.speed125Button.AutoSize = True
        Me.speed125Button.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.speed125Button.Location = New System.Drawing.Point(326, 120)
        Me.speed125Button.Name = "speed125Button"
        Me.speed125Button.Size = New System.Drawing.Size(32, 13)
        Me.speed125Button.TabIndex = 22
        Me.speed125Button.Text = "125%"
        '
        'speed150Button
        '
        Me.speed150Button.AutoSize = True
        Me.speed150Button.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.speed150Button.Location = New System.Drawing.Point(380, 120)
        Me.speed150Button.Name = "speed150Button"
        Me.speed150Button.Size = New System.Drawing.Size(32, 13)
        Me.speed150Button.TabIndex = 23
        Me.speed150Button.Text = "150%"
        '
        'speed175Button
        '
        Me.speed175Button.AutoSize = True
        Me.speed175Button.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.speed175Button.Location = New System.Drawing.Point(435, 120)
        Me.speed175Button.Name = "speed175Button"
        Me.speed175Button.Size = New System.Drawing.Size(32, 13)
        Me.speed175Button.TabIndex = 24
        Me.speed175Button.Text = "175%"
        '
        'speed200Button
        '
        Me.speed200Button.AutoSize = True
        Me.speed200Button.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.speed200Button.Location = New System.Drawing.Point(484, 120)
        Me.speed200Button.Name = "speed200Button"
        Me.speed200Button.Size = New System.Drawing.Size(34, 13)
        Me.speed200Button.TabIndex = 25
        Me.speed200Button.Text = "200%"
        '
        'speed75Button
        '
        Me.speed75Button.AutoSize = True
        Me.speed75Button.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.speed75Button.Location = New System.Drawing.Point(220, 120)
        Me.speed75Button.Name = "speed75Button"
        Me.speed75Button.Size = New System.Drawing.Size(28, 13)
        Me.speed75Button.TabIndex = 20
        Me.speed75Button.Text = "75%"
        '
        'speed50Button
        '
        Me.speed50Button.AutoSize = True
        Me.speed50Button.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.speed50Button.Location = New System.Drawing.Point(166, 120)
        Me.speed50Button.Name = "speed50Button"
        Me.speed50Button.Size = New System.Drawing.Size(28, 13)
        Me.speed50Button.TabIndex = 19
        Me.speed50Button.Text = "50%"
        '
        'speed25Button
        '
        Me.speed25Button.AutoSize = True
        Me.speed25Button.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.speed25Button.Location = New System.Drawing.Point(113, 120)
        Me.speed25Button.Name = "speed25Button"
        Me.speed25Button.Size = New System.Drawing.Size(28, 13)
        Me.speed25Button.TabIndex = 18
        Me.speed25Button.Text = "25%"
        '
        'speed0Button
        '
        Me.speed0Button.AutoSize = True
        Me.speed0Button.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.speed0Button.Location = New System.Drawing.Point(63, 120)
        Me.speed0Button.Name = "speed0Button"
        Me.speed0Button.Size = New System.Drawing.Size(22, 13)
        Me.speed0Button.TabIndex = 17
        Me.speed0Button.Text = "0%"
        '
        'loadOptionsWindowButton
        '
        Me.loadOptionsWindowButton.BackgroundImage = CType(resources.GetObject("loadOptionsWindowButton.BackgroundImage"), System.Drawing.Image)
        Me.loadOptionsWindowButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.loadOptionsWindowButton.Location = New System.Drawing.Point(13, 96)
        Me.loadOptionsWindowButton.Name = "loadOptionsWindowButton"
        Me.loadOptionsWindowButton.Size = New System.Drawing.Size(40, 40)
        Me.loadOptionsWindowButton.TabIndex = 15
        Me.loadOptionsWindowButton.UseVisualStyleBackColor = True
        '
        'showPlayingFileInExplorerButton
        '
        Me.showPlayingFileInExplorerButton.BackgroundImage = Global.MPC_HC_Looper_VB.My.Resources.Resources.Folder_42px_Grey
        Me.showPlayingFileInExplorerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.showPlayingFileInExplorerButton.Location = New System.Drawing.Point(521, 96)
        Me.showPlayingFileInExplorerButton.Name = "showPlayingFileInExplorerButton"
        Me.showPlayingFileInExplorerButton.Size = New System.Drawing.Size(40, 40)
        Me.showPlayingFileInExplorerButton.TabIndex = 26
        Me.showPlayingFileInExplorerButton.UseVisualStyleBackColor = True
        '
        'slipTimer
        '
        '
        'xZoomLabel
        '
        Me.xZoomLabel.AutoSize = True
        Me.xZoomLabel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xZoomLabel.Location = New System.Drawing.Point(296, 154)
        Me.xZoomLabel.MaximumSize = New System.Drawing.Size(91, 13)
        Me.xZoomLabel.Name = "xZoomLabel"
        Me.xZoomLabel.Size = New System.Drawing.Size(51, 13)
        Me.xZoomLabel.TabIndex = 28
        Me.xZoomLabel.Text = "X ZOOM"
        Me.xZoomLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'xZoomTF
        '
        Me.xZoomTF.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.xZoomTF.Location = New System.Drawing.Point(353, 151)
        Me.xZoomTF.Name = "xZoomTF"
        Me.xZoomTF.Size = New System.Drawing.Size(50, 22)
        Me.xZoomTF.TabIndex = 29
        Me.xZoomTF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'yZoomTF
        '
        Me.yZoomTF.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.yZoomTF.Location = New System.Drawing.Point(449, 151)
        Me.yZoomTF.Name = "yZoomTF"
        Me.yZoomTF.Size = New System.Drawing.Size(50, 22)
        Me.yZoomTF.TabIndex = 31
        Me.yZoomTF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'yZoomLabel
        '
        Me.yZoomLabel.AutoSize = True
        Me.yZoomLabel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.yZoomLabel.Location = New System.Drawing.Point(502, 154)
        Me.yZoomLabel.MaximumSize = New System.Drawing.Size(91, 13)
        Me.yZoomLabel.Name = "yZoomLabel"
        Me.yZoomLabel.Size = New System.Drawing.Size(51, 13)
        Me.yZoomLabel.TabIndex = 30
        Me.yZoomLabel.Text = "Y ZOOM"
        Me.yZoomLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'yPosTF
        '
        Me.yPosTF.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.yPosTF.Location = New System.Drawing.Point(188, 151)
        Me.yPosTF.Name = "yPosTF"
        Me.yPosTF.Size = New System.Drawing.Size(50, 22)
        Me.yPosTF.TabIndex = 28
        Me.yPosTF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'yPosLabel
        '
        Me.yPosLabel.AutoSize = True
        Me.yPosLabel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.yPosLabel.Location = New System.Drawing.Point(128, 154)
        Me.yPosLabel.MaximumSize = New System.Drawing.Size(91, 13)
        Me.yPosLabel.Name = "yPosLabel"
        Me.yPosLabel.Size = New System.Drawing.Size(55, 13)
        Me.yPosLabel.TabIndex = 34
        Me.yPosLabel.Text = "Y OFFSET"
        Me.yPosLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'xPosTF
        '
        Me.xPosTF.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.xPosTF.Location = New System.Drawing.Point(73, 151)
        Me.xPosTF.Name = "xPosTF"
        Me.xPosTF.Size = New System.Drawing.Size(50, 22)
        Me.xPosTF.TabIndex = 27
        Me.xPosTF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'xPosLabel
        '
        Me.xPosLabel.AutoSize = True
        Me.xPosLabel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xPosLabel.Location = New System.Drawing.Point(14, 154)
        Me.xPosLabel.MaximumSize = New System.Drawing.Size(91, 13)
        Me.xPosLabel.Name = "xPosLabel"
        Me.xPosLabel.Size = New System.Drawing.Size(55, 13)
        Me.xPosLabel.TabIndex = 32
        Me.xPosLabel.Text = "X OFFSET"
        Me.xPosLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'zoomAxesLinkButton
        '
        Me.zoomAxesLinkButton.Appearance = System.Windows.Forms.Appearance.Button
        Me.zoomAxesLinkButton.AutoSize = True
        Me.zoomAxesLinkButton.Checked = True
        Me.zoomAxesLinkButton.CheckState = System.Windows.Forms.CheckState.Checked
        Me.zoomAxesLinkButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.zoomAxesLinkButton.Location = New System.Drawing.Point(409, 150)
        Me.zoomAxesLinkButton.Name = "zoomAxesLinkButton"
        Me.zoomAxesLinkButton.Size = New System.Drawing.Size(39, 23)
        Me.zoomAxesLinkButton.TabIndex = 30
        Me.zoomAxesLinkButton.Text = "LINK"
        Me.zoomAxesLinkButton.UseVisualStyleBackColor = True
        '
        'mainWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(565, 187)
        Me.Controls.Add(Me.zoomAxesLinkButton)
        Me.Controls.Add(Me.yPosTF)
        Me.Controls.Add(Me.yPosLabel)
        Me.Controls.Add(Me.xPosTF)
        Me.Controls.Add(Me.xPosLabel)
        Me.Controls.Add(Me.yZoomTF)
        Me.Controls.Add(Me.yZoomLabel)
        Me.Controls.Add(Me.xZoomTF)
        Me.Controls.Add(Me.xZoomLabel)
        Me.Controls.Add(Me.showPlayingFileInExplorerButton)
        Me.Controls.Add(Me.loadOptionsWindowButton)
        Me.Controls.Add(Me.speed75Button)
        Me.Controls.Add(Me.speed50Button)
        Me.Controls.Add(Me.speed25Button)
        Me.Controls.Add(Me.speed0Button)
        Me.Controls.Add(Me.speed200Button)
        Me.Controls.Add(Me.speed175Button)
        Me.Controls.Add(Me.speed150Button)
        Me.Controls.Add(Me.speed125Button)
        Me.Controls.Add(Me.speed100Button)
        Me.Controls.Add(Me.speedSlider)
        Me.Controls.Add(Me.currentPositionBG)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.loopsRepeatTF)
        Me.Controls.Add(Me.currentPositionTF)
        Me.Controls.Add(Me.hotkeysTF)
        Me.Controls.Add(Me.togglePlaylistButton)
        Me.Controls.Add(Me.currentRemainingStatus)
        Me.Controls.Add(Me.clearAllPointsButton)
        Me.Controls.Add(Me.outTF)
        Me.Controls.Add(Me.outPointSlipRightButton)
        Me.Controls.Add(Me.outPointClearButton)
        Me.Controls.Add(Me.outPointButton)
        Me.Controls.Add(Me.outPointSlipLeftButton)
        Me.Controls.Add(Me.alwaysOnTopButton)
        Me.Controls.Add(Me.inTF)
        Me.Controls.Add(Me.inPointSlipRightButton)
        Me.Controls.Add(Me.inPointClearButton)
        Me.Controls.Add(Me.inPointButton)
        Me.Controls.Add(Me.inPointSlipLeftButton)
        Me.Controls.Add(Me.loopModeButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(20, 20)
        Me.MaximumSize = New System.Drawing.Size(581, 226)
        Me.MinimumSize = New System.Drawing.Size(581, 226)
        Me.Name = "mainWindow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "mainWindow"
        Me.TransparencyKey = System.Drawing.Color.PeachPuff
        CType(Me.speedSlider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents loopModeButton As Button
    Friend WithEvents inPointSlipLeftButton As Button
    Friend WithEvents inPointButton As Button
    Friend WithEvents inPointClearButton As Button
    Friend WithEvents inPointSlipRightButton As Button
    Friend WithEvents inTF As TextBox
    Friend WithEvents outTF As TextBox
    Friend WithEvents outPointSlipRightButton As Button
    Friend WithEvents outPointClearButton As Button
    Friend WithEvents outPointButton As Button
    Friend WithEvents outPointSlipLeftButton As Button
    Friend WithEvents alwaysOnTopButton As Button
    Friend WithEvents clearAllPointsButton As Button
    Friend WithEvents currentPositionTF As Label
    Friend WithEvents currentRemainingStatus As Label
    Friend WithEvents togglePlaylistButton As Button
    Friend WithEvents hotkeysTF As Label
    Friend WithEvents currentPositionBG As Label
    Friend WithEvents loopsRepeatTF As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents speedSlider As TrackBar
    Friend WithEvents speed100Button As Label
    Friend WithEvents speed125Button As Label
    Friend WithEvents speed150Button As Label
    Friend WithEvents speed175Button As Label
    Friend WithEvents speed200Button As Label
    Friend WithEvents speed75Button As Label
    Friend WithEvents speed50Button As Label
    Friend WithEvents speed25Button As Label
    Friend WithEvents speed0Button As Label
    Friend WithEvents loadOptionsWindowButton As Button
    Friend WithEvents showPlayingFileInExplorerButton As Button
    Friend WithEvents toolTips As ToolTip
    Friend WithEvents slipTimer As Timer
    Friend WithEvents xZoomLabel As Label
    Friend WithEvents xZoomTF As TextBox
    Friend WithEvents yZoomTF As TextBox
    Friend WithEvents yZoomLabel As Label
    Friend WithEvents yPosTF As TextBox
    Friend WithEvents yPosLabel As Label
    Friend WithEvents xPosTF As TextBox
    Friend WithEvents xPosLabel As Label
    Friend WithEvents zoomAxesLinkButton As CheckBox
End Class

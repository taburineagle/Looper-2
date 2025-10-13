<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class hotKeySettings
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
        Me.currentHKTF = New System.Windows.Forms.TextBox()
        Me.currentHKLabel = New System.Windows.Forms.Label()
        Me.currentHKDesc = New System.Windows.Forms.Label()
        Me.newHKDesc = New System.Windows.Forms.Label()
        Me.newHKTF = New System.Windows.Forms.TextBox()
        Me.newHKLabel = New System.Windows.Forms.Label()
        Me.setNewHKButton = New System.Windows.Forms.Button()
        Me.setDefaultHKButton = New System.Windows.Forms.Button()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.hotKeysLV = New System.Windows.Forms.ListView()
        Me.hotKeyColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.resetAllButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'currentHKTF
        '
        Me.currentHKTF.BackColor = System.Drawing.Color.Gainsboro
        Me.currentHKTF.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.currentHKTF.Location = New System.Drawing.Point(11, 288)
        Me.currentHKTF.Name = "currentHKTF"
        Me.currentHKTF.ReadOnly = True
        Me.currentHKTF.Size = New System.Drawing.Size(48, 22)
        Me.currentHKTF.TabIndex = 2
        Me.currentHKTF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'currentHKLabel
        '
        Me.currentHKLabel.AutoSize = True
        Me.currentHKLabel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.currentHKLabel.Location = New System.Drawing.Point(8, 272)
        Me.currentHKLabel.Name = "currentHKLabel"
        Me.currentHKLabel.Size = New System.Drawing.Size(102, 13)
        Me.currentHKLabel.TabIndex = 37
        Me.currentHKLabel.Text = "CURRENT HOTKEY"
        Me.currentHKLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'currentHKDesc
        '
        Me.currentHKDesc.AutoSize = True
        Me.currentHKDesc.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.currentHKDesc.Location = New System.Drawing.Point(66, 292)
        Me.currentHKDesc.Name = "currentHKDesc"
        Me.currentHKDesc.Size = New System.Drawing.Size(0, 13)
        Me.currentHKDesc.TabIndex = 38
        Me.currentHKDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'newHKDesc
        '
        Me.newHKDesc.AutoSize = True
        Me.newHKDesc.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.newHKDesc.Location = New System.Drawing.Point(240, 292)
        Me.newHKDesc.Name = "newHKDesc"
        Me.newHKDesc.Size = New System.Drawing.Size(0, 13)
        Me.newHKDesc.TabIndex = 41
        Me.newHKDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'newHKTF
        '
        Me.newHKTF.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.newHKTF.Location = New System.Drawing.Point(185, 288)
        Me.newHKTF.Name = "newHKTF"
        Me.newHKTF.Size = New System.Drawing.Size(48, 22)
        Me.newHKTF.TabIndex = 3
        Me.newHKTF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'newHKLabel
        '
        Me.newHKLabel.AutoSize = True
        Me.newHKLabel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.newHKLabel.Location = New System.Drawing.Point(182, 272)
        Me.newHKLabel.Name = "newHKLabel"
        Me.newHKLabel.Size = New System.Drawing.Size(78, 13)
        Me.newHKLabel.TabIndex = 40
        Me.newHKLabel.Text = "NEW HOTKEY"
        Me.newHKLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'setNewHKButton
        '
        Me.setNewHKButton.Enabled = False
        Me.setNewHKButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.setNewHKButton.Location = New System.Drawing.Point(185, 314)
        Me.setNewHKButton.Name = "setNewHKButton"
        Me.setNewHKButton.Size = New System.Drawing.Size(125, 24)
        Me.setNewHKButton.TabIndex = 5
        Me.setNewHKButton.Text = "SET NEW HOTKEY"
        Me.setNewHKButton.UseVisualStyleBackColor = True
        '
        'setDefaultHKButton
        '
        Me.setDefaultHKButton.Enabled = False
        Me.setDefaultHKButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.setDefaultHKButton.Location = New System.Drawing.Point(12, 314)
        Me.setDefaultHKButton.Name = "setDefaultHKButton"
        Me.setDefaultHKButton.Size = New System.Drawing.Size(119, 24)
        Me.setDefaultHKButton.TabIndex = 4
        Me.setDefaultHKButton.Text = "SET TO DEFAULT"
        Me.setDefaultHKButton.UseVisualStyleBackColor = True
        '
        'OKButton
        '
        Me.OKButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OKButton.Location = New System.Drawing.Point(265, 344)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(98, 24)
        Me.OKButton.TabIndex = 7
        Me.OKButton.Text = "OK"
        Me.OKButton.UseVisualStyleBackColor = True
        '
        'hotKeysLV
        '
        Me.hotKeysLV.BackColor = System.Drawing.Color.White
        Me.hotKeysLV.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.hotKeyColumn})
        Me.hotKeysLV.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.hotKeysLV.FullRowSelect = True
        Me.hotKeysLV.HideSelection = False
        Me.hotKeysLV.Location = New System.Drawing.Point(-2, -1)
        Me.hotKeysLV.MultiSelect = False
        Me.hotKeysLV.Name = "hotKeysLV"
        Me.hotKeysLV.Size = New System.Drawing.Size(376, 258)
        Me.hotKeysLV.TabIndex = 1
        Me.hotKeysLV.UseCompatibleStateImageBehavior = False
        Me.hotKeysLV.View = System.Windows.Forms.View.Details
        '
        'hotKeyColumn
        '
        Me.hotKeyColumn.Text = "Hotkey"
        Me.hotKeyColumn.Width = 350
        '
        'resetAllButton
        '
        Me.resetAllButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.resetAllButton.Location = New System.Drawing.Point(12, 344)
        Me.resetAllButton.Name = "resetAllButton"
        Me.resetAllButton.Size = New System.Drawing.Size(247, 24)
        Me.resetAllButton.TabIndex = 6
        Me.resetAllButton.Text = "RESET ALL HOTKEYS TO DEFAULTS"
        Me.resetAllButton.UseVisualStyleBackColor = True
        '
        'hotKeySettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 375)
        Me.Controls.Add(Me.resetAllButton)
        Me.Controls.Add(Me.hotKeysLV)
        Me.Controls.Add(Me.OKButton)
        Me.Controls.Add(Me.setDefaultHKButton)
        Me.Controls.Add(Me.setNewHKButton)
        Me.Controls.Add(Me.newHKDesc)
        Me.Controls.Add(Me.newHKTF)
        Me.Controls.Add(Me.newHKLabel)
        Me.Controls.Add(Me.currentHKDesc)
        Me.Controls.Add(Me.currentHKTF)
        Me.Controls.Add(Me.currentHKLabel)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(391, 414)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(391, 414)
        Me.Name = "hotKeySettings"
        Me.Text = "Set Hotkeys"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents currentHKTF As TextBox
    Friend WithEvents currentHKLabel As Label
    Friend WithEvents currentHKDesc As Label
    Friend WithEvents newHKDesc As Label
    Friend WithEvents newHKTF As TextBox
    Friend WithEvents newHKLabel As Label
    Friend WithEvents setNewHKButton As Button
    Friend WithEvents setDefaultHKButton As Button
    Friend WithEvents OKButton As Button
    Friend WithEvents hotKeysLV As ListView
    Friend WithEvents hotKeyColumn As ColumnHeader
    Friend WithEvents resetAllButton As Button
End Class

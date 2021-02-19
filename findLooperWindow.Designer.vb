<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class findLooperWindow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(findLooperWindow))
        Me.looperPic = New System.Windows.Forms.PictureBox()
        Me.looperTextHeader = New System.Windows.Forms.Label()
        Me.looperTextDescription = New System.Windows.Forms.TextBox()
        Me.MPCEXEPathButton = New System.Windows.Forms.Button()
        Me.MPCEXEPathTF = New System.Windows.Forms.Label()
        Me.MPCEXELabel = New System.Windows.Forms.Label()
        Me.saveChangesButton = New System.Windows.Forms.Button()
        CType(Me.looperPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'looperPic
        '
        Me.looperPic.Image = Global.MPC_HC_Looper_VB.My.Resources.Resources.Looper_Logo_Large
        Me.looperPic.Location = New System.Drawing.Point(12, 12)
        Me.looperPic.Name = "looperPic"
        Me.looperPic.Size = New System.Drawing.Size(128, 128)
        Me.looperPic.TabIndex = 0
        Me.looperPic.TabStop = False
        '
        'looperTextHeader
        '
        Me.looperTextHeader.AutoSize = True
        Me.looperTextHeader.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.looperTextHeader.Location = New System.Drawing.Point(142, 8)
        Me.looperTextHeader.Name = "looperTextHeader"
        Me.looperTextHeader.Size = New System.Drawing.Size(372, 25)
        Me.looperTextHeader.TabIndex = 1
        Me.looperTextHeader.Text = "Welcome to Looper 2 by Zach Glenwright!"
        '
        'looperTextDescription
        '
        Me.looperTextDescription.BackColor = System.Drawing.SystemColors.Control
        Me.looperTextDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.looperTextDescription.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.looperTextDescription.Location = New System.Drawing.Point(145, 34)
        Me.looperTextDescription.Multiline = True
        Me.looperTextDescription.Name = "looperTextDescription"
        Me.looperTextDescription.ReadOnly = True
        Me.looperTextDescription.Size = New System.Drawing.Size(455, 116)
        Me.looperTextDescription.TabIndex = 2
        Me.looperTextDescription.Text = resources.GetString("looperTextDescription.Text")
        '
        'MPCEXEPathButton
        '
        Me.MPCEXEPathButton.Location = New System.Drawing.Point(13, 157)
        Me.MPCEXEPathButton.Name = "MPCEXEPathButton"
        Me.MPCEXEPathButton.Size = New System.Drawing.Size(30, 27)
        Me.MPCEXEPathButton.TabIndex = 4
        Me.MPCEXEPathButton.Text = "..."
        Me.MPCEXEPathButton.UseVisualStyleBackColor = True
        '
        'MPCEXEPathTF
        '
        Me.MPCEXEPathTF.AutoSize = True
        Me.MPCEXEPathTF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MPCEXEPathTF.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MPCEXEPathTF.Location = New System.Drawing.Point(47, 158)
        Me.MPCEXEPathTF.MaximumSize = New System.Drawing.Size(545, 25)
        Me.MPCEXEPathTF.MinimumSize = New System.Drawing.Size(545, 25)
        Me.MPCEXEPathTF.Name = "MPCEXEPathTF"
        Me.MPCEXEPathTF.Size = New System.Drawing.Size(545, 25)
        Me.MPCEXEPathTF.TabIndex = 5
        Me.MPCEXEPathTF.Text = "< Click on the button to select either MPC-HC or MPC-BE's .exe file"
        Me.MPCEXEPathTF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MPCEXELabel
        '
        Me.MPCEXELabel.AutoSize = True
        Me.MPCEXELabel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MPCEXELabel.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.MPCEXELabel.Location = New System.Drawing.Point(45, 187)
        Me.MPCEXELabel.Name = "MPCEXELabel"
        Me.MPCEXELabel.Size = New System.Drawing.Size(155, 13)
        Me.MPCEXELabel.TabIndex = 3
        Me.MPCEXELabel.Text = "PATH TO MPC-HC OR MPC-BE"
        '
        'saveChangesButton
        '
        Me.saveChangesButton.Enabled = False
        Me.saveChangesButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.saveChangesButton.Location = New System.Drawing.Point(376, 191)
        Me.saveChangesButton.Name = "saveChangesButton"
        Me.saveChangesButton.Size = New System.Drawing.Size(216, 28)
        Me.saveChangesButton.TabIndex = 6
        Me.saveChangesButton.Text = "Save Changes and Run Looper"
        Me.saveChangesButton.UseVisualStyleBackColor = True
        '
        'findLooperWindow
        '
        Me.AcceptButton = Me.saveChangesButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(602, 228)
        Me.Controls.Add(Me.saveChangesButton)
        Me.Controls.Add(Me.MPCEXEPathTF)
        Me.Controls.Add(Me.MPCEXEPathButton)
        Me.Controls.Add(Me.MPCEXELabel)
        Me.Controls.Add(Me.looperTextDescription)
        Me.Controls.Add(Me.looperTextHeader)
        Me.Controls.Add(Me.looperPic)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Location = New System.Drawing.Point(0, -250)
        Me.Name = "findLooperWindow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Find MPC-HC/BE"
        CType(Me.looperPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents looperPic As PictureBox
    Friend WithEvents looperTextHeader As Label
    Friend WithEvents looperTextDescription As TextBox
    Friend WithEvents MPCEXEPathButton As Button
    Friend WithEvents MPCEXEPathTF As Label
    Friend WithEvents MPCEXELabel As Label
    Friend WithEvents saveChangesButton As Button
End Class

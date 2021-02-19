Public Class ListViewDoubleBuffered
    Inherits ListView
    Public Sub New()
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
    End Sub
End Class
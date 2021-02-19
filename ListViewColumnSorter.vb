Public Class ListViewColumnSorter
    Implements System.Collections.IComparer

    Private _ColumnIndex As Integer
    Private _SortingOrder As SortOrder
    Private ItemComparer As CaseInsensitiveComparer

    Public Sub New()
        _ColumnIndex = 0
        _SortingOrder = SortOrder.None
        ItemComparer = New CaseInsensitiveComparer()
    End Sub

    Public Property ColumnIndex() As Integer
        Get
            Return _ColumnIndex
        End Get
        Set(ByVal Value As Integer)
            _ColumnIndex = Value
        End Set
    End Property

    Public Property SortingOrder() As SortOrder
        Get
            Return _SortingOrder
        End Get
        Set(ByVal Value As SortOrder)
            _SortingOrder = Value
        End Set
    End Property

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim compareResult As Integer
        Dim LviStrX As String = DirectCast(x, ListViewItem).SubItems(_ColumnIndex).Text
        Dim LviStrY As String = DirectCast(y, ListViewItem).SubItems(_ColumnIndex).Text

        Dim numX, numY As Integer
        Dim dtX, dtY As Date

        If Integer.TryParse(LviStrX, numX) AndAlso Integer.TryParse(LviStrY, numY) Then
            compareResult = ItemComparer.Compare(numX, numY)
        ElseIf Date.TryParse(LviStrX, dtX) AndAlso Date.TryParse(LviStrY, dtY) Then
            compareResult = ItemComparer.Compare(dtX, dtY)
        Else
            compareResult = ItemComparer.Compare(LviStrX, LviStrY)
        End If

        If _SortingOrder = SortOrder.Ascending Then
            Return compareResult
        ElseIf _SortingOrder = SortOrder.Descending Then
            Return -compareResult
        Else
            Return 0
        End If
    End Function
End Class
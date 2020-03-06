'Public Class GUI_Group
'    Public Visable As Boolean
'    Public Location As Point
'    Public Size As Size
'    Public Name As String
'End Class
Public Class GUI_TextBox
    Public Visable As Boolean
    Public Location As Point
    Public Size As Size
    Public Text As String
    Public PassWord As Boolean
    'Public ENonly As Boolean
End Class
Public Class GUI_Lable
    Public Text As String
    Public Location As Point
    Public Visable As Boolean
End Class
Public Class GUI_Button
    Public Visable As Boolean
    Public Location As Point
    Public Size As Size
    Public Text As String
End Class
Public Class GUI_NumberSelect
    Public Visable As Boolean
    Public Location As Point
    Public Value As Integer
End Class
Public Class GUI_Bar
    Public Visable As Boolean
    Public Location As Point
    Public Size As Size
    Public Max As Integer
    Public Value As Integer
End Class
Public Class GUI_GButton
    Public Visable As Boolean
    Public Location As Point
    Public Size As Size
    Public Graphic() As Point
End Class
Public Class GUI_MsgBox
    Public Visable As Boolean
    Public Location As Point
    Public Size As Size
    Public Title As String
    Public Msg As String
    Public Button As GUI_Button
End Class
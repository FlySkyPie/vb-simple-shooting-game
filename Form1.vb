Public Class Form1



    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Gaming Then
            e.Handled = True
            If e.KeyValue = Keys.W Then key_up = True
            If e.KeyValue = Keys.A Then key_left = True
            If e.KeyValue = Keys.D Then key_right = True
            If e.KeyValue = Keys.S Then key_down = True
            If e.KeyValue = Keys.R Then
                If Not UserObj.Reloading Then
                    UserObj.Reload_Count = UserObj.Weapon(0).ReloadTime
                    UserObj.Reloading = True
                End If
            End If
            If e.KeyValue = Keys.ShiftKey Then UserObj.Running = True

        End If
    End Sub
    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If Gaming Then
            e.Handled = True
            If e.KeyValue = Keys.W Then key_up = False
            If e.KeyValue = Keys.A Then key_left = False
            If e.KeyValue = Keys.D Then key_right = False
            If e.KeyValue = Keys.S Then key_down = False
            If e.KeyValue = Keys.ShiftKey Then UserObj.Running = False
        End If
    End Sub
    Private Sub Form1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If TypeTaging Then
            If Asc(e.KeyChar) >= 32 And Asc(e.KeyChar) <= 126 Then
                TypeTag.Text += e.KeyChar

            ElseIf e.KeyChar = vbBack Then
                Dim _str As String = TypeTag.Text
                If _str.Length >= 1 Then
                    TypeTag.Text = Mid(_str, 1, _str.Length - 1)
                End If
            End If
        End If
    End Sub




    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Initialize_GUI_Object()
        Load_GUI_Objects()
        Initialize_Screen()
        Initialize_Timer()
    End Sub
End Class

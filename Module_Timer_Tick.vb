Imports System.Timers
Module Module_Timer_Tick
    Public Sub Timer_Game_Tick(ByVal sender As System.Object, ByVal e As ElapsedEventArgs)
        UserObj.Moving = (key_up Or key_down Or key_right Or key_left)
        If UserObj.Moving Then
            Dim _move_x, _move_y As Integer
            _move_x = 0 : _move_y = 0
            If key_up Then _move_y = _move_y - 1
            If key_down Then _move_y = _move_y + 1
            If key_right Then _move_x = _move_x + 1
            If key_left Then _move_x = _move_x - 1
            UserObj.Move_direct = Math.Atan2(_move_y, _move_x)
        Else
        End If

        '遊戲運算
        '玩家的運算

        '移動運算
        If UserObj.Running And UserObj.MP >= 5 Then
            UserObj.MP += -5
            Dim speed As Single = 0.085 'M per 10m Sec(M/10mS)  '考慮系統運算作出的妥協
            UserObj.X = UserObj.X + speed * Math.Cos(UserObj.Angle_W)
            UserObj.Y = UserObj.Y + speed * Math.Sin(UserObj.Angle_W)
        ElseIf UserObj.Moving Then
            Dim speed As Single = 0.045 'M per 10m Sec(M/10mS)  '考慮系統運算作出的妥協
            UserObj.X = UserObj.X + speed * Math.Cos(UserObj.Move_direct)
            UserObj.Y = UserObj.Y + speed * Math.Sin(UserObj.Move_direct)
        End If
        '開槍運算
        If UserObj.Attack_count > 0 Then
            UserObj.Attack_count += -1
        End If

        If UserObj.Reloading Then
            If UserObj.Reload_Count > 0 Then
                UserObj.Reload_Count += -1
            Else
                UserObj.Reloading = False
                If UserObj.Ammo_Amount(0) > 0 Then
                    UserObj.Ammo_Amount(0) = 31
                Else
                    UserObj.Ammo_Amount(0) = 30
                End If
            End If
        End If

        If UserObj.Attacking And Not UserObj.Reloading Then
            If UserObj.Attack_count = 0 Then '冷卻完畢 開槍
                Dim _X = UserObj.X + 0.4 * Math.Cos(UserObj.Angle_W)
                Dim _Y = UserObj.Y + 0.4 * Math.Sin(UserObj.Angle_W)
                Dim _Angle = UserObj.Angle_W + UserObj.Weapon(0).Shift * NormSInv(Rnd) '常態分布著彈
                Create_BulletObj(UserObj.NickName, _X, _Y, _Angle, UserObj.Weapon(0))
                UserObj.Attack_count = UserObj.Weapon(0).Delay
                UserObj.Ammo_Amount(0) += -1
                If UserObj.Ammo_Amount(0) = 0 Then
                    UserObj.Reload_Count = UserObj.Weapon(0).ReloadTime
                    UserObj.Reloading = True
                End If
            End If
        End If
        ' 體力回覆
        If UserObj.MP < 100 Then
            UserObj.MP += 1
        End If

        '飛行子彈運算
        For i = 0 To Max_Bullet
            If BulletObj(i).Enable Then
                '讓子彈飛~~
                BulletObj(i).X = BulletObj(i).X + BulletObj(i).Speed * Math.Cos(BulletObj(i).Angle)
                BulletObj(i).Y = BulletObj(i).Y + BulletObj(i).Speed * Math.Sin(BulletObj(i).Angle)
                '飛太遠啦~銷毀子彈
                BulletObj(i).Count += 1
                If BulletObj(i).Count > 100 Then
                    BulletObj(i).Enable = False
                End If
                '命中判定
                Dim rang As Single
                rang = (UserObj.X - BulletObj(i).X) ^ 2 + (UserObj.Y - BulletObj(i).Y) ^ 2
                rang = rang ^ 0.5
                If rang <= 0.3 Then    '命中玩家
                    BulletObj(i).Enable = False
                    UserObj.HP = UserObj.HP - BulletObj(i).Damage
                    If UserObj.HP <= 0 Then  '死亡判定
                        UserObj.HP = 0
                    End If
                    Exit For
                End If
            End If
        Next
    End Sub
    Public Sub Timer_GUI_Tick(ByVal sender As System.Object, ByVal e As ElapsedEventArgs)
        Timer_GUI.Enabled = False
        Dim GUI As Bitmap
        GUI = New Bitmap(832, 600)
        Dim g As Graphics
        g = Graphics.FromImage(GUI)
        g.Clear(Color.FromArgb(32, 32, 32))

        If Gaming Then
            Draw_Game(GUI)
        End If
        Draw_GUI_Objects(GUI)
        If Loading Then
            Draw_Loading_Animation(GUI)
        End If
        Screen.Image = GUI
        Timer_GUI.Enabled = True
    End Sub
    Private Sub Draw_GUI_Objects(ByVal _bitmap As Bitmap)
        Dim drawFont As New Font("Arial", 9)
        Dim drawBrush As New SolidBrush(Color.FromArgb(64, 225, 225))
        Dim drawpen As New Pen(drawBrush)
        Dim g As Graphics
        g = Graphics.FromImage(_bitmap)
        '繪製文字
        For i = 0 To MAX_Lable
            Dim _Font As New Font("Arial", 9)
            Dim _Brush As New SolidBrush(Color.FromArgb(64, 225, 225))
            If Not G_Lable(i).Visable = Nothing Then
                g.DrawString(G_Lable(i).Text, _Font, _Brush, G_Lable(i).Location)
            End If
        Next
        '繪製文字方塊
        For i = 0 To MAX_TextBox
            Dim _Font As New Font("Arial", 9)
            Dim _Brush As New SolidBrush(Color.FromArgb(64, 225, 225))
            Dim _pen As New Pen(_Brush)
            If G_Text(i).Visable Then
                g.DrawRectangle(_pen, G_Text(i).Location.X, G_Text(i).Location.Y, G_Text(i).Size.Width, G_Text(i).Size.Height)
                If G_Text(i).PassWord Then
                    Dim _str As String = ""
                    For j = 1 To G_Text(i).Text.Length
                        _str += "*"
                    Next
                    g.DrawString(_str, _Font, _Brush, G_Text(i).Location)
                Else
                    g.DrawString(G_Text(i).Text, _Font, _Brush, G_Text(i).Location)
                End If
            End If
        Next
        '繪製按鈕
        For i = 0 To MAX_Button
            If G_Button(i).Visable Then
                Dim _Brush As New SolidBrush(Color.FromArgb(64, 225, 225))
                Dim _pen As New Pen(_Brush)
                Dim _Font As New Font("Arial", 12)
                _pen.Width = 2
                g.DrawRectangle(_pen, G_Button(i).Location.X, G_Button(i).Location.Y, G_Button(i).Size.Width, G_Button(i).Size.Height)
                _pen.Width = 1
                g.DrawRectangle(_pen, G_Button(i).Location.X + 4, G_Button(i).Location.Y + 4, G_Button(i).Size.Width - 8, G_Button(i).Size.Height - 8)
                Dim Lx, Ly As Integer
                Lx = G_Button(i).Location.X + 5 'G_Button(i).Location.X + G_Button(i).Size.Width / 2 - (G_Button(i).Text.Length) / 2 * 16
                Ly = G_Button(i).Location.Y + G_Button(i).Size.Height / 2 - 9 '- (G_Button(i).Text.Length) / 2 * 10
                g.DrawString(G_Button(i).Text, _Font, _Brush, Lx, Ly)
            End If
        Next

        If TypeTaging Then  '繪製文字標記框
            If TypeShiny <= 12 Then
                TypeShiny += 1
                If TypeShiny <= 8 Then
                    drawpen.Color = Color.FromArgb(96, 250, 250)
                    drawpen.Width = 2
                    g.DrawRectangle(drawpen, TypeTag.Location.X, TypeTag.Location.Y, TypeTag.Size.Width, TypeTag.Size.Height)
                End If
            Else
                TypeShiny = 0
            End If
        End If
    End Sub
    Public Sub Draw_Loading_Animation(ByVal _bitmap As Bitmap)
        Dim teeths As Integer = 8
        If Animation_frame < 10 Then
            Animation_frame += 1
        Else
            Animation_frame = 0
        End If
        Dim drawFont As New Font("Arial", 9)
        Dim drawBrush As New SolidBrush(Color.FromArgb(64, 225, 225))
        Dim drawpen As New Pen(drawBrush)
        Dim g As Graphics
        g = Graphics.FromImage(_bitmap)
        drawBrush.Color = Color.FromArgb(64, 225, 225)
        'draw a gear
        For i = 0 To teeths * 5 - 1
            Dim angle As Double = 2 * 3.1415926 / (teeths * 5) * i + Animation_frame * 2 * 3.1415926 / teeths / 11
            Dim x, y, R As Integer
            Dim _d As Double = 0.05
            If i Mod 5 = 0 Then
                R = 120
                angle -= _d
            ElseIf (i - 1) Mod 5 = 0 Then
                R = 120
                angle += _d
            Else
                R = 80
            End If
            x = Int(R * Math.Cos(angle)) + 390
            y = Int(R * Math.Sin(angle)) + 325
            LOGO_Gear(i) = New Point(x, y)
        Next
        g.DrawPolygon(drawpen, LOGO_Gear)
        g.DrawEllipse(drawpen, 390 - 40, 325 - 40, 80, 80)
        g.DrawPolygon(drawpen, LOGO_Cat)
    End Sub
    Public Sub Draw_Game(ByVal _bitmap As Bitmap)
        If DrawTime >= 20 Then
            If (Now.Minute > Tq.Minute) Then
                fps = 1 / (60 + Now.Second + Now.Millisecond / 1000 - Tq.Second - Tq.Millisecond / 1000)
            Else
                fps = 1 / (Now.Second + Now.Millisecond / 1000 - Tq.Second - Tq.Millisecond / 1000)
            End If
            fps = Int(fps * 20 * 10) / 10
            Tq = Now
            DrawTime = 1
        Else
            DrawTime += 1
        End If
        '繪圖
        Dim drawFont As New Font("Arial", 9)
        Dim drawBrush As New SolidBrush(Color.FromArgb(64, 225, 225))
        Dim drawpen As New Pen(drawBrush)
        Dim g As Graphics
        g = Graphics.FromImage(_bitmap)
        drawBrush.Color = Color.FromArgb(64, 225, 225)
        '-圖像
        '--定位攝影機
        Camera_X = UserObj.X + Mouse_Rang / Ratio / 2 * Math.Cos(UserObj.Angle_W)
        Camera_Y = UserObj.Y + Mouse_Rang / Ratio / 2 * Math.Sin(UserObj.Angle_W)
        '--攝影機看得到的範圍
        Dim _Y_min, _Y_max, _X_min, _X_max As Single
        _X_min = Camera_X - Screen.Width / 2 / Ratio
        _X_max = Camera_X + Screen.Width / 2 / Ratio
        _Y_min = Camera_Y - Screen.Height / 2 / Ratio
        _Y_max = Camera_Y + Screen.Height / 2 / Ratio
        '---繪製地圖線
        drawpen.Color = Color.FromArgb(16, 64, 70)
        Dim hh, Sx, Sy As Integer
        Sx = Screen.Width / 2
        Sy = Screen.Height / 2
        hh = Int((Fix(Camera_X) - Camera_X) * Ratio)
        Dim tw, th As Integer
        tw = Int(Screen.Width / Ratio / 2)
        th = Int(Screen.Height / Ratio / 2)
        For k = -tw To tw
            Dim pp1 As Point = New Point(Sx + hh + k * Ratio, 0)
            Dim pp2 As Point = New Point(Sx + hh + k * Ratio, Screen.Height)
            g.DrawLine(drawpen, pp1, pp2)
        Next
        hh = Int((Fix(Camera_Y) - Camera_Y) * Ratio)
        For k = -th To th
            Dim pp1 As Point = New Point(0, Sy + hh + k * Ratio)
            Dim pp2 As Point = New Point(Screen.Width, Sy + hh + k * Ratio)
            g.DrawLine(drawpen, pp1, pp2)
        Next
        '---繪製玩家
        drawpen.Color = Color.FromArgb(64, 255, 255)
        Dim i As Integer


        Dim c_x, c_y As Integer
        c_x = Int(Screen.Width / 2) + (UserObj.X - Camera_X) * Ratio
        c_y = Int(Screen.Height / 2) + (UserObj.Y - Camera_Y) * Ratio
        Dim _R As Single = 0.3 * Ratio
        g.DrawEllipse(drawpen, c_x - _R, c_y - _R, _R * 2, _R * 2)
        Dim p1 As Point = New Point(c_x, c_y)
        Dim p2 As Point = New Point(c_x + Int(_R * 2 * Math.Cos(UserObj.Angle_W)), c_y + Int(_R * 2 * Math.Sin(UserObj.Angle_W)))
        g.DrawLine(drawpen, p1, p2)



        '---繪製子彈
        For i = 0 To Max_Bullet
            If BulletObj(i).Enable Then
                If (BulletObj(i).X > _X_min And BulletObj(i).X < _X_max) And (BulletObj(i).Y > _Y_min And BulletObj(i).Y < _Y_max) Then '繪製範圍內 開始繪圖
                    Dim c_bx, c_by As Integer
                    c_bx = Int(Screen.Width / 2) + (BulletObj(i).X - Camera_X) * Ratio
                    c_by = Int(Screen.Height / 2) + (BulletObj(i).Y - Camera_Y) * Ratio
                    'g.DrawEllipse(drawpen, c_x, c_y, 2, 2)
                    Dim bp1 As Point = New Point(c_bx, c_by)
                    Dim bp2 As Point = New Point(c_bx - Int(0.15 * Ratio * Math.Cos(BulletObj(i).Angle)), c_by - Int(0.15 * Ratio * Math.Sin(BulletObj(i).Angle)))
                    g.DrawLine(drawpen, bp1, bp2)
                End If
            End If
        Next
        '-文字
        drawFont = New Font("Arial", 9)
        Dim Stmp As String
        Stmp = "=====GUI=====" & vbNewLine
        Stmp += "FPS: " & fps & vbNewLine
        Stmp += "(" & Mouse_Loact.X & "," & Mouse_Loact.Y & "," & Mouse_Angle & ")" & vbNewLine
        Stmp += key_up & key_right & key_down & key_left & vbNewLine
        Stmp += "Name:" & UserObj.NickName & vbNewLine
        Stmp += "HP:" & UserObj.HP & vbNewLine
        Stmp += "MP:" & UserObj.MP & vbNewLine
        Stmp += "Location:(" & UserObj.X & "," & UserObj.Y & ")" & vbNewLine
        Stmp += "Moving Direcion:" & UserObj.Move_direct & vbNewLine
        Stmp += "Running:" & UserObj.Running & vbNewLine
        Stmp += "body Direcion:" & UserObj.Angle_W & vbNewLine
        Stmp += "attacking:" & UserObj.Attacking & "/" & UserObj.Attack_count & vbNewLine
        Stmp += "Ammo:" & UserObj.Ammo_Amount(0) & vbNewLine
        Stmp += "Reload Time:" & UserObj.Reload_Count & vbNewLine
        Stmp += "Main Weapon:" & UserObj.Weapon(0).Name & vbNewLine
        g.DrawString(Stmp, drawFont, drawBrush, 1, 1)
    End Sub
End Module

Module Module_GameProcess
    Sub Create_BulletObj(ByVal _owner As String, ByVal _X As Single, ByVal _Y As Single, ByVal _angle As Double, ByVal _gun As Weapon)
        Dim i As Integer
        For i = 0 To Max_Bullet
            If BulletObj(i).Enable = False Then
                BulletObj(i).Owner = _owner
                BulletObj(i).X = _X
                BulletObj(i).Y = _Y
                BulletObj(i).Angle = _angle
                BulletObj(i).Speed = _gun.Speed
                BulletObj(i).Count = 0
                BulletObj(i).Damage = _gun.Damage
                BulletObj(i).Enable = True
                Exit For        '跳脫回圈
            End If
        Next
    End Sub
    Public Function NormSInv(ByVal p As Double) As Double   '符合常態分布的亂數
        Const a1 = -39.6968302866538, a2 = 220.946098424521, a3 = -275.928510446969
        Const a4 = 138.357751867269, a5 = -30.6647980661472, a6 = 2.50662827745924
        Const b1 = -54.4760987982241, b2 = 161.585836858041, b3 = -155.698979859887
        Const b4 = 66.8013118877197, b5 = -13.2806815528857, c1 = -0.00778489400243029
        Const c2 = -0.322396458041136, c3 = -2.40075827716184, c4 = -2.54973253934373
        Const c5 = 4.37466414146497, c6 = 2.93816398269878, d1 = 0.00778469570904146
        Const d2 = 0.32246712907004, d3 = 2.445134137143, d4 = 3.75440866190742
        Const p_low = 0.02425, p_high = 1 - p_low
        Dim q As Double, r As Double
        If p < 0 Or p > 1 Then
            Err.Raise(vbObjectError, , "NormSInv: Argument out of range.")
        ElseIf p < p_low Then
            q = (-2 * Math.Log(p)) ^ 0.5
            NormSInv = (((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / _
               ((((d1 * q + d2) * q + d3) * q + d4) * q + 1)
        ElseIf p <= p_high Then
            q = p - 0.5 : r = q * q
            NormSInv = (((((a1 * r + a2) * r + a3) * r + a4) * r + a5) * r + a6) * q / _
               (((((b1 * r + b2) * r + b3) * r + b4) * r + b5) * r + 1)
        Else
            q = (-2 * Math.Log(1 - p)) ^ 0.5
            NormSInv = -(((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / _
               ((((d1 * q + d2) * q + d3) * q + d4) * q + 1)
        End If
    End Function
End Module

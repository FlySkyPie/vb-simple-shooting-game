Imports System.Timers
Module Module_Declaration
    '變數宣告都放這好了=w= 看過學長用這種方法整理 我來也試試
    Public Const Pi = 3.141592653589
    Public Const Max_Bullet = 199
    Public Const Max_Item_Type = 99

    Public Const MAX_TextBox = 20
    Public Const MAX_Lable = 20
    Public Const MAX_NumberSelect = 20
    Public Const MAX_Button = 20
    Public Const Version = "0.0.2"
    Public MonLocl As String = Application.StartupPath

    '圖形化用戶介面類的變數都放這裡
    Public TypeTag As GUI_TextBox
    Public TypeTaging As Boolean = False
    Public TypeShiny As Integer = 0

    Public Screen As PictureBox
    Public G_Text(MAX_TextBox) As GUI_TextBox
    Public G_Lable(MAX_Lable) As GUI_Lable
    Public G_NumberSelect(MAX_NumberSelect) As GUI_NumberSelect
    Public G_Button(MAX_Button) As GUI_Button

    Public Timer_Game, Timer_GUI As System.Timers.Timer
    Public Mouse_Loact As Point

    '圖形化用戶介面變數
    Public Tq As Date = Now
    Public fps As Single
    'Public Mouse_X As Single
    'Public Mouse_Y As Single
    Public Mouse_Angle As Double
    Public Mouse_Rang As Single
    Public Ratio As Single = 30
    Public key_up As Boolean
    Public key_right As Boolean
    Public key_down As Boolean
    Public key_left As Boolean

    Public Camera_X As Single      '攝影機座標
    Public Camera_Y As Single      '攝影機座標
    Public DrawTime As Integer = 0

    '啟動LOGO動畫
    Public Animation_frame As Integer
    Public LOGO_Gear(39) As Point
    Public LOGO_Cat() As Point = {New Point(221, 75), New Point(238, 85), New Point(242, 58), New Point(250, 58), New Point(271, 85), New Point(300, 104), New Point(355, 103), New Point(391, 110), New Point(465, 121), New Point(534, 150), New Point(584, 176), New Point(638, 204), New Point(674, 214), New Point(681, 219), New Point(674, 234), New Point(658, 236), New Point(626, 226), New Point(595, 216), New Point(566, 204), New Point(546, 195), New Point(545, 205), New Point(560, 237), New Point(579, 263), New Point(597, 277), New Point(595, 307), New Point(594, 304), New Point(587, 333), New Point(574, 339), New Point(565, 333), New Point(564, 316), New Point(570, 298), New Point(564, 288), New Point(544, 286), New Point(527, 280), New Point(514, 299), New Point(501, 303), New Point(490, 303), New Point(482, 298), New Point(483, 292), New Point(493, 282), New Point(503, 266), New Point(492, 253), New Point(479, 242), New Point(463, 230), New Point(445, 218), New Point(418, 204), New Point(394, 193), New Point(366, 198), New Point(339, 204), New Point(311, 222), New Point(292, 242), New Point(274, 262), New Point(261, 279), New Point(253, 286), New Point(245, 290), New Point(237, 283), New Point(237, 267), New Point(247, 247), New Point(260, 217), New Point(238, 231), New Point(219, 249), New Point(206, 255), New Point(197, 246), New Point(198, 230), New Point(216, 212), New Point(250, 181), New Point(226, 180), New Point(207, 172), New Point(200, 162), New Point(201, 150), New Point(206, 146), New Point(206, 126), New Point(209, 104), New Point(225, 91)}

    Public Loading As Boolean = False  '載入中
    Public Gaming As Boolean = False  '遊戲進行中
    '遊戲宣告
    Public UserObj As Player
    Public WeaponData(99) As Weapon
    Public BulletObj(Max_bullet) As Bullet
    Public ItemData(Max_Item_Type) As Item
End Module

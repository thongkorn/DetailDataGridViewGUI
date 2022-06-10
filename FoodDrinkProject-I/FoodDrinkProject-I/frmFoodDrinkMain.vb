#Region "ABOUT"
' / --------------------------------------------------------------------------------
' / Developer : Mr.Surapon Yodsanga (Thongkorn Tubtimkrob)
' / eMail : thongkorn@hotmail.com
' / URL: http://www.g2gnet.com (Khon Kaen - Thailand)
' / Facebook: http://www.facebook.com/g2gnet (for Thailand)
' / Facebook: http://www.facebook.com/commonindy (Worldwide)
' / More Info: http://www.g2gsoft.com
' /
' / Purpose: Demonstrate search product from DataTable and calculate product sales results.
' /              Create a TabPage/Panel to contain dynamic Buttons.
' / Microsoft Visual Basic .NET (2010)
' /
' / This is open source code under @CopyLeft by Thongkorn Tubtimkrob.
' / You can modify and/or distribute without to inform the developer.
' / --------------------------------------------------------------------------------
#End Region

Public Class frmFoodDrinkMain

    '/ กำหนดตำแหน่งการเก็บไฟล์ภาพ ... โฟลเดอร์เก็บ Execute File\Images\
    Dim strImagePath As String = MyPath(Application.StartupPath & "Images\")

    ' / --------------------------------------------------------------------------------
    '/ ฟังค์ชั่นในการกำหนดพาธให้กับโปรแกรม
    Private Function MyPath(ByVal AppPath As String) As String
        MyPath = AppPath.ToLower.Replace("\bin\debug", "\").Replace("\bin\release", "\").Replace("\bin\x86\debug", "\")
        '/ ASCII Code (92) = \ (Backslash)
        If Microsoft.VisualBasic.Right(MyPath, 1) <> Chr(92) Then MyPath = MyPath & Chr(92)
    End Function

    ' / --------------------------------------------------------------------------------
    '/ Don't forget to set Form has KeyPreview = True
    '/ เวลาทำการ Debugger ให้ปิด Handle ที่เหตุการณ์นี้
    Private Sub frmFoodDrinkMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F8
                '/ Remove Row
                Call DeleteRow("btnDelRow")
        End Select
    End Sub

    ' / --------------------------------------------------------------------------------
    '// START HERE
    Private Sub frmFoodDrinkMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.KeyPreview = True  '/ สามารถกดปุ่มฟังค์ชั่นคีย์ลงในฟอร์มได้
        Call InitializeGrid()
        '// สร้าง TabPage แล้วตามด้วยสร้าง Panel จากนั้นให้สร้างปุ่มคำสั่ง (Button) ตามจำนวนรายการสินค้าที่อยู่ในแต่ละกลุ่ม/ประเภท
        Call AddTabPage()
        '//
        txtSumTotal.ReadOnly = True
        txtSumTotal.Text = "0.00"
        lblLastPrice.Text = ""
    End Sub

    ' / --------------------------------------------------------------------------------
    '// เพิ่มแท็บเพจ (TabPage) คอนโทรลแบบไดนามิคลงไปใน TabControl1
    ' / --------------------------------------------------------------------------------
    Private Sub AddTabPage()
        Dim CatDataTable As New DataTable
        '/ Remove First TabPage
        Me.TabControl1.TabPages.Remove(TabPage1)
        '// รับข้อมูลสมมุติมาจากตารางข้อมูล (DataTable) ในการจัดกลุ่ม/ประเภท (Category)
        CatDataTable = GetCatDataTable()
        '/ สร้าง Panel ตามจำนวนของกลุ่ม/ประเภท (Category) ในตัวอย่างมี 4 กลุ่ม และกลุ่มรวม (ALL) รวมเป็น 5 กลุ่ม
        Dim pn(CatDataTable.Rows.Count)
        Dim iCat As Byte = 0
        ' Loop and display the keys.
        For Each CatRow As DataRow In CatDataTable.Rows
            ' / Create a tabpage
            Dim tabPg As New TabPage
            ' / Set the tabpage to be your desired tab.
            tabPg.Name = "Tab" & CStr(iCat)
            '// แสดงชื่อกลุ่ม Category ใน TabPage
            tabPg.Text = CatRow(1).ToString
            '// เพิ่ม TabPage ลงใน TabControl1
            Me.TabControl1.Controls.Add(tabPg)
            '// เพิ่ม Panel
            pn(iCat) = New Panel
            pn(iCat).Name = "pn" & iCat
            '/ สลับสีพื้นหลังของ Panel
            With pn(iCat)
                If (iCat Mod 2) = 0 Then
                    '/ เลขคู่แสดงสีนี้
                    .BackColor = Color.Beige
                Else
                    '/ เลขคี่แสดงสีนี้
                    .BackColor = Color.PaleTurquoise
                End If
                '/ ปรับคุณสมบัติของ Panel แบบ Run-Time
                .Dock = DockStyle.Fill
                .Location = New System.Drawing.Point(1, 1)
                .Size = New System.Drawing.Size(TabControl1.Width - 10, TabControl1.Height - 30)
                .BackColor = Color.Moccasin
                .AutoScroll = True
                .Anchor = AnchorStyles.Bottom + AnchorStyles.Top
            End With
            '/ Add the panel into the TabControl
            tabPg.Controls.Add(pn(iCat))
            '/
            Dim dt As New DataTable
            dt = GetDataTable(CatRow(0).ToString)
            ' / --------------------------------------------------------------------------------
            '/ ไปที่โปรแกรมย่อยการเพิ่มปุ่มคำสั่ง (Button)
            '/ รอบแรกจะแสดงผลรายการสินค้าทั้งหมด เพราะส่งค่า 0 ออกไป ไม่ตรงกับค่ากลุ่มใดๆ
            '/ กำหนด 3 หลัก, นับจำนวนรายการเพื่อทำปุ่ม, Panel(กลุ่ม/ประเภทสินค้า), DataTable
            Call AddButton(3, dt.Rows.Count, pn(iCat), dt)
            ' / --------------------------------------------------------------------------------
            iCat += 1
            dt.Dispose()
        Next
        '/ ตั้งค่าที่แท็บเพจตัวแรก (ALL)
        TabControl1.SelectedIndex = 0
        CatDataTable.Dispose()
    End Sub

    ' / --------------------------------------------------------------------------------
    '// เพิ่มปุ่มคำสั่งแบบไดนามิค ตามจำนวนในแต่ละกลุ่ม/ประเภทสินค้า
    ' / --------------------------------------------------------------------------------
    Private Sub AddButton(ByVal ColCount As Byte, ByVal btnCount As Byte, ByRef pn As Panel, ByRef dt As DataTable)
        Dim iCount As Byte = 0
        '/ วนรอบไปจนกว่าจำนวนปุ่ม (Button) มันเกินค่าที่มีอยู่
        While btnCount <> iCount
            Dim B As New Button
            B.Height = 140
            B.Width = 140
            Dim LB As New Label
            With LB
                .Height = 40
                .Width = 140
                '// สลับสีของป้ายลาเบล
                If iCount Mod 2 = 0 Then
                    '// เลขคู่
                    .BackColor = Color.Orange
                Else
                    '// เลขคี่
                    .BackColor = Color.Crimson
                End If
            End With
            pn.Controls.Add(B)  '/ Add Button
            pn.Controls.Add(LB) '/ Add Label อยู่ด้านล่างของ Button
            '// Button
            With B
                ' / ตัวอย่างกำหนดเอาไว้ให้แสดงผลปุ่มคำสั่ง (Button) 3 หลัก
                ' / --------------------------------------------------------------------------------
                '// หาตำแหน่งซ้าย (Left) ... ด้วยการหารเอาเศษ (Mod) ด้วย 3 จะได้คำตอบ 0, 1, 2 ตลอด เพื่อใช้จัดวางตำแหน่งหลัก Button ได้ทีละ 3 หลัก
                '// การหารเอาเศษจะได้ค่าสูงสุด คือ ค่าตัวหาร (Mod) ลบออก 1 เช่น X Mod 3 จะได้ค่า 0, 1, 2 (หรือ 3 - 1 = 2)
                '// เริ่มนับจาก 0 ไปเรื่อยๆ
                '// 0 Mod 3 = 0, 1 Mod 3 = 1, 2 Mod 3 = 2 ... รอบที่ 1
                '// 3 Mod 3 = 0, 4 Mod 3 = 1, 5 Mod 3 = 2 ... รอบที่ 2
                '// 6 Mod 3 = 0, 7 Mod 3 = 1, 8 Mod 3 = 2 ... รอบที่ 3 ... ทำไปเรื่อยๆ
                '// มีคำตอบแค่ 0, 1, 2 เราก็เลยหาจำนวนหลักได้ตามที่กำหนด คือ 3 หลัก
                '// เอาค่าที่ได้แต่ละหลักมาคูณความกว้างของปุ่มคำสั่ง
                '// เป็นการเลื่อนตำแหน่งไปทางซ้าย ด้วยการคูณด้วย 0, 1 และ 2 เป็นจำนวนเท่าของความกว้างของ Button
                .Left = (iCount Mod ColCount) * B.Width
                ' / --------------------------------------------------------------------------------
                '/ Left ของ Button และ Label จะเท่ากัน
                LB.Left = B.Left
                ' / --------------------------------------------------------------------------------
                '// หาตำแหน่งบน (Top) ... ด้วยการหารตัดเศษ \ ... การหารปกติ 3 / 2 = 1.5, การหารตัดเศษ 3 \ 2 = 1
                '// เริ่มนับจาก 0 ไปเรื่อยๆ
                '// 0 \ 3 = 0, 1 \ 3 = 0, 2 \ 3 = 0 ... รอบที่ 1 (คำตอบคือ 0 เหมือนกัน)
                '// 3 \ 3 = 1, 4 \ 3 = 1, 5 \ 3 = 1 ... รอบที่ 2 (คำตอบคือ 1 เหมือนกัน)
                '// 6 \ 3 = 2, 7 \ 3 = 2, 8 \ 3 = 2 ... รอบที่ 3 (คำตอบคือ 2 เหมือนกัน) ... ทำไปเรื่อยๆ (คือการเพิ่มตัวคูณเข้าไปทีละ 1)
                '// จะเห็นว่าหลักทั้ง 3 หรือ 3 ปุ่ม (Button) ในแต่ละแถว จะมีค่าคงที่ตลอด ไม่มีการเปลี่ยนค่าเลย จึงทำให้ตำแหน่ง Top ในแต่ละแถวเท่ากันเสมอ
                '// เอาค่าที่ได้จากการหารตัดเศษในแต่ละแถว X (ความสูงของปุ่มคำสั่ง+ความสูงของลาเบล)
                '// แถว 1 จึงคูณ (ความสูงของปุ่มคำสั่ง+ความสูงของลาเบล) ด้วย 0 ดังนั้น Top = 0
                '// แถว 2 จึงคูณ (ความสูงของปุ่มคำสั่ง+ความสูงของลาเบล) ด้วย 1 ดังนั้น Top = (ความสูงของ Button+ความสูงของ Label) X 1
                '// แถว 3 จึงคูณ (ความสูงของปุ่มคำสั่ง+ความสูงของลาเบล) ด้วย 2 ดังนั้น Top = (ความสูงของ Button+ความสูงของ Label) X 2
                .Top = (iCount \ ColCount) * (B.Height + LB.Height)
                ' / --------------------------------------------------------------------------------
                Dim row As DataRow = dt.Rows(iCount)
                .Name = "Button" & row(0).ToString
                .Text = "ID : " & row(1).ToString
                '// นำค่า ProductPK (Primary Key) ไปซ่อนไว้ในคุณสมบัติ Tag ของ Button ในแต่ละตัว ซึ่งเราจะใช้ค่านี้เมื่อตอนคลิ๊กกดปุ่มคำสั่งในแต่ละตัว
                '// เพื่อนำไปเปรียบเทียบค่าในตารางกริด (หลัก 0) หรือการค้นหาข้อมูลสินค้าด้วยค่า Primary Key
                '// หรือใครที่กำหนดฟิลด์เอาไว้แค่ ID โดยไม่มี PK ... ก็ใช้ให้ ID ไปเลยก็ได้
                .Tag = row(0).ToString
                '// ทำการแสดงผลภาพลงบนปุ่ม
                Dim imgFile As String = strImagePath & row(4).ToString
                '/ ถ้าไม่ได้กำหนดรูป หรือหารูปภาพไม่เจอ ให้ใส่ภาพ NoImage.jpg ป้องกัน Error
                If Not System.IO.File.Exists(imgFile) Then imgFile = strImagePath & "NoImage.jpg"
                '/ นำภาพไปแสดงบน Button
                .BackgroundImage = New System.Drawing.Bitmap(imgFile)
                .BackgroundImageLayout = ImageLayout.Stretch
                '/ ปรับคุณสมบัติ Button
                .Font = New Font("Century Gothic", 10, FontStyle.Bold)
                .ForeColor = Color.Black
                .TextAlign = ContentAlignment.BottomCenter
                .TextImageRelation = TextImageRelation.ImageAboveText
                .UseVisualStyleBackColor = True
                .Cursor = Cursors.Hand
                .FlatStyle = FlatStyle.Standard
                '// ปรับคุณสมบัติของ Label
                With LB
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Top = B.Top + B.Height
                    .ForeColor = Color.White
                    .Font = New Font("Tahoma", 10, FontStyle.Bold)
                    '/ Label ป้ายบอกเพื่อแสดงราคาใน Label
                    .Text = row(2).ToString & vbCrLf & "ราคา: " & Format(Convert.ToDecimal(row(3).ToString), "#,##0.00")
                End With
            End With
            iCount += 1
            ' / --------------------------------------------------------------------------------
            '// Event Handler ในการกดคลิ๊กที่ปุ่มคำสั่ง (Button)
            AddHandler B.Click, AddressOf ClickButton
            ' / --------------------------------------------------------------------------------
        End While
    End Sub

    ' / --------------------------------------------------------------------------------
    Public Sub ClickButton(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = sender
        'MessageBox.Show("Primary Key: " & btn.Tag)
        '/ นำ Tag ซึ่งเก็บค่า ProductPK ไปใช้งานในการค้นหาสินค้า
        Call AddToDataGridView(btn.Tag)
    End Sub

    ' / --------------------------------------------------------------------------------
    ' / ค้นหารายการสินค้าของเดิมในตารางกริด ก่อนที่จะเพิ่มรายการแถวใหม่
    ' / อันนี้เขียนโค้ดกลับด้านกับเหตุการณ์ txtSearch_KeyPress
    ' / โดยใช้ค่า ProductPK (Primary Key ที่เป็นเลขจำนวนเต็ม) มาเปรียบเทียบกับค่าในตารางกริดก่อน
    ' / หากหาข้อมูลในตารางกริดไม่เจอ ก็จะไปค้นจาก DataTable อีกที
    ' / --------------------------------------------------------------------------------
    Private Sub AddToDataGridView(ByVal PK As Integer)
        Dim blnExist As Boolean = False
        '// ตรวจสอบว่า Primary Key มีอยู่ในตารางกริดหรือไม่ (หากมีให้ +เพิ่ม, หากไม่มีค่อยไปค้นหาข้อมูล)
        For iRow As Integer = 0 To dgvData.RowCount - 1
            ' / หากพบ Primary Key ในหลัก 0 มาตรงกันกับค่าในแถวของตารางกริด
            ' / ค่า Primary Key (PK) ตัวนี้ได้จากการกดปุ่ม (Button) โดยซ่อนค่าไว้ในคุณสมบัติ Tag ของ Button
            If dgvData.Rows(iRow).Cells(0).Value = PK Then
                ' / ให้บวกจำนวนที่เลือกเพิ่มขึ้นอีก 1 (Quantity = Quantity + 1)
                dgvData.Rows(iRow).Cells(3).Value = dgvData.Rows(iRow).Cells(3).Value + 1
                '/ หลัก Index = 4 คือ UnitPrice
                lblLastPrice.Text = "Last Price: " & Format(CDbl(dgvData.Rows(iRow).Cells(4).Value), "#,##0.00")
                '// เจอข้อมูลเดิม
                blnExist = True
                '// ออกจากลูปไปเลย เพื่อไม่ให้เสียเวลา
                Exit For
            End If
        Next
        '// ไม่พบข้อมูลในตารางกริด ก็ทำการเรียกจากตารางข้อมูลเข้ามาแสดงผล
        If Not blnExist Then
            '/ สร้าง DataTable สมมุติขึ้นมา
            Dim DT As DataTable = GetDataTable()
            '/ ค้นหาข้อมูลจาก DataTable แล้วรับค่ามาใส่ไว้ใน DataRow
            '/ การค้นหาข้อมูลแบบเลขจำนวนเต็ม เช่น ProductPK = 1
            Dim r() As DataRow = DT.Select(" ProductPK = " & PK)
            '// หากพบข้อมูลใน DataTable
            If r.Count > 0 Then
                '/ จากโครงสร้าง DataTable
                '/ Primary Key, Product ID, Product Name, Quantity, UnitPrice, Total
                dgvData.Rows.Add(r(0).Item(0), r(0).Item(1), r(0).Item(2), "1", Format(CDbl(r(0).Item(3).ToString), "0.00"), "0.00")
                lblLastPrice.Text = "Last Price: " & CDbl(r(0).Item(3)).ToString("0.00")
            End If
            DT.Dispose()
        End If
        '// หาจำนวนเงินรวมใหม่
        Call CalSumTotal()
        '// โฟกัสไปที่ DataGridView แล้วย้ายไปแถวล่าสุด และไปทางซ้ายเพื่อไปที่ช่องจำนวน (Quantity)
        'dgvData.Focus()
        'SendKeys.Send("^{END}{LEFT}{LEFT}{LEFT}")
    End Sub

    ' / --------------------------------------------------------------------------------
    ' / DataTable ของกลุ่ม/ประเภทสินค้า (Category)
    ' / --------------------------------------------------------------------------------
    Function GetCatDataTable() As DataTable
        Dim DT As New DataTable
        With DT
            .Columns.Add("CategoryPK", GetType(Byte)) '<<< Index = 0
            .Columns.Add("CategoryName", GetType(String))    '<< Index = 1
        End With
        '// ... Add rows in the Category.
        With DT
            .Rows.Add(0, "ALL")
            .Rows.Add(1, "Coffee")
            .Rows.Add(2, "Burger")
            .Rows.Add(3, "Soft Drink")
            .Rows.Add(4, "Beverages")
        End With
        Return DT
    End Function

    ' / --------------------------------------------------------------------------------
    ' / S A M P L E ... D A T A T A B L E (Products)
    ' / --------------------------------------------------------------------------------
    Function GetDataTable(Optional ByVal Cat As Byte = 0) As DataTable
        '// Add Column
        Dim DT As New DataTable
        With DT
            .Columns.Add("ProductPK", GetType(Integer)) '<< Index = 0
            .Columns.Add("ProductID", GetType(String))    '<< 1
            .Columns.Add("ProductName", GetType(String)) '<< 2
            .Columns.Add("UnitPrice", GetType(Double)) '<< 3
            .Columns.Add("PictureName", GetType(String)) '<< 4
            .Columns.Add("CategoryFK", GetType(Byte))   '<< 5 (เอากลุ่ม/ประเภทสินค้า Foreign Key มาไว้ท้ายสุด เพราะในตารางข้อมูลจริงจะเชื่อมความสัมพันธ์กัน)
        End With
        '// ... Add rows in the first category. (1 - Coffee)
        '/ ProductPK, ProductID, ProductName, UnitPrice, PictureName, CategoryFK
        With DT.Rows
            .Add(1, "01", "กาแฟร้อน", "50.00", "Coffee.jpg", 1)
            .Add(2, "02", "กาแฟเย็น", "60.00", "Coffee4.png", 1)
            .Add(3, "03", "คาปูชิโน่", "75.00", "Cappuccino.jpg", 1)
            .Add(4, "04", "คาปูชิโน่ - ลาเต้", "80.00", "CappuccinoLatte.jpg", 1)
            .Add(5, "05", "เอ็กซ์เพรสโซ่", "90.00", "Expresso.jpg", 1)
        End With
        '// ... Add rows in category. (2 - Burger)
        With DT.Rows
            .Add(11, "11", "Classic Chicken", "20.00", "BurgerChicken.png", 2)
            .Add(12, "12", "Mexicana", "25.00", "BurgerMexicana.png", 2)
            .Add(13, "13", "Lemon Shrimp", "30.00", "BurgerLemonShrimp.png", 2)
            .Add(14, "14", "Bacon", "40.00", "BurgerBacon.png", 2)
            .Add(15, "15", "Spicy Shrimp", "45.00", "BurgerSpicyShrimp.png", 2)
            .Add(16, "16", "Tex Supreme", "50.00", "BurgerTexSupreme.png", 2)
            .Add(17, "17", "Fish", "55.00", "BurgerFish.png", 2)
        End With
        '// ... Add rows in category. (3 - Soft Drink)
        With DT.Rows
            .Add(21, "21", "Pepsi Can", "20.00", "PepsiCan.png", 3)
            .Add(22, "22", "Coke Can", "20.00", "CokeCan.png", 3)
            .Add(23, "23", "7Up Can", "20.00", "7upCan.png", 3)
            .Add(24, "24", "Pepsi 2 ลิตร", "50.00", "Pepsi2L.jpg", 3)
            .Add(25, "25", "Coke 2 ลิตร", "50.00", "Coke2L.jpg", 3)
            .Add(26, "26", "น้ำเปล่า", "15.00", "Water.jpg", 3)
        End With
        '// ... Add rows in category. (4 - Whisky)
        With DT.Rows
            .Add(41, "41", "วิสกี้ (เพียว)", "100.00", "Whisky.jpg", 4)
            .Add(42, "42", "เหล้าขาว (เป๊ก)", "40.00", "Pek.png", 4)
            .Add(43, "43", "ม้ากระทืบโรง (เป๊ก)", "50.00", "Horse.jpg", 4)
            .Add(44, "44", "เบียร์สิงห์ (กระป๋อง)", "80.00", "SinghaCan.jpg", 4)
            .Add(45, "45", "เบียร์ลีโอ (กระป๋อง)", "70.00", "LeoCan.jpg", 4)
            .Add(46, "46", "เบียร์ช้าง (กระป๋อง)", "70.00", "ChangCan.jpg", 4)
            .Add(47, "47", "Spy Wine Cooler", "50.00", "Spy.jpg", 4)
            .Add(48, "48", "โซจู (สตรอเบอรี่)", "90.00", "Soju.jpg", 4)
        End With
        '// กรองเอาเฉพาะพวกที่แบ่งกลุ่ม/ประเภทสินค้า โดยมีการค้นหาด้วย CategoryFK (หลักสุดท้ายใน DataTable)
        If Cat > 0 Then
            Dim Result() As DataRow = DT.Select("CategoryFK = " & Cat)
            Dim MyDT As New DataTable
            If Not Result.Length = 0 Then MyDT = Result.CopyToDataTable
            Return MyDT
            '// เอาข้อมูลสินค้าทั้งหมด
        Else
            Return DT
        End If
    End Function

    ' / --------------------------------------------------------------------------------
    ' / ตั้งค่าเริ่มต้นให้กับ DataGridView แบบ Run Time (ใช้โค้ดทั้งหมด)
    Private Sub InitializeGrid()
        With dgvData
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .MultiSelect = False
            .ReadOnly = False
            .RowTemplate.MinimumHeight = 27
            .RowTemplate.Height = 27
            '/ Columns Specified
            '/ Index = 0
            .Columns.Add("PK", "Primary Key")
            With .Columns("PK")
                .ReadOnly = True
                .DefaultCellStyle.BackColor = Color.LightGoldenrodYellow
                .Visible = False '/ ปกติหลัก Primary Key จะต้องถูกซ่อนไว้
            End With
            '/ Index = 1
            .Columns.Add("ProductID", "Product ID")
            .Columns("ProductID").ReadOnly = True
            .Columns("ProductID").Visible = False
            '/ Index = 2
            .Columns.Add("ProductName", "Product Name")
            .Columns("ProductName").ReadOnly = True
            .Columns("ProductName").DefaultCellStyle.BackColor = Color.LightGoldenrodYellow
            '/ Index = 3
            .Columns.Add("Quantity", "Quantity")
            .Columns("Quantity").ValueType = GetType(Integer)
            '/ Index = 4
            .Columns.Add("UnitPrice", "Unit Price")
            .Columns("UnitPrice").ValueType = GetType(Double)
            '/ Index = 5
            .Columns.Add("Total", "Total")
            .Columns("Total").ValueType = GetType(Double)
            .Font = New Font("Tahoma", 11)
            '/ Total Column (5)
            With .Columns("Total")
                .ReadOnly = True
                .DefaultCellStyle.BackColor = Color.LightGoldenrodYellow
                .DefaultCellStyle.ForeColor = Color.Blue
                .DefaultCellStyle.Font = New Font(dgvData.Font, FontStyle.Bold)
            End With
            '// เพิ่มปุ่มลบ (Index = 6)
            Dim btnDelRow As New DataGridViewButtonColumn
            dgvData.Columns.Add(btnDelRow)
            With btnDelRow
                .HeaderText = "Delete F8"
                .Text = "Delete"
                .UseColumnTextForButtonValue = True
                .Width = 30
                .ReadOnly = True
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable  '/ Not sort order but can click header for delete row.
            End With
            '/ Alignment MiddleRight only columns 3 to 5
            For i As Byte = 3 To 5
                '/ Header Alignment
                .Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                '/ Cell Alignment
                .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Next
            '/ Auto size column width of each main by sorting the field.
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            '/ Adjust Header Styles
            With .ColumnHeadersDefaultCellStyle
                .BackColor = Color.RoyalBlue
                .ForeColor = Color.White
                .Font = New Font("Tahoma", 11, FontStyle.Bold)
            End With
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 36
            '/ กำหนดให้ EnableHeadersVisualStyles = False เพื่อให้ยอมรับการเปลี่ยนแปลงสีพื้นหลังของ Header
            .EnableHeadersVisualStyles = False
        End With

    End Sub

    ' / --------------------------------------------------------------------------------
    ' / การค้นหาข้อมูลในช่อง TextBox
    ' / --------------------------------------------------------------------------------
    Private Sub txtSearch_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        '// เมื่อกดคีย์ ENTER เพื่อเริ่มต้นการค้นหาข้อมูล
        If e.KeyChar = Chr(13) Then
            '/ Replace some word for reserved in DataBase.
            txtSearch.Text = txtSearch.Text.Trim.Replace("'", "").Replace("*", "").Replace("%", "")
            e.Handled = True    '// ปิดเสียง
            '/ สร้าง DataTable สมมุติขึ้นมา
            Dim DT As DataTable = GetDataTable()
            '/ ค้นหาข้อมูลจาก DataTable แล้วรับค่ามาใส่ไว้ใน DataRow
            '/ การค้นหาข้อมูลแบบ String จะต้องใส่เครื่องหมาย Single Quote ครอบเอาไว้ เช่น ProductID = '01'
            Dim r() As DataRow = DT.Select(" ProductID = " & "'" & txtSearch.Text.Trim & "'")
            '// หากพบข้อมูลใน DataTable
            If r.Count > 0 Then
                '/ ตัวแปรบูลีน Flag แจ้งการค้นหาข้อมูลในตารางกริด (True = พบรายการในแถว, False = ไม่พบ)
                Dim blnExist As Boolean = False
                '/ ต้องค้นหาข้อมูลจากตารางกริดก่อน เพื่อค้นหาว่ามีรายการสินค้าเดิมหรือไม่?
                '/ หากในตารางกริดยังไม่มีแถวรายการ ก็จะข้าม For Loop นี้ไปเพิ่มรายการใหม่ทันที (ครั้งแรกที่ยังไม่มีข้อมูลในตารางกริด)
                For iRow As Integer = 0 To dgvData.RowCount - 1
                    '/ ทดสอบด้วย Primary Key r(0).Item(0) หรือ Product ID r(0).Item(1) ก็ได้
                    If r(0).Item(0) = dgvData.Rows(iRow).Cells(0).Value Then
                        '/ เมื่อพบรายการเดิม ก็ให้เพิ่มจำนวนขึ้น 1 
                        dgvData.Rows(iRow).Cells(3).Value += 1
                        lblLastPrice.Text = "Last Price: " & CDbl(dgvData.Rows(iRow).Cells(4).Value).ToString("0.00")
                        '/ Flag แจ้งว่าพบข้อมูลเดิมแล้ว
                        blnExist = True
                        '/ เมื่อเจอสินค้าเดิมในตารางกริดแล้ว ไม่ว่าจะอยู่แถวลำดับที่เท่าไหร่ ก็ให้ออกจาก For Loop การค้นหาได้เลย
                        '/ เพราะรายการสินค้าใดๆ จะต้องมีอยู่เพียงแค่รายการเดียว ไม่ต้องเสียเวลาวนรอบกลับไปทำให้จนครบจำนวนแถว
                        Exit For
                    End If
                Next
                '/ กรณีที่พบสินค้าในตารางกริด กำหนด blnExist = True ทำให้ Not True = False จะทำให้ข้ามเงื่อนไขนี้ออกไป
                '/ กรณีที่ไม่พบข้อมูลสินค้าเดิมในตารางกริด กำหนด blnExist = False ทำให้ Not False = True เพิ่มรายการสินค้าแถวใหม่เข้าไปในตารางกริดได้
                If Not blnExist Then
                    '/ Primary Key, Product ID, Product Name, Quantity, UnitPrice, Total
                    dgvData.Rows.Add(r(0).Item(0), r(0).Item(1), r(0).Item(2), "1", Format(CDbl(r(0).Item(3).ToString), "0.00"), "0.00")
                    lblLastPrice.Text = "Last Price: " & CDbl(r(0).Item(3)).ToString("0.00")
                End If
                '/ หากไม่ใช้ NOT ก็จะต้องเขียนโปรแกรมแบบนี้
                '/ If blnExist = True Then
                '/     ไม่ต้องทำอะไร
                '/ Else
                '/     ทำคำสั่งเพิ่มรายการ
                '/ End If
                '// คำนวณผลรวมใหม่
                Call CalSumTotal()
                DT.Dispose()
            End If
            txtSearch.Clear()
            txtSearch.Focus()
        End If
    End Sub

    ' / --------------------------------------------------------------------------------
    ' / Calcualte sum of Total (Column Index = 5)
    ' / ทำทุกครั้งที่มีการเพิ่มหรือลบแถวรายการ และมีการเปลี่ยนแปลงค่าในเซลล์ Quantity, UnitPrice
    Private Sub CalSumTotal()
        txtSumTotal.Text = "0.00"
        '/ วนรอบตามจำนวนแถวที่มีอยู่ปัจจุบัน
        For i As Integer = 0 To dgvData.RowCount - 1
            '/ หลักสุดท้ายของตารางกริด = [จำนวน x ราคา]
            dgvData.Rows(i).Cells(5).Value = Format(dgvData.Rows(i).Cells(3).Value * dgvData.Rows(i).Cells(4).Value, "#,##0.00")
            '/ นำค่าจาก Total มารวมกันเพื่อแสดงผลในสรุปผลรวม (x = x + y)
            txtSumTotal.Text = Format(CDbl(txtSumTotal.Text) + CDbl(dgvData.Rows(i).Cells(5).Value), "#,##0.00")
        Next
    End Sub

    ' / --------------------------------------------------------------------------------
    ' / โปรแกรมย่อยในการลบแถวรายการที่เลือกออกไป
    Private Sub DeleteRow(ByVal ColName As String)
        If dgvData.RowCount = 0 Then Return
        '/ ColName เป็นชื่อของหลัก Index = 6 ของตารางกริด (ไปดูที่โปรแกรมย่อย InitializeGrid)
        If ColName = "btnDelRow" Then
            '// ลบรายการแถวที่เลือกออกไป
            dgvData.Rows.Remove(dgvData.CurrentRow)
            '/ เมื่อแถวรายการถูกลบออกไป ต้องไปคำนวณหาค่าผลรวมใหม่
            Call CalSumTotal()
        End If
        txtSearch.Focus()
    End Sub

    ' / --------------------------------------------------------------------------------
    ' / ปุ่มลดจำนวนสินค้าลงทีละ 1 ... แถวของตารางกริดที่ถูกโฟกัส
    Private Sub btnDecrement_Click(sender As System.Object, e As System.EventArgs) Handles btnDecrement.Click
        If dgvData.RowCount = 0 Then Return
        dgvData.Focus()
        For iRow As Integer = 0 To dgvData.RowCount - 1
            ' / หากพบ Primary Key ในหลัก 0 มาตรงกันกับค่าปัจจุบันในแถวของตารางกริด
            If dgvData.Rows(iRow).Cells(0).Value = dgvData.CurrentRow.Cells(0).Value Then
                If dgvData.Rows(iRow).Cells(3).Value = 0 Then
                    '/ หรือหากจำนวนมีค่าเป็น 0 ก็ลบแถวออกไปเลย
                    'dgvData.Rows.Remove(dgvData.CurrentRow)
                    'dgvData.Refresh()
                    Exit For
                Else
                    ' / ให้ลดจำนวนลง 1 (Quantity = Quantity - 1)
                    dgvData.Rows(iRow).Cells(3).Value -= 1
                    Exit For
                End If
            End If
        Next
        '// รวมจำนวนเงิน
        Call CalSumTotal()
    End Sub

    ' / --------------------------------------------------------------------------------
    ' / ปุ่มเพิ่มจำนวนสินค้าขึ้นทีละ 1 ... แถวของตารางกริดที่ถูกโฟกัส
    Private Sub btnIncrement_Click(sender As System.Object, e As System.EventArgs) Handles btnIncrement.Click
        If dgvData.RowCount = 0 Then Return
        dgvData.Focus()
        For iRow As Integer = 0 To dgvData.RowCount - 1
            ' / หากพบ Primary Key ในหลัก 0 มาตรงกันกับค่าปัจจุบันในแถวของตารางกริด
            If dgvData.Rows(iRow).Cells(0).Value = dgvData.CurrentRow.Cells(0).Value Then
                ' / ให้เพิ่มจำนวนขึ้น 1 (Quantity = Quantity + 1)
                dgvData.Rows(iRow).Cells(3).Value += 1
                Exit For
            End If
        Next
        '// รวมจำนวนเงิน
        Call CalSumTotal()
    End Sub

    Private Sub dgvData_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvData.CellClick
        Select Case e.ColumnIndex
            '// Delete Button
            Case 6
                'MsgBox(("Row : " + e.RowIndex.ToString & "  Col : ") + e.ColumnIndex.ToString)
                Call DeleteRow("btnDelRow")
        End Select
    End Sub

    ' / --------------------------------------------------------------------------------
    ' / After you press Enter
    Private Sub dgvData_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvData.CellEndEdit
        '/ เกิดการเปลี่ยนแปลงค่าในหลัก Index ที่ 3 หรือ 4
        Select Case e.ColumnIndex
            Case 3, 4 '/ Column Index = 3 (Quantity), Column Index = 4 (UnitPrice)
                '/ Quantity
                '/ การดัก Error กรณีมีค่า Null Value ให้ใส่ค่า 0 ลงไปแทน
                If IsDBNull(dgvData.Rows(e.RowIndex).Cells(3).Value) Then dgvData.Rows(e.RowIndex).Cells(3).Value = "0"
                Dim Quantity As Integer = dgvData.Rows(e.RowIndex).Cells(3).Value
                '/ UnitPrice
                '/ If Null Value
                If IsDBNull(dgvData.Rows(e.RowIndex).Cells(4).Value) Then dgvData.Rows(e.RowIndex).Cells(4).Value = "0.00"
                Dim UnitPrice As Double = dgvData.Rows(e.RowIndex).Cells(4).Value
                dgvData.Rows(e.RowIndex).Cells(4).Value = Format(CDbl(dgvData.Rows(e.RowIndex).Cells(4).Value), "0.00")

                '/ Quantity x UnitPrice
                dgvData.Rows(e.RowIndex).Cells(5).Value = CDbl((Quantity * UnitPrice).ToString("#,##0.00"))
                '/ Calculate Summary
                Call CalSumTotal()
        End Select
    End Sub

    ' / --------------------------------------------------------------------------------
    Private Sub dgvData_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvData.EditingControlShowing
        Select Case dgvData.Columns(dgvData.CurrentCell.ColumnIndex).Name
            ' / Can use both Colume Index or Field Name
            Case "Quantity", "UnitPrice"
                '/ Stop and Start event handler
                RemoveHandler e.Control.KeyPress, AddressOf ValidKeyPress
                AddHandler e.Control.KeyPress, AddressOf ValidKeyPress
        End Select
    End Sub

    ' / --------------------------------------------------------------------------------
    Private Sub ValidKeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim tb As TextBox = sender
        Select Case dgvData.CurrentCell.ColumnIndex
            Case 3  ' Quantity is Integer
                Select Case e.KeyChar
                    Case "0" To "9"   ' digits 0 - 9 allowed
                    Case ChrW(Keys.Back)    ' backspace allowed for deleting (Delete key automatically overrides)

                    Case Else ' everything else ....
                        ' True = CPU cancel the KeyPress event
                        e.Handled = True ' and it's just like you never pressed a key at all
                End Select

            Case 4  ' UnitPrice is Double
                Select Case e.KeyChar
                    Case "0" To "9"
                        ' Allowed "."
                    Case "."
                        ' can present "." only one
                        If InStr(tb.Text, ".") Then e.Handled = True

                    Case ChrW(Keys.Back)
                        '/ Return False is Default value

                    Case Else
                        e.Handled = True

                End Select
        End Select
    End Sub

    Private Sub frmFoodDrinkMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        GC.SuppressFinalize(Me)
        Application.Exit()
    End Sub

End Class

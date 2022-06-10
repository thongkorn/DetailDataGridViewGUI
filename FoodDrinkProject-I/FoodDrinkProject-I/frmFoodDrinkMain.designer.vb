<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFoodDrinkMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgvData = New System.Windows.Forms.DataGridView()
        Me.txtSumTotal = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblLastPrice = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnDecrement = New System.Windows.Forms.Button()
        Me.btnIncrement = New System.Windows.Forms.Button()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvData
        '
        Me.dgvData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvData.Location = New System.Drawing.Point(452, 36)
        Me.dgvData.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvData.Name = "dgvData"
        Me.dgvData.Size = New System.Drawing.Size(663, 574)
        Me.dgvData.TabIndex = 2
        '
        'txtSumTotal
        '
        Me.txtSumTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSumTotal.BackColor = System.Drawing.Color.Yellow
        Me.txtSumTotal.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumTotal.ForeColor = System.Drawing.Color.Blue
        Me.txtSumTotal.Location = New System.Drawing.Point(942, 616)
        Me.txtSumTotal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumTotal.Name = "txtSumTotal"
        Me.txtSumTotal.Size = New System.Drawing.Size(173, 30)
        Me.txtSumTotal.TabIndex = 5
        Me.txtSumTotal.TabStop = False
        Me.txtSumTotal.Text = "txtSumTotal"
        Me.txtSumTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(819, 623)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 18)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Total Amount :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(453, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 18)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Search ID:"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(528, 6)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(186, 26)
        Me.txtSearch.TabIndex = 0
        '
        'lblLastPrice
        '
        Me.lblLastPrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLastPrice.ForeColor = System.Drawing.Color.Firebrick
        Me.lblLastPrice.Location = New System.Drawing.Point(900, 9)
        Me.lblLastPrice.Name = "lblLastPrice"
        Me.lblLastPrice.Size = New System.Drawing.Size(215, 23)
        Me.lblLastPrice.TabIndex = 6
        Me.lblLastPrice.Text = "lblLastPrice"
        Me.lblLastPrice.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(2, 6)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(450, 647)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Size = New System.Drawing.Size(442, 616)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(4, 4)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(434, 608)
        Me.Panel1.TabIndex = 0
        '
        'btnDecrement
        '
        Me.btnDecrement.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDecrement.BackColor = System.Drawing.Color.DarkOrange
        Me.btnDecrement.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDecrement.FlatAppearance.BorderColor = System.Drawing.Color.DarkOrange
        Me.btnDecrement.FlatAppearance.BorderSize = 0
        Me.btnDecrement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDecrement.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnDecrement.ForeColor = System.Drawing.Color.White
        Me.btnDecrement.Location = New System.Drawing.Point(452, 613)
        Me.btnDecrement.Name = "btnDecrement"
        Me.btnDecrement.Size = New System.Drawing.Size(106, 36)
        Me.btnDecrement.TabIndex = 3
        Me.btnDecrement.Text = "Decrement"
        Me.btnDecrement.UseVisualStyleBackColor = False
        '
        'btnIncrement
        '
        Me.btnIncrement.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnIncrement.BackColor = System.Drawing.Color.RoyalBlue
        Me.btnIncrement.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnIncrement.FlatAppearance.BorderColor = System.Drawing.Color.DarkOrange
        Me.btnIncrement.FlatAppearance.BorderSize = 0
        Me.btnIncrement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIncrement.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnIncrement.ForeColor = System.Drawing.Color.White
        Me.btnIncrement.Location = New System.Drawing.Point(564, 613)
        Me.btnIncrement.Name = "btnIncrement"
        Me.btnIncrement.Size = New System.Drawing.Size(106, 36)
        Me.btnIncrement.TabIndex = 4
        Me.btnIncrement.Text = "Increment"
        Me.btnIncrement.UseVisualStyleBackColor = False
        '
        'frmFoodDrinkMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1116, 651)
        Me.Controls.Add(Me.btnIncrement)
        Me.Controls.Add(Me.btnDecrement)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.lblLastPrice)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtSumTotal)
        Me.Controls.Add(Me.dgvData)
        Me.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmFoodDrinkMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Food & Drink Project - coDe bY: Thongkorn Tubtimkrob"
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvData As System.Windows.Forms.DataGridView
    Friend WithEvents txtSumTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblLastPrice As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnDecrement As System.Windows.Forms.Button
    Friend WithEvents btnIncrement As System.Windows.Forms.Button

End Class

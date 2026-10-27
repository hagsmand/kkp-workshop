<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt3 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmb1 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtp1 = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmb2 = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmb3 = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt4 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtp2 = New System.Windows.Forms.DateTimePicker()
        Me.btn5 = New System.Windows.Forms.Button()
        Me.btn6 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "MP Name:"
        '
        'txt2
        '
        Me.txt2.Location = New System.Drawing.Point(120, 12)
        Me.txt2.Name = "txt2"
        Me.txt2.Size = New System.Drawing.Size(250, 20)
        Me.txt2.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Question Text:"
        '
        'txt3
        '
        Me.txt3.Location = New System.Drawing.Point(120, 42)
        Me.txt3.Multiline = True
        Me.txt3.Name = "txt3"
        Me.txt3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt3.Size = New System.Drawing.Size(250, 60)
        Me.txt3.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 115)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "PQ Type:"
        '
        'cmb1
        '
        Me.cmb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb1.FormattingEnabled = True
        Me.cmb1.Items.AddRange(New Object() {"Written", "Oral", "Priority Written"})
        Me.cmb1.Location = New System.Drawing.Point(120, 112)
        Me.cmb1.Name = "cmb1"
        Me.cmb1.Size = New System.Drawing.Size(121, 21)
        Me.cmb1.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 145)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Date Received:"
        '
        'dtp1
        '
        Me.dtp1.Location = New System.Drawing.Point(120, 142)
        Me.dtp1.Name = "dtp1"
        Me.dtp1.Size = New System.Drawing.Size(200, 20)
        Me.dtp1.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 175)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Assigned Team:"
        '
        'cmb2
        '
        Me.cmb2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb2.FormattingEnabled = True
        Me.cmb2.Items.AddRange(New Object() {"Policy Team A", "Policy Team B", "Private Office"})
        Me.cmb2.Location = New System.Drawing.Point(120, 172)
        Me.cmb2.Name = "cmb2"
        Me.cmb2.Size = New System.Drawing.Size(121, 21)
        Me.cmb2.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 205)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Status:"
        '
        'cmb3
        '
        Me.cmb3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb3.FormattingEnabled = True
        Me.cmb3.Items.AddRange(New Object() {"Received", "Assigned", "Drafted", "Approved", "Answered"})
        Me.cmb3.Location = New System.Drawing.Point(120, 202)
        Me.cmb3.Name = "cmb3"
        Me.cmb3.Size = New System.Drawing.Size(121, 21)
        Me.cmb3.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 235)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Response Text:"
        '
        'txt4
        '
        Me.txt4.Location = New System.Drawing.Point(120, 232)
        Me.txt4.Multiline = True
        Me.txt4.Name = "txt4"
        Me.txt4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt4.Size = New System.Drawing.Size(250, 60)
        Me.txt4.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 305)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Date Answered:"
        '
        'dtp2
        '
        Me.dtp2.Location = New System.Drawing.Point(120, 302)
        Me.dtp2.Name = "dtp2"
        Me.dtp2.Size = New System.Drawing.Size(200, 20)
        Me.dtp2.TabIndex = 15
        '
        'btn5
        '
        Me.btn5.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btn5.Location = New System.Drawing.Point(215, 340)
        Me.btn5.Name = "btn5"
        Me.btn5.Size = New System.Drawing.Size(75, 23)
        Me.btn5.TabIndex = 16
        Me.btn5.Text = "OK"
        Me.btn5.UseVisualStyleBackColor = True
        '
        'btn6
        '
        Me.btn6.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn6.Location = New System.Drawing.Point(296, 340)
        Me.btn6.Name = "btn6"
        Me.btn6.Size = New System.Drawing.Size(75, 23)
        Me.btn6.TabIndex = 17
        Me.btn6.Text = "Cancel"
        Me.btn6.UseVisualStyleBackColor = True
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 375)
        Me.Controls.Add(Me.btn6)
        Me.Controls.Add(Me.btn5)
        Me.Controls.Add(Me.dtp2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txt4)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmb3)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmb2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dtp1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmb1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add/Edit PQ"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txt2 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt3 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmb1 As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents dtp1 As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents cmb2 As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmb3 As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txt4 As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents dtp2 As DateTimePicker
    Friend WithEvents btn5 As Button
    Friend WithEvents btn6 As Button
End Class
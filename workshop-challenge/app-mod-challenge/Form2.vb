Public Class Form2
    Public isEdit As Boolean = False
    Public mpName As String = ""
    Public questionText As String = ""
    Public pqType As String = ""
    Public dateReceived As Date = Date.Now
    Public assignedTeam As String = ""
    Public status As String = ""
    Public responseText As String = ""
    Public dateAnswered As Date = Nothing

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If isEdit Then
            Me.Text = "Edit PQ"
            txt2.Text = mpName
            txt3.Text = questionText
            cmb1.Text = pqType
            dtp1.Value = dateReceived
            cmb2.Text = assignedTeam
            cmb3.Text = status
            txt4.Text = responseText
            If dateAnswered <> Nothing Then
                dtp2.Value = dateAnswered
            End If
        Else
            Me.Text = "Add New PQ"
            cmb1.SelectedIndex = 0
            cmb2.SelectedIndex = 0
            cmb3.SelectedIndex = 0
            dtp1.Value = Date.Now
        End If
    End Sub

    Private Sub btn5_Click(sender As Object, e As EventArgs) Handles btn5.Click
        If txt2.Text.Trim() = "" Then
            MessageBox.Show("MP Name is required")
            txt2.Focus()
            Return
        End If
        
        If txt3.Text.Trim() = "" Then
            MessageBox.Show("Question Text is required")
            txt3.Focus()
            Return
        End If
        
        If cmb1.SelectedIndex = -1 Then
            MessageBox.Show("PQ Type is required")
            cmb1.Focus()
            Return
        End If
        
        If cmb2.SelectedIndex = -1 Then
            MessageBox.Show("Assigned Team is required")
            cmb2.Focus()
            Return
        End If
        
        mpName = txt2.Text.Trim()
        questionText = txt3.Text.Trim()
        pqType = cmb1.Text
        dateReceived = dtp1.Value
        assignedTeam = cmb2.Text
        status = cmb3.Text
        responseText = txt4.Text.Trim()
        
        If cmb3.Text = "Answered" Then
            dateAnswered = dtp2.Value
        Else
            dateAnswered = Nothing
        End If
        
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btn6_Click(sender As Object, e As EventArgs) Handles btn6.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class
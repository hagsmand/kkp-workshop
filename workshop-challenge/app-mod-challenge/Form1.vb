Public Class Form1
    Dim pqData As New DataTable
    Dim fileName As String = "pqdata.csv"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setupDataTable()
        loadData()
        If pqData.Rows.Count = 0 Then
            generateSampleData()
        End If
        refreshGrid()
    End Sub

    Private Sub setupDataTable()
        pqData.Columns.Add("Reference")
        pqData.Columns.Add("MPName")
        pqData.Columns.Add("QuestionText")
        pqData.Columns.Add("PQType")
        pqData.Columns.Add("DateReceived", GetType(Date))
        pqData.Columns.Add("Deadline", GetType(Date))
        pqData.Columns.Add("AssignedTeam")
        pqData.Columns.Add("Status")
        pqData.Columns.Add("ResponseText")
        pqData.Columns.Add("DateAnswered", GetType(Date))
        pqData.Columns.Add("DaysRemaining")
    End Sub

    Private Sub refreshGrid()
        For Each row As DataRow In pqData.Rows
            If Not IsDBNull(row("Deadline")) Then
                Dim deadline As Date = row("Deadline")
                Dim daysLeft As Integer = calculateWorkingDays(Date.Now, deadline)
                row("DaysRemaining") = daysLeft
            End If
        Next
        dgv1.DataSource = pqData
        dgv1.Columns("QuestionText").Width = 200
        dgv1.Columns("ResponseText").Width = 200
        dgv1.Columns("MPName").Width = 120
        dgv1.Columns("Reference").Width = 120
    End Sub

    Private Function calculateWorkingDays(startDate As Date, endDate As Date) As Integer
        Dim workingDays As Integer = 0
        Dim currentDate As Date = startDate.Date
        
        While currentDate <= endDate.Date
            If currentDate.DayOfWeek <> DayOfWeek.Saturday And currentDate.DayOfWeek <> DayOfWeek.Sunday Then
                workingDays += 1
            End If
            currentDate = currentDate.AddDays(1)
        End While
        
        Return workingDays - 1
    End Function

    Private Function addWorkingDays(startDate As Date, workingDays As Integer) As Date
        Dim currentDate As Date = startDate.Date
        Dim daysAdded As Integer = 0
        
        While daysAdded < workingDays
            currentDate = currentDate.AddDays(1)
            If currentDate.DayOfWeek <> DayOfWeek.Saturday And currentDate.DayOfWeek <> DayOfWeek.Sunday Then
                daysAdded += 1
            End If
        End While
        
        Return currentDate
    End Function

    Private Function generatePQRef() As String
        Dim dateStr As String = Date.Now.ToString("yyyyMMdd")
        Dim counter As Integer = 1
        Dim baseRef As String = "PQ-" & dateStr & "-"
        
        For Each row As DataRow In pqData.Rows
            If row("Reference").ToString().StartsWith(baseRef) Then
                counter += 1
            End If
        Next
        
        Return baseRef & counter.ToString("000")
    End Function

    Private Sub generateSampleData()
        Try
            Dim sampleData As String() = {
                "PQ-20240315-001,John Smith MP,What is the department budget for 2024?,Written,2024-03-15,2024-03-26,Policy Team A,Received,,",
                "PQ-20240316-001,Sarah Jones MP,How many staff work in the department?,Oral,2024-03-16,2024-03-21,Policy Team B,Assigned,,",
                "PQ-20240317-001,Michael Brown MP,What are the key priorities for this year?,Priority Written,2024-03-17,2024-03-22,Private Office,Drafted,Draft response prepared,",
                "PQ-20240318-001,Emma Wilson MP,How much was spent on IT last year?,Written,2024-03-18,2024-03-29,Policy Team A,Approved,Response approved by Minister,2024-03-28",
                "PQ-20240319-001,David Taylor MP,What is the department doing about climate change?,Written,2024-03-19,2024-04-02,Policy Team B,Answered,The department is committed to reducing emissions by 50% by 2030,2024-04-01",
                "PQ-20240320-001,Lisa Anderson MP,How many FOI requests were received last year?,Oral,2024-03-20,2024-03-25,Private Office,Received,,",
                "PQ-20240321-001,Robert Clark MP,What training is provided to new staff?,Priority Written,2024-03-21,2024-03-26,Policy Team A,Overdue,,",
                "PQ-20240322-001,Helen Davis MP,What is the department policy on remote working?,Written,2024-03-22,2024-04-04,Policy Team B,Assigned,,"
            }
            
            For Each line As String In sampleData
                Dim parts As String() = line.Split(","c)
                Dim newRow As DataRow = pqData.NewRow()
                newRow("Reference") = parts(0)
                newRow("MPName") = parts(1)
                newRow("QuestionText") = parts(2)
                newRow("PQType") = parts(3)
                newRow("DateReceived") = Date.Parse(parts(4))
                newRow("Deadline") = Date.Parse(parts(5))
                newRow("AssignedTeam") = parts(6)
                newRow("Status") = parts(7)
                newRow("ResponseText") = parts(8)
                If parts(9) <> "" Then
                    newRow("DateAnswered") = Date.Parse(parts(9))
                End If
                pqData.Rows.Add(newRow)
            Next
            
        Catch ex As Exception
            MessageBox.Show("Error generating sample data: " & ex.Message)
        End Try
    End Sub

    Private Sub loadData()
        Try
            If System.IO.File.Exists(fileName) Then
                Dim lines As String() = System.IO.File.ReadAllLines(fileName)
                If lines.Length > 1 Then
                    For i As Integer = 1 To lines.Length - 1
                        Dim parts As String() = lines(i).Split(","c)
                        If parts.Length >= 10 Then
                            Dim newRow As DataRow = pqData.NewRow()
                            newRow("Reference") = parts(0)
                            newRow("MPName") = parts(1)
                            newRow("QuestionText") = parts(2)
                            newRow("PQType") = parts(3)
                            newRow("DateReceived") = Date.Parse(parts(4))
                            newRow("Deadline") = Date.Parse(parts(5))
                            newRow("AssignedTeam") = parts(6)
                            newRow("Status") = parts(7)
                            newRow("ResponseText") = parts(8)
                            If parts(9) <> "" Then
                                newRow("DateAnswered") = Date.Parse(parts(9))
                            End If
                            pqData.Rows.Add(newRow)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        End Try
    End Sub

    Private Sub saveData()
        Try
            Dim lines As New List(Of String)
            lines.Add("Reference,MPName,QuestionText,PQType,DateReceived,Deadline,AssignedTeam,Status,ResponseText,DateAnswered")
            
            For Each row As DataRow In pqData.Rows
                Dim line As String = row("Reference") & "," &
                                   row("MPName") & "," &
                                   row("QuestionText") & "," &
                                   row("PQType") & "," &
                                   row("DateReceived") & "," &
                                   row("Deadline") & "," &
                                   row("AssignedTeam") & "," &
                                   row("Status") & "," &
                                   row("ResponseText") & ","
                If Not IsDBNull(row("DateAnswered")) Then
                    line &= row("DateAnswered")
                End If
                lines.Add(line)
            Next
            
            System.IO.File.WriteAllLines(fileName, lines.ToArray())
            MessageBox.Show("Data saved successfully")
        Catch ex As Exception
            MessageBox.Show("Error saving data: " & ex.Message)
        End Try
    End Sub

    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        Dim form2 As New Form2()
        form2.isEdit = False
        If form2.ShowDialog() = DialogResult.OK Then
            Dim newRow As DataRow = pqData.NewRow()
            newRow("Reference") = generatePQRef()
            newRow("MPName") = form2.mpName
            newRow("QuestionText") = form2.questionText
            newRow("PQType") = form2.pqType
            newRow("DateReceived") = form2.dateReceived
            
            Dim workingDays As Integer = 7
            If form2.pqType = "Oral" Or form2.pqType = "Priority Written" Then
                workingDays = 3
            End If
            
            newRow("Deadline") = addWorkingDays(form2.dateReceived, workingDays)
            newRow("AssignedTeam") = form2.assignedTeam
            newRow("Status") = "Received"
            newRow("ResponseText") = ""
            
            pqData.Rows.Add(newRow)
            refreshGrid()
            saveData()
        End If
    End Sub

    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click
        If dgv1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = dgv1.SelectedRows(0)
            Dim form2 As New Form2()
            form2.isEdit = True
            form2.mpName = selectedRow.Cells("MPName").Value.ToString()
            form2.questionText = selectedRow.Cells("QuestionText").Value.ToString()
            form2.pqType = selectedRow.Cells("PQType").Value.ToString()
            form2.dateReceived = selectedRow.Cells("DateReceived").Value
            form2.assignedTeam = selectedRow.Cells("AssignedTeam").Value.ToString()
            form2.status = selectedRow.Cells("Status").Value.ToString()
            form2.responseText = selectedRow.Cells("ResponseText").Value.ToString()
            If Not IsDBNull(selectedRow.Cells("DateAnswered").Value) Then
                form2.dateAnswered = selectedRow.Cells("DateAnswered").Value
            End If
            
            If form2.ShowDialog() = DialogResult.OK Then
                Dim dataRow As DataRow = pqData.Rows(selectedRow.Index)
                dataRow("MPName") = form2.mpName
                dataRow("QuestionText") = form2.questionText
                dataRow("PQType") = form2.pqType
                dataRow("DateReceived") = form2.dateReceived
                dataRow("AssignedTeam") = form2.assignedTeam
                dataRow("Status") = form2.status
                dataRow("ResponseText") = form2.responseText
                If form2.dateAnswered <> Nothing Then
                    dataRow("DateAnswered") = form2.dateAnswered
                End If
                
                refreshGrid()
                saveData()
            End If
        Else
            MessageBox.Show("Please select a PQ to edit")
        End If
    End Sub

    Private Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click
        refreshGrid()
    End Sub

    Private Sub btn4_Click(sender As Object, e As EventArgs) Handles btn4.Click
        If txt1.Text.Trim() <> "" Then
            Dim searchText As String = txt1.Text.Trim().ToLower()
            Dim filteredData As DataTable = pqData.Clone()
            
            For Each row As DataRow In pqData.Rows
                If row("MPName").ToString().ToLower().Contains(searchText) Or
                   row("Reference").ToString().ToLower().Contains(searchText) Then
                    filteredData.ImportRow(row)
                End If
            Next
            
            dgv1.DataSource = filteredData
        Else
            dgv1.DataSource = pqData
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        saveData()
    End Sub

    Private Sub LoadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click
        pqData.Clear()
        loadData()
        refreshGrid()
    End Sub

    Private Sub OverdueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OverdueToolStripMenuItem.Click
        Dim overdueData As DataTable = pqData.Clone()
        
        For Each row As DataRow In pqData.Rows
            If Not IsDBNull(row("Deadline")) Then
                If Date.Parse(row("Deadline")) < Date.Now.Date And row("Status").ToString() <> "Answered" Then
                    overdueData.ImportRow(row)
                End If
            End If
        Next
        
        dgv1.DataSource = overdueData
        MessageBox.Show("Showing " & overdueData.Rows.Count & " overdue PQs")
    End Sub

End Class
' =====================================================================================
' EVALUATION SPEC - CANNOT BE RUN.
' 12 xUnit tests describing the business rules of the legacy VB.NET Parliamentary
' Question tracker (Form1.vb / Form2.vb). Every expected value was derived by reading
' that source; the legacy app itself has no tests and cannot run on macOS/Linux.
'
' PqRules is a hypothetical, pure (UI-free) class. It does not exist. A Python rebuild
' is judged by whether it reproduces the same behaviour for the same inputs.
' Calendar facts used: 2024-03-15 = Friday, 2024-03-18 = Monday, 2024-03-20 = Wednesday.
' Bank holidays are NOT considered by the legacy code, so none are considered here.
' =====================================================================================
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports Xunit

Public Class PqRulesTests

    ' ---- Working days remaining (Form1.calculateWorkingDays) ------------------------

    <Fact>
    Public Sub T01_WorkingDaysRemaining_SameWeekday_IsZero()
        ' Weekdays counted inclusively from start to end, minus 1.
        Dim d = New Date(2024, 3, 20)                         ' Wednesday
        Assert.Equal(0, PqRules.CalculateWorkingDays(d, d))
    End Sub

    <Fact>
    Public Sub T02_WorkingDaysRemaining_MondayToFriday_IsFour()
        Assert.Equal(4, PqRules.CalculateWorkingDays(New Date(2024, 3, 18), New Date(2024, 3, 22)))
    End Sub

    <Fact>
    Public Sub T03_WorkingDaysRemaining_SkipsWeekend_FridayToMonday_IsOne()
        ' Fri + Mon are weekdays (2), minus 1. Saturday and Sunday are not counted.
        Assert.Equal(1, PqRules.CalculateWorkingDays(New Date(2024, 3, 15), New Date(2024, 3, 18)))
    End Sub

    <Fact>
    Public Sub T04_WorkingDaysRemaining_DeadlineAlreadyPassed_IsMinusOne()
        ' Start after end: the loop never runs, 0 - 1 = -1 (legacy returns -1, not a count of late days).
        Assert.Equal(-1, PqRules.CalculateWorkingDays(New Date(2024, 3, 18), New Date(2024, 3, 15)))
    End Sub

    ' ---- Deadline by PQ type (Form1.btn1_Click + addWorkingDays) -------------------

    <Fact>
    Public Sub T05_Deadline_Written_IsSevenWorkingDaysAfterReceipt()
        ' Mon 18 Mar + 7 working days -> Wed 27 Mar.
        Assert.Equal(New Date(2024, 3, 27), PqRules.CalculateDeadline("Written", New Date(2024, 3, 18)))
    End Sub

    <Fact>
    Public Sub T06_Deadline_Oral_IsThreeWorkingDays_SkippingWeekend()
        ' Thu 14 Mar + 3 working days (Fri, Mon, Tue) -> Tue 19 Mar.
        Assert.Equal(New Date(2024, 3, 19), PqRules.CalculateDeadline("Oral", New Date(2024, 3, 14)))
    End Sub

    <Fact>
    Public Sub T07_Deadline_PriorityWritten_IsThreeWorkingDays_NotSeven()
        ' Priority Written follows the Oral rule. Mon 18 Mar + 3 -> Thu 21 Mar.
        Assert.Equal(New Date(2024, 3, 21), PqRules.CalculateDeadline("Priority Written", New Date(2024, 3, 18)))
    End Sub

    ' ---- Reference numbers (Form1.generatePQRef) -----------------------------------

    <Fact>
    Public Sub T08_Reference_FirstOfTheDay_Ends001()
        Dim today = New Date(2024, 3, 25)
        Assert.Equal("PQ-20240325-001", PqRules.GenerateReference(today, New List(Of String)()))
    End Sub

    <Fact>
    Public Sub T09_Reference_CounterCountsOnlySameDateReferences_ThreeDigitPadded()
        Dim today = New Date(2024, 3, 25)
        Dim existing = New List(Of String) From {
            "PQ-20240325-001",
            "PQ-20240325-002",
            "PQ-20240324-001"      ' different day: must not be counted
        }
        Assert.Equal("PQ-20240325-003", PqRules.GenerateReference(today, existing))
    End Sub

    ' ---- Search and overdue report (Form1.btn4_Click / OverdueToolStripMenuItem) ----

    <Fact>
    Public Sub T10_Search_IsCaseInsensitive_MatchesMpNameOrReference_NotQuestionText()
        Dim records = New List(Of PqRecord) From {
            New PqRecord("PQ-20240315-001", "John Smith MP", "What is the budget?", New Date(2024, 3, 26), "Received"),
            New PqRecord("PQ-20240316-001", "Sarah Jones MP", "Ask about SMITH report", New Date(2024, 3, 21), "Assigned")
        }
        Dim byName = PqRules.Search(records, "  smith ")      ' trimmed, lower-cased
        Dim byRef = PqRules.Search(records, "20240316")
        Assert.Equal({"PQ-20240315-001"}, byName.Select(Function(r) r.Reference).ToArray())   ' question text ignored
        Assert.Equal({"PQ-20240316-001"}, byRef.Select(Function(r) r.Reference).ToArray())
        Assert.Equal(2, PqRules.Search(records, "   ").Count)                                 ' blank = show all
    End Sub

    <Fact>
    Public Sub T11_Overdue_DeadlineBeforeTodayAndNotAnswered_OnlyAnsweredAndTodayExcluded()
        Dim today = New Date(2024, 3, 25)
        Dim records = New List(Of PqRecord) From {
            New PqRecord("A", "x", "q", New Date(2024, 3, 22), "Drafted"),    ' late, open        -> overdue
            New PqRecord("B", "x", "q", New Date(2024, 3, 22), "Approved"),   ' late, only Answered excludes -> overdue
            New PqRecord("C", "x", "q", New Date(2024, 3, 22), "Answered"),   ' late but answered   -> not overdue
            New PqRecord("D", "x", "q", New Date(2024, 3, 25), "Received")    ' due today          -> not overdue
        }
        Dim overdue = PqRules.OverdueReport(records, today).Select(Function(r) r.Reference).ToArray()
        Assert.Equal({"A", "B"}, overdue)
    End Sub

    ' ---- Input validation (Form2.btn5_Click) ---------------------------------------

    <Fact>
    Public Sub T12_Validation_BlankOrWhitespaceMpNameOrQuestion_IsRejectedWithMessage()
        Assert.Equal("MP Name is required", PqRules.ValidateNewPq("   ", "A question"))
        Assert.Equal("Question Text is required", PqRules.ValidateNewPq("Jane Doe MP", "   "))
        Assert.Null(PqRules.ValidateNewPq("Jane Doe MP", "A question"))      ' Nothing = valid
    End Sub

End Class

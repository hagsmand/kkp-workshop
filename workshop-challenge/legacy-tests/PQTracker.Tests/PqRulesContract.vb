' Signatures the tests assume. NOT IMPLEMENTED - this file documents the contract only.
' A Python rebuild should expose equivalent pure functions (no UI, no file or clock access
' except through parameters).
Imports System
Imports System.Collections.Generic

Public Class PqRecord
    Public Sub New(reference As String, mpName As String, questionText As String, deadline As Date, status As String)
    End Sub
    Public ReadOnly Property Reference As String
End Class

Public Module PqRules
    ' Weekdays from start..end inclusive, minus 1. Weekends only; no bank holidays.
    Public Function CalculateWorkingDays(startDate As Date, endDate As Date) As Integer
        Throw New NotImplementedException()
    End Function

    ' 3 working days for "Oral" and "Priority Written"; 7 for everything else.
    Public Function CalculateDeadline(pqType As String, dateReceived As Date) As Date
        Throw New NotImplementedException()
    End Function

    ' "PQ-" & yyyyMMdd & "-" & (count of existing refs with that prefix + 1) padded to 3 digits.
    Public Function GenerateReference(today As Date, existingReferences As IEnumerable(Of String)) As String
        Throw New NotImplementedException()
    End Function

    ' Trim + lower-case; match MP name OR reference (substring). Blank text returns all.
    Public Function Search(records As IEnumerable(Of PqRecord), text As String) As List(Of PqRecord)
        Throw New NotImplementedException()
    End Function

    ' Deadline < today AND status <> "Answered".
    Public Function OverdueReport(records As IEnumerable(Of PqRecord), today As Date) As List(Of PqRecord)
        Throw New NotImplementedException()
    End Function

    ' Returns the first error message, or Nothing when valid. MP name is checked before question.
    Public Function ValidateNewPq(mpName As String, questionText As String) As String
        Throw New NotImplementedException()
    End Function
End Module

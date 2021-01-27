﻿
Partial Class Admin_EditDeposit
    Inherits System.Web.UI.Page

    Public mode As String = "edit"
    Public tid As String = 0
    Public bankList As New Dictionary(Of Integer, String)

    Private Sub Admin_EditTransaction_Load(sender As Object, e As EventArgs) Handles Me.Load
        mode = Request.QueryString("mode")
        tid = Request.QueryString("id")

        If Not IsPostBack Then
            Dim banks = db.TblBanks.Where(Function(x) x.Status = 1).ToList
            For Each bank As TblBank In banks
                cmbPaymentMethod.Items.Add(New ListItem(bank.BankName.Trim & " (" & bank.AccountName.Trim & ")", bank.BankName.Trim))
                bankList.Add(bank.BankID, bank.BankName.Trim & " (" & bank.AccountName.Trim & ")")
            Next
            Dim rejects = db.TblTRejectReasons.Where(Function(x) x.Status = True).ToList
            For Each reason As TblTRejectReason In rejects
                cmbRejectReason.Items.Add(New ListItem(reason.TrReason.Trim, reason.TrReason.Trim))
            Next

            Pending(CInt(tid))

            Select Case mode
                Case "edit"
                    Try
                        Dim t = db.TblTransactions.Single(Function(x) x.TransactionID = CInt(tid))
                        Dim p = db.TblProducts.Single(Function(x) x.ProductID = t.ProductID)
                        Dim m = db.TblMembers.Single(Function(x) x.UserName = t.UserName)

                        h6Title.InnerText = "Transaction " & t.TransactionID.ToString("00000")
                        lblID.InnerText = t.TransactionID.ToString("00000")
                        lblProduct.InnerText = p.ProductName.Trim
                        lblProductUsername.InnerText = t.ProductUserName.Trim
                        lblUserID.InnerText = t.UserName.Trim
                        lblFullName.InnerText = m.FullName.Trim
                        lblIpAddress.InnerText = t.IPAddress.Trim
                        lblTime.InnerText = t.TransactionDate.ToString(dateFormat)
                        lblAmount.InnerText = If(t.TransType = 0, t.Credit.ToString("0.00"), t.Promotion.ToString("0.00"))
                        lblMethod.InnerText = t.Method.Trim
                        lblChannel.InnerText = TransactionChannelToString(t.Channel)
                        lblDepositTime.InnerText = t.TransactionDateUser.ToString(dateFormat)
                        lblRefNo.InnerText = If(t.Reference = Nothing, "-", t.Reference.Trim)
                        lblAffiliate.InnerText = If(m.Affiliate = Nothing, "-", m.Affiliate.Trim)
                        lblStatus.InnerText = StatusToString(t.Status)
                        If t.Status <= 1 Then
                            btnSave.Visible = False
                            btnApprove.Visible = True
                            btnReject.Visible = True
                        Else
                            btnSave.Visible = True
                            btnApprove.Visible = False
                            btnReject.Visible = False
                        End If
                        If t.UploadFile <> Nothing Then
                            bankSlip.Attributes("href") = "..\" & t.UploadFile.Trim
                        Else
                            bankSlip.Visible = False
                        End If
                        If Not t.ApproveBank = Nothing Then cmbPaymentMethod.SelectedValue = t.ApproveBank.Trim
                        If Not t.Reason = Nothing Then cmbRejectReason.SelectedValue = t.Reason.Trim
                        If Not t.Remark = Nothing Then txtRemarks.Text = t.Remark.Trim
                        If t.ApproveByUser.Trim <> "None" Then
                            txtConfirmUser.Text = t.ApproveByUser.Trim
                            txtConfirmDate.Text = t.ApproveDate.ToString(dateFormat)
                        End If
                    Catch ex As Exception
                        JsMsgBox("Transaction not found!")
                        btnApprove.Enabled = False
                        btnReject.Enabled = False
                        Exit Sub
                    End Try
                Case "approve"

                Case "reject"

                Case Else
            End Select
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("Transactions.aspx")
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Select Case mode
            Case "edit"
                If TryApproveTransaction() Then
                    If TryCreditToBank() Then
                        If TryCreditToSummary() Then
                            Response.Redirect("Transactions.aspx")
                        Else
                            JsMsgBox("Credit to summary failed! Please contact Administrator.")
                        End If
                    Else
                        JsMsgBox("Credit to bank record failed! Please contact Administrator.")
                    End If
                Else
                    JsMsgBox("Transaction Reject failed! Please contact Administrator.")
                End If
        End Select
    End Sub

    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Select Case mode
            Case "edit"
                If TryRejectTransaction() Then
                    Response.Redirect("Transactions.aspx")
                Else
                    JsMsgBox("Transaction Reject failed! Please contact Administrator.")
                End If
        End Select
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Select Case mode
            Case "edit"
                If TrySaveTransaction() Then
                    Response.Redirect("Transactions.aspx")
                Else
                    JsMsgBox("Transaction edit failed! Please contact Administrator.")
                End If
        End Select
    End Sub

    Private Function TryRejectTransaction() As Boolean
        Try
            Dim editTransaction = db.TblTransactions.Single(Function(x) x.TransactionID = CInt(tid))
            With editTransaction
                .Reason = cmbRejectReason.SelectedValue.Trim
                .Remark = txtRemarks.Text.Trim
                .Status = 3
                .ApproveByUser = Session("username").ToString.Trim
                .ApproveDate = Now
            End With

            db.SubmitChanges()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function TrySaveTransaction() As Boolean
        Try
            Dim editTransaction = db.TblTransactions.Single(Function(x) x.TransactionID = CInt(tid))
            With editTransaction
                .Reason = cmbRejectReason.SelectedValue.Trim
                .Remark = txtRemarks.Text.Trim
                .ApproveByUser = Session("username").ToString.Trim
                .ApproveDate = Now
            End With

            db.SubmitChanges()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function TryApproveTransaction() As Boolean
        Try
            Dim editTransaction = db.TblTransactions.Single(Function(x) x.TransactionID = CInt(tid))
            With editTransaction
                .Remark = txtRemarks.Text.Trim
                .Status = 2
                .ApproveByUser = Session("username").ToString.Trim
                .ApproveDate = Now
                .ApproveBank = cmbPaymentMethod.SelectedValue
            End With

            db.SubmitChanges()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function TryCreditToBank() As Boolean
        Dim t = db.TblTransactions.Single(Function(x) x.TransactionID = CInt(tid))

        Dim addBankRecord As New TblBankRecord
        With addBankRecord
            .BankID = bankList.Single(Function(x) x.Value = cmbPaymentMethod.SelectedItem.Text).Key
            .TransactionID = CInt(tid)
            .Credit = t.Credit
            .Debit = t.Debit
            .Description = lblUserID.InnerText.Trim & " deposited " & lblAmount.InnerText & "."
            .RecordDatetime = Now
        End With

        Try
            db.TblBankRecords.InsertOnSubmit(addBankRecord)
            db.SubmitChanges()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function TryCreditToSummary() As Boolean
        Dim t = db.TblTransactions.Single(Function(x) x.TransactionID = CInt(tid))

        Dim addSummary As New TblSummary
        With addSummary
            .ProductID = t.ProductID
            .Debit = t.Debit
            .Credit = t.Credit
            .Promotion = t.Promotion
            .SummaryDate = Now
            .TransactionID = CInt(tid)
        End With

        Try
            db.TblSummaries.InsertOnSubmit(addSummary)
            db.SubmitChanges()
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

End Class

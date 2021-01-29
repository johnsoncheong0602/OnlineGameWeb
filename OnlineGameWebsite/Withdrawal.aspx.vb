﻿
Partial Class Withdrawal
    Inherits System.Web.UI.Page

    Dim captcha As String = String.Empty

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If txtVerification.Text.Trim.Equals(Session("captcha").ToString.Trim) Then
            If Withdrawal() Then
                LogAction(Session("username").ToString.Trim, Request.UserHostAddress, eLogType.Debit)
                swalRedirect("TransactionHistory.aspx", "Success", "Your withdrawal request has been submitted successfully.", "success")
            Else
                swal("Oops!", "Withdrawal failed! Please contact Customer Service.", "error")
            End If
        Else
            swal("Oops!", "Incorrect captcha code, please try again.", "error")
        End If
    End Sub

    Private Sub Withdrawal_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim role As String = HttpContext.Current.Session("role")

        If role <> "user" Then
            LoginMsgBox
        Else
            Using db As New DataClassesDataContext
                Dim products = db.TblProducts.Where(Function(x) x.Status = True).ToList
                For Each pdt As TblProduct In products
                    Dim pdtName As String = Nothing
                    If String.IsNullOrWhiteSpace(pdt.ProductAlias) Then pdtName = pdt.ProductName.Trim Else pdtName = pdt.ProductAlias.Trim
                    cmbProduct.Items.Add(New ListItem(pdtName, pdt.ProductID))
                Next

                txtBankAccountName.Text = Session("fullname").ToString.Trim

                Try
                    Dim m = db.TblMembers.Single(Function(x) x.UserID = Session("userid").ToString.Trim)
                    txtBankAccountNo.Text = m.AccountNo.Trim
                    cmbBank.SelectedValue = m.BankName
                Catch ex As Exception
                    Log(ex)
                End Try
            End Using
        End If

        If Not IsPostBack Then
            captcha = New Random().Next(0, 999999)
            Session("captcha") = captcha
            captchaImg.Attributes("src") = "data:image/png;base64, " & TextToImage(captcha)
        End If
    End Sub

    Private Function Withdrawal() As Boolean
        Try
            Using db As New DataClassesDataContext
                Dim newTransaction As New TblTransaction
                With newTransaction
                    .TransactionDate = Now
                    .UserName = Session("username").ToString.Trim
                    .Method = cmbBank.SelectedItem.Text
                    .TransType = 1 'debit
                    .Debit = CSng(txtAmount.Text.Trim)
                    .Credit = 0F
                    .Promotion = 0F
                    .Channel = 4 'withdrawal
                    .Reason = Nothing
                    .ProductID = cmbProduct.SelectedValue
                    .ProductUserName = GetProductUserName(cmbProduct.SelectedValue, Session("username").ToString.Trim)
                    .Bank = txtBankAccountName.Text.Trim
                    .BankAccount = txtBankAccountNo.Text.Trim
                    .UploadFile = Nothing
                    .Reference = txtRemark.Text
                    .Status = 0
                    .IPAddress = Request.UserHostAddress.Trim
                    .ApproveByUser = "None"
                    .ApproveDate = Now
                    .Remark = Nothing
                    .TransactionDateUser = Now
                End With

                db.TblTransactions.InsertOnSubmit(newTransaction)
                db.SubmitChanges()
            End Using
        Catch ex As Exception
            Log(ex)
            Return False
        End Try
        Return True
    End Function

End Class
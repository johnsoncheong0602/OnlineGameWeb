﻿
Partial Class Admin_AdjustCredit
    Inherits System.Web.UI.Page

    Public mode As String = "credit"
    Public mid As String = 0

    Private Sub Admin_AdjustCredit_Load(sender As Object, e As EventArgs) Handles Me.Load
        mode = Request.QueryString("mode")
        mid = Request.QueryString("id")

        If Not IsPostBack Then
            If Not mid = Nothing Then txtUserID.Text = mid

            Select Case mode
                Case "credit"
                    Try
                        h6.InnerText = "Credit Adjustment"
                        Using db As New DataClassesDataContext
                            Dim members = db.TblMembers.Where(Function(x) x.Enabled = True).ToList
                            For Each m In members
                                userIdList.Controls.Add(AddOption(m.UserName.Trim, m.UserName.Trim))
                            Next

                            Dim banks = db.TblBanks.Where(Function(x) x.AllowCredit = True And x.Status = 1).ToList
                            For Each bank As TblBank In banks
                                cmbPaymentMethod.Items.Add(New ListItem(bank.BankName.Trim & " (" & bank.AccountName.Trim & ")", bank.BankID))
                            Next

                            Dim products = db.TblProducts.Where(Function(x) x.Status = True).ToList
                            For Each pdt As TblProduct In products
                                cmbProduct.Items.Add(New ListItem(pdt.ProductName, pdt.ProductID))
                            Next
                        End Using
                    Catch ex As Exception
                        Log(ex)
                    End Try
                Case "promo"
                    Try
                        h6.InnerText = "Promotion Adjustment"
                        Using db As New DataClassesDataContext
                            Dim members = db.TblMembers.Where(Function(x) x.Enabled = True).ToList
                            For Each m In members
                                userIdList.Controls.Add(AddOption(m.UserName.Trim, m.UserName.Trim))
                            Next

                            Dim bonuses = db.TblPromotions.Where(Function(x) x.AllowOnDeposit = True And x.Status = 1).ToList
                            For Each bonus As TblPromotion In bonuses
                                cmbPaymentMethod.Items.Add(New ListItem(bonus.PromoName.Trim, bonus.PromoID))
                            Next

                            Dim products = db.TblProducts.Where(Function(x) x.Status = True).ToList
                            For Each pdt As TblProduct In products
                                cmbProduct.Items.Add(New ListItem(pdt.ProductName, pdt.ProductID))
                            Next
                        End Using
                    Catch ex As Exception
                        Log(ex)
                    End Try
                Case "debit"
                    Try
                        h6.InnerText = "Debit Adjustment"
                        Using db As New DataClassesDataContext
                            Dim members = db.TblMembers.Where(Function(x) x.Enabled = True).ToList
                            For Each m In members
                                userIdList.Controls.Add(AddOption(m.UserName.Trim, m.UserName.Trim))
                            Next

                            Dim banks = db.TblBanks.Where(Function(x) x.AllowDebit = True And x.Status = 1).ToList
                            For Each bank As TblBank In banks
                                cmbPaymentMethod.Items.Add(New ListItem(bank.BankName.Trim & " (" & bank.AccountName.Trim & ")", bank.BankID))
                            Next

                            Dim products = db.TblProducts.Where(Function(x) x.Status = True).ToList
                            For Each pdt As TblProduct In products
                                cmbProduct.Items.Add(New ListItem(pdt.ProductName, pdt.ProductID))
                            Next
                        End Using
                    Catch ex As Exception
                        Log(ex)
                    End Try
            End Select
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Select Case mode
            Case "credit"
                If Deposit() Then
                    JsMsgBox("Credit adjustment added successfully.")
                    Response.Redirect("Transactions.aspx")
                Else
                    JsMsgBox("Credit adjustment add failed.")
                End If
            Case "promo"
                If Promotion() Then
                    JsMsgBox("Promotion adjustment added successfully.")
                    Response.Redirect("Transactions.aspx")
                Else
                    JsMsgBox("Promotion adjustment add failed.")
                End If
            Case "debit"
                If Withdrawal() Then
                    JsMsgBox("Debit adjustment added successfully.")
                    Response.Redirect("Transactions.aspx")
                Else
                    JsMsgBox("Debit adjustment add failed.")
                End If
        End Select
    End Sub

    Private Function AddOption(text As String, value As String) As HtmlGenericControl
        Dim opt As New HtmlGenericControl("option")
        With opt
            .Attributes("value") = value.Trim
            .InnerText = text.Trim
        End With
        Return opt
    End Function

    Private Function Deposit() As Boolean
        Try
            Using db As New DataClassesDataContext
                Dim newTransaction As New TblTransaction
                With newTransaction
                    .TransactionDate = Now
                    .UserName = txtUserID.Text.Trim
                    .Method = cmbPaymentMethod.SelectedItem.Text
                    .TransType = 0 'credit
                    .Debit = 0F
                    .Credit = CSng(txtAmount.Text.Trim)
                    .Promotion = 0F
                    .Channel = 5
                    .Reason = Nothing
                    .ProductID = cmbProduct.SelectedValue
                    .ProductUserName = GetProductUserName(cmbProduct.SelectedValue, txtUserID.Text.Trim)
                    .Bank = Nothing
                    .BankAccount = Nothing
                    .UploadFile = Nothing
                    .Reference = Nothing
                    .Status = 0
                    .IPAddress = "127.0.0.1"
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

    Private Function Promotion() As Boolean
        Try
            Using db As New DataClassesDataContext
                Dim newTransaction As New TblTransaction
                With newTransaction
                    .TransactionDate = Now
                    .UserName = txtUserID.Text.Trim
                    .Method = cmbPaymentMethod.SelectedItem.Text
                    .TransType = 2 'promotion
                    .Debit = 0F
                    .Credit = 0F
                    .Promotion = CSng(txtAmount.Text.Trim)
                    .Channel = 5
                    .Reason = Nothing
                    .ProductID = cmbProduct.SelectedValue
                    .ProductUserName = GetProductUserName(cmbProduct.SelectedValue, txtUserID.Text.Trim)
                    .Bank = Nothing
                    .BankAccount = Nothing
                    .UploadFile = Nothing
                    .Reference = Nothing
                    .Status = 0
                    .IPAddress = "127.0.0.1"
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

    Private Function Withdrawal() As Boolean
        Try
            Using db As New DataClassesDataContext
                Dim member = db.TblMembers.Single(Function(x) x.UserName = txtUserID.Text.Trim)
                Dim newTransaction As New TblTransaction
                With newTransaction
                    .TransactionDate = Now
                    .UserName = txtUserID.Text.Trim
                    .Method = cmbPaymentMethod.SelectedItem.Text
                    .TransType = 1 'debit
                    .Debit = CSng(txtAmount.Text.Trim)
                    .Credit = 0F
                    .Promotion = 0F
                    .Channel = 5
                    .Reason = Nothing
                    .ProductID = cmbProduct.SelectedValue
                    .ProductUserName = GetProductUserName(cmbProduct.SelectedValue, txtUserID.Text.Trim)
                    .Bank = member.MemberBank
                    .BankAccount = member.AccountNo.Trim
                    .UploadFile = Nothing
                    .Reference = Nothing
                    .Status = 0
                    .IPAddress = "127.0.0.1"
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

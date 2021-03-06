﻿
Partial Class Admin_EditAffiliate
    Inherits System.Web.UI.Page

    Public mode As String = "edit"
    Public aid As String = 0

    Private Sub Admin_EditAffiliate_Load(sender As Object, e As EventArgs) Handles Me.Load
        mode = Request.QueryString("mode")
        aid = Request.QueryString("id")

        If Not IsPostBack Then
            Select Case mode
                Case "edit"
                    Try
                        Using db As New DataClassesDataContext
                            Dim p = db.TblAffiliates.Single(Function(x) x.AffiliateID = CInt(aid))
                            txtUserName.Text = p.UserName.Trim
                            txtPassword.Text = p.Password.Trim
                            txtCode.Text = p.Code.Trim
                            txtFullName.Text = p.FullName.Trim
                            txtPhoneNo.Text = p.PhoneNo.Trim
                            txtEmail.Text = p.Email.Trim
                            cmbCalculation.SelectedValue = p.Calculation
                            txtPercentage.Text = p.Percentage * 100
                            cmbEnabled.SelectedValue = p.Status

                            txtUserName.ReadOnly = True
                            txtCode.ReadOnly = True

                            h6.InnerText = "Edit " & p.AffiliateID.ToString("00000")
                        End Using
                    Catch ex As Exception
                        Log(ex)
                        JsMsgBox("Affiliate not found!")
                        btnSubmit.Enabled = False
                    End Try
                Case "delete"
                    Try
                        Using db As New DataClassesDataContext
                            Dim p = db.TblAffiliates.Single(Function(x) x.AffiliateID = CInt(aid))
                            txtUserName.Text = p.UserName.Trim
                            txtPassword.Text = p.Password.Trim
                            txtCode.Text = p.Code.Trim
                            txtFullName.Text = p.FullName.Trim
                            txtPhoneNo.Text = p.PhoneNo.Trim
                            txtEmail.Text = p.Email.Trim
                            cmbCalculation.SelectedValue = p.Calculation
                            txtPercentage.Text = p.Percentage * 100
                            cmbEnabled.SelectedValue = p.Status

                            txtUserName.ReadOnly = True
                            txtPassword.ReadOnly = True
                            txtCode.ReadOnly = True
                            txtFullName.ReadOnly = True
                            txtPhoneNo.ReadOnly = True
                            txtEmail.ReadOnly = True
                            cmbCalculation.Enabled = False
                            txtPercentage.ReadOnly = True
                            cmbEnabled.Enabled = False

                            h6.InnerText = "Are you sure you want to delete " & p.UserName & " (" & p.AffiliateID.ToString("00000") & ")?"
                            btnSubmit.Text = "Delete"
                        End Using
                    Catch ex As Exception
                        Log(ex)
                        JsMsgBox("Affiliate not found!")
                        btnSubmit.Enabled = False
                    End Try
                Case Else
                    h6.InnerText = "Add New Affiliate"
                    btnSubmit.Text = "Insert"
            End Select
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Select Case mode
            Case "edit"
                If TryEditAffiliate() Then
                    JsMsgBoxRedirect("Affiliate update successfully.", "Affiliates.aspx")
                Else
                    JsMsgBox("Affiliate edit failed! Please contact Administrator.")
                End If
            Case "delete"
                Try
                    Using db As New DataClassesDataContext
                        Dim affToDelete = db.TblAffiliates.Single(Function(x) x.AffiliateID = CInt(aid))
                        For Each m In db.TblMembers.Where(Function(x) x.Affiliate = affToDelete.Code)
                            m.Affiliate = Nothing
                        Next

                        db.TblAffiliates.DeleteOnSubmit(affToDelete)
                        db.SubmitChanges()

                        JsMsgBoxRedirect("Affiliate delete successfully.", "Affiliates.aspx")
                    End Using
                Catch ex As Exception
                    Log(ex)
                    JsMsgBox("Delete affiliate failed! Please contact Administrator.")
                End Try
            Case Else
                If IsPartnerExists(txtUserName.Text.Trim) Then
                    JsMsgBox("User ID already exist, please try another.")
                ElseIf IsEmailExists(txtEmail.Text.Trim, eCheckEmail.Affiliate) Then
                    JsMsgBox("Email already exist, please try another.")
                Else
                    If AddNewAffiliate() Then
                        JsMsgBoxRedirect("Affiliate added successfully.", "Affiliates.aspx")
                    Else
                        JsMsgBox("Add affiliate failed! Please contact Administrator.")
                    End If
                End If
        End Select
    End Sub

    Private Function TryEditAffiliate() As Boolean
        Try
            Using db As New DataClassesDataContext
                Dim editAffiliate = db.TblAffiliates.Single(Function(x) x.AffiliateID = CInt(aid))
                With editAffiliate
                    .Password = txtPassword.Text.Trim
                    .FullName = txtFullName.Text.Trim
                    .Email = txtEmail.Text.Trim
                    .PhoneNo = txtPhoneNo.Text.Trim
                    .Status = cmbEnabled.SelectedValue
                    .Calculation = cmbCalculation.SelectedValue
                    .Percentage = CSng(CSng(txtPercentage.Text.Trim) / 100)
                End With

                db.SubmitChanges()
            End Using
        Catch ex As Exception
            Log(ex)

            Return False
        End Try

        Return True
    End Function

    Private Function AddNewAffiliate() As Boolean
        Try
            Using db As New DataClassesDataContext
                Dim newPartner As New TblAffiliate
                With newPartner
                    .Code = txtCode.Text.Trim
                    .UserName = txtUserName.Text.Trim
                    .Password = txtPassword.Text.Trim
                    .FullName = txtFullName.Text.Trim
                    .Email = txtEmail.Text.Trim
                    .PhoneNo = txtPhoneNo.Text.Trim
                    .DateCreated = Now
                    .Status = cmbEnabled.SelectedValue
                    .Calculation = cmbCalculation.SelectedValue
                    .Percentage = CSng(CSng(txtPercentage.Text.Trim) / 100)
                    .LastLoginDate = Now
                    .LastLoginIP = "127.0.0.1"
                End With

                db.TblAffiliates.InsertOnSubmit(newPartner)
                db.SubmitChanges()
            End Using
        Catch ex As Exception
            Log(ex)
            Return False
        End Try

        Return True
    End Function

End Class

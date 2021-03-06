﻿
Partial Class Partners_Members
    Inherits System.Web.UI.Page

    Private Sub Partners_Members_Load(sender As Object, e As EventArgs) Handles Me.Load
        Using db As New DataClassesDataContext
            Dim members = (From m In db.TblMembers Where m.Affiliate = Session("code").ToString.Trim Order By m.UserID Descending)
            For Each m As TblMember In members
                dataTable.AddTableItem(m.UserID.ToString("00000"), m.DateCreated.ToString(dateFormat), m.UserName.Trim, m.FullName.Trim, m.PhoneNo.Trim, m.Email.Trim,
                                      RB("ViewMember.aspx?id=" & m.UserID, "fas fa-eye", "btn-success", "View"))
            Next
        End Using
    End Sub
End Class

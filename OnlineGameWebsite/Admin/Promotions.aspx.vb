﻿
Partial Class Admin_Promotions
    Inherits System.Web.UI.Page

    Private Sub Admin_Promotions_Load(sender As Object, e As EventArgs) Handles Me.Load
        Using db As New DataClassesDataContext
            Dim promo = db.TblPromotions.OrderByDescending(Function(x) x.DisplayIndex)
            For Each p As TblPromotion In promo
                Dim hasImage As Boolean = p.PromoImage <> Nothing
                Dim value As String = String.Empty
                If p.PromoType = 0 Then
                    'Percent
                    value = (p.PromoPercent * 100) & "%"
                Else
                    'Fixed
                    value = CSng(p.PromoPercent).ToString("N")
                End If

                dataTable.AddTableItem(p.PromoID.ToString("00000"), p.PromoName.Trim, If(hasImage, Img("..\" & p.PromoImage.Trim, p.PromoImage.Trim), ""), p.DisplayIndex,
                                       value, p.MaxPayout.ToString("N"), PromotionTypeToString(p.PromoType), IntStatusToString(p.Status),
                                       RB("EditPromotion.aspx?mode=edit&id=" & p.PromoID, "fas fa-edit") &
                                       RB("EditPromotion.aspx?mode=delete&id=" & p.PromoID, "fas fa-trash", "btn-danger", "Delete") &
                                       RB("EditPromotion.aspx?mode=duplicate&id=" & p.PromoID, "fas fa-clone", "btn-info", "Duplicate"))
            Next
        End Using
    End Sub

    Private Sub btnAddPromo_Click(sender As Object, e As EventArgs) Handles btnAddPromo.Click
        Response.Redirect("EditPromotion.aspx?mode=new&id=-1")
    End Sub
End Class

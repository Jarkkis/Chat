Public Class Form1

    'Tämä alustetaan kirjautumisruudussa ennen tähän ikkunaan siirtymistä
    Public xmpp As xmpp

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'Suljetaan myös DEGUB-Ikkuna
        DEBUG.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        ' TODO: Jotai ikkunanalustusjuttuja
        xmpp.haeTunnukset(False)

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click

        'Muutetaan DEBUG-Ikkunan näkyvyys
        If DEBUG.Visible Then
            DEBUG.Hide()
        Else
            DEBUG.Show()
        End If

    End Sub
End Class

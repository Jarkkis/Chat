Public Class Form1

    'Tämä alustetaan kirjautumisruudussa ennen tähän ikkunaan siirtymistä
    Public xmpp As xmpp

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        xmpp.sisaan()
        ' TODO: Jotai ikkunanalustusjuttuja

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        xmpp.lue()
    End Sub
End Class


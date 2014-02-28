Public Class srv2client

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        'AutoScroll
        TextBox1.Select(TextBox1.TextLength, 0)
        TextBox1.ScrollToCaret()

        'Rivitys kuntoon
        TextBox1.Text = TextBox1.Text.Replace("><", ">" + Chr(13) + Chr(10) + "<")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Tarkistetaan lukemisen tarve
        If Form1.xmpp.VirranLukukelpoisuus Then
            Form1.xmpp.lue()
        End If

    End Sub

End Class

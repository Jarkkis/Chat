Public Class srv2client

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        'AutoScroll
        TextBox1.Select(TextBox1.TextLength, 0)
        TextBox1.ScrollToCaret()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Form1.xmpp.lue()

    End Sub

End Class

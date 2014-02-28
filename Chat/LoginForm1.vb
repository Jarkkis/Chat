Public Class LoginForm1

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

        srv2client.Show()

        'Alustetaan xmpp-kirjasto
        Form1.xmpp = New xmpp(Me.UsernameTextBox.Text, Me.PasswordTextBox.Text)

        If Form1.xmpp.sisaan Then
            'Vaihdetaan pääikkunaan
            Form1.Show()
            Me.Close()
        Else
            'Virheilmoitus, koska kirjautuminen ei onnistunut
            MsgBox("Tarkista käyttäjätunnus ja salasana!" + Chr(13) + Chr(10) + "Vika voi kyllä olla ohjelmassakin...", MsgBoxStyle.Exclamation, "Kirjautuminen ei onnistunut")
        End If


    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        End
    End Sub

End Class

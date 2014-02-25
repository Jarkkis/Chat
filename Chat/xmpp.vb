Public Class xmpp

    Private palvelin As String
    Private tunnus As String
    Private sala As String


    Public Sub New(usr As String, pw As String, Optional srv As String = "jarkkis.dy.fi")

        'Palvelin on oletuksena jarkkis.dy.fi, mutta sen voi asettaa construn kutsussa.
        palvelin = srv
        'Muut kirjautumiseen tarvittavat muuttujat tulee aina asettaa
        tunnus = usr
        sala = pw

    End Sub

End Class

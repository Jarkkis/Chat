Imports System.Net.Sockets
Imports System.Net.WebSockets
Imports System.Net
Imports System.Text

Public Class xmpp

    ' TODO: Pitäis kai sitä yhteyttä muodostella jotenki...

    'Palvelimen tiedot
    Private palvelin As String
    Private portti As Integer

    'Käyttäjän tiedot
    Private tunnus As String
    Private sala As String
    Private lahde As String
    Private id As String

    'Yhteys
    Dim yhteys As New TcpClient()
    Dim virta As NetworkStream

    'Puskuri sisään
    Dim puskuriS() As Byte
    Dim luku As String
    'Puskuri ulos
    Dim puskuriU() As Byte
    Dim kirjoitus As String

    Public Sub New(usr As String, pw As String, Optional src As String = "VirallinenSofta", Optional srv As String = "jarkkis.dy.fi", Optional port As Integer = 5222)

        'Palvelin on oletuksena jarkkis.dy.fi, mutta sen voi asettaa construn kutsussa.
        palvelin = srv
        portti = port

        'Muut kirjautumiseen tarvittavat muuttujat tulee aina asettaa
        tunnus = usr
        sala = pw
        lahde = src

    End Sub

    Public Function sisaan()

        Try
            'Yhdistä ja alusta
            yhteys.Connect(Dns.GetHostEntry(palvelin).AddressList.First.ToString, portti)
            virta = yhteys.GetStream
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.ToString)
        End Try

        'Alustetaan puskuri
        ReDim puskuriS(yhteys.ReceiveBufferSize)
        ReDim puskuriU(yhteys.SendBufferSize)

        'Lähetetään XML:n ja XMPP:n alustus
        kirjoita("<?xml version='1.0'?><stream:stream to='" + palvelin + "' xmlns='jabber:client' xmlns:stream='http://etherx.jabber.org/streams' version='1.0'>")

        'Luetaan
        lue()

        'Tarkistetaan tuliko ID jo
        While InStr(luku.ToLower, " id=") = 0
            lue()
        End While

        'Nyt se tuli
        id = luku.Substring(InStr(luku, " id=") + 4, 8)

        'Sitten PLAIN-authentikointi (oletetaan toimivan)
        kirjoita("<iq type='set' id='" + id + "'><query xmlns='jabber:iq:auth'><username>" + tunnus + "</username><password>" + sala + "</password><resource>" + lahde + "</resource></query></iq>")
        lue()
        If luku.ToLower.StartsWith("<iq type=""result""") Then
            'Kirjautuminen onnistui
            Return True
        ElseIf luku.ToLower.StartsWith("<iq type=""error""") Then
            'Kirjautuminen epäonnistui
            Return False
        Else
            'Tunnistamaton virhe
            MsgBox("Tuntematon vastaus palvelimelta!", MsgBoxStyle.Exclamation, "Virhe kirjautumisessa")
            Return False
        End If

    End Function

    Public Sub lue()

        'Tyhjennä puskuri
        Array.Clear(puskuriS, 0, puskuriS.Length)

        'Luetaan
        virta.Read(puskuriS, 0, yhteys.ReceiveBufferSize)
        'Muutetaan tekstiksi
        luku = Encoding.UTF8.GetString(puskuriS)

        'Pistellään debugiin
        srv2client.TextBox1.Text += luku
        srv2client.TextBox1.Text += Chr(13) + Chr(10) + Chr(13) + Chr(10)

    End Sub

    Public Sub kirjoita(teksti As String)

        'Muutetaan taulukoksi
        puskuriU = Encoding.UTF8.GetBytes(teksti)
        'Ulos
        virta.Write(puskuriU, 0, puskuriU.Length)

    End Sub

End Class

Imports System.Net
Imports System.Net.Sockets

Imports System.Text
Imports System.Text.Encoding

Public Class xmpp

    'Palvelimen tiedot
    Private palvelin As String
    Private portti As Integer

    'Käyttäjän tiedot
    Private tunnus As String
    Private sala As String
    Private lahde As String
    Private id As String

    'Yhteys
    Private yhteys As New TcpClient()
    Private virta As NetworkStream

    'Puskuri sisään
    Private puskuriS() As Byte
    Private luku As String
    'Puskuri ulos
    Private puskuriU() As Byte
    Private kirjoitus As String

    Public Sub New(usr As String, pw As String, Optional src As String = "VirallinenSofta", Optional srv As String = "jarkkis.dy.fi", Optional port As Integer = 5222)

        'Palvelin on oletuksena jarkkis.dy.fi, mutta sen voi asettaa construn kutsussa.
        palvelin = srv
        portti = port

        'Muut kirjautumiseen tarvittavat muuttujat tulee aina asettaa
        tunnus = usr
        sala = pw
        lahde = src

    End Sub

    Public Function sisaan() As Boolean

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
        kirjoita("<?xml version=""1.0"" encoding=""UTF-8""?>")
        kirjoita("<stream:stream to='" + palvelin + "' xmlns='jabber:client' xmlns:stream='http://etherx.jabber.org/streams' version='1.0'>")
        lue()   'XML-Muoto
        lue()   'XMPP-Virran aloitustiedot

        'Nyt se tuli
        id = luku.Substring(InStr(luku.ToLower, " id=") + 4, 8)

        'Sitten PLAIN-authentikointi (oletetaan toimivan)
        kirjoita("<iq type='set' id='" + id + "'><query xmlns='jabber:iq:auth'><username>" + tunnus + "</username><password>" + sala + "</password><resource>" + lahde + "</resource></query></iq>")
        lue()
        If luku.ToLower.StartsWith("<iq type=""result"" id=""" + id + """>") Then
            'Kirjautuminen onnistui
            Return True
        ElseIf luku.ToLower.StartsWith("<iq type=""error"" id=""" + id + """>") Then
            'Kirjautuminen epäonnistui
            Return False
        Else
            'Tunnistamaton virhe
            MsgBox("Tuntematon vastaus palvelimelta!", MsgBoxStyle.Exclamation, "Virhe kirjautumisessa")
            DEBUG.show()
            Return False
        End If

    End Function

    Public Sub lue()

        'Tyhjennä puskuri
        Array.Clear(puskuriS, 0, puskuriS.Length)

        'Luetaan
        virta.Read(puskuriS, 0, yhteys.ReceiveBufferSize)
        'Muutetaan tekstiksi
        luku = UTF8.GetString(puskuriS)

        'Pistellään debugiin
        DEBUG.TextBox1.Text += "<<< " + Chr(13) + Chr(10) + luku
        DEBUG.TextBox1.Text += Chr(13) + Chr(10) + Chr(13) + Chr(10)

    End Sub

    Public Sub kirjoita(teksti As String)

        'Muutetaan taulukoksi
        puskuriU = UTF8.GetBytes(teksti)
        'Ulos
        virta.Write(puskuriU, 0, puskuriU.Length)

        'Pistellään debugiin
        DEBUG.TextBox1.Text += ">>> " + Chr(13) + Chr(10) + teksti
        DEBUG.TextBox1.Text += Chr(13) + Chr(10) + Chr(13) + Chr(10)

    End Sub

    Public Sub haeTunnukset(Optional vainOnline As Boolean = True)

        'Ei huomioida roskadataa
        While VirranLukukelpoisuus()
            lue()
        End While

        'Lähetetään pyyntö
        kirjoita("<iq from=""" + tunnus + "@" + palvelin + "/" + lahde + """ id=""" + id + """ type=""get""><query xmlns=""jabber:iq:roster""/></iq>")
        'Vastaus
        lue()
    End Sub

    Public Function VirranLukukelpoisuus() As Boolean
        Return virta.DataAvailable
    End Function

End Class

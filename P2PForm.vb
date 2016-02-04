Imports System
Imports System.Threading
Imports System.Windows.Forms

Public Class P2PForm

    Private userName As String
    Private userMessage As UserMessage
    Private listenerThread As Thread
    Private lastMessageCount As Integer
    Private vectorClock As ArrayList

    Public Sub New(userName As String, ByRef messageQueue As ArrayList, vectorClock As ArrayList)
        InitializeComponent()
        lastMessageCount = messageQueue.Count
        Me.userName = userName
        Me.vectorClock = vectorClock
        listenerThread = New Thread(AddressOf Listener)
        listenerThread.IsBackground = True
        listenerThread.Start()
    End Sub

    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        If txtChat.Text.Trim() <> "" Then
            Dim i As Integer
            Dim vectorClockString As String

            vectorClockString = "("
            For Each i In vectorClock
                vectorClockString = vectorClockString & i.ToString & " "
            Next
            vectorClockString = vectorClockString & ")"

            userMessage = New UserMessage(userName, txtChat.Text)
            messageQueue.Add(userMessage.SentAtTime & " - " & userName & " says: " & userMessage.SentMessage & " - " & vectorClockString)
            txtChat.Text = ""
        End If
    End Sub

    Delegate Sub MyDelegate()

    Private Sub Listener()
        While (True)
            If lastMessageCount <> messageQueue.Count Then
                If txtHistory.InvokeRequired Then
                    BeginInvoke(New MyDelegate(AddressOf MySub))

                End If
                lastMessageCount = messageQueue.Count
            End If
        End While
    End Sub

    Sub MySub()
        txtHistory.Text = txtHistory.Text & messageQueue(messageQueue.Count - 1) & vbNewLine
    End Sub

    Private Sub P2PForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ActiveControl = txtChat
    End Sub
End Class

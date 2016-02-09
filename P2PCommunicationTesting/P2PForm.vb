Imports System
Imports System.Threading
Imports System.Windows.Forms

Public Class P2PForm

    Private userMessage As UserMessage
    Private listenerThread As Thread
    Private lastMessageCount As Integer
    Private currentTimeStamp As Integer
    Private currentProcess As UserProcess

    Public Sub New(userProcess As UserProcess, ByRef messageQueue As ArrayList, ByRef vectorClock As ArrayList)
        InitializeComponent()
        lastMessageCount = messageQueue.Count
        Me.currentProcess = userProcess
        Me.lblUsername.Text = currentProcess.User
        listenerThread = New Thread(AddressOf Listener)
        listenerThread.IsBackground = True
        listenerThread.Start()
    End Sub

    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        If txtChat.Text.Trim() <> "" Then
            Dim i As Integer
            Dim vectorClockStringBefore As String = String.Empty
            Dim vectorClockStringAfter As String = String.Empty

            vectorClockStringBefore = "("
            For Each i In vectorClock
                vectorClockStringBefore = vectorClockStringBefore & i.ToString & " "
            Next
            vectorClockStringBefore = vectorClockStringBefore & ")"

            Me.currentTimeStamp = currentTimeStamp + 1
            vectorClock(Me.currentProcess.PID) = currentTimeStamp

            vectorClockStringAfter = "("
            For Each i In vectorClock
                vectorClockStringAfter = vectorClockStringAfter & i.ToString & " "
            Next
            vectorClockStringAfter = vectorClockStringAfter & ")"

            userMessage = New UserMessage(Me.currentProcess.User, txtChat.Text)
            messageQueue.Add(userMessage.SentAtTime & " - " & Me.currentProcess.User & " says: " & userMessage.SentMessage & " - " & vectorClockStringBefore & "->" & vectorClockStringAfter)
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
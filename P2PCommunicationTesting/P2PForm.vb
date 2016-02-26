Imports System
Imports System.Threading
Imports System.Windows.Forms

''' <summary>
''' Class representing the interface
''' </summary>
Public Class P2PForm

    Private userMessage As UserMessage
    Private listenerThread As Thread
    Private lastMessageCount As Integer
    Private currentTimeStamp As Integer
    Private currentProcess As UserProcess
    Private messageQueue As ArrayList

    ''' <summary>
    ''' Generate new interface for process
    ''' </summary>
    ''' <param name="userProcess">process pertaining to this interface</param>
    ''' <param name="messageQueue">message queue pertaining to this interface</param>
    ''' <param name="vectorClock">vectorclock pertaining to this interface</param>
    Public Sub New(userProcess As UserProcess, messageQueue As ArrayList, ByRef vectorClock As ArrayList)
        InitializeComponent()
        Me.messageQueue = messageQueue
        lastMessageCount = messageQueue.Count
        Me.currentProcess = userProcess
        Me.lblUsername.Text = currentProcess.User

        'Generate listener
        listenerThread = New Thread(AddressOf Listener)
        listenerThread.IsBackground = True
        listenerThread.Start()
    End Sub

    ''' <summary>
    ''' Send message to other processes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        If txtChat.Text.Trim() <> "" Then

            IncrementVectorClockAndAddToQueue()

            lastMessageCount = messageQueue.Count
            txtHistory.Text = txtHistory.Text & messageQueue(messageQueue.Count - 1) & vbNewLine
            txtChat.Text = ""
        End If
    End Sub

    ''' <summary>
    ''' Increment the vectorclock for this process and add the message to the holder queue of the receivers
    ''' </summary>
    Private Sub IncrementVectorClockAndAddToQueue()
        Dim i As Integer
        Dim vectorClockStringBefore As String = String.Empty
        Dim vectorClockStringAfter As String = String.Empty

        'Set up vectorclock string
        vectorClockStringBefore = "("
        For Each i In vectorClock
            vectorClockStringBefore = vectorClockStringBefore & i.ToString & " "
        Next
        vectorClockStringBefore = vectorClockStringBefore & ")"

        'Increment vectorclock
        Me.currentTimeStamp = currentTimeStamp + 1
        vectorClock(Me.currentProcess.PID) = currentTimeStamp

        'Finish vectorclock string
        vectorClockStringAfter = "("
        For Each i In vectorClock
            vectorClockStringAfter = vectorClockStringAfter & i.ToString & " "
        Next
        vectorClockStringAfter = vectorClockStringAfter & ")"

        'Create message to send
        userMessage = New UserMessage(Me.currentProcess.User, txtChat.Text)

        'Broadcast message to neighbouring processes
        Dim p As UserProcess
        For Each p In currentProcess.AllProcesses
            p.AddToMessageQueue(userMessage.SentAtTime & " - " & Me.currentProcess.User & " says: " & userMessage.SentMessage & " - " & vectorClockStringBefore & "->" & vectorClockStringAfter)
        Next
    End Sub

    Delegate Sub MyDelegate()

    ''' <summary>
    ''' Listener waits for messages to be added to the holder queue
    ''' </summary>
    Private Sub Listener()
        'Always checking...
        While (True)
            'Compare last message count to new messages in holder
            If lastMessageCount <> messageQueue.Count Then
                If txtHistory.InvokeRequired Then
                    BeginInvoke(New MyDelegate(AddressOf DisplayNewMessages))
                End If
            End If
            'Simulates slight network lag
            Thread.Sleep(currentProcess.SleepInterval)
        End While
    End Sub

    ''' <summary>
    ''' Displays the newly received messages to user
    ''' </summary>
    Sub DisplayNewMessages()
        Dim i As Integer

        'Check message queue and display new messages
        For i = 0 To messageQueue.Count
            If i > lastMessageCount Then
                txtHistory.Text = txtHistory.Text & messageQueue(i - 1) & vbNewLine
            End If
        Next

        'Set last message counter to the newest count
        lastMessageCount = messageQueue.Count
    End Sub

    Private Sub P2PForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ActiveControl = txtChat
    End Sub
End Class
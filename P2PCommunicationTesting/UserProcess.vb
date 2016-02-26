Imports System.Threading
Imports System.Runtime.InteropServices

''' <summary>
''' Class represents a user process to be generated
''' </summary>
Public Class UserProcess

    Private id As Integer                       'id of the process
    Private userName As String                  'username of the process
    Private guiThread As Thread                 'gui thread
    Private chatForm As P2PForm                 'GUI of the process
    Private timeStamp As Integer                'timestamp for vectorclock
    Private rand As Integer                     'random integer for sleep timer of listener
    Private messageQueue As New ArrayList       'message queue for the messages
    Private otherProcesses() As UserProcess     'reference to neighbouring processes

    ''' <summary>
    ''' Generate a new user process
    ''' </summary>
    ''' <param name="id">id to assign to process</param>
    ''' <param name="processes">reference to other processes</param>
    ''' <param name="vectorClock">vectorclock for the process</param>
    ''' <param name="rand">random number for the sleep timer</param>
    Public Sub New(id As Integer, processes() As UserProcess, ByRef vectorClock As ArrayList, rand As Integer)
        Me.id = id
        Me.userName = "P" & id
        Me.timeStamp = 0
        Me.otherProcesses = processes
        Me.rand = rand

        'Generate sender thread
        guiThread = New Thread(AddressOf MessageTask)
        guiThread.IsBackground = False
        guiThread.Start()
    End Sub

    ''' <summary>
    ''' Show GUI
    ''' </summary>
    Sub MessageTask()
        chatForm = New P2PForm(Me, messageQueue, vectorClock)
        chatForm.ShowDialog()
    End Sub

    ''' <summary>
    ''' Adds messages to the qeueue from senders of other processes
    ''' </summary>
    ''' <param name="message">message to receive</param>
    Public Sub AddToMessageQueue(message As String)
        messageQueue.Add(message)
    End Sub

#Region "Getters/Setters"
    Property User() As String
        Get
            Return Me.userName
        End Get
        Set(ByVal Value As String)
            Me.userName = Value
        End Set
    End Property

    Property PID() As Integer
        Get
            Return Me.id
        End Get
        Set(ByVal Value As Integer)
            Me.id = Value
        End Set
    End Property

    Property CurrentTimeStamp() As Integer
        Get
            Return Me.timeStamp
        End Get
        Set(ByVal Value As Integer)
            Me.timeStamp = Value
        End Set
    End Property

    Property AllProcesses As UserProcess()
        Get
            Return Me.otherProcesses
        End Get
        Set(ByVal Value As UserProcess())
            Me.otherProcesses = Value
        End Set
    End Property

    Property SleepInterval() As Integer
        Get
            Return Me.rand
        End Get
        Set(ByVal Value As Integer)
            Me.rand = Value
        End Set
    End Property
#End Region

End Class

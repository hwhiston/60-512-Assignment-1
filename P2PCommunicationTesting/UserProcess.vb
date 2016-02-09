Imports System.Threading
Imports System.Runtime.InteropServices

Public Class UserProcess

    Private id As Integer
    Private userName As String
    Private userThread As Thread
    Private chatForm As P2PForm
    Private timeStamp As Integer

    Public Sub New(id As Integer, ByRef messageQueue As ArrayList, ByRef vectorClock As ArrayList)
        Me.id = id
        Me.userName = "P" & id
        Me.timeStamp = 0

        userThread = New Thread(AddressOf MessageTask)
        userThread.IsBackground = False
        userThread.Start()
    End Sub
    Sub MessageTask()
        chatForm = New P2PForm(Me, messageQueue, vectorClock)
        chatForm.ShowDialog()
    End Sub

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
            Return Me.id
        End Get
        Set(ByVal Value As Integer)
            Me.timeStamp = Value
        End Set
    End Property

End Class

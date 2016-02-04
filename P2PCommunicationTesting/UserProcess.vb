Imports System.Threading
Imports System.Runtime.InteropServices

Public Class UserProcess

    Private id As Integer
    Private userName As String
    Private userThread As Thread
    Private chatForm As P2PForm
    Private vectorClock As ArrayList

    Public Sub New(id As Integer, ByRef messageQueue As ArrayList, numProcesses As Integer)
        Dim i As Integer

        Me.id = id
        Me.userName = "P" & id
        Me.vectorClock = New ArrayList

        For i = 0 To numProcesses
            Me.vectorClock.Add(0)
        Next

        userThread = New Thread(AddressOf MessageTask)
        userThread.IsBackground = False
        userThread.Start()
    End Sub
    Sub MessageTask()
        chatForm = New P2PForm(userName, messageQueue, vectorClock)
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

End Class

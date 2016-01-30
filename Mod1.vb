Imports System.Threading

Module Mod1

    Private numProcesses As Integer
    Private processes() As UserProcess
    Public messageQueue As New ArrayList

    Sub Main()
        Dim i As Integer
        Console.WriteLine("Welcome to P2P chatting in VB!")
        Console.WriteLine("Please select the number of processes to run:")
        numProcesses = Console.ReadLine()
        numProcesses = numProcesses - 1
        Console.WriteLine("Generating...")
        Console.WriteLine("Process IDs are as follows:")
        ReDim processes(numProcesses)

        For i = 0 To numProcesses
            processes(i) = New UserProcess("P" & i, messageQueue)
            Console.WriteLine(processes(i).User)
        Next
    End Sub

End Module

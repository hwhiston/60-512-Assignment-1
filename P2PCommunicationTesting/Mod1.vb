Imports System.Threading

Module Mod1

    Private numProcesses As String
    Private processes() As UserProcess
    Public messageQueue As New ArrayList
    Public vectorClock As New ArrayList
    Private randomNum As Random

    Sub Main()
        Dim i As Integer
        Dim n As Integer
        Dim r As Integer

        Console.WriteLine("Welcome to P2P chatting in VB!")
        Console.WriteLine("Please select the number (greater than 0) of processes to run:")
        numProcesses = Console.ReadLine()
        While (Not Integer.TryParse(numProcesses, n) Or numProcesses <= 0)
            Console.WriteLine("Invalid input. Please select the number (greater than 0) of processes to run:")
            numProcesses = Console.ReadLine()
        End While
        numProcesses = numProcesses - 1
        Console.WriteLine("Generating...")
        Console.WriteLine("Process IDs are as follows:")
        ReDim processes(numProcesses)
        randomNum = New Random

        For i = 0 To numProcesses
            vectorClock.Add(0)
            r = randomNum.Next(0, 10000)
            processes(i) = New UserProcess(i, messageQueue, vectorClock, r)
            Console.WriteLine(processes(i).User)
        Next
    End Sub

End Module

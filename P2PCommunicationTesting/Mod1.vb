Imports System.Threading

Module Mod1

    ''' <summary>
    ''' Title: P2P Communication Testing
    ''' Summary: A simulation of P2P communication using causal ordering
    ''' Assumptions: Reliable network communication, no fault tolerance, no join/leave
    ''' Submitted by: Halen Whiston, Shalini Bhattacharjee
    ''' Course: 60-512
    ''' Semester: Winter 2016
    ''' Due: February 28
    ''' </summary>

    Private numProcesses As String          'number of processes to generate (0 < n < 10)
    Private processes() As UserProcess      'array of processes to generate
    Public vectorClock As New ArrayList     'array of vectorclocks for the processes
    Private randomNum As Random             'number used to determine random sleep timer for listeners

    ''' <summary>
    ''' Main entry point of program
    ''' </summary>
    Sub Main()
        Dim i As Integer
        Dim n As Integer
        Dim r As Integer

        'Get user input on number of processes to generate
        Console.WriteLine("Welcome to P2P chatting in VB!")
        Console.WriteLine("Please select the number (0 < n < 10) of processes to run:")
        numProcesses = Console.ReadLine()
        While (Not Integer.TryParse(numProcesses, n) Or numProcesses <= 0 Or numProcesses >= 10)
            Console.WriteLine("Invalid input. Please select the number (0 < n < 10) of processes to run:")
            numProcesses = Console.ReadLine()
        End While
        numProcesses = numProcesses - 1
        Console.WriteLine("Generating...")
        Console.WriteLine("Process IDs are as follows:")
        ReDim processes(numProcesses)
        randomNum = New Random

        'Generate processes, assign ids and vectorclocks
        For i = 0 To numProcesses
            vectorClock.Add(0)
            r = randomNum.Next(0, 10000)
            processes(i) = New UserProcess(i, processes, vectorClock, r)
            Console.WriteLine(processes(i).User)
        Next
    End Sub

End Module

Public Class Dark
    Private Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As Integer, ByVal hwndCallback As Integer) As Integer

    Dim filename As String
    Dim retVal As Integer
    Dim File As String
    Dim PPState As String = "Pause"
    Dim TmrNum As Integer = 1000
    Private Sub Dark_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form1.Hide()
        retVal = mciSendString("setaudio movie volume to " & TrackBar1.Value, 0, 0, 0) 'Sets the volume to the trackbar
        filename = Chr(34) & filename & Chr(34) 'quotes around the file name
        retVal = mciSendString("open " & filename & " type mpegvideo alias movie", 0, 0, 0) 'opens the file
        retVal = mciSendString("close movie", 0, 0, 0) 'closes the movie
        retVal = mciSendString("open " & filename & " type mpegvideo alias movie parent " & PictureBox1.Handle.ToInt32 & " style child", 0, 0, 0) 'sets it to play in the movie window...
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button3.Text = "Choose File"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then 'If the OpenFileDialog is OK then it will do the following statements
            filename = OpenFileDialog1.FileName.ToString 'Sets the filename
            filename = Chr(34) & filename & Chr(34) 'Quotes around
            retVal = mciSendString("open " & filename & " type mpegvideo alias movie", 0, 0, 0) 'opens it...
            'Re-enables the buttons, so you cant play nothing..
            retVal = mciSendString("close movie", 0, 0, 0) 'closes it
            retVal = mciSendString("open " & filename & " type mpegvideo alias movie parent " & PictureBox1.Handle.ToInt32 & " style child", 0, 0, 0) 'opens it to the movie window
            File = OpenFileDialog1.FileName 'Sets the file
            retVal = mciSendString("setaudio movie volume to " & TrackBar1.Value, 0, 0, 0) 'gets volume
            Label1.Text = "File:" & OpenFileDialog1.FileName
            Button1.PerformClick()
            Me.Text = "nuggPlayer - " & OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Play" Then
            Button1.Text = "Pause"
            Dim leftpos, Top, Width, Height As Integer 'Declareations for the size and pos.
            PPState = "Play" 'gets the state
            Width = PictureBox1.Size.Width 'changes the width
            Height = PictureBox1.Size.Height 'changes the height...
            retVal = mciSendString("resume movie", 0, 0, 0) 'i chose resume instead of play because it will play when paused...
            retVal = mciSendString("put movie window at " & leftpos & " " & Top & " " & Width & " " & Height, 0, 0, 0) 'alignes the movie window...
            Top = 0 'more aligning
            leftpos = 0 'more...
        ElseIf Button1.Text = "Pause" Then
            Button1.Text = "Play"
            retVal = mciSendString("pause movie", 0, 0, 0) 'Pauses the movie...
            PPState = "Pause" 'Changes the state...
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button1.Text = "Play"
        Dim newPosition As Integer = 0 'declares the position so that it will start from the beinging
        retVal = mciSendString("stop movie", 0, 0, 0) 'stops it
        retVal = mciSendString("set movie time format ms", 0, 0, 0) 'changes format to milliseconds
        retVal = mciSendString("play movie from " & newPosition, 0, 0, 0) 'sets movie to newPosition
        retVal = mciSendString("stop movie", 0, 0, 0) 'Stops it.e=
        PPState = "Stopped"
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        retVal = mciSendString("setaudio movie volume to " & TrackBar1.Value, 0, 0, 0)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MessageBox.Show("By continuing, you agree to download ffmpeg. It may take up to 3 minutes to download, and once it's done, the converter will open.")
        converter.Show()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
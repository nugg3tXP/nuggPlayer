Public Class converter
    Dim InputVideo As String = ""
    Dim WithEvents proc As New Process

    Private Sub converter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        Button1.Text = "Open Video"
        Button2.Text = "Convert"
        Button3.Text = "Stop"
        Button2.Enabled = False
        Button3.Enabled = False
        proc.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\ffmpeg.exe"
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        proc.StartInfo.RedirectStandardInput = True
        proc.EnableRaisingEvents = True


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ofd As New OpenFileDialog
        ofd.Title = "Open Video"
        ofd.Multiselect = False
        ofd.Filter = "Video Files|*.mp4;"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            InputVideo = ofd.FileName
            Button2.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim sfd As New SaveFileDialog
        sfd.Title = "Save Video As..."
        sfd.FileName = "Untitled"
        sfd.DefaultExt = "avi"
        sfd.AddExtension = True
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = True

            Dim saveas As String = Chr(34) & sfd.FileName & Chr(34)
            InputVideo = "-i " & Chr(34) & InputVideo & Chr(34)
            Dim VidBitRate As String = " -b:v 100k"
            Dim VidFrameRate As String = " -r 60"
            Dim AudBitRate As String = " -b:a 128k"
            Dim AudSampleRate As String = " -ar 44100"

            proc.StartInfo.Arguments = InputVideo & " -c:v mpeg4" & VidBitRate & VidFrameRate & " -g 300 -bf 2 -c:a libmp3lame" & AudBitRate & AudSampleRate & " -y " & saveas
            proc.Start()
        End If
    End Sub

    Private Sub proc_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles proc.Exited
        MessageBox.Show("Finished converting video.")
        ResetButtons()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        proc.StandardInput.WriteLine("q")
        ResetButtons()
    End Sub

    Private Delegate Sub ResetButtonsDel()
    Private Sub ResetButtons()
        If Me.InvokeRequired Then
            Dim D As New ResetButtonsDel(AddressOf ResetButtons)
            Me.Invoke(D)
            Exit Sub
        End If
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = False
    End Sub
End Class
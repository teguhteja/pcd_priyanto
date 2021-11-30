<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frMiniInstagram
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageEffectsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EmbossToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageSmoothingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageSharpeningToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InkWellToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BlackAndWhiteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EdgeOnlyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.picSource = New System.Windows.Forms.PictureBox()
        Me.picResult = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.picSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ImageEffectsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(884, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.OpenToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(149, 6)
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.OpenToolStripMenuItem.Text = "&Open"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save &As"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'ImageEffectsToolStripMenuItem
        '
        Me.ImageEffectsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EmbossToolStripMenuItem, Me.ImageSmoothingToolStripMenuItem, Me.ImageSharpeningToolStripMenuItem, Me.InkWellToolStripMenuItem, Me.BlackAndWhiteToolStripMenuItem, Me.EdgeOnlyToolStripMenuItem})
        Me.ImageEffectsToolStripMenuItem.Name = "ImageEffectsToolStripMenuItem"
        Me.ImageEffectsToolStripMenuItem.Size = New System.Drawing.Size(90, 20)
        Me.ImageEffectsToolStripMenuItem.Text = "&Image Effects"
        '
        'EmbossToolStripMenuItem
        '
        Me.EmbossToolStripMenuItem.Name = "EmbossToolStripMenuItem"
        Me.EmbossToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.EmbossToolStripMenuItem.Text = "Em&boss"
        '
        'ImageSmoothingToolStripMenuItem
        '
        Me.ImageSmoothingToolStripMenuItem.Name = "ImageSmoothingToolStripMenuItem"
        Me.ImageSmoothingToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ImageSmoothingToolStripMenuItem.Text = "Image S&moothing"
        '
        'ImageSharpeningToolStripMenuItem
        '
        Me.ImageSharpeningToolStripMenuItem.Name = "ImageSharpeningToolStripMenuItem"
        Me.ImageSharpeningToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ImageSharpeningToolStripMenuItem.Text = "Image S&harpening"
        '
        'InkWellToolStripMenuItem
        '
        Me.InkWellToolStripMenuItem.Name = "InkWellToolStripMenuItem"
        Me.InkWellToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.InkWellToolStripMenuItem.Text = "Ink&Well"
        '
        'BlackAndWhiteToolStripMenuItem
        '
        Me.BlackAndWhiteToolStripMenuItem.Name = "BlackAndWhiteToolStripMenuItem"
        Me.BlackAndWhiteToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.BlackAndWhiteToolStripMenuItem.Text = "Blac&k and White"
        '
        'EdgeOnlyToolStripMenuItem
        '
        Me.EdgeOnlyToolStripMenuItem.Name = "EdgeOnlyToolStripMenuItem"
        Me.EdgeOnlyToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.EdgeOnlyToolStripMenuItem.Text = "&Edge Only"
        '
        'picSource
        '
        Me.picSource.Location = New System.Drawing.Point(17, 38)
        Me.picSource.Name = "picSource"
        Me.picSource.Size = New System.Drawing.Size(400, 400)
        Me.picSource.TabIndex = 1
        Me.picSource.TabStop = False
        '
        'picResult
        '
        Me.picResult.Location = New System.Drawing.Point(466, 38)
        Me.picResult.Name = "picResult"
        Me.picResult.Size = New System.Drawing.Size(400, 400)
        Me.picResult.TabIndex = 2
        Me.picResult.TabStop = False
        '
        'frMiniInstagram
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 462)
        Me.Controls.Add(Me.picResult)
        Me.Controls.Add(Me.picSource)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frMiniInstagram"
        Me.Text = "miniInstagram"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.picSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImageEffectsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmbossToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImageSmoothingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImageSharpeningToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InkWellToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BlackAndWhiteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EdgeOnlyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents picSource As System.Windows.Forms.PictureBox
    Friend WithEvents picResult As System.Windows.Forms.PictureBox

End Class

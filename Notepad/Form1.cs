/**
 * CREDITS *
 * Developer - George Ongoro
 * Designer - George Ongoro
 * Contributors - Wesley Weiss, Feedback
 *                Victor Andole, Feedback
 *                
 *                $$Contributors have no special rights in this work.
 *                
 * Copyright(c) 2024, SoftLeap Technologies. 
 * Privacy Policies - https://www.softleaptech.netlify.app/privacy-policies
 * Licence agreement - https://www.softleaptech.netlify.app/licences
 * **///Credits



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();

            charsLabel.Text = $"|       Characters: {CharsCount}       |";
            int zoomFactor = 1;
            back.ZoomFactor = zoomFactor;
            labelZoom.Text = $"    Zoom: {(zoomFactor / 64) * 100}%    |";
        }


        string currentFilePath = null;
        string TitleName;
        private bool textSaved = false;
        int CharsCount = 0;
        object currentFont;
        bool isRtoL;



        private void saveText(string filePath)
        {
            try
            {
                File.WriteAllText(filePath, back.Text);
                currentFilePath = filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void GetTypingSpeed(int speedValue)
        {
            int typingSpeed = speedValue;
            labelTypingSpeed.Text = $"|       Typing Speed: {typingSpeed}%         |";
        }

        private void DisableControls()
        {
            if(back.Text == "")
            {
                saveAsToolStripMenuItem.Enabled = false;
                saveToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                printToolStripMenuItem.Enabled = false;
                undoToolStripMenuItem.Enabled = false;
                findNextToolStripMenuItem.Enabled = false;
                findToolStripMenuItem.Enabled = false;
                selectAllToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem1.Enabled = false;
                cutToolStripMenuItem1.Enabled = false;
                undoToolStripMenuItem1.Enabled = false;
                selectAllToolStripMenuItem1.Enabled = false;

            }
            else
            {
                saveAsToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                printToolStripMenuItem.Enabled = true;
                undoToolStripMenuItem.Enabled = true;
                findNextToolStripMenuItem.Enabled = true;
                findToolStripMenuItem.Enabled = true;
                selectAllToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem1.Enabled = true;
                cutToolStripMenuItem1.Enabled = true;
                undoToolStripMenuItem1.Enabled = true;
                selectAllToolStripMenuItem1.Enabled = true;
            }
        }

            private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt) | *.txt | All Files (*.*) | *.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                saveText(saveFileDialog.FileName);
                textSaved = true;
            }

            Text = saveFileDialog.FileName.Trim() + " - GelNotes";
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            back.Clear();
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            main newWindow = new main();
            newWindow.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Open";
            fileDialog.Filter = "Text Document(*.txt) | *.txt | All Files(*.*) | *.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                back.LoadFile(fileDialog.FileName, RichTextBoxStreamType.PlainText);
            }

            if (textSaved)
            {


                TitleName = $"{fileDialog.SafeFileName} - GelNotes";
                Text = TitleName;
            }
            else
            {
                TitleName = $"*{fileDialog.SafeFileName} - GelNotes";
                Text = TitleName;
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(currentFilePath != null)
            {
                saveText(currentFilePath);
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            back.Text = "Time: " + DateTime.Now.ToShortTimeString() + ", \n Date: " +DateTime.Now.ToShortDateString();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            back.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            back.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            back.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            back.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            back.Clear();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFindReplace findReplace = new FormFindReplace();
            findReplace.Show();
            
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFindReplace findReplace = new FormFindReplace();
            findReplace.Show();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            back.SelectAll();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            back.WordWrap = true;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            if(font.ShowDialog() == DialogResult.OK)
            {
                back.Font = font.Font;
            }
            
        }

        private void colourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colour = new ColorDialog();
            if(colour.ShowDialog() == DialogResult.OK)
            {
                back.ForeColor = colour.Color;
            }
            
        }

        

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog print = new PrintDialog();
            if(print.ShowDialog() == DialogResult.OK )
            {
                
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        private void aboutGsNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpPage help = new HelpPage();
            help.Show();
            WindowState = FormWindowState.Minimized;
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            back.Paste();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            back.Copy();
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatusBar statusBar = new StatusBar();
            statusBar.Show();
        }

        private void back_TextChanged(object sender, EventArgs e)
        {
            ////  CAUTION \\\\ --> Counting characters as below slows down the app.
            //int i;
            //for(i = 0; i <= back.Text.Length; i++)
            //{
            //    charsLabel.Text = $"|       Characters: {i.ToString()}       |";
            //    GetTypingSpeed(i);
           // }
            DisableControls();

        }

        private void statusBarToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (statusBarToolStripMenuItem.Checked)
            {
                statusStrip1.Show();
            }
            else
            {
                statusStrip1.Hide();
            }
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int zoomFactor = 2;

            if (zoomFactor > 1 && zoomFactor < 64)
            {
                back.ZoomFactor = zoomFactor - 1;
                labelZoom.Text = $"    Zoom: {(zoomFactor / 64) * 100}%    |";
            }
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int zoomFactor = 1;

            if (zoomFactor > 1 && zoomFactor < 64)
            {
                back.ZoomFactor = zoomFactor + 1;
                labelZoom.Text = $"    Zoom: {(zoomFactor / 64) * 100}%    |";
            }
        }

        private void defaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int zoomFactor = 1;
            back.ZoomFactor = zoomFactor;
            labelZoom.Text = $"|    Zoom: {(zoomFactor / 64) * 100}%    |";
        }

        private void back_FontChanged(object sender, EventArgs e)
        {
            currentFont = back.Font;
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            back.Cut();
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            back.Undo();
        }

        private void rightToLeftReadingOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (isRtoL)
            {
                back.RightToLeft = RightToLeft.No;
                isRtoL = false;
                rightToLeftReadingOrderToolStripMenuItem.Text = "Right to left reading order";
            }
            else
            {
                back.RightToLeft = RightToLeft.Yes;
                isRtoL = true;
                rightToLeftReadingOrderToolStripMenuItem.Text = "Left to right reading order";
            }
            
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            back.SelectAll();
        }

        private void wordWrapToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            back.WordWrap = true;
        }

        private void back_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void main_Load(object sender, EventArgs e)
        {
            DisableControls();
        }
    }
}




using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{

    /// <summary>
    /// Find and replace functionality
    /// </summary>
    public partial class FormFindReplace : Form
    {
        public FormFindReplace()
        {
            InitializeComponent();
        }

        public string FindText;
        public string ReplaceText;

        public void Find(string text)
        {
            main _main = new main(); 

            foreach(var word in _main.back.Text)
            {
                textBoxReplace.Text = word.ToString();
            }

           
        }

        private void textBoxFind_TextChanged(object sender, EventArgs e)
        {
            FindText = textBoxFind.Text;
            Find(FindText);
        }
    }
}

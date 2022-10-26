using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Notepad
{
    public partial class Form1 : Form
    {
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        static private FontDialog fontDialog;
        private List<TabPage> pages = new List<TabPage>();
        public Form1()
        {
            InitializeComponent();
            NewTab();
            NewTab();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }
        private void NewFile()
        {
            try
            {
                if (!string.IsNullOrEmpty(GetRichTextBox().Text))
                {
                    MessageBox.Show("You need to save first!");
                }
                else
                {
                    GetRichTextBox().Text = string.Empty;
                    this.Text = "Untitled";
                }
            }
            catch (Exception)
            {

            }

        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        private void OpenFile()
        {
            try
            {
                openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    GetRichTextBox().Text = File.ReadAllText(openFileDialog.FileName);
                    tabControl1.SelectedTab.Controls[0].Text = openFileDialog.Filter;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error while trying to open file!");
            }
            finally
            {
                openFileDialog = null;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        private void SaveFile()
        {
            try
            {
                if (!string.IsNullOrEmpty(GetRichTextBox().Text))
                {
                    saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text File (*.txt) | *.txt";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog.FileName, GetRichTextBox().Text);
                        this.Text = saveFileDialog.FileName;
                    }
                }
                else
                {
                    MessageBox.Show("There's no text");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There's an error in saving file");
            }
        }
        private void SaveFileAs()
        {
            try
            {
                if (!string.IsNullOrEmpty(GetRichTextBox().Text))
                {
                    saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "RichTextFormate | *.rtf |Text Files | *.txt |All Files| *.*";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog.FileName, GetRichTextBox().Text);
                        this.Text = saveFileDialog.FileName;
                    }
                }
                else
                {
                    MessageBox.Show("There's no text");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There's an error in saving file");
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(GetRichTextBox().Text))
                {
                    SaveFile();
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There's an error in exiting program");
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                fontDialog = new FontDialog();
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    GetRichTextBox().SelectionFont = fontDialog.Font;
                }
            }
            catch (Exception)
            {

            }
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private RichTextBox GetRichTextBox()
        {
            return tabControl1.SelectedTab.Controls[0] as RichTextBox;
        }
        private void NewTab()
        {
            TabPage tabPage = new TabPage("Untitled");
            RichTextBox richTextBox = new RichTextBox();
            // Добавление вкладки в массив
            pages.Add(tabPage);
            // Растяжение области текста на всю площадь.
            richTextBox.Dock = DockStyle.Fill;
            // Соедиенение вкладки и области текста.
            tabPage.Controls.Add(richTextBox);
            // Добавление вкладки на окно.
            tabControl1.TabPages.Add(tabPage);
        }
    }
}

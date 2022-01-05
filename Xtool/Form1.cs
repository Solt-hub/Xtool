using System.Diagnostics;
using System.Xml;
namespace Xtool
{
    public partial class Form1 : Form
    {
        XmlDocument xml;
        XmlDocument xmlDoc;
        XmlNode node;
        XmlNode exename;
        XmlNode lpath;
        XmlNode XCSS;
        string EXE;
        string Lpath;
        string XCSSpath;
        Dictionary<string, MessageBoxButtons> mapMSGButton;
        Dictionary<string, MessageBoxIcon> mapMSGIcon;
        
        public struct StyleConrtol
        {
            public Font Font;
            public float FontSize;
            public StyleConrtol(Font font,float fsize)
            {
                Font = font;
                FontSize = fsize;
            }
        }
        public StyleConrtol[] stc;
        struct Ctl
        {
            public Control control;
            public int id;
            public Type type;
            public Ctl(Control con, int v, Type t)
            {
                id = v;
                control = con;
                type = t;
            }
        };
        List<Ctl> Ctls;

        public Form1()
        {
            InitializeComponent();
        }

        public void readXCSS(string path)
        {
            stc = new StyleConrtol[1024];
            xmlDoc = new XmlDocument();
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.IgnoreWhitespace = true;
            xmlReaderSettings.IgnoreComments = true;
            XmlReader xmlReader = XmlReader.Create(path, xmlReaderSettings);
            xmlDoc.Load(xmlReader);
            foreach(XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes)
            {
                stc[Convert.ToInt32(xmlNode.Attributes["StyleId"].Value)] = new StyleConrtol(new Font(xmlNode.Attributes["Font"].Value, Convert.ToSingle(xmlNode.Attributes["Fsize"].Value)), Convert.ToSingle(xmlNode.Attributes["Fsize"].Value));
            }

        }
        public void rereadxml(string path)
        {
            Ctls = new List<Ctl>();
            Ctls.Clear();
            
            foreach (Control control in this.Controls)
            {
                control.Hide();
            }
            xml = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(path, settings);
            xml.Load(reader);
            node = xml.SelectSingleNode("Xtool/Control");
            exename = xml.SelectSingleNode("Xtool/EXEname");
            lpath = xml.SelectSingleNode("Xtool/Lpath");
            XCSS = xml.SelectSingleNode("Xtool/DoeshaveXCSS");
            if(XCSS.Attributes["Does"].Value == "True")
            {
                XCSSpath = XCSS.InnerText;
                readXCSS(XCSSpath);
            }
            Lpath = lpath.InnerText;
            fileSystemWatcher1.Filter = "*" + lpath.Attributes["Filter"].Value;
            fileSystemWatcher1.Path = Lpath;
            EXE = exename.InnerText;
            if (node != null)
            {
                while (node != null)
                {
                    if (node.Name == "Control")
                    {
                        XmlAttributeCollection xmlAttributeCollection = node.Attributes;
                        switch (xmlAttributeCollection["Type"].Value)
                        {
                            case "Button":
                                {
                                    Button button = new Button();
                                    button.Parent = this;
                                    button.Text = node.InnerText;
                                    button.Location = new Point(Convert.ToInt32(xmlAttributeCollection["X"].Value), Convert.ToInt32(xmlAttributeCollection["Y"].Value));
                                    button.Width = Convert.ToInt32(xmlAttributeCollection["Width"].Value);
                                    button.Height = Convert.ToInt32(xmlAttributeCollection["Height"].Value);
                                    if(Convert.ToInt32(xmlAttributeCollection["StyleId"].Value) != -1)
                                    {
                                        button.Font = stc[Convert.ToInt32(xmlAttributeCollection["StyleId"].Value)].Font;
                                    }
                                    else
                                    {
                                        button.Font = new Font(xmlAttributeCollection["Font"].Value, (float)Convert.ToDouble(xmlAttributeCollection["Emsize"].Value));
                                    }
                                    button.Show();
                                    Ctls.Add(new Ctl(button, Convert.ToInt32(xmlAttributeCollection["Id"].Value), button.GetType()));
                                    button.Click += Xtool_click;
                                    break;
                                }
                            case "Lable":
                                {
                                    Label label = new Label();
                                    label.Parent = this;
                                    label.Text = node.InnerText;
                                    label.Location = new Point(Convert.ToInt32(xmlAttributeCollection["X"].Value), Convert.ToInt32(xmlAttributeCollection["Y"].Value));
                                    label.Width = Convert.ToInt32(xmlAttributeCollection["Width"].Value);
                                    label.Height = Convert.ToInt32(xmlAttributeCollection["Height"].Value);
                                    if (Convert.ToInt32(xmlAttributeCollection["StyleId"].Value) != -1)
                                    {
                                        label.Font = stc[Convert.ToInt32(xmlAttributeCollection["StyleId"].Value)].Font;
                                    }
                                    else
                                    {
                                        label.Font = new Font(xmlAttributeCollection["Font"].Value, (float)Convert.ToDouble(xmlAttributeCollection["Emsize"].Value));
                                    }
                                    label.Show();
                                    Ctls.Add(new Ctl(label, Convert.ToInt32(xmlAttributeCollection["Id"].Value), label.GetType()));
                                    label.Click += Xtool_click;
                                    break;
                                }
                            case "TextBox":
                                {
                                    TextBox textBox = new TextBox();
                                    textBox.Parent = this;
                                    textBox.Text = node.InnerText;
                                    textBox.Location = new Point(Convert.ToInt32(xmlAttributeCollection["X"].Value), Convert.ToInt32(xmlAttributeCollection["Y"].Value));
                                    textBox.Width = Convert.ToInt32(xmlAttributeCollection["Width"].Value);
                                    textBox.Height = Convert.ToInt32(xmlAttributeCollection["Height"].Value);
                                    textBox.Font = new Font(xmlAttributeCollection["Font"].Value, (float)Convert.ToDouble(xmlAttributeCollection["Emsize"].Value));
                                    if (Convert.ToInt32(xmlAttributeCollection["StyleId"].Value) != -1)
                                    {
                                        textBox.Font = stc[Convert.ToInt32(xmlAttributeCollection["StyleId"].Value)].Font;
                                    }
                                    else
                                    {
                                        textBox.Font = new Font(xmlAttributeCollection["Font"].Value, (float)Convert.ToDouble(xmlAttributeCollection["Emsize"].Value));
                                    }
                                    textBox.Multiline = xmlAttributeCollection["Ismutiline"].Value == "true" ? true : false;
                                    textBox.Show();
                                    Ctls.Add(new Ctl(textBox, Convert.ToInt32(xmlAttributeCollection["Id"].Value), textBox.GetType()));
                                    textBox.TextChanged += Xtool_click;
                                    break;
                                }
                            case "MSGBox":
                                {
                                    MessageBox.Show(node.InnerText, node.Attributes["Title"].Value, mapMSGButton[xmlAttributeCollection["MSGButton"].Value], mapMSGIcon[xmlAttributeCollection["MSGIcon"].Value]);
                                    break;
                                }
                        }
                    }
                    if(node.NextSibling != null)
                    {
                        node = node.NextSibling;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Xtool_click(object sender, EventArgs e)
        {
            foreach (Ctl i in Ctls)
            {
                if (i.control == (Control)sender)
                {
                    Process.Start(EXE, i.id.ToString() + " " + i.type.ToString());
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XTool插件|*.xml|所有文件|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(ofd.FileName))
                {
                    rereadxml(ofd.FileName);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mapMSGIcon = new Dictionary<string, MessageBoxIcon>();
            mapMSGButton = new Dictionary<string, MessageBoxButtons>();
            mapMSGButton.Add("OK", MessageBoxButtons.OK);
            mapMSGButton.Add("OKCancel", MessageBoxButtons.OKCancel);
            mapMSGButton.Add("YesNo", MessageBoxButtons.YesNo);
            mapMSGButton.Add("YesNoCancel", MessageBoxButtons.YesNoCancel);
            mapMSGButton.Add("CancelTryContinue", MessageBoxButtons.CancelTryContinue);
            mapMSGButton.Add("AbortRetryIgnore", MessageBoxButtons.AbortRetryIgnore);
            mapMSGButton.Add("RetryCancel", MessageBoxButtons.RetryCancel);
            mapMSGIcon.Add("Information", MessageBoxIcon.Information);
            mapMSGIcon.Add("Warning", MessageBoxIcon.Warning);
            mapMSGIcon.Add("Question", MessageBoxIcon.Question);
            mapMSGIcon.Add("Hand", MessageBoxIcon.Hand);
            mapMSGIcon.Add("Stop", MessageBoxIcon.Stop);
            mapMSGIcon.Add("Asterisk", MessageBoxIcon.Asterisk);
            mapMSGIcon.Add("Error",MessageBoxIcon.Error);
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            rereadxml(e.FullPath);
        }
    }
}
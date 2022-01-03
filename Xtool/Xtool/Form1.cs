using System.Diagnostics;
using System.Xml;
namespace Xtool
{
    public partial class Form1 : Form
    {
        XmlDocument xml;
        XmlNode node;
        XmlNode exename;
        XmlNode lpath;
        string EXE;
        string Lpath;
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
                                    if(xmlAttributeCollection["Font"].Value != null && xmlAttributeCollection["Emsize"].Value != null)
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
                                    label.Font = new Font(xmlAttributeCollection["Font"].Value, (float)Convert.ToDouble(xmlAttributeCollection["Emsize"].Value));
                                    label.Show();
                                    Ctls.Add(new Ctl(label, Convert.ToInt32(xmlAttributeCollection["Id"].Value), label.GetType()));
                                    label.Click += Xtool_click;
                                    break;
                                }
                            case "MSGBox":
                                {
                                    MessageBox.Show(node.InnerText, node.Attributes["Title"].Value);
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

        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            rereadxml(e.FullPath);
        }
    }
}
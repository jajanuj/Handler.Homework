using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ArtCommonLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;

namespace ArtSystem
{
    public partial class ucClassDataViewer : TreeView
    {
        #region //========== 變數設置 ==========

        private Size initialSize = new Size();
        public object g_NowObject = null;
        private string sSelectingNode = "";
        private TreeNode pSelectingNode = null;
        public bool g_bListItemShowToString = false;
        #endregion

        #region //========== Private Class (測試用) ==========
        private PropertyGrid m_PropertyGrid = new PropertyGrid();
        private class CustomTypeDescriptor<T> : CustomTypeDescriptor
        {
            private readonly T _component;

            public CustomTypeDescriptor(ICustomTypeDescriptor parent, T component)
                : base(parent)
            {
                _component = component;
            }

            public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
            {
                PropertyDescriptorCollection properties = base.GetProperties(attributes);

                // 使用反射获取公共字段
                FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);

                PropertyDescriptor[] descriptors = new PropertyDescriptor[fields.Length + properties.Count];

                // 复制现有的属性描述符
                properties.CopyTo(descriptors, 0);

                // 创建新的字段描述符
                for (int i = 0; i < fields.Length; i++)
                {
                    descriptors[properties.Count + i] = new FieldPropertyDescriptor(fields[i]);
                }

                return new PropertyDescriptorCollection(descriptors);
            }
        }
        private class FieldPropertyDescriptor : PropertyDescriptor
        {
            private readonly FieldInfo _field;

            public FieldPropertyDescriptor(FieldInfo field)
                : base(field.Name, null)
            {
                _field = field;
            }

            public override object GetValue(object component)
            {
                return _field.GetValue(component);
            }

            public override void SetValue(object component, object value)
            {
                _field.SetValue(component, value);
            }

            public override bool CanResetValue(object component) { return false; }

            public override Type ComponentType { get { return _field.DeclaringType; } }

            public override bool IsReadOnly { get { return false; } }

            public override Type PropertyType { get { return _field.FieldType; } }

            public override void ResetValue(object component) { }

            public override bool ShouldSerializeValue(object component) { return true; }
        }
        #endregion

        #region //========== 必要函式設置 ==========

        static private object objLock = new object();
        static private ucClassDataViewer m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucClassDataViewer GetSingleton()
        {
            lock (objLock)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucClassDataViewer();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucClassDataViewer()
        {
            InitializeComponent();
            this.initialSize = this.Size;
            //this.m_PropertyGrid.Parent = this;
            //this.m_PropertyGrid.Dock = DockStyle.Fill;
            this.HideSelection = false;
            this.BeforeExpand += new TreeViewCancelEventHandler(ucClassDataViewer_BeforeExpand);
            this.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.DrawNode += treeView1_DrawNode;
            this.HandleCreated += new EventHandler(ucClassDataViewer_HandleCreated);
        }


        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //========== ShowForm 函式設置 ==========
        private Form mForm = null;

        private TreeNode p_AddTreeNode = null;
        private void ucClassDataViewer_HandleCreated(object sender, EventArgs e)
        {
            if (p_AddTreeNode != null)
            {
                this.Nodes.Clear();
                this.BeginInvoke((Action)(() =>
                {
                    this.Nodes.Add(p_AddTreeNode);
                    if (this.Nodes.Count > 0)
                    {
                        this.Nodes[0].Expand();
                    }
                    p_AddTreeNode = null;
                }));
            }
        }
        /// <summary> 使用Form顯示 </summary>
        public void _ShowForm<T>(T p_Data, string p_sItemName, string p_sItemText = "", bool Dialog = true)
        {
            try
            {
                if (mForm == null)
                {
                    this.DrawMode = TreeViewDrawMode.Normal;
                    mForm = new Form();
                    mForm.Load += new EventHandler(mForm_Load);
                    mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
                    //mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
                }
                this.DrawMode = TreeViewDrawMode.Normal;
                this.DrawNode -= treeView1_DrawNode;
                this.BeforeExpand -= new TreeViewCancelEventHandler(ucClassDataViewer_BeforeExpand);
                mForm.WindowState = FormWindowState.Normal;
                mForm.Size = new Size(this.initialSize.Width + 16, this.initialSize.Height + 39);
                mForm.StartPosition = FormStartPosition.CenterScreen;
                mForm.Text = clsLanguage.GetTranslation(this.Name, false);
                this.Parent = mForm;
                this.g_NowObject = p_Data;
                this.Private_UpdateDataToTreeView(p_Data, p_sItemName, p_sItemText);

                this.Dock = DockStyle.Fill;
                //this.DrawNode += treeView1_DrawNode;
                if (Dialog == true)
                {
                    mForm.ShowDialog();
                }
                else
                {
                    mForm.Show();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void mForm_Load(object sender, EventArgs e)
        {
            //this.DrawNode += treeView1_DrawNode;
            this.BeforeExpand += new TreeViewCancelEventHandler(ucClassDataViewer_BeforeExpand);
        }
        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Form mForm = (Form)sender;
                mForm.Visible = false;
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }

        }
        private void mForm_Deactivate(object sender, EventArgs e)
        {
            //this.SetReflashTimerStart(false);
            //this.Parent = null;
            //Form mForm = (Form)sender;
            mForm.Close();
        }

        #endregion

        #region//========== Public 函式 ==========

        public void UpdateDataToTreeView<T>(T p_Data, string p_sItemName, string p_sItemText = "")
        {
            try
            {
                if (this.InvokeRequired == true)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        this.g_NowObject = p_Data;
                        this.pSelectingNode = null;
                        Private_UpdateDataToTreeView(p_Data, p_sItemName, p_sItemText);
                    }));
                }
                else
                {
                    this.g_NowObject = p_Data;
                    this.pSelectingNode = null;
                    Private_UpdateDataToTreeView(p_Data, p_sItemName, p_sItemText);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion


        #region//========== Public 函式 (取得TreeNode路徑) ==========

        public List<string> GetSelectingNodePath()
        {
            List<string> rValue = null;
            try
            {
                if (this.SelectedNode != null)
                {
                    rValue = sItemPath(this.SelectedNode);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            if (rValue == null)
            {
                rValue = new List<string>();
            }
            return rValue;
        }
        private List<string> sItemPath(TreeNode p_TreeNode, List<string> p_Source = null)
        {
            try
            {
                if (p_TreeNode != null)
                {
                    if (p_Source == null)
                    {
                        p_Source = new List<string>();
                    }
                    if (p_TreeNode.Parent != null)
                    {
                        string sName = p_TreeNode.Text;
                        if (sName.Contains(" : ") == true)
                        {
                            sName = sName.Split(':')[0];
                            sName = sName.Replace(" ", "");
                        }
                        p_Source.Insert(0, sName);
                        sItemPath(p_TreeNode.Parent, p_Source);
                    }
                    else
                    {
                        string sName = p_TreeNode.Text;
                        if (sName.Contains(" : ") == true)
                        {
                            sName = sName.Split(':')[0];
                            sName = sName.Replace(" ", "");
                        }
                        p_Source.Insert(0, sName);
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return p_Source;
        }

        /// <summary> 取得選擇項目的完整路徑 </summary>
        public string GetSelectingNode()
        {
            string rValue = "";
            if (this.SelectedNode != null)
            {
                rValue = this.SelectedNode.Name.Split(':')[0].Replace(" ", "");
                GetSelectedNodePathName(this.SelectedNode, ref rValue);
            }
            return rValue;
        }
        /// <summary> 取得此項目的完整路徑 </summary>
        public void GetSelectedNodePathName(TreeNode p_TreeNode, ref string p_sNodePathName)
        {
            if (p_TreeNode.Parent != null)
            {
                string sParentPmtName = p_TreeNode.Parent.Name.Split(':')[0].Replace(" ", "");
                p_sNodePathName = sParentPmtName + ";" + p_sNodePathName;
                GetSelectedNodePathName(p_TreeNode.Parent, ref p_sNodePathName);
            }
        }

        #endregion
        #region//========== Public 函式 (ClassToTreeNode) ==========
        private void Private_UpdateDataToTreeView(object p_Data, string p_sItemName, string p_sItemText = "")
        {
            try
            {
                sSelectingNode = GetSelectingNode();
                pSelectingNode = null;
                TreeNode AddNode = ConvertObjToNote(p_Data, p_sItemName, p_sItemName);
                if (p_sItemText != null && p_sItemText != "")
                {
                    AddNode.Text = p_sItemText;
                }
                if (AddNode != null)
                {
                    if (this.IsHandleCreated)
                    {
                        // TreeView 清空数据
                        this.Nodes.Clear();
                        //// 将 JToken 转换为 TreeView
                        //TreeNode rootNode = ConvertJTokenToTreeNode(jsonObject, p_sItemName, true);
                        this.Nodes.Add(AddNode);

                        //展開第一層
                        if (this.Nodes.Count > 0)
                        {
                            this.Nodes[0].Expand();
                        }
                    }
                    else
                    {
                        p_AddTreeNode = AddNode;
                    }
                }
                if (pSelectingNode != null)
                {
                    this.SelectedNode = pSelectingNode;
                    pSelectingNode.EnsureVisible(); // 自動捲動到該節點
                }                
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private TreeNode ConvertObjToNote(object p_Data, string p_sItemName, string p_sFullName)
        {
            TreeNode rValue = null;
            try
            {
                if (IsValueType(p_Data))
                {
                    //ValueType
                }
                else if (p_Data.GetType().IsArray == true)
                {
                    #region//Array
                    rValue = new TreeNode(p_sItemName);
                    rValue.Name = p_sItemName;
                    rValue.Tag = p_Data;
                    rValue.Text = p_sItemName + " (Array)";
                    Array ArrayObj_Source = (Array)p_Data;
                    List<TreeNode> NotValueType = new List<TreeNode>();
                    List<string> LstIndex = new List<string>();
                    int ArrayRank_Source = ArrayObj_Source.Rank;
                    if (ArrayRank_Source > 0)
                    {
                        int[] index_Source = new int[ArrayRank_Source];
                        int[] DimensiongLength = new int[ArrayRank_Source];
                        for (int iRank = 0; iRank < ArrayRank_Source; iRank++)
                        {
                            DimensiongLength[iRank] = ArrayObj_Source.GetLength(iRank);
                        }
                        while(true)
                        {
                            string sIndex = "";
                            for (int i = 0; i < index_Source.Length; i++)
                            {
                                if (sIndex != "")
                                { sIndex += ","; }
                                sIndex += index_Source[i];
                            }
                            sIndex = "(" + sIndex + ")";
                            if (IsValueType(ArrayObj_Source.GetValue(index_Source)) == true)
                            {
                                string sName = "[" + sIndex + "]";
                                string sValue = ": (null)";
                                if (ArrayObj_Source.GetValue(index_Source) != null)
                                {
                                    sValue = ": " + ArrayObj_Source.GetValue(index_Source).ToString();
                                }
                                TreeNode ChildrenNote = new TreeNode(sName);
                                ChildrenNote.Name = sName;
                                ChildrenNote.Text = sName + sValue;
                                ChildrenNote.Tag = ArrayObj_Source.GetValue(index_Source);
                                rValue.Nodes.Add(ChildrenNote);
                                //ValueType
                            }
                            else
                            {
                                string sName = "[" + sIndex + "]";
                                TreeNode ChildrenNote = new TreeNode(sName);
                                TreeNode ChildrenItems = new TreeNode("Loading...");
                                ChildrenNote.Name = sName;
                                ChildrenNote.Nodes.Add(ChildrenItems);
                                ChildrenNote.Tag = ArrayObj_Source.GetValue(index_Source);
                                NotValueType.Add(ChildrenNote);

                            }

                            #region  // 更新索引（模擬多維計數器）
                            int dim = ArrayRank_Source - 1;
                            while (dim >= 0)
                            {
                                index_Source[dim]++;
                                if (index_Source[dim] < DimensiongLength[dim])
                                {
                                    break;
                                }
                                else
                                {
                                    index_Source[dim] = 0;
                                    dim--;
                                }
                            }

                            // 如果最高位元也溢位了，表示已完成所有元素
                            if (dim < 0)
                            {
                                break;
                            }
                            #endregion
                        }
                    }
                    rValue.Nodes.AddRange(NotValueType.ToArray());
                    #endregion
                }
                else if (p_Data is IList)
                {
                    #region//List
                    IList IDic_Source = (IList)p_Data;
                    rValue = new TreeNode(p_sItemName);
                    rValue.Name = p_sItemName;
                    rValue.Tag = p_Data;
                    rValue.Text = p_sItemName + " (List[" + IDic_Source.Count + "])";
                    List<TreeNode> NotValueType = new List<TreeNode>();
                    for (int i = 0; i < IDic_Source.Count; i++)
                    {
                        if (IsValueType(IDic_Source[i]) == true)
                        {
                            string sName = "[" + i.ToString() + "]";
                            string sValue = ": (null)";
                            if (IDic_Source[i] != null)
                            {
                                sValue = ": " + IDic_Source[i].ToString();
                            }
                            TreeNode ChildrenNote = new TreeNode(sName);
                            ChildrenNote.Name = sName;
                            ChildrenNote.Text = sName + sValue;
                            ChildrenNote.Tag = IDic_Source[i];
                            rValue.Nodes.Add(ChildrenNote);
                            //ValueType
                        }
                        else
                        {
                            string sName = "[" + i.ToString() + "]";
                            TreeNode ChildrenNote = new TreeNode(sName);
                            TreeNode ChildrenItems = new TreeNode("Loading...");
                            ChildrenNote.Name = sName;
                            if(g_bListItemShowToString == false)
                            {
                                ChildrenNote.Text = sName;
                            }
                            else
                            {
                                ChildrenNote.Text = sName + IDic_Source[i].ToString();
                            }
                            ChildrenNote.Nodes.Add(ChildrenItems);
                            ChildrenNote.Tag = IDic_Source[i];
                            NotValueType.Add(ChildrenNote);
                        }
                    }
                    rValue.Nodes.AddRange(NotValueType.ToArray());
                    #endregion
                }
                else if (p_Data is IDictionary)
                {
                    #region//Dictionary
                    IDictionary IDic_Source = (IDictionary)p_Data;
                    rValue = new TreeNode(p_sItemName);
                    rValue.Name = p_sItemName;
                    rValue.Tag = p_Data;
                    rValue.Text = p_sItemName + " (Dictionary[" + IDic_Source.Count + "])";
                    if (sSelectingNode.Contains(p_sFullName))//需要展開
                    {
                        rValue.Expand();
                        List<TreeNode> NotValueType = new List<TreeNode>();
                        foreach (DictionaryEntry entry in IDic_Source)
                        {
                            if (IsValueType(IDic_Source[entry.Key]) == true)
                            {
                                string sName = "[" + entry.Key.ToString() + "]";
                                string sValue = ": (null)";
                                if (IDic_Source[entry.Key] != null)
                                {
                                    sValue  = ": " + IDic_Source[entry.Key].ToString();
                                }
                                TreeNode ChildrenNote = new TreeNode(sName);
                                ChildrenNote.Name = sName;
                                ChildrenNote.Text = sName + sValue;
                                ChildrenNote.Tag = IDic_Source[entry.Key];
                                rValue.Nodes.Add(ChildrenNote);
                                //ValueType
                            }
                            else
                            {
                                string sName = "[" + entry.Key.ToString() + "]";
                                TreeNode ChildrenItems = ConvertObjToNote(IDic_Source[entry.Key], sName, p_sFullName + ";" + sName);
                                NotValueType.Add(ChildrenItems);
                            }
                        }
                        rValue.Nodes.AddRange(NotValueType.ToArray());
                    }
                    else if(IDic_Source.Count > 0)
                    {
                        TreeNode ChildrenItems = new TreeNode("Loading...");
                        rValue.Nodes.Add(ChildrenItems);
                    }
                    #endregion
                }
                else
                {
                    #region//Class
                    rValue = new TreeNode(p_sItemName);
                    rValue.Name = p_sItemName;
                    rValue.Tag = p_Data;
                    if (sSelectingNode.Contains(p_sFullName))//需要展開
                    {
                        rValue.Expand();
                    }
                    var LstFields = p_Data.GetType().GetFields().ToList();
                    var LstProperties = p_Data.GetType().GetProperties().ToList();
                    List<TreeNode> NotValueType = new List<TreeNode>();
                    foreach (var FieldItem in LstFields)
                    {
                        if (Attribute.IsDefined(FieldItem, typeof(JsonIgnoreAttribute)) == false
                            && Attribute.IsDefined(FieldItem, typeof(XmlIgnoreAttribute)) == false)
                        {
                            if (clsClassFunc.FieldCanWrite(FieldItem) == true)
                            {
                                string sName = FieldItem.Name;
                                var SourceValue = FieldItem.GetValue(p_Data);
                                if (IsValueType(SourceValue) == true)
                                {
                                    string sValue = ": (null)";
                                    if (SourceValue != null)
                                    {
                                        sValue = ": " + SourceValue;
                                    }
                                    TreeNode ChildrenNote = new TreeNode(sName);
                                    ChildrenNote.Name = sName;
                                    ChildrenNote.Text = sName + sValue;
                                    rValue.Nodes.Add(ChildrenNote);
                                    if (rValue != null && sSelectingNode == p_sFullName + ";" + sName)
                                    {
                                        pSelectingNode = ChildrenNote;
                                    }
                                    //ValueType
                                }
                                else
                                {
                                    TreeNode ChildrenNote = ConvertObjToNote(SourceValue, sName, p_sFullName + ";" + sName);
                                    NotValueType.Add(ChildrenNote);
                                }
                            }
                        }
                    }
                    foreach (var PropertiesItem in LstProperties)
                    {
                        if (Attribute.IsDefined(PropertiesItem, typeof(JsonIgnoreAttribute)) == false
                            && Attribute.IsDefined(PropertiesItem, typeof(XmlIgnoreAttribute)) == false)
                        {
                            MethodInfo setMethod = PropertiesItem.GetSetMethod(/* nonPublic */ true);
                            bool hasPublicSet = setMethod != null && setMethod.IsPublic;
                            if (hasPublicSet == true)
                            {
                                var SourceValue = PropertiesItem.GetValue(p_Data, null);
                                if (IsValueType(SourceValue) == true)
                                {
                                    string sName = PropertiesItem.Name;
                                    string sValue = ": (null)";
                                    if (SourceValue != null)
                                    {
                                        sValue = ": " + SourceValue;
                                    }
                                    TreeNode ChildrenNote = new TreeNode(sName);
                                    ChildrenNote.Name = sName;
                                    ChildrenNote.Text = sName + sValue;
                                    rValue.Nodes.Add(ChildrenNote);
                                    if (rValue != null && sSelectingNode == p_sFullName +";"+ sName)
                                    {
                                        pSelectingNode = ChildrenNote;
                                    }
                                    //ValueType
                                }
                                else
                                {
                                    TreeNode ChildrenNote = ConvertObjToNote(SourceValue, PropertiesItem.Name, p_sFullName + ";" + PropertiesItem.Name);
                                    NotValueType.Add(ChildrenNote);
                                }
                            }
                        }
                    }
                    rValue.Nodes.AddRange(NotValueType.ToArray());
                    #endregion
                }
                if (rValue != null && sSelectingNode == p_sFullName)
                {
                    pSelectingNode = rValue;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        #endregion

        #region//========== Public 函式 (其他) ==========
        private bool IsValueType(object p_Obj)
        {
            bool rValue = false;
            try
            {
                if (p_Obj == null
                    || p_Obj.GetType().IsValueType == true
                    || p_Obj is string)
                {
                    rValue = true;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        #endregion


        #region//========== Private 函式 ==========

        private List<TreeNode> ConvertChildrenObjToNote(object p_Data)
        {
            List<TreeNode> rValue = new List<TreeNode>();
            try
            {
                if (IsValueType(p_Data))
                {
                    //ValueType
                }
                else if (p_Data.GetType().IsArray == true)
                {
                    #region//Array
                    Array ArrayObj_Source = (Array)p_Data;
                    List<TreeNode> NotValueType = new List<TreeNode>();
                    List<string> LstIndex = new List<string>();
                    int ArrayRank_Source = ArrayObj_Source.Rank;
                    if (ArrayRank_Source > 0)
                    {
                        int[] index_Source = new int[ArrayRank_Source];
                        int[] DimensiongLength = new int[ArrayRank_Source];
                        for (int iRank = 0; iRank < ArrayRank_Source; iRank++)
                        {
                            DimensiongLength[iRank] = ArrayObj_Source.GetLength(iRank);
                        }
                        while (true)
                        {
                            for (int iRank = 0; iRank < LstIndex.Count; iRank++)
                            {
                                index_Source[iRank] = Convert.ToInt32(LstIndex[iRank]);
                            }
                            string sIndex = "";
                            for (int i = 0; i < index_Source.Length; i++)
                            {
                                if (sIndex != "")
                                { sIndex += ","; }
                                sIndex += index_Source[i];
                            }
                            sIndex = "(" + sIndex + ")";
                            if (IsValueType(ArrayObj_Source.GetValue(index_Source)) == true)
                            {
                                string sName = "[" + sIndex + "]";
                                if (ArrayObj_Source.GetValue(index_Source) != null)
                                {
                                    sName += ": " + ArrayObj_Source.GetValue(index_Source).ToString();
                                }
                                else
                                {
                                    sName += ": (null)";
                                }
                                TreeNode ChildrenNote = new TreeNode(sName);
                                ChildrenNote.Tag = ArrayObj_Source.GetValue(index_Source);
                                rValue.Add(ChildrenNote);
                                //ValueType
                            }
                            else
                            {
                                string sName = "[" + sIndex + "]";
                                TreeNode ChildrenNote = new TreeNode(sName);
                                TreeNode ChildrenItems = new TreeNode("Loading...");
                                ChildrenNote.Nodes.Add(ChildrenItems);
                                ChildrenNote.Tag = ArrayObj_Source.GetValue(index_Source);
                                NotValueType.Add(ChildrenNote);
                            }
                            #region  // 更新索引（模擬多維計數器）
                            int dim = ArrayRank_Source - 1;
                            while (dim >= 0)
                            {
                                index_Source[dim]++;
                                if (index_Source[dim] < DimensiongLength[dim])
                                {
                                    break;
                                }
                                else
                                {
                                    index_Source[dim] = 0;
                                    dim--;
                                }
                            }

                            // 如果最高位元也溢位了，表示已完成所有元素
                            if (dim < 0)
                            {
                                break;
                            }
                            #endregion
                        }
                    }
                    rValue.AddRange(NotValueType.ToArray());
                    #endregion
                }
                else if (p_Data is IList)
                {
                    #region//List
                    IList IDic_Source = (IList)p_Data;
                    List<TreeNode> NotValueType = new List<TreeNode>();
                    for (int i = 0; i < IDic_Source.Count; i++)
                    {
                        if (IsValueType(IDic_Source[i]) == true)
                        {
                            string sName = "[" + i.ToString() + "]";
                            if (IDic_Source[i] != null)
                            {
                                sName += ": " + IDic_Source[i].ToString();
                            }
                            else
                            {
                                sName += ": (null)";
                            }
                            TreeNode ChildrenNote = new TreeNode(sName);
                            ChildrenNote.Tag = IDic_Source[i];
                            rValue.Add(ChildrenNote);
                            //ValueType
                        }
                        else
                        {
                            string sName = "[" + i.ToString() + "]";
                            TreeNode ChildrenNote = new TreeNode(sName);
                            TreeNode ChildrenItems = new TreeNode("Loading...");
                            ChildrenNote.Nodes.Add(ChildrenItems);
                            ChildrenNote.Tag = IDic_Source[i];
                            NotValueType.Add(ChildrenNote);
                        }
                    }
                    rValue.AddRange(NotValueType.ToArray());
                    #endregion
                }
                else if (p_Data is IDictionary)
                {
                    #region//Dictionary
                    IDictionary IDic_Source = (IDictionary)p_Data;
                    List<TreeNode> NotValueType = new List<TreeNode>();
                    foreach (DictionaryEntry entry in IDic_Source)
                    {
                        if (IsValueType(IDic_Source[entry.Key]) == true)
                        {
                            string sName = "[" + entry.Key.ToString() + "]";
                            if (IDic_Source[entry.Key] != null)
                            {
                                sName += ": " + IDic_Source[entry.Key].ToString();
                            }
                            else
                            {
                                sName += ": (null)";
                            }
                            TreeNode ChildrenNote = new TreeNode(sName);
                            ChildrenNote.Tag = IDic_Source[entry.Key];
                            rValue.Add(ChildrenNote);
                            //ValueType
                        }
                        else
                        {
                            string sName = "[" + entry.Key.ToString() + "]";
                            TreeNode ChildrenNote = new TreeNode(sName);
                            TreeNode ChildrenItems = new TreeNode("Loading...");
                            ChildrenNote.Nodes.Add(ChildrenItems);
                            ChildrenNote.Tag = IDic_Source[entry.Key];
                            NotValueType.Add(ChildrenNote);
                        }
                    }
                    rValue.AddRange(NotValueType.ToArray());
                    #endregion
                }
                else
                {
                    #region//Class
                    var LstFields = p_Data.GetType().GetFields().ToList();
                    var LstProperties = p_Data.GetType().GetProperties().ToList();
                    List<TreeNode> NotValueType = new List<TreeNode>();
                    foreach (var FieldItem in LstFields)
                    {
                        if (Attribute.IsDefined(FieldItem, typeof(JsonIgnoreAttribute)) == false
                            && Attribute.IsDefined(FieldItem, typeof(XmlIgnoreAttribute)) == false)
                        {
                            if (clsClassFunc.FieldCanWrite(FieldItem) == true)
                            {
                                string sName = FieldItem.Name;
                                var SourceValue = FieldItem.GetValue(p_Data);
                                if (IsValueType(SourceValue) == true)
                                {
                                    if (SourceValue != null)
                                    {
                                        sName += ": " + SourceValue;
                                    }
                                    else
                                    {
                                        sName += ": (null)";
                                    }
                                    TreeNode ChildrenNote = new TreeNode(sName);
                                    rValue.Add(ChildrenNote);
                                    //ValueType
                                }
                                else
                                {
                                    if (SourceValue.GetType().IsArray == true)
                                    {
                                        #region//Array
                                        sName += " (Array)";
                                        #endregion
                                    }
                                    else if (SourceValue is IList)
                                    {
                                        #region//List
                                        IList IList_Source = (IList)SourceValue;
                                        sName += " (List[" + IList_Source.Count + "])";
                                        #endregion
                                    }
                                    else if (SourceValue is IDictionary)
                                    {
                                        #region//Dictionary
                                        IDictionary IDic_Source = (IDictionary)SourceValue;
                                        sName += " (Dictionary[" + IDic_Source.Count + "])";
                                        #endregion
                                    }
                                    TreeNode ChildrenNote = new TreeNode(sName);
                                    TreeNode ChildrenItems = new TreeNode("Loading...");
                                    ChildrenNote.Nodes.Add(ChildrenItems);
                                    ChildrenNote.Tag = SourceValue;
                                    NotValueType.Add(ChildrenNote);
                                }
                            }
                        }
                    }
                    foreach (var PropertiesItem in LstProperties)
                    {
                        if (Attribute.IsDefined(PropertiesItem, typeof(JsonIgnoreAttribute)) == false
                            && Attribute.IsDefined(PropertiesItem, typeof(XmlIgnoreAttribute)) == false)
                        {
                            MethodInfo setMethod = PropertiesItem.GetSetMethod(/* nonPublic */ true);
                            bool hasPublicSet = setMethod != null && setMethod.IsPublic;
                            if (hasPublicSet == true)
                            {
                                var SourceValue = PropertiesItem.GetValue(p_Data, null);
                                if (IsValueType(SourceValue) == true)
                                {
                                    string sName = PropertiesItem.Name;
                                    if (SourceValue != null)
                                    {
                                        sName += ": " + SourceValue;
                                    }
                                    else
                                    {
                                        sName += ": (null)";
                                    }
                                    TreeNode ChildrenNote = new TreeNode(sName);
                                    rValue.Add(ChildrenNote);
                                    //ValueType
                                }
                                else
                                {
                                    string sItemName = PropertiesItem.Name;
                                    if (SourceValue.GetType().IsArray == true)
                                    {
                                        #region//Array
                                        sItemName += " (Array)";
                                        #endregion
                                    }
                                    else if (SourceValue is IList)
                                    {
                                        #region//List
                                        IList IList_Source = (IList)SourceValue;
                                        sItemName += " (List[" + IList_Source.Count + "])";
                                        #endregion
                                    }
                                    else if (SourceValue is IDictionary)
                                    {
                                        #region//Dictionary
                                        IDictionary IDic_Source = (IDictionary)SourceValue;
                                        sItemName += " (Dictionary[" + IDic_Source.Count + "])";
                                        #endregion
                                    }
                                    TreeNode ChildrenNote = new TreeNode(sItemName);
                                    TreeNode ChildrenItems = new TreeNode("Loading...");
                                    ChildrenNote.Nodes.Add(ChildrenItems);
                                    ChildrenNote.Tag = SourceValue;
                                    NotValueType.Add(ChildrenNote);
                                }
                            }
                        }
                    }
                    rValue.AddRange(NotValueType.ToArray());
                    #endregion
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }






        #endregion

        #region//========== 事件處理 ==========

        private void ucClassDataViewer_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                var parentNode = e.Node;
                if (parentNode.Nodes.Count == 1)
                {
                    if (parentNode.Nodes[0] == null)
                    {
                        return;
                    }
                }
                if (parentNode.Nodes.Count == 1 && parentNode.Nodes[0].Text == "Loading..."
                    && parentNode.Tag != null)
                {
                    string sNodeFullName = parentNode.Name;
                    GetSelectedNodePathName(parentNode, ref sNodeFullName);
                    sSelectingNode = sNodeFullName;
                    TreeNode tNode = ConvertObjToNote(parentNode.Tag, parentNode.Name, sNodeFullName);
                    parentNode.Nodes.Clear();
                    for (int i = 0; i < tNode.Nodes.Count; i++)
                    {
                        parentNode.Nodes.Add(tNode.Nodes[i]);
                    }
                    this.SelectedNode = pSelectingNode;
                }

                //展開指定Node
                try
                {
                    if (pSelectingNode != null && this.SelectedNode != pSelectingNode)
                    {
                        this.SelectedNode = pSelectingNode;
                        pSelectingNode.EnsureVisible(); // 自動捲動到該節點
                    }
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            try
            {
                TreeView treeView = sender as TreeView;

                if (e.Node != null && e.Node.Text != null)
                {
                    // 如果节点被选中并且 TreeView 没有焦点
                    if (e.Node == treeView.SelectedNode)
                    {
                        // TreeView 有焦点时，使用正常的选中颜色
                        e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                        TextRenderer.DrawText(e.Graphics, e.Node.Text, treeView.Font, e.Bounds, SystemColors.HighlightText);
                    }
                    else
                    {
                        // 如果节点未被选中，使用默认的绘制
                        e.DrawDefault = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

    }
}

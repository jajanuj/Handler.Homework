using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Windows.Forms;
using ArtCommonLib;
using Shell32;


namespace ArtSystem
{
    public partial class ucDLLVersion : ucBaseUserControl
    {
        #region //========== 變數設置 ========== 

        /// <summary> DLL資訊載入只需要執行一次 </summary>
        private bool m_bAlreadyUpdateControls = false;

        private List<AssemblyInfoModel> assemblyList = new List<AssemblyInfoModel>();

        /// <summary> DLL已載入DLL資訊 (使用Binding連結DataGridView) </summary>
        private SortableBindingList<AssemblyInfoModel> DGVassemblyList = new SortableBindingList<AssemblyInfoModel>();

        /// <summary> 儲存顯示資訊的文件路徑 </summary>
        private string configPath = "INI\\visibleFields.config";
        /// <summary> 儲存隱藏DLL的文件路徑 </summary>
        private string dllConfigPath = "INI\\hiddenAssemblies.config";

        /// <summary> 要顯示哪些資訊收集 </summary>
        private AssemblyInfoFields visibleFields = AssemblyInfoFields.Name | AssemblyInfoFields.Version | AssemblyInfoFields.Company;

        /// <summary> 要隱藏哪些DLL收集  </summary>
        private HashSet<string> hiddenAssemblies = new HashSet<string>();



        #endregion

        #region //========== Class 定義 ========== 
        /// <summary> DLL資訊 </summary>
        public class AssemblyInfoModel
        {
            public string Name { get; set; }
            public string Version { get; set; }
            public string FileVersion { get; set; }
            public string InformationalVersion { get; set; }
            public string Company { get; set; }
            public string Product { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }
            public string TargetFramework { get; set; }
            public string ReleaseDate { get; set; }
            public string FileSize { get; set; }
        }

        /// <summary> 將Binding結構定義成可以Sortable(預設無法Sort) </summary>
        public class SortableBindingList<T> : BindingList<T>
        {
            private bool isSorted;
            private ListSortDirection sortDirection;
            private PropertyDescriptor sortProperty;

            //protected override bool SupportsSortingCore => true;
            //protected override bool IsSortedCore => isSorted;
            //protected override PropertyDescriptor SortPropertyCore => sortProperty;
            //protected override ListSortDirection SortDirectionCore => sortDirection;

            protected override bool SupportsSortingCore { get { return true; } }
            protected override bool IsSortedCore { get { return isSorted; } }
            protected override PropertyDescriptor SortPropertyCore { get { return sortProperty; } }
            protected override ListSortDirection SortDirectionCore { get { return sortDirection; } }


            protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
            {
                var items = (List<T>)Items;
                items.Sort((x, y) =>
                {
                    var xValue = prop.GetValue(x);
                    var yValue = prop.GetValue(y);
                    return direction == ListSortDirection.Ascending
                    ? Comparer<object>.Default.Compare(xValue, yValue)
                    : Comparer<object>.Default.Compare(yValue, xValue);
                });

                sortProperty = prop;
                sortDirection = direction;
                isSorted = true;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }

            protected override void RemoveSortCore()
            {
                isSorted = false;
            }
        }


        #endregion

        #region //========== Enum 定義 ========== 

        /// <summary> DLL資訊Flag </summary>
        [Flags]
        public enum AssemblyInfoFields
        {
            None = 0,
            Name = 1 << 0,
            Version = 1 << 1,
            FileVersion = 1 << 2,
            InformationalVersion = 1 << 3,
            Company = 1 << 4,
            Product = 1 << 5,
            Description = 1 << 6,
            Location = 1 << 7,
            TargetFramework = 1 << 8,
            ReleaseDate = 1 << 9,
            FileSize = 1 << 10,
        }

        #endregion

        #region //==========  必要函式設置 ========== 

        static private object objLock = new object();
        static private ucDLLVersion m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucDLLVersion GetSingleton()
        {
            lock (objLock)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucDLLVersion();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucDLLVersion()
        {
            InitializeComponent();
            if (ArtSystem.clsArtSystem.bIsProgramOpen == false)
            {
                return;
            }
            this.VisibleChanged += new EventHandler(UserControl_VisibleChanged);
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                if (m_bAlreadyUpdateControls == false)
                {
                    m_bAlreadyUpdateControls = true;
                    LoadVisibleFields();
                    LoadHiddenAssemblies();
                    InitializeDataGridViewColumns();
                    InitializeContextMenu();
                    LoadAssemblies();
                }
                LoadAssemblies();
                UpdateAssemblies();

            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 自動更新介面參數 </summary>
        protected override void ReflashTimerFunc()
        {
            try
            {
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 進入此介面時,自動執行UpdateControls </summary>
        protected void UserControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                UpdateControls();
            }
        }


        #endregion

        #region //========== ShowForm 函式設置 ==========
        private Form mForm = null;
        private Control m_OrgParent = null;
        private Size initialSize = new Size();
        /// <summary> 使用Form顯示 </summary>
        public void _ShowForm(bool Dialog = true)
        {
            if (mForm == null)
            {
                mForm = new Form();
                mForm.WindowState = FormWindowState.Normal;
                mForm.ClientSize = this.initialSize;
                mForm.StartPosition = FormStartPosition.CenterScreen;
                mForm.Text = clsLanguage.GetTranslation(this.Name, false);
                mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
                mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
                this.m_OrgParent = this.Parent;
                this.Parent = mForm;
                this.SetReflashTimerStart(true);
                this.Dock = DockStyle.Fill;
                if (Dialog == true)
                {
                    mForm.ShowDialog();
                }
                else
                {
                    mForm.Show();
                }
            }
        }
        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.SetReflashTimerStart(false);
                this.Parent = this.m_OrgParent;
                this.mForm = null;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void mForm_Deactivate(object sender, EventArgs e)
        {
            try
            {
                if (this.mForm != null)
                {
                    Form mForm = this.mForm;
                    this.SetReflashTimerStart(false);
                    this.Parent = this.m_OrgParent;
                    this.mForm = null;
                    mForm.Close();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region//========== Public 函式 ========== 


        #endregion

        #region//========== Private (Initial)函式 ========== 
        /// <summary> 初始化所有Column(只需要執行一次) </summary>
        private void InitializeDataGridViewColumns()
        {

            try
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                foreach (AssemblyInfoFields field in Enum.GetValues(typeof(AssemblyInfoFields)))
                {
                    if (field == AssemblyInfoFields.None) continue;

                    var column = new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = field.ToString(),
                        HeaderText = field.ToString(),
                        Name = field.ToString(),
                        Visible = visibleFields.HasFlag(field),
                        SortMode = DataGridViewColumnSortMode.Automatic,
                        Width = 300,
                    };

                    dataGridView1.Columns.Add(column);
                }

                dataGridView1.DataSource = DGVassemblyList;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void InitializeContextMenu()
        {
            try
            {
                dataGridView1.MouseDown += (s, e) =>
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        var hit = dataGridView1.HitTest(e.X, e.Y);
                        if (hit.Type == DataGridViewHitTestType.ColumnHeader)
                        {
                            ShowColumnControlMenu(e.Location);
                        }
                        else
                        {
                            string sDLLName = dataGridView1[0, hit.RowIndex].Value.ToString();
                            ShowAssemblyControlMenu(sDLLName, e.Location);
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void LoadAssemblies()
        {
            try
            {
                assemblyList.Clear();
                visibleFields |= AssemblyInfoFields.Name;
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    try
                    {
                        if (asm.GlobalAssemblyCache) continue;

                        var name = asm.GetName();
                        var model = new AssemblyInfoModel();
                        if (name.Name.StartsWith("System") || name.Name.StartsWith("Microsoft"))
                        {
                            continue;
                        }
                        //if (hiddenAssemblies.Contains(name.Name)) continue;
                        model.Name = name.Name;
                        model.Version = name.Version.ToString();
                        model.FileVersion = GetAttributeValue<AssemblyFileVersionAttribute>(asm, a => a.Version);
                        model.InformationalVersion = GetAttributeValue<AssemblyInformationalVersionAttribute>(asm, a => a.InformationalVersion);
                        model.Company = GetAttributeValue<AssemblyCompanyAttribute>(asm, a => a.Company);
                        model.Product = GetAttributeValue<AssemblyProductAttribute>(asm, a => a.Product);
                        model.Description = GetAttributeValue<AssemblyDescriptionAttribute>(asm, a => a.Description);
                        try
                        {
                            model.Location = asm.Location;
                            if (System.IO.File.Exists(model.Location) == true)
                            {
                                model.FileSize = GetFileSizeString(model.Location);
                                model.ReleaseDate = System.IO.File.GetLastWriteTime(model.Location).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                        model.TargetFramework = GetTargetFramework(asm);

                        assemblyList.Add(model);
                    }
                    catch (Exception ex)
                    {
                        clsArtSystem.CatchLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        static public AssemblyInfoModel ReadAssemblyInfoFromFile(string dllPath)
        {
            AssemblyInfoModel rValue = null;
            try
            {
                if (File.Exists(dllPath) == true)
                {
                    rValue = new AssemblyInfoModel();
                    var info = FileVersionInfo.GetVersionInfo(dllPath);
                    rValue.Name = Path.GetFileNameWithoutExtension(dllPath);
                    rValue.Version = info.ProductVersion;
                    rValue.FileVersion = info.FileVersion;
                    rValue.InformationalVersion = info.ProductVersion; // FileVersionInfo 無此欄位，暫用 ProductVersion
                    rValue.Company = info.CompanyName;
                    rValue.Product = info.ProductName;
                    rValue.Description = info.FileDescription;
                    rValue.Location = dllPath;
                    rValue.FileSize = GetFileSizeString(dllPath);
                    rValue.ReleaseDate = File.GetLastWriteTime(dllPath).ToString("yyyy-MM-dd HH:mm:ss");
                    // TargetFramework 無法透過 FileVersionInfo 取得，可留空或用其他方法補上
                    rValue.TargetFramework = "";
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex); // 你原本的錯誤處理方式
            }

            return rValue;
        }

        #endregion

        #region//========== Private 函式 (DLL資訊) ========== 

        static private string GetFileSizeString(string filePath)
        {
            string rValue = "";
            try
            {

                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists == true)
                {

                    long size = fileInfo.Length;
                    string[] units = { "Bytes", "KB", "MB", "GB", "TB" };
                    double formattedSize = size;
                    int unitIndex = 0;
                    while (formattedSize >= 1024 && unitIndex < units.Length - 1)
                    {
                        formattedSize /= 1024;
                        unitIndex++;
                    }
                    rValue = formattedSize.ToString("F2") + " " + units[unitIndex];
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }


        private void UpdateAssemblies()
        {
            try
            {
                DGVassemblyList.Clear();
                foreach (var Item in assemblyList)
                {
                    if (hiddenAssemblies.Contains(Item.Name)) continue;
                    DGVassemblyList.Add(Item);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void UpdateColumnVisibility()
        {

            foreach (AssemblyInfoFields field in Enum.GetValues(typeof(AssemblyInfoFields)))
            {
                if (field == AssemblyInfoFields.None) continue;

                var column = dataGridView1.Columns[field.ToString()];
                if (field == AssemblyInfoFields.Name == true)
                {
                    column.Visible = true;//DLL名稱一定要顯示
                }
                else if (column != null)
                {
                    column.Visible = visibleFields.HasFlag(field);
                }
            }
        }

        private string GetAttributeValue<T>(Assembly asm, Func<T, string> valueSelector) where T : Attribute
        {
            var attrs = asm.GetCustomAttributes(typeof(T), false);
            if (attrs.Length > 0)
            {
                return valueSelector((T)attrs[0]);
            }
            return string.Empty;
        }

        private string GetTargetFramework(Assembly asm)
        {
            var attrs = asm.GetCustomAttributes(typeof(TargetFrameworkAttribute), false);
            if (attrs.Length > 0)
            {
                return ((TargetFrameworkAttribute)attrs[0]).FrameworkName;
            }
            return string.Empty;
        }

        #endregion

        #region//========== Preivate (Show Control Menu) =========

        private void ShowColumnControlMenu(Point location)
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add(new ToolStripSeparator());

            foreach (AssemblyInfoFields field in Enum.GetValues(typeof(AssemblyInfoFields)))
            {
                if (field == AssemblyInfoFields.None || field == AssemblyInfoFields.Name) continue;

                var currentField = field; // ✅ 建立區域變數
                var item = new ToolStripMenuItem(field.ToString())
                {
                    Tag = currentField,
                    Checked = visibleFields.HasFlag(field),
                    CheckOnClick = true
                };

                item.CheckedChanged += (s, e) =>
                {
                    if (item.Checked)
                        visibleFields |= currentField;
                    else
                        visibleFields &= ~currentField;

                    UpdateColumnVisibility();
                    SaveVisibleFields();
                };

                menu.Items.Add(item);
            }
            menu.Show(dataGridView1, location);
        }

        private void ShowAssemblyControlMenu(string assemblyName, Point location)
        {
            var menu = new ContextMenuStrip();

            bool isHidden = hiddenAssemblies.Contains(assemblyName);

            menu.Items.Add("隱藏此 DLL", null, (s, e) =>
            {
                if (isHidden)
                    hiddenAssemblies.Remove(assemblyName);
                else
                    hiddenAssemblies.Add(assemblyName);

                SaveHiddenAssemblies();
                UpdateAssemblies();
            });
            menu.Items.Add("顯示全部", null, (s, e) =>
            {
                hiddenAssemblies.Clear();
                SaveHiddenAssemblies();
                UpdateAssemblies();
            });

            ToolStripMenuItem subItem = new ToolStripMenuItem("取消隱藏 DLL");
            foreach (string hideItemName in hiddenAssemblies)
            {
                subItem.DropDownItems.Add(hideItemName, null, (s, e) =>
                {
                    hiddenAssemblies.Remove(hideItemName);
                    SaveHiddenAssemblies();
                    UpdateAssemblies();
                });
            }
            subItem.Enabled = hiddenAssemblies.Count > 0;

            menu.Items.Add(subItem);


            menu.Show(dataGridView1, location);
        }

        #endregion

        #region//========== Preivate (設定儲存功能) =========

        private void SaveVisibleFields()
        {
            System.IO.File.WriteAllText(configPath, ((int)visibleFields).ToString());
        }

        private void LoadVisibleFields()
        {
            if (System.IO.File.Exists(configPath))
            {
                var text = System.IO.File.ReadAllText(configPath);
                int value = 0;
                if (int.TryParse(text, out value))
                {
                    visibleFields = (AssemblyInfoFields)value;
                }
            }
        }

        private void SaveHiddenAssemblies()
        {
            System.IO.File.WriteAllLines(dllConfigPath, hiddenAssemblies);
        }

        private void LoadHiddenAssemblies()
        {
            if (System.IO.File.Exists(dllConfigPath))
            {
                hiddenAssemblies = new HashSet<string>(System.IO.File.ReadAllLines(dllConfigPath));
            }
        }

        #endregion

        #region//========== 事件處理 ========== 

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Threading;

namespace ArtTeach
{
    public partial class SlideList : UserControl
    {
        #region //=====================  區域變數設置 =====================       
        Bitmap bmpList;
        Graphics gList;
        private float fItemWidth = 0;
        private float fItemHeight = 0;
        private bool bIsMouseEnter = false;
        private bool bIsMouseClickCheck = false;
        private bool bIsMouseDown = false;
        private int iScrollButton = 0;
        private int _pItemHeight = 150;
        private int _pSpacerHeight = 10;
        private List<string> _list = new List<string>();
        //private Dictionary<string, int> _showListLinkIndex = new Dictionary<string, int>();
        private List<ListItem> _showList = new List<ListItem>();
        private int iMouseWheelMove = 0;
        private int iMouseY = 0;
        private int iMouseDownX = 0;
        private int iMouseDownY = 0;
        private int iShowItemCount = 0;
        private int iSelectedItem = 0;
        private string strSelectedItem = "";
        private string strSelectedParentsItem = "";

        public enum ShowButtonType
        {
            Normal,
            Parents,
            Child,
        }

        public enum ShowButtonStatus
        {
            Normal,
            MouseOver,
            MouseDown,
            Selected,
            //Unfolded,            
        }

        public class ListItem
        {
            public ShowButtonStatus sbsStatus;
            public ShowButtonType sbsType;
            public string strItemList;
            public List<ListItem> listChildList;
            public int iStartY = 0;
            public int iEndY = 0;
            public bool bIsSelected = false;
            public bool bIsUnfolded = false;

            public ListItem Clone()
            {
                ListItem tmpItem = new ListItem();
                tmpItem.sbsStatus = this.sbsStatus;
                tmpItem.strItemList = this.strItemList;
                tmpItem.iEndY = this.iEndY;
                tmpItem.iStartY = this.iStartY;
                tmpItem.listChildList = new List<ListItem>();
                tmpItem.sbsType = this.sbsType;
                tmpItem.bIsSelected = this.bIsSelected;
                tmpItem.bIsUnfolded = this.bIsUnfolded;

                foreach (ListItem childItem in this.listChildList)
                {
                    ListItem tmpChildItem = new ListItem();
                    tmpChildItem.sbsStatus = childItem.sbsStatus;
                    tmpChildItem.sbsType = childItem.sbsType;
                    tmpChildItem.strItemList = childItem.strItemList;
                    tmpChildItem.iEndY = childItem.iEndY;
                    tmpChildItem.iStartY = childItem.iStartY;
                    tmpChildItem.listChildList = null;
                    tmpChildItem.bIsSelected = false;
                    tmpChildItem.bIsUnfolded = false;
                    tmpItem.listChildList.Add(tmpChildItem);

                }
                return tmpItem;
            }
        }

        //private Color m_colorNull = Color.WhiteSmoke;
        //private Color m_colorExist = Color.Lime;
        //private Color m_colorSelectAndExist = Color.Aqua;
        //private Color m_colorSelect = Color.Orange;
        //private Color m_colorMouseEnter = Color.Pink;
        //private Color m_colorSelected = Color.Gray;
        //private Color m_colorWorking = Color.Green;
        //private Color m_colorDone = Color.GreenYellow;

        SolidBrush m_BrushButton;
        SolidBrush m_BrushMouseDownButton;
        SolidBrush m_BrushMouseOverButton;
        SolidBrush m_BrushChildButton;
        SolidBrush m_BrushChildMouseDownButton;
        SolidBrush m_BrushChildMouseOverButton;
        SolidBrush m_BrushScrollButton;
        SolidBrush m_BrushScrollButtonArrow;
        SolidBrush m_BrushText;
        SolidBrush m_BrushShadow1;
        SolidBrush m_BrushShadow2;
        Pen m_PenSelected;
       
        /// <summary>List項目</summary>
        [Description("List項目")]
        [Category("User define"), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design", typeof(UITypeEditor))]
        public List<string> _ItemList
        {
            get
            {
                return _list;
            }
            set
            {
                _list = value;
            }
        }

        [Description("項目高度")]
        [Category("User define"), Browsable(true)]
        [DefaultValue(150)]
        public int _ItemHeight
        {
            get
            {
                return _pItemHeight;
            }
            set
            {
                _pItemHeight = value;
            }
        }

        [Description("間隔高度")]
        [Category("User define"), Browsable(true)]
        [DefaultValue(10)]
        public int _SpacerHeight
        {
            get
            {
                return _pSpacerHeight;
            }
            set
            {
                _pSpacerHeight = value;
            }
        }

        //[Description("自動換行")]
        //[Category("User define"), Browsable(true)]
        //public bool _AutoWrap
        //{
        //    get;
        //    set;
        //}
        
        [Description("按鈕顏色")]
        [Category("User define"), Browsable(true)]
        public Color _ButtonColor
        {
            get;
            set;
        }

        [Description("滑鼠游標在上的按鈕顏色")]
        [Category("User define"), Browsable(true)]
        public Color _MouseOverButtonColor
        {
            get;
            set;
        }

        [Description("滑鼠按下的按鈕顏色")]
        [Category("User define"), Browsable(true)]
        public Color _MouseDownButtonColor
        {
            get;
            set;
        }

        [Description("Child按鈕顏色")]
        [Category("User define"), Browsable(true)]
        public Color _ChildButtonColor
        {
            get;
            set;
        }

        [Description("滑鼠游標在上的Child按鈕顏色")]
        [Category("User define"), Browsable(true)]
        public Color _ChildMouseOverButtonColor
        {
            get;
            set;
        }

        [Description("滑鼠按下的Child按鈕顏色")]
        [Category("User define"), Browsable(true)]
        public Color _ChildMouseDownButtonColor
        {
            get;
            set;
        }

        [Description("捲軸按鈕顏色")]
        [Category("User define"), Browsable(true)]
        public Color _ScrollButtonColor
        {
            get;
            set;
        }

        [Description("捲軸按鈕箭頭顏色")]
        [Category("User define"), Browsable(true)]
        public Color _ScrollButtonArrowColor
        {
            get;
            set;
        }

        [Description("選擇框顏色")]
        [Category("User define"), Browsable(true)]
        public Color _SelectedColor
        {
            get;
            set;
        }

        [Description("文字顏色")]
        [Category("User define"), Browsable(true)]
        public Color _TextColor
        {
            get;
            set;
        }

        [Description("顯示陰影")]
        [Category("User define"), Browsable(true)]
        public bool _Shadow
        {
            get;
            set;
        }

        public string SelectedItem
        {
            get
            {
                return strSelectedItem;
            }
        }

        #endregion //======================================================
        public delegate void ItemClickDelegate(object sender, EventArgs e); // 自定一個委派
        public event ItemClickDelegate ItemClick; //使用此委派宣告event

       
        public SlideList()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void CreateShowList()
        {
            m_BrushButton = new SolidBrush(_ButtonColor);
            m_BrushMouseDownButton = new SolidBrush(_MouseDownButtonColor);
            m_BrushMouseOverButton = new SolidBrush(_MouseOverButtonColor);
            m_BrushChildButton = new SolidBrush(_ChildButtonColor);
            m_BrushChildMouseDownButton = new SolidBrush(_ChildMouseDownButtonColor);
            m_BrushChildMouseOverButton = new SolidBrush(_ChildMouseOverButtonColor);
            m_BrushScrollButton = new SolidBrush(Color.FromArgb(64, _ScrollButtonColor));
            m_BrushScrollButtonArrow = new SolidBrush(Color.FromArgb(128, _ScrollButtonArrowColor));
            m_BrushText = new SolidBrush(_TextColor);
            m_BrushShadow1 = new SolidBrush(Color.FromArgb(128, 128, 128, 128));
            m_BrushShadow2 = new SolidBrush(Color.FromArgb(192, 192, 192, 192));
            m_PenSelected = new Pen(_SelectedColor, _SpacerHeight / 2);

            string[] childItemList;
            int tmpCount = 0;
            iShowItemCount = _list.Count();
            for (int i = 0; i < _ItemList.Count; i++)
            {
                if (_ItemList[i].Contains(','))
                {
                    childItemList = _ItemList[i].Split(',');
                    ListItem tmpParentsItem = new ListItem();
                    tmpParentsItem.sbsStatus = ShowButtonStatus.Normal;
                    tmpParentsItem.sbsType = ShowButtonType.Parents;
                    tmpParentsItem.strItemList = childItemList[0];
                    tmpParentsItem.listChildList = new List<ListItem>();
                    tmpParentsItem.bIsSelected = false;
                    _showList.Add(tmpParentsItem);
                    //_showListLinkIndex.Add(tmpParentsItem.strItemList, tmpCount);
                    tmpCount++;

                    for (int j = 1; j < childItemList.Count(); j++)
                    {
                        ListItem tmpChildItem = new ListItem();
                        tmpChildItem.strItemList = childItemList[j];
                        tmpChildItem.sbsStatus = ShowButtonStatus.Normal;
                        tmpChildItem.sbsType = ShowButtonType.Child;
                        tmpChildItem.listChildList = null;
                        tmpChildItem.bIsSelected = false;
                        tmpParentsItem.listChildList.Add(tmpChildItem);
                        //_showListLinkIndex.Add(tmpChildItem.strItemList, tmpCount);
                        tmpCount++;
                    }
                }
                else
                {
                    ListItem tmpItem = new ListItem();
                    tmpItem.strItemList = _ItemList[i];
                    tmpItem.sbsStatus = ShowButtonStatus.Normal;
                    tmpItem.sbsType = ShowButtonType.Normal;
                    tmpItem.bIsSelected = false;
                    _showList.Add(tmpItem);
                    //_showListLinkIndex.Add(tmpItem.strItemList, tmpCount);
                    tmpCount++;
                }
            }
        }

        private SolidBrush GetItemBrush(ShowButtonStatus p_sbsStatus, ShowButtonType p_sbsType)
        {
            switch (p_sbsType)
            {
                case ShowButtonType.Child:
                    switch (p_sbsStatus)
                    {
                        case ShowButtonStatus.MouseDown:
                            return m_BrushChildMouseDownButton;
                        case ShowButtonStatus.MouseOver:
                            return m_BrushChildMouseOverButton;
                        case ShowButtonStatus.Normal:
                            return m_BrushChildButton;
                        case ShowButtonStatus.Selected:
                            return m_BrushChildButton;
                        default:
                            return m_BrushButton;
                    }
                case ShowButtonType.Normal:
                    switch (p_sbsStatus)
                    {
                        case ShowButtonStatus.MouseDown:
                            return m_BrushMouseDownButton;
                        case ShowButtonStatus.MouseOver:
                            return m_BrushMouseOverButton;
                        case ShowButtonStatus.Normal:
                            return m_BrushButton;
                        case ShowButtonStatus.Selected:
                            return m_BrushButton;
                        default:
                            return m_BrushButton;
                    }
                case ShowButtonType.Parents:
                    switch (p_sbsStatus)
                    {
                        case ShowButtonStatus.MouseDown:
                            return m_BrushMouseDownButton;
                        case ShowButtonStatus.MouseOver:
                            return m_BrushMouseOverButton;
                        case ShowButtonStatus.Normal:
                            return m_BrushButton;
                        //case ShowButtonStatus.Unfolded:
                        //    return m_BrushButton;
                        default:
                            return m_BrushButton;
                    }
                default:
                    return m_BrushButton;
            }
        }

        private void DrawList(bool p_forceReflash)
        {
            if (_showList == null || _showList.Count == 0)
            {
                CreateShowList();
            }
            if (p_forceReflash && _showList != null)
            {
                Thread.Sleep(33);
                

                //建立外型                
                pbCanvas.Width = this.Width;
            

                pbCanvas.Height = iShowItemCount * (_ItemHeight + _SpacerHeight) + _SpacerHeight;
                pbCanvas.Location = new Point(0, pbCanvas.Location.Y + iMouseWheelMove);

                if (pbCanvas.Location.Y < this.Height - pbCanvas.Height)
                {
                    pbCanvas.Location = new Point(0, this.Height - pbCanvas.Height);
                }
                else if (pbCanvas.Location.Y > 0)
                {
                    pbCanvas.Location = new Point(0, 0);
                }

                try
                {
                    if (bmpList.Height != pbCanvas.Height)
                    {
                        bmpList = new Bitmap(pbCanvas.Width, pbCanvas.Height);
                        GC.Collect();
                    }
                }
                catch
                {
                    if (pbCanvas.Width == 0)
                    {
                        pbCanvas.Width = 100;
                    }
                    if (pbCanvas.Height == 0)
                    {
                        pbCanvas.Height = 100;
                    }
                    bmpList = new Bitmap(pbCanvas.Width, pbCanvas.Height);
                    GC.Collect();
                }
                gList = Graphics.FromImage(bmpList);
                gList.Clear(this.BackColor);
                
                try
                {
                    float fItemStartY = 0;
                    float fItemEndY = 0;
                    float fItemStartX = 0;
                    float fItemEndX = 0;

                    fItemWidth = pbCanvas.Width - _SpacerHeight * 2;
                    fItemHeight = _ItemHeight;

                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    Font drawFont = new Font("Verdana", Math.Min(fItemWidth, fItemHeight) * 0.2f, FontStyle.Regular);

                    iShowItemCount = 0;

                    for (int i = 0; i < _showList.Count; i++)
                    {                       
                        if (_showList[i].sbsType == ShowButtonType.Parents &&
                            _showList[i].bIsUnfolded)
                        {
                            if (strSelectedParentsItem == _showList[i].strItemList)
                            {
                                fItemStartY = iShowItemCount * _ItemHeight + (iShowItemCount + 1) * _SpacerHeight;
                                fItemEndY = (iShowItemCount + 1 + _showList[i].listChildList.Count()) * _ItemHeight +
                                    (iShowItemCount + 1 + _showList[i].listChildList.Count()) * _SpacerHeight;
                                fItemStartX = _SpacerHeight;
                                fItemEndX = this.Width - 2 * _SpacerHeight;
                                fItemWidth = fItemEndX - fItemStartX;
                                fItemHeight = fItemEndY - fItemStartY;

                                _showList[i].iStartY = (int)fItemStartY;
                                _showList[i].iEndY = (int)fItemEndY;
                                iShowItemCount++;

                                if (iMouseY > _showList[i].iStartY &&
                                    iMouseY < _showList[i].iStartY + _ItemHeight)
                                {
                                    if (bIsMouseDown)
                                    {
                                        _showList[i].sbsStatus = ShowButtonStatus.MouseDown;
                                    }
                                    else
                                    {
                                        if (bIsMouseEnter)
                                        {
                                            if (bIsMouseClickCheck)
                                            {
                                                _showList[i].sbsStatus = ShowButtonStatus.Normal;
                                                _showList[i].bIsSelected = false;
                                                _showList[i].bIsUnfolded = false;
                                                strSelectedItem = "";
                                                strSelectedParentsItem = "";
                                                bIsMouseClickCheck = false;
                                                iSelectedItem = -1;
                                            }
                                            else
                                            {
                                                _showList[i].sbsStatus = ShowButtonStatus.Normal;
                                            }
                                        }
                                        else
                                        {
                                            _showList[i].sbsStatus = ShowButtonStatus.Normal;
                                        }                               
                                    }
                                }

                                if (_Shadow)
                                {
                                    gList.FillRectangle(m_BrushShadow2,
                                        fItemStartX + 2,
                                        fItemStartY + 2,
                                        fItemWidth + 1,
                                        fItemHeight + 1);
                                    //gList.FillRectangle(m_BrushShadow1,
                                    //    fItemStartX + 1,
                                    //    fItemStartY + 1,
                                    //    fItemWidth + 1,
                                    //    fItemHeight + 1);
                                }

                                gList.FillRectangle(GetItemBrush(_showList[i].sbsStatus, _showList[i].sbsType),
                                    fItemStartX,
                                    fItemStartY,
                                    fItemWidth,
                                    fItemHeight);

                                gList.DrawString(_showList[i].strItemList,
                                        drawFont,
                                        m_BrushText,
                                        new RectangleF(fItemStartX,
                                            fItemStartY,
                                            fItemWidth,
                                            _ItemHeight),
                                        sf);

                                for (int j = 0; j < _showList[i].listChildList.Count(); j++)
                                {
                                    fItemStartY = iShowItemCount * _ItemHeight + iShowItemCount * _SpacerHeight;
                                    fItemEndY = (iShowItemCount + 1) * _ItemHeight + iShowItemCount * _SpacerHeight;
                                    fItemStartX = _SpacerHeight * 2;
                                    fItemEndX = this.Width - 3 * _SpacerHeight;
                                    fItemWidth = fItemEndX - fItemStartX;
                                    fItemHeight = fItemEndY - fItemStartY;
                                    _showList[i].listChildList[j].iStartY = (int)fItemStartY;
                                    _showList[i].listChildList[j].iEndY = (int)fItemEndY;
                                    iShowItemCount++;

                                    if (iMouseY > _showList[i].listChildList[j].iStartY &&
                                        iMouseY < _showList[i].listChildList[j].iEndY)
                                    {
                                        if (bIsMouseDown)
                                        {
                                            _showList[i].listChildList[j].sbsStatus = ShowButtonStatus.MouseDown;
                                        }
                                        else
                                        {
                                            if (bIsMouseEnter)
                                            {
                                                if (bIsMouseClickCheck)
                                                {
                                                    _showList[i].listChildList[j].sbsStatus = ShowButtonStatus.Selected;
                                                    _showList[i].listChildList[j].bIsSelected = true;
                                                    strSelectedItem = _showList[i].listChildList[j].strItemList;
                                                    bIsMouseClickCheck = false;
                                                    iSelectedItem = iShowItemCount;
                                                }
                                                else
                                                {
                                                    _showList[i].listChildList[j].sbsStatus = ShowButtonStatus.MouseOver;
                                                }
                                            }
                                            else
                                            {
                                                _showList[i].listChildList[j].sbsStatus = ShowButtonStatus.Normal;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (_showList[i].listChildList[j].sbsStatus == ShowButtonStatus.Selected
                                            || _showList[i].listChildList[j].bIsSelected)
                                        {
                                            _showList[i].listChildList[j].sbsStatus = ShowButtonStatus.Normal;
                                            if (strSelectedItem != _showList[i].listChildList[j].strItemList)
                                            {
                                                _showList[i].listChildList[j].bIsSelected = false;
                                            }
                                        }
                                        else
                                        {
                                            _showList[i].listChildList[j].sbsStatus = ShowButtonStatus.Normal;
                                            _showList[i].listChildList[j].bIsSelected = false;
                                        }
                                    }

                                    if (_Shadow)
                                    {
                                        gList.FillRectangle(m_BrushShadow2,
                                            fItemStartX + 2,
                                            fItemStartY + 2,
                                            fItemWidth + 1,
                                            fItemHeight + 1);
                                        //gList.FillRectangle(m_BrushShadow1,
                                        //    fItemStartX + 1,
                                        //    fItemStartY + 1,
                                        //    fItemWidth + 1,
                                        //    fItemHeight + 1);
                                    }

                                    gList.FillRectangle(GetItemBrush(_showList[i].listChildList[j].sbsStatus, _showList[i].listChildList[j].sbsType),
                                    fItemStartX,
                                    fItemStartY,
                                    fItemWidth,
                                    fItemHeight);

                                    gList.DrawString(_showList[i].listChildList[j].strItemList,
                                        drawFont,
                                        m_BrushText,
                                        new RectangleF(fItemStartX,
                                            fItemStartY,
                                            fItemWidth,
                                            fItemHeight),
                                        sf);
                                    if (_showList[i].listChildList[j].sbsStatus == ShowButtonStatus.Selected
                                        || _showList[i].listChildList[j].bIsSelected)
                                    {
                                        gList.DrawRectangle(m_PenSelected,
                                                fItemStartX,
                                                fItemStartY,
                                                fItemWidth,
                                                fItemHeight);
                                    }
                                }
                            }
                            else
                            {
                                _showList[i].sbsStatus = ShowButtonStatus.Normal;
                                _showList[i].bIsUnfolded = false;
                                iShowItemCount++;
                                if (iSelectedItem > iShowItemCount)
                                {
                                    pbCanvas.Location = new Point(pbCanvas.Location.X,
                                        pbCanvas.Location.Y + _showList[i].listChildList.Count() * (_ItemHeight + _SpacerHeight));
                                }
                            }
                        }
                        else
                        {
                            fItemStartY = iShowItemCount * _ItemHeight + (iShowItemCount + 1) * _SpacerHeight;
                            fItemEndY = (iShowItemCount + 1) * _ItemHeight + (iShowItemCount + 1) * _SpacerHeight;
                            fItemStartX = _SpacerHeight;
                            fItemEndX = this.Width - 2 * _SpacerHeight;
                            fItemWidth = fItemEndX - fItemStartX;
                            fItemHeight = fItemEndY - fItemStartY;
                            _showList[i].iStartY = (int)fItemStartY;
                            _showList[i].iEndY = (int)fItemEndY;
                            iShowItemCount++;

                            if (iMouseY > _showList[i].iStartY &&
                                iMouseY < _showList[i].iEndY)
                            {
                                if (bIsMouseDown)
                                {
                                    _showList[i].sbsStatus = ShowButtonStatus.MouseDown;
                                }
                                else
                                {
                                    if (bIsMouseEnter)
                                    {
                                        if (bIsMouseClickCheck)
                                        {
                                            if (_showList[i].sbsType == ShowButtonType.Parents)
                                            {
                                                //_showList[i].sbsStatus = ShowButtonStatus.Unfolded;
                                                _showList[i].bIsSelected = false;
                                                _showList[i].bIsUnfolded = true;
                                                strSelectedItem = "";
                                                strSelectedParentsItem = _showList[i].strItemList;
                                                iSelectedItem = -1;
                                            }
                                            else
                                            {
                                                _showList[i].sbsStatus = ShowButtonStatus.Selected;
                                                _showList[i].bIsSelected = true;
                                                _showList[i].bIsUnfolded = false;
                                                strSelectedItem = _showList[i].strItemList;
                                                strSelectedParentsItem = "";
                                                iSelectedItem = iShowItemCount;
                                            }
                                            bIsMouseClickCheck = false;
                                        }
                                        else
                                        {
                                            _showList[i].sbsStatus = ShowButtonStatus.MouseOver;
                                        }
                                    }
                                    else
                                    {
                                        _showList[i].sbsStatus = ShowButtonStatus.Normal;
                                    }
                                }
                            }
                            else
                            {
                                if (_showList[i].sbsStatus == ShowButtonStatus.Selected
                                    || _showList[i].bIsSelected)
                                {
                                    _showList[i].sbsStatus = ShowButtonStatus.Normal;
                                    if (strSelectedItem != _showList[i].strItemList)
                                    {                                        
                                        _showList[i].bIsSelected = false;
                                    }
                                }
                                else
                                {
                                    _showList[i].sbsStatus = ShowButtonStatus.Normal; 
                                    _showList[i].bIsSelected = false;
                                }
                            }
                            if (_Shadow)
                            {
                                gList.FillRectangle(m_BrushShadow2,
                                    fItemStartX + 2,
                                    fItemStartY + 2,
                                    fItemWidth + 1,
                                    fItemHeight + 1);
                                //gList.FillRectangle(m_BrushShadow1,
                                //    fItemStartX + 1,
                                //    fItemStartY + 1,
                                //    fItemWidth + 1,
                                //    fItemHeight + 1);
                            }
                            gList.FillRectangle(GetItemBrush(_showList[i].sbsStatus, _showList[i].sbsType),
                                fItemStartX,
                                fItemStartY,
                                fItemWidth,
                                fItemHeight);
                            gList.DrawString(_showList[i].strItemList,
                                    drawFont,
                                    m_BrushText,
                                    new RectangleF(fItemStartX,
                                        fItemStartY,
                                        fItemWidth,
                                        fItemHeight),
                                    sf);
                            if (_showList[i].sbsStatus == ShowButtonStatus.Selected
                                || _showList[i].bIsSelected)
                            {
                                gList.DrawRectangle(m_PenSelected,
                                        fItemStartX,
                                        fItemStartY,
                                        fItemWidth,
                                        fItemHeight);
                            }
                        }
                    }

                    if (this.Height < pbCanvas.Height)
                    {
                        int iScrollButtonHeight = (int)(this.Height * 0.05);
                        PointF[] trianglePoint = new PointF[3];
                        switch (iScrollButton)
                        {
                            case 1:
                                gList.FillRectangle(m_BrushScrollButton,
                                    0,
                                    this.Height - iScrollButtonHeight - pbCanvas.Location.Y,
                                    this.Width,
                                    iScrollButtonHeight);
                                trianglePoint[0] = new PointF(this.Width * 0.5f, this.Height - iScrollButtonHeight + iScrollButtonHeight * 0.66f - pbCanvas.Location.Y);
                                trianglePoint[1] = new PointF(this.Width * 0.33f, this.Height - iScrollButtonHeight + iScrollButtonHeight * 0.33f - pbCanvas.Location.Y);
                                trianglePoint[2] = new PointF(this.Width * 0.66f, this.Height - iScrollButtonHeight + iScrollButtonHeight * 0.33f - pbCanvas.Location.Y);
                                gList.FillPolygon(m_BrushScrollButtonArrow, trianglePoint);
                                break;
                            case -1:
                                gList.FillRectangle(m_BrushScrollButton,
                                    0,
                                    -pbCanvas.Location.Y,
                                    this.Width,
                                    iScrollButtonHeight);
                                trianglePoint[0] = new PointF(this.Width * 0.5f, iScrollButtonHeight * 0.33f - pbCanvas.Location.Y);
                                trianglePoint[1] = new PointF(this.Width * 0.33f, iScrollButtonHeight * 0.66f - pbCanvas.Location.Y);
                                trianglePoint[2] = new PointF(this.Width * 0.66f, iScrollButtonHeight * 0.66f - pbCanvas.Location.Y);
                                gList.FillPolygon(m_BrushScrollButtonArrow, trianglePoint);
                                break;
                        }
                    }

                    //gList.DrawString(iSelectedItem.ToString() + "," + strSelectedItem,
                    //            drawFont,
                    //            m_BrushText,
                    //            new RectangleF(0,
                    //                -pbCanvas.Location.Y,
                    //                fItemWidth,
                    //                30),
                    //            sf);

                    pbCanvas.BackgroundImage = bmpList;
                    iMouseWheelMove = (int)(iMouseWheelMove * 0.9);
                    pbCanvas.Refresh();
                }
                catch
                {
                }
            }
        }
        
        private void SlideList_Resize(object sender, EventArgs e)
        {
            pbCanvas.Width = this.Width;
            pbCanvas.BackColor = this.BackColor;
            DrawList(true);
        }

        private void SlideList_Load(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.BackColor = this.Parent.BackColor;
            }
            pbCanvas.Width = this.Width;
            pbCanvas.BackColor = this.BackColor;
            DrawList(true);
        }

        private void SlideList_MouseWheel(object sender, MouseEventArgs e)
        {
            int numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            iMouseWheelMove = (int)(numberOfTextLinesToMove * Math.Min(fItemWidth, fItemHeight) * 0.05);

            if (pbCanvas.Location.Y == 0 && iMouseWheelMove >0)
            {
                iMouseWheelMove = 0;
            }
            else if (pbCanvas.Location.Y == this.Height - pbCanvas.Height && iMouseWheelMove < 0)
            {
                iMouseWheelMove = 0;
            }

            DrawList(true);
        }

        private void SlideList_MouseMove(object sender, MouseEventArgs e)
        {
            if (bIsMouseEnter)
            {
                iMouseY = e.Y;
                if (e.Y + pbCanvas.Location.Y <= this.Height * 0.05)
                {
                    iScrollButton = -1;
                    if (pbCanvas.Location.Y == 0)
                    {
                        iMouseWheelMove = 0;
                    }
                    else
                    {
                        iMouseWheelMove = 12;
                    }
                }
                else if (e.Y + pbCanvas.Location.Y >= this.Height * 0.95)
                {
                    iScrollButton = 1;
                    if (pbCanvas.Location.Y == this.Height - pbCanvas.Height)
                    {
                        iMouseWheelMove = 0;
                    }
                    else
                    {
                        iMouseWheelMove = -12;
                    }
                }
                else
                {
                    iScrollButton = 0;
                }
                DrawList(true);
            }
            else
            {
                iMouseY = -1;
            }
        }

        private void SlideList_MouseEnter(object sender, EventArgs e)
        {
            bIsMouseEnter = true;
            pbCanvas.Focus();
        }

        private void SlideList_MouseLeave(object sender, EventArgs e)
        {
            bIsMouseEnter = false;
            iScrollButton = 0;
            DrawList(true);
        }

        private void SlideList_MouseUp(object sender, MouseEventArgs e)
        {
            iMouseWheelMove = 0;

            iScrollButton = 0;
            if (Math.Abs(e.X - iMouseDownX) > 10
                || Math.Abs(e.Y - iMouseDownY) > 10)
            {
                //bIsMouseClickCheck = false;
            }
            else
            {

            }
            bIsMouseDown = false;
            DrawList(true);
            if (ItemClick != null)
            {
                ItemClick(sender, e);
            }
        }

        private void SlideList_MouseDown(object sender, MouseEventArgs e)
        {
            bIsMouseClickCheck = true;
            bIsMouseDown = true;
            iMouseDownX = e.X;
            iMouseDownY = e.Y;
            DrawList(true);
        }


    }
}

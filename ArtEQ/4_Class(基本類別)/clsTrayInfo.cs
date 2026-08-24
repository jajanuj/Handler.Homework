using ArtControlLib;
using ArtEQ._4_Class_基本類別_;
using ArtTeach;
using System;
using System.Collections.Generic;
using System.Linq;
using static ArtData.clsEnum;

namespace ArtEQ
{
    /// <summary>
    /// Tray 盤資訊類別，記錄格子數量 (Rows x Cols) 與每個位置目前的狀態
    /// </summary>
    public class clsTrayInfo : clsDataFunction
    {
        #region Fields

        public bool bTrayDone;
        public int iColumnID;

        public int iRowID;
        public string sTrayID;

        #endregion

        #region Constructors

        public clsTrayInfo()
        {
            // 依 Recipe 設定 Tray 格數(欄/列)。Math.Max(1, ...) 防呆：
            // 程式啟動早期 Recipe 可能還沒真的載入、GetValueInt 回傳預設 0，
            // SetGridSize(0, 0) 會直接 throw，比原本寫死 2/3 的舊行為風險更高，所以擋下限。
            int iRecipeRows = Math.Max(1, ucParameter.GetValueInt(enuPmtName.Rec_Tray_Row_Number));
            int iRecipeCols = Math.Max(1, ucParameter.GetValueInt(enuPmtName.Rec_Tray_Column_Number));
            SetGridSize(iRecipeRows, iRecipeCols);

            for (int i = 0; i < iRows * iCols; i++)
            {
                Materials.Add(new clsMaterial { IsExist = true });
                AssyRecords.Add(new clsAssyRecord());
            }
        }

        #endregion

        #region Properties

        /// <summary> Tray 盤列數 </summary>
        public int iRows { get; private set; } = 2;

        /// <summary> Tray 盤欄數 </summary>
        public int iCols { get; private set; } = 3;

        /// <summary> 每個位置的狀態 (依 Row 優先排序：index = iRow * iCols + iCol) </summary>
        public TrayItemStatus[] arrItemStatus { get; private set; }

        public List<clsMaterial> Materials { get; set; } = new List<clsMaterial>();
        public List<clsAssyRecord> AssyRecords { get; set; } = new List<clsAssyRecord>();

        public bool bIsExist { get; set; }

        #endregion

        #region Public Methods

        public void SetMaterialType(MaterialType materialType)
        {
            foreach (var material in Materials)
            {
                material.MaterialType = materialType;
            }
        }

        /// <summary> 設定格子數量，會重新初始化所有狀態為 Pending (未執行) </summary>
        public void SetGridSize(int p_iRows, int p_iCols)
        {
            if (p_iRows <= 0 || p_iCols <= 0)
            {
                throw new ArgumentException("Rows / Cols 必須大於 0");
            }

            iRows = p_iRows;
            iCols = p_iCols;
            arrItemStatus = new TrayItemStatus[p_iRows * p_iCols];
        }

        /// <summary> 設定單一格子狀態 </summary>
        public void SetItemStatus(int iIndex, TrayItemStatus status)
        {
            if (iIndex < 0 || iIndex >= arrItemStatus.Length)
            {
                return;
            }

            arrItemStatus[iIndex] = status;
        }

        /// <summary> 一次設定所有格子狀態，陣列長度必須與 iRows*iCols 相同 </summary>
        public void SetAllItemStatus(TrayItemStatus[] arrStatus)
        {
            if (arrStatus == null || arrStatus.Length != arrItemStatus.Length)
            {
                return;
            }

            Array.Copy(arrStatus, arrItemStatus, arrItemStatus.Length);
        }

        /// <summary> 取得單一格子狀態 </summary>
        public TrayItemStatus GetItemStatus(int iIndex)
        {
            if (iIndex < 0 || iIndex >= arrItemStatus.Length)
            {
                return TrayItemStatus.Pending;
            }

            return arrItemStatus[iIndex];
        }

        /// <summary>
        /// 將 Row/Col 轉換成對應的線性 index (Row 優先，index = iRow * iCols + iCol)
        /// </summary>
        public int GetIndexFromRowCol(int iRow, int iCol)
        {
            if (iRow < 0 || iRow >= iRows || iCol < 0 || iCol >= iCols)
            {
                return -1; // 超出範圍，回傳 -1 表示無效
            }

            return iRow * iCols + iCol;
        }

        /// <summary>
        /// 將線性 index 轉換回對應的 Row/Col (Row 優先，與 GetIndexFromRowCol 互為反函式)
        /// </summary>
        /// <param name="iIndex">線性 index</param>
        /// <param name="iRow">輸出：對應的 Row</param>
        /// <param name="iCol">輸出：對應的 Col</param>
        /// <returns>index 是否在有效範圍內；超出範圍時 iRow/iCol 會回傳 -1</returns>
        public bool GetRowColFromIndex(int iIndex, out int iRow, out int iCol)
        {
            if (iIndex < 0 || iIndex >= iRows * iCols)
            {
                iRow = -1;
                iCol = -1;
                return false; // 超出範圍，無效 index
            }

            iRow = iIndex / iCols;
            iCol = iIndex % iCols;
            return true;
        }

        public TrayItemStatus ConvertToItemStatus(AoiResult result) => result == AoiResult.Ok ? TrayItemStatus.OK : TrayItemStatus.NG;

        public void Clear()
        {
            bIsExist = false;
            sTrayID = null;

            iRowID = 0;
            iColumnID = 0;
        }

        public void CopyTo(clsTrayInfo p_Target)
        {
            if (p_Target == null)
            {
                return;
            }

            // 先同步格數(欄/列)，順便重配 arrItemStatus。
            // 沒有這一步的話，來源跟目標建立時間點的 Recipe 值不同時，
            // 目標的 arrItemStatus 長度會跟複製過去的 AssyRecords/Materials 對不上，
            // SetItemStatus() 對超出舊長度的格子會靜默不做事(不噴例外，畫面就是不會更新)。
            p_Target.SetGridSize(iRows, iCols);

            p_Target.bIsExist = bIsExist;
            p_Target.sTrayID = sTrayID;

            p_Target.iRowID = iRowID;
            p_Target.iColumnID = iColumnID;

            p_Target.Materials = Materials.Select(m => m.Clone()).ToList();
            p_Target.AssyRecords = AssyRecords.Select(m => m.Clone()).ToList();
        }

        #endregion
    }
}
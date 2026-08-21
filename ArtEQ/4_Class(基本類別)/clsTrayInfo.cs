using System;
using System.Collections.Generic;
using System.Linq;
using ArtEQ._4_Class_基本類別_;
using ArtTeach;
using static ArtData.clsEnum;

namespace ArtEQ
{
    /// <summary>
    /// Tray 盤資訊類別，記錄格子數量 (Rows x Cols) 與每個位置目前的狀態
    /// </summary>
    public class clsTrayInfo : clsDataFunction
    {
        /// <summary> Tray 盤列數 </summary>
        public int iRows { get; private set; } = 2;

        /// <summary> Tray 盤欄數 </summary>
        public int iCols { get; private set; } = 3;

        /// <summary> 每個位置的狀態 (依 Row 優先排序：index = iRow * iCols + iCol) </summary>
        public TrayItemStatus[] arrItemStatus { get; private set; }

        public List<clsMaterial> Materials { get; set; } = new List<clsMaterial>();
        public List<clsAssyRecord> AssyRecords { get; set; } = new List<clsAssyRecord>();

        public bool bIsExist { get; set; } = false;
        public string sTrayID;

        public int iRowID = 0;
        public int iColumnID = 0;


        public bool bTrayDone;

        public clsTrayInfo()
        {
            SetGridSize(iRows, iCols);

            for (int i = 0; i < iRows * iCols; i++)
            {
                Materials.Add(new clsMaterial() { IsExist = true });
                AssyRecords.Add(new clsAssyRecord());
            }
        }

        public void SetMaterialType(MaterialType materialType)
        {
            foreach (var material in Materials)
            {
                material.MaterialType = materialType;
            }
        }

        /// <summary> 設定格子數量，會重新初始化所有狀態為 Pending (未執行) </summary>
        public void SetGridSize(int iRows, int iCols)
        {
            if (iRows <= 0 || iCols <= 0)
            {
                throw new ArgumentException("Rows / Cols 必須大於 0");
            }

            this.iRows = iRows;
            this.iCols = iCols;
            arrItemStatus = new TrayItemStatus[iRows * iCols];
        }

        /// <summary> 設定單一格子狀態 </summary>
        public void SetItemStatus(int iIndex, TrayItemStatus status)
        {
            if (iIndex < 0 || iIndex >= arrItemStatus.Length) { return; }
            arrItemStatus[iIndex] = status;
        }

        /// <summary> 一次設定所有格子狀態，陣列長度必須與 iRows*iCols 相同 </summary>
        public void SetAllItemStatus(TrayItemStatus[] arrStatus)
        {
            if (arrStatus == null || arrStatus.Length != arrItemStatus.Length) { return; }
            Array.Copy(arrStatus, arrItemStatus, arrItemStatus.Length);
        }

        /// <summary> 取得單一格子狀態 </summary>
        public TrayItemStatus GetItemStatus(int iIndex)
        {
            if (iIndex < 0 || iIndex >= arrItemStatus.Length) { return TrayItemStatus.Pending; }
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

            return (iRow * iCols) + iCol;
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
            if (iIndex < 0 || iIndex >= (iRows * iCols))
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

            //todo: delete
            //if (m_CupInfo == null)
            //    m_CupInfo = new clsCupInfo();

            //m_CupInfo.Clear();

            //if (m_CupInfoList == null || m_CupInfoList.Length != CUP_COUNT)
            //{
            //    m_CupInfoList = new clsCupInfo[CUP_COUNT];
            //    for (int i = 0; i < CUP_COUNT; i++)
            //        m_CupInfoList[i] = new clsCupInfo();
            //}
            //else
            //{
            //    for (int i = 0; i < CUP_COUNT; i++)
            //    {
            //        if (m_CupInfoList[i] == null)
            //            m_CupInfoList[i] = new clsCupInfo();

            //        m_CupInfoList[i].Clear();
            //    }
            //}
        }

        public void CopyTo(clsTrayInfo p_Target)
        {
            if (p_Target == null)
                return;

            p_Target.bIsExist = this.bIsExist;
            p_Target.sTrayID = this.sTrayID;

            p_Target.iRowID = this.iRowID;
            p_Target.iColumnID = this.iColumnID;

            p_Target.Materials = Materials.Select(m => m.Clone()).ToList();
            p_Target.AssyRecords = AssyRecords.Select(m => m.Clone()).ToList();

            //todo: delete
            //if (p_Target.m_CupInfo == null)
            //    p_Target.m_CupInfo = new clsCupInfo();

            //if (this.m_CupInfo != null)
            //    this.m_CupInfo.CopyTo(p_Target.m_CupInfo);

            //if (p_Target.m_CupInfoList == null || p_Target.m_CupInfoList.Length != CUP_COUNT)
            //{
            //    p_Target.m_CupInfoList = new clsCupInfo[CUP_COUNT];
            //    for (int i = 0; i < CUP_COUNT; i++)
            //        p_Target.m_CupInfoList[i] = new clsCupInfo();
            //}

            //if (this.m_CupInfoList != null)
            //{
            //    for (int i = 0; i < CUP_COUNT && i < this.m_CupInfoList.Length; i++)
            //    {
            //        if (this.m_CupInfoList[i] != null)
            //            this.m_CupInfoList[i].CopyTo(p_Target.m_CupInfoList[i]);
            //    }
            //}
        }
    }
}
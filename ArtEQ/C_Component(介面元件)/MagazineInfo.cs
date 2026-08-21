using System;

namespace ArtData
{
    /// <summary>
    /// 料盒資訊類別，記錄每個 Slot 的狀態與目前選取的 Slot Index
    /// </summary>
    public class MagazineInfo
    {
        #region //=====================  區域變數設置 =====================

        /// <summary> 料盒層數 (Slot 總數) </summary>
        public int iSlotCount { get; private set; }

        /// <summary> 每個 Slot 是否有料 (true = 有料, false = 空槽) </summary>
        public bool[] bSlotExist { get; private set; }

        /// <summary> 目前選取的 Slot Index (0-based)，-1 表示未選取 </summary>
        public int iSelectedIndex { get; set; } = -1;

        #endregion

        #region //=====================  必要函式設置 =====================

        public MagazineInfo(int iCount)
        {
            SetSlotCount(iCount);
        }

        /// <summary> 設定料盒層數，會重新初始化狀態陣列 (原本的狀態會被清空) </summary>
        public void SetSlotCount(int iCount)
        {
            if (iCount <= 0)
            {
                throw new ArgumentException("Slot 數量必須大於 0");
            }

            iSlotCount = iCount;
            bSlotExist = new bool[iCount];

            if (iSelectedIndex >= iCount)
            {
                iSelectedIndex = -1;
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        /// <summary> 設定單一 Slot 狀態 </summary>
        public void SetSlotState(int iIndex, bool bExist)
        {
            if (iIndex < 0 || iIndex >= iSlotCount) { return; }
            bSlotExist[iIndex] = bExist;
        }

        /// <summary> 一次設定所有 Slot 狀態，陣列長度必須與 iSlotCount 相同 </summary>
        public void SetAllSlotState(bool[] bStates)
        {
            if (bStates == null || bStates.Length != iSlotCount) { return; }
            Array.Copy(bStates, bSlotExist, iSlotCount);
        }

        /// <summary> 取得單一 Slot 是否有料 </summary>
        public bool GetSlotExist(int iIndex)
        {
            if (iIndex < 0 || iIndex >= iSlotCount) { return false; }
            return bSlotExist[iIndex];
        }

        #endregion
    }
}

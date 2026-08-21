using System.Collections.Generic;
using ArtTeach;

namespace ArtEQ
{
    public class clsMagazineInfo : clsDataFunction
    {
        public bool bIsExist = false;

        /// <summary>
        /// Magazine 每一格 Slot 的 Tray 帳。
        /// Key = Slot 編號，例如 1~5。
        /// Value = 該 Slot 的 Tray 資料。
        /// </summary>
        public Dictionary<int, clsTrayInfo> m_trayInfo;

        public clsMagazineInfo()
        {
            m_trayInfo = new Dictionary<int, clsTrayInfo>();
        }

        /// <summary>
        /// 目前作業的層數索引 (Slot 編號)，從 1 開始。
        /// </summary>
        public int iSelectedIndex { get; set; }

        /// <summary>
        /// 初始化 Slot 帳。
        /// 例如 slotMax = 5，會建立 Slot 1~5。
        /// </summary>
        public void InitialSlot(int slotMax)
        {
            if (m_trayInfo == null)
                m_trayInfo = new Dictionary<int, clsTrayInfo>();

            m_trayInfo.Clear();

            for (int slot = 1; slot <= slotMax; slot++)
            {
                m_trayInfo.Add(slot, new clsTrayInfo());
            }
        }

        public void Clear()
        {
            // 不用 base.Clear();
            // 因為你的 clsDataFunction.Clear<T>() 需要參數。

            bIsExist = false;

            if (m_trayInfo == null)
                m_trayInfo = new Dictionary<int, clsTrayInfo>();

            // 保留 Slot 結構，只清每一格 Tray 帳
            foreach (var item in m_trayInfo)
            {
                if (item.Value != null)
                    item.Value.Clear();
            }
        }

        public new void CopyTo(object p_Target)
        {
            clsMagazineInfo temp = p_Target as clsMagazineInfo;

            if (temp == null)
                return;

            temp.bIsExist = this.bIsExist;

            temp.m_trayInfo = new Dictionary<int, clsTrayInfo>();

            if (this.m_trayInfo == null)
                return;

            foreach (var item in this.m_trayInfo)
            {
                clsTrayInfo tray = new clsTrayInfo();

                if (item.Value != null)
                    item.Value.CopyTo(tray);

                temp.m_trayInfo.Add(item.Key, tray);
            }
        }
    }
}
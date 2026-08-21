namespace ArtEQ.B_Tools
{
    /// <summary>
    /// Tray 盤 Cell 位置計算工具。
    /// </summary>
    public static class clsTrayPositionHelper
    {
        /// <summary>
        /// 計算指定 Row/Col 的 Cell 中心點座標。
        /// </summary>
        /// <param name="iRow">Cell 列索引 (0-based)，往下遞增</param>
        /// <param name="iCol">Cell 欄索引 (0-based)，往右遞增</param>
        /// <param name="dCellWidth">單一 Cell 寬度 (X 方向尺寸)</param>
        /// <param name="dCellLength">單一 Cell 長度 (Y 方向尺寸)</param>
        /// <param name="dCellPitchX">相鄰 Cell 在 X 方向的節距 (中心到中心的實際馬達距離；若 Cell 間有間距，Pitch 會大於 CellWidth)</param>
        /// <param name="dCellPitchY">相鄰 Cell 在 Y 方向的節距 (同上，Y 方向)</param>
        /// <param name="dBaseCellPosX">Row0,Col0 那顆 Cell 左上角的實際教點 X 座標 (唯一有實際教點紀錄的 Cell)</param>
        /// <param name="dBaseCellPosY">Row0,Col0 那顆 Cell 左上角的實際教點 Y 座標</param>
        /// <param name="dPosX">輸出：該 Cell 中心點 X 座標</param>
        /// <param name="dPosY">輸出：該 Cell 中心點 Y 座標</param>
        /// <remarks>
        /// 只有 Row0,Col0 這顆 Cell 有實際教點紀錄 (dBaseCellPosX/Y)，其餘 Cell 的位置都是用 Pitch 從這顆基準點換算出來，
        /// 不是各自獨立教點，所以 Row/Col + Pitch 是必要的計算，不能省略。
        /// 座標系假設：X 隨 Col 增加往右遞增，Y 隨 Row 增加往下遞增；若方向相反，把對應的 Pitch 傳負值即可。
        /// </remarks>
        public static void GetCellCenterPos(
            int iRow,
            int iCol,
            double dCellWidth,
            double dCellLength,
            double dCellPitchX,
            double dCellPitchY,
            double dBaseCellPosX,
            double dBaseCellPosY,
            out double dPosX,
            out double dPosY)
        {
            // Step1：從 Row0,Col0 的教點，用 Pitch 換算出指定 Row/Col 那顆 Cell 的左上角座標
            double dCellTopLeftX = dBaseCellPosX + (iCol * dCellPitchX);
            double dCellTopLeftY = dBaseCellPosY + (iRow * dCellPitchY);

            // Step2：從左上角推算中心點 (加上 Cell 自身尺寸的一半)
            dPosX = dCellTopLeftX + (dCellWidth / 2.0);
            dPosY = dCellTopLeftY + (dCellLength / 2.0);
        }
    }
}

using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtTeach
{
	public class clsProtect
	{
		/// <summary>  保護機制, 不可刪除列舉</summary>
		public enum enuProtect
		{
			Protect_1,
			Protect_2,
			Protect_3,
			Protect_4,
			Protect_5,
		}

		static public bool IsAxisSafe(enuProtect? p_enuProtect, clsEnum.enuAxis? p_enuAxis, bool p_IsRelativeMove) 
		{
			bool bIsPass = false;

			switch (p_enuProtect) 
			{
				default:
					bIsPass = true;
					break;
			}
			return bIsPass;
		}

		//public bool IsAxisSafe(clsEnum.enuAxis p_Axis, bool IsRelativeMove) 
		//{
		//    bool IsSafe = true;
		//    switch (p_Axis) 
		//    {

		//        #endregion

		//        default:
		//            return IsSafe;
		//    }
		//    if (IsSafe == false && IsRelativeMove == true) 
		//    {
		//        formMessageBox.Show("Motion Is Not Safe, But Can Move On Relative Mode With 1(mm).") ;
		//        SetRelativeMove() ;
		//        if (numRelativeDist._Value > 1) 
		//        {
		//            numRelativeDist._Value = 1;
		//        }
		//    }
		//    return IsSafe;
		//}
	}
}

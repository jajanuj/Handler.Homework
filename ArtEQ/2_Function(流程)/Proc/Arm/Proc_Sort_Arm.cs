using ArtData;
using ArtEQ._2_Function_流程_.BaseProc;
using Axis = ArtData.clsEnum.enuAxis;
using Di = ArtData.clsEnum.enuDi;
using Do = ArtData.clsEnum.enuDo;

namespace ArtEQ._2_Function_流程_.Proc
{
    /// <summary>
    /// 組裝手臂
    /// </summary>
    public class Proc_Sort_Arm : BaseArm
    {
        #region ===================== Singleton 設置 =====================

        public static Proc_Sort_Arm GetSingleton() => GetSingletonInstance(() => new Proc_Sort_Arm("Sort_Arm"));

        #endregion

        protected Proc_Sort_Arm(string p_strName) : base(p_strName)
        {
        }

        protected override void BindHardwarePoint()
        {
            m_DI_Vacuum = Di.Sort_Arm_Vacuum;
            m_DO_Vacuum_Activate = Do.Sort_Arm_Vacuum;
            m_DO_Vacuum_Break = Do.Sort_Arm_Air;

            m_Motor_X = Axis.Sort_Arm_X;
            m_Motor_Y = Axis.Sort_Arm_Y;
            m_Motor_Z = Axis.Sort_Arm_Z;

        }

        protected override bool ReadyToPick()
        {
            return GetPickLane().m_Temp_Tray_Info.bIsExist && GetPickLane().ArrivalSignal;
        }

        protected override BaseLane GetPickLane()
        {
            BaseLane pickLane = null;
            switch (PPStation)
            {
                case clsEnum.PPStation.OK:
                    if (m_pickPlace == PickPlace.Pick)
                    {
                        pickLane = Proc_OK_Lane.GetSingleton();
                    }
                    break;
                case clsEnum.PPStation.NG:
                    if (m_pickPlace == PickPlace.Pick)
                    {
                        pickLane = Proc_NG_Lane.GetSingleton();
                    }
                    break;
                default:
                    return null;
            }

            return pickLane;
        }

        protected override BaseLane GetPlaceLane()
        {
            BaseLane placeLane = null;
            switch (PPStation)
            {
                case clsEnum.PPStation.OK:
                    if (m_pickPlace == PickPlace.Place)
                    {
                        placeLane = Proc_OK_Lane.GetSingleton();
                    }
                    break;
                case clsEnum.PPStation.NG:
                    if (m_pickPlace == PickPlace.Place)
                    {
                        placeLane = Proc_NG_Lane.GetSingleton();
                    }
                    break;
                default:
                    return null;
            }

            return placeLane;
        }

        protected override bool ReadyToPlace()
        {
            return GetPlaceLane().m_Temp_Tray_Info.bIsExist && GetPlaceLane().ArrivalSignal;
        }

        protected override void TransferToLane()
        {
            var placeLane = GetPlaceLane();
            if (placeLane == null)
                return;

            var index = placeLane.m_Temp_Tray_Info.GetIndexFromRowCol(m_iPlaceRow, m_iPlaceColumn);

            AssyRecord.CopyTo(placeLane.m_Temp_Tray_Info.AssyRecords[index]);
            var assyRecord = placeLane.m_Temp_Tray_Info.AssyRecords[index];
            var itemStatus = m_Temp_Tray_Info.ConvertToItemStatus(assyRecord.AoiResult);
            AssyRecord.IsExist = false;
            placeLane.m_Temp_Tray_Info.SetItemStatus(index, itemStatus);
        }
    }
}

using ArtEQ._2_Function_流程_.BaseProc;
using static ArtData.clsEnum;
using Axis = ArtData.clsEnum.enuAxis;
using Di = ArtData.clsEnum.enuDi;
using Do = ArtData.clsEnum.enuDo;

namespace ArtEQ._2_Function_流程_.Proc
{
    /// <summary>
    /// 組裝手臂
    /// </summary>
    public class Proc_ASM_Arm : BaseArm
    {
        #region ===================== Singleton 設置 =====================

        public static Proc_ASM_Arm GetSingleton() => GetSingletonInstance(() => new Proc_ASM_Arm("ASM_Arm"));

        #endregion

        protected Proc_ASM_Arm(string p_strName) : base(p_strName)
        {
            PickLane = Proc_HS_Lane.GetSingleton();
            PlaceLane = Proc_ASM_Lane.GetSingleton();
        }
        protected override void BindHardwarePoint()
        {
            m_DI_Vacuum = Di.ASM_Arm_Vacuum;
            m_DO_Vacuum_Activate = Do.ASM_Arm_Vacuum;
            m_DO_Vacuum_Break = Do.ASM_Arm_Air;

            m_Motor_X = Axis.ASM_Arm_X;
            m_Motor_Y = Axis.ASM_Arm_Y;
            m_Motor_Z = Axis.ASM_Arm_Z;

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
                case ArtData.clsEnum.PPStation.IC:
                    if (m_pickPlace == PickPlace.Pick)
                    {
                        pickLane = Proc_ASM_Lane.GetSingleton();
                    }
                    break;
                case ArtData.clsEnum.PPStation.None:
                case ArtData.clsEnum.PPStation.HeatSink:
                    if (m_pickPlace == PickPlace.Pick)
                    {
                        pickLane = Proc_HS_Lane.GetSingleton();
                    }
                    break;
            }

            return pickLane;
        }

        protected override BaseLane GetPlaceLane()
        {
            BaseLane placeLane = null;
            switch (PPStation)
            {
                case ArtData.clsEnum.PPStation.None:
                case ArtData.clsEnum.PPStation.IC:
                    if (m_pickPlace == PickPlace.Place)
                    {
                        placeLane = Proc_ASM_Lane.GetSingleton();
                    }
                    break;
                case ArtData.clsEnum.PPStation.HeatSink:
                    if (m_pickPlace == PickPlace.Place)
                    {
                        placeLane = Proc_HS_Lane.GetSingleton();
                    }
                    break;
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
            placeLane.m_Temp_Tray_Info.AssyRecords[index].IsAssembled = true;
            placeLane.m_Temp_Tray_Info.AssyRecords[index].CurrentStation = WorkStationType.ASM;
            AssyRecord.IsExist = false;
            placeLane.m_Temp_Tray_Info.SetItemStatus(index, TrayItemStatus.Assembly);
        }
    }
}

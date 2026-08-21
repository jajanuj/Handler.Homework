using ArtTeach;
using static ArtData.clsEnum;

namespace ArtEQ._4_Class_基本類別_
{
    public class clsMaterial : clsDataFunction
    {
        public bool IsExist { get; set; }

        public MaterialType MaterialType { get; set; }

        public string SerialNumber { get; set; }

        public void CopyTo(clsMaterial p_Target)
        {
            if (p_Target == null)
                return;

            p_Target.IsExist = IsExist;
            p_Target.MaterialType = MaterialType;
            p_Target.SerialNumber = SerialNumber;

        }

        /// <summary>
        /// 深拷貝：建立一個全新的 clsMaterial 物件，欄位值與目前物件相同，
        /// 但不共用參考，修改其中一個不會影響另一個。
        /// </summary>
        public clsMaterial Clone()
        {
            var target = new clsMaterial();
            CopyTo(target);
            return target;
        }

        public override string ToString()
        {
            return $"MaterialType:{MaterialType}, SerialNumber:{SerialNumber}";
        }
    }
}

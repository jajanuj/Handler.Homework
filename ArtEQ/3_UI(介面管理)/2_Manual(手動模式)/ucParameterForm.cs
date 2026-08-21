using ArtCommonLib;
using ArtControlLib;

namespace ArtEQ._3_UI_介面管理_._2_Manual_手動模式_
{
    public partial class ucParameterForm : ucBaseUserControl
    {
        #region  ===================== Singleton 設置 =====================

        private static ucParameterForm m_singleton;
        private static object s_lock = new object();

        public static ucParameterForm GetSingleton()
        {
            if (m_singleton == null)
            {
                lock (s_lock)
                {
                    m_singleton = new ucParameterForm();
                }

                return m_singleton;
            }

            return m_singleton;
        }
        #endregion

        public ucParameterForm()
        {
            InitializeComponent();
            ucParameter.Add(this);
        }
    }
}

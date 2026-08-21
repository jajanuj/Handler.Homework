using System;

namespace ArtEQ.B_Tools
{
    public static class SingletonHelper<T> where T : class
    {
        private static T m_Instance;
        private static readonly object m_objLock = new object();

        public static T GetOrCreate(Func<T> factory)
        {
            if (m_Instance == null)
            {
                lock (m_objLock)
                {
                    if (m_Instance == null)
                    {
                        m_Instance = factory();
                    }
                }
            }
            return m_Instance;
        }
    }
}

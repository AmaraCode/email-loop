using System;

namespace EmailLoop
{

    /// <summary>
    /// 
    /// </summary>
    public interface IMenu

    {
        /// <summary>
        /// 
        /// </summary>
        void Invoke();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="padded"></param>
        void ShowMenu(bool padded);
    }
}
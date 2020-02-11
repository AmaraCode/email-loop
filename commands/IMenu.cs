using System;


namespace EmailLoop
{
    public interface IMenu

    {
        void Invoke();
        void ShowMenu(bool padded);
    }
}
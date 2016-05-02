using System;

namespace PCLExt.Input
{
    public class KeyPressedEventArgs : EventArgs
    {
        public string Key { get; set; }

        public KeyPressedEventArgs(string key) { Key = key; }
    }

    public interface IInput
    {
        event EventHandler<KeyPressedEventArgs> KeyPressed;

        void ShowKeyboard();

        void HideKeyboard();

        void ConsoleWrite(String message);

        void LogWriteLine(DateTime time, String message);
    }
}
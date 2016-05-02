using System;

namespace PCLExt.Input
{
#if DESKTOP || ANDROID || __IOS__ || MAC
    internal static class LogManager
    {
        private static FileStorage.IFile LogFile { get; }

        static LogManager()
        {
            FileStorage.IFolder folder;
            if(FileStorage.FileSystem.Current.BaseStorage != null)
                folder = FileStorage.FileSystem.Current.BaseStorage;
            else if (FileStorage.FileSystem.Current.LocalStorage != null)
                folder = FileStorage.FileSystem.Current.LocalStorage;
            else
                folder = FileStorage.FileSystem.Current.RoamingStorage;

            LogFile = folder.CreateFolderAsync("Logs", FileStorage.CreationCollisionOption.OpenIfExists).Result
                .CreateFileAsync($"{DateTime.Now:yyyy-MM-dd_HH.mm.ss}.log", FileStorage.CreationCollisionOption.OpenIfExists).Result;
        }

        public static void WriteLine(string message)
        {
            lock (LogFile)
            {
                using (var stream = LogFile.OpenAsync(FileStorage.FileAccess.ReadAndWrite).Result)
                using (var writer = new System.IO.StreamWriter(stream) { AutoFlush = true })
                {
                    writer.BaseStream.Seek(0, System.IO.SeekOrigin.End);
                    writer.WriteLine(message);
                }
            }
        }
    }
#endif

    /// <summary>
    /// 
    /// </summary>
    public static class Input
    {
        private static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException(@"This functionality is not implemented in the portable version of this assembly.
You should reference the PCLExt.Input NuGet package from your main application project in order to reference the platform-specific implementation.");


        public static event EventHandler<KeyPressedEventArgs> KeyPressed;


        public static void ShowKeyboard()
        {
#if DESKTOP || MAC
            return;
#endif

            throw NotImplementedInReferenceAssembly();
        }

        public static void HideKeyboard()
        {
#if DESKTOP || MAC
            return;
#endif

            throw NotImplementedInReferenceAssembly();
        }

        public static void ConsoleWrite(string message)
        {
#if DESKTOP || MAC
            if(ConsoleManager.FastConsole.Enabled)
                ConsoleManager.FastConsole.WriteLine(message);
            else
                Console.WriteLine(message);
            return;
#elif ANDROID || __IOS__
            Console.WriteLine(message);
            return;
#endif

            throw NotImplementedInReferenceAssembly();
        }

        public static void LogWriteLine(DateTime time, string message)
        {
#if DESKTOP || MAC
            var msg_0 = $"[{DateTime.Now:yyyy-MM-dd_HH:mm:ss}]_{message}";
            LogManager.WriteLine(msg_0);

            var msg_1 = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            if (ConsoleManager.FastConsole.Enabled)
                ConsoleManager.FastConsole.WriteLine(msg_1);
            else
                Console.WriteLine(msg_1);
            return;
#elif ANDROID || __IOS__
            var msg_0 = $"[{DateTime.Now:yyyy-MM-dd_HH:mm:ss}]_{message}";
            LogManager.WriteLine(msg_0);

            var msg_1 = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            Console.WriteLine(msg_1);
            return;
#endif

            throw NotImplementedInReferenceAssembly();
        }
    }
}

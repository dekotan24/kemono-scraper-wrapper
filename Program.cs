namespace KemonoScraperGUI;

static class Program
{
    /// <summary>
    /// アプリケーションのメインエントリポイント
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}

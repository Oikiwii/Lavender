namespace logger
{
    public class LoggerClass
    {
        public void MLogg(string log)
        {
            File.AppendAllText("logg.txt", $"[{DateTime.Now}] - {log}\n");
        }
    }
}
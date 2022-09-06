namespace Business.CCS
{
    public class DatabaseLogger : ILogger
    {
        //logger to db
        public void Log()
        {
            Console.WriteLine("Veritabanına loglandı");
        }
    }
}

public class ServerTimeService : IServerTimeService
{
    private readonly TimeZoneInfo _timeZone;

    public ServerTimeService()
    {
        // Configura la zona horaria deseada
        _timeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil");
    }

    public DateTime GetCurrentServerTime()
    {
        // Obtiene la hora actual del servidor y la convierte a la zona horaria configurada
        var serverTime = DateTime.UtcNow;
        return TimeZoneInfo.ConvertTimeFromUtc(serverTime, _timeZone);
    }
}

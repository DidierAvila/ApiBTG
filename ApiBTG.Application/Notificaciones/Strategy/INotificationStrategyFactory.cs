namespace ApiBTG.Application.Notificaciones.Strategy
{
    public interface INotificationStrategyFactory
    {
        INotificationStrategy GetStrategy(string preferencia);
    }
}

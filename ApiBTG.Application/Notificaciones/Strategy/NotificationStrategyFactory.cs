namespace ApiBTG.Application.Notificaciones.Strategy
{
    public class NotificationStrategyFactory : INotificationStrategyFactory
    {
        private readonly Dictionary<string, INotificationStrategy> _strategies;

        public NotificationStrategyFactory(IEnumerable<INotificationStrategy> strategies)
        {
            _strategies = strategies.ToDictionary(s => s.Tipo, StringComparer.OrdinalIgnoreCase);
        }

        public INotificationStrategy GetStrategy(string preferencia)
        {
            if (_strategies.TryGetValue(preferencia, out var strategy))
                return strategy;

            throw new NotSupportedException($"Tipo de notificación '{preferencia}' no soportado");
        }
    }
}

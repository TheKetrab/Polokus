namespace Polokus.App.Utils
{
    public static class ControlExtensions
    {

        public static string DimString(this Control form)
        {
            return $"{form.Width}x{form.Height}";
        }

        public static async Task AnimateProperty(this Control control, Action<float> update, int from, int to, int ms)
        {
            const int timestep = 15; // depends on system tick time, Windows ~15ms
            float step = timestep * (float)(to - from) / (float)ms;

            update(from);

            Func<float, int, bool> relation = from < to
                ? (float f, int i) => f < i
                : (float f, int i) => f > i;

            for (float current = from; relation(current, to); current += step)
            {
                await Task.Delay(timestep);
                update(current);
            }

            update(to);
        }
    }
}

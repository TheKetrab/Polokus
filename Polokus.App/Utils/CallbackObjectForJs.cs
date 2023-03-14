namespace Polokus.App.Utils
{
    public class CallbackObjectForJs
    {
        public void showMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        public void downloadPng(string svg)
        {
            ImageConverter.SavePngFromSvg(svg);
        }
    }
}

using System.Windows.Forms;

namespace System
{
    internal class MouseEventHandler
    {
        private Action<object, MouseEventArgs> addPoint;

        public MouseEventHandler(Action<object, MouseEventArgs> addPoint)
        {
            this.addPoint = addPoint;
        }
    }
}
using System.Windows.Forms;

namespace Common.Extensions
{
    public static class FormExtension
    {
        //Make Thread-Safe Calls to Windows Forms Controls
        /*  1. use lambda
         *  tb.InvokeIfRequired(() =>{
         *    //control
         *  });
         *  
         *  2. use action
         *  tb.InvokeIfRequired(myAction);
         */        
        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}

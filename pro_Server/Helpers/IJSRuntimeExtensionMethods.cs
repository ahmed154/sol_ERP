using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public enum SweetAlertMessageType
        {
            question, warning, error, success, info
        }
        //public static ValueTask DisplayMessage(this IJSRuntime js, string message)
        //{
        //    return js.InvokeVoidAsync("Swal.fire", message);
        //}
        //public static ValueTask DisplayMessage(this IJSRuntime js, string title, string message, SweetAlertMessageType sweetAlertMessageType)
        //{
        //    return js.InvokeVoidAsync("Swal.fire", title, message, sweetAlertMessageType.ToString());
        //}
        public static ValueTask<bool> Confirm(this IJSRuntime js, string title, string message, SweetAlertMessageType sweetAlertMessageType)
        {
            return js.InvokeAsync<bool>("CustomConfirm", title, message, sweetAlertMessageType.ToString());
        }
        public static ValueTask Notfication(this IJSRuntime js, string title, SweetAlertMessageType sweetAlertMessageType, int time)
        {
            return js.InvokeVoidAsync("Notifcation", title, sweetAlertMessageType.ToString(), time);
        }
        public static ValueTask ReloadPage(this IJSRuntime js)
        {
            return js.InvokeVoidAsync("ReloadPage");
        }
        public static ValueTask<object> SaveAs(this IJSRuntime js, string fileName, byte[] data)
            => js.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(data));
  
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
            => js.InvokeAsync<object>(
             "localStorage.setItem",
             key, content
             );
        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<string>(
                "localStorage.getItem",
                key
                );
        public static ValueTask<object> RemoveItem(this IJSRuntime js, string key)
            => js.InvokeAsync<object>(
                "localStorage.removeItem",
                key);
    }
}

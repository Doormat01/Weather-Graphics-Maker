﻿#pragma checksum "C:\Users\conno\Downloads\organized\coding\WIP\weatherGraphicsMaker\WGM Application\AppWindows\SettingsWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "62C1C010FBF81FE5B526CA4B36012B25D34A33197F92AEF193536F933C706D51"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WGM_Application.AppWindows
{
    partial class SettingsWindow : 
        global::Microsoft.UI.Xaml.Window, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2505")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // AppWindows\SettingsWindow.xaml line 17
                {
                    this.PollingCheckbox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.CheckBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.CheckBox)this.PollingCheckbox).Checked += this.Polling_Checked;
                    ((global::Microsoft.UI.Xaml.Controls.CheckBox)this.PollingCheckbox).Unchecked += this.Polling_Unchecked;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }


        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2505")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}


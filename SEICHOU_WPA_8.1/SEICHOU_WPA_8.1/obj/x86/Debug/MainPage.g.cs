﻿

#pragma checksum "C:\Users\jk44\Downloads\SEICHOU_WPA_8.1\SEICHOU_WPA_8.1\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CB7905D1525CFFF2BBC28A5A4FCC5447"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SEICHOU_WPA_8._1
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 14 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).KeyDown += this.userName_TextBox_KeyDown;
                 #line default
                 #line hidden
                #line 14 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.userName_TextBox_Tapped;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 16 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.passwordBox_Tapped;
                 #line default
                 #line hidden
                #line 16 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).KeyDown += this.passwordBox_KeyDown;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 18 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.logIn_Button_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}



﻿

#pragma checksum "C:\Users\barak\Documents\Visual Studio 2015\Projects\SEICHOU_WPA_8.1\SEICHOU_WPA_8.1\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "516FDF26EAF7C126AAA2591D913432D7"
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
                #line 28 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).KeyDown += this.userName_TextBox_KeyDown;
                 #line default
                 #line hidden
                #line 28 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.userName_TextBox_Tapped;
                 #line default
                 #line hidden
                #line 28 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBox)(target)).TextChanged += this.userName_TextBox_TextChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 30 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.passwordBox_Tapped;
                 #line default
                 #line hidden
                #line 30 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).KeyDown += this.passwordBox_KeyDown;
                 #line default
                 #line hidden
                #line 30 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.PasswordBox)(target)).PasswordChanged += this.passwordBox_PasswordChanged;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 32 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.logIn_Button_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}



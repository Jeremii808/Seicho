﻿

#pragma checksum "C:\Users\jk44\Downloads\SEICHOU_WPA_8.1\SEICHOU_WPA_8.1\characterPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3E85F1F91D8D46156170E5F9E00ABEAE"
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
    partial class characterPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 22 "..\..\..\characterPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.createButton_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 23 "..\..\..\characterPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).KeyDown += this.playerName_TextBox_KeyDown;
                 #line default
                 #line hidden
                #line 23 "..\..\..\characterPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.playerName_TextBox_Tapped;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 24 "..\..\..\characterPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.maleButton_Tapped;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 25 "..\..\..\characterPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.femaleButton_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


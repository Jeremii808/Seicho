﻿

#pragma checksum "C:\Users\barak\Documents\Visual Studio 2015\Projects\SEICHOU_WPA_8.1\SEICHOU_WPA_8.1\battleSequence.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C52FF5503D97DA937EC206B4AA20C5FD"
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
    partial class battleSequence : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 53 "..\..\battleSequence.xaml"
                ((global::Windows.UI.Xaml.Media.Animation.Timeline)(target)).Completed += this.DecreasePlayerHealth_Completed;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 44 "..\..\battleSequence.xaml"
                ((global::Windows.UI.Xaml.Media.Animation.Timeline)(target)).Completed += this.DecreaseMonsterHealth_Completed;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 20 "..\..\battleSequence.xaml"
                ((global::Windows.UI.Xaml.Media.Animation.Timeline)(target)).Completed += this.DecraseParticlesTimer_Completed;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 121 "..\..\battleSequence.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.particleButtonLEFT_Tapped;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 126 "..\..\battleSequence.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.particleButtonMIDDLE_Tapped;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 131 "..\..\battleSequence.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.particleButtonRIGHT_Tapped;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 136 "..\..\battleSequence.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.particleButtonRIGHTRIGHT_Tapped;
                 #line default
                 #line hidden
                break;
            case 8:
                #line 148 "..\..\battleSequence.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.resultsButton_Tapped;
                 #line default
                 #line hidden
                break;
            case 9:
                #line 152 "..\..\battleSequence.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.tryagainButton_Click;
                 #line default
                 #line hidden
                break;
            case 10:
                #line 201 "..\..\battleSequence.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.heal_Tapped;
                 #line default
                 #line hidden
                break;
            case 11:
                #line 211 "..\..\battleSequence.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.stoptime_Tapped;
                 #line default
                 #line hidden
                break;
            case 12:
                #line 227 "..\..\battleSequence.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.closefinalmessage_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}



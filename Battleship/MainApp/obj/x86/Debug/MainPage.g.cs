﻿#pragma checksum "E:\Desktop\M1\CSharp\C-Tranchant\Battleship\MainApp\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A1267E17C8D28FC9BBF30428BD888A39"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MainApp
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // MainPage.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).KeyDown += this.KeyPressed;
                }
                break;
            case 3: // MainPage.xaml line 20
                {
                    this.layoutRoot = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 4: // MainPage.xaml line 32
                {
                    this.SeaBorder = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 5: // MainPage.xaml line 70
                {
                    this.textBox1 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6: // MainPage.xaml line 71
                {
                    this.StartButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.StartButton).Click += this.StartButton_Click;
                }
                break;
            case 7: // MainPage.xaml line 72
                {
                    this.image = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 8: // MainPage.xaml line 67
                {
                    this.TextBoxStatus = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 9: // MainPage.xaml line 58
                {
                    this.textBox1_Y = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 10: // MainPage.xaml line 59
                {
                    this.textBoxRemainingShots = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 11: // MainPage.xaml line 60
                {
                    this.Level = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 12: // MainPage.xaml line 54
                {
                    this.textBox1_X = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 13: // MainPage.xaml line 55
                {
                    this.nbTotalImpacts = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 14: // MainPage.xaml line 48
                {
                    this.CarrierBorder = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 15: // MainPage.xaml line 49
                {
                    this.SubmarineBorder = (global::Windows.UI.Xaml.Controls.Border)(target);
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}


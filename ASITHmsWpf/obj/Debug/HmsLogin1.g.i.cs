﻿#pragma checksum "..\..\HmsLogin1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4D3C097921EEC293CD8CA7037A02875C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ASITHmsWpf {
    
    
    /// <summary>
    /// HmsLogin1
    /// </summary>
    public partial class HmsLogin1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\HmsLogin1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image test;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\HmsLogin1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblUser;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\HmsLogin1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblPass;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\HmsLogin1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtPassword;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\HmsLogin1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLogin;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\HmsLogin1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\HmsLogin1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUserName;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\HmsLogin1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblLogin1;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\HmsLogin1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblWelCome;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ASITHmsWpf;component/hmslogin1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\HmsLogin1.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 5 "..\..\HmsLogin1.xaml"
            ((ASITHmsWpf.HmsLogin1)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            
            #line 5 "..\..\HmsLogin1.xaml"
            ((ASITHmsWpf.HmsLogin1)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.test = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.lblUser = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.lblPass = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.txtPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.btnLogin = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\HmsLogin1.xaml"
            this.btnLogin.Click += new System.Windows.RoutedEventHandler(this.btnLogin_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\HmsLogin1.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            
            #line 12 "..\..\HmsLogin1.xaml"
            this.btnCancel.Loaded += new System.Windows.RoutedEventHandler(this.btnCancel_Loaded);
            
            #line default
            #line hidden
            return;
            case 8:
            this.txtUserName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.lblLogin1 = ((System.Windows.Controls.Label)(target));
            
            #line 14 "..\..\HmsLogin1.xaml"
            this.lblLogin1.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.lblLogin1_MouseDown);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lblWelCome = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

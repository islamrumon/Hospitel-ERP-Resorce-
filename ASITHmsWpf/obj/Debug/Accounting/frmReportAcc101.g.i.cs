﻿#pragma checksum "..\..\..\Accounting\frmReportAcc101.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AF6F16BCD6146C8D20DABABF1E65799AAD851C30"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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


namespace ASITHmsWpf.Accounting {
    
    
    /// <summary>
    /// frmReportAcc101
    /// </summary>
    public partial class frmReportAcc101 : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Accounting\frmReportAcc101.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stkpDataGrid;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Accounting\frmReportAcc101.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbltleA03;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Accounting\frmReportAcc101.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbltleA03A;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Accounting\frmReportAcc101.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgOverallA03;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Accounting\frmReportAcc101.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupStyle grp11;
        
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
            System.Uri resourceLocater = new System.Uri("/ASITHmsWpf;component/accounting/frmreportacc101.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Accounting\frmReportAcc101.xaml"
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
            
            #line 7 "..\..\..\Accounting\frmReportAcc101.xaml"
            ((ASITHmsWpf.Accounting.frmReportAcc101)(target)).Initialized += new System.EventHandler(this.UserControl_Initialized);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\Accounting\frmReportAcc101.xaml"
            ((ASITHmsWpf.Accounting.frmReportAcc101)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.stkpDataGrid = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.lbltleA03 = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.lbltleA03A = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.dgOverallA03 = ((System.Windows.Controls.DataGrid)(target));
            
            #line 15 "..\..\..\Accounting\frmReportAcc101.xaml"
            this.dgOverallA03.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dgOverallA03_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\Accounting\frmReportAcc101.xaml"
            this.dgOverallA03.LoadingRow += new System.EventHandler<System.Windows.Controls.DataGridRowEventArgs>(this.dgOverallA03_LoadingRow);
            
            #line default
            #line hidden
            return;
            case 6:
            this.grp11 = ((System.Windows.Controls.GroupStyle)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


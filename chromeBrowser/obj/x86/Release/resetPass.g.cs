﻿#pragma checksum "..\..\..\resetPass.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FB977972BE1B5819E12EF9C18445AD08"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
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


namespace chromeBrowser {
    
    
    /// <summary>
    /// resetPass
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class resetPass : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 148 "..\..\..\resetPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox currentPass;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\resetPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image failedCurrent;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\resetPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image CurrentOk;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\..\resetPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userpass;
        
        #line default
        #line hidden
        
        
        #line 165 "..\..\..\resetPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image failedNewCurrent;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\..\resetPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image passCheck;
        
        #line default
        #line hidden
        
        
        #line 178 "..\..\..\resetPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox passConfirm;
        
        #line default
        #line hidden
        
        
        #line 181 "..\..\..\resetPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image matchPassFail;
        
        #line default
        #line hidden
        
        
        #line 184 "..\..\..\resetPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image matchPassTrue;
        
        #line default
        #line hidden
        
        
        #line 186 "..\..\..\resetPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image usersubmit;
        
        #line default
        #line hidden
        
        
        #line 190 "..\..\..\resetPass.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image retbtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/S;component/resetpass.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\resetPass.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 142 "..\..\..\resetPass.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.currentPass = ((System.Windows.Controls.TextBox)(target));
            
            #line 145 "..\..\..\resetPass.xaml"
            this.currentPass.MouseEnter += new System.Windows.Input.MouseEventHandler(this.currentPass_MouseEnter);
            
            #line default
            #line hidden
            
            #line 146 "..\..\..\resetPass.xaml"
            this.currentPass.KeyDown += new System.Windows.Input.KeyEventHandler(this.currentPass_KeyDown);
            
            #line default
            #line hidden
            
            #line 146 "..\..\..\resetPass.xaml"
            this.currentPass.MouseLeave += new System.Windows.Input.MouseEventHandler(this.currentPass_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 3:
            this.failedCurrent = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.CurrentOk = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.userpass = ((System.Windows.Controls.TextBox)(target));
            
            #line 160 "..\..\..\resetPass.xaml"
            this.userpass.MouseLeave += new System.Windows.Input.MouseEventHandler(this.userpass_MouseLeave);
            
            #line default
            #line hidden
            
            #line 160 "..\..\..\resetPass.xaml"
            this.userpass.MouseEnter += new System.Windows.Input.MouseEventHandler(this.userpass_MouseEnter);
            
            #line default
            #line hidden
            
            #line 162 "..\..\..\resetPass.xaml"
            this.userpass.KeyDown += new System.Windows.Input.KeyEventHandler(this.userpass_KeyDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.failedNewCurrent = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            this.passCheck = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.passConfirm = ((System.Windows.Controls.TextBox)(target));
            
            #line 174 "..\..\..\resetPass.xaml"
            this.passConfirm.MouseEnter += new System.Windows.Input.MouseEventHandler(this.passConfirm_MouseEnter);
            
            #line default
            #line hidden
            
            #line 174 "..\..\..\resetPass.xaml"
            this.passConfirm.MouseLeave += new System.Windows.Input.MouseEventHandler(this.passConfirm_MouseLeave);
            
            #line default
            #line hidden
            
            #line 175 "..\..\..\resetPass.xaml"
            this.passConfirm.KeyUp += new System.Windows.Input.KeyEventHandler(this.passConfirm_KeyUp);
            
            #line default
            #line hidden
            
            #line 177 "..\..\..\resetPass.xaml"
            this.passConfirm.KeyDown += new System.Windows.Input.KeyEventHandler(this.passConfirm_KeyDown);
            
            #line default
            #line hidden
            
            #line 177 "..\..\..\resetPass.xaml"
            this.passConfirm.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.passConfirm_MouseDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.matchPassFail = ((System.Windows.Controls.Image)(target));
            return;
            case 10:
            this.matchPassTrue = ((System.Windows.Controls.Image)(target));
            return;
            case 11:
            this.usersubmit = ((System.Windows.Controls.Image)(target));
            return;
            case 12:
            this.retbtn = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


﻿#pragma checksum "..\..\..\Windows\NewItem.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2B8853BE61313AC415749321BCE7A7A979A11296"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LSNCaseManagerEditor.Windows;
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


namespace LSNCaseManagerEditor.Windows {
    
    
    /// <summary>
    /// NewItem
    /// </summary>
    public partial class NewItem : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Windows\NewItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Windows\NewItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Column1;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Windows\NewItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DataTypeLabel;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Windows\NewItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Column2;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Windows\NewItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DataTypeBox;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Windows\NewItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label IDLabel;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Windows\NewItem.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IDBox;
        
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
            System.Uri resourceLocater = new System.Uri("/LSNCaseManagerEditor;component/windows/newitem.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\NewItem.xaml"
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
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.Column1 = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.DataTypeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.Column2 = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.DataTypeBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 43 "..\..\..\Windows\NewItem.xaml"
            this.DataTypeBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataTypeBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.IDLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.IDBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 54 "..\..\..\Windows\NewItem.xaml"
            this.IDBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.IDBox_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


/****************************** Module Header ******************************\
 Module Name:   AutoCompleteTextBox.cs
 Project:       CSWPFAutoCompleteTextBox
 Copyright (c) Microsoft Corporation.
 
 This example demonstrates how to achieve AutoComplete TextBox in WPF Application.
 
 This source is subject to the Microsoft Public License.
 See http://www.microsoft.com/en-us/openness/resources/licenses.aspx#MPL
 All other rights reserved.
 
 THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
 EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
 WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ASITHmsWpf.UserControls
{

    /// <summary>
    /// Achieve AutoComplete TextBox or ComboBox
    /// </summary>
    public class AutoCompleteTextBox : ComboBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteTextBox"/> class.
        /// </summary>
        /// 
        public class txtItem
        {
            public string itemtxt { get; set; }
            public string itemvalue { get; set; }
        }
        public AutoCompleteTextBox()
        {
            //load and apply style to the ComboBox.
            ResourceDictionary rd = new ResourceDictionary();
            rd.Source = new Uri("/" + this.GetType().Assembly.GetName().Name + ";component/UserControls/AutoCompleteComboBoxStyle.xaml", UriKind.Relative);
            this.Resources = rd;
            //disable default Text Search Function
            this.IsTextSearchEnabled = false;
        }

        /// <summary>
        /// Override OnApplyTemplate method 
        /// Get TextBox control out of Combobox control, and hook up TextChanged event.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //get the textbox control in the ComboBox control
            TextBox textBox = this.Template.FindName("PART_EditableTextBox", this) as TextBox;
            if (textBox != null)
            {
                //disable Autoword selection of the TextBox
                textBox.AutoWordSelection = false;
                //handle TextChanged event to dynamically add Combobox items.

                textBox.TextChanged += new TextChangedEventHandler(textBox_TextChanged);
                textBox.LostFocus += new RoutedEventHandler(textBox_LostFocus);

            }
        }

        //The autosuggestionlist source.
        private ObservableCollection<txtItem> autoSuggestionList = new ObservableCollection<txtItem>();
        private string value1 = "";

        /// <summary>
        /// Gets or sets the auto suggestion list.
        /// </summary>
        /// <value>The auto suggestion list.</value>
        public ObservableCollection<txtItem> AutoSuggestionList
        {
            get { return autoSuggestionList; }
            set { autoSuggestionList = value; }
        }
        public void AddSuggstionItem(string txt, string val)
        {
            AutoSuggestionList.Add(new txtItem() { itemtxt = txt, itemvalue = val });
        }

        public string SearchType { get; set; }
        //public string Value { get; set; }

        public string Value
        {
            get { return value1; }
            set
            {
                this.value1 = value;
                foreach (var s3 in this.autoSuggestionList)
                {
                    if (s3.itemvalue.Trim() == value1)
                    {
                        this.Text = s3.itemtxt.Trim();
                        this.Tag = s3.itemvalue;
                        break;
                    }
                }
                this.ToolTip = this.value1 + " - " + this.Text;
            }
            //set { Value = value; }
        }


        /// <summary>
        /// main logic to generate auto suggestion list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.TextChangedEventArgs"/> 
        /// instance containing the event data.</param>
        void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.AutoWordSelection = false;
            string x = (((TextBox)sender).Tag == null ? "S" : ((TextBox)sender).Tag.ToString());
            // if the word in the textbox is selected, then don't change item collection
            if ((textBox.SelectionStart != 0 || textBox.Text.Length == 0))
            {
                this.Items.Clear();
                //add new filtered items according the current TextBox input
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    foreach (var s in this.autoSuggestionList)
                    {
                        if (this.SearchType == "C")
                        {
                            // if (s.StartsWith(textBox.Text, StringComparison.InvariantCultureIgnoreCase))
                            if (s.itemtxt.ToUpper().Contains(textBox.Text.Trim().ToUpper()))
                            {

                                string unboldpart = s.itemtxt.Substring(textBox.Text.Length);
                                string boldpart = s.itemtxt.Substring(0, textBox.Text.Length);
                                //construct AutoCompleteEntry and add to the ComboBox
                                AutoCompleteEntry entry = new AutoCompleteEntry(s.itemtxt, boldpart, unboldpart);
                                this.Items.Add(entry);
                            }
                        }
                        else
                        {
                            // if (s.Contains(textBox.Text.Trim()))
                            if (s.itemtxt.ToUpper().StartsWith(textBox.Text.ToUpper(), StringComparison.InvariantCultureIgnoreCase))
                            {
                                string unboldpart = s.itemtxt.Substring(textBox.Text.Length);
                                string boldpart = s.itemtxt.Substring(0, textBox.Text.Length);
                                //construct AutoCompleteEntry and add to the ComboBox
                                AutoCompleteEntry entry = new AutoCompleteEntry(s.itemtxt, boldpart, unboldpart);
                                this.Items.Add(entry);
                            }
                        }
                    }
                }
            }
            // open or close dropdown of the ComboBox according to whether there are items in the 
            // fitlered result.
            this.IsDropDownOpen = this.HasItems;

            //avoid auto selection
            textBox.Focus();
            textBox.SelectionStart = textBox.Text.Length;
        }

        void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.value1 = "";
            string s1 = this.Text.Trim();
            foreach (var s3 in this.autoSuggestionList)
            {
                //if (s3.itemtxt.Contains(s1))
                if (s3.itemtxt.Trim() == s1)
                {
                    this.value1 = s3.itemvalue;
                    this.Tag = s3.itemvalue;
                    break;
                }
            }
            this.ToolTip = this.value1 + " - " + this.Text;
        }
    }

    /// <summary>
    /// Extended ComboBox Item
    /// </summary>
    public class AutoCompleteEntry : ComboBoxItem
    {
        private TextBlock tbEntry;

        //text of the item
        private string text;

        /// <summary>
        /// Contrutor of AutoCompleteEntry class
        /// </summary>
        /// <param name="text">All the Text of the item </param>
        /// <param name="bold">The already entered part of the Text</param>
        /// <param name="unbold">The remained part of the Text</param>
        public AutoCompleteEntry(string text, string bold, string unbold)
        {
            this.text = text;
            tbEntry = new TextBlock();
            //highlight the current input Text
            tbEntry.Inlines.Add(new Run
            {
                Text = bold,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.RoyalBlue)
            });
            tbEntry.Inlines.Add(new Run { Text = unbold });
            this.Content = tbEntry;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text
        {
            get { return this.text; }
        }
    }
}

   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      8    2ASITHmsWpf.Commercial.SuperShop.frmEntryShopPOS103   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      -   5        
  � Tag$  SUPER SHOP SALES REPORTS��5	   8      IOUserControl#��[  6X   $��14(�6    � 	Resources 5
       �� 
TargetType �� Property �� Value��%T   'R�       �� 5   
   " R�6   .�5      ��  � ��$
 Bold"�6+   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��65 �6   �� 5      $
��Auto �6   $	��512��6*   .+�5   
   ��  �� Orientation$ Vertical=�6   $	��Top=�6,   $��0,5,0,0q�6D   $��0��6W   $��2��6d   $��60��6x   .+�5      �� $ 
Horizontal=�6   .+�5      �� -   5         ��Name$ stkpDateFrom��5      $ 
Horizontal=�63   .+�5      �� $��Dated From : ��6   $��95��64   $��Right=�6?   $��0,0,0,0q�6b   &   Xceed.Wpf.Toolkit.DateTimePicker  5      -   5          Name$ xctk_dtpFrom��5   *     StdDateTimePickerStyle01#��[ 6@   $
��Bold"�6r   $��22��6�   $	��115��6�   5      �� 5      -   5        $ stkOptBranch��5      $ 
Horizontal=�64   .+�5      �� $��	Branch : ��6   $��70��60   $��Right=�6;   �� 5      -   5        	 ��Name$	 
cmbSBrnCod��5      
 �� SelectedIndex$
 0��63   $	��150��6E   $��24��6�   5      �� 5      -   5        $ stkpItemGroup��5      $ 
Horizontal=�64   .+�5       �� $��Item Group :��6   $��90��63   $��Right=�6>   �� 5!      -   5        $	 cmbItemGroup��5!      $
 0��65   $	��257��6G   $��22��6S   5"      �� 5#      $��Outputs Options :��6   $	��120��64   $��Right=�6@   /   )ASITHmsWpf.UserControls.ComboOutputOption  5$      -   5          Name$ cmbOutputOption��5$   '   $	��205��6@   $��24��6L   �� 5%      -	   5         ��Name$ btnGenerate��5%        StdBtnStyle01#��[ 6.   $��Center=�6U   $��24��6p   $��	_Generate��6|   $
��Bold"�6�   $��75��5&      $��10,0,0,0q�6>   5'      �� 5(      $ 
Horizontal=�6   .+�5)      �� -
   5        $ 
stkpDateTo��5)      $ 
Horizontal=�61   .+�5*      �� -   5         ��Name$ chkAsonDate��5*      $��As On/��64   $��Center=�6E   $��60��6`   $��5,0,0,0q�6k   6|   �� 5+      $
��To :��6   $��30��6+   $��Right=�66     5,      -   5        $ 
xctk_dtpTo��5,   *   #��[ 6>   $
��Bold"�6p   $��22��6�   $	��115��6�   5-      �� 5.      -   5        $ stkOptTerminal��5.      $ 
Horizontal=�65   .+�5/      �� -   5         ��Name$ lblTerminals��5/      $��
Terminal :��62   $��Right=�6G   $��70��6j   �� 50      -   5        $	 cmbTerminals��50      $
 0��65   $	��150��6H   $��22��6T   51      �� 52      -   5        $ stkpItemName��52      $ 
Horizontal=�63   .+�53      �� -   5        $ lblItemNameTitle��53      $��Item Name :��66   $��Right=�6L   $��90��6o   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  54      -   5          Name$ 
AtxtItemID��54   -      
SearchType$ C��6A   $
��Left=�6P   $��22��6r   $	��257��6~   55      �� 56      -   5        $ stkRptOptions��56      $ 
Horizontal=�64   .+�57      �� -   5        $ lblRptOptions��57      $��Report Options :��63   $��Right=�6N   $	��120��6q   �� 58      -   5        $	 cmbRptOptions��58      $
 0��66   $	��290��6I   $��22��6U   .@�59      �� $��Default��6'   $
  NONE��69   5:      5;      5=      5>      �� 5?   
   $��1��6   $ 
Horizontal=�6"   .+�5@      �� .+�5A      R� -   5         R�Name$ 
tvRptTitle��5A      $
��Left=�6/   
������ $	��480��6c   $	��280��6p   $	��Top=�6|   $��14(�6�    � ContextMenu 5B      ��  �� ElementName$ cmbOutputOption�� �� Path$ ContextMenu�6,  5C      �� 5D      $��5,0,0,0q�6   .+�5E      �� $ 
Horizontal=�6   $��45��66   $��Center=�6B   .+�5F      �� -   5        $ lbltle1��5F      ���	#FF862222$��Center=�6D   $
��Bold"�6b   $��Verdana)�6t   �� 5G      -   5        $ lbltle2��5G      ���	#FF862222$��Center=�6D   $
��Bold"�6b   $��Verdana)�6t   5H      �� 5I      -   5        $ stkpDataGrid��5I      5K      5L      5M      5N      5O      
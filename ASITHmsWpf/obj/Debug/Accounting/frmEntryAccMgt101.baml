   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      -    'ASITHmsWpf.Accounting.frmEntryAccMgt101   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4UC%clr-namespace:ASITHmsWpf.UserControls     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      0sys&clr-namespace:System;assembly=mscorlib  5      -   5        $��12(�5	      
  � Tag$4  .Cash & Bank Reconciliation - Entry/Edit Screen��6-      IOUserControl#��[  6f   .��5
      � .+�5   
   �� .+�5      ��  �� Orientation$ 
Horizontal=�6   $��10,0,0,0q�62   .+�5      �� -   5         ��Name$ stkpAccHead��5      $ 
Horizontal=�62   .+�5      �� $��Date :��6   &   Xceed.Wpf.Toolkit.DateTimePicker  5      -   5          Name$ xctk_dtpRecnDate��5   *     StdDateTimePickerStyle01#��[ 6D   $
��Bold"�6v   $��24��6�   $	��110��6�   �� 5      $��Account Head :��6   $��40,0,0,0q�65   $	��100��6G   $
��Bold"�6S   �� 5      -   5         ��Name$ 	cmbAcHead��5      $	��570��62   5      �� 5      $ 
Horizontal=�6   .+�5      �� -   5         ��Name$ btnShow��5        StdBtnStyle01#��[ 6.   $
��Show��6U   $��80��6d   $��20,0,0,0q�6o   6�   �� 5      -   5        $ 	btnUpdate��5      #��[ 60   $��Update��6W   $��80��6h   $��10,0,0,0q�6s   y�5      ��  �� ElementName$ 	stkpRecon�� �� Path$ 
Visibility�6[   �� 5      -   5        $ btnPrint��5      #��[ 6/   $��Print��6V   $��80��6f   $��10,0,0,0q�6q   y�5      �� $ 	stkpRecon��$ 
Visibility�5      5      �� 5      $y�	Collapsed=�6   .+�5      &   System.Windows.Controls.Calendar  -   5          Name$ 
MyCalendar��5      	 �� Today
   SelectedDate#
 Z	 6/   �� 5      ��6   �� $ 
MyCalendar��$ SelectedDate� �� StringFormat$ d��5      �� 5      -	   5        $ 	stkpRecon��5      $y�Visible=�6,   .+�5       �� .+�5!      �� $��14(�6   $
��1200��6*   $
��Bold"�67   ��5"      �� 5#      .+�5$   "   �� .+�5%   &   �� -
   5         ��Name$ 
txtAcHead1��5%   0    �� TextAlignment$ Center=�6D   $��CASH / BANK NAME��6[   $
��1200��6s   5&   #   �� 5'   "   $ 
Horizontal=�6-   $��
10,10,10,0q�6F   .+�5(   &   �� -   5        $ txtBalTitle1��5(   0   $��Balance As Per Book��6F   $	��400��6a   �� 5)   &   -   5        $ 
txtBalAmt1��5)   0   $ Right=�6D   $��999,999,999.99��6Z   $	��120��6p   �� 5*   &   -   5        $ txtBalTitle2��5*   0   $��Balance As Statement��6F   $	��400��6b   $��90,0,0,0q�6n   �� 5+   &   -   5        $ 
txtBalAmt2��5+   0   $ Right=�6D   $��999,999,999.99��6Z   $	��120��6p   5,   #   �� 5-   "   $ 
Horizontal=�6-   $��10,5,0,0q�6F   .+�5.   &   �� -   5        $ txtVouAmt1Title��5.   0   $:��4Add: Cheque issued but not yet presented or cleared:��6I   $	��400��6�   �� 5/   &   -   5        $ 
txtVouAmt1��5/   0   $ Right=�6D   $��999,999,999.99��6Z   $	��120��6p   �� 50   &   -   5        $ txtVouAmt2Title��50   0   $-��'Less: Cheque deposited but not cleared:��6I   $	��400��6x   $��90,0,0,0q�6�   �� 51   &   -   5        $ 
txtVouAmt2��51   0   $ Right=�6D   $��999,999,999.99��6Z   $	��120��6p   52   #   53      55      56      �� 57      $ 
Horizontal=�6   $��
10,5,10,10q�66   .+�58      �� .+�59      &   System.Windows.Controls.DataGrid  -   5          Name$ dgRecon1��59   #      CanUserResizeRows .  
������    
IsReadOnly .  $��0q�6{   $��0,0,0,0q�6�      HeadersVisibility$	 All=�6�      RowHeaderWidth$ 30Q�6�   $	��Top=�5:      $	��450��67   $	��560��6D   $
��Auto=�6P      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $��Hidden=�5;         SelectionUnit$ FullRow=�6S      SelectionMode$ Single=�6k    �� AlternationCount$ 2��6�      AlternatingRowBackground �	#FFEDFFEE � 	Resources 5<      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value*  $System.Windows.Controls.DataGridCell �� Triggers P� Property  P� Value)  #System.Windows.Controls.DataGridRow��%�  '        '  �     '  H    �� 5=   "   "  6(   .�5>   &   ��  � �� �	#FFE2D2CE6C   �� 5?   &    � ��$
 Bold"�6C   �� 5@   &    � ��$ 25��6?   5A   #   �� 5B   "   "  6(    5C   &   P� 5D   *    �@ 
IsSelected  . . �5E   .   ��  � ��
 �����6L   �� 5F   .    � ��
 �   �6L   5G   +   5I   #   �� 5J   "   "  6(   .�5K   &   ��  � ��$ 25��6?   5L   #   5M      !   Columns! 5N      4  .System.Windows.Controls.DataGridTemplateColumn  5O   "   ,  &System.Windows.Controls.DataGridColumn"   Width5	  /System.Windows.Controls.DataGridLengthConverter$" 90	 69   #   Header$# Voucher Date��6D   $   CellTemplate$ 5P   &   �� 5Q   *   .R�5R   .   �� $
��Left=�68   $��Center=�6S   $
��Wrap=�6�   ��6n   �� $ voudat�$ {0: dd-MMM-yyyy }��  6�   �� $ voudat�6�   5S   +   5U   #     5W   "   $" 90	 69   $#   Recon. Date��6D   $ 5X   &   �� 5Y   *   .R�5Z   2     $ dgxctk_dtpRecn1Date��6F   -   5        #��[ 5Z   c   
������ 5
  /Xceed.Wpf.Toolkit.Primitives.DateTimePickerBase% 
  ShowDropDownButton% .  $��90��5[   �   $��0q�5\   2   $��0,3,5,0q�6   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]&   Value& 5[   2   �� $ recndat�' �� Mode$' TwoWay=�( �� UpdateSourceTrigger$( PropertyChanged=�  6�   �� $ vounum�5\      5a   +   5c   #     5e   "   $	" 120	 69   $# 
Voucher No��6E   $ 5f   &   �� 5g   *   .R�5h   .   �� $��Center=�68   $	��Top=�6U   $��NoWrap=�6�   ��6m   �� $ vounum1�  6�   �� $ vounum�6�   5i   +   5k   #     5l   "   $" 90	 69   $# Cheq./Ref. No��6D   $ 5m   &   �� 5n   *   .R�5o   .   �� $
��Left=�68   $	��Top=�6S   $��NoWrap=�6�   ) �� Padding$) 5,0,5,0q�6�   ��6k   �� $ refnum�  6�   �� $ vounum�6�   5p   +   5r   #     5s   "   $	" 120	 69   *   
IsReadOnly* .  +   HeaderTemplate+ 5t   &   �� 5u   *   .R�5v   .   �� $��Amount��68   $ Center=�6F   
������ $	��120��6v   5w   +   $ 5y   &   �� 5z   *   .R�5{   .   �� $ Right=�68   $	��Top=�6N   $��NoWrap=�6�   $) 0,0,5,0q�6�   $	��120��6�   ��6f   �� $
 dram�$  {0: #,##0.00;(#,##0.00); }��  6�   �� $ vounum�6�   5|   +   5~   #   5�      5�      �� 5�      $��50,0,0,0q�6!   .+�5�        -   5        $ dgRecon2��5�   #    .  
������  .  $��0q�6{   $��0,0,0,0q�6�   $	 All=�6�   $ 30Q�6�   $	��Top=�5�      $	��450��67   $	��560��6D   $
��Auto=�6P    .   .   .  $��Hidden=�5�      $ FullRow=�6S   $ Single=�6k   $ 2��6�    �	#FFEDFFEE 5�      ��%�  '        '  �     '  H    �� 5�   "   "  6(   .�5�   &   ��  � �� �	#FFE2D2CE6C   �� 5�   &    � ��$
 Bold"�6C   �� 5�   &    � ��$ 25��6?   5�   #   �� 5�   "   "  6(    5�   &   P� 5�   *    �@ 
IsSelected  . . �5�   .   ��  � ��
 �����6L   �� 5�   .    � ��
 �   �6L   5�   +   5�   #   �� 5�   "   "  6(   .�5�   &   ��  � ��$ 25��6?   5�   #   5�      ! 5�        5�   "   $" 90	 69   $# Voucher Date��6D   $ 5�   &   �� 5�   *   .R�5�   .   �� $
��Left=�68   $��Center=�6S   $
��Wrap=�6�   ��6n   �� $ voudat�$ {0: dd-MMM-yyyy }��  6�   �� $ voudat�6�   5�   +   5�   #     5�   "   $" 90	 69   $#   Recon. Date��6D   $ 5�   &   �� 5�   *   .R�5�   .     $ dgxctk_dtpRecn2Date��6B   -   5        #��[ 5�   _   
������ % .  $��90��5�   �   $��0q�5�   2   $��0,3,5,0q�6~   & 5�   2   �� $ recndat�$' TwoWay=�$( PropertyChanged=�  6�   �� $ vounum�5�   ~   5�   +   5�   #     5�   "   $	" 120	 69   $# 
Voucher No��6E   $ 5�   &   �� 5�   *   .R�5�   .   �� $��Center=�68   $	��Top=�6U   $��NoWrap=�6�   ��6m   �� $ vounum1�  6�   �� $ vounum�6�   5�   +   5�   #     5�   "   $" 90	 69   $# Cheq./Ref. No��6D   $ 5�   &   �� 5�   *   .R�5�   .   �� $
��Left=�68   $	��Top=�6S   $��NoWrap=�6�   $) 5,0,5,0q�6�   ��6k   �� $ refnum�  6�   �� $ vounum�6�   5�   +   5�   #     5�   "   $	" 120	 69   * .  + 5�   &   �� 5�   *   .R�5�   .   �� $��Amount��68   $ Center=�6F   
������ $	��120��6v   5�   +   $ 5�   &   �� 5�   *   .R�5�   .   �� $ Right=�68   $	��Top=�6N   $��NoWrap=�6�   $) 0,0,5,0q�6�   $	��120��6�   ��6f   �� $
 dram�$  {0: #,##0.00;(#,##0.00); }��  6�   �� $ vounum�6�   5�   +   5�   #   5�      5�      5�      �� 5�      $ 
Horizontal=�6   .+�5�      �� $1��+Set Reconciliation Date With Voucher Date :��6   $	��300��6R   $��Right=�6^   $
��Bold"�6�   �� 5�      -   5        $ 
cmbVouType��5�      $	��150��63   , �� SelectedIndex$, 0��6?   $
��Bold"�6R   .@�5�      �� $��Cash Vouchers��6'   $  PVC_RVC��6?   �� 5�      $��Fund Transfer Vouchers��6'   $	  FTV��6H   5�      �� 5�      -   5        $ btnReconDate��5�      #��[ 63   $��Go��6Z   $��60��6g   $��10,0,0,0q�6r   6�   5�      5�      5�      5�      5�      
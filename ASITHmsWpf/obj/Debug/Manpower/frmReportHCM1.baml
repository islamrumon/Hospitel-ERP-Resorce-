   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      '    !ASITHmsWpf.Manpower.frmReportHCM1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      -   5        
  � Tag$,  &HR General & Attnendance Report Screen��5	   8      IOUserControl#��[  6i   $��14(�6�   .��5      � .+�5   
   �� .+�5      ��  �� Orientation$ Vertical=�6   $	��Top=�60   $��90��6H   $��5,0,0,0q�6T   .+�5      �� $ 
Horizontal=�6   .+�5      �� -   5         ��Name$ stkpDateFrom��5   !   $ 
Horizontal=�67   .+�5      �� $��Dated From :��6    $��90��67   $��Right=�6B   &   Xceed.Wpf.Toolkit.DateTimePicker  5      -   5          Name$ xctk_dtpFrom��5   .     StdDateTimePickerStyle01#��[ 6D   $
��Bold"�6v   $��24��6�   $	��150��6�   5      �� 5      $��Branch :��6   $��90��6/   $��Right=�6:   �� 5      -   5         ��Name$ 
cmbSBrnCod��5       �� SelectedIndex$ 0��63   $	��150��6E   6Q   �� 5      $��
Location :��6   $��70��61   $��Right=�6<   �� 5      -   5        $ 
cmbSectCod��5      $ 0��63   $	��240��6F   �� 5      $��Output Option :��6   $	��110��66   $��Right=�6B   /   )ASITHmsWpf.UserControls.ComboOutputOption  5      -   5          Name$ cmbOutputOption��5   +   $	��180��6D   $��24��6P   �� 5      -   5         ��Name$ btnGenerate��5        StdBtnStyle01#��[ 62   $��	_Generate��6Y   $
��Bold"�6m   $��90��6�   $��24��6�   $��10,0,0,0q�6�   5      �� 5      $ 
Horizontal=�6   $��0,0,0,0q�66   .+�5      �� -   5        $ 
stkpDateTo��5   !   $ 
Horizontal=�65   .+�5      �� $
��To :��6    $��90��6/   $��Right=�6:     5      -	   5        $ 
xctk_dtpTo��5   .   #��[ 6B   $
��Bold"�6t   $��24��6�   $	��150��6�   5      �� 5       -
   5        $ stkEmpId��5    !   $ 
Horizontal=�63   .+�5!      �� -   5         ��Name$ lblStaffIdTitle��5!       $��Emp. Name :��69   $��Right=�6O   $��90��6r   $��0,0,0,0q�6}   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  5"      -   5        	  Name$	 
AtxtEmpAll��5"   1   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox
   MouseDoubleClick$!
 AtxtEmpAll_MouseDoubleClick��6�      
SearchType$ C��6E   $	��460��6T   $��24��6`    � ToolTip$= 7Right Click or Double Click to view existing staff list��6l    � ContextMenu 5#      �� 5$   "   -   5         ��Name$ conMenuEmpAll��5$   .   $	��400��6E   $	��400��6R   5&      5'      �� 5(      -   5        $ stkRptOptions��5(   !   $ 
Horizontal=�68   .+�5)      �� -   5        $ lblRptOptions��5)       $��Report Options :��67   $��Right=�6R   $	��110��6u   �� 5*      -   5        $ cmbRptOptions��5*   #   $ 0��6:   $	��280��6L   $��24��6X   $
��Bold"�6d   .@�5+      �� $
��None��6+   $
  NONE��6:   5,      5-      5.      �� 5/      $ 
Horizontal=�6   $��0,0,0,0q�67   .+�50      �� -   5        $ stkpMonthInfo��50   !   $ 
Horizontal=�68   .+�51      �� $��Month :��6    $��90��62   $��Right=�6=   �� 52      -   5        $ cmbInfoMonth��52   #   $��24��69   $	��150��6E   $
��Bold"�6Q   53      �� 54      -   5        $ stkpBank��54   !   $ 
Horizontal=�63   .+�55      �� -   5        $ lblBankTitle��55       $��Bank Name :��66   $��Right=�6L   $��90��6o   $��0,0,0,0q�6z   �� 56      -   5        $ 
cmbBankCod��56   #   $ 0��67   $	��460��6J   $��24��6V   57      �� 58      -   5        $ stkpJoinDate��58   !   $ 
Horizontal=�67   .+�59      �� $��Min Join Date :��6    $	��110��6:   $��Right=�6F   $��0,0,0,0q�6i     5:      -   5        $ xctk_dtpJoin��5:   .   #��[ 6D   $
��Bold"�6v   $��22��6�   $	��140��6�   �� 5;      -   5         ��Name$ chkGeneralRpt��5;   #   $��G��6:   $��Center=�6F   $y�	Collapsed=�6a    d� 	IsChecked$
 TrueY�6x   �� 5<      -   5        $ 
chkAttnRpt��5<   #   $��A��67   $��Center=�6C   $y�	Collapsed=�6^   $
 TrueY�6u   �� 5=      -   5        $ chkPayrollRpt��5=   #   $��P��6:   $��Center=�6F   $y�	Collapsed=�6a   $
 TrueY�6x   5>      5@      5A      �� 5B      $ 
Horizontal=�6   .+�5C      �� $	��245��6   .+�5D      R� -   5         R�Name$ tvRptRtTitle��5D      
������ $	��480��6O   $��Hidden=�6\    5E      ��  �� ElementName$ cmbOutputOption�� �� Path$ ContextMenu�5F   �   5G      �� 5H      .+�5I      �� -   5        $ stkpUpdateAccVoucher��5I   !   $ 
Horizontal=�6?   $
��Left=�6X   $y�Visible=�6s   .+�5J      �� -   5        $ btnUpdateVoucher��5J   !   #��[ 6;   $��Center=�6b   $��24��6}   $��Create PF Journal Voucher��6�   $
��Bold"�5K      $	��200��6+   $��10,0,0,0q�6V   �� 5L      -   5         ��Name$ txtVoucherMsg��5L   "   $��11(�69   $
��Bold"�6G   $��10,0,0,0q�6Y   5M      �� 5N      -   5         ��Name$ 
TabUcGrid1��5N   !   $	��950��65   
������  � 	Resources 5O       �� 
TargetType �� Property �� Value��%h   '��       �� 5P      " ��6$   .�5Q   "   ��  �@J�	AllowDrop . 6>   -    5        5T      5U      5V      �� 5W      $��45��6!   $��Center=�6-   $y�	Collapsed=�6J   .+�5X      �� -!   5        $ lbltle1��5X       ���	#FF862222$��Center=�6H   $
��Bold"�6f   $��Verdana)�6x   �� 5Y      -"   5        $ lbltle2��5Y       ���	#FF862222$��Center=�6H   $
��Bold"�6f   $��Verdana)�6x   $��0,-5,0,0q�6�   5Z      �� 5[      -#   5        $ stkpDataGrid��5[   !   5]      �� 5^      $y�	Collapsed=�6!   .+�5_      &   System.Windows.Controls.DataGrid  -$   5          Name$ dgOverall01��5_   #      AlternatingRowBackground �	#FFE4F2E1   CanUserSortColumns .     
IsReadOnly . $	��910��6�   $��Center=�6�   $	��370��6�   $��Hidden=�6�   $��0,0,0,0q�6�    �� 
GroupStyle 5a      (  "System.Windows.Controls.GroupStyle  5b   "      ContainerStyle 5c   &   �� 5d   *   " ��60   .�5e   .   ��  � �� 5f   2   �� 5g   6     �� 
TargetType"  ��6F   .R�5h   :   9� ! 9� 
IsExpanded! .  
�������$��1q�6n   ��5i   >   �� 5j   B   .+�5k   F   �� $
��Bold"�6P   
����  �$��12(�6t   " �� TextAlignment$" Center=�5l   !   ��68   �� *Name+5m   C   .��5o   >   �� 5p   ;   5q   7   5s   /   5t   +   5v   #   5x      5y      5z      5{      5}      5~      5      
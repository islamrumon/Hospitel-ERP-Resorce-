   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      'clr-namespace:ASITHmsWpf
ASITHmsWpf  5      9    3ASITHmsWpf.Commercial.FoodShop.frmEntryRestauPOS103   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      $localclr-namespace:ASITHmsWpf  5      -   5           IOUserControl#��[  5
   9   $��14(�6`   
  � Tag$#  SALES OPERATION REPORT SCREEN��6o   .��5      � .+�5   
   �� .+�5      ��  �� Orientation$ Vertical=�6   $	��Top=�60   $��90��6H   $��0,10,0,0q�6T   $��0��6h   $��0��6x   $��3��6�   $��2��6�   .+�5      �� $ 
Horizontal=�6   $��10,0,0,0q�66   .+�5      �� -   5         ��Name$ stkOptBranch��5   !   $ 
Horizontal=�67   .+�5      �� $��Branch :��6    $��90��63   $��Right=�6>   �� 5      -   5         ��Name$ 
cmbSBrnCod��5   #    �� SelectedIndex$ 0��67   $	��150��6I   6U   5      �� 5      -   5        $ stkOptLocation��5   !   $ 
Horizontal=�69   $��10,0,0,0q�6S   .+�5      �� $��
Location :��6    $��90��65   $��Right=�6@   �� 5      -   5        $ 
cmbSectCod��5   #   $ 0��67   $	��320��6I   5      �� 5      $ 
Horizontal=�6!   $��10,0,0,0q�6;   .+�5      �� $��Output Options :��6    $	��120��6;   $��Right=�6G   /   )ASITHmsWpf.UserControls.ComboOutputOption  5      -   5          Name$ cmbOutputOption��5   /   $	��200��6H   $��24��6T   �� 5      -   5         ��Name$ btnGenerate��5   !     StdBtnStyle01#��[ 66   $��	_Generate��6]   $
��Bold"�6q   $��90��6�   $��24��6�   $��15,0,0,0q�6�   5      5      �� 5      $ 
Horizontal=�6   $��10,0,0,0q�66   .+�5      �� -   5        $ stkpDateFrom��5   !   $ 
Horizontal=�67   .+�5      �� $��Dated From :��6    $��90��67   $��Right=�6B   &   Xceed.Wpf.Toolkit.DateTimePicker  5       -	   5          Name$ xctk_dtpFrom��5    .     StdDateTimePickerStyle01#��[ 6D   $
��Bold"�6v   $��24��6�   $	��150��6�   5!      �� 5"      $��Item Group :��6   $��90��63   $��Right=�6>   $��10,0,0,0q�6a   �� 5#      -
   5        $ cmbItemGroup��5#      $ 0��65   $	��320��6G   $��24��6S   6_   �� 5$      $ 
Horizontal=�6!   $��10,0,0,0q�6;   .+�5%      �� $��Print Option :��6    $	��120��69   $��Right=�6E   $��0,0,0,0q�6h   �� 5&      -   5        $ cmbPrintOption��5&   #   $��90��6;   $��24��6F   $ 0��6R   .@�5'      �� $��
Small Size��6+   $  SS��6@   �� 5(      $��A4 Size��6+   $	  A4S��6>   5)      �� 5*      -   5        $ stkpShopType��5*   %   $ 
Horizontal=�6;   $y�Hidden=�6T   .+�5+      �� $��Shop Type :��6$   $��90��6:   $��Right=�6E   $��0,0,0,0q�6h   �� 5,      -   5        $ cmbShop��5,   '   $ 1��68   $	��125��6K   $��24��6W   {�.  .@�5-   "   �� $��Medicine Shop��6/   $	  MSI��6G   6S   �� 5.   "   $��	Food Shop��6/   $	  FSI��6C   6O   �� 5/   "   $��
Goods Shop��6/   $	  GSI��6D   6P   50      51      53      54      �� 55      $ 
Horizontal=�6   $��	10, 0,0,0q�66   .+�56      �� -   5         ��Name$ chkAsonDate��56      $��As On/��64   $��Center=�6E   $��60��6`   6k   �� 57      $
��To :��6   $��30��6+   $��Right=�66     58      -   5        $ 
xctk_dtpTo��58   *   #��[ 6>   $
��Bold"�6p   $��24��6�   $	��150��6�   �� 59      $ 
Horizontal=�6!   .+�5:      �� -   5        	 ��Name$	 lblItemIdTitle��5:       $��Item Name :��68   $��Right=�6N   $��90��6q   $��10,0,0,0q�6|   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  5;      -   5        
  Name$
 
AtxtItemId��5;   1      
SearchType$ C��6E   $
��Left=�6T   $��24��6v   $	��320��6�   5<      �� 5=      $ 
Horizontal=�6!   $��10,0,0,0q�6:   .+�5>      �� -   5        $	 lblStaffIdTitle��5>       $��Staff Name :��69   $��Right=�6P   $	��120��6s     5?      -   5        $
 AtxtStaffId��5?   1   $ C��6F   $
��Left=�6U   $��22��6w   $	��305��6�   5@      5A      5B      �� 5C      $ 
Horizontal=�6   .+�5D      �� $	��250��6   .+�5E      R� -   5         R�Name$ tvRptRtTitle��5E      
������ $	��450��6O   $��0,5,0,0q�6\   $��14(�6n   $��Hidden=�6|    � ContextMenu 5F      ��  �� ElementName$ cmbOutputOption�� �� Path$ ContextMenu�5H      5I      �� 5J      .+�5K      �� $��45��6!   $��Center=�6-   $	��830��6J   $��5,10,9,0q�6V   .+�5L      �� -   5        $	 lbltle1��5L       ���	#FF862222$��Center=�6H   $
��Bold"�6f   $��Verdana)�6x   �� 5M      -   5        $	 lbltle2��5M       ���	#FF862222$��Center=�6H   $
��Bold"�6f   $��Verdana)�6x   $��0,-5,0,0q�6�   5N      &   System.Windows.Controls.DataGrid  5O      -   5          Name$ dgOverall01��5O         AlternatingRowBackground �	#FFE4F2E1   CanUserSortColumns .     
IsReadOnly . $	��830��6�   $��Center=�6�   $	��400��6�   $��Hidden=�6�   $��5,0,9,-5q�6�    �� 
GroupStyle 5Q      (  "System.Windows.Controls.GroupStyle  5R         ContainerStyle 5S   "   �� 5T   &    �� 
TargetType" ��6,   .�5U   *   ��  �� Property � �� �� Value 5V   .   �� 5W   2    �� 
TargetType" ��6B   .R�5X   6   9�  9� 
IsExpanded .  
�������$��1q�6j   ��5Y   :   �� 5Z   >   .+�5[   B   �� $
��Bold"�6L   
����  �$��12(�6p    �� TextAlignment$ Center=�5\       ��67   �� *Name+5]   ?   .��5_   :   �� 5`   7   5a   3   5c   +   5d   '   5f      5h      5i      5j      5l      5m      5n      
   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      9    3ASITHmsWpf.Commercial.ParkTicket.frmEntryParkPOS103   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4UC%clr-namespace:ASITHmsWpf.UserControls     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5        
  � Tag$   PARK TICKET/COUPON REPORTS��5	   8   $��14(�6Y   .��5
      � .+�5   
   �� .+�5      ��  �� Orientation$ Vertical=�6   $	��Top=�60   $��90��6H   $��0,10,0,0q�6T   $��0��6h   $��0��6x   $��3��6�   $��2��6�   .+�5      �� $ 
Horizontal=�6   $��5,0,0,0q�66   .+�5      �� -   5         ��Name$ stkOptLocation��5   !   $ 
Horizontal=�69   .+�5      �� $��
Location :��6    $��90��65   $��Right=�6@   �� 5      -   5         ��Name$ 
cmbSectCod��5   #    �� SelectedIndex$ 0��67   $	��150��6I   $��24��6U   5      �� 5      -   5        $ stkOptTerminal��5   !   $ 
Horizontal=�69   $��20,0,0,0q�6S   .+�5      �� $��Terminal ID :��6    $	��100��68   $��Right=�6D   �� 5      -   5        $ cmbTrmID��5   #   $ 0��65   $	��320��6G   $��24��6S   5      �� 5      $ 
Horizontal=�6!   $��15,0,0,0q�6:   .+�5      �� $��Output Options :��6    $	��120��6;   $��Right=�6G   /   )ASITHmsWpf.UserControls.ComboOutputOption  5      -   5          Name$ cmbOutputOption��5   /   $	��230��6H   $��24��6T   �� 5      -   5         ��Name$ btnGenerate��5   !      StdBtnStyle01#��[  66   $��	_Generate��6]   $
��Bold"�6q   $��90��6�   $��24��6�   $��25,0,0,0q�6�   5      5      �� 5      $ 
Horizontal=�6   $��5,0,0,0q�66   .+�5      �� -   5        $ stkpDateFrom��5   !   $ 
Horizontal=�67   .+�5      �� $��Dated From :��6    $��90��67   $��Right=�6B   &   Xceed.Wpf.Toolkit.DateTimePicker  5       -	   5          Name$ xctk_dtpFrom��5    .     StdDateTimePickerStyle01#��[ 6D   $
��Bold"�6v   $��24��6�   $	��150��6�   5!      �� 5"      -
   5        $ stkpOptItemGroup��5"   !   $ 
Horizontal=�6;   .+�5#      �� $��Ticket Group :��6    $	��100��69   $��Right=�6E   $��20,0,0,0q�6h   �� 5$      -   5        $ cmbItemGroup��5$   #   $ 0��69   $��24��6K   $	��320��6W   6c   5%      �� 5&      -   5        $ 
stkOptUser��5&   !   $ 
Horizontal=�65   $��15,0,0,0q�6O   .+�5'      �� $��Apps. User ID :��6    $	��120��6:   $��Right=�6F   *   $ASITHmsWpf.UserControls.AutoComplete  5(      -   5          Name$ autoUserSearch��5(   *   *   $ASITHmsWpf.UserControls.AutoComplete	   PatternChanged$#	 autoUserSearch_PatternChanged��6�   
   MouseDoubleClick$%
 autoUserSearch_MouseDoubleClick��5)   �   $	��350��5(   B    �� SelectedValuePath$ hccode��6N    �� DisplayMemberPath$ userrmrk��6i   $��12(�6�    % !AutoCompleteComboBoxWithoutButton#��[ 5)   .      Delay$	 500��6i   $��24��6u    � ContextMenu 5*      �� 5+   "   $	��400��6.   5-      5/      50      �� 51      $ 
Horizontal=�6   $��5,0,0,0q�66   .+�52      �� -   5         ��Name$ chkAsonDate��52      $��As On/��64   $��Center=�6E   $��60��6`   6k   �� 53      $
��To :��6   $��30��6+   $��Right=�66     54      -   5        $ 
xctk_dtpTo��54   *   #��[ 6>   $
��Bold"�6p   $��24��6�   $	��150��6�   �� 55      -   5        $ stkpOptItem��55   !   $ 
Horizontal=�66   .+�56      �� -   5         ��Name$ lblItemIdTitle��56       $��Ticket Name :��68   $��Right=�6P   $	��100��6s   $��20,0,0,0q�6�     57      -   5        $ autoItemSearch��57   *   $#	 autoItemSearch_PatternChanged��6�   $%
 autoItemSearch_MouseDoubleClick��58   �   $	��320��57   B   $ sircode��6N   $ sirdesc��6j   $��12(�6�   #��[ 58   .   $	 500��6i   $��24��6u    59      �� 5:   "   5<      5=      �� 5>      -   5        $ stkRptOptions��5>   !   $ 
Horizontal=�68   .+�5?      �� -   5        $ lblRptOptions��5?       $��Report Options :��67   $��Right=�6R   $	��120��6u   $��15,0,0,0q�6�   �� 5@      -   5        $ chkDate1��5@   #    d� 	IsChecked$
 TrueY�65   $��Center=�6F   $��60��6a   .��6m   Date6s   �� 5A      -   5        $ 
chkCoupon1��5A   #   $
 TrueY�67   $��Center=�6H   $��70��6c   .��6o   Ticket6w   �� 5B      -   5        $ chkTerminal1��5B   #   $
 TrueY�69   $��Center=�6J   $��80��6e   .��6q   
Terminal6{   �� 5C      -   5        $ chkUser1��5C   #   $
 TrueY�65   $��Center=�6F   $��60��6a   .��6m   User6s   5D      5E      5F      �� 5G      $ 
Horizontal=�6   .+�5H      �� $	��280��6   .+�5I      R� -   5         R�Name$ tvRptRtTitle��5I      
������ $	��480��6O   $	��280��6\   $��0,2,0,0q�6h   $��Hidden=�6z    5J      ��  �� ElementName$ cmbOutputOption�� �� Path$ ContextMenu�5K   �   5L      �� 5M      .+�5N      �� $��45��6!   $��Center=�6-   $��5,2,9,0q�6J   .+�5O      �� -   5        $ lbltle1��5O       ���	#FF862222$��Center=�6H   $
��Bold"�6f   $��Verdana)�6x   �� 5P      -   5        $ lbltle2��5P       ���	#FF862222$��Center=�6H   $
��Bold"�6f   $��Verdana)�6x   $��0,-5,0,0q�6�   5Q      �� 5R      -   5        $ stkpDataGrid��5R   !   5S      &   System.Windows.Controls.DataGrid  5T      -   5          Name$ dgOverall01��5T         AlternatingRowBackground �	#FFE4F2E1   CanUserSortColumns .  $	��900��6t   $	��400��5U      $
��Auto=�6,   $
��Auto=�6\   $��5,0,0,0q�6�   5W      5X      5Y      5Z      5[      5\      
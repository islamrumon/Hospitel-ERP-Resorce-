   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      *    $ASITHmsWpf.Inventory.frmReportStore1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      -   5        
  � Tag$;  5Store (Inventory) Operation Reports View/Print Screen��5         IOUserControl#��[  6[   .��5	      � $	��Top=�6    � RowDefinitions 5
   
   �� 5      $��90 �6   �� 5      $
��500* �6    � ColumnDefinitions 5   
   �� 5      $
��100* �6   �� 5      $
��Auto �6    � Triggers 5   
   :� 5       :� RoutedEvent$ Window.Loaded��6   .[�5      �� .��5      �� .��5      W�  W� From$ 0X�6*   	 W� To$ 1X�63    g� Duration$ 00:00:2D�6:    �� TargetProperty$ Opacity�6M   5      5      5      .+�5   
   �� 	 �� Orientation$	 Vertical=�6   $	��Top=�6,   $	��280��6D   $��0,10,0,0q�6Q   $��0��6e   $��0��6u   $��3��6�   $��2��6�   .+�5      �� $	 
Horizontal=�6   .+�5      �� $��Dated From :��6   $��90��6/   $��Right=�6:   &   Xceed.Wpf.Toolkit.DateTimePicker  5       -   5        
  Name$
 xctk_dtpFrom��5    &     StdDateTimePickerStyle01#��[ 6<   $
��Bold"�6n   $��24��6�   $	��140��6�   �� 5!      $��Location-1 :��6   $��80��6/   $��Right=�6:   �� 5"      -   5         ��Name$ 
cmbSectCod��5"       �� SelectedIndex$ 0��6/   $	��280��6A   �� 5#      $��Location-2 :��6   $��90��6/   $��Right=�6:   $��0,0,0,0q�6]   �� 5$      -   5        $ cmbSectCod2��5$      $ 0��60   $	��275��6C   �� 5%      $��Output :��6   $��60��6+   $��Right=�66   /   )ASITHmsWpf.UserControls.ComboOutputOption  5&      -   5          Name$ cmbOutputOption��5&   '   $	��175��6@   5'      �� 5(      $	 
Horizontal=�6   $��0,2,0,0q�62   .+�5)      �� $��As On /��6   $��Center=�6-   $��60��6H   �� 5*      $
��To :��6   $��30��6'   $��Right=�62     5+      -   5        $
 
xctk_dtpTo��5+   &   #��[ 6:   $
��Bold"�6l   $��24��6~   $	��140��6�   �� 5,      $��Item Group :��6   $��80��6/   $��Right=�6:   �� 5-      -   5        $ cmbItemGroup��5-      $ 0��61   $	��280��6D   6P   �� 5.      -   5         ��Name$ stkpSup��5.      $	 
Horizontal=�6.   .+�5/      �� -	   5         ��Name$ lblSupIdTitle��5/      $��Supply Source :��63   $��Right=�6M   $��90��6p   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  50      -
   5          Name$ 	AtxtSupId��50   -      
SearchType$ C��6@   $
��Left=�6O   $��24��6q   $	��390��6}   51      �� 52      -   5        $ 
stkpStaff1��52      $	 
Horizontal=�61   $y�	Collapsed=�6J   .+�53      �� -   5        $ lblStaffIdTitle1��53      $��Staff-1:��66   $��Right=�6I   $��90��6l     54      -   5        $ AtxtStaffId1��54   -   $ C��6C   $
��Left=�6R   $��24��6t   $	��390��6�   55      �� 56      -   5         ��Name$ btnGenerate��56        StdBtnStyle01#��[ 6.   $��_Generate Report��6U   $	��100��6q   $��20,0,0,0q�6�   57      �� 58      $	 
Horizontal=�6   $��0,0,0,0q�62   .+�59      �� $��Show Type :��6   $��Right=�6.   $��90��6Q   �� 5:      -   5        $ cmbShowOptions��5:      $ 0��63   $	��140��6E   $��24��6Q   .@�5;      �� $��	All Items��6#   $	  ALL��67   �� 5<      $��Current Period��6#   $  CURRALL��6<   �� 5=      $��Current Received/In��6#   $  CURRIN��6A   �� 5>      $��Current Issued/Out��6#   $  CURROUT��6A   �� 5?      $��Previous Period��6#   $  
REVIOUSALL��6=   �� 5@      $��Opening Exist��6#   $  OPENING��6<   �� 5A      $��Closing Exist��6#   $  CLOSING��6<   5B      �� 5C      -   5        $ lblItemIdTitle��5C      $��Item Name :��60   $��Right=�6F   $��80��6i   $��0,0,0,0q�6t     5D      -   5        $ 
AtxtItemId��5D   )   $ C��6=   $
��Left=�6L   $��22��6n   $	��280��6z   �� 5G      -   5        $ lblStaffIdTitle��5G      $��Staff Name :��61   $��Right=�6H   $��90��6k     5H      -   5        $ AtxtStaffId��5H   )   $ C��6>   $
��Left=�6M   $��24��6o   $	��390��6{   5I      �� 5J      $��0q�6   
��� ���6#   5K      R� 5L   
   -   5         R�Name$ 
tvRptTitle��5L      $��0��6'   
������ $��1��6P   $��Hidden=�6]   $��0,2,0,0q�5N   c    � ContextMenu 5M      ��  �� ElementName$ cmbOutputOption�� �� Path$ ContextMenu�5N   c   �� 5O   
   $��1��6   $��1��6%   $��2��62   .+�5P      �� -   5         ��Name$ 
TabUcGrid1��5P      $	��950��6-   
������  � 	Resources 5Q       �� 
TargetType �� Property �� Value��%h   '��       �� 5R      " ��6   .�5S      ��  �@J�	AllowDrop . 66   -   5        5V      5W      5X      5Y      5Z      5[      
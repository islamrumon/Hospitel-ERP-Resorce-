   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      'clr-namespace:ASITHmsWpf
ASITHmsWpf  5      8    2ASITHmsWpf.Commercial.Pharmacy.frmReportPharmaPOS1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      $localclr-namespace:ASITHmsWpf  5      -   5        
  � Tag$*  $Shop Sales Reports View/Print Screen��5	         IOUserControl#��[  6J   .��5
      �  � RowDefinitions 5   
   �� 5      $	��95* �6   �� 5      $
��500* �6    � ColumnDefinitions 5   
   �� 5      $	��77* �6   �� 5      $
��323* �6   .+�5   
   R� -   5         R�Name$ 
tvRptTitle��5      $��0��6'   
������ $��1��6P   5      �� 5   
    �� Orientation$ Vertical=�6   $	��Top=�6,   $	��280��6D   $��0,10,0,0q�6Q   $��0��6e   $��0��6u   $��3��6�   $��2��6�   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��Dated From :��6   $��90��6/   $��Right=�6:   &   Xceed.Wpf.Toolkit.DateTimePicker  5      -   5          Name$ xctk_dtpFrom��5   &     StdDateTimePickerStyle01#��[ 6<   $
��Bold"�6n   $��22��6�   $	��130��6�   �� 5      $��Main Location :��6   $	��100��62   $��Right=�6>   �� 5      -   5         ��Name$ 
cmbSectCod��5       �� SelectedIndex$ 0��6/   $	��230��6A   �� 5      -   5         ��Name$ lblStaffIdTitle��5      $��Staff :��61   $��Right=�6C   $��60��6f   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  5      -   5        	  Name$	 AtxtStaffId��5   )   
   
SearchType$
 C��6>   $
��Left=�6M   $��22��6o   $	��330��6{   �� 5      $��Output Options :��6   $	��100��63   $��Right=�6?   �� 5      -   5        $ cmbOutputOption��5      $	��150��64   $
��Bold"�6@   $ 3��6R   .@�5       �� $��Print preview��6#   $  PP��6;   �� 5!      $��Save as PDF file��6#   $	  PDF��6?   �� 5"      $��Print directly to printer��6#   $  DP��6G   �� 5#      $��Show in screen��6#   $  SS��6<   �� 5$      $!��Export to Excel with format��6#   $  EXCELF��6I   �� 5%      $!��Export to Excel - Data only��6#   $  EXCEL��6I   �� 5&      $ ��Export to Word with format��6#   $
  WORD��6H   5'      5(      �� 5)      $ 
Horizontal=�6   $��0,5,0,0q�62   .+�5*      �� $��As On /��6   $��Center=�6-   �� 5+      $
��To :��6   $��30��6'   $��Right=�62     5,      -   5        $ 
xctk_dtpTo��5,   &   #��[ 6:   $
��Bold"�6l   $��22��6~   $	��130��6�   �� 5-      $��Other Location :��6   $	��100��63   $��Right=�6?   �� 5.      -	   5        $ cmbSectCod2��5.      $ 0��60   $	��230��6C   �� 5/      -
   5        $ lblSupIdTitle��5/      $��Client :��6/   $��Right=�6B   $��60��6e     50      -   5        $	 	AtxtSupId��50   )   $
 C��6<   $
��Left=�6K   $��22��6m   $	��330��6y   51      �� 52      $ 
Horizontal=�6   $��0,5,0,0q�62   .+�53      �� $��Select Shop :��6   $��90��60   $��Right=�6;   �� 54      -   5        $ cmbShop��54      $ 0��6,   $	��130��6?   {�.  .@�55      �� $��Medicine Shop��6#   $	  MSI��6;   6G   �� 56      $��	Food Shop��6#   $	  FSI��67   6C   �� 57      $��Hardware Shop��6#   $	  FSI��6;   6G   58      �� 5:      $��Item Group :��6   $	��100��6/   $��Right=�6;   �� 5;      -   5        $ cmbItemGroup��5;      $ 0��61   $	��230��6D   6P   �� 5<      -   5        $ lblItemIdTitle��5<      $��Item :��60   $��Right=�6A   $��60��6d     5=      -   5        $	 
AtxtItemId��5=   )   $
 C��6=   $
��Left=�6L   $��22��6n   $	��330��6z   �� 5>      $��10��6   $y�Hidden=�6(   �� 5?      -   5         ��Name$ btnGenerate��5?        StdBtnStyle01#��[ 6.   $��	_Generate��6U   $��75��6j   6u   5@      5A      �� 5C   
   $��1��6   $��1��6%   $��2��62   .+�5D      �� $��0q�6   
��� ���6#   �� 5E      $��45��6   $��Center=�6%   $	��950��6B   .+�5F      �� -   5        $ lbltle1��5F      ���	#FF862222$��Center=�6@   $
��Bold"�6^   $��Verdana)�6p   �� 5G      -   5        $ lbltle2��5G      ���	#FF862222$��Center=�6@   $
��Bold"�6^   $��Verdana)�6p   5H      �� 5J      .+�5K      &   System.Windows.Controls.DataGrid  -   5          Name$ dgOverall01��5K         AlternatingRowBackground �	#FFE4F2E1   CanUserSortColumns .     
IsReadOnly . $	��950��6�   $��Center=�6�   $	��450��6�   $��Visible=�6  $��10,0,9,0q�6?   �� 
GroupStyle 5L      (  "System.Windows.Controls.GroupStyle  5M         ContainerStyle 5N      �� 5O   "    �� 
TargetType" ��6(   .�5P   &   ��  �� Property � �� �� Value 5Q   *   �� 5R   .    �� 
TargetType" ��6>   .R�5T   2   9�  9� 
IsExpanded .  
�������$��1q�6f   ��5U   6   �� 5V   :   .+�5W   >   �� $
��Bold"�6H   
����  �$��12(�6l    �� TextAlignment$ Center=�5X   ,   ��6C   �� *Name+5Y   ;   .��5[   6   �� 5\   3   5^   /   5`   '   5a   #   5c      5e      5f      5i      5j      5k      
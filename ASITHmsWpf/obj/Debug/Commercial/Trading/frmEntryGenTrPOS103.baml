   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      'clr-namespace:ASITHmsWpf
ASITHmsWpf  5      7    1ASITHmsWpf.Commercial.Trading.frmEntryGenTrPOS103   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      $localclr-namespace:ASITHmsWpf  5      -   5           IOUserControl#��[  5
   8   $��14(�6_   
  � Tag$  SALES REPORT��6m   .��5      � $	��Top=�6    � Triggers 5   
   :� 5       :� RoutedEvent$ Window.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$ (Grid.Height)�6*    W� From$ 0X�6U   	 W� To$	 650X�6^    g� Duration$ 0:0:1D�6g   W� 5      $ 0X�6*   $ 1X�63   $ 00:00:2D�6:   $ Opacity�6M   5      5      5      .+�5   
   �� .+�5      ��  �� Orientation$ Vertical=�6   $	��Top=�60   $��90��6H   $��0,10,0,0q�6T   $��0��6h   $��0��6x   $��3��6�   $��2��6�   .+�5      �� $ 
Horizontal=�6   $��5,0,0,0q�66   .+�5      �� -   5         ��Name$ stkOptBranch��5   !   $ 
Horizontal=�67   .+�5      �� $��Branch :��6    $��90��63   $��Right=�6>   �� 5      -   5        	 ��Name$	 
cmbSBrnCod��5   #   
 �� SelectedIndex$
 0��67   $	��150��6I   $��24��6U   6a   5      �� 5      -   5        $ stkOptLocation��5   !   $ 
Horizontal=�69   $��20,0,0,0q�6S   .+�5      �� $��
Location :��6    $��90��65   $��Right=�6@   �� 5       -   5        $	 
cmbSectCod��5    #   $
 0��67   $	��320��6I   $��24��6U   5!      �� 5"      $ 
Horizontal=�6!   $y�	Collapsed=�6:   .+�5#      �� $��Shop Type :��6    $��90��66   $��Right=�6A   $��0,0,0,0q�6d   �� 5$      -   5        $	 cmbShop��5$   #   $
 2��64   $	��120��6G   .@�5%      �� $��Medicine Shop��6+   $	  MSI��6C   6O   �� 5&      $��
Foods Shop��6+   $	  FSI��6@   6L   �� 5'      $��
Goods Shop��6+   $	  GSI��6@   6L   5(      5)      �� 5*      $ 
Horizontal=�6!   $��25,0,0,0q�6:   .+�5+      �� $��Output Options :��6    $	��120��6;   $��Right=�6G   /   )ASITHmsWpf.UserControls.ComboOutputOption  5,      -   5          Name$ cmbOutputOption��5,   /   $	��230��6H   $��24��6T   �� 5-      -   5         ��Name$ btnGenerate��5-   !     StdBtnStyle01#��[ 66   $��	_Generate��6]   $
��Bold"�6q   $��90��6�   $��24��6�   $��45,0,0,0q�6�   5.      5/      �� 50      $ 
Horizontal=�6   $��5,0,0,0q�66   .+�51      �� -	   5        $ stkpDateFrom��51   !   $ 
Horizontal=�67   .+�52      �� $��Dated From :��6    $��90��67   $��Right=�6B   &   Xceed.Wpf.Toolkit.DateTimePicker  53      -
   5          Name$ xctk_dtpFrom��53   .     StdDateTimePickerStyle01#��[ 6D   $
��Bold"�6v   $��24��6�   $	��150��6�   54      �� 55      $ 
Horizontal=�6!   .+�56      �� $��Item Group :��6    $��90��67   $��Right=�6B   $��20,0,0,0q�6e   �� 57      -   5        $	 cmbItemGroup��57   #   $
 0��69   $��24��6K   $	��320��6X   6d   58      �� 59      $ 
Horizontal=�6!   .+�5:      �� $��Cust_omer Name :��6    $��Right=�6;   $	��120��6�   $��25,0,0,0q�6�    �� Target 6^   ��  �� ElementName$ autoCustSearch��6�   *   $ASITHmsWpf.UserControls.AutoComplete  5;      -   5          Name$ autoCustSearch��5;   *   *   $ASITHmsWpf.UserControls.AutoComplete   PatternChanged$# autoCustSearch_PatternChanged��6�   $	��365��6B    �� SelectedValuePath$ sircode��6N    �� DisplayMemberPath$ sirdesc1��6j    % !AutoCompleteComboBoxWithoutButton#��[ 5<   .      Delay$	 500��6i   $��24��6u   5=      5>      �� 5?      $ 
Horizontal=�6   $��5,0,0,0q�66   .+�5@      �� -   5         ��Name$ chkAsonDate��5@      $��As On/��64   $��Center=�6E   $��60��6`   6k   �� 5A      $
��To :��6   $��30��6+   $��Right=�66     5B      -   5        $ 
xctk_dtpTo��5B   *   #��[ 6>   $
��Bold"�6p   $��24��6�   $	��150��6�   �� 5C      $ 
Horizontal=�6!   .+�5D      �� -   5         ��Name$ lblItemIdTitle��5D       $��Item Name :��68   $��Right=�6N   $��90��6q   $��20,0,0,0q�6}     5E      -   5        $ autoItemSearch��5E   *   $# autoItemSearch_PatternChanged��6�   $	��320��6B   $ sircode��6N   $ sirdesc��6j   $��12(�6�   #��[ 5F   .   $	 500��6i   $��24��6u   5G      �� 5H      $ 
Horizontal=�6!   .+�5I      �� $��Staff Name :��6    $��Right=�67   $	��120��6�   $��25,0,0,0q�6�    6Z   �� $ autoCustSearch��6�     5J      -   5        $ autoStaffSearch��5J   *   $$ autoStaffSearch_PatternChanged��6�   $	��365��6C   $ sircode��6O   $ sirdesc1��6k   #��[ 5K   .   $	 500��6i   $��24��6u   5L      5M      5N      �� 5O      $ 
Horizontal=�6   .+�5P      �� $	��245��6   .+�5Q      R� -   5         R�Name$ tvRptRtTitle��5Q      
������ $	��480��6O   $��0,2,0,0q�6\   $��Hidden=�6n    � ContextMenu 5R      �� $ cmbOutputOption�� �� Path$ ContextMenu�5S   �   5T      �� 5U      .+�5V      �� $��45��6!   $��Center=�6-   $��5,2,9,0q�6J   .+�5W      �� -   5        $ lbltle1��5W       ���	#FF862222$��Center=�6H   $
��Bold"�6f   $��Verdana)�6x   �� 5X      -   5        $ lbltle2��5X       ���	#FF862222$��Center=�6H   $
��Bold"�6f   $��Verdana)�6x   $��0,-5,0,0q�6�   5Y      �� 5Z      -   5        $ stkpDataGrid��5Z   !   5[      &   System.Windows.Controls.DataGrid  5\      -   5          Name$ dgOverall01��5\         AlternatingRowBackground �	#FFE4F2E1   CanUserSortColumns .     
IsReadOnly . $	��830��6�   $��Center=�5]      $	��400��6<   $��Hidden=�6I   $��5,0,9,-5q�6{   $y�	Collapsed=�6�    �� 
GroupStyle 5^      (  "System.Windows.Controls.GroupStyle  5_         ContainerStyle 5`   "   �� 5a   &     �� 
TargetType"  ��6,   .�5b   *   �� ! �� Property! � ��" �� Value" 5c   .   �� 5d   2   # �� 
TargetType"# ��6B   .R�5e   6   9� $ 9� 
IsExpanded$ .  
�������$��1q�6j   ��5f   :   �� 5g   >   .+�5h   B   �� $
��Bold"�6L   
����  �$��12(�6p   % �� TextAlignment$% Center=�5i       ��67   �� *Name+5j   ?   .��5l   :   �� 5m   7   5n   3   5p   +   5q   '   5s      5u      5v      5w      5x      5y      5z      
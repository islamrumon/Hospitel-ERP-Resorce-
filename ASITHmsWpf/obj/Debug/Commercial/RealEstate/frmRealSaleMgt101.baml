   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      8    2ASITHmsWpf.Commercial.RealEstate.frmRealSaleMgt101   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4UC%clr-namespace:ASITHmsWpf.UserControls     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5        
  � Tag$-  'RECOVERY AGAINST REAL ESTATE UNIT SALES��5   I   $��14(�6w   .��5
      � .+�5   
   � -   5        $��GridDataEntry��5      .+�5      ��  � Triggers 5      :� 5       :� RoutedEvent$ Window.Loaded��6#   .[�5      �� .��5      �� .��5   "   W�  W� From$ 0X�62   	 W� To$ 1X�6;    g� Duration$ 00:00:2D�6B    �� TargetProperty$ Opacity�6U   5      5      5      .+�5      ��  �� Orientation$ 
Horizontal=�6   $��5,5,0,0q�66   $	��135��6G   .+�5      �� -   5         ��Name$ stkpTitleEntry��5   !   $	��985��69   .+�5      �� -   5        $ 
stkpTitle1��5   %   $ 
Horizontal=�69   $��26��6R   .+�5      �� $��MR No :��6(   $	��100��67   	 �� TextAlignment$	 Right=�6C   
 �� Padding$
 0,2,4,0q�6Y   �� 5      -   5         ��Name$ 
txtTransID��5   (   $	��120��6<   
�������$��16(�6j   $
��Bold"�6x   
����  �6�   �� 5      -   5         ��Name$ chkDateBlocked��5   '   |�.  $��Right=�6Q   $��Center=�6t   $
��Bold"�5   '   $��15��69   $��20��6D   $��5,0,0,0q�6P   �� 5      $��Date :��6$   $��50��65   $
��Left=�6@   $��Right=�6[   $
��Bold"�6~   $��22��6�   $��0,2,4,0q�6�   &   Xceed.Wpf.Toolkit.DateTimePicker  5      -   5          Name$ xctk_dtpMrDat��5   2      StdDateTimePickerStyle01#��[  6I   $
��Left=�6{   $
��Bold"�6�   $��24��6�   $	��120��6�   $y�Visible=�5   $   {�69   ��  �� ElementName$ chkDateBlocked�� �� Path$ 	IsChecked�6z   �� 5       -   5        $ stkpCustReference��5    )   $ 
Horizontal=�6D   .+�5!   "   �� $��Cust. Ref. :��6(   $��80��6q   $��Right=�6|   $��5,3,4,0q�6�    �� Target 6?   �� $ autoStaffRefSearch��6�   *   $ASITHmsWpf.UserControls.AutoComplete  5"   "   -	   5          Name$ autoCustRefSearch��5"   2   *   $ASITHmsWpf.UserControls.AutoComplete   PatternChanged$&  autoCustRefSearch_PatternChanged��5#   ,   $	��490��5"   M   $��24��6Y   $��12(�6e    �� SelectedValuePath$ sircode��6s    �� DisplayMemberPath$ sirdesc1��6�    % !AutoCompleteComboBoxWithoutButton#��[ 5#   _      Delay$	 500��6�   5$      5%      �� 5&      
����  �$	��980��66   $��2��6B   �� 5'      -
   5        $ 
stkpTitle2��5'   %   $	��Top=�69   .+�5(      �� $ 
Horizontal=�6)   $��25��6B   .+�5)   "   �� -   5         ��Name$ lblClientName��5)   (   $��Full Name :��6?   $	��100��6U   $��0,3,4,0q�6a   $��Right=�6s   �� 5*   "   -   5        $ txtClientName��5*   ,   $	��555��6C   $
 5,3,0,0q�6O   $
��Bold"�6a   
�������6s   �� 5+   "   $��P_hone :��6(   $��75��6<   $��Right=�6G   $��5,2,4,0q�6j   �� 5,   "   -   5        $ txtContactNo��5,   ,   $	��250��6B   $
 5,0,0,0q�6N   
�������6`   5-      �� 5.      $ 
Horizontal=�6)   $��0,2,0,0q�6B   .+�5/   "   �� $��	Address :��6(   $	��100��6<   $��Right=�6H   $��5,3,4,0q�6k   �� 50   "   -   5        $ txtClientAdd��50   ,   $	��555��6B   $��50��6N   $
 5,3,0,0q�6Z   $
��Wrap=�6l   
�������6�   �� 51   "   .+�52   &   �� $ 
Horizontal=�61   .+�53   *   �� $��Alt.P_hone :��60   $��75��6G   $��Right=�6R   $��5,2,4,0q�6u   �� 54   *   -   5        $ txtRefContactNo��54   4   $	��250��6M   $
 5,0,0,0q�6Y   
�������6k   55   '   �� 56   &   $ 
Horizontal=�61   $��0,5,0,0q�6J   .+�57   *   �� $��E-Mail :��60   $��75��6p   $��Right=�6{   $��5,2,4,0q�6�    6C   �� $ txtContactNo��6�   �� 58   *   -   5        $ txtEMailAdd��58   4   $	��250��6I   $
 5,0,0,0q�6U   
�������6g   59   '   5:   #   5;      �� 5<      $ 
Horizontal=�6)   $��0,2,0,0q�6B   .+�5=   "   �� $��Official Ref. :��6(   $	��100��6t   $��Right=�6�   $��5,3,4,0q�6�    6B   �� $ autoStaffRefSearch��6�   �� 5>   "   -   5        $ txtStaffRef��5>   ,   $	��555��6A   $
 5,3,0,0q�6M   $
��Bold"�6_   
�������6q   �� 5?   "   $��	Hanover :��6(   $��75��6i   $��Right=�6t   $��5,2,4,0q�6�    6<   �� $ txtContactNo��6�   �� 5@   "   -   5        $ txtHandOverDate��5@   ,   $
��Left=�6E   $
��Bold"�6`   $��24��6r   $	��115��6~   
�������6�   5A      5B      5C      �� 5D      -   5        $ 	stkpPrint��5D   !   $ 
Horizontal=�64   $	��Top=�6N   $��-140,110,0,0q�6f   .+�5E      �� -   5        $ chkPrintDirect��5E   #   $��Direct��6;   |�.  $��60��6^   $��0,2,0,0q�6i   $��Center=�6z   �� 5F      -   5         ��Name$ btnPrintTrans��5F   !     StdBtnStyle01#��[ 68   $��75��6_   $��0,0,0,0q�6j   $��_Print��6{   $��25��6�   5G      �� 5I      $ 
Horizontal=�6!   $��2,0,0,0q�6:   .+�5J      �� -   5        $ 	stkpPhoto��5J   %   $	��135��68   $	��115��6E   $	��Top=�6Q   $��0,0,0,0q�6i   .+�5K      �� -   5         ��Name$ imgEmptyPhoto��5K   $   $-��'/ASITHmsWpf;component/Images/NoUser.jpg��6;   $y�Hidden=�6l   $��0��6�   $��0��6�   �� 5L      -   5        $ btnPatPhoto��5L   %   $��0q�6:   |�.  .��5M   "   �� -   5        $ imgPatPhoto��5M   (   $
��Fill=�6=   $	��135��6L   $	��115��6Y   $  000000��6e   5N      5O      �� 5P      $��5,0,0,0q�6%   .+�5Q      �� -   5        $ 
btnNewShow��5Q   %   $
��Left=�69   #��[ 6T   $��75��6{   $
��Bold"�6�   $	��_Ok��6�   $  Ok��6�   6�   �� 5R      -   5        $ btnTransList��5R   %   
������ 
����  �$��0q�6f   $��75��6z   $��0,20,0,0q�6�   $��Trans. List��5S   %   |�.  6V   �� 5T      -   5        $ btnUpdateTrans��5T   %   #��[ 6=   $��75��6d   $��0,20,0,0q�6o   $��_Save��6�   $
  Save��6�   6�   5U      5V      5W      5X      5Y      5Z      5[      
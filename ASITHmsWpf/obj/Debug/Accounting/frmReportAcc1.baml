   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      )    #ASITHmsWpf.Accounting.frmReportAcc1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      -   5        
  � Tag$6  0Accounting Information Reports View/Print Screen��5         IOUserControl#��[  6V    � 	Resources 5	       �� 
TargetType �� Property �� Value��%T   'R�       �� 5
   
   " R�6   .�5      ��  � ��$
 Bold"�6+   5      5      .��5      �  � Triggers 5   
   :� 5       :� RoutedEvent$ Window.Loaded��6   .[�5      �� .��5      �� .��5      W�  W� From$ 1X�6*   	 W� To$ 0X�63   	 g� Duration$	 	00:00:0.5D�6:   
 �� TargetProperty$
 Opacity�6O    g� AutoReverse . 6s   5      5      5       � RowDefinitions 5   
   �� 5      $
��110* �6   �� 5      $
��500* �6    � ColumnDefinitions 5   
   �� 5      $
��280* �6   �� 5       $
��920* �6   .+�5"   
   R� -   5         R�Name$ 
tvRptTitle��5"      $��0��6'   $
��Left=�67   
������ $��1��6k   $	��280��6y   $	��Top=�6�   $��Hidden=�5$       � ContextMenu 5#      ��  �� ElementName$ cmbOutputOption�� �� Path$ ContextMenu�5%   F   �� 5&   
    �� Orientation$ Vertical=�6   $	��Top=�6,   $��0,10,0,0q�6D   $��0��6X   $��0��6h   $��2��6u   $	��104��6�   .+�5'      �� $ 
Horizontal=�6   .+�5(      �� -   5         ��Name$ stkOptBranch��5(      $ 
Horizontal=�63   .+�5)      �� $��Branch Name :��6   $��90��64   $��Right=�6?   �� 5*      -   5         ��Name$ 
cmbSBrnCod��5*       �� SelectedIndex$ 0��63   $	��190��6E   6Q   5+      �� 5,      -   5        $ stkOptLocation��5,      $ 
Horizontal=�65   .+�5-      �� $��
Location :��6   $��75��61   $��Right=�6<   �� 5.      -   5        $ 
cmbSectCod��5.      $ 0��63   $	��455��6E   6Q   5/      �� 50      $ 
Horizontal=�6   $��0,0,0,0q�66   .+�51      �� $��Output Options :��6   $	��100��67   $��Right=�6C   /   )ASITHmsWpf.UserControls.ComboOutputOption  52      -   5          Name$ cmbOutputOption��52   +   $	��195��6D   �� 53      -   5         ��Name$ btnGenerate��53        StdBtnStyle01#��[ 62   $��Center=�6Y   $��24��6t   $��	_Generate��6�   $
��Bold"�6�   $��75��6�   $��10,0,0,0q�6�   54      55      �� 56      $ 
Horizontal=�6   .+�57      �� $ 
Horizontal=�6   .+�58      �� $��Dated From :��6   $��90��63   $��Right=�6>   &   Xceed.Wpf.Toolkit.DateTimePicker  59      -	   5          Name$ xctk_dtpFrom��59   *     StdDateTimePickerStyle01#��[ 6@   $
��Bold"�6r   $��22��6�   $	��120��6�   5:      �� 5;      -
   5        $ stkOptActCode��5;      $ 
Horizontal=�64   .+�5<      �� -   5         ��Name$ lblActCodeTitle��5<      $��_A/c Head :��65   $��Right=�6K   $��70��6n    �� Target 6y   �� $ cmbActGroup���� 5=      -   5        $ cmbActGroup��5=      $ 0��64   $��75��6F   $��22��6Q   6]   *   $ASITHmsWpf.UserControls.AutoComplete  5>      -   5          Name$ AutoCompleteActCode��5>   &   *   $ASITHmsWpf.UserControls.AutoComplete   PatternChanged$( "AutoCompleteActCode_PatternChanged��5?   f   $	��455��5>   C    % !AutoCompleteComboBoxWithoutButton#��[ 6O      Delay$	 500��6�   $��22��6�    �� SelectedValuePath$ actcode��5?   -    �� DisplayMemberPath$ actdesc1��6I   6f   5@      �� 5A      -   5        $ stkOptTB��5A      $ 
Horizontal=�6/   $��24��6H   $��5,2,0,0q�6T   .+�5B      �� -   5          ��Name$  	chkLevel1��5B      ! d� 	IsChecked$
! TrueY�62   $��Center=�6C   $��60��6^   .��6j   	Level-16s   �� 5C      -   5        $  	chkLevel2��5C      $
! TrueY�62   $��Center=�6C   $��60��6^   .��6j   	Level-26s   �� 5D      -   5        $  	chkLevel3��5D      $
! TrueY�62   $��Center=�6C   $��60��6^   .��6j   	Level-36s   �� 5E      -   5        $  	chkLevel4��5E      $
! TrueY�62   $��Center=�6C   $��60��6^   .��6j   	Level-46s   �� 5F      -   5        $ 
stkOptMore��5F   !   $ 
Horizontal=�65   $y�Visible=�6N   $��24��6c   $��Bottom=�6o   .+�5G      �� $��More :��6    $��Right=�61   $��45��6T   $��0,-2,0,0q�6_   �� 5H      -   5        $ cmbSubLevel��5H   #   $ 0��68   $��90��6J   $��22��6U   .@�5I      �� $
��None��6+   $
  NONE��6:   �� 5J      $��Branch��6+   $  BRANCH��6<   �� 5K      $��Top Location��6+   $  TOPLOCATION��6B   �� 5L      $��Location��6+   $  LOCATION��6>   �� 5M      $��Level-1��6+   $  	SUBLEVEL1��6=   �� 5N      $��Level-2��6+   $  	SUBLEVEL2��6=   �� 5O      $��Level-3��6+   $  	SUBLEVEL3��6=   �� 5P      $��Level-4��6+   $  	SUBLEVEL4��6=   �� 5Q      $��Level-5��6+   $  	SUBLEVEL5��6=   5R      5S      5T      5U      �� 5V      $ 
Horizontal=�6   .+�5W      �� $ 
Horizontal=�6   .+�5X      �� -   5        $  chkAsonDate��5X      $��As On/��64   $��Center=�6E   $��55��6`   $��5,0,0,0q�6k   �� 5Y      $
��To :��6   $��30��6+   $��Right=�66     5Z      -   5        $ 
xctk_dtpTo��5Z   *   #��[ 6>   $
��Bold"�6p   $��22��6�   $	��120��6�   5[      �� 5\      -   5        $ stkOptSirCode��5\      $ 
Horizontal=�64   $��0,0,0,0q�6M   .+�5]      �� -   5        $ lblSirCodeTitle��5]      $��_Sub. Head :��65   $��Right=�6L   $��70��6o    6z   �� $ cmbSirGroup���� 5^      -   5        $ cmbSirGroup��5^      $ 0��64   $��75��6F   $��22��6Q   6]   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  5_      -   5        "  Name$" AtxtSirCode��5_   -   #   
SearchType$# C��6B   $
��Left=�6Q   $��22��6s   $	��400��6   {�.  $y�	Collapsed=�6�     5`      -   5        $ AutoCompleteSirCode��5`   &   $( "AutoCompleteSirCode_PatternChanged��5a   f   $	��455��5`   C   #��[ 6O   $	 500��6�   $��22��6�   $ sircode��5a   -   $ sirdesc1��6I   6f   5c      �� 5d      -   5        $ stkRptOptions��5d      $ 
Horizontal=�64   .+�5e      �� -   5        $ lblRptOptions��5e      $��Report Options :��63   $��Right=�6N   $	��100��6q   �� 5f      -   5        $ cmbRptOptions��5f      $ 0��66   $	��280��6I   $��22��6U   .@�5g      �� $��Default��6'   $
  NONE��69   5h      5i      5k      �� 5l      $ 
Horizontal=�6   .+�5m      �� -   5        $ stkOptSchedule��5m      $ 
Horizontal=�66   $��0,0,0,0q�6O   .+�5n      �� -    5        $ lblPeriodTitle��5n      $��Report Period :��64   $��Right=�6N   $��90��6q   �� 5o      -!   5        $ 	cmbPeriod��5o      $ 0��62   $	��190��6D   $��22��6P   .@�5p      �� $!��Opening, Current & Closing ��6'   $  01OCCLB��6Q   �� 5q      $��Current Period Only��6'   $  02CPSO��6E   �� 5r      $��6 Months Summary��6'   $  03MWS06��6B   �� 5s      $��12 Months Summary��6'   $  04MWS12��6C   5t      5u      �� 5v      -"   5        $ stkOptDrCrColumns��5v      $ 
Horizontal=�69   $��0,0,0,0q�6R   .+�5w      �� -#   5        $ lblDrCrTitle��5w      $��	Columns :��6.   $��Right=�6B   $��70��6e   �� 5x      -$   5        $ cmbDrCr��5x      $ 0��60   $	��200��6B   $��22��6N   .@�5y      �� $ ��Both Debit & Credit Amount��6'   $
  DRCR��6P   �� 5z      $��Debit Amount Only��6'   $
  DRO1��6C   �� 5{      $��Credit Amount Only��6'   $
  CRO1��6D   �� 5|      $��Debit Balance Only��6'   $
  DRB1��6D   �� 5}      $��Credit Balance Only��6'   $
  CRB1��6E   5~      5      �� 5�      -%   5        $ stkOptVouType��5�      $ 
Horizontal=�64   .+�5�      �� -&   5        $ lblVouTypeTitle��5�      $��Voucher _Type :��65   $��Right=�6O   $��85��6r    6}   �� $ 
cmbVouType���� 5�      -'   5        $ 
cmbVouType��5�      
�������$	��300��6F   $��22��6R   $ �� 
IsReadOnly$ . $ 0��6p   5�      5�      5�      �� 5�   
   $��1��6   $��1��6%   .+�5�      �� $��0q�6   
��� ���6#   �� 5�      -(   5        % ��Name$% 
TabUcGrid1��5�      $	��920��6-   $	��530��69   
������  5�      ��%h   '��       �� 5�      " ��6   .�5�      ��  �@J�	AllowDrop . 66   -)   5        5�      5�      5�      5�      5�      5�      
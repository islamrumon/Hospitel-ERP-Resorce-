   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      9    3ASITHmsWpf.Commercial.Hospital.frmEntryFrontDesk103   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      -   5        
  � Tag$  FRONT DESK REPORTS��5   I      IOUserControl#��[  6b    � 	Resources 5
       �� 
TargetType �� Property �� Value��%T   'R�       �� 5   
   " R�6   .�5      ��  � ��$
 Bold"�6+   5      5      .��5      � $	��Top=�6    � Triggers 5   
   :� 5       :� RoutedEvent$ Window.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$ (Grid.Height)�6*    W� From$ 0X�6U   		 W� To$		 650X�6^   
 g� Duration$
 0:0:1D�6g   W� 5      $ 0X�6*   $	 1X�63   $
 00:00:2D�6:   $ Opacity�6M   5      5      5       � RowDefinitions 5   
   �� 5      $��90 �6   �� 5      $
��Auto �6   $	��512��6*   .+�5   
   ��  �� Orientation$ Vertical=�6   $	��Top=�6,   $��0,10,0,0q�6D   $��0��6X   $��2��6e   $��80��6y   .+�5       �� $ 
Horizontal=�6   .+�5!      �� -   5         ��Name$ stkOptBranch��5!      $ 
Horizontal=�64   .+�5"      �� $��Branch Name : ��6   $��90��65   $��Right=�6@   $��0,0,0,0q�6c   �� 5#      -   5         ��Name$ 
cmbSBrnCod��5#       �� SelectedIndex$ 0��63   $	��160��6E   6Q   5$      �� 5%      -   5        $ stkpOptUser��5%      $ 
Horizontal=�62   .+�5&      �� -   5         ��Name$ lblStaffIdTitle��5&      $��App. User :��65   $��Right=�6K   $��70��6n   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  5'      -   5          Name$ AtxtStaffId��5'   -      
SearchType$ C��6B   $
��Left=�6Q   $��22��6s   $	��295��6   �� 5(      -   5        $ 
lblSession��5(      $��User Session :��60   $��Right=�6I   $��90��6l   �� 5)      -   5        $ 
cmbSession��5)      $ 0��63   $��80��6F   $��22��6Q   5*      �� 5,      $��Output Options :��6   $	��100��63   $��Right=�6?   /   )ASITHmsWpf.UserControls.ComboOutputOption  5-      -	   5          Name$ cmbOutputOption��5-   '   $	��200��6@   �� 5.      -
   5         ��Name$ btnGenerate��5.        StdBtnStyle01#��[ 6.   $��Center=�6U   $��24��6p   $��	_Generate��6|   $
��Bold"�5/      $��90��6,   $��10,0,0,0q�6Q   50      �� 51      $ 
Horizontal=�6   .+�52      �� -   5        $ stkpDateFrom��52      $ 
Horizontal=�63   .+�53      �� $��Dated From :��6   $��90��63   $��Right=�6>   $��0,0,0,0q�6a   &   Xceed.Wpf.Toolkit.DateTimePicker  54      -   5          Name$ xctk_dtpFrom��54   *     StdDateTimePickerStyle01#��[ 6@   $
��Bold"�6r   $��22��6�   $	��100��6�   55      �� 56      -   5        $ stkOptTerminal��56      $ 
Horizontal=�65   .+�57      �� -   5        $ lblTerminals��57      $��
Terminal :��62   $��Right=�6G   $��60��6j   �� 58      -   5        $ cmbTerminals��58      $ 0��65   $	��145��6H   $��22��6T   59      �� 5:      -   5        $ stkOptItemGroup��5:      $ 
Horizontal=�66   .+�5;      �� $��Item Group :��6   $��80��63   $��Right=�6>   �� 5<      -   5        $ cmbItemGroup��5<      $ 0��65   $	��240��6G   $��22��6S   �� 5=      -   5        $ lblItemNameTitle��5=      $��Item Name :��66   $��Right=�6L   $	��100��6o     5>      -   5        $ 
AtxtItemID��5>   -   $ C��6A   $
��Left=�6P   $��22��6r   $	��300��6~   5?      5@      �� 5A      $ 
Horizontal=�6   .+�5B      �� -   5         ��Name$ chkAsonDate��5B      $��As On/��60   $��Center=�6A   $��56��6\   $��5,0,0,0q�6g   6x   �� 5C      $
��To :��6   $��29��6'   $��Right=�62     5D      -   5        $ 
xctk_dtpTo��5D   &   #��[ 6:   $
��Bold"�6l   $��22��6~   $	��100��6�   �� 5E      -   5        $ stkRptOptRefBy��5E      $ 
Horizontal=�65   .+�5F      �� -   5        $ lblRefByTitle��5F      $��	Ref. By :��63   $��Right=�6G   $��60��6j     5G      -   5        $ AtxtRefById��5G   -   $ C��6B   $
��Left=�6Q   $��22��6s   $	��465��6   5H      �� 5I      -   5        $ stkRptOptions��5I      $ 
Horizontal=�64   .+�5J      �� -   5        $ lblRptOptions��5J      $��Report Options :��63   $��Right=�6N   $	��100��6q   �� 5K      -   5        $ cmbRptOptions��5K      $ 0��66   $	��300��6I   $��22��6U   .@�5L      �� $��Default��6'   $
  NONE��69   5M      5N      5O      �� 5P      $��0q�6   
��� ���6#   5Q      �� 5R   
   $��1��6   $ 
Horizontal=�6"   .+�5S      �� .+�5T      R� -   5         R�Name$ 
tvRptTitle��5T      $
��Left=�6/   
������ $	��480��6c   $	��250��6p   $	��Top=�6|   $��14(�6�    � ContextMenu 5U      ��  �� ElementName$ cmbOutputOption�� �� Path$ ContextMenu�5V   �   5W      �� 5X      $	��940��6   $��5,0,0,0q�6%   .+�5Y      �� $ 
Horizontal=�6   $��45��66   $��Center=�6B   $y�	Collapsed=�6_   .+�5Z      �� -   5        $ lbltle1��5Z      ���	#FF862222$��Center=�6D   $
��Bold"�6b   $��Verdana)�6t   �� 5[      -   5        $ lbltle2��5[      ���	#FF862222$��Center=�6D   $
��Bold"�6b   $��Verdana)�6t   5\      �� 5]      -   5        $ stkpUpdateAccVoucher��5]      $ 
Horizontal=�6;   $
��Left=�6T   $y�Visible=�6o   .+�5^      �� -    5        $ btnUpdateVoucher��5^      #��[ 67   $��Center=�6^   $��24��6y   $��Create Receipt Voucher��6�   $
��Bold"�5_      $	��150��6+   $��10,0,0,0q�6V   �� 5`      -!   5         ��Name$ txtVoucherMsg��5`      $��11(�65   $
��Bold"�6C   $��10,0,0,0q�6U   5a      �� 5b      -"   5         ��Name$ 
TabUcGrid1��5b      $	��950��61   
������  5c      ��%h   '��       �� 5d      " ��6    .�5e      ��  �@J�	AllowDrop . 6:   -#   5        5h      5i      5j      �� 5k      -$   5        $ stkpDataGrid��5k      5m      5n      5o      5p      5q      
   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      8    2ASITHmsWpf.Commercial.RealEstate.frmRealSaleMgt107   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4UC%clr-namespace:ASITHmsWpf.UserControls     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5        
  � Tag$'  !REAL ESTATE PROJECT SALES REPORTS��5   I   $��14(�6q    � 	Resources 5
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
   ��  �� Orientation$ Vertical=�6   $	��Top=�6,   $��0,10,0,0q�6D   $��0��6X   $��2��6e   $��78��6y   .+�5       �� $ 
Horizontal=�6   .+�5!      �� -   5         ��Name$ stkOptBranch��5!      $ 
Horizontal=�64   .+�5"      �� $��Branch Name : ��6   $��90��65   $��Right=�6@   $��0,0,0,0q�6c   �� 5#      -   5         ��Name$ 
cmbSBrnCod��5#       �� SelectedIndex$ 0��63   $	��160��6E   6Q   5$      �� 5%      -   5        $ stkpOptUser��5%      $ 
Horizontal=�62   .+�5&      �� -   5         ��Name$ lblStaffIdTitle��5&      $��App. User :��65   $��Right=�6K   $��70��6n   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  5'      -   5          Name$ AtxtStaffId��5'   -      
SearchType$ C��6B   $
��Left=�6Q   $��22��6s   $	��295��6   �� 5(      -   5        $ 
lblSession��5(      $��User Session :��60   $��Right=�6I   $��90��6l   �� 5)      -   5        $ 
cmbSession��5)      $ 0��63   $��80��6F   $��22��6Q   5*      �� 5+      $��Output Options :��6   $	��100��63   $��Right=�6?   /   )ASITHmsWpf.UserControls.ComboOutputOption  5,      -	   5          Name$ cmbOutputOption��5,   '   $	��200��6@   �� 5-      -
   5         ��Name$ btnGenerate��5-         StdBtnStyle01#��[  6.   $��Center=�6U   $��24��6p   $��	_Generate��6|   $
��Bold"�5.      $��90��6,   $��10,0,0,0q�6Q   5/      �� 50      $ 
Horizontal=�6   .+�51      �� -   5        $ stkpDateFrom��51      $ 
Horizontal=�63   .+�52      �� $��Dated From :��6   $��90��63   $��Right=�6>   $��0,0,0,0q�6a   &   Xceed.Wpf.Toolkit.DateTimePicker  53      -   5          Name$ xctk_dtpFrom��53   *     StdDateTimePickerStyle01#��[ 6@   $
��Bold"�6r   $��22��6�   $	��100��6�   54      �� 55      -   5        $ stkOptTerminal��55      $ 
Horizontal=�65   .+�56      �� -   5        $ lblTerminals��56      $��
Terminal :��62   $��Right=�6G   $��60��6j   �� 57      -   5        $ cmbTerminals��57      $ 0��65   $	��145��6H   $��22��6T   58      �� 59      -   5        $ stkOptItemGroup��59      $ 
Horizontal=�66   .+�5:      �� $��Item Group :��6   $��80��63   $��Right=�6>   �� 5;      -   5        $ cmbItemGroup��5;      $ 0��65   $	��240��6G   $��22��6S   �� 5<      -   5        $ lblItemNameTitle��5<      $��Item Name :��66   $��Right=�6L   $	��100��6o     5=      -   5        $ 
AtxtItemID��5=   -   $ C��6A   $
��Left=�6P   $��22��6r   $	��300��6~   5>      5?      �� 5@      $ 
Horizontal=�6   .+�5A      �� -   5         ��Name$ chkAsonDate��5A      $��As On/��60   $��Center=�6A   $��56��6\   $��5,0,0,0q�6g   6x   �� 5B      $
��To :��6   $��29��6'   $��Right=�62     5C      -   5        $ 
xctk_dtpTo��5C   &   #��[ 6:   $
��Bold"�6l   $��22��6~   $	��100��6�   �� 5D      -   5        $ stkRptOptRefBy��5D      $ 
Horizontal=�65   .+�5E      �� -   5        $ lblRefByTitle��5E      $��	Ref. By :��63   $��Right=�6G   $��60��6j     5F      -   5        $ AtxtRefById��5F   -   $ C��6B   $
��Left=�6Q   $��22��6s   $	��465��6   5G      �� 5H      -   5        $ stkRptOptions��5H      $ 
Horizontal=�64   .+�5I      �� -   5        $ lblRptOptions��5I      $��Report Options :��63   $��Right=�6N   $	��100��6q   �� 5J      -   5        $ cmbRptOptions��5J      $ 0��66   $	��300��6I   $��22��6U   .@�5K      �� $��Default��6'   $
  NONE��69   5L      5M      5N      5O      �� 5P   
   $��1��6   $ 
Horizontal=�6"   .+�5Q      �� .+�5R      R� -   5         R�Name$ 
tvRptTitle��5R      $
��Left=�6/   
������ $	��480��6c   $	��250��6p   $	��Top=�6|   $��14(�6�    � ContextMenu 5S      ��  �� ElementName$ cmbOutputOption�� �� Path$ ContextMenu�5U      5V      �� 5W      $	��940��6   $��5,0,0,0q�6%   .+�5X      �� $ 
Horizontal=�6   $��45��66   $��Center=�6B   .+�5Y      �� -   5        $ lbltle1��5Y      ���	#FF862222$��Center=�6D   $
��Bold"�6b   $��Verdana)�6t   �� 5Z      -   5        $ lbltle2��5Z      ���	#FF862222$��Center=�6D   $
��Bold"�6b   $��Verdana)�6t   5[      �� 5\      -   5        $ stkpDataGrid��5\      5^      5_      5`      5a      5b      
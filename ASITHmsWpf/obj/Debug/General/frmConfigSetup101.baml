   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      'clr-namespace:ASITHmsWpf
ASITHmsWpf  5      7 clr-namespace:ASITHmsWpf.GeneralASITHmsWpf.General  5	      *    $ASITHmsWpf.General.frmConfigSetup101   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      $localclr-namespace:ASITHmsWpf  5      /localGen clr-namespace:ASITHmsWpf.General  5	      -   5        
  � Tag$  USER CONFIGURATION��5
         IOUserControl#��[  68    � 	Resources 5      4   .ASITHmsWpf.General.AuthObjGridToolTipConverter  AuthDescToolTip��%   &	         5   
   6/   5      .��5      � .+�5   
   ��  �� Orientation$ Vertical=�6   $��
0,10, 0, 0q�6-   z�5      �� 5      -   5        	noFreeze4��5   "    J� RenderTransformOrigin 5      8� 5       8� X$	 0.5Q�6    8� Y$	 0.5Q�6     � Triggers 5      :� 5       :� RoutedEvent$ Window.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$ (StackPanel.Height)�6.   	 W� From$	 0X�6_   	
 W� To$	
 650X�6h    g� Duration$ 0:0:1D�6q   W� 5      $ (StackPanel.Width)�6.   $	 0X�6^   $

 1200X�6g   $ 0:0:1D�6q   W� 5      $: 4(StackPanel.RenderTransform).(RotateTransform.Angle)�5   -   $	 0X�5   /   $	
 360X�68   $ 0:0:1D�6A   5       5!      5"      .+�5$      �� $ 
Horizontal=�6   .+�5&      �� $ 
Horizontal=�6   .+�5'      *   $ASITHmsWpf.UserControls.AutoComplete  -   5          Name$ autoUserSearch��5'   &   *   $ASITHmsWpf.UserControls.AutoComplete   PatternChanged$# autoUserSearch_PatternChanged��6�      MouseDoubleClick$% autoUserSearch_MouseDoubleClick��5(   �   $	��430��5'   >    �� SelectedValuePath$ sircode��6J    �� DisplayMemberPath$ sirdesc��6f    % !AutoCompleteComboBoxWithoutButton#��[ 5(   .      Delay$	 500��6i   $��24��6u    � ToolTip$= 7Right Click or Double Click to view existing staff list��5)   &    � ContextMenu 5*      �� 5+      $	��500��6*   $	��500��67   5-      �� 5.      -   5         ��Name$ btnFind��5.      $��_Find & Show��6.   $	��100��6I     StdBtnStyle01#��[ 6U   $��5,0,0,0q�6�   5/      �� 50      $ 
Horizontal=�6   
�������$	��620��6I   $��10,0,0,0q�6U   .+�51      �� $��Reports Title : ��6   $��90��67   $��Right=�6B   �� 52      -   5         ��Name$ 	cmbReport��52      $	��130��62    �� SelectedIndex$ 0��6>   $
��Bold"�6P   .@�53      �� $��	User List��6'   $  USERLIST��6;   �� 54      $��User Privileges��6'   $  
PRIVILEGES��6A   55      �� 56      $��Output Options : ��6   $	��100��68   $��Right=�6D   /   )ASITHmsWpf.UserControls.ComboOutputOption  57      -   5          Name$ cmbOutputOption��57   +   $	��180��6D   $��24��6P   �� 58      -   5        $ btnGenerate��58      #��[ 62   $��Center=�6Y   $��24��6t   $��	_Generate��6�   $
��Bold"�59      $	��110��6,   $��5,0,0,0q�6R   5:      5<      �� 5=      -   5         ��Name$ stkpUser��5=      $ 
Horizontal=�6+   $	��500��6D   $��0,10,0,0q�6Q   .+�5>      �� $ Vertical=�6   $	��500��64   $	��Top=�6A   .+�5?      �� $��Application users��6   $
��Bold"�68   �� 5@      -	   5         ��Name$ ListBoxUserList��5@       � IsSharedSizeScope . $	��150��6U   $	��450��6e   $	��160��6r   ��5B      �� 5C       �� 
TargetType" ��6$    �� Triggers 5D   "   P� 5E   &    P� Property �@��
IsSelected P� Value . . �5F   *   ��  �� Property � ��  �� Value$
  Bold"�6G   �� 5G   *    � ��
  ���� 6G   �� 5H   *    � ��
  �   �6G   5I   '   ! �� 	Resources! 5K   "   " �� Color��%X   (��       5L   6   * SystemColors.HighlightBrushKey+)�� 6&   $" Transparent��6h   5M   #   5N      ��5P      �� 5Q      .R�5R   "   � # � ColumnDefinitions# 5S   &   �� 5T   *   $��20 �6;   $ z� SharedSizeGroup$$ Column1��6F   �� 5U   *   $
��Auto �6;   $$ Column2��6H   .+�5W   &   �� $��0��60   $��Right=�6@   ��6\   �� *slnum+  6s   �� *hccode+ 6�   �� *
userrmrk+�� 5X   &   $��1��60   % �� Padding$% 5,0,0,0q�6[   ��6@   �� *	signinnam+  6m   �� *hccode+ 6�   �� *	namedsg+5Y   #   5Z      5\      5]      �� 5^      $ Vertical=�6   $	��370��64   $	��500��6@   $	��Top=�6M   $��10,0,0,0q�6e   .+�5_      �� $��User description��6   $
��Bold"�67   6K   �� 5`      $ 
Horizontal=�6!   .+�5a      �� -
   5        & ��Name$& 	imgPhoto2��5a       $
��Left=�63   $��..\Images\NoUser.jpg��6N   $y�	Collapsed=�6l   $
��Fill=�6�   $	��125��6�   $	��140��6�   �� 5b      -   5        $& imgPhoto��5b       $
��Left=�62   $��..\Images\NoUser.jpg��6M   $y�Visible=�6k   $
��Fill=�6�   $	��125��6�   $	��140��6�   �� 5c      $��5,0,0,0q�6%   .+�5d      �� $ 
Horizontal=�6)   .+�5e   "   �� $��Staff ID��6(   $��70��6;   $
��Left=�6G   �� 5f   "   $��:��6(   $
��Left=�64   �� 5g   "   -   5        ' ��Name$' lblHrId��5g   (   $�� ��69   $	��150��6D   $
��Left=�6P   
�������6k   5h      �� 5i      $ 
Horizontal=�6)   $��0,2,0,3q�6B   .+�5j   "   �� $��
Sign In ID��6(   $��70��6=   $
��Left=�6H   �� 5k   "   $��:��6(   $
��Left=�64   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5l   "   -   5        (  Name$( txtSignInId��5l   8    
 wtxtBx#��[ 6N   )   	Watermark$) Sign In Name��6n   $��5,0,0,0q�6�   * �� CharacterCasing$* Upper=�6�   $�� ��5m   5   $
��Left=�6>   $	��150��6`   + �� 	MaxLength$+ 20��6l   5n      �� 5o      $ 
Horizontal=�6)   .+�5p   "   �� $��Password��6(   $��70��6;   $
��Left=�6F   �� 5q   "   $��:��6(   $
��Left=�64   G� 5r   "   -   5        , G�Name$, txtUserPass1��5r   .   - G� Password$-  ��6D   $	��150��6P   . G� 	MaxLength$. 20��6\   G� 5s   "   -   5        $, txtUserPass1h��5s   .   $-  ��6E   $��0��6Q   $. 20��6[   $y�	Collapsed=�6j   5t      �� 5u      $ Vertical=�6)   .+�5v   "   �� $��	Remarks :��6,   $��70��6=   $
��Left=�6H   
���   �$��5,0,0,0q�6v     5w   "   -   5        $( txtSignInRmrk��5w   8   #��[ 6O   $) Remarks (if any)��6o   $��5,0,0,0q�6�   / �� TextWrapping$
/ Wrap=�5x   9   $�� ��6M   $
��Left=�6U   $��40��6w   5y      5z      5{      �� 5|      $
��Left=�6!   .+�5}      �� $ 
Horizontal=�6%   .+�5~      �� $��Name & Designation : ��6$   $
��Left=�6H   5      �� 5�      -   5        0 ��Name$0 txtHrNamDsg��5�   $   $
��Left=�69   $��40��6T   $	��360��6`   $
��Bold"�6l   $
��Wrap=�6~   5�      �� 5�      $
��Left=�6!   .+�5�      �� $��Module Description��6    $
��Bold"�6=   &   System.Windows.Controls.DataGrid  5�      -   5        1  Name$1 dgModule��5�   #   $	��220��65   $	��Top=�6B   $��11(�6Z   
������ 2   AutoGenerateColumns2 .  $��0q�6�   3   CanUserAddRows3 .  4   CanUserResizeRows4 .  5   SelectionMode$5 Single=�5�   A   6   SelectionUnit$6 FullRow=�6X   $	��360��6p   7   CanUserDeleteRows7 .  8   CanUserResizeColumns8 .  9 �� AlternationCount$9 2��5�   '   :   AlternatingRowBackground: �	#FFE1F1CA$
��Auto=�5�   '   $��Hidden=�6W   ;   ColumnHeaderStyle; 5�      �� 5�   "   =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6(   .�5�   &   ��  � ��$
  Bold"�6C   �� 5�   &    � ��$  Stretch=�6L   �� 5�   &    � ��$
  Left=�6S   5�   #   <   Columns< 5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�   "   ,	  &System.Windows.Controls.DataGridColumn= 	  Header$=  Sl.#��69   > 	  Width5
  /System.Windows.Controls.DataGridLengthConverter$> 40
 6H   ?   CellTemplate? 5�   &   �� 5�   *   .R�5�   .   �� $��Right=�6O   $��0q�6�   $��40��6�   ��64   �� *slnum+��6r   �� *
fontbold+6�   5�   +   5�   #     5�   "   $> 50
 69   = 5�   &   �� 5�   *   -   5        $' lblHeaderAllModule��5�   0   $
��Mark��6L   $
  None��6[   $6 0Double click here to select/unselect all modules��6f   5�   0   ? 5�   &   �� 5�   *   .R�5�   .   �� @ ��Name$@ 	chkModule��67   -   5        $��Center=�5�   J   $��Center=�6g   $��20��6�   A d� 	IsCheckedA 6�   �� B �� Path$B objallow�C �� Mode$C TwoWay=�D �� UpdateSourceTrigger$D PropertyChanged=�6�   5�   +   5�   #     5�   "   $= Name of module��69   $	> 230
 6Q   ? 5�   &   �� 5�   *   .R�5�   .   �� $��0q�6�   ��64   �� *
moduleid+  6T   �� *
moduleid+��6o   �� *
fontbold+6�   5�   +   5�   #   5�      5�      5�      �� 5�      $	��Top=�6   $
��Left=�66   $	��500��6Q   $��10,0,0,0q�6^   .+�5�      �� $ 
Horizontal=�6!   $��0,0,0,5q�6:   .+�5�      �� $!��Privileges of selected user��6    $
��Bold"�6F   $	��160��6X     5�      -   5        $( txtPrivCopyFromID��5�   0   #��[ 6L   $) Source Staff ID��6l   $	��115��6�   $+ 12��6�   $��24��6�   $9 3Put Staff ID and click next for copy all privileges��5�   5   �� 5�      -   5        $ btnCopyPrivilege��5�   !   $��40��6;   $
��Copy��6F   |�.  $��2,0,0,0q�6�   $��24��6�   �� 5�      -   5        $ btnUpdateUser��5�   !   $	��110��68   $��Update Privileges��6D   $4 .Update password & privileges for existing user��6`   $��65,0,0,0q�5�   1   $
��Bold"�6C   #��[ 6U   $��24��6�   5�        5�      -   5        $1 dgPermission��5�      $	��450��65   $	��Top=�6B   $��11(�6Z   
������ 2 .  $��0q�6�   3 .  4 .  $5 Single=�5�   A   $6 FullRow=�6X   $	��610��6q   7 .  8 .  $9 2��5�   '   : �	#FFE1F1CA$
��Auto=�6a   $��Hidden=�6�   ; 5�      �� 5�      "  6$   .�5�   "   ��  � ��$
  Bold"�6?   �� 5�   "    � ��$  Stretch=�6H   �� 5�   "    � ��$
  Left=�6O   5�      < 5�        5�      $=   Sl. #��65   $> 55
 6F   ? 5�   "   �� 5�   &   .R�5�   *   �� $��Right=�6K   $��0q�6�   $��50��6�   ��60   �� *slnum+��6n   �� *
fontbold+6�   5�   '   5�        5�      $= Module��65   $	> 120
 6E   ? 5�   "   �� 5�   &   .R�5�   *   �� $��0q�6�   $	��115��6�   ��60   �� *
moduleid+  6P   �� *
moduleid+��6k   �� *
fontbold+6�   5�   '   5�        5�      $> 50
 65   = 5�   "   �� 5�   &   -   5        $' lblHeaderAllow��5�   ,   $��Allow��6D   $
  None��6T   $/ )Double click here to select/unselect all ��6_   6�   ? 5�   "   �� 5�   &   .R�5�   *   �� $@ chkAllow��63   $��Center=�6E   $��Center=�6b   $��20��6�   A 6}   �� $B objallow�$C TwoWay=�$D PropertyChanged=�6�   5�   '   5�        5�      $= Object Description��65   $	> 360
 6Q   ? 5�   "   �� 5�   &   .R�5�   *   �� $��0q�6�   $	��355��6�   ��60   �� *uidesc+ 6L   �� E �� 	Converter#E [ ��6�   �� *
fontbold+  6�   �� *uicode+5�   '   5�      5�      5�      5�      5�      5�      5�      
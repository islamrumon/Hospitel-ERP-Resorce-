   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ASITHmsWpf
ASITHmsWpf  5      A%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      .    (ASITHmsWpf.Diagnostic.frmEntryLabMagt107   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:ASITHmsWpf     5      4UC%clr-namespace:ASITHmsWpf.UserControls     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5           IOUserControl#��[  5	      
  � Tag$  Labratory Report Templete��6F   $��14(�6f   .��5
      � .+�5   
   � $��20,5,0,0q�6   .+�5      �� .+�5      ��  �� Orientation$ 
Horizontal=�6   $��25��66   .+�5      �� -   5         ��Name$ chkTestName��5      $��Selected _Test Name : ��64   $	��150��6V   $��Center=�6b   $��Right=�6�   $
��Left=�6�   6�   �� 5      -   5         ��Name$ lblSelectedTest��5      $	��580��65   $$��NAME OF THE SELECTED TEST ITEM��6A   $
��Bold"�6j   $��5,3,0,0q�6|   5      �� 5      $��4��6   �� 5      -   5         ��Name$ 	stkpEntry��5      $y�Visible=�60   .+�5      �� $ 
Horizontal=�6!   .+�5      �� $ 
Horizontal=�6%   .+�5      �� $��Disgnosis Report Title :��6$   $	��150��6G   $��Right=�6S   �� 5      -   5        $ lblRptTitle��5   $   $	��510��69   $&�� ENDOCRINOLOGY EXAMINATION REPORT��6E   $
��Bold"�6p   �� 5      -   5        $ lblSpecimenTitle��5   $   $��
Specimen :��6>   $	��100��6S   $��Right=�6_   �� 5      -   5        $ lblSpecimen��5   $   $	��230��69   $��BLOOD��6E   $
��Bold"�6U   5      �� 5      $ 
Horizontal=�6%   .+�5      �� -   5         ��Name$ btnPrintTemplate��5   %     StdBtnStyle01#��[ 6?   $��_Print��6f   $��75��6w   $��24��6�   $��10,0,0,0q�6�   $	��Top=�6�   6�   �� 5      -	   5        $ btnUpdateTemplate��5   %   #��[ 6@   $��_Update��6g   $��75��6y   $��24��6�   $��15,0,0,0q�6�   $	��Top=�6�   6�   5      5      �� 5      $ 
Horizontal=�6!   .+�5       �� $��Machine use in test :��6    $	��150��6@   $��Right=�6L   �� 5!      -
   5        $ 
lblMachine��5!       $
��1020��64   $���The test is carried out by Fully Automated Biochemistry Analyzer (BECKMAN COULTER® AU480, Japan, KONELAB 60i, Thermo, Finland)��6A   $
��Bold"�6�   5"      �� 5#      $ 
Horizontal=�6!   $��24��6:   $��0,0,0,5q�6F   .+�5$      �� -   5        $ 
chkElement��5$   #   $��Add/Edit Elements : ��67   $	��150��6W   $��Center=�6c   $��Right=�6�   �� 5%      -   5        $ stkpAddElement��5%   %   $ 
Horizontal=�6=   $��Center=�6V   $y�Visible=�6s   .+�5&      �� -   5        $ stkpGroupElement��5&   )   $ 
Horizontal=�6C   .+�5'   "   �� $��Group Sl# : ��6,   $��80��6@    �� TextAlignment$ Right=�6K   $��Center=�6a   %  Xceed.Wpf.Toolkit.IntegerUpDown  5(   "   -   5          Name$ udGroup��5(   5   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value$ 01X�6F   $��25��6Q   	   ShowButtonSpinner	 .  
   Minimum$
 01X�6v      Maximum$ 20X�6�   �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$ 00��6�     5)   "   -   5        $ udSlNum��5)   5   $ 01X�6F   $��25��6Q   	 .  $
 01X�6v   $ 50X�6�   $ 00��6�   5*      �� 5+      $ 
Horizontal=�6)   .+�5,   "   �� -   5         ��Name$ cmbElementType��5,   +   $	��120��6C    �� SelectedIndex$ 0��6O   .@�5-   &   �� $��Report Title��63   $  	SILBRPT01��6J   �� 5.   &   $��Specimen��63   $  	SILBRPT02��6F   �� 5/   &   $��Machine��63   $  	SILBRPT03��6E   �� 50   &   $��Group Title��63   $  	SILBRPT06��6I   �� 51   &   $��Test Element��63   $  	SILBRPT08��6J   52   #   *   $ASITHmsWpf.UserControls.AutoComplete  53   "   -   5          Name$ autoElementSearch��53   2   *   $ASITHmsWpf.UserControls.AutoComplete   PatternChanged$&  autoElementSearch_PatternChanged��6�      MouseDoubleClick$( "autoElementSearch_MouseDoubleClick��54   u   $	��590��53   M    �� SelectedValuePath$ actcode��6Y    �� DisplayMemberPath$ actdesc��6u    % !AutoCompleteComboBoxWithoutButton#��[ 54   "      Delay$	 500��6]   $��24��6i    � ContextMenu 55   &   �� 56   *   $	��400��66   58   #   �� 59   "   -   5        $ btnSelectElement��59   )   #��[ 6C   $��_Select��6j   $��75��6|   $��10,0,0,0q�6�   6�   5:      5;      5<      �� 5=      .+�5>      &   System.Windows.Controls.DataGrid  -   5          Name$ dgvRpt��5>   #      CanUserResizeRows .  
������    
IsReadOnly .  $��0q�6y   $
��1170��6�      HeadersVisibility$	 All=�6�      GridLinesVisibility$ Vertical=�6�   $	��Top=�5?   #   $
��Left=�6;   $	��450��6V   $
��Auto=�6c      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $��Hidden=�5@   =      SelectionUnit$ FullRow=�6q      SelectionMode$ Single=�6�       RowHeaderWidth$  0Q�6�   ! �� AlternationCount$! 2��6�   "   AlternatingRowBackground" �	#FFEDFFEE# � 	Resources# 5B      =  7System.Windows.Controls.Primitives.DataGridColumnHeader$ �� 
TargetType% �� Property& �� Value*  $System.Windows.Controls.DataGridCell' �� Triggers( P� Property) P� Value��%�  '        '  �     �� 5C   "   "$  6(   .�5D   &   �� % � ��& �	#FFE2D2CE6C   �� 5E   &   % � ��$
& Bold"�6C   �� 5F   &   % � ��$& 1q�6H   �� 5G   &   % � ��
& �   �6D   �� 5H   &   % � ��$& 25��6?   5I   #   �� 5J   "   "$  6(   ' 5K   &   P� 5L   *   ( �@ 
IsSelected) . . �5M   .   �� % � ��
& �����6L   �� 5N   .   % � ��
& �   �6L   5O   +   5Q   #   5U      *   Columns* 5V      4	  .System.Windows.Controls.DataGridTemplateColumn	  5W   "   ,
  &System.Windows.Controls.DataGridColumn+ 
  Width5  /System.Windows.Controls.DataGridLengthConverter$+ 70 69   , 
  HeaderTemplate, 5X   &   �� 5Y   *   .R�5Z   .   �� $ btndgvRptHeader��65   -   5        $��	Group Sl#��5Z   N   $��0q�6b   
������ - � ToolTip$- Click here to sort��6�   5[   +   . 	  CellTemplate. 5]   &   �� 5^   *   .R�5_   .   �� $ 
Horizontal=�69   .+�5`   2   $  Xceed.Wpf.Toolkit.DoubleUpDown  $��30��6�   $	��Top=�5a   8   �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]/   FormatString$/ 00��6P   $��0,0,0,5q�6b   $��0q�6t   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]0   ShowButtonSpinner0 .  1   Minimum$1 01X�6�   2   Maximum$2 99X�6�   
������ 3   Value3 5`   D   �� 4 �� Path$4 elgrpsl�5 �� StringFormat$5 {0: 00}��6 �� Mode$6 TwoWay=�7 �� UpdateSourceTrigger$7 PropertyChanged=���6�   �� *
elFontBold+5a   �   �� 5b   2   $��.��6<   $��5��6E     5c   2   $��30��6�   $	��Top=�5d   8   $/ 00��6P   $��0,0,0,5q�6b   $��0q�6t   0 .  $1 01X�6�   $2 99X�6�   
������ 3 5c   D   �� $4 elressl�$5 {0: 00}��$6 TwoWay=�$7 PropertyChanged=�{�6�   �� *elSlEnabled+5d   �   �� 5e   2   $��.��6<   $��5��6E   5f   /   5g   +   5i   #   	  5j   "   $	+ 300 69   8 
  Header$8 Test Descriptions��6E   . 5k   &   �� 5l   *   .R�5m   .   �� .+�5n   2   �� $	��Top=�6<   $��2,0,0,0q�6�   ��6T   �� *eldesc+- 6l   �� *eldesc+��6�   �� *
elFontBold+5o   /   5p   +   5r   #   	  5s   "   $	+ 500 69   $8 Result Format��6E   . 5u   &   �� 5v   *   .R�5w   .   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  $��0q�6D   )  #Xceed.Wpf.Toolkit.AutoSelectTextBox9   AutoSelectBehavior$9 OnFocus=�6X   $��2,0,0,0q�6u    
 wtxtBx#��[ 6�   :   	Watermark$: Result Format��6�   
������ $	��Top=�5x   5   ; �� AcceptsReturn; . < �� TextWrapping$
< Wrap=�5y   <   ��5x   b   �� $4 elresval�$6 TwoWay=�$7 PropertyChanged=���6�   �� *
elFontBold+y�5y   Q   �� *	elVisible+5z   +   5|   #   	  5}   "   $	+ 230 69   $8 Reference Values��6E   . 5~   &   �� 5   *   .R�5�   .     $��0q�6D   $9 OnFocus=�6Y   $��2,0,0,0q�6v   #��[ 6�   $: Reference Values��6�   
������ ; . $	��Top=�5�   M   ��6e   �� $4 elrefval�$6 TwoWay=�$7 PropertyChanged=���5�   D   �� *
elFontBold+y�6f   �� *	elVisible+5�   +   5�   #   	  5�   "   $+ 45 69   $8 Style��6D   . 5�   &   �� 5�   *   .R�5�   .     $��0q�6D   $9 OnFocus=�6Y   $��2,0,0,0q�6v   #��[ 6�   $: Style��6�   
������ ; . $	��Top=�5�   N   = �� 	MaxLength$= 5��6�   ��6f   �� $4 elstyle�$6 TwoWay=�$7 PropertyChanged=�6�   5�   +   5�   #   5�      5�      �� 5�      -   5        $ stkpNav1��5�   !   $ 
Horizontal=�63   $
��Left=�6L   $��20��6g   $	��190��6s   $y�Visible=�6   $��530,-875,0,0q�6�   ���	#FFEEEDE4.+�5�      �� -   5        $ btnTop��5�   !   $��|<��61   $- First record��6A   $��30��6X   $
��Bold"�6c   
������ $��0q�6�   $��0,0,0,0q�6�   |�.  6�   �� 5�      -   5        $ btnPrev��5�   !   $��<��62   $- Previous record��6A   $��30��6[   $
��Bold"�6f   
������ $��0q�6�   $��0,0,0,0q�6�   $��5,0,0,0q�6�   |�.  6�   �� 5�      -   5        $ btnNext��5�   !   $��>��62   $- Next record��6B   $��30��6X   $
��Bold"�6c   
������ $��0q�6�   $��0,0,0,0q�6�   $��5,0,0,0q�6�   |�.  6�   �� 5�      -   5        $ 	btnBottom��5�   !   $��>|��64   $- Last record��6D   $��30��6Z   $
��Bold"�6e   
������ $��0q�6�   $��0,0,0,0q�6�   $��5,0,0,0q�6�   |�.  6�   �� 5�      -   5        $ 	btnDelete��5�   !   $��[ x ]��64   $- Delete record��6D   $��30��6\   $
��Bold"�6g   
������ $��0q�6�   $��0,0,0,0q�6�   $��20,0,0,0q�6�   |�.  6�   5�      5�      5�      5�      � 5�   
   -   5        > �Name$> gridItemList��5�      $y�	Collapsed=�6%   $��	20,30,0,0q�6<   .+�5�      �� $	��500��6   $
��Left=�6%   .+�5�      �� $ 
Horizontal=�6   .+�5�        -   5        $ autoTestItemSearch��5�   &   $' !autoTestItemSearch_PatternChanged��6�   $) #autoTestItemSearch_MouseDoubleClick��5�   u   ?   	LostFocus$"? autoTestItemSearch_LostFocus��6�   $	��410��5�   B   $ sircode��6N   $ sirdesc��6j   $
��Bold"�6�   #��[ 5�   "   $	 500��6]   $��24��6i    5�      �� 5�      $	��400��6*   5�      �� 5�      -   5        $ btnSelectTestItem��5�      #��[ 68   $��_Select��6_   $��80��6q   $��24��6|   $��10,0,0,0q�6�   6�   5�      �� 5�      .+�5�      R� -   5        @ R�Name$@ treeItemGroup��5�      $	��500��66   $	��500��6C   $��Right=�6m   
������ $��Hidden=�6�   5�      5�      5�      5�      5�      
   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      9    3ASITHmsWpf.Commercial.ParkTicket.frmEntryParkPOS105   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4UC%clr-namespace:ASITHmsWpf.UserControls     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5        
  � Tag$  PARK TICKET/COUPON DESIGN��5	   8   $��14(�6X   .��5
      � .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0,10,0,0q�6.   .+�5      �� $	��400��6   $
��Left=�6%   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��_Find : ��6   $��45��6/   $��Right=�6:   $��24��6]   $��2,2,0,0q�6i    �� Target 6{   ��  �� ElementName$ autoTicketItemSearch��*   $ASITHmsWpf.UserControls.AutoComplete  5      -   5          Name$ autoTicketItemSearch��5   &   *   $ASITHmsWpf.UserControls.AutoComplete   PatternChanged$) #autoTicketItemSearch_PatternChanged��6�      MouseDoubleClick$+ %autoTicketItemSearch_MouseDoubleClick��5   u      	LostFocus$$ autoTicketItemSearch_LostFocus��6�   $	��290��5   D    �� SelectedValuePath$ sircode��6P   	 �� DisplayMemberPath$	 sirdesc��6l   $
��Bold"�6�    %  !AutoCompleteComboBoxWithoutButton#��[  5   "   
   Delay$	
 500��6]   $��24��6i    � ContextMenu 5      �� 5      $	��400��6*   5      �� 5      -   5         ��Name$ btnSelectTicketItem��5        StdBtnStyle01#��[ 6:   $��_Select��6a   $��60��6s   $��24��6~   $��5,0,0,0q�6�   6�   5      �� 5      $��0,5,0,0q�6   .+�5      R� -   5         R�Name$ treeItemGroup��5      $	��500��66   $	��400��6C   $
��Left=�6m   
������ $��Hidden=�6�   5      5      �� 5      $��10,0,0,0q�6   .+�5      �� $ 
Horizontal=�6   $��25��66   .+�5      �� $��Selected _Ticket : ��6   $��0,2,0,0q�6:   $	��120��6L   $��Right=�6X   �� 5      -   5         ��Name$ lblSelectedTest��5      $	��400��65   $&�� NAME OF THE SELECTED TICKET ITEM��6A   $
��Bold"�6l   $��5,3,0,0q�6~   �� 5      $ 
Horizontal=�6!   .+�5       �� -   5        $ btnPrintTemplate��5    !   #��[ 6;   $��_Print��6b   $��60��6s   $��24��6~   $��30,0,0,0q�6�   $	��Top=�6�   6�   �� 5!      -   5        $ btnUpdateTemplate��5!   !   #��[ 6<   $��_Update��6c   $��80��6u   $��20,0,0,1q�6�   6�   5"      5#      �� 5$      $��4��6   �� 5%      -   5         ��Name$ 	stkpEntry��5%      $y�Visible=�60   .+�5&      �� .+�5'      �� $ 
Horizontal=�6%   $y�	Collapsed=�6>   .+�5(      �� $��Ticket Title :��6$   $	��100��6=   $��Right=�6I   �� 5)      -	   5        $ lblRptTitle��5)   $   $	��320��69   $��(AS DESCRIBED)��6E   $
��Bold"�6^   5*      �� 5+      $ 
Horizontal=�6%   $y�	Collapsed=�6?   .+�5,      �� $ 
Horizontal=�6)   .+�5-   "   �� $��
SubTitle :��6(   $	��100��6=   $��Right=�6I   �� 5.   "   -
   5        $ lblSubTitle��5.   (   $	��260��6=   $��(UNSPCIFIED)��6I   $
��Bold"�6`   5/      �� 50      -   5        $ stkpName��50   )   $ 
Horizontal=�6;   .+�51   "   �� $��Note (if any) :��6(   $	��100��6B   $��Right=�6N   �� 52   "   -   5        $ lblTicketNote��52   (   $	��350��6?   $��SPECIAL PROMOTION��6K   $
��Bold"�6g   53      54      �� 56      $ 
Horizontal=�6%   $
��Left=�6>   $��0,5,0,5q�6Y   .+�57      �� -   5         ��Name$ 
chkElement��57   '   $��Add/Edit Item��6;   $	��110��6T   $��Center=�6`   $
��Left=�6�   $
��Left=�6�   �� 58      -   5        $ stkpAddElement��58   )   $ 
Horizontal=�6A   $��Center=�6Z   $y�Visible=�6w   .+�59   "   �� -   5        $ stkpGroupElement��59   -   $ 
Horizontal=�6G   .+�5:   &   �� $��Group Sl# : ��60   $	��100��6D    �� TextAlignment$ Right=�6P   $��Center=�6f   $y�	Collapsed=�6�   %  Xceed.Wpf.Toolkit.IntegerUpDown  5;   &   -   5          Name$ udGroup��5;   9   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value$ 01X�6J   $��25��6U      ShowButtonSpinner .     Minimum$ 01X�6z      Maximum$ 20X�6�   �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$ 00��6�     5<   &   -   5        $ udSlNum��5<   9   $ 01X�6J   $��25��6U    .  $ 01X�6z   $ 20X�6�   $ 00��6�   5=   #   �� 5>   "   $ 
Horizontal=�6-   .+�5?   &   �� -   5         ��Name$ cmbElementType��5?   /   $	��100��6G    �� SelectedIndex$ 0��6S   .@�5C   *   �� $��Group Title��67   $  	SIPOTKT06��6M   �� 5D   *   $��Ticket Item��67   $  	SIPOTKT08��6M   5E   '     5F   &   -   5        $ autoElementSearch��5F   6   $&  autoElementSearch_PatternChanged��6�   $( "autoElementSearch_MouseDoubleClick��5G   x   $	��285��5F   Q   $ actcode��6]   $	 actdesc��6y   #��[  5G   %   $	
 500��6`   $��24��6l    5H   *   �� 5I   .   $	��400��6:   5K   '   �� 5L   &   -   5        $ btnSelectElement��5L   -   #��[ 6G   $��_Select��6n   $��60��6�   $��24��6�   $��5,0,0,0q�6�   $	��Top=�6�   5N   #   5O      5P      �� 5Q      $��	110,0,0,0q�6%   .+�5R      &   System.Windows.Controls.DataGrid  -   5          Name$ dgvTkt��5R   '      CanUserResizeRows .  
������    
IsReadOnly .  $��0q�6}   $	��500��6�      HeadersVisibility$	 All=�6�      GridLinesVisibility$ Vertical=�5S   #   $	��Top=�6B   $
��Left=�6Z   $	��350��6u   $
��Auto=�6�      AutoGenerateColumns .      CanUserAddRows  .  !   CanUserDeleteRows! .  $��Hidden=�5T   p   "   SelectionUnit$" FullRow=�6�   #   SelectionMode$# Single=�5U   #   $   RowHeaderWidth$$ 0Q�6:   % �� AlternationCount$% 2��6M   &   AlternatingRowBackground& �	#FFEDFFEE' � 	Resources' 5V   "   =  7System.Windows.Controls.Primitives.DataGridColumnHeader( �� 
TargetType) �� Property* �� Value*  $System.Windows.Controls.DataGridCell+ �� Triggers, P� Property- P� Value��%�  '        '  �     �� 5W   &   "(  6,   .�5X   *   �� ) � ��* �	#FFE2D2CE6G   �� 5Y   *   ) � ��$
* Bold"�6G   �� 5Z   *   ) � ��$* 1q�6L   �� 5[   *   ) � ��
* �   �6H   �� 5\   *   ) � ��$* 25��6C   5]   '   �� 5^   &   "(  6,   + 5_   *   P� 5`   .   , �@ 
IsSelected- . . �5a   2   �� ) � ��
* �����6P   �� 5b   2   ) � ��
* �   �6P   5c   /   5e   '   5i   #   .   Columns. 5j   "   4	  .System.Windows.Controls.DataGridTemplateColumn	  5k   &   ,
  &System.Windows.Controls.DataGridColumn/ 
  Width5  /System.Windows.Controls.DataGridLengthConverter$/ 70 6=   0 
  HeaderTemplate0 5l   *   �� 5m   .   .R�5n   2   �� $ btndgvTktHeader��69   -   5        $��	Group Sl#��5n   R   $��0q�6f   
������ 1 � ToolTip$1 Click here to sort��6�   5o   /   2 	  CellTemplate2 5q   *   �� 5r   .   .R�5s   2   �� $ 
Horizontal=�6=   .+�5t   6   $  Xceed.Wpf.Toolkit.DoubleUpDown  $��30��6�   $	��Top=�5u   8   �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]3   FormatString$3 00��6P   $��0,0,0,5q�6b   $��0q�6t   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]4   ShowButtonSpinner4 .  5   Minimum$5 01X�6�   6   Maximum$6 99X�6�   
������ 7   Value7 5t   H   �� 8 �� Path$8 elgrpsl�9 �� StringFormat$9 {0: 00}��: �� Mode$: TwoWay=�; �� UpdateSourceTrigger$; PropertyChanged=���6�   �� *
elFontBold+5u   �   �� 5v   6   $��.��6@   $��5��6I     5w   6   $��30��6�   $	��Top=�5x   8   $3 00��6P   $��0,0,0,5q�6b   $��0q�6t   4 .  $5 01X�6�   $6 99X�6�   
������ 7 5w   H   �� $8 elressl�$9 {0: 00}��$: TwoWay=�$; PropertyChanged=�{�6�   �� *elSlEnabled+5x   �   �� 5y   6   $��.��6@   $��5��6I   5z   3   5{   /   5}   '   	  5~   &   $	/ 350 6=   < 
  Header$< Ticket Descriptions��6I   2 5   *   �� 5�   .   .R�5�   2   �� .+�5�   6   �� $	��Top=�6@   $��2,0,0,0q�6�   ��6X   �� *eldesc+1 6p   �� *eldesc+��6�   �� *
elFontBold+5�   3   5�   /   5�   '   	  5�   &   $/ 50 6=   $< Style��6H   2 5�   *   �� 5�   .   .R�5�   2   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  $��0q�6H   )  #Xceed.Wpf.Toolkit.AutoSelectTextBox=   AutoSelectBehavior$= OnFocus=�6]   $��2,0,0,0q�6z    
 wtxtBx#��[ 6�   >   	Watermark$> Style��6�   
������ ? �� AcceptsReturn? . $	��Top=�5�   g   @ �� 	MaxLength$@ 5��5�   8   ��5�      �� $8 elstyle�$: TwoWay=�$; PropertyChanged=���5�   F   �� *
elFontBold+5�   /   5�   '   5�      5�      �� 5�      -   5        $ stkpNav1��5�   %   $ 
Horizontal=�67   $
��Left=�6P   $��24��6k   $y�Visible=�6w   $��320,-676,0,0q�6�   ���	#FFEEEDE4.+�5�      �� -   5        $ btnTop��5�   %   $��|<��65   $1 First record��6E   $��30��6\   $
��Bold"�6g   
������ $��0q�6�   $��0,0,0,0q�6�   |�.  6�   �� 5�      -   5        $ btnPrev��5�   %   $��<��66   $1 Previous record��6E   $��30��6_   $
��Bold"�6j   
������ $��0q�6�   $��0,0,0,0q�6�   $��5,0,0,0q�6�   |�.  6�   �� 5�      -   5        $ btnNext��5�   %   $��>��66   $1 Next record��6F   $��30��6\   $
��Bold"�6g   
������ $��0q�6�   $��0,0,0,0q�6�   $��5,0,0,0q�6�   |�.  6�   �� 5�      -   5        $ 	btnBottom��5�   %   $��>|��68   $1 Last record��6H   $��30��6^   $
��Bold"�6i   
������ $��0q�6�   $��0,0,0,0q�6�   $��5,0,0,0q�6�   |�.  6�   �� 5�      -   5        $ 	btnDelete��5�   %   $��[ x ]��68   $1 Delete record��6H   $��30��6`   $
��Bold"�6k   
������ $��0q�6�   $��0,0,0,0q�6�   $��20,0,0,0q�6�   |�.  6�   5�      5�      5�      5�      5�      5�      5�      
   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      .    (ASITHmsWpf.Inventory.frmEntryStoreIssue1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      -   5        
  � Tag$9  3Item Issue/Transfer/Consumption - Entry/Edit Screen��5         IOUserControl#��[  6Y   .��5	      � .+�5
   
   � -   5         �Name$ GridDataEntry��5
       � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      � -   5        $ gridMastersi��5      $��0��6)   $	��Top=�66   $��0,0,10,0q�6O    5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$    � ColumnDefinitions 5      �� 5      $
��245* �6'   �� 5      $
��190* �6'   �� 5      $
��181* �6'   .+�5      �� $��0��6   $��0��6-    �� Orientation$ 
Horizontal=�6:   $
��Left=�6S   $	��380��6n   .+�5      �� $��New/Selected Transaction:��6    $��14(�6A   $��Italic$�6O   $
��Bold"�6b   
����� �$	��180��6�   �� 5      -   5         ��Name$ txtblEditMode��5       $y�Hidden=�67   $"��[ Store Issue In Edit Mode ]��6K   $��14(�6o    �� TextAlignment$ Center=�6}   
��� ���$
��Bold"�6�   
���  ��$	��180��6�   $��20,0,0,0q�6�   5      �� 5      -   5         ��Name$ stkIntro��5      $ Vertical=�6/   $��0��6F   $��2��6V   $��1��6k   $��3��6x   $	��Top=�6�   $
��Left=�6�   $��85��6�   $��	0,3,3.6,0q�6�   .+�5       �� $ 
Horizontal=�6!   .+�5!      �� -   5         ��Name$ 
lblsectcod��5!       $��Issue _From :��64   $��Right=�6L   $��80��6o   	 �� Target	 6z   �� 
 �� ElementName$
 
cmbSectCod���� 5"      -   5         ��Name$ 
cmbSectCod��5"   #   $	��270��67    �� 
IsReadOnly .  �� SelectedIndex$ 0��6U   $��24��6g   6s   �� 5#      -   5        $ lblSirByidTitle��5#       $��Issued _By :��69   $��Right=�6P   $��80��6s   	 6~   �� $
 autoIssueByStaffSearch��*   $ASITHmsWpf.UserControls.AutoComplete  5$      -	   5          Name$ autoIssueByStaffSearch��5$   *   *   $ASITHmsWpf.UserControls.AutoComplete   PatternChanged$+ %autoIssueByStaffSearch_PatternChanged��5%   &   $	��400��5$   J    �� SelectedValuePath$ sircode��6V    �� DisplayMemberPath$ sirdesc��6r    % !AutoCompleteComboBoxWithoutButton#��[ 5%   ^      Delay$	 500��6�   $��24��6�   5&      �� 5'      $ 
Horizontal=�6!   $��0,2,0,0q�6:   .+�5(      �� -
   5        $ lblsectCode2Title��5(       $��Issue _To :��6;   $��Right=�6Q   $��80��6t   	 6   �� $
 cmbSectCod2���� 5)      -   5        $ cmbSectCod2��5)   #   $	��270��68    . $ 0��6V   $��24��6h   6�   �� 5+      -   5        $ lblrecvbyidTitle��5+       $��Recei_ved By :��6:   $��Right=�6S   $��80��6v   	 6�   �� $
 autoRecByStaffSearch��  5,      -   5        $ autoRecByStaffSearch��5,   *   $) #autoRecByStaffSearch_PatternChanged��5-   &   $	��400��5,   H   $ sircode��6T   $ sirdesc��6p   #��[ 5-   [   $	 500��6�   $��24��6�   5.      �� 50      $ 
Horizontal=�6!   $��0,2,0,0q�6:   .+�51      �� -   5        $ lblsirrefTitle��51       $��_Ref. (if any) :��68   $��Right=�6S   $	��Top=�6v   $��80��6�   	 6�   �� $
 txtissRefpr��(  "Xceed.Wpf.Toolkit.WatermarkTextBox  52      -   5          Name$ txtissRefpr��52   0   $	��Top=�6E    
 wtxtBx#��[ 6]      	Watermark$ Type Referance��6}   $��5,0,0,0q�6�    �� TextWrapping$
 Wrap=�53   -   $�� ��6A   $
��Left=�6J   $��24��6l   $	��100��6x   �� 54      -   5        $ lblSrfNarTitle��54       $��_Narration :��68   $	��Top=�6O   $��Right=�6g   $��25��6�   $��70��6�   	 6�   �� $
 	txtSrfNar���� 55      -   5         ��Name$ 	txtSrfNar��55   "   $�� ��65   $
��Left=�6=   $	��580��6`   56      57      �� 58      $ Vertical=�6   $��2��64   $��1��6E   $	��Top=�6R   $��Right=�6j   $��85��6�   .+�59      �� $ 
Horizontal=�6!   .+�5:      �� -   5         ��Name$ chkDateBlocked��5:   #   $��Issue _Date : ��6;   |�.  $
��Left=�6f   $��Center=�6�   $	��115��6�   �� 5;      -   5        $ lblDateBlocked��5;       $��Issue Date :��68   $	��115��6O   $��Center=�6[   $��Right=�6v   $y�	Collapsed=�6�   &   Xceed.Wpf.Toolkit.DateTimePicker  5<      -   5          Name$ xctk_dtpSrfDat��5<   .     StdDateTimePickerStyle01#��[ 6F   $
��Bold"�6x   $��22��6�   $	��130��6�   {�5=      �� *	IsChecked+$
 chkDateBlocked���� 5>      -   5         ��Name$ btnOk��5>   !     StdBtnStyle01#��[ 60   $	��_Ok��6W   $��75��6f   $��30,0,0,0q�6�   5?      �� 5@      $ 
Horizontal=�6!   .+�5A      �� -   5        $ lblSrfNoTitle��5A       $��
S.I.R. No:��67   $��Right=�6L   $	��115��6o   �� 5B      -   5        $ lblSrfNo��5B       $��SIRMM-XXXXX��62   $  SIRYYYYMMCCCCXXXXX��6H   $	��130��6a   $
��Bold"�6m   $
��Left=�6   5C      �� 5D      $
��Left=�6!   $��
0, 0, 0, 0q�6<   $��40��6P   .+�5E      �� $ 
Horizontal=�6%   $��Right=�6>   $��0, -5, 0, 0q�6Z   .+�5F      �� -   5        $ chkAuthorize��5F   '   $��Show Authorization Panel��6=   |�.  $��Center=�5G   #   y�5F   r   ��  �� Path$ 
Visibility�$
 	btnPrint2��5G   >   �� 5H      -   5        $ 	btnPrint2��5H   %   $��_Print��68   $��75��6I   $��Bottom=�6T   $��12(�6�   $��Verdana)�6�   #��[ 5I   %   $��20,0,0,0q�6L   �� 5J      -   5        $ 	btnUpdate��5J   %   #��[ 68   $��_Update��6_   $��75��6r   $��25,0,0,0q�6�   5K      �� 5L      -   5        $ xctkTransList��5L   %   $ 
Horizontal=�6<   $��0,2,0,0q�6U   .+�5M      �� -   5        $ chkAutoTransList��5M   '   $��_Show transactions��6A   $	��140��6^   |�.  6|   �� 5N      -   5        $ chkWithoutReq��5N   '   $��Issue without requisition��6>   $	��151��6b   |�.  $��10,0,0,0q�6�   5O      5P      5Q      5R      �� 5S      $��1��6   $	��Top=�6%   
��� ���$��5��6O   � 5T      -   5        $ gridDetails��5T      $��2��6(   $	��Top=�65   $y�	Collapsed=�6M   $��	10,0,10,0q�6d   .+�5U      �� .+�5V      �� -   5        $ stkItem��5V   !   $ Vertical=�62   $	��Top=�6J   $��Stretch=�6b   .+�5X      �� -    5        $ stkpManualItemAdd��5X   %   $y�Visible=�6@   .+�5Y      �� $ 
Horizontal=�6)   .+�5Z   "   �� $ 
Horizontal=�6-   $y�	Collapsed=�6F   $��0,5,0,0q�6]   .+�5[   &   �� -!   5        $ lblUID��5[   ,   $��Item UI_D Mark:��6<   $	��100��6V   $��Right=�6b   	 6�   �� $
 txtUID��  5\   &   -"   5        $ txtUID��5\   <   #��[ 6L   $ Manufacturer's Product ID��6l   $��5,0,0,0q�6�   $��22��5]   ,   $	��150��68   )  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    AutoSelectBehavior$ OnFocus=�6Z   5^   #   �� 5_   "   $ 
Horizontal=�6-   .+�5`   &   �� -#   5        $ lblRSirCodeTitle��5`   ,   $��_Item Name :��6F   $
��Left=�6]   $��75��6   	 6�   �� $
 txtRSirDesc��  5a   &   -$   5        $ txtRSirCode��5a   <   #��[ 6Q   $ 	Item Code��6q   $��5,0,0,0q�6�   $��22��5b   ,   $	��100��68    . $ OnFocus=�6[    �� 
IsReadOnly . 6x     5d   &   -%   5        $ txtRSirDesc��5d   <   #��[ 6Q   $ Item Description��6q   $��5,0,0,0q�6�   $��22��5e   ,   $	��400��68    . $ OnFocus=�6Y   6v   5f   #   �� 5g   "   $ 
Horizontal=�6-   .+�5h   &   �� -&   5        $ lblSrfQtyTitle��5h   ,   $��Issue _Qty:��6D   $��Right=�6Z   $��60��6}   $��5,0,0,0q�6�   	 6�   �� $
 	txtSirQty��6�     5i   &   -'   5        $ 	txtSirQty��5i   <    � 
InputScope$ Number��6O    �� TextAlignment$ Right=�6c   #��[ 5j   <   $ Issue Quantity��6\   $��5,0,0,0q�6w   $��22��5k   ,   $��70��68    . $ OnFocus=�6Y   �� 5l   &   -(   5        $ lblUnit1��5l   ,   $
��Unit��6>   $��40��6M   �� 5m   &   -)   5        $ btnAddRecord��5m   -   #��[ 6C   $
��_Add��6�   $	��Top=�6�   $��22��5n   -   $��50��6:   6E   �� 5o   &   -*   5        $ lblStkBalDes��5o   ,   $�� ��6B   $	��285��6M   $��10(�6Y   �� 5p   &   -+   5        $ chkZeroStock��5p   /   $��Zero Stocks��6E   $��Center=�6[   |�.  $��90��6�   $��5,0,0,0q�6�   5q   #   5r      5s      �� 5u      -,   5        $ stkpReqList��5u   %   $
��Auto��6:   $	��Top=�6H   $��0,0,0,10q�6a   $y�Visible=�6s   .+�5v      �� $ 
Horizontal=�6)   $��0,0,0,5q�6B   .+�5w   "   �� --   5        $ 
lblSrfList��5w   (   $��_Prv. Req. List :��6<   $��Right=�6X   $��90��6{   	 6�   �� $
 
cmbSrfList���� 5x   "   -.   5        $ 
cmbSrfList��5x   +   $
��1010��6?    . $ 0��6^   ��5y   &   �� 5z   *   .R�5{   .   �  5|   2   �� 5}   6   $
��Auto �6G     z� SharedSizeGroup$  Column1��6T   �� 5~   6   $
��Auto �6G   $  Column2��6T   �� 5   6   $
��Auto �6G   $  Column3��6T   .+�5�   2   �� $ 
Horizontal=�6=   $��0��6V   $��0,1,0,0q�6f   .+�5�   6   �� $��Date : ��6@   �� 5�   6   $��90��6Z   ��6@   �� *	srfdat1+6Z   5�   3   �� 5�   2   $ 
Horizontal=�6=   $��1��6V   $��0,1,0,0q�6f   .+�5�   6   �� $��
Req. No : ��6@   �� 5�   6   $	��120��6Z   ��6@   �� *srfno1+6Z   5�   3   �� 5�   2   $ 
Horizontal=�6=   $��2��6V   $��0,1,0,0q�6f   .+�5�   6   �� $��Dept. : ��6@   �� 5�   6   ��6@   �� *
sectdes2+�� 5�   6   $��, Req. by : ��6@   �� 5�   6   ��6@   �� *
srfbynam+5�   3   5�   /   5�   +   5�   #   �� 5�   "   -/   5        $ btnAddAllRecord��5�   )   #��[ 6B   $��_Add All��6�   $	��Top=�5�   %   $��22��6>   $��75��6K   $��5,0,0,0q�6V   5�      &   System.Windows.Controls.DataGrid  5�      -0   5        !  Name$! 	dgvIssue1��5�   '   "   CanUserResizeRows" .  
������ #   
IsReadOnly# .  $��0q�6�   $   HeadersVisibility$$ Column=�6�   $y�Visible=�6�   $	��Top=�5�      $	��150��6%   $
��1170��65   $
��Auto=�6B   %   AutoGenerateColumns% .  &   CanUserAddRows& .  '   CanUserDeleteRows' .  $��Hidden=�5�      (   SelectionUnit$( FullRow=�6A   )   SelectionMode$) Single=�6Y   * �� AlternationCount$* 2��6p   +   AlternatingRowBackground+ �	#FFEDFFEE$	��5,0q�6�   , � 	Resources, 5�   "   *  $System.Windows.Controls.DataGridCell- �� 
TargetType. �� Triggers/ P� Property0 P� Value1 �� Property2 �� Value)  #System.Windows.Controls.DataGridRow��%
  '        '  �     �� 5�   &   "-  6,   . 5�   *   P� 5�   .   / �@ 
IsSelected0 . . �5�   2   �� 1 � ��
2 �����6P   �� 5�   2   1 � ��
2 �   �6P   5�   /   5�   '   �� 5�   &   "-  6,   .�5�   *   �� 1 � ��$2 25��6C   5�   '   5�   #   3   Columns3 5�   "   4	  .System.Windows.Controls.DataGridTemplateColumn	  5�   &   ,
  &System.Windows.Controls.DataGridColumn4 
  Width5  /System.Windows.Controls.DataGridLengthConverter$4 30 6=   5 
  HeaderTemplate5 5�   *   5  -System.Windows.Controls.ItemContainerTemplate�  5�   .   .R�5�   2   �� $��Center=�6<   $��Right=�6W   $	��Sl#��6s   $��30��6~   5�   /   6 	  CellTemplate6 5�   *   �� 5�   .   .R�5�   2   �� $ dgvlblSlNum��68   $��Right=�6M   $��Center=�6p   $��0,0,2,0q�6�   $��15��6�   ��6�   �� *slnum+  6�   �� *	sircode+5�   /   5�   '   	  5�   &   $4 30 6=   5 5�   *     5�   .   .R�5�   2   �� $	��Top=�6<   $�� ��6T   $��30��6\   5�   /   6 5�   *   �� 5�   .   .R�5�   2   �� $��0,0,2,0q�6�   $��15��6�   $��Right=�6�   7 d� 	IsChecked7 6;   �� $ mark1�8 �� Mode$8 TwoWay=�9 �� UpdateSourceTrigger$9 PropertyChanged=�{�6�   �� *	markable1+  6�   �� *	sircode+6�   5�   /   5�   '   	  5�   &   $4 90 6=   5 5�   *   �� 5�   .   .R�5�   2   �� $��	Item Code��6<   $ Center=�6M   
������ $��80��6}   $��15��6�   : �� Padding$: 5,2,0,0q�6�   5�   /   6 5�   *   �� 5�   .   .R�5�   2   �� $��Center=�6<   $��Center=�6Y   ��6t   �� *trcode+  6�   �� *
rsircode+5�   /   5�   '   	  5�   &   $	4 350 6=   5 5�   *     5�   .   .R�5�   2   �� $ 
Horizontal=�6=   .+�5�   6   �� $��Description of item��6A   $	��150��6\   $: 10,0,0,0q�6h   5�   3   5�   /   6 5�   *   �� 5�   .   .R�5�   2   �� $��Center=�6�   ��6<   �� *trdesc+  6T   �� *
rsircode+; � ToolTip; 6m   �� *	sirdesc+6�   5�   /   5�   '   	  5�   &   $4 50 6=   < 
  Header$
< Unit��6H   6 5�   *   �� 5�   .   .R�5�   2   �� $
��Left=�6<   $��Center=�6W   $: 5,0,5,0q�6�   ��6r   �� *trunit+  6�   �� *
rsircode+6�   5�   /   5�   '   	  5�   &   $4 60 6=   $< 	Req. Qty.��6H   6 5�   *   �� 5�   .   .R�5�   2   �� $��Right=�6<   $��Center=�6X   $: 5,0,5,0q�6�   ��6s   �� *srfqty+= �� StringFormat$ = {0: #,##0.00;(#,##0.00); }��  6�   �� *
rsircode+6�   5�   /   5�   '   	  5�   &   $4 80 6=   $< Prv. Issue Qty.��6H   6 5�   *   �� 5�   .   .R�5�   2   �� $��Right=�6<   $��Center=�6X   $: 5,0,5,0q�6�   ��6s   �� *	prvsirqty+$ = {0: #,##0.00;(#,##0.00); }��  6�   �� *	srqbalqty+6�   5�   /   5�   '   	  5�   &   $4 60 6=   $< Bal.Req��6H   6 5�   *   �� 5�   .   .R�5�   2   �� $��Right=�6<   $��Center=�6X   $: 5,0,5,0q�6�   ��6s   �� *	srqbalqty+$ = {0: #,##0.00;(#,##0.00); }��  6�   �� *	srqbalqty+6�   5�   /   5�   '   	  5   &   $4 80 6=   $< Month.Issue��6H   6 5  *   �� 5  .   .R�5  2   �� $��Right=�6<   $��Center=�6X   $: 5,0,5,0q�6�   ��6s   �� *	MissueQty+$ = {0: #,##0.00;(#,##0.00); }��  6�   �� *	srqbalqty+6�   5  /   5  '   	  5  &   $4 80 6=   $< 	Stock Qty��6H   6 5  *   �� 5	  .   .R�5
  2   �� $��Right=�6<   $��Center=�6X   $: 5,0,5,0q�6�   ��6s   �� *	stokqty+$#= {0: #,##0.00;(#,##0.00); Nil}��  6�   �� *
rsircode+6�   5  /   5  '   5     5     5     �� 5     $
��Auto��6"   $	��Top=�60   
�������.+�5       -1   5        $! dgvIssue��5  #   " .  
������ # .  $��0q�6{   $$ Column=�6�   $y�Visible=�6�   $	��Top=�5     $	��270��6%   $
��1170��62   $
��Auto=�6?   % .  & .  ' .  $��Hidden=�5     $( FullRow=�6A   $) Single=�6Y   $* 2��6p   + �	#FFEDFFEE, 5     ��%
  '        '  �     �� 5  "   "-  6(   . 5  &   P� 5  *   / �@ 
IsSelected0 . . �5  .   �� 1 � ��
2 �����6L   �� 5   .   1 � ��
2 �   �6L   5!  +   5#  #   �� 5$  "   "-  6(   .�5%  &   �� 1 � ��$2 25��6?   5&  #   5'     3 5(     	  5)  "   $4 30 69   5 5*  &     5+  *   .R�5,  .   �� $	��Top=�68   $	��Sl#��6P   $��30��6[   5-  +   6 5/  &   �� 50  *   .R�51  .   �� $ dgvlblSlNum��64   $��Right=�6I   $��Center=�6l   $��0,0,2,0q�6�   ��6�   �� *trsl+  6�   �� *	sircode+52  +   54  #   	  55  "   $	4 110 69   5 56  &   �� 57  *   .R�58  .   �� $��Req. No.��68   $ Center=�6H   
������ $��80��6x   $: 5,2,0,0q�6�   59  +   6 5;  &   �� 5<  *   .R�5=  .   �� $
��Left=�68   $��Center=�6S   ��6n   �� *srfno1+  6�   �� *srfno+5>  +   5@  #   	  5A  "   $4 90 69   5 5B  &   �� 5C  *   .R�5D  .   �� $��	Item Code��68   $ Center=�6I   
������ $��80��6y   $: 5,2,0,0q�6�   5E  +   6 5G  &   �� 5H  *   .R�5I  .   �� $
��Left=�68   $��Center=�6S   ��6n   �� *
rsircode+  6�   �� *
rsircode+5J  +   5L  #   	  5M  "   $	4 520 69   5 5N  &     5O  *   .R�5Q  .   �� $ 
Horizontal=�69   .+�5R  2   �� $ 
Horizontal=�6=   .+�5S  6   �� $��Description of item��6A   $	��150��6\   $: 10,0,0,0q�6h   5T  3   �� 5U  2   $ stkpNav1��6=   $ 
Horizontal=�6O   $
��Left=�6h   $��20��6�   $	��340��6�   $y�Visible=�6�   $��10,0,0,0q�6�   ���	#FFEEEDE4.+�5V  6   �� $ btnTop��6=   -2   5        $��|<��5V  M   $; First record��6]   $��25��6t   $
��Bold"�6   
������ $��0q�6�   $��0,0,0,0q�6�   |�.  6�   �� 5W  6   $ btnPrev��6=   -3   5        $��<��5W  N   $; Previous record��6]   $��25��6w   $
��Bold"�6�   
������ $��0q�6�   $��0,0,0,0q�6�   $��5,0,0,0q�6�   |�.  6�   �� 5X  6   $ btnNext��6=   -4   5        $��>��5X  N   $; Next record��6^   $��25��6t   $
��Bold"�6   
������ $��0q�6�   $��0,0,0,0q�6�   $��5,0,0,0q�6�   |�.  6�   �� 5Y  6   $ 	btnBottom��6=   -5   5        $��>|��5Y  P   $; Last record��6`   $��25��6v   $
��Bold"�6�   
������ $��0q�6�   $��0,0,0,0q�6�   $��5,0,0,0q�6�   |�.  6�   �� 5Z  6   $ 	btnDelete��6=   -6   5        $��[ x ]��5Z  P   $; Delete record��6`   $��25��6x   $
��Bold"�6�   
������ $��0q�6�   $��0,0,0,0q�6�   $��15,0,0,0q�6�   |�.  6�   �� 5[  6   $ chkMulReqAdd��6?   -7   5        $!��_Allow Multiple Requisition��5[  U   $	��160��6{   |�.  $��Center=�6�   $��5,0,0,0q�6�   $��15,4,0,0q�5\  ?   y�6l   �� $
 stkpReqList��$ 
Visibility�5]  3   5^  /   5_  +   6 5a  &   �� 5b  *   .R�5c  .   �� $��Center=�6�   ��68   �� *trdesc+  6P   �� *
rsircode+; 6i   �� *trdesc+6�   5d  +   5f  #   	  5g  "   $4 50 69   $
< Unit��6D   6 5h  &   �� 5i  *   .R�5j  .   �� $
��Left=�68   $��Center=�6S   $: 5,0,5,0q�6�   ��6n   �� *trunit+  6�   �� *
rsircode+6�   5k  +   5m  #   	  5n  "   $4 60 69   $< 	Req. Qty.��6D   6 5o  &   �� 5p  *   .R�5q  .   �� $��Right=�68   $��Center=�6T   $: 5,0,5,0q�6�   ��6o   �� *srfqty+$ = {0: #,##0.00;(#,##0.00); }��  6�   �� *
rsircode+6�   5r  +   5t  #   	  5u  "   $4 80 69   $< Prv. Issue Qty.��6D   6 5v  &   �� 5w  *   .R�5x  .   �� $��Right=�68   $��Center=�6T   $: 5,0,5,0q�6�   ��6o   �� *	prvsirqty+$ = {0: #,##0.00;(#,##0.00); }��  6�   �� *	srqbalqty+6�   5y  +   5{  #   	  5|  "   $	4 100 69   $< New Issue Qty��6E   6 5}  &   �� 5~  *   .R�5  .     -8   5        #��[ 5  D   $��Right=�5�  o   $��0q�6�   
�������$ OnFocus=�6�   ��5  d   �� $ sirqty�$8 TwoWay=�$9 PropertyChanged=�$= {0: #,##0.00}��5�  �   5�  +   5�  #   	  5�  "   $4 80 69   $< 	Stock Qty��6D   6 5�  &   �� 5�  *   .R�5�  .   �� $��Right=�68   $��Center=�6T   $: 5,0,5,0q�6�   ��6o   �� *	stokqty+$#= {0: #,##0.00;(#,##0.00); Nil}��  6�   �� *
rsircode+6�   5�  +   5�  #   5�     5�     5�     5�     � 5�     -9   5        $ gridTransList��5�     $��2��6*   $	��Top=�67   
�������$y�Visible=�6c   $��	10,0,10,0q�6x   ��5�     �� 5�     "- �6   . 5�     P� 5�     / � {�0 . > N� EnterActions> 5�  "   �� 5�  &   .��5�  *   �� .��5�  .   W� ? �� TargetProperty$? (Grid.Height)�6>   @ W� From$@ 0X�6h   	A W� To$	A 450X�6q   B g� Duration$B 0:0:1D�6z   W� 5�  .   $@ 0X�6>   $A 1X�6G   $B 00:00:2D�6N   $? Opacity�6a   5�  +   5�  '   5�     5�     .+�5�     �� $ Vertical=�6   $	��Top=�64   $	��450��6L   .+�5�     �� $ 
Horizontal=�6!   .+�5�     �� $ 
Horizontal=�6%   $
��Left=�6>   .+�5�     �� -:   5        $ btnRecurring��5�  %   $��	Recurring��6;   #��[ 6O   $��80��6v   $��22��6�   {�.  6�   �� 5�     $��From :��6$   $��12(�65   $��Italic$�6C   $��45��6V   $
��Left=�6a     5�     -;   5        $ xctk_dtpFromDate��5�  2   #��[ 6L   $��20��6~   $	��100��6�   �� 5�     $
��To :��6$   $��Italic$�63   $��28��6F   $��Center=�6Q     5�     -<   5        $ xctk_dtpToDate��5�  2   #��[ 6J   $��20��6|   $	��100��6�   �� 5�     -=   5        $ chkSelectedSender��5�  '   $	��120��6B   $��Selected Sender��6N   $��Center=�6h   $
7 TrueY�6�   $��10,0,0,0q�6�   �� 5�     ->   5        $ chkSelectedReceiver��5�  '   $	��120��6D   $��Selected Receiver��6P   $��Center=�6l   $
7 TrueY�6�   $��10,0,0,0q�6�   �� 5�     -?   5        $ 	btnFilter��5�  %   $��	Find List��68   $��20��6L   $��80��6X   $��12(�6c   $��Verdana)�6q   #��[ 5�  )   $��10,0,0,0q�6h   5�     �� 5�     $ 
Horizontal=�6%   $��Right=�6>   $��Right=�6Z   .+�5�     �� -@   5        $ 	chkPrint2��5�  '   $7 FalseY�6:   $��Print Memo/List��6L   $��Center=�6f   $��Center=�6�   $��15,0,0,0q�6�   � 5�     C � AllowsTransparencyC . $��Slide=�6�   ��6$   �� $
 	chkPrint2��$ 	IsChecked�D � PlacementTargetD 6]   �� $
 	chkPrint2��.��5�  "   �� $	��134��6)   $	��146��65   ��5�  &   �� 5�  *   E �� EndPoint$E 0.5,1&�6>   F �� 
StartPoint$F 0.5,0&�6O   .��5�  .   � G � Color$G 	#FFE4F2E1��6;   H � Offset$H 0.491Q�6M   � 5�  .   $G 	#FF69B28B��6;   $H 0.957Q�6M   5�  +   z�5�  &   �� 5�  *   -A   5        theTransform2��5�  :   .+�5�  &   �� $ Vertical=�61   $	��134��6H   .+�5�  *   �� $��Print Options��60   $��12(�6H   $��Italic$�6V   $
��Bold"�6i   
���**��$
��Left=�6�   �� 5�  *   $ Vertical=�65   $��10,0,0,0q�6L   .+�5�  .   	� -B   5        I 	�Name$I rb3SelectedMemopr��5�  :   $��Selected Memo��6U   $
7 TrueY�6m   �� 5�  .   $y�Hidden=�68   	� 5�  .   -C   5        $I rb3TableRecoredspr��5�  :   $��Table Records��6V   $7 FalseY�6n   5�  +   �� 5�  *   �� 5�  *   $ Vertical=�65   $��10,2,0,0q�6L   .+�5�  .   	� -D   5        $I rb3QuickPrintpr��5�  :   $��Quick Print��6T   $7 FalseY�6j   �� 5�  .   $y�Hidden=�68   	� 5�  .   -E   5        $I rb3PrintPreviewpr��5�  :   $��Print Preview��6U   $
7 TrueY�6m   5�  +   5�  '   �� 5�  &   -F   5        $ 	btnPrint3��5�  -   $��Go Print��6@   $��20��6S   $��60��6_   $��12(�6j   $��Verdana)�6x   #��[ 6�   J �� Left$J 34��6�   
K �� Top$	K 116��6�   5�  #   5�     �� 5�     -G   5        $ cmdEditIssueMemo��5�  %   $
��Edit��6?   #��[ 6N   $��80��6u   $��22��6�   $��20,0,0,0q�6�   �� 5�     -H   5        $ btnEditAuthorize��5�  %   $��Authorization��6?   #��[ 6W   $��80��6~   $��22��6�   $��20,0,0,0q�6�   �� 5�     -I   5        $ cmdCancelIssueMemo��5�  %   $��Cancel��6A   #��[ 6R   $��80��6y   $��22��6�   $��70,0,0,0q�6�   6�   5�     5�     �� 5�     $��5��6    �� 5�     $ 
Horizontal=�6!   $
��Left=�6:   .+�5�     �� $!��Existing Transactions List:��6$   $��14(�6G   $��Italic$�6U   $
��Bold"�6h   
����� �$	��180��6�   �� 5�     -J   5        $ txtTransTitle��5�  $   $'��!From: 01-Dec-2015 To: 15-Dec-2015��6;   $ Center=�6d   $	��800��6{   $
��Bold"�6�   5�       5�     -K   5        $! dgvTransList��5�     " .  
������ $��0q�6h   $��11(�6|   $��5,3,5,0q�6�   $) Single=�6�   $	��Top=�5�  #   $	��375��6;   % .  & .  ' .  $��Hidden=�6�   $( FullRow=�5�  #   $* 2��6l   + �	#FFEDFFEE, 5�     =  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�  '        '  �     '  l    �� 5�     "-  6$   .�5�  "   �� 1 � ��2 �	#FFE2D2CE6?   �� 5�  "   1 � ��$
2 Bold"�6?   �� 5�  "   1 � ��$2 25��6;   �� 5�  "   1 � ��$2 12(�6=   5�     �� 5�     "-  6$   . 5�  "   P� 5�  &   / �@ 
IsSelected0 . . �5�  *   �� 1 � ��
2 �����6H   �� 5�  *   1 � ��
2 �   �6H   5�  '   5�     �� 5�     "-  6$   .�5�  "   �� 1 � ��$2 25��6;   5�     5�     3 5�     	  5�     $4 40 65   5 5�  "     5�  &   .R�5�  *   �� $ Center=�64   $	��Sl#��6K   $��40��6V   5�  '   6 5�  "   �� 5�  &   .R�5�  *   �� $ Right=�64   $��Center=�6J   $: 0,0,2,0q�6�   ��6e   �� *slnum+$= {0: ###0) }��  6�   �� *	memonum+5�  '   5�     	  5�     $4 75 65   5 5�  "     5�  &   .R�5   *   �� $ Center=�64   $��S.I.R. Date��6K   5  '   6 5  "   �� 5  &   .R�5  *   �� $ Center=�6T   $��Center=�6k   ��64   �� $ 	memoDate1�  6�   �� *	memoDate1+5  '   5     	  5	     $	4 110 66   5 5
  "     5  &   .R�5  *   �� $ Center=�64   $��
S.I.R. No.��6K   $��80��6]   5  '   6 5  "   �� 5  &   .R�5  *   �� $ Center=�64   $��Center=�6K   ��6f   �� *
memonum1+; 6�   �� *	Referance+5  '   5     	  5     $	4 300 66   5 5  "     5  &   .R�5  *   �� $ Center=�64   $��
Issue From��6K   $	��120��6]   5  '   6 5  "   �� 5  &   .R�5  *   �� $��Center=�64   ��6O   �� *
sectname+  6j   �� *
PreparById+5  '   5      	  5!     $	4 200 66   5 5"  "     5#  &   .R�5$  *   �� $ Center=�64   $��Issue To��6K   $	��150��6[   5%  '   6 5'  "   �� 5(  &   .R�5)  *   �� $��Center=�64   ��6O   �� *	sectname2+5*  '   5,     	  5-     $	4 180 66   5 5.  "     5/  &   .R�50  *   �� $ Center=�64   $��Received By��6K   $	��120��6^   51  '   6 53  "   �� 54  &   .R�55  *   �� $��Center=�64   ��6O   �� *
recvbyName+56  '   58     	  59     $	4 100 66   5 5:  "     5;  &   .R�5<  *   �� $ Center=�64   $��	Reference��6K   $	��100��6\   5=  '   6 5?  "   �� 5@  &   .R�5A  *   �� $��Center=�64   ��6O   �� *	Referance+; 6j   �� *
Naration+5B  '   5D     	  5E     $4 40 66   5 5F  "     5G  &   .R�5H  *   �� $ Center=�64   $��Status��6K   $��40��6Y   5I  '   6 5K  "   �� 5L  &   .R�5M  *   �� $ Center=�64   $��Center=�6K   ��6f   �� *
MemoStatus+; 6�   �� *
Naration+5N  '   5P     	  5Q     $	4 100 66   5 5R  "     5S  &   .R�5T  *   �� $��Status Desc.��64   $��80��6H   5U  '   6 5W  "   �� 5X  &   .R�5Y  *   �� $��Center=�64   ��6O   �� *
Naration+; 6i   �� *
Naration+5Z  '   5\     5^     5_     5`     � 5a     -L   5        $ GridItemList��5a     $��2��6)   $��	45,24,0,0q�66   $	��540��6I   $	��400��6U   $y�	Collapsed=�6b   $	��Top=�6y   $
��Left=�6�   
�������.+�5b     �� $ 
Horizontal=�6   .+�5c     �� $��_List :��6   $��40��6U   $��Right=�6`   	 6.   �� $
 lstItem��6`   �� 5d     -M   5        L ��Name$L lstItem��5d     $	��400��6/   $	��600��6<   
������ $	��Top=�6a   $
��Auto=�6y   $��12(�6�   $��Hidden=�5e  	   M �� SelectionMode$M Single=�6=   ��5f     �� 5g     .R�5h  "   �  5i  &   �� 5j  *   $
��Auto �6;   $  Column1��6H   �� 5k  *   $
��Auto �6;   $  Column2��6H   �� 5l  *   $
��Auto �6;   $  Column3��6H   �� 5m  *   $
��Auto �6;   $  Column3��6H   .+�5o  &   �� $��0��60   $��90��6Y   
�������$��0,1,0,0q�6w   ��6@   �� *	sircode+6w   �� 5p  &   $��1��60   $	��250��6Y   
�������$��0,1,0,0q�6�   ��6@   �� *	sirdesc+; 6e   �� *	sirdesc+6�   �� 5q  &   $��2��60   $��40��6Y   
�������$��0,1,0,0q�6w   ��6@   �� *	sirunit+6w   �� 5r  &   $��3��60   $ Right=�6�   $��80��6�   
�������$��0,1,0,0q�6�   ��6@   �� *stkqty+$"= {0: #,##0.00;-#,##0.00;Nil }��6�   5s  #   5t     5v     5w     5x     5y     � 5z  
   -N   5        $ gridAuthorize��5z     $y�Visible=�6&   {�.  $
��Left=�6M   $��Bottom=�6h   $��	10,0,0,60q�6�   ��5{     �� 5|     "- �6   . 5}     P� 5~     / � {�0 . > 5     �� 5�  "   .��5�  &   �� .��5�  *   W� $? (Grid.Height)�6:   $@ 0X�6d   $	A 600X�6m   $B 0:0:1D�6v   W� 5�  *   $? (StackPanel.Width)�6:   $@ 0X�6i   $	A 360X�6r   $B 0:0:0.5D�6{   N g� AutoReverseN .  6�   W� 5�  *   $@ 0X�6:   $A 1X�6C   $B 00:00:2D�6J   $? Opacity�6]   5�  '   5�  #   5�     5�     .+�5�     �� $��Bottom=�6   
�������.+�5�     �� $ 
Horizontal=�6   .+�5�     �� -O   5        $ lblAuthorizeMemoDesc��5�     $4��.Date: 01-Sep-2018,  S.I.R No: SIR09-1011-00005��6:   
���  ��$	��350��6�   $
��Bold"�6�   $��14(�6�   $
  NONE��6�   5�     �� 5�     .+�5�       -P   5        $! dgvAuthorize��5�     " .  $��0q�6O   $) Single=�6c   $	��Top=�6z   % .  & .  ' .  $��Hidden=�5�  P   $
$ None=�6�   O   GridLinesVisibility$
O None=�6�   $	��350��5�     $	��360��6/   3 5�     	  5�     $	4 340 65   6 5�  "   �� 5�  &   .R�5�  *   �� 
���   �$��1q�6G   $��85��6[   $	��335��6j   ��5�  .   �� 5�  2   $
��Bold"�6<   
���  ��$��14(�6b   �� 5�  6   $ 
Horizontal=�6A   .+�5�  :   �� ��6D   �� $ slnum��� 5�  :   ��6D   �� $ autitle��� 5�  :   $��:��6D   5�  7   5�  5   .��5�  .   �� .+�5�  2   �� $
��Bold"�6�   $	��330��6�   ��6<   �� $ auhcnam�; 6\   �� $ auhcnam�  6   �� $ auhcid�6�   �� 5�  2   $ 
Horizontal=�6=   $��0,5,0,5q�6V   .+�5�  6   �� $��	Status : ��6@   $��40��6Q   �� 5�  6   $��60��6`   $
��Bold"�6k   ��6@   �� $ statdes�6k   �� 5�  6   $��Date & Time : ��6@   $��80��6Z   �� 5�  6   $	��150��6�   ��6@   �� $ autime�$= {0: dd-MMM-yyyy hh:mm tt}��6�   5�  3   �� 5�  2   $ 
Horizontal=�6=   y�6V   �� $ notevisible�.+�5�  6   �� $��Note : ��6@   $��40��6O   �� 5�  6   $	��280��6|   ��6@   �� $ aunote�; 6_   �� *aunote+6|   5�  3   5�  /   5�  +   5�  '   5�     5�     5�     �� 5�     -Q   5        $ 
StkpCurAut��5�     $y�Visible=�61   .+�5�     �� $
��Left=�6   
���  ��$��1q�6L   $	��110��6`   $	��355��6m   ��5�     �� 5�     -R   5        $ TxtbCurAuHeader��5�  (   $��Curr. Auth. By : ��6A   $
��Bold"�6Z   
���  ��$   ��6}   $��14(�6�   .��5�     �� $��90��6%   .+�5�     �� $
��Left=�6)   $��0,2,0,0q�6D   .+�5�  "     -S   5        $ autoAutByStaffSearch��5�  2   $) #autoAutByStaffSearch_PatternChanged��5�     $	��340��5�  P   $ sircode��6\   $ sirdesc��6x   #��[ 5�  T   $	 500��6�   $��24��6�   ; 5�     �� $
 Text�$
 autoAutByStaffSearch��5�  �   5�     �� 5�     $ 
Horizontal=�6)   $��0,3,0,4q�6B   .+�5�  "   �� $��Status :��6,   $��25��6<   $��40��6H   �� 5�  "   -T   5        $ 	cmbCurAut��5�  +   $��80��6>   $��22��6I   $ 1��6U   $��0q�6g   .@�5�  &   �� $��Pending��63   $  U��6E   �� 5�  &   $��Accepted��63   $  Y��6F   �� 5�  &   $��Rejected��63   $  N��6F   5�  #   �� 5�  "   $��Date & Time : ��6,   $��80��6F   $��10,0,0,0q�6Q   �� 5�  "   -U   5        $ txtbCurAuTime��5�  ,   $��09-Sep-2018 11:20 AM��6C   $	��130��6_   5�     �� 5�     $ 
Horizontal=�6)   .+�5�  "   �� $��Note :��6,   $��40��6:   �� 5�  "   -V   5        $ txtCurAutNote��5�  *   $�� ��6A   $��24��6~   $	��300��6�   ; 6I   �� $
 Text�$
 	txtChkNar��6�   5�     5�     5�     5�     �� 5�     $ 
Horizontal=�6   $��Right=�66   $��2,2,5,5q�6R   .+�5�     �� -W   5        $ btnCancelAuthorize��5�     $��Close without change��69   #��[ 6X   $��Right=�6   $	��140��6�   $��22��6�   $��0,5,20,0q�5�  >   �� 5�     -X   5        $ btnOkAuthorize��5�     $��Ok��65   #��[ 6B   $��Right=�5�     $��75��69   $��22��6D   $��	70,5,10,0q�6m   y�5�  i   �� $ 
Visibility�$
 
StkpCurAut��5�  m   5�     5�     5�     5�     5�     
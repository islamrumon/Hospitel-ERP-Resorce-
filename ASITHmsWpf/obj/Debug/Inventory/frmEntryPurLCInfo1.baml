   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      -    'ASITHmsWpf.Inventory.frmEntryPurLCInfo1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .UC%clr-namespace:ASITHmsWpf.UserControls  5      -   5        
  � Tag$7  1Import L/C Information - Entry/Edit/Status Screen��5         IOUserControl#��[  6W   $��14(�5	   *   .��5
      � .+�5   
   �� .+�5      �� $
��Left=�6    �� Orientation$ 
Horizontal=�64   $	��680��6M   .+�5      �� $��New/Selected Transaction:��6   $��14(�6=   $��Italic$�6K   $
��Bold"�6^   
����� �$	��180��6�   �� 5      -   5         ��Name$ txtblEditMode��5      $y�Hidden=�63   $��[ LC Info. In Edit Mode ]��6G   $��14(�6h    �� TextAlignment$ Center=�6v   
��� ���$
��Bold"�6�   
���  ��$	��180��6�   $��20,0,0,0q�6�   5      �� 5      -   5         ��Name$ stkIntro��5      $ Vertical=�6+   $	��Top=�6B   $
��Left=�6Z   $��0,3,0,0q�6u   .+�5      �� $ 
Horizontal=�6   .+�5      �� $ 
Horizontal=�6!   .+�5      �� -   5         ��Name$ lblPlcTitle��5       $��_L/C Description & No :��65   $��Right=�6[   $	��150��6~    �� Target 6�   ��  �� ElementName$ 	AtxtPlcId��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  5      -   5          Name$ 	AtxtPlcId��5   1   1   +ASITHmsWpf.UserControls.AutoCompleteTextBox	   MouseDoubleClick$ 	 AtxtPlcId_MouseDoubleClick��5   P   
   
SearchType$
 C��5   D   $
��Left=�6S   $	��550��6u   $
��Bold"�6�    � ToolTip$2 ,Right Click or Double Click to view L/C list��5       � ContextMenu 5      �� 5   "   -   5         ��Name$ conMenuPlcId��5   .   $	��400��6D   $	��550��6Q   5      �� 5      -   5        $ 
lblLCDesc1��5       $�� ��64   $	��550��6?   $y�	Collapsed=�6K   $
��Bold"�6b   5      �� 5      $ 
Horizontal=�6!   .+�5      �� -   5         ��Name$ btnOk��5   !     StdBtnStyle01#��[ 60   $	��_Ok��6W   $��75��6f   $��10,0,0,0q�6�   �� 5      -	   5        $ btnPrint��5   !   #��[ 63   $��_Print��6Z   $��75��6l   $��	100,0,0,0q�6�   �� 5      -
   5        $ 	btnUpdate��5   !   #��[ 64   $��_Update��6[   $��75��6m   $��	100,0,0,0q�6�   5       5!      5"      �� 5#      $	��Top=�6   
��� ���$��5��6B   �� 5$      -   5        $ stkpLCDetailsInfo��5$      .+�5%      �� $ 
Horizontal=�6   .+�5&      �� $	��700��6!   $	��300��6-   $y�Visible=�6:   
�������$	��Top=�6f   .+�5'      �� $��L/C General Information :��6    $
��Bold"�6D   &   System.Windows.Controls.DataGrid  5(      -   5          Name$ dgLCGenInfo��5(   #      CanUserResizeRows .  
������ $��0q�6k   $��0,0,0,0q�6   $��12(�6�      GridLinesVisibility$ 
Horizontal=�6�   $	��Top=�5)      $	��270��67   $	��700��6D   $
��Auto=�6P      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     HeadersVisibility$
 None=�5*   9   $��Hidden=�6R      SelectionUnit$ FullRow=�6�      SelectionMode$ Single=�6�      Columns 5+      4  .System.Windows.Controls.DataGridTemplateColumn  5,   "   ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 69      Header$	 Sl#��6D      CellTemplate 5-   &   �� 5.   *   .R�5/   .   �� $ 
Horizontal=�69   $��Right=�6R   $	��Top=�6n   $��40��6�   .+�50   2   �� $ lbldgExtraInfoRptSlno��68   -   5        $��Right=�50   W   $��22��6z   $��30��6�   $	��Top=�6�   $��0,0,2,0q�51   m     50   �   �� *	actcode+��51   8   �� *slnum+ �� StringFormat$ 
{0: ##0) }��6   �� 52   2   $��.��68   $	��Top=�6D   $��0,0,2,0q�6]   53   /   54   +   56   #     57   "   $	 220 69   $ Title Description��6E    58   &   �� 59   *   .R�5:   .   �� $ 
Horizontal=�69   .+�5;   2   �� $	��210��6q   ��6<   �� *	actdesc+ 6U   �� *	actdesc+6q   �� 5<   2   $��Center=�6<   $	��Top=�6Y   $��10��6q   $��:��6|   $
��Bold"�6�   5=   /   5>   +   5@   #     5A   "   $ 30 6:   $  ��6E      
Visibility$ 	Collapsed=�6P    5B   &   �� 5C   *   .R�5D   .   � $��Right=�63   $	��Top=�6O   $��30��6g   .+�5E   2   �� $��Right=�6<   $	��Top=�6X   ��6p   �� *
repeatsl+5F   /   5G   +   5I   #     5J   "   $	 420 69   $ Value Description��6E    5K   &   �� 5L   *   .R�5M   .   �� $ 
Horizontal=�69   .+�5N   2   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  $��0q�6H   )  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoSelectBehavior$ OnFocus=�6\   $��0,0,0,0q�6y    
 wtxtBx#��[ 6�   ��6�   ��  �� Path$ 	gendataw1���5O   2   �� $ 	lcgendata�  �� Mode$  TwoWay=�! �� UpdateSourceTrigger$! PropertyChanged=��� 5P   2   
���  ����6<   �� $ 
lcgendata2�$  TwoWay=�$! PropertyChanged=���6�   �� $ 	gendataw2�6�   5Q   /   5R   +   5T   #   5V      5W      �� 5X      
�������$	��300��68   $��10,0,0,0q�6E   .+�5Y      �� $ 
Horizontal=�6%   .+�5Z      �� $��Payment Information : ��6$   $
��Bold"�6E   $	��160��6W   5[        5\      -   5        $ dgLcPayInfo��5\   #    .  
������ $��0q�6k   $��0,0,0,0q�6   $��12(�6�   $	��Top=�5]      $	��210��67   $	��490��6G   $
��Auto=�6S    .   .   .  $ Column=�5^   9   $��Hidden=�6T   $ FullRow=�6�   $ Single=�6�    5_        5`   "   $ 40 69   $	 Sl#��6D    5a   &   �� 5b   *   .R�5c   .   �� $ 
Horizontal=�69   $��Right=�6R   $	��Top=�6n   $��40��6�   .+�5d   2   �� $��Right=�68   $��22��6[   $��30��6g   $	��Top=�6r   $��0,0,2,0q�5e   m     5d   �   �� *	actcode+��5e   8   �� *slnum+$ 
{0: ##0) }��6m   �� 5f   2   $��.��68   $	��Top=�6D   $��0,0,2,0q�6]   5g   /   5h   +   5j   #     5k   "   $	 250 69   $ Title Description��6E    5l   &   �� 5m   *   .R�5n   .   �� $ 
Horizontal=�69   .+�5o   2   �� $	��230��6q   ��6<   �� *	actdesc+ 6U   �� *	actdesc+6q   5p   /   5q   +   5s   #     5t   "   $	 110 69   $     Paid Amount��6E    5u   &   �� 5v   *   .R�5w   .   �� " �� Padding$" 0,0,5,0q�68   
������ $	��110��6�   $ Right=�6�   ��6d   �� $ payamt�$  {0: #,##0.00;(#,##0.00); }��6�   5x   +   5z   #     5{   "   $ 70 69   $ 
    Paid %��6D    5|   &   �� 5}   *   .R�5~   .   �� $ 
Horizontal=�69   $��Right=�6R   $	��Top=�6n   $��70��6�   .+�5   2   �� $" 0,0,0,0q�6<   
������ $ Right=�6g   $��50��6�   ��6~   �� $ payper�$# {0: #,##0.00;(#,##0.00);0.00}��6�   �� 5�   2   $�� %��68   $	��Top=�6E   $��0,0,2,0q�6^   5�   /   5�   +   5�   #   5�      �� 5�      $��12(�6!   
������ $��0q�6H   {�.  ��5�      �� 5�   "   .+�5�   &   �� $ 
Horizontal=�61   .+�5�   *   �� $��TOTAL PAYMENT [ A + B ]��64   $
��Bold"�6S   $	��255��6e   $ Right=�6q   �� 5�   *   $�� :��64   $
��Bold"�6>   $��20��6P   �� 5�   *   -   5        $ txtTotalPayment��5�   4   $�� ��6M   $
��Bold"�6U   $ Right=�6g   $	��110��6}   �� 5�   *   $��100.00 %��64   $
��Bold"�6D   $ Right=�6V   $��75��6l   5�   '   �� 5�   &   $ 
Horizontal=�61   $��0,5,0,5q�6J   .+�5�   *   �� $*��$TOTAL L/C VALUE PAID [ A = a1 + a2 ]��64   $
��Bold"�6`   $	��255��6r   $ Right=�6~   �� 5�   *   $�� :��64   $
��Bold"�6>   $��20��6P   �� 5�   *   -   5        $ txtLCValuePaid��5�   4   $�� ��6L   $
��Bold"�6T   $ Right=�6f   $	��110��6|   �� 5�   *   -   5        $ txtLCValuePaidPer��5�   4   $�� ��6O   $
��Bold"�6W   $ Right=�6i   $��75��6   5�   '   �� 5�   &   $ 
Horizontal=�61   $��0,0,0,5q�6J   .+�5�   *   �� $��L/C MARGIN - a1 ��64   $
��Bold"�6L   $	��255��6^   $ Right=�6j   �� 5�   *   $�� :��64   $
��Bold"�6>   $��20��6P   �� 5�   *   -   5        $ txtTotalLCMargin��5�   4   $�� ��6N   $
��Bold"�6V   $ Right=�6h   $	��110��6~   �� 5�   *   -   5        $ txtTotalLCMarginPer��5�   4   $�� ��6Q   $
��Bold"�6Y   $ Right=�6k   $��75��6�   5�   '   �� 5�   &   $ 
Horizontal=�61   .+�5�   *   �� $��INVOICE VALUE - a2 ��64   $
��Bold"�6O   $	��255��6a   $ Right=�6m   �� 5�   *   $�� :��64   $
��Bold"�6>   $��20��6P   �� 5�   *   -   5        $ txtTotalLCInvValuPaid��5�   4   $�� ��6S   $
��Bold"�6[   $ Right=�6m   $	��110��6�   �� 5�   *   -   5        $ txtTotalLCInvValuPaidPer��5�   4   $�� ��6V   $
��Bold"�6^   $ Right=�6p   $��75��6�   5�   '   �� 5�   &   $ 
Horizontal=�61   $��0,5,0,5q�6J   .+�5�   *   �� $��TOTAL OVERHEAD PAID [ B ]��64   $
��Bold"�6U   $	��255��6g   $ Right=�6s   �� 5�   *   $�� :��64   $
��Bold"�6>   $��20��6P   �� 5�   *   -   5        $ txtEstTotalLCOverheadPaid��5�   4   $�� ��6W   $
��Bold"�6_   $ Right=�6q   $	��110��6�   �� 5�   *   -   5        $" txtEstTotalLCOverheadPaidPer��5�   4   $�� ��6Z   $
��Bold"�6b   $ Right=�6t   $��75��6�   5�   '   5�   #   5�      5�      5�      �� 5�      
�������$	��300��67   .+�5�      �� $ 
Horizontal=�6!   .+�5�      �� $'��!L/C Item(s) Details Information :��6    $
��Bold"�6L   $	��220��6^   �� 5�      -   5        $ lblRSirCodeTitle��5�       $��L/C _Item :��6:   $��Right=�6P   $��70��6s    6~   �� $ autoRSirDescSearch��*	   $ASITHmsWpf.UserControls.AutoComplete	  5�      -   5        # 	 Name$# autoRSirDescSearch��5�   *   *
   $ASITHmsWpf.UserControls.AutoComplete$ 
  PatternChanged$'$ !autoRSirDescSearch_PatternChanged��6�   % 
  	LostFocus$"% autoRSirDescSearch_LostFocus��5�   g   $	��470��5�   F   & �� SelectedValuePath$& sircode��6R   ' �� DisplayMemberPath$' sirdesc1��6n    % !AutoCompleteComboBoxWithoutButton#��[ 5�      ( 	  Delay$	( 500��6O   $��24��6[   6g   �� 5�      -   5        $ lblmrrQtyTitlepr��5�       $��_Qty :��6:   $��Right=�6K   $��40��6n   $��0,2,2,0q�6�    6y   �� $ txtLCQty��6�     5�      -   5        )  Name$) txtLCQty��5�   0   * �� TextAlignment$* Right=�6B   + �� TextWrapping$
+ Wrap=�6X   ,   AutoMoveFocus, . $ OnFocus=�5�      $��5,0,0,0q�61   #��[ 6C   -   	Watermark$- Quantity��6c   $��24��6x   $��80��6�   �� 5�      -   5        . ��Name$. 
cmbQtyUnit��5�   #   $��50��67   $��0q�6B   $��2,2,0,2q�6V   / �� SelectedIndex$/ 0��6h   $��Center=�6z   {�.  6�   �� 5�      -   5        $. cmbRateUnit��5�   #   $��80��68   $��Right=�6C   $��0q�6f   $��2,2,0,0q�6z   $/ 0��6�   $��Center=�6�   .@�5�      �� $��	Rate /Pcs��6+   $ Invoice with balance��6?   $  INV01��6^   �� 5�      $��	Rate /Ban��6+   $ Invoice without balance��6?   $  INV02��6a   5�      �� 5�      -   5        $ lblUnit1��5�       $
��Unit��62   $��35��6A   $��0,5,0,0q�6L   $y�	Collapsed=�6^     5�      -   5        $) 	txtrqRate��5�   0   $* Right=�6C   $��0��6Y   $
+ Wrap=�6f   , . $ OnFocus=�5�      $��5,0,0,0q�61   #��[ 6C   $
- Rate��5�      $�� ��6%   $
��Left=�6-   $��22��6O   $	��100��6[   �� 5�      -    5        $ btnAddRecord��5�   !   $��5,0,0,0q�67   #��[ 6H   $��	_Add Item��6o   $��24��6�   $��75��6�   6�   5�      �� 5�      $
��Left=�6!   .+�5�        -!   5        $ dgLCItemInfo��5�   #    .  
������ $��0q�6l   $��0,0,0,0q�6�   $��12(�6�   0   FrozenColumnCount$0 11��6�   $	��Top=�5�      $	��180��6,   $
��1200��6<   $
��Auto=�6I   $
��Auto=�6y    .   .   .  $ FullRow=�5�   /   $ Single=�6G    5�        5�   "   $ 30 69   $	 Sl#��6D    5�   &   �� 5�   *   .R�5�   .   �� $ 
Horizontal=�69   $��Right=�6R   $	��Top=�6n   $��30��6�   .+�5�   2   �� $��Right=�68   $��24��6[   $��25��6g   $	��Top=�6r   $��0,0,2,0q�5�   O     5�   �   �� *	sircode+��5�      �� *slnum+$ 
{0: ##0) }��6O   �� 5�   2   $��.��68   $	��Top=�6D   $��0,0,2,0q�6]   5�   /   5�   +   5�   #     5�   "   $	 220 69   $ Title Description��6E    5�   &   �� 5�   *   .R�5�   .   �� $	��220��6m   ��68   �� *	sirrmrk+ 6Q   �� *	sirdesc+6m   5�   +   5�   #     5�   "   $ 30 6:   $
 Unit��6E    5�   &   �� 5�   *   .R�5�   .   �� $��Right=�68   $	��Top=�6T   ��6l   �� *	sirunit+5�   +   5�   #     5�   "   $ 50 69   $
 Qty.��6D    5�   &   �� 5�   *   .R�5�   .     $��0q�6D   $ OnFocus=�6X   $��0,0,5,0q�6u   #��[ 6�   
������ ��5�   3   �� *lcqty+$  TwoWay=�$  {0: #,##0.00;(#,##0.00); }��$! PropertyChanged=�5�   +   5�   #     5�   "   $ 70 69   $ 	FC Rate-1��6D    5�   &   �� 5�   *   .R�5�   .     $��0q�6D   $ OnFocus=�6X   $��0,0,5,0q�6u   #��[ 6�   
������ ��5�   3   �� *fcrat1+$  TwoWay=�$  {0: #,##0.00;(#,##0.00); }��$! PropertyChanged=�5�   +   5�   #     5�   "   $ 90 69   $ 	FC Amount��6D    5�   &   �� 5�   *   .R�5�   .   �� $" 0,0,5,0q�68   
������ $��90��6�   $ Right=�6�   ��6d   �� *fcval1+$  {0: #,##0.00;(#,##0.00); }��6�   5�   +   5�   #     5�   "   $	 100 69   $ Amount-1 (Taka)��6E    5�   &   �� 5�   *   .R�5�   .   �� $" 0,0,5,0q�68   
������ $	��100��6�   $ Right=�6�   ��6d   �� *dcval1+$  {0: #,##0.00;(#,##0.00); }��6�   5�   +   5   #     5  "   $	 100 69   $ Rate-1 (Taka)��6E    5  &   �� 5  *   .R�5  .   �� $" 0,0,5,0q�68   
������ $	��100��6�   $ Right=�6�   ��6d   �� *dcrat1+$  {0: #,##0.00;(#,##0.00); }��6�   5  +   5  #     5  "   $ 70 69   $ 
Over ( % )��6D    5	  &   �� 5
  *   .R�5  .   �� $ 
Horizontal=�69   $��Right=�6R   $	��Top=�6n   $��70��6�   .+�5  2   �� $" 0,0,0,0q�6<   
������ $ Right=�6g   $��50��6�   ��6~   �� *	overper+$# {0: #,##0.00;(#,##0.00);0.00}��6�   �� 5  2   $�� %��68   $	��Top=�6E   $��0,0,2,0q�6^   5  /   5  +   5  #     5  "   $	 100 69   $ Overhead (Taka)��6E    5  &   �� 5  *   .R�5  .   �� $" 0,0,5,0q�68   
������ $	��100��6�   $ Right=�6�   ��6d   �� *dcover+$  {0: #,##0.00;(#,##0.00); }��6�   5  +   5  #     5  "   $	 100 69   $ Amount-2 (Taka)��6E    5  &   �� 5  *   .R�5  .   �� $" 0,0,5,0q�68   
������ $	��100��6�   $ Right=�6�   ��6d   �� *dcval2+$  {0: #,##0.00;(#,##0.00); }��6�   5  +   5   #     5!  "   $ 80 69   $ Rate-2 (Taka)��6D    5"  &   �� 5#  *   .R�5$  .   �� $" 0,0,5,0q�68   
������ $	��100��6�   $ Right=�6�   ��6d   �� *dcrat2+$  {0: #,##0.00;(#,##0.00); }��6�   5%  +   5'  #     5(  "   $ 70 69   $ 	FC Rate-2��6D    5)  &   �� 5*  *   .R�5+  .     $��0q�6D   $ OnFocus=�6X   $��0,0,5,0q�6u   #��[ 6�   
������ ��5,  3   �� *fcrat2+$  TwoWay=�$  {0: #,##0.00;(#,##0.00); }��$! PropertyChanged=�5-  +   5/  #     50  "   $	 100 69   $ Amount-3 (Taka)��6E    51  &   �� 52  *   .R�53  .   �� $" 0,0,5,0q�68   
������ $	��100��6�   $ Right=�6�   ��6d   �� *dcval2+$  {0: #,##0.00;(#,##0.00); }��6�   54  +   56  #     57  "   $ 80 69   $ Rate-3 (Taka)��6D    58  &   �� 59  *   .R�5:  .   �� $" 0,0,5,0q�68   
������ $	��100��6�   $ Right=�6�   ��6d   �� *dcrat2+$  {0: #,##0.00;(#,##0.00); }��6�   5;  +   5=  #   5@     �� 5A     -"   5        $ stkpTotalCost��5A  %   $ 
Horizontal=�6<   $y�Visible=�6U   .+�5C     �� -#   5        $ 
btnRefresh��5C  %   $��	_Total : ��69   $��75��6M   $��	260,0,0,0q�6q   
������ 1 � Cursor$
1 Hand��6�   $��0q�6�   $��Right=�5D     $
��Bold"�68   �� 5E     -$   5        $ lblTotalFcAmt1��5E  $   $
��0.00��6<   $��90��6K   $��Right=�6V   $��75,0,0,0q�6y   $
��Bold"�6�   $��12(�6�   �� 5F     -%   5        $ lblTotalDcAmt1��5F  $   $
��0.00��6<   $	��100��6K   $��Right=�6W   $��0,0,0,0q�6z   $
��Bold"�6�   $��12(�6�   �� 5G     $��100.00 %��6$   $��70��67   $��Right=�6B   $��	100,0,0,0q�6e   $
��Bold"�6x   $��12(�6�   �� 5H     -&   5        $ lblTotalDcOverAmt1��5H  $   $
��0.00��6@   $	��100��6O   $��Right=�6[   $��0,0,0,0q�6~   $
��Bold"�6�   $��12(�6�   �� 5I     -'   5        $ lblTotalDcAmt2��5I  $   $
��0.00��6<   $	��100��6K   $��Right=�6W   $��0,0,0,0q�6z   $
��Bold"�6�   $��12(�6�   5J     5K     5L     5M     5N     5O     5P     
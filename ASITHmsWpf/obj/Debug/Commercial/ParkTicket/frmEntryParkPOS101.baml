   M S B A M L   `   `   `  ����E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  5      9    3ASITHmsWpf.Commercial.ParkTicket.frmEntryParkPOS101   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4UC%clr-namespace:ASITHmsWpf.UserControls     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5        
  � Tag$  PARK TICKET/COUPON SALES��5	   8   $��14(�6W   .��5
      � .+�5   
   � -   5        $��GridDataEntry��5      .+�5      �� $��0,5,0,0q�6   .+�5      �� -   5         ��Name$ 	stkpTitle��5      .+�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��Ticket No. : ��6    $��80��68   $��Right=�6D   $��0,2,0,0q�6g   �� 5      -   5         ��Name$ 
txtTransID��5   $   $	��125��68   $
��Bold"�6D   $��22��6V   
�������6b   �� 5      -   5         ��Name$ chkDateBlocked��5   #   |�.  $��Right=�6M   $��Center=�6p   $
��Bold"�6�   $��15��6�   $��20��6�   $��5,0,0,0q�6�   �� 5      $��Date :��6    $��50��61   $
��Left=�6<   $��Right=�6W   $
��Bold"�6z   $��22��6�   $��0,2,4,0q�6�   &   Xceed.Wpf.Toolkit.DateTimePicker  5      -   5          Name$ xctk_dtpInvDat��5   .      StdDateTimePickerStyle01#��[  6F   $
��Left=�6x   $
��Bold"�6�   $��24��6�   $	��120��6�   $y�Visible=�5   $   {�69   ��  �� ElementName$ chkDateBlocked�� �� Path$ 	IsChecked�5  /Xceed.Wpf.Toolkit.Primitives.DateTimePickerBase   ShowDropDownButton 6z   �� $ chkDateBlocked��$ 	IsChecked��� 5      -   5        $ stkpPrevTrans��5   %   $ 
Horizontal=�6<   $��10,0,0,0q�6U   $y�Visible=�6g   .+�5      �� -   5        $ chkPrevTransFilter��5   '   $��Center=�6C   $��15��6^   � 5      	 � AllowsTransparency	 . $��Slide=�6�   ��6$   �� $ chkPrevTransFilter��$ 	IsChecked�
 � PlacementTarget
 6f   �� $ chkPrevTransFilter��.��5   "   �� $��0,0,5,5q�6)   $��0q�6:    �� CornerRadius$ 8��6N   ��5   &   �� 5   *    �� EndPoint$	 0,1&�6>    �� 
StartPoint$	 0,1&�6M   .��5   .   �  � Color$ 	#FFECFBFB��6;    � Offset$	 0.0Q�6M   � 5   .   $ 	#FFCED9E4��6;   $	 0.5Q�6M   � 5   .   $ 	#FF5F6E6E��6;   $	 1.0Q�6M   5   +   ��5    &   �� 5!   *   $ 0.5,1&�6>   $ 0.5,0&�6O   .��5"   .   � $ 	#FFF7F7F7��6;   $	 0.0Q�6M   � 5#   .   $ 	#FFCED9E4��6;   $	 0.5Q�6M   � 5$   .   $ 	#FF22F5F5��6;   $	 1.0Q�6M   5%   +    J� Effect 5'   &   3  -System.Windows.Media.Effects.DropShadowEffect  5(   *      Color$ Black��6;      ShadowDepth$ 5Q�6I      
BlurRadius$ 20Q�6Y      Opacity$	 0.8Q�6i      RenderingBias$ Quality=�6w   ._�5+   &   �� $	��340��6-   $��90��69   ��5,   *   �� 5-   .   $ 0.5,1&�6B   $ 0.5,0&�6S   .��5.   2   � $ 	#FFE4F2E1��6?   $ 0.491Q�6Q   � 5/   2   $ 	#FF69B28B��6?   $ 0.957Q�6Q   50   /   z�52   *   �� 53   .   -	   5        theTransform1��53   >   .+�55   *   �� $ Vertical=�65   .+�56   .   �� $1��+Filter Option For Getting Transaction List:��64   $��12(�6j   $��Italic$�6x   $	��340��6�   $��Center=�6�   |�. ��57   2   �� 58   6   $ 0.5,1&�6J    � MappingMode$ RelativeToBoundingBox=�6[   $ 0.5,0&�6   .��59   :   � $ White��6G   � 5:   :   $ 	#FFB7B7B7��6G   $
 0.46Q�6Y   � 5;   :   $ 	#FFDDDDDD��6G   $ 0.233Q�6Y   5<   7   5>   /   �� 5?   .   $ 
Horizontal=�69   .+�5@   2   �� $��From :��68   $��12(�6I   $��Italic$�6W   $��45��6j   $
��Left=�6u     5A   2   -
   5        $ xctk_dtpFromDate��5A   F   #��[  6`   $��20��6�   $	��130��6�   �� 5B   2   $
��To :��68   $��Italic$�6G   $��28��6Z   $��Center=�6e     5C   2   -   5        $ xctk_dtpToDate��5C   F   #��[  6^   $��20��6�   $	��130��6�   5D   /   �� 5E   .   -   5         ��Name$ btnFilterPrevTrans��5E   5   $��	Go Filter��6Q   $��20��6e   $��60��6q   $��12(�6|   $��Verdana)�6�   $��0,10,0,0q�6�     StdBtnStyle01#��[ 5F   )    �� Left$	 155��6r   
 �� Top$	 125��6�   5G   +   5H   '   5I   #   5J      �� 5K      -   5         ��Name$ lblPrevTransListTitle��5K   $   $��Prev.Trans : ��6C   $��Right=�6[   $��24��6   $��0,2,0,0q�6�   $��75��6�    �� Target 6�   �� $ cmbPrevTransList���� 5L      -   5         ��Name$ cmbPrevTransList��5L   '   $	��270��6A    �� 
IsReadOnly . $��24��6_    �� SelectedIndex$ 0��6k   |�.  6�   �� 5M      -   5        $ 	btnPrint1��5M   %   $��_View��68   $��60��6H   $��24��6S   #��[ 6_   $��5,0,0,0q�6�   5N      �� 5O      $ 
Horizontal=�6%   $��5,0,0,0q�6>   .+�5P      �� $��Location : ��6(   $��70��6;    �� TextAlignment$ Right=�6F   $��24��6\   $��0,0,0,0q�6h   �� 5Q      -   5        $ 
cmbSectCod��5Q   '   $	��280��6;   $��24��6G   $
��Bold"�6T   $ 0��6f   
�������$��0q�6�   
������ {�. 5R   E   5S      5T      �� 5U      -   5        $ stkpControlButton��5U   !   $ 
Horizontal=�6<   .+�5V      �� -   5        $ lblSummaryInfo��5V       $2��,TOTAL TICKET = 150, SALES AMOUNT = 15,250.00��68   $
��Bold"�6o   �� 5W      -   5        $ stkpNewEdit��5W   %   $ 
Horizontal=�6:   $��20,0,0,0q�6S   $��24��6e   .+�5X      �� -   5        $ 
btnNewShow��5X   %   #��[ 69   $��65��6`   $
��_New��6k   $	  New��6z   6�   �� 5Y      -   5        $ btnEdit1��5Y   %   #��[ 67   $��65��6^   $
��Edit��6i   $
  Edit��6x   $��10,0,0,0q�6�   5Z      �� 5[      $ 
Horizontal=�6%   $��Center=�6>   $��10,0,0,0q�6Y   .+�5\      �� -   5        $ btnUpdateTrans��5\   %   #��[ 6=   $��75��6d   $��_Save��6p   $	  New��6�   $��24��6�   �� 5]      -   5        $ 
btnDelete1��5]   %   $��Delete��69   $��65��6J   $��24��6U   #��[ 6a   $��10,0,0,0q�6�   �� 5^      -   5        $ btnPrintTrans��5^   %   #��[ 6<   $��75��6c   $��_Print��6n   $��10,0,0,0q�6�   5_      �� 5`      $ 
Horizontal=�6%   $	��Top=�6?   $y�Visible=�6X   .+�5a      �� $��VAT Amount :��6$   $��12(�6;   $
��Bold"�6I   $��24��6[   $��Center=�6g   $��15,0,0,0q�6�   �� 5b      -   5        $ lblVATTotal��5b   $   $��00,000��69   $��Right=�6J   $��80��6n   
�������$
��Bold"�6�   
����  �6�   5d      5f      5g      �� 5h      -   5        $ 	stkpEntry��5h      $ 
Horizontal=�60   $y�Visible=�6I   $��0,5,0,0q�6^   .+�5i      �� .+�5j      �� .+�5k      �� $ 
Horizontal=�6)   .+�5l   "   �� -   5        $ 
lblItemCat��5l   (   $��Category : ��6<   $��0,0,0,0q�6|   $	��100��6�   $��Right=�6�   $��16(�6�    6R   �� $ 
cmbItemCat��6�   �� 5m   "   -   5        $ 
cmbItemCat��5m   +   $	��400��6?   $ 0��6L   $��16(�6�   �� 5n   "   -   5        $ btnShowGroupItems��5n   )   #��[ 6D   $
��Show��6k   $��60��6z   $��24��6�   $��5,0,0,0q�6�   $y�	Collapsed=�6�   �� 5o   &   -   5        $ 
lblMemoNar��5o   ,   $��Note(if any) :��6@   $	��100��6Y   $��16(�6e   $��Right=�6�    6s   �� $ 
txtMemoNar��6�   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5p   &   -   5           Name$  
txtMemoNar��5p   <   $	��540��6P   !   	Watermark$! References of guest��6\    
 wtxtBx#��[ 6|   $��5,0,0,0q�6�   $��16(�6�   $��24��6�   )  #Xceed.Wpf.Toolkit.AutoSelectTextBox"   AutoMoveFocus" . #   AutoSelectBehavior$# OnFocus=�6�   $ �� 	MaxLength$	$ 100��6�   5q      �� 5r      $ 
Horizontal=�6)   $
��Left=�6B   $��0,5,0,0q�6]   .+�5s   "   �� $��_Find Ticket :��6(   $��Right=�6o   $	��100��6�   $��5,0,5,0q�6�   $��16(�6�    6A   �� $ autoItemSearch��6�   *   $ASITHmsWpf.UserControls.AutoComplete  5t   "   -    5        %  Name$% autoItemSearch��5t   2   *   $ASITHmsWpf.UserControls.AutoComplete&   PatternChanged$#& autoItemSearch_PatternChanged��6�   '   MouseDoubleClick$%' autoItemSearch_MouseDoubleClick��5u   u   (   	LostFocus$( autoItemSearch_LostFocus��5v   .   $	��400��5t   J   ) �� SelectedValuePath$) sircode��6V   * �� DisplayMemberPath$* sirdesc��6r    % !AutoCompleteComboBoxWithoutButton#��[ 5u   .   +   Delay$	+ 500��6i   $��16(�6�   , � ContextMenu, 5w   &   �� 5x   *   5z   #   �� 5{   "   $��
Quantity :��6(   $	��100��6k   $��Right=�6w   $��16(�6�   $��5,0,5,0q�6�    6=   �� $ autoItemSearch��6�   %  Xceed.Wpf.Toolkit.IntegerUpDown  5|   "   -!   5        -  Name$- intUdQty��5|   5   �	  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]. 	  Value$. 1X�6G   $��80��6Q   $��0,0,10,0q�6\   / 	  ShowButtonSpinner/ . $
��Bold"�6�   0 	  Minimum$0 1X�6�   1 	  Maximum$1 99999X�6�   $��16(�6�   6�   �� 5}   "   $��Rate :��6(   $��55��6h   $��Right=�6s   $��16(�6�   $��5,0,5,0q�6�    69   �� $ autoItemSearch��6�   �� 5   "   -"   5        $ lblUnitPrice��5   (   $��00,000��6>   $��Right=�6O   $��80��6r   
�������$��16(�6�   $
��Bold"�6�   
����  �6�   �� 5�   "   $��Total Amount :��6(   $	��120��6B   $��Right=�6N   $��16(�6q   $��5,0,5,0q�6   �� 5�   "   -#   5        $ lblGrandTotal��5�   (   $��00,000��6?   $��Right=�6P   $	��120��6s   
�������$��16(�6�   $
��Bold"�6�   
����  �6�   �� 5�   "   -$   5        $ btnAddSearch��5�   )   #��[ 6?   $	��_Ok��6f   $��75��6t   $��10,0,0,0q�6   6�   5�      �� 5�      $ 
Horizontal=�6)   $��0,5,0,0q�6B   .+�5�   "   �� -%   5        $ chkConfrmSave��5�   +   $��Auto Confirm��6B   $	��120��6Y   $��16(�6e   $��Center=�6s   $��25,0,0,0q�6�   2 d� 	IsChecked$
2 TrueY�6�   �� 5�   "   -&   5        $ chkPrintDirect��5�   +   $��Direct print��6C   $	��120��6Z   $��16(�6f   $��Center=�6t   $��50,0,0,0q�6�   $
2 TrueY�6�   �� 5�   "   -'   5        $ chkDiscount��5�   +   $��	Disco_unt��6@   $	��100��6U   |�.  $��Center=�6s   $��16(�6�   $��85,0,0,0q�6�   �� 5�   "   $ 
Horizontal=�6-   
�������$	��190��6�   {�6F   �� $ chkDiscount��$ 	IsChecked�.+�5�   &   $
  Xceed.Wpf.Toolkit.DoubleUpDown
  -(   5        3 
 Name$3 	iudDisPer��5�   8   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]4   Maximum$	4 100X�6K   5   Minimum$5 0X�6Y   6   Value$6 0X�6e   $��60��6o   {�. $��16(�6�   �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]7   	Increment$7 1X�6�   8   FormatString$8 00.00��6�   $��10,0,0,0q�5�   8   $��24��6J   6V   �� 5�   &   $��%��6,   $��30��68   $
��Left=�6C   $
��Bold"�6e   $��2,5,0,0q�6w   $��16(�6�     5�   &   -)   5        $  	txtDisAmt��5�   <   $��80��6O   $��14(�6Z   $��24��6h   6t   5�   #   �� 5�   "   -*   5        $ btnTotal��5�   )   $��80��6<   $��
_Calculate��6G   $��16(�6\   $��45,0,0,0q�6k   $��24��5�   )   $
��Bold"�65   
����  �
�������$��0q�6l   
������ 6�   �� 5�   "   $��Net Amount :��6(   $	��120��6@   $��Right=�6L   $��16(�6o   $��5,0,5,0q�6}   $��0,0,0,0q�6�   �� 5�   "   -+   5        $ lblNetTotal��5�   (   $��00,000��6=   $��Right=�6O   $	��120��6r   
�������$
��Bold"�6�   $��16(�6�   
����  �6�   5�      5�      �� 5�      $��0,5,0,0q�6%   $
��Left=�66   $
��1190��6Q   $y�Visible=�6^   .+�5�      �� -,   5        9 ��Name$9 lstvRetailItem��5�   '   : �� SelectionMode$: Single=�6?   $	��470��6V   $��Disabled=�6d   $��Stretch=�6�   ��5�   "   �� 5�   &   .R�5�   *   � ; � Orientation$; 
Horizontal=�64   5�   '   ��5�   "   �� 5�   &   .R�5�   *   �� $ 
btnAddItem��61   --   5        $��90��5�   E   $	��375��6Q   #��[ 6]     6�   �� $ sircode�< � ToolTip< 6�   �� $ sirdesc�.��5�   .   �� $ 
Horizontal=�69   $
��Left=�6R   .+�5�   2   �� #��[ 69   $	��110��6`   .��5�   6   �� $��90��6A   $	��110��6M   $
��Left=�6Y   $	��Top=�6t   .+�5�   :   �� $
��Fill=�6a   ��6@   �� $ sirimage�6a   5�   7   5�   3   �� 5�   2   #��[ 69   $	��295��6`   $��10,0,0,0q�6l   .��5�   6   �� $	��295��6A   $
��Left=�6M   .+�5�   :   �� $ 
Horizontal=�6E   .+�5�   >   �� $
��Wrap=�6f   $��16(�6z   $
��Bold"�6�   ��6H   �� $ sirdesc�6�   5�   ;   �� 5�   :   $ 
Horizontal=�6E   $
��Left=�6^   .+�5�   >   �� $��[��6H   �� 5�   >   ��6H   �� $ sircode1�  6g   �� $ sircode��� 5�   >   $��] ��6H   �� 5�   >   $��16(�6�   $
��Bold"�6�   
���  ����6H   �� $ 	saleprice�= �� StringFormat$= {0:Price ৳ #,##0.00}��6�   5�   ;   5�   7   5�   3   5�   /   5�   +   5�   '   5�      5�      5�      5�      �� 5�      -.   5        $ stkpSalesReturn��5�      $ 
Horizontal=�66   $	��500��6O   $
��1000��6\   
�������$y�	Collapsed=�6|   5�      5�      5�      5�      5�      
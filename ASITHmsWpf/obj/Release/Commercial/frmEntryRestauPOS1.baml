   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  'clr-namespace:ASITHmsWpf
ASITHmsWpf  .    (ASITHmsWpf.Commercial.frmEntryRestauPOS1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  $localclr-namespace:ASITHmsWpf  -   
  � Tag$3  -Restaurant/Canteen Sales Invoice Entry Screen��   IOUserControl#��[  .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� -    �Name$ 
gridMaster��$��0��$	��Top=�$��	10,0,10,0q�$��2��.+���  �� Orientation$ Vertical=�.+��� $
��Left=�$ 
Horizontal=�.+��� $��Sales Tranasction:��$��14(�$��Italic$�$
��Bold"�
����� �$	��140���� -    ��Name$ lblPrevInvListTitle��$��Previous Invoice List :��$��Right=�$	��140�� �� Target ��  �� ElementName$ cmbPrevInvList���� -    ��Name$ cmbPrevInvList��$	��620�� �� 
IsReadOnly . 	 �� SelectedIndex$	 0��|�.  �� $��9��$y�Hidden=��� -   
 ��Name$
 	btnPrint1��$��_Print��$��75��  StdBtnStyle01#��[ �� $��20��$y�Hidden=��� -    ��Name$ ChkDuecollection��$��Due Collection��$��Center=��  � AllowsTransparency . $��Slide=� � HorizontalOffset$
 -780�� � VerticalOffset$ 60������ $ ChkDuecollection�� �� Path$ 	IsChecked� � PlacementTarget �� $ ChkDuecollection��.���� $��0,0,5,5q�$��0q� �� CornerRadius$ 8�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ Black��   ShadowDepth$ 5Q�   
BlurRadius$ 20Q�   Opacity$	 0.8Q�   RenderingBias$ Quality=�._��� $	��750��$	��450������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFE4F2E1�� � Offset$ 0.491Q�� $ 	#FF69B28B��$ 0.957Q�.+��� $ Vertical=�.+��� $��Due Collection��$��Center=�$��16(�$	��750������ $ 0.5,1&�$ 0.5,0&�.��� $ Black��$ 0.008Q�� $ 	#FFCBDAC5��$ 0.165Q��� $ 
Horizontal=�.+��� $��Invoice Date From : ��$	��120��$��Right=�&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtDueInvDat1��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130���� $�� To : ��  -   $ xctk_dtDueInvDat2��#��[ $
��Bold"�$��22��$	��130���� $�� Ref : ��(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -	     Name$ 	txtDueRef�� 
 wtxtBx#��[    	Watermark$ Cell No./ Name / Memo No��$��5,0,0,0q�$��22��$	��194��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .     AutoSelectBehavior$  OnFocus=��� $��10��$y�Hidden=��� -
   $
 btnFindDueList1��$��Find Invoices��$��80��#��[ �� $ 
Horizontal=�$��0,5,0,0q�.+��� $��Dues Memo List : ��$	��120��$��Right=��� -   $ cmbDuesInvList��$	��530�� . $	 0��|�.  �� $��10��$y�Hidden=��� -   $
 btnShowDueMemo��$��	Show Memo��$��80��#��[ �� -   ! ��Name$! 	stkDueCol��$ Vertical=�$��0,5,0,0q�.+��� $ 
Horizontal=�.+��� -   $ lblDueInvDate1��$	��150��$
��Bold"��� -   $ lblDueInvNo1��$	��200��$
��Bold"��� -   $ lblDueInvRef1��$	��300���� $ 
Horizontal=�.+��� -   " ��Name$" txtbDueInvNar1��$	��650��&   System.Windows.Controls.DataGrid  -   #  Name$# 	dgDueMemo��$   CanUserResizeRows$ .  
������ %   
IsReadOnly% .  $��0q�$��0,0,0,0q�$��0��$	��Top=�$	��150��$
��Auto=�&   AutoGenerateColumns& .  '   CanUserAddRows' .  (   CanUserDeleteRows( .  $��Hidden=�)   SelectionUnit$
) Cell=�*   SelectionMode$* Single=�+   FrozenColumnCount$+ 2��,   Columns, 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn-   Width5  /System.Windows.Controls.DataGridLengthConverter$- 40 .   Header$	. Sl#��/ +� IsFocusScope/ .  0   CanUserSort0 .  1   
IsReadOnly1 . 2   CanUserReorder2 .  3   CanUserResize3 .  1	  +System.Windows.Controls.DataGridBoundColumn4 	  Binding4 �� $ slnum�  $	- 350 0 .  1 . 4 �� $ sirdesc�5   HeaderTemplate5 5
  -System.Windows.Controls.ItemContainerTemplate�
  .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description��|�.  $	��300��4  .System.Windows.Controls.DataGridTemplateColumn  $- 80 $. Quantity��1 .  6   CellTemplate6 �� .R��� $��70��$��22������ $ invqty�7 �� StringFormat$7 {0: #,##0.00}��  $- 45 $
. Unit��6 �� .R��� $��40��$��22������ $ sirunit�  $- 80 $
. Rate��6 �� .R�� $��Right=�$	��Top=�$��70��.+��� $��Right=�$	��Top=����� *itmrat+$7 {0: #,##0.00}��  $	- 110 $. Amount��6 �� .R�� $��Right=�$	��Top=�$	��100��.+��� $��Right=�$	��Top=����� *itmam+$7 {0: #,##0.00}���� $ 
Horizontal=�.+��� -   $ lblDueAmtDes1��$��Right=�$	��600��$��Total :��$
��Bold"��� -   $ lblDueBillGrossAmt��$��Right=�$	��113��$
��Bold"��� $ 
Horizontal=�.+��� $��Collection Date : ��$	��120��$��Right=�  -   $ xctk_dtDuePayDat1��#��[ $
��Bold"�$��22��$	��130��{�.  �� $��Amount : Tk. ��$	��100��$��Right=�  -   $ txtDueColAmt1��#��[ $ Due Collection��$�� ��$��Right=� . $  OnFocus=�$��22��$	��100���� -   $ lblDueBalAmt��$%��Balance Due : Tk. 00,000,000.00��$	��200��$��Center=�
���  ��$y�Hidden=��� $��10��$y�Hidden=��� -   $
 btnUpdateDueCol1��$��Save Recovery��$��80��#��[ �� $ Vertical=�.+��� $��Collection Details��$	��Top=�$
��Bold"�$��Stretch=�8 �� TextAlignment$8 Center=�
�������  -   $# dgDueCollMemo��$ .  
������ % .  $��0q�$��0,0,0,0q�$��0��$	��Top=�$��80��$
��Auto=�& .  ' .  ( .  $��Hidden=�$
) Cell=�$* Single=�$+ 2��,   $- 40 $	. Sl#��/ .  0 .  1 . 2 .  3 .  4 �� $ slnum�  $	- 150 $. 	Col. Date��/ .  0 .  1 . 2 .  3 .  4 �� $ 	bilcoldat�$"7 {0: dd-MMM-yyyy hh:mm:ss tt}��  $	- 410 $. Description��6 �� .R�� $��Right=�$	��Top=�.+��� $
8 Left=�$	��Top=����� *bcnote+9 � ToolTip9 �� *bcnote+  $	- 110 $. Col. Amount��6 �� .R�� $��Right=�$	��Top=�$	��100��.+��� $��Right=�$	��Top=����� *
bilcolam+$7 {0: #,##0.00}���� $ 
Horizontal=�.+��� -   $! stkIntro��$ Vertical=�$	��Top=�$
��Left=�$��	0,3,3.6,0q�.+��� $ 
Horizontal=�.+��� -   $ lblSectCodTitle��$��Restaurant S_tore :��$��Right=�$	��100�� �� $ 
cmbSectCod���� -   $ 
cmbSectCod��$	��300�� . $	 0��|�.  �� -   $ chkDateBlocked��$��Invoice _Date : ��|�.  $��Right=�$��Center=�$	��100��  -   $ xctk_dtSalesDat��#��[ $
��Bold"�$��22��$	��130��{�.  �� -   $ lblinvNoTitle��$��Invoice No:��$��Right=�$	��115���� -    $ lblinvNo��$��FSIMM-XXXXX��$  FSIYYYYMMCCCCXXXXX��$	��140��$
��Bold"�$
��Left=��� $��20��$y�Hidden=��� $ 
Horizontal=�$��24��.+��� -!   $
 btnOk��#��[ $	��_Ok��$��75���� $��20��$y�Hidden=��� -"   $
 	btnPrint2��$��_Print��$��70��#��[ �� $��20��$y�Hidden=��� -#   $
 	btnUpdate��#��[ $��_Save��$��75���� $��1��$	��Top=�
��� ���$��5��$��2��$��0,2,0,0q�� -$   $ gridDetails��$��2��$	��Top=�$y�Visible=�$��	10,0,10,0q�$��2��.+��� $ Vertical=�$
��Auto��$	��Top=�
�������.+��� $ Vertical=�.+��� $ Vertical=�.+��� $ 
Horizontal=�.+��� -%   $ lblUID��$��
Item _UID:��$��60��$��Right=� �� $ txtUID��  -&   $ txtUID��#��[ $ Manufacturer's Product ID��$��5,0,0,0q�$��22��$	��150�� . $  OnFocus=��� $ 
Horizontal=�.+��� -'   $ lblItemSearch��$��_Item:��$��60��$��Right=� �� $ AtxtItemCode��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -(   :  Name$: AtxtItemCode��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox;   	LostFocus$; AtxtItemCode_LostFocus��<   
SearchType$< C��$
��Left=�$��22��$	��580���� -)   $ lblinvQtyTitle��$��_Quantity :��$��Right=�$	��100�� �� $ 	txtinvQty��  -*   $ 	txtinvQty��= �� TextAlignment$= Right=� . $  OnFocus=�$��5,0,0,0q�#��[ $ Sales Quantity��$�� ��$��22��$	��110���� -+   $ lblUnit1��$
��Unit��$��50���� -,   $
 btnAddRecord��#��[ $
��_Add��$	��Top=�$��Right=�$��22��$��75���� $ 
Horizontal=�$
��Left=�.+��� $�� ��$
��Left=�$	��542���� --   $ lblSalesRateTitle��$��Rate :��$
��Left=�$��38���� -.   $ lblSalesRate��$�� ��$
��Left=�$	��100���� -/   $ lblinAmount��$��Amount :��$
��Left=�$��60�� �� $ 
txtinvRate���� -0   $ lblAmountShow��$	��110��$�� ��$
��Left=�$
��Bold"��� $��_List�� �� $ dgvMemo��  -1   $# dgvMemo��$ .  
������ % .  $��0q�$��0,0,0,0q�$��0��$	��Top=�$	��200��$
��Auto=�& .  ' .  ( .  $��Hidden=�$
) Cell=�$* Single=�$+ 2��,   $- 40 $	. Sl#��/ .  0 .  1 . 2 .  3 .  4 �� $
 trsl�  $	- 550 0 .  1 . 4 �� $ trdesc�5 
  .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description��|�.  $	��500��  $- 80 $. Quantity��1 .  6 �� .R�  -2   $= Right=� . $��0q�$��70��$  OnFocus=�$��5,0,0,0q�#��[ $ Quantity������ $ invqty�> �� Mode$> TwoWay=�? �� UpdateSourceTrigger$? PropertyChanged=�  $- 60 $
. Unit��6 �� .R��� $��50��$��22������ $ trunit�  $- 80 $
. Rate��6 �� .R�� $��Right=�$	��Top=�$��70��.+��� $��Right=�$	��Top=����� *	invrate+$7 {0: #,##0.00}��  $	- 110 $. Amount��6 �� .R�� $��Right=�$	��Top=�$	��100��.+��� $��Right=�$	��Top=����� *invamt+$7 {0: #,##0.00}��  $	- 100 $. Discount��6 �� .R�  -3   $= Right=� . $��0q�$��90��$  OnFocus=�$��5,0,0,0q�#��[ $ Discount������ $ 	invdisamt�$> TwoWay=�$? PropertyChanged=�$7 {0: #,##0.00}��  $	- 110 $. 
Net Amount��6 �� .R�� $��Right=�$	��Top=�$	��100��.+��� $��Right=�$	��Top=����� *	invnetamt+$7 {0: #,##0.00}���� $	��200��$��0,0,50,0q�.+��� �� $ 
Horizontal=�$��Right=�.+��� -4   $
 btnRecal��#��[ $	��100��$��_Recalculate���� $��50��$y�Hidden=��� -5   $ ckhDiscountPer��$��_Discount : ��$��Center=�  -6   $ 	txtDisPer��$= Right=�$  OnFocus=�#��[ $
��0.00��$��40���� $��%��$��20���� -7   $ lblTotalTitle��$��Total : ��$	��120��$��Right=�$
��Bold"����	#FF091EF0�� -8   $ lblTotalAmt��$�� ��$	��110��$��Right=�$
��Bold"����	#FF091EF0�� -9   $ lblTDiscAmt��$�� ��$	��100��$��Right=�$
��Bold"����	#FF091EF0�� -:   $ lblTotalNetAmt��$�� ��$	��110��$��Right=�$
��Bold"����	#FF091EF0�� �� $ 
Horizontal=�$��Right=�.+��� -;   $ lblinvRefTitle��$��Phone/Re_f. :��$��Right=�$	��100�� �� $ 	txtinvRef��  -<   $ 	txtinvRef��#��[ $, &Contact phone no (or other references)��@ �� 	MaxLength$@ 30��$�� ��$
��Left=�$��22��$	��150���� $	��370��$y�Hidden=��� -=   $ lblRcvTitle��$��Received Amoun_t��$	��120��$��Right=� �� $ 	txtRcvAmt���� $��:��$��15��  ->   $ 	txtRcvAmt��#��[ $ Received��$�� ��$��Right=�$��22��$	��100���� -?   $ lblPaidTitle��$��Paid Amount��$	��120��$��Right=��� $��:��$��15���� -@   $ 
lblPaidAmt��$�� ��$	��120��$��Right=��� $ 
Horizontal=�$��Right=�.+��� -A   $ lblreqNarTitle��$��Remarks (if any) :��$	��Top=�$��Right=�$��25��$	��100�� �� $ 	txtreqNar��  -B   $ 	txtinvNar��#��[ $ Remarks ��$	@ 100��$�� ��$
��Left=�$��22��$	��500���� $��40��$y�Hidden=��� -C   $ lblReturnTitle��$��Return Amount��$	��100��$��Right=��� $��:��$��15���� -D   $ lblReturnAmt��$�� ��$	��100��$��Right=��� -E   $ lblDueTitle��$��
Due Amount��$	��120��$��Right=��� $��:��$��15���� -F   $ 	lblDueAmt��$�� ��$	��120��$��Right=��� �� $ 
Horizontal=�$��Right=�
   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  'clr-namespace:ASITHmsWpf
ASITHmsWpf  .    (ASITHmsWpf.Commercial.frmEntryPharmaPOS1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  $localclr-namespace:ASITHmsWpf  -   
  � Tag$)  #Medicine Sales Invoice Entry Screen��   IOUserControl#��[  .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� -    �Name$ 
gridMaster��$��0��$	��Top=�$��	10,0,10,0q�$��2��.+���  �� Orientation$ Vertical=�.+��� $
��Left=�$ 
Horizontal=�.+��� $��Sales Tranasction:��$��14(�$��Italic$�$
��Bold"�
����� �$	��140���� -    ��Name$ lblPrevInvListTitle��$��Prev.Invoice List :��$��Right=�$	��110�� �� Target ��  �� ElementName$ cmbPrevInvList���� -    ��Name$ cmbPrevInvList��$	��560�� �� 
IsReadOnly . 	 �� SelectedIndex$	 0��|�.  �� $��5��$y�Hidden=��� -   
 ��Name$
 	btnPrint1��$��_View/Print��$��75��$��22��  StdBtnStyle01#��[ �� $��20��$y�Hidden=��� -    ��Name$ ChkPrintDirect��$��Direct Print�� d� 	IsChecked$
 TrueY�$��Center=��� $��45��$y�Hidden=��� -   $ ChkDuecollection��$��Due Collection��$��Center=��  � AllowsTransparency . $��Slide=� � HorizontalOffset$
 -780�� � VerticalOffset$ 60������ $ ChkDuecollection�� �� Path$ 	IsChecked� � PlacementTarget �� $ ChkDuecollection��.���� $��0,0,5,5q�$��0q� �� CornerRadius$ 8�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ Black��   ShadowDepth$ 5Q�   
BlurRadius$ 20Q�   Opacity$	 0.8Q�   RenderingBias$ Quality=�._��� $	��750��$	��450������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFE4F2E1�� � Offset$ 0.491Q�� $ 	#FF69B28B��$ 0.957Q�.+��� $ Vertical=�.+��� $��Due Collection��$��Center=�$��16(�$	��750������ $ 0.5,1&�$ 0.5,0&�.��� $ Black��$ 0.008Q�� $ 	#FFCBDAC5��$ 0.165Q��� $ 
Horizontal=�.+��� $��Invoice Date From : ��$	��120��$��Right=�&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtDueInvDat1��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130���� $�� To : ��  -	   $ xctk_dtDueInvDat2��#��[ $
��Bold"�$��22��$	��130���� $�� Ref : ��(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -
     Name$ 	txtDueRef�� 
 wtxtBx#��[    	Watermark$ Cell No./ Name / Memo No��$��5,0,0,0q�$��22��$	��194��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox    AutoMoveFocus  . !   AutoSelectBehavior$! OnFocus=��� $��10��$y�Hidden=��� -   $
 btnFindDueList1��$��Find Invoices��$��80��#��[ �� $ 
Horizontal=�$��0,5,0,0q�.+��� $��Dues Memo List : ��$	��120��$��Right=��� -   $ cmbDuesInvList��$	��530�� . $	 0��|�.  �� $��10��$y�Hidden=��� -   $
 btnShowDueMemo��$��	Show Memo��$��80��#��[ �� -   " ��Name$" 	stkDueCol��$ Vertical=�$��0,5,0,0q�.+��� $ 
Horizontal=�.+��� -   $ lblDueInvDate1��$	��150��$
��Bold"��� -   $ lblDueInvNo1��$	��200��$
��Bold"��� -   $ lblDueInvRef1��$	��300���� $ 
Horizontal=�.+��� -   # ��Name$# txtbDueInvNar1��$	��650��&   System.Windows.Controls.DataGrid  -   $  Name$$ 	dgDueMemo��%   CanUserResizeRows% .  
������ &   
IsReadOnly& .  $��0q�$��0,0,0,0q�$��0��$	��Top=�$	��150��$
��Auto=�'   AutoGenerateColumns' .  (   CanUserAddRows( .  )   CanUserDeleteRows) .  $��Hidden=�*   SelectionUnit$
* Cell=�+   SelectionMode$+ Single=�,   FrozenColumnCount$, 2��-   Columns- 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn.   Width5  /System.Windows.Controls.DataGridLengthConverter$. 40 /   Header$	/ Sl#��0 +� IsFocusScope0 .  1   CanUserSort1 .  2   
IsReadOnly2 . 3   CanUserReorder3 .  4   CanUserResize4 .  1	  +System.Windows.Controls.DataGridBoundColumn5 	  Binding5 �� $ slnum�  $	. 350 1 .  2 . 5 �� $ sirdesc�6   HeaderTemplate6 5
  -System.Windows.Controls.ItemContainerTemplate�
  .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description��|�.  $	��300��4  .System.Windows.Controls.DataGridTemplateColumn  $. 80 $/ Quantity��2 .  7   CellTemplate7 �� .R��� $��70��$��22������ $ invqty�8 �� StringFormat$8 {0: #,##0.00}��  $. 45 $
/ Unit��7 �� .R��� $��40��$��22������ $ sirunit�  $. 80 $
/ Rate��7 �� .R�� $��Right=�$	��Top=�$��70��.+��� $��Right=�$	��Top=����� *itmrat+$8 {0: #,##0.00}��  $	. 110 $/ Amount��7 �� .R�� $��Right=�$	��Top=�$	��100��.+��� $��Right=�$	��Top=����� *itmam+$8 {0: #,##0.00}���� $ 
Horizontal=�.+��� -   $ lblDueAmtDes1��$��Right=�$	��600��$��Total :��$
��Bold"��� -   $ lblDueBillGrossAmt��$��Right=�$	��113��$
��Bold"��� $ 
Horizontal=�.+��� $��Collection Date : ��$	��120��$��Right=�  -   $ xctk_dtDuePayDat1��#��[ $
��Bold"�$��22��$	��130��{�.  �� $��Amount : Tk. ��$	��100��$��Right=�  -   $ txtDueColAmt1��#��[ $ Due Collection��$�� ��$��Right=�  . $! OnFocus=�$��22��$	��100���� -   $ lblDueBalAmt��$%��Balance Due : Tk. 00,000,000.00��$	��200��$��Center=�
���  ��$y�Hidden=��� $��10��$y�Hidden=��� -   $
 btnUpdateDueCol1��$��Save Recovery��$��80��#��[ �� $ Vertical=�.+��� $��Collection Details��$	��Top=�$
��Bold"�$��Stretch=�9 �� TextAlignment$9 Center=�
�������  -   $$ dgDueCollMemo��% .  
������ & .  $��0q�$��0,0,0,0q�$��0��$	��Top=�$��80��$
��Auto=�' .  ( .  ) .  $��Hidden=�$
* Cell=�$+ Single=�$, 2��-   $. 40 $	/ Sl#��0 .  1 .  2 . 3 .  4 .  5 �� $ slnum�  $	. 150 $/ 	Col. Date��0 .  1 .  2 . 3 .  4 .  5 �� $ 	bilcoldat�$"8 {0: dd-MMM-yyyy hh:mm:ss tt}��  $	. 410 $/ Description��7 �� .R�� $��Right=�$	��Top=�.+��� $
9 Left=�$	��Top=����� *bcnote+: � ToolTip: �� *bcnote+  $	. 110 $/ Col. Amount��7 �� .R�� $��Right=�$	��Top=�$	��100��.+��� $��Right=�$	��Top=����� *
bilcolam+$8 {0: #,##0.00}���� $ 
Horizontal=�.+��� -   $" stkIntro��$ Vertical=�$	��Top=�$
��Left=�$��	0,3,3.6,0q�.+��� $ 
Horizontal=�.+��� -   $ lblSectCodTitle��$��Medicine S_tore :��$��Right=�$	��100�� �� $ 
cmbSectCod���� -   $ 
cmbSectCod��$	��300�� . $	 0��|�.  �� -   $ chkDateBlocked��$��Invoice _Date : ��|�.  $��Right=�$��Center=�$	��100��  -   $ xctk_dtSalesDat��#��[ $
��Bold"�$��22��$	��130��{�.  �� -    $ lblinvNoTitle��$��Invoice No:��$��Right=�$	��115���� -!   $ lblinvNo��$��MSIMM-XXXXX��$  MSIYYYYMMCCCCXXXXX��$	��140��$
��Bold"�$
��Left=��� $��20��$y�Hidden=��� $ 
Horizontal=�$��24��.+��� -"   $
 	btnPrint2��$��_Print��$��75��#��[ $��24��$	��Top=��� $��15��$y�Hidden=��� -#   $
 btnOk��#��[ $	��_Ok��$��75��$��24��$	��Top=��� $��15��$y�Hidden=��� -$   $
 	btnUpdate��#��[ $��_Save��$��75���� $��1��$	��Top=�
��� ���$��5��$��2��$��0,2,0,0q�� -%   $ gridDetails��$��2��$	��Top=�$y�Visible=�$��	10,0,10,0q�$��2��.+��� $ Vertical=�$
��Auto��$	��Top=�
�������.+��� $ Vertical=�.+��� $ Vertical=�.+��� $ 
Horizontal=�.+��� -&   $ lblUID��$��
Item _UID:��$	��100��$��Right=� �� $ txtUID��  -'   $ txtUID��#��[ $ Manufacturer's Product ID��$��5,0,0,0q�$��22��$	��150��  . $! OnFocus=�1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -(   ;  Name$; AtxtItemCode��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox<   	LostFocus$< AtxtItemCode_LostFocus��=   
SearchType$= C��$
��Left=�$��0��$��0��$y�Hidden=��� $ 
Horizontal=�.+��� -)   $ lblItemSearch��$��_Item:��$	��100��$��Right=� �� $ txtSearchItem1��  -*   $ txtSearchItem1��#��[ $   ��$��22��$	��685��$! OnFocus=��� -+   $ lblinvQtyTitle��$��_Quantity :��$��Right=�$	��100�� �� $ 	txtinvQty��  -,   $ 	txtinvQty��> �� TextAlignment$> Right=�  . $! OnFocus=�$��5,0,0,0q�#��[ $ Sales Quantity��$�� ��$��22��$	��110���� --   $ lblUnit1��$
��Unit��$��50���� -.   $
 btnAddRecord��#��[ $
��_Add��$	��Top=�$��Right=�$��22��$��75���� $ 
Horizontal=�$
��Left=�.+��� -/   $ lblMfgByTitle��$��	Mfg. By :��$	��100��$��Right=� �� $ AtxtItemCode���� -0   $ lblMfgByName��$�� ��$
��Left=�$	��589���� -1   $ lblSalesRateTitle��$��Rate :��$
��Left=�$��38���� -2   $ lblSalesRate��$�� ��$
��Left=�$	��100���� -3   $ lblinAmount��$��Amount :��$
��Left=�$��60�� �� $ 
txtinvRate���� -4   $ lblAmountShow��$	��110��$�� ��$
��Left=�$
��Bold"��� $ 
Horizontal=�$
��Left=�.+��� $��_List :��$	��100��$��Right=� �� $ lstItem1���� -5   ? ��Name$? lstItem1��$	��685��$	��100��  -6   $$ dgvMemo��% .  
������ & .  $��0q�$��0,0,0,0q�$��0��$	��Top=�$	��200��$
��Auto=�' .  ( .  ) .  $��Hidden=�$
* Cell=�$+ Single=�$, 2��-   $. 40 $	/ Sl#��0 .  1 .  2 . 3 .  4 .  5 �� $
 trsl�  $	. 100 $/ Inv.Code��0 .  1 .  2 . 3 .  4 .  5 �� $ invcode�  $	. 380 1 .  2 . 5 �� $ trdesc�6 
  .R�� $��Center=�$	��Top=�.+��� $��Center=�$	��Top=�$��Description��|�.  $	��200��  $	. 150 1 .  2 . 5 �� $ 	mfgcomnam�6 
  .R�� $��Center=�$	��Top=�.+��� $��Center=�$	��Top=�$��Manufacturer��|�.  $	��120��  $. 70 $/ Quantity��2 .  7 �� .R�  -7   $> Right=�  . $��0q�$��60��$! OnFocus=�$��5,0,0,0q�#��[ $ Quantity������ $ invqty�@ �� Mode$@ TwoWay=�A �� UpdateSourceTrigger$A PropertyChanged=�  $. 40 $
/ Unit��7 �� .R��� $��50��$��22������ $ trunit�  $. 70 $
/ Rate��7 �� .R�� $��Right=�$	��Top=�$��60��.+��� $��Right=�$	��Top=����� *	invrate+$8 {0: #,##0.00}��  $	. 110 $/ Amount��7 �� .R�� $��Right=�$	��Top=�$	��100��.+��� $��Right=�$	��Top=����� *invamt+$8 {0: #,##0.00}��  $. 80 $/ Discount��7 �� .R�  -8   $> Right=�  . $��0q�$��70��$! OnFocus=�$��5,0,0,0q�#��[ $ Discount������ $ 	invdisamt�$@ TwoWay=�$A PropertyChanged=�$8 {0: #,##0.00}��  $	. 110 $/ 
Net Amount��7 �� .R�� $��Right=�$	��Top=�$	��100��.+��� $��Right=�$	��Top=����� *	invnetamt+$8 {0: #,##0.00}���� $��0,0,25,0q�.+��� �� $ 
Horizontal=�$��Right=�.+��� -9   $
 btnRecal��#��[ $	��100��$��22��$��_Recalculate���� $��50��$y�Hidden=��� -:   $ ckhDiscountPer��$��_Discount : ��$��Center=�  -;   $ 	txtDisPer��$> Right=�$! OnFocus=�#��[ $
��0.00��$��40���� $��%��$��20���� -<   $ lblTotalTitle��$��Total : ��$	��120��$��Right=�$
��Bold"����	#FF091EF0�� -=   $ lblTotalAmt��$�� ��$	��110��$��Right=�$
��Bold"����	#FF091EF0�� ->   $ lblTDiscAmt��$�� ��$��80��$��Right=�$
��Bold"����	#FF091EF0�� -?   $ lblTotalNetAmt��$�� ��$	��110��$��Right=�$
��Bold"����	#FF091EF0�� �� $ 
Horizontal=�$��Right=�.+��� -@   $ lblinvRefTitle��$��Phone/Re_f. :��$��Right=�$	��100�� �� $ 	txtinvRef��  -A   $ 	txtinvRef��#��[ $, &Contact phone no (or other references)��B �� 	MaxLength$B 30��$�� ��$
��Left=�$��22��$	��150���� $	��370��$y�Hidden=��� -B   $ lblRcvTitle��$��Received Amoun_t��$	��120��$��Right=� �� $ 	txtRcvAmt���� $��:��$��15��  -C   $ 	txtRcvAmt��#��[ $ Received��$! OnFocus=�$�� ��$��Right=�$��22��$	��100���� -D   $ lblPaidTitle��$��Paid Amount��$	��120��$��Right=��� $��:��$��15���� -E   $ 
lblPaidAmt��$�� ��$	��120��$��Right=��� $ 
Horizontal=�$��Right=�.+��� -F   $ lblreqNarTitle��$��Remarks (if any) :��$	��Top=�$��Right=�$��25��$	��100�� �� $ 	txtreqNar��  -G   $ 	txtinvNar��#��[ $ Remarks ��$	B 100��$�� ��$
��Left=�$��22��$	��500���� $��40��$y�Hidden=��� -H   $ lblReturnTitle��$��Return Amount��$	��100��$��Right=��� $��:��$��15���� -I   $ lblReturnAmt��$�� ��$	��100��$��Right=��� -J   $ lblDueTitle��$��
Due Amount��$	��120��$��Right=��� $��:��$��15���� -K   $ 	lblDueAmt��$�� ��$	��120��$��Right=��� 
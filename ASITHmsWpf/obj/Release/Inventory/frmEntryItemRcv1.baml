   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  +    %ASITHmsWpf.Inventory.frmEntryItemRcv1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  :d2http://schemas.microsoft.com/expression/blend/2008  -   
  � Tag$4  .Purchased Item Receive (MRR) Input/Edit Screen��   IOUserControl#��[  .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� -    �Name$ gridMasterpr��$��0��$	��Top=�$��	10,0,10,0q� �� $
��Auto ��� $	��133 � � ColumnDefinitions �� $
��245* ��� $
��174* ��� $
��181* �.+��� $��0��$��0��$
��Left=�.+��� $��New/Selected Tranasction:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� -    ��Name$ 
stkIntropr�� �� Orientation$ Vertical=�$��0��$��2��$��1��$��3��$	��Top=�$
��Left=�$	��140��$��	0,3,3.6,0q�.+��� $ 
Horizontal=�.+��� -    ��Name$ lblSectCodTitlepr��$��S_tore :��$��Right=�$	��100�� �� Target ��  �� ElementName$ cmbSectCodpr���� -   	 ��Name$	 cmbSectCodpr��$	��300��
 �� 
IsReadOnly
 .  �� SelectedIndex$ 0���� -   $ lblssirCodTitlepr��$��S_upplier :��$��Right=�$	��110�� �� $ AtxtssirCodpr��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -     Name$ AtxtssirCodpr��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox   	LostFocus$ AtxtssirCodpr_LostFocus��   
SearchType$ C��$
��Left=�$��22��$	��300���� $ 
Horizontal=�.+��� -    ��Name$ btnOrderList��$��10,2,0,0q�$��O_rder��$	��Top=�$��Right=�$��22��$��50���� -	   $ 
lblorderno��$��40��$��Right=�$��List :���� -
   $	 cmbordernopr��$	��299��$��0,0,0,0q�
 . $ 0���� $ 
Horizontal=�$��27,0,0,0q�$
��Left=�.+��� -   $ 
lblchlndat��$��Cha_llan Date :�� �� $ xctk_dtchlndatpr��&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtchlndatpr��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130���� $ 
Horizontal=�$
��Left=�.+��� -   $ 	lblchlnno��$��Challa_n no :�� �� $ 	txtchlnno���� -    ��Name$ 	txtchlnno��$��98���� $ 
Horizontal=�.+��� -   $ lblrecevbyIdTitlepr��$��Received _By :��$��Right=�$	��100�� �� $ AtxtrecevbyIdpr��  -   $ AtxtrecevbyIdpr��$ C��$
��Left=�$��22��$	��300���� -   $ lblmrrRefTitlepr��$��_Reference (if any) :��$��Right=�$	��110�� �� $ txtmrrRefpr���  �� .+�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ txtmrrRefpr��$��0�� 
 wtxtBx#��[    	Watermark$ Type Referance��$��5,0,0,0q� �� TextWrapping$
 Wrap=�$�� ��$
��Left=�$��22��$	��299���� -   $ 
btnReferpr��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�$
��Left=�.+��� -   $ lblmrrNarTitlepr��$��Narrat_ion(if any):��$	��Top=�$��Right=�$��25��$	��100�� �� $ txtmrrNarpr���� -   $ txtmrrNarpr��$�� ��$
��Left=�$��50�� J� 	AllowDrop .  �� AcceptsReturn . $	��710��$
 Wrap=��� $ Vertical=�$��2��$��1��$	��Top=�$��Right=�$	��103��.+��� $ 
Horizontal=�.+��� -    ��Name$ chkDateBlocked��$��M.R.R _Date : ��|�.  $��Right=�$��Center=�$	��115��  -   $ xctk_dtpreqDatpr��#��[ $
��Bold"�$��22��$	��130��{�.  �� $��20��$y�Hidden=��� -   $ btnOkpr��  StdBtnStyle01#��[ $	��_Ok��$��75���� $ 
Horizontal=�.+��� -   $ lblreqNoTitlepr��$��
M.R.R. No:��$��Right=�$	��115���� -   $ 
lblreqNopr��$��MRRMM-XXXXX��$  MRRYYYYMMCCCCXXXXX��$	��130��$
��Bold"�$
��Left=��� $ 
Horizontal=�$��Right=�$��0, -8, 0, 0q�.+��� -   $ chkAutoTransListpr��$��_Show transactions list��|�.  $��Bottom=��� $��40��$��22��$y�Hidden=��� -   $ btnPrint2pr��$��_Print��$��70��$��22��$��Bottom=�$��12(�$��Verdana)�#��[ �� $��20��$y�Hidden=��� -   $ btnUpdatepr��#��[ $��_Update��$��75���� $��1��$	��Top=�
��� ���$��5��� -   $ gridDetailspr��$��2��$	��Top=�$y�Visible=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto �.+��� -   $ 	stkItempr��$ Vertical=�$��0��$	��Top=�$��Stretch=�.+��� -    $ stkItSp��.+��� $ 
Horizontal=�.+��� -!   $ lblUID��$��Item UI_D :��$��80��$��Right=� �� $ txtUID��  -"   $ txtUID��#��[ $ Mfg Product ID��$��5,0,0,0q�$��22��$	��130��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    AutoSelectBehavior$ OnFocus=��� -#   $ lblRSirCode��$��
Item Code:��$��70��$��Right=� �� $ txtRSirCodepr��  -$   $ txtRSirCodepr��#��[ $ 	Item Code��$��5,0,0,0q�|�.  $�� ��$
��Left=�$��22��$	��130�� �� 
IsReadOnly . �� -%   $ 
btnAddCode��$��_+��$��20��$��22��|�.  �� $��1��$y�Hidden=��� $ 
Horizontal=�.+��� -&   $ lblRSirCodeTitlepr��$��Descr_iption :��$��Right=�$��80�� �� $ txtRSirDescpr���  �� .+�  -'   $ txtRSirDescpr��$��0��#��[ $ Item Description��$��5,0,0,0q�$��22��$
 Wrap=�$	��330�� . $ OnFocus=��� -(   $ btntxtRSirDescpr��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� -)   $ btnRefreshCode��$��_f��$��20��$��22��|�.  �� $ 
Horizontal=�.+��� -*   $ lblMfgTitle��$��	Mfg. By :��$��Right=�$��80�� �� $ txtrqRatepr���� -+   $ 
lblmfgName��$	��240��$
��Left=��� -,   $ lblStdRatepr��$��
Std.Rate :��$��Right=�$��60���� --   $ 
lblStdRate��$��70��$
��Left=��� $��1��$y�Hidden=��� $ 
Horizontal=�.+��� -.   $ lblmrrQtyTitlepr��$��Recv. _Qty :��$��Right=�$��80�� �� $ txtmrrQtypr��  -/   $ txtmrrQtypr�� �� TextAlignment$ Right=�$��0��$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $ Type quent.��$�� ��$
��Left=�$��22��$��80���� -0   $ 
lblUnit1pr��$
��Unit��$��40���� -1   $ 
lblchlnqty��$��Challan Qt_y :��$��Right=�$��80�� �� $ 
txtchlnqty��  -2   $ 
txtchlnqty��$ Right=�$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $ Type quent.��$�� ��$
��Left=�$��22��$��90���� -3   $ 
lblUnit2pr��$��40���� $ 
Horizontal=�$
��Left=�.+��� -4   $ 	lblRatepr��$��Pur. _Rate :��$��Right=�$��80�� �� $ txtrqRatepr��  -5   $ txtrqRatepr��$ Right=�$��0��$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $
 Rate��$�� ��$
��Left=�$��22��$��80���� -6   $ lblAmtTitle��$��70��$��Amount :��$��Right=��� -7   $ lblAmountShow��$��80��$
��Left=�$
��Bold"��� -8   $ lblDisAmtTitle��$��
Discount :��$��Right=�$��65�� �� $ txtrqRatepr��  -9   $ txtDiscountpr��$ Right=�$��0��$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $ Discount��$�� ��$
��Left=�$��22��$��75���� $ 
Horizontal=�$
��Left=�.+��� -:   $ lblVatAmtTitle��$��	VAT Amt :��$��Right=�$��80��  -;   $ txtrqVatAmtpr��$ Right=�$��0��$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $ 
VAT Amount��$�� ��$
��Left=�$��22��$��80���� -<   $ lblNetAmtTitle��$��70��$��
Net Amt. :��$��Right=��� -=   $ lblNetAmtShow��$��80��$
��Left=�$
��Bold"��� ->   $ 
lblbatchno��$��	Batch # :��$��Right=�$��65�� �� $ 
txtbatchno��  -?   $ 
txtbatchno��$
 Left=�$ NoWrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $ Batch No��$�� ��$
��Left=�$��22��$��75���� $ 
Horizontal=�.+��� -@   $ 	lblmfgdat��$��
Mfg.Date :��$��Right=�$��80�� �� $ 
txtbatchno��  -A   $ 
xctk_dtMfg��#��[ $
��Bold"�$��22��$	��140���� -B   $ 	lblexpdat��$��Exp .Date :��$��Right=�$��90�� �� $ 
txtbatchno��  -C   $ 
xctk_dtExp��#��[ $
��Bold"�$��22��$	��140���� $y�Hidden=��� $ 
Horizontal=�.+��� -D   $ lblListBox1Title��$��_List :��$��65�� �� $ ListView1pr���� -E   $ 	lblOrdqty��$��Center=�$	��300���� $��10��$y�Hidden=��� -F   $ btnAddRecordpr��$��0,0,0,0q�#��[ $
��_Add��$	��Top=�$��Right=�$��22��$��75���� $ 
Horizontal=�$	��Top=�.+��� -G    ��Name$ ListView1pr��$	��450��$	��200�� �� View �� .P���  �� Width$	 100��$
��Code�� �� DisplayMemberBinding ��   �� Path$  trcode��� $	 270��$��Description�� �� $  trdesc1��� $ 45��$
��Unit�� �� $  trunit��� $ Vertical=�$��1��$
��Auto��$	��Top=�
�������$��10,0,0,0q�.+��� -H   $ ListView2pr��$	��400��$	��725��$��12(�$
��Auto=�$��Hidden=����� ! �� 
TargetType"! ��.��� " �� Property" � ��# �� Value# �	#FFE4F2E1 �� .P��� $ 30��$	��Sl#�� �� $
  trsl��� $	 100�� �� $  invcode�$ �� HeaderTemplate$ 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Center=�$	��Top=�.+��� $��Right=�$	��Top=�$��80��$��	Inv. Code���� $	 190��% �� CellTemplate%   .R��� $
��Left=�$	��Top=�$	��180������ $  trdesc�& � ToolTip& �� $  trdesc2�$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description of items��$	��150���� $ 70��$	��Qty��%   .R�� $��Center=�$	��Top=�$��70��.+��� ' ��Name$' txtBlocLvkreqQty��$��Right=�$	��Top=����� *mrrqty+( �� StringFormat$( {0: #,##0.00}���� $ 40��$
��Unit�� �� $  trunit��� $ 80��$��
Net Amount��%   .R�� $��Center=�$	��Top=�$��80��.+��� $��Right=�$	��Top=����� *mrramt+$( {0: #,##0.00}���� $ 80��$��Disc.Amt��%   .R�� $��Center=�$	��Top=�$��80��.+��� $��Right=�$	��Top=����� *	discamt+$( {0: #,##0.00}���� $ 60��$	��VAT��%   .R�� $��Center=�$	��Top=�$��60��.+��� $��Right=�$	��Top=����� *vatamt+$( {0: #,##0.00}���� $ 50��%   .R�� $��Right=�$��Center=�$��40��.+��� $ 
Horizontal=�.+��� �� ) ��Name$) hlDeleteRow��-I   * � ToolTip$* Delete record��
+ � Tag+ �� $
  trsl�.�.��� $��..\Images\bin1.png��$��16��$��16��$
��Fill=��� $��10��$y�Hidden=��� �� $) 	hlEditRow��-J   $* Edit record��+ �� $
  trsl�.�.��� $��..\Images\edit.png��$��18��$��20��$
��Fill=��� $ 
Horizontal=�.+��� -K   $ lblTotalTitle��$	��400��$��Right=�$��Total: ���� -L   $ lblTotaNetAmt��$	��100��$��Right=��� -M   $ lblTotaDiscAmt��$��80��$��Right=��� -N   $ lblTotaVATAmt��$��60��$��Right=�� -O   $ gridTransListpr��$��2��$	��Top=�$y�Hidden=�
�������$��	10,0,10,0q�.+��� $ Vertical=�$	��Top=�$	��450��.+��� $ 
Horizontal=�$
��Left=�.+��� $!��Existing Tranasctions List:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� $��10��$y�Hidden=��� -P   $' txtTransTitle��$>��8Transaction Date Range From: 01-Dec-2015 To: 15-Dec-2015��, �� TextAlignment$, Center=�$	��800���� -Q   $ lvTransListpr��$	��350��$��14(�$
��Auto=�$��Hidden=����� "! ��.��� " � ��# �	#FFE4F2E1 �� .P��� $ 60��$	��Sl#��%   .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=�$
��Bold"����� *slnum+�� $�.���� $	 100��$
��Date�� �� $  	memoDate1��� $	 140��$��Memo No.��$
��Bold"� �� $  memonum1��� $	 110��$��	Order No.��$
��Bold"� �� $  orderno��� $	 100��$��Challan No.��$
��Bold"� �� $  chlnno��� $	 170��$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��
Store Name��$	��170��%   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *
sectname+& �� *	reqbyName+�� $	 190�� �� $  ssirname�$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Supplier��$	��190���� $	 250��$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��
Receive by��$	��220��%   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *
recvbyName+& �� *
Naration+�� $��5��]� .+��� $ 
Horizontal=�$
��Left=�$
��Left=�.+��� $��	Recurring��#��[ $��80��$��22��{�.  �� $��10��$y�Hidden=��� -R   $ chkFilterpr��- d� 	IsChecked$- FalseY�$��Filter Records��$��Center=�$��Center=�� . � AllowsTransparency. . $��Slide=����� $ chkFilterpr��$  	IsChecked�/ � PlacementTarget/ �� $ chkFilterpr��.���� $��0,0,5,5q�$��0q�0 �� CornerRadius$0 8������ 1 �� EndPoint$	1 0,1&�2 �� 
StartPoint$	2 0,1&�.��� 3 � Color$3 	#FFECFBFB��4 � Offset$	4 0.0Q�� $3 	#FFCED9E4��$	4 0.5Q�� $3 	#FF5F6E6E��$	4 1.0Q����� $1 0.5,1&�$2 0.5,0&�.��� $3 	#FFF7F7F7��$	4 0.0Q�� $3 	#FFCED9E4��$	4 0.5Q�� $3 	#FF22F5F5��$	4 1.0Q�5 J� Effect5 3  -System.Windows.Media.Effects.DropShadowEffect  6   Color$6 Black��7   ShadowDepth$7 5Q�8   
BlurRadius$8 20Q�9   Opacity$	9 0.8Q�:   RenderingBias$: Quality=�._��� $	��340��$	��130������ $1 0.5,1&�$2 0.5,0&�.��� $3 	#FFE4F2E1��$4 0.491Q�� $3 	#FF69B28B��$4 0.957Q�z��� -S   theTransform1��.+��� $ Vertical=�.+��� $1��+Filter Option For Getting Transaction List:��$��12(�$��Italic$�$	��340��$��Center=�|�. ���� $1 0.5,1&�; � MappingMode$; RelativeToBoundingBox=�$2 0.5,0&�.��� $3 White��� $3 	#FFB7B7B7��$
4 0.46Q�� $3 	#FFDDDDDD��$4 0.233Q��� $ 
Horizontal=�.+��� $��From :��$��12(�$��Italic$�$��45��$
��Left=�  -T   $ xctk_dtpFromDate��#��[ $��20��$	��130���� $
��To :��$��Italic$�$��28��$��Center=�  -U   $ xctk_dtpToDate��#��[ $��20��$	��130���� $y�Hidden=��� $y�Hidden=��� $ 
Horizontal=�.+��� $��Store :��$��45��$
��Left=��� -V   $	 cmbfStNm��$	��288��< �� SelectedValue$	< Tag��$ 0���� -W   $ 	btnFilter��$��	Go Filter��$��20��$��60��$��12(�$��Verdana)�#��[ = �� Left$	= 155��
> �� Top$	> 100���� $��30��$y�Hidden=��� $ 
Horizontal=�$��Right=�$��Right=�.+��� $��10��$y�Hidden=��� $
��Edit��#��[ $��80��$��22���� $��10��$y�Hidden=��� $��Delete��#��[ $��80��$��22���� $��10��$y�Hidden=��� -X   $ chkPrint2pr��$- FalseY�$��Print Memo/List��$��Center=�$��Center=�� . . $��Slide=����� $ chkPrint2pr��$  	IsChecked�/ �� $ chkPrint2pr��.���� $��0,0,5,5q�$��0q�$0 8������ $	1 0,1&�$	2 0,1&�.��� $3 	#FFECFBFB��$	4 0.0Q�� $3 	#FFCED9E4��$	4 0.5Q�� $3 	#FF5F6E6E��$	4 1.0Q����� $1 0.5,1&�$2 0.5,0&�.��� $3 	#FFF7F7F7��$	4 0.0Q�� $3 	#FFCED9E4��$	4 0.5Q�� $3 	#FF22F5F5��$	4 1.0Q�5   $6 Black��$7 5Q�$8 20Q�$	9 0.8Q�$: Quality=�._��� $	��130��$	��150������ $1 0.5,1&�$2 0.5,0&�.��� $3 	#FFE4F2E1��$4 0.491Q�� $3 	#FF69B28B��$4 0.957Q�.+��� $ Vertical=�$	��134��.+��� $��Print Options��$��12(�$��Italic$�$
��Bold"�
���**��$
��Left=��� $ Vertical=�$��10,0,0,0q�.+�	� -Y   ? 	�Name$? rb3SelectedMemopr��$��Selected Memo��$
- TrueY��� $y�Hidden=�	� -Z   $? rb3TableRecoredspr��$��Table Records���� �� $ Vertical=�$��10,2,0,0q�.+�	� -[   $? rb3QuickPrintpr��$��Quick Print���� $y�Hidden=�	� -\   $? rb3PrintPreviewpr��$��Print Preview��$
- TrueY��� -]   $ 	btnPrint3��$��Go Print��$��20��$��60��$��12(�$��Verdana)�#��[ $= 34��$	> 116���� $��20��$y�Hidden=�
   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  *    $ASITHmsWpf.Inventory.frmEntryPurReq1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  :d2http://schemas.microsoft.com/expression/blend/2008  -   
  � Tag$3  -Item Purchase Requisition - Entry/Edit Screen��   IOUserControl#��[  .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� -    �Name$ gridMasterpr��$��0��$	��Top=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��245* ��� $
��174* ��� $
��181* �.+��� $��0��$��0��$
��Left=�.+��� $��New/Selected Tranasction:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� -    ��Name$ 
stkIntropr�� �� Orientation$ Vertical=�$��0��$��2��$��1��$��3��$	��Top=�$
��Left=�$	��100��$��	0,3,3.6,0q�.+��� $ 
Horizontal=�.+��� -    ��Name$ lblSectCodTitlepr��$��Requision S_tore :��$��Right=�$	��100�� �� Target ��  �� ElementName$ cmbSectCodpr���� -   	 ��Name$	 cmbSectCodpr��$	��300��
 �� 
IsReadOnly
 .  �� SelectedIndex$ 0���� $ 
Horizontal=�.+��� -   $ lblreqByIdTitlepr��$��Requision _By :��$��Right=�$	��100�� �� $ AtxtreqByIdpr��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -     Name$ AtxtreqByIdpr��   
SearchType$ C��$
��Left=�$��22��$	��300���� -   $ lblreqRefTitlepr��$��_Reference (if any) :��$��Right=�$	��110�� �� $ txtreqRefpr���  �� .+�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -	     Name$ txtreqRefpr��$��0�� 
 wtxtBx#��[    	Watermark$ Type Referance��$��5,0,0,0q� �� TextWrapping$
 Wrap=�$�� ��$
��Left=�$��22��$	��300���� -
    ��Name$ 
btnReferpr��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�.+��� -   $ lblreqNarTitlepr��$��Narrat_ion(if any):��$	��Top=�$��Right=�$��25��$	��100�� �� $ txtreqNarpr���� -    ��Name$ txtreqNarpr��$�� ��$
��Left=�$��50�� J� 	AllowDrop .  �� AcceptsReturn . $	��710��$
 Wrap=��� $ Vertical=�$��2��$��1��$��Bottom=�$��Right=�$��80��.+��� $ 
Horizontal=�.+��� -    ��Name$ chkDateBlocked��$��Requision _Date : ��|�.  $��Right=�$��Center=�$	��115��&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtpreqDatpr��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130��{�.  �� $��20��$y�Hidden=��� -   $ btnOkpr��  StdBtnStyle01#��[ $	��_Ok��$��75���� $ 
Horizontal=�.+��� -   $ lblreqNoTitlepr��$��Requisition No:��$��Right=�$	��115���� -   $ 
lblreqNopr��$��REQMM-XXXXX��$  REQYYYYMMCCCCXXXXX��$	��130��$
��Bold"�$
��Left=��� $ 
Horizontal=�$��Right=�$��0, -8, 0, 0q�.+��� -   $ chkAutoTransListpr��$��_Show transactions list��|�.  $��Bottom=��� $��40��$��22��$y�Hidden=��� -   $ btnPrint2pr��$��_Print��$��70��$��22��$��Bottom=�$��12(�$��Verdana)�#��[ �� $��20��$y�Hidden=��� -   $ btnUpdatepr��#��[ $��_Update��$��75���� $��1��$	��Top=�
��� ���$��5��� -   $ gridDetailspr��$��2��$	��Top=�$y�Visible=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto �.+��� -   $ 	stkItempr��$ Vertical=�$��0��$	��Top=�$��Stretch=�.+��� $ 
Horizontal=�.+��� -   $ lblUID��$��Item UI_D Mark:��$	��100��$��Right=� �� $ txtUID��  -   $ txtUID��#��[ $ Manufacturer's Product ID��$��5,0,0,0q�$��22��$	��130��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    AutoSelectBehavior$ OnFocus=��� -   $ lblRSirCode��$��_Item Code:��$��70��$��Right=� �� $ txtRSirDescpr��  -   $ txtRSirCodepr��#��[ $ 	Item Code��$��5,0,0,0q�|�.  $�� ��$
��Left=�$��22��$	��130�� �� 
IsReadOnly . �� -   $ 
btnAddCode��$��_+��$��20��$��22��|�.  �� $��1��$y�Hidden=��� $ 
Horizontal=�.+��� -   $ lblRSirCodeTitlepr��$��Description:��$	��100��$��Right=� �� $ txtRSirDescpr���  �� .+�  -   $ txtRSirDescpr��$��0��#��[ $ Item Description��$��5,0,0,0q�$��22��$
 Wrap=�$	��330�� . $ OnFocus=��� -   $ btntxtRSirDescpr��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� -   $ btnRefreshCode��$��_f��$��20��$��22��|�.  �� $ 
Horizontal=�$	��450��.+��� -    $ lblreqQtyTitlepr��$��Req. _Quantity :��$��Right=�$	��100�� �� $ txtreqQtypr��  -!   $ txtreqQtypr�� �� TextAlignment$ Right=�$��0��$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $ Req. Quantity��$�� ��$
��Left=�$��22��$��80���� -"   $ 
lblUnit1pr��$
��Unit��$��50���� -#   $ 	lblRatepr��$��_Rate :��$
��Left=�$��38�� �� $ txtrqRatepr��  -$   $ txtrqRatepr��$ Right=�$��0��$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $
 Rate��$�� ��$
��Left=�$��22��$��70���� -%   $ lblAmountShow��$	��110��$��Amount��$��Right=�$
��Bold"��� $ 
Horizontal=�.+��� -&   $ lblListBox1Title��$��_List :��$��80�� �� $ ListView1pr���� $	��295��$y�Hidden=��� -'   $ btnAddRecordpr��#��[ $
��_Add��$	��Top=�$��Right=�$��22��$��75���� $ 
Horizontal=�$	��Top=�.+��� -(    ��Name$ ListView1pr��$	��450��$	��340�� �� View �� .P���  �� Width$	 100��$
��Code�� �� DisplayMemberBinding ��  �� Path$ trcode��� $	 320��$��Description�� �� $ trdesc��� $ 
Horizontal=�$��1��$
��Auto��$	��Top=�
�������$��10,0,0,0q�.+��� -)   $ ListView2pr��$	��450��$	��721��$��14(�$
��Auto=�$��Hidden=�����   �� 
TargetType"  ��.��� ! �� Property! � ��" �� Value" �	#FFE4F2E1 �� .P��� $ 50��$	��Sl#��# �� CellTemplate# 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=����� *trsl+�� $	 300�� �� $ trdesc�$ �� HeaderTemplate$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description of items��$	��430���� $ 80��$	��Qty��#   .R�� $��Right=�$	��Top=�$��80��.+��� % ��Name$% txtBlocLvkreqQty��$��Right=�$	��Top=����� *reqqty+& �� StringFormat$& {0: #,##0.00}���� $ 50��$
��Unit�� �� $ trunit��� $ 80��$
��Rate��#   .R�� $��Right=�$	��Top=�$��80��.+��� $% txtBlocLvkreqRate��$��Right=�$	��Top=����� *	reqrate+$& {0: #,##0.00}���� $ 90��$��Amount��#   .R�� $��Right=�$	��Top=�$��80��.+��� $% txtBlocLvkreqAmt��$��Right=�$	��Top=����� *	reqamount+$& {0: #,##0.00}���� $ 80��#   .R�� $��Right=�$��Center=�$��70��.+��� $ 
Horizontal=�.+��� �� ' ��Name$' hlDeleteRow��-*   ( � ToolTip$( Delete record��
) � Tag) �� $
 trsl�.�.��� $��..\Images\bin1.png��$��16��$��16��$
��Fill=��� $��10��$y�Hidden=��� �� $' 	hlEditRow��-+   $( Edit record��) �� $
 trsl�.�.��� $��..\Images\edit.png��$��18��$��20��$
��Fill=�� -,   $ gridTransListpr��$��2��$	��Top=�$y�Hidden=�
�������$��	10,0,10,0q�.+��� $ Vertical=�$	��Top=�$	��450��.+��� $ 
Horizontal=�$
��Left=�.+��� $!��Existing Tranasctions List:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� $��10��$y�Hidden=��� $>��8Transaction Date Range From: 01-Dec-2015 To: 15-Dec-2015��* �� TextAlignment$* Center=�$	��800���� --   $ lvTransListpr��$	��350��$��14(�$
��Auto=�$��Hidden=����� "  ��.��� ! � ��" �	#FFE4F2E1 �� .P��� $ 60��$	��Sl#��#   .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=�$
��Bold"����� *slnum+�� $�.���� $	 100��$
��Date�� �� $ 	memoDate1��� $	 140��$��Memo No.��$
��Bold"� �� $ memonum1��� $	 250��$   .R�� $��Center=�$	��Top=�.+��� $��Center=�$	��Top=�$��
Store Name��$	��220��#   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *
sectname+�� $	 250�� �� $ PreparByName�$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Requision By��$	��220���� $	 250��$   .R�� $��Center=�$	��Top=�.+��� $��Center=�$	��Top=�$��	Reference��$	��220��#   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *	Referance++ � ToolTip+ �� *
Naration+�� $	 100��$��Status�� �� $ 
MemoStatus��� $��5��]� .+��� $ 
Horizontal=�$
��Left=�$
��Left=�.+��� $��	Recurring��#��[ $��80��$��22���� $��10��$y�Hidden=��� -.   $ chkFilterpr��, d� 	IsChecked$, FalseY�$��Filter Records��$��Center=�$��Center=�� - � AllowsTransparency- . $��Slide=����� $ chkFilterpr��$ 	IsChecked�. � PlacementTarget. �� $ chkFilterpr��.���� $	��180��$	��146������ / �� EndPoint$/ 0.5,1&�0 �� 
StartPoint$0 0.5,0&�.��� 1 � Color$1 Black��2 � Offset$2 1Q�� $1 	#FFF1EFC9��$2 0.206Q�� $1 	#FF7D8575��$2 0.041Q�� $1 	#FFC4E2EB��$2 0.254Q�� $1 	#FFA5CF91��$2 0.911Q�z��� -/   theTransform1pr��.+��� $ Vertical=�.+��� $��Transaction Date Range��$��12(�$��Italic$�$	��180��$
��Left=��� $ 
Horizontal=�.+��� $��From:��$��12(�$��Italic$�$��45��$��Right=�  -0   $ xctk_dtpFromDatepr��#��[ $��20��$	��130���� $ 
Horizontal=�.+��� $	��To:��$��Italic$�$��45��$��Right=�  -1   $ xctk_dtpToDatepr��#��[ $��20��$	��130���� -2   $ btnFilterpr��$��	Go Filter��$��20��$��60��$��12(�$��Verdana)�#��[ 3 �� Left$3 60��
4 �� Top$	4 116���� $��30��$y�Hidden=��� $ 
Horizontal=�$��Right=�$��Right=�.+��� $��10��$y�Hidden=��� $
��Edit��#��[ $��80��$��22���� $��10��$y�Hidden=��� $��Delete��#��[ $��80��$��22���� $��10��$y�Hidden=��� -3   $ chkPrint2pr��$, FalseY�$��Print Memo/List��$��Center=�$��Center=�� - . $��Slide=����� $ chkPrint2pr��$ 	IsChecked�. �� $ chkPrint2pr��.���� $	��134��$	��146������ $/ 0.5,1&�$0 0.5,0&�.��� $1 Black��$2 1Q�� $1 	#FFF1EFC9��$2 0.206Q�� $1 	#FF7D8575��$2 0.041Q�� $1 	#FFC4E2EB��$2 0.254Q�� $1 	#FFA5CF91��$2 0.911Q�z��� -4   theTransform2pr��.+��� $ Vertical=�$	��134��.+��� $��Print Options��$��12(�$��Italic$�$
��Bold"�
���**��$
��Left=��� $ Vertical=�$��10,0,0,0q�.+�	� -5   5 	�Name$5 rb3SelectedMemopr��$��Selected Memo��$
, TrueY��� $y�Hidden=�	� -6   $5 rb3TableRecoredspr��$��Table Records���� �� $ Vertical=�$��10,2,0,0q�.+�	� -7   $5 rb3QuickPrintpr��$��Quick Print���� $y�Hidden=�	� -8   $5 rb3PrintPreviewpr��$��Print Preview��$
, TrueY��� -9   $ 	btnPrint3��$��Go Print��$��20��$��60��$��12(�$��Verdana)�#��[ $3 34��$	4 116���� $��20��$y�Hidden=�
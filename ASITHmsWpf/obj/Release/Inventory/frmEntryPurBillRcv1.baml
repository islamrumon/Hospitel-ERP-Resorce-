   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  .    (ASITHmsWpf.Inventory.frmEntryPurBillRcv1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  -   
  � Tag$6  0Purchase/Supplier Bill Receive Input/Edit Screen��   IOUserControl#��[  .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� -    �Name$ gridMasterpr��$��0��$	��Top=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��245* ��� $
��174* ��� $
��181* �.+��� $��0��$��0��$
��Left=�.+��� $��Bill Receive Tranasction:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� -    ��Name$ 
stkIntropr�� �� Orientation$ Vertical=�$��0��$��2��$��1��$��3��$	��Top=�$
��Left=�$	��130��$��	0,3,3.6,0q�.+��� $ 
Horizontal=�.+��� -    ��Name$ lblSectCodTitlepr��$��	 S_tore :��$��Right=�$	��100�� �� Target ��  �� ElementName$ cmbSectCodpr���� -   	 ��Name$	 
cmbSectCod��$	��300��
 �� 
IsReadOnly
 .  �� SelectedIndex$ 0���� -   $ lblssirCodTitlesup��$��S_upplier :��$��Right=�$	��110�� �� $ 
AtxSsircod��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -     Name$ 
AtxSsircod��   
SearchType$ C��$
��Left=�$��22��$	��300���� $ 
Horizontal=�.+��� -   $ lblapproveById��$��Received _By :��$��Right=�$	��100�� �� $ AtxtRcvById��  -	   $ AtxtRcvById��$ C��$
��Left=�$��22��$	��300���� -
   $ 
lblorderno��$	��110��$��Right=�$��
Order No :�� �� $ 
cmborderno���� -   $	 
cmborderno��$	��300��$��0,0,0,0q�
 . $ 0���� $ 
Horizontal=�.+��� -   $ lbqbillRefTitlepr��$��Sup. Bill_Ref.:��$��Right=�$	��100�� �� $ txtbillRefpr���  �� .+�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ txtbillRefpr��$��0�� 
 wtxtBx#��[    	Watermark$ Type Referance��$��5,0,0,0q� �� TextWrapping$
 Wrap=�$�� ��$
��Left=�$��22��$	��300���� -    ��Name$ 
btnReferpr��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $��Sub. Bill Date :��$	��110��$��Right=�&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtrefdatDatpr��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130���� $ 
Horizontal=�.+��� -   $ lblqrNarTitlepr��$��Narrat_ion(if any):��$	��Top=�$��Right=�$��25��$	��100�� �� $ 
txtqrNarpr���� -    ��Name$ 
txtqrNarpr��$�� ��$
��Left=�$��50�� J� 	AllowDrop .  �� AcceptsReturn . $	��710��$
 Wrap=��� $ Vertical=�$��2��$��1��$��Bottom=�$��Right=�$��80��.+��� $ 
Horizontal=�.+��� -    ��Name$ chkDateBlocked��$��Bill _Date : ��|�.  $��Center=�$��Center=�$	��115��  -   $ xctk_dtbillDat��#��[ $
��Bold"�$��22��$	��130��{�.  �� $��20��$y�Hidden=��� -   $ btnOkpr��  StdBtnStyle01#��[ $	��_Ok��$��75���� $ 
Horizontal=�.+��� -   $ lblreqNoTitlepr��$��Bill No:��$��Right=�$	��115���� -   $ 	lblbillNo��$��BILMM-XXXXX��$  BILYYYYMMCCCCXXXXX��$	��130��$
��Bold"�$
��Left=��� $ 
Horizontal=�$��Right=�$��0, -8, 0, 0q�.+��� -   $ chkAutoTransList��$��_Show transactions list��|�.  $��Bottom=��� $��40��$��22��$y�Hidden=��� -   $ 	btnPrint2��$��_Print��$��70��$��22��$��Bottom=�$��12(�$��Verdana)�#��[ �� $��20��$y�Hidden=��� -   $ 	btnUpdate��#��[ $��_Update��$��75���� $��1��$	��Top=�
��� ���$��5��� -   $ gridDetails��$��2��$	��Top=�$y�Visible=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto �.+��� -   $ stkItem��$ Vertical=�$��0��$	��Top=�$��Stretch=�.+��� $ 
Horizontal=�.+��� -   $ lblmrrno��$��Right=�$��
M.R.R no :��$	��100�� �� $ cmbmrrno���� -   $	 cmbmrrno��$	��150��$��0,0,0,0q�
 . $ 0���� $ 
Horizontal=�.+��� -   $ lblUID��$��Item UI_D Mark:��$	��100��$��Right=� �� $ txtUID��  -   $ txtUID��#��[ $ Manufacturer's Product ID��$��5,0,0,0q�$��22��$	��150��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    AutoSelectBehavior$ OnFocus=��� -    $ lblRSirCode��$��
Item Code:��$��70��$��Right=� �� $ txtRSirCode��  -!   $ txtRSirCode��#��[ $ 	Item Code��$��5,0,0,0q�$�� ��$
��Left=�$��22��$	��130�� �� 
IsReadOnly . �� $ 
Horizontal=�.+��� -"   $ lblRSirCodeTitle��$��Descr_iption:��$��Right=�$	��100�� �� $ txtRSirDesc���  �� .+�  -#   $ txtRSirDesc��$��0��#��[ $ Item Description��$��5,0,0,0q�$��22��$
 Wrap=�$	��350�� . $ OnFocus=��� -$   $ btntxtRSirDesc��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�$
��Left=�.+��� $ 
Horizontal=�.+��� -%   $ lblordQtyTitle��$��_Quantity :��$
��Left=�$��60�� �� $ 
txtbillQty��  -&   $ 
txtbillQty�� �� TextAlignment$ Right=�$��0��$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $ Type quent.��$�� ��$
��Left=�$��22��$��70���� -'   $ lblUnitordq1��$�� Unit��$��60���� -(   $ lblRateTitle��$��Rat_e :��$��Right=�$��60�� �� $ txtrate��  -)   $ txtrate��$��Right=�$��0��$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $
 Rate��$�� ��$��22��$��90���� -*   $ lblAmountCal��$��Right=�$��Amount��$	��110��$
��Bold"��� $ 
Horizontal=�$
��Left=�$��1��$��0��.+��� -+   $ lbldisamtTitle��$��_Dis. Amt :��$
��Left=�$��61�� �� $ 	txtdisamt��  -,   $ 	txtdisamt��$ Right=�$��0��$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $ Type quent.��$�� ��$
��Left=�$��22��$��70���� --   $ lbldisamtUnit2��$	�� TK��$��40���� -.   $ lblnetAmtTitle��$��Net Amount :��$��Right=��� -/   $ lblnetAmount��$	��110��
���  ��$
��Bold"��� $��10��$y�Hidden=��� -0   $ btnAddRecord��#��[ $
��_Add��$	��Top=�$��22��$��75���� $ 
Horizontal=�.+��� -1   $ lblListBox1Title��$��_List :��$��80�� �� $ 	ListView1���� $ 
Horizontal=�$	��Top=�.+��� -2    ��Name$ 	ListView1��$	��450��$	��263�� �� View �� .P���  �� Width$ 90��$
��Code�� �� DisplayMemberBinding ��  �� Path$ trcode��� $	 300��$��Description�� �� $ trdesc��� $ 40��$
��Unit�� �� $ trunit��� $ 
Horizontal=�$��1��$
��Auto��$	��Top=�
�������$��10,0,0,0q�.+��� -3   $ 	ListView2��$	��420��$	��721��$��14(�$
��Auto=�$��Hidden=�����   �� 
TargetType"  ��.��� ! �� Property! � ��" �� Value" �	#FFE4F2E1 �� .P��� $ 40��$	��Sl#�� �� $
 trsl��� $	 280�� �� $ trdesc�# �� HeaderTemplate# 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description of items��$	��280���� $ 80��$	��Qty��$ �� CellTemplate$   .R�� $��Center=�$	��Top=�$��80��.+��� % ��Name$% txtBlocLvkreqQty��$��Center=�$	��Top=�$
��Bold"����� *	billqty+& �� StringFormat$& {0: #,##0.00}���� $ 65��$��	Dis. Amt.�� �� $ disamt�$& {0: #,##0.00}���� $ 60��$
��Unit�� �� $ trunit��� $ 90��$��
Net Amount�� �� $ netamt�$& {0: #,##0.00}���� $ 80��$   .R�� $��Right=�$��Center=�$��70��.+��� $ 
Horizontal=�.+��� �� ' ��Name$' hlDeleteRow��-4   ( � ToolTip$( Delete record��
) � Tag) �� $
 trsl�.�.��� $��..\Images\bin1.png��$��16��$��16��$
��Fill=��� $��10��$y�Hidden=��� �� $' 	hlEditRow��-5   $( Edit record��) �� $
 trsl�.�.��� $��..\Images\edit.png��$��18��$��20��$
��Fill=�� -6   $ gridTransList��$��2��$	��Top=�$y�Hidden=�
�������$��	10,0,10,0q�.+��� $ Vertical=�$	��Top=�$	��380��.+��� $ 
Horizontal=�$
��Left=�.+��� $!��Existing Tranasctions List:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� $��10��$y�Hidden=��� $>��8Transaction Date Range From: 01-Dec-2015 To: 15-Dec-2015��* �� TextAlignment$* Center=�$	��800���� -7   $ lvTransList��$	��330��$��14(�$
��Auto=�$��Hidden=����� "  ��.��� ! � ��" �	#FFE4F2E1 �� .P��� $ 60��$	��Sl#��$   .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=�$
��Bold"����� *slnum+�� $�.���� $	 100��$
��Date�� �� $ 	memoDate1��� $	 140��$��Memo No.��$
��Bold"� �� $ memonum1��� $	 250��#   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Supplier Name��$	��250��$   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *
ssirName+�� $	 250��#   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��
Store Name��$	��250��$   .R�� $
��Left=�$	��Top=�$	��250��.+��� $
��Left=�$	��Top=����� *
sectName++ � ToolTip+ �� *
recvbyName+�� $	 250��#   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��	Reference��$	��220��$   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *	Referance++ �� *
Naration+�� $	 100��$��Status�� �� $ 
MemoStatus��� $��5��]� .+��� $ 
Horizontal=�$
��Left=�$
��Left=�.+��� $��	Recurring��#��[ $��80��$��22���� $��10��$y�Hidden=��� -8   $ 	chkFilter��, d� 	IsChecked$, FalseY�$��Filter Records��$��Center=�$��Center=�� - � AllowsTransparency- . $��Slide=����� $ 	chkFilter��$ 	IsChecked�. � PlacementTarget. �� $ 	chkFilter��.���� $	��180��$	��146������ / �� EndPoint$/ 0.5,1&�0 �� 
StartPoint$0 0.5,0&�.��� 1 � Color$1 Black��2 � Offset$2 1Q�� $1 	#FFF1EFC9��$2 0.206Q�� $1 	#FF7D8575��$2 0.041Q�� $1 	#FFC4E2EB��$2 0.254Q�� $1 	#FFA5CF91��$2 0.911Q�z��� -9   theTransform1��.+��� $ Vertical=�.+��� $��Transaction Date Range��$��12(�$��Italic$�$	��180��$
��Left=��� $ 
Horizontal=�.+��� $��From:��$��12(�$��Italic$�$��45��$��Right=�  -:   $ xctk_dtpFromDate��#��[ $��20��$	��130���� $ 
Horizontal=�.+��� $	��To:��$��Italic$�$��45��$��Right=�  -;   $ xctk_dtpToDate��#��[ $��20��$	��130���� -<   $ 	btnFilter��$��	Go Filter��$��20��$��60��$��12(�$��Verdana)�#��[ 3 �� Left$3 60��
4 �� Top$	4 116���� $��30��$y�Hidden=��� $ 
Horizontal=�$��Right=�$��Right=�.+��� $��10��$y�Hidden=��� $
��Edit��#��[ $��80��$��22���� $��10��$y�Hidden=��� $��Delete��#��[ $��80��$��22���� $��10��$y�Hidden=��� -=   $ 	chkPrint2��$, FalseY�$��Print Memo/List��$��Center=�$��Center=�� - . $��Slide=����� $ 	chkPrint2��$ 	IsChecked�. �� $ 	chkPrint2��.���� $	��134��$	��146������ $/ 0.5,1&�$0 0.5,0&�.��� $1 Black��$2 1Q�� $1 	#FFF1EFC9��$2 0.206Q�� $1 	#FF7D8575��$2 0.041Q�� $1 	#FFC4E2EB��$2 0.254Q�� $1 	#FFA5CF91��$2 0.911Q�z��� ->   theTransform2��.+��� $ Vertical=�$	��134��.+��� $��Print Options��$��12(�$��Italic$�$
��Bold"�
���**��$
��Left=��� $ Vertical=�$��10,0,0,0q�.+�	� -?   5 	�Name$5 rb3SelectedMemo��$��Selected Memo��$
, TrueY��� $y�Hidden=�	� -@   $5 rb3TableRecoreds��$��Table Records���� �� $ Vertical=�$��10,2,0,0q�.+�	� -A   $5 rb3QuickPrint��$��Quick Print���� $y�Hidden=�	� -B   $5 rb3PrintPreview��$��Print Preview��$
, TrueY��� -C   $ 	btnPrint3��$��Go Print��$��20��$��60��$��12(�$��Verdana)�#��[ $3 34��$	4 116���� $��20��$y�Hidden=�
   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  0    *ASITHmsWpf.Inventory.frmEntryPurQuotation1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4UC%clr-namespace:ASITHmsWpf.UserControls     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -   
  � Tag$3  -Suppliers Price Quotation - Entry/Edit Screen��   IOUserControl#��[  .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� -    �Name$ 
gridMaster��$��0��$	��Top=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��245* ��� $
��174* ��� $
��181* �.+��� $��0��$��0��$
��Left=� �� Orientation$ 
Horizontal=�.+��� $�� Quation   Section:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� -    ��Name$ stkIntro��$ Vertical=�$��0��$��2��$��1��$��3��$	��Top=�$
��Left=�$	��100��$��	0,3,3.6,0q�.+��� $ 
Horizontal=�.+��� -    ��Name$ lblSectCodTitle��$��S_tore :��$��Right=�$	��100�� �� Target ��  �� ElementName$ 
cmbSectCod���� -   	 ��Name$	 
cmbSectCod��$	��300��
 �� 
IsReadOnly
 .  �� SelectedIndex$ 0���� -   $ lblSectCod2Title��$��
Supp_lier:��$��Right=�$	��110�� �� $ AtxtSsircod��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -     Name$ AtxtSsircod��   
SearchType$ C��$
��Left=�$��22��$	��300���� $ 
Horizontal=�.+��� -   $ lblRecByIdTitle��$��Received _By:��$��Right=�$	��100�� �� $ AtxtRecById��  -	   $ AtxtRecById��$ C��$
��Left=�$��22��$	��300���� -
   $ lblSrfRefTitle��$��_Reference (if any):��$��Right=�$	��110�� �� $ 	txtSrfRef���  �� .+�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ 	txtSrfRef��$��0�� 
 wtxtBx#��[    	Watermark$ Type Referance (if any)��$��5,0,0,0q�$��22�� �� TextWrapping$
 Wrap=�$	��300��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    AutoSelectBehavior$ OnFocus=��� -    ��Name$ btntxtSrfRef��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�.+��� -   $ lblSrfNarTitle��$��_Narration (if any):��$	��Top=�$��Right=�$��25��$	��100�� �� $ 	txtSrfNar���� -    ��Name$ 	txtSrfNar��$�� ��$
��Left=�$��50�� J� 	AllowDrop .  �� AcceptsReturn . $	��710��$
 Wrap=��� $ Vertical=�$��2��$��1��$��Bottom=�$��Right=�$��80��.+��� $ 
Horizontal=�.+��� -    ��Name$ chkDateBlocked��$��Quotation _Date : ��|�.  $��Right=�$��Center=�$	��115��&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtpQutDat��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130��{�.  �� $��20��$y�Hidden=��� -   $ btnOk��  StdBtnStyle01#��[ $	��_Ok��$��75���� $ 
Horizontal=�.+��� -   $ lblQutNoTitle��$��Quotation No:��$��Right=�$	��115���� -   $ lblQutNo��$��PQTMM-XXXXX��$  PQTYYYYMMCCCCXXXXX��$	��130��$
��Bold"�$
��Left=��� $ 
Horizontal=�$��Right=�$��0, -8, 0, 0q�.+��� -   $ chkAutoTransList��$��_Show transactions list��|�.  $��Bottom=��� $��25��$��22��$y�Hidden=��� -   $ 	btnPrint2��$��_Print��$��75��$��22��$��Bottom=�$��12(�$��Verdana)�#��[ �� $��30��$y�Hidden=��� -   $ 	btnUpdate��#��[ $��_Update��$��75���� $��1��$	��Top=�
��� ���$��5��� -   $ gridDetails��$��2��$	��Top=�$y�Visible=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto �.+��� -   $ stkItem��$ Vertical=�$��0��$	��Top=�$��Stretch=�.+��� $ 
Horizontal=�.+��� -   $ lblQuotCodeTitle��$��_Item Code & Name:��$��Right=�$	��115�� �� $ txtRSirDesc���� $ 
Horizontal=�.+�  -   $ txtRSirCode��#��[ $ 	Item Code��$��5,0,0,0q� �� 
IsReadOnly . $��22��$	��100�� . $ OnFocus=��  �� $��22 �.+�  -   $ txtRSirDesc��$��0��#��[ $ Item Description��$��5,0,0,0q�$��22��$
 Wrap=�$	��350�� . $ OnFocus=��� -   $ 
btntxtQuot��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�.+��� -   $ lblSrfQtyTitle��$��_Quantity :��$��Right=�$	��100�� �� $ 
txtQuotQty��  -   $ 
txtQuotQty��$��0��#��[ $ Type Quant.��$��5,0,0,0q�$��22��$
 Wrap=�$��70�� . $ OnFocus=��� -   $ lblUnit1��$
��Unit��$��35���� -    $ lblSrfRatTitle��$��_Rate:��$
��Left=�$��35�� �� $ 
txtQuotRat��  -!   $ 
txtQuotRat��$��0��#��[ $ 
Type Rate.��$��5,0,0,0q�$��22��$
 Wrap=�$��70�� . $ OnFocus=��� -"   $ lblUnit2��$��Amount:��$��55���� -#   $ 	lblAmount��$�� ��$��80���� $ 
Horizontal=�$
��Left=�.+��� -$   $ lblQutNoteTitle��$��N_ote :��$��Right=�$	��Top=�$��25��$	��100�� �� $ 
txtQutNote���  �� $��22 �.+�  -%   $ 
txtQutNote��$��0��#��[ $ Note If Required.�� .  $��25��$
 Wrap=�$	��260�� . $ OnFocus=��� -&   $ btntxtNtCross��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $��15��$y�Hidden=��� -'   $ btnAddRecord��#��[ $
��_Add��$	��Top=�$��22��$��75���� $ 
Horizontal=�.+��� -(   $ lblListBox1Title��$��_List��$��80�� �� $ 	ListView1���� $ 
Horizontal=�$	��Top=�.+��� -)    ��Name$ 	ListView1��$	��450��$	��300�� �� View �� .P���  �� Width$ 90��$
��Code�� �� DisplayMemberBinding ��  �� Path$ trcode��� $	 260��$��Description�� �� $ trdesc��� $ 90��$
��Unit�� �� $ trunit��� $ 
Horizontal=�$��1��$
��Auto��$	��Top=�
�������$��10,0,0,0q�.+��� -*   $ ListView2qt��$	��471��$	��720�� �� .P��� $ 50��$	��Sl#�� �� CellTemplate 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=����� *trsl+�� $	 320�� �� $ trdesc�  �� HeaderTemplate    .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description of items��$	��320���� $ 70��$	��Qty��   .R�� $��Right=�$	��Top=�$��70��.+��� ! ��Name$! txtBlocLvkQty��$��Right=�$	��Top=����� *quotquantity+" �� StringFormat$" {0: #,##0.00}���� $ 40��$
��Unit�� �� $ trunit��� $ 70��$
��Rate��   .R�� $��Right=�$	��Top=�$��70��.+��� $! txtBlocLvkRate��$��Right=�$	��Top=����� *
quotrate+$" {0: #,##0.00}���� $	 100��$��Amount��   .R�� $��Right=�$	��Top=�$	��100��.+��� $! txtBlocLvkAmt��$��Right=�$	��Top=����� *
quotamount+$" {0: #,##0.00}���� $ 80��   .R�� $��Right=�$��Center=�$��70��.+��� $ 
Horizontal=�.+��� �� # ��Name$# hlDeleteRow��-+   $ � ToolTip$$ Delete record��
% � Tag% �� $
 trsl�.�.��� $��..\Images\bin1.png��$��16��$��16��$
��Fill=��� $��10��$y�Hidden=��� �� $# 	hlEditRow��-,   $$ Edit record��% �� $
 trsl�.�.��� $��..\Images\edit.png��$��18��$��20��$
��Fill=�� --   $ gridTransList��$��2��$	��Top=�
�������$y�Hidden=�$��	10,0,10,0q�.+��� $	��Top=�.+��� $ 
Horizontal=�$
��Left=�.+��� $!��Existing Tranasctions List:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� $��10��$y�Hidden=��� $>��8Transaction Date Range From: 01-Dec-2015 To: 15-Dec-2015��& �� TextAlignment$& Center=�$	��800���� -.   $ lvTransList��$	��350��$��14(�$
��Auto=�$��Hidden=����� ' �� 
TargetType"' ��.��� ( �� Property( � ��) �� Value) �	#FFE4F2E1 �� .P��� $ 60��$	��Sl#��   .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=�$
��Bold"����� *slnum+�� $�.���� $	 100��$
��Date�� �� $ 	memoDate1��� $	 140��$��Memo No.��$
��Bold"� �� $ memonum1��� $	 250��    .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Supplier Name��$	��220��   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *
ssirname+�� $	 250��    .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��
Store Name��$	��250��   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *
sectname+�� $	 250��    .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��	Reference��$	��220��   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *	Referance+* � ToolTip* �� *
Naration+�� $	 100��$��Status�� �� $ 
MemoStatus��� $��5��]� .+��� $ 
Horizontal=�$
��Left=�$
��Left=�.+��� $��	Recurring��#��[ $��80��$��22���� $��10��$y�Hidden=��� -/   $ 	chkFilter��+ d� 	IsChecked$+ FalseY�$��Filter Records��$��Center=�$��Center=�� , � AllowsTransparency, . $��Slide=����� $ 	chkFilter��$ 	IsChecked�- � PlacementTarget- �� $ 	chkFilter��.���� $	��180��$	��146������ . �� EndPoint$. 0.5,1&�/ �� 
StartPoint$/ 0.5,0&�.��� 0 � Color$0 Black��1 � Offset$1 1Q�� $0 	#FFF1EFC9��$1 0.206Q�� $0 	#FF7D8575��$1 0.041Q�� $0 	#FFC4E2EB��$1 0.254Q�� $0 	#FFA5CF91��$1 0.911Q�z��� -0   theTransform1��.+��� $ Vertical=�.+��� $��Transaction Date Range��$��12(�$��Italic$�$	��180��$
��Left=��� $ 
Horizontal=�.+��� $��From:��$��12(�$��Italic$�$��45��$��Right=�  -1   $ xctk_dtpFromDate��#��[ $��20��$	��130���� $ 
Horizontal=�.+��� $	��To:��$��Italic$�$��45��$��Right=�  -2   $ xctk_dtpToDate��#��[ $��20��$	��130���� -3   $ 	btnFilter��$��	Go Filter��$��20��$��60��$��12(�$��Verdana)�#��[ 2 �� Left$2 60��
3 �� Top$	3 116���� $��30��$y�Hidden=��� $ 
Horizontal=�$��Right=�$��Right=�.+��� $��10��$y�Hidden=��� $
��Edit��#��[ $��80��$��22���� $��10��$y�Hidden=��� $��Delete��#��[ $��80��$��22���� $��10��$y�Hidden=��� -4   $ 	chkPrint2��$+ FalseY�$��Print Memo/List��$��Center=�$��Center=�� , . $��Slide=����� $ 	chkPrint2��$ 	IsChecked�- �� $ 	chkPrint2��.���� $	��134��$	��146������ $. 0.5,1&�$/ 0.5,0&�.��� $0 Black��$1 1Q�� $0 	#FFF1EFC9��$1 0.206Q�� $0 	#FF7D8575��$1 0.041Q�� $0 	#FFC4E2EB��$1 0.254Q�� $0 	#FFA5CF91��$1 0.911Q�z��� -5   theTransform2��.+��� $ Vertical=�$	��134��.+��� $��Print Options��$��12(�$��Italic$�$
��Bold"�
���**��$
��Left=��� $ Vertical=�$��10,0,0,0q�.+�	� -6   4 	�Name$4 rb3SelectedMemo��$��Selected Memo��$
+ TrueY��� $y�Hidden=�	� -7   $4 rb3TableRecoreds��$��Table Records���� �� $ Vertical=�$��10,2,0,0q�.+�	� -8   $4 rb3QuickPrint��$��Quick Print���� $y�Hidden=�	� -9   $4 rb3PrintPreview��$��Print Preview��$
+ TrueY��� -:   $ 	btnPrint3��$��Go Print��$��20��$��60��$��12(�$��Verdana)�#��[ $2 34��$	3 116���� $��20��$y�Hidden=�
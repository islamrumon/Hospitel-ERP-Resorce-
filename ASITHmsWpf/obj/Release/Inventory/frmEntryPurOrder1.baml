   M S B A M L   `   `   `  ���� E  AASITHmsWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ASITHmsWpf.UserControlsASITHmsWpf.UserControls  ,    &ASITHmsWpf.Inventory.frmEntryPurOrder1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .UC%clr-namespace:ASITHmsWpf.UserControls  :d2http://schemas.microsoft.com/expression/blend/2008  -   
  � Tag$-  'Item Purchase Order - Entry/Edit Screen��   IOUserControl#��[  .���  � RowDefinitions �� $	��190 ��� $
��Auto ��� $
��Auto �.+�� -    �Name$ gridMasterpr��$��0��$	��Top=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��245* ��� $
��174* ��� $
��181* �.+��� $��0��$��0��$
��Left=�.+��� $��Order Tranasction:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� -    ��Name$ stkIntro�� �� Orientation$ Vertical=�$��0��$��2��$��1��$��3��$	��Top=�$
��Left=�$��	0,3,3.6,0q�.+��� $ 
Horizontal=�.+��� -    ��Name$ lblSectCodTitlesr��$��S_tore :��$��Right=�$	��100�� �� Target ��  �� ElementName$ 
cmbSectCod���� -   	 ��Name$	 
cmbSectCod��$	��300��
 �� 
IsReadOnly
 .  �� SelectedIndex$ 0���� -   $ lblssirCodTitlesup��$��S_upplier :��$��Right=�$	��110�� �� $ 
AtxSsircod��1   +ASITHmsWpf.UserControls.AutoCompleteTextBox  -     Name$ 
AtxSsircod��   
SearchType$ C��$
��Left=�$��22��$	��300���� $ 
Horizontal=�.+��� -   $ lblrecevbyIdOTitle��$��Order Issue _By :��$��Right=�$	��100�� �� $ 
AtxordbyId��  -	   $ 
AtxordbyId��$ C��$
��Left=�$��22��$	��300���� -
   $ lblOrdRefTitle��$��_Reference (if any) :��$��Right=�$	��110�� �� $ 	txtordRef���  �� .+�(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ 	txtordRef��$��0�� 
 wtxtBx#��[    	Watermark$ Type Referance��$��5,0,0,0q� �� TextWrapping$
 Wrap=�$�� ��$
��Left=�$��22��$	��300���� -    ��Name$ btnRefer��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�$
��Left=�.+��� -   $ lblordNarTitle��$��Narrat_ion(if any):��$	��Top=�$��Right=�$��25��$	��100�� �� $ 	txtordNar���� -    ��Name$ 	txtordNar��$�� ��$
��Left=�$��40�� J� 	AllowDrop .  �� AcceptsReturn . $	��710��$
 Wrap=��� $ 
Horizontal=�$
��Left=�.+��� -   $ lblordsubTitle��$��	Sub_ject:��$	��Top=�$��Right=�$��25��$	��100�� �� $ txtordsubject���� -   $ txtordsubject��$�� ��$
��Left=�$��25�� .  . $	��710���� $ 
Horizontal=�.+��� -   $ lblordleterTitle��$��
_Dear Sir:��$	��Top=�$��Right=�$��25��$	��100�� �� $ txtordleterdes���� -   $ txtordleterdes��$�� ��$
��Left=�$��50�� .  . $	��710��$
 Wrap=��� $ Vertical=�$��2��$��1��$	��Top=�$��Right=�$	��103��.+��� $ 
Horizontal=�.+��� -    ��Name$ chkDateBlocked��$��Order _Date : ��|�.  $
��Left=�$��Center=�$	��115��&   Xceed.Wpf.Toolkit.DateTimePicker  -     Name$ xctk_dtpordDat��  StdDateTimePickerStyle01#��[ $
��Bold"�$��22��$	��130��{�.  �� $��20��$y�Hidden=��� -   $ btnOk��  StdBtnStyle01#��[ $	��_Ok��$��75���� $ 
Horizontal=�.+��� -   $ lblordNoTitle��$��Order Form No:��$��Right=�$	��115���� -   $ lblordNo��$��PORMM-XXXXX��$  PORYYYYMMCCCCXXXXX��$	��130��$
��Bold"�$
��Left=��� $ 
Horizontal=�$��Right=�$��0, -8, 0, 0q�.+��� -   $ chkAutoTransListpr��$��_Show transactions list��|�.  $��Bottom=��� $��40��$��22��$y�Hidden=��� -   $ 	btnPrint2��$��_Print��$��70��$��22��$��Bottom=�$��12(�$��Verdana)�#��[ �� $��20��$y�Hidden=��� -   $ btnUpdateor��#��[ $��_Update��$��75���� $��1��$	��Top=�
��� ���$��5��� -   $ gridDetails��$��2��$	��Top=�$��	10,0,10,0q� �� $
��Auto ��� $
��Auto �.+��� -   $ stkItem��$ Vertical=�$��0��$	��Top=�$��Stretch=�.+��� $ 
Horizontal=�.+��� -   $ 	lblordAno��$	��100��$��Right=�$��Appro_ve no :�� �� $ 	cmbordAno���� -   $	 	cmbordAno��$	��350��$��0,0,0,0q�
 . $ 0���� $ 
Horizontal=�.+��� -   $ lblUID��$��Item UI_D Mark:��$	��100��$��Right=� �� $ txtUID��  -    $ txtUID��#��[ $ Manufacturer's Product ID��$��5,0,0,0q�$��22��$	��150��)  #Xceed.Wpf.Toolkit.AutoSelectTextBox   AutoMoveFocus .    AutoSelectBehavior$ OnFocus=��� -!   $ lblRSirCode��$��
Item Code:��$��70��$��Right=� �� $ txtRSirCode��  -"   $ txtRSirCode��#��[ $ 	Item Code��$��5,0,0,0q�$�� ��$
��Left=�$��22��$	��130�� �� 
IsReadOnly . �� $ 
Horizontal=�.+��� -#   $ lblRSirCodeTitle��$��Descr_iption:��$��Right=�$	��100�� �� $ txtRSirDesc���  �� .+�  -$   $ txtRSirDesc��$��0��#��[ $ Item Description��$��5,0,0,0q�$��22��$
 Wrap=�$	��350�� . $ OnFocus=��� -%   $ btntxtRSirDesc��$��0��$��20��$��20��$��Right=�$��x��|�.  $y�	Collapsed=��� $ 
Horizontal=�$
��Left=�.+��� $ 
Horizontal=�$	��170��.+��� -&   $ lblordQtyTitle��$��_Quantity :��$
��Left=�$��60�� �� $ 	txtordQty��  -'   $ 	txtordQty�� �� TextAlignment$ Right=�$��0��$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $ Type quent.��$�� ��$
��Left=�$��22��$��70���� -(   $ lblUnitordq1��$�� Unit��$��80���� $ 
Horizontal=�$	��280��.+��� -)   $ lblRateTitle��$��Rat_e :��$
��Left=�$��48�� �� $ txtrate��  -*   $ txtrate��$ Right=�$��0��$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $ 
Type Rate.��$�� ��$
��Left=�$��22��$��70���� -+   $ 	lblAmtCal��$��Amount :���� -,   $ lblAmountCal��$�� ��$	��100���� $ 
Horizontal=�$
��Left=�$��1��$��0��.+��� --   $ lbldisamtTitle��$��_Dis. Amt :��$
��Left=�$��61�� �� $ 	txtdisamt��  -.   $ 	txtdisamt��$ Right=�$��0��$
 Wrap=� . $ OnFocus=�$��5,0,0,0q�#��[ $ Type quent.��$�� ��$
��Left=�$��22��$��70���� -/   $ lbldisamtUnit2��$	�� TK��$��40���� -0   $ lblnetAmtTitle��$��Net Amount :��$��Right=��� -1   $ lblnetAmount��$	��110��
���  ��$
��Bold"��� -2   $ btnAddRecordor��#��[ $
��_Add��$	��Top=�$��Right=�$��22��$��75���� $ 
Horizontal=�.+��� -3   $ lblListBox1Title��$��_List :��$��80�� �� $ 	ListView1���� $ 
Horizontal=�$	��Top=�.+��� -4    ��Name$ 	ListView1��$	��450��$	��225�� �� View �� .P���  �� Width$	 100��$
��Code�� �� DisplayMemberBinding ��  �� Path$ trcode��� $	 320��$��Description�� �� $ trdesc��� $ Vertical=�$��1��$
��Auto��$	��Top=�
�������$��10,0,0,0q�.+��� -5   $ 	ListView2��$	��190��$	��721��$��14(�$
��Auto=�$��Hidden=�����   �� 
TargetType"  ��.��� ! �� Property! � ��" �� Value" �	#FFE4F2E1 �� .P��� $ 50��$	��Sl#��# �� CellTemplate# 5  -System.Windows.Controls.ItemContainerTemplate�  .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=����� *trsl+�� $	 320�� �� $ trdesc�$ �� HeaderTemplate$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Description of items��$	��320���� $ 80��$	��Qty��#   .R�� $��Right=�$	��Top=�$��80��.+��� % ��Name$% txtBlocLvkreqQty��$��Right=�$	��Top=�$
��Bold"����� *
orderqty+& �� StringFormat$& {0: #,##0.00}���� $ 40��$
��Unit�� �� $ trunit��� $ 65��$��	Dis. Amt.�� �� $ disamt�$& {0: #,##0.00}��#   .R�� $��Right=�$	��Top=�$��65��.+��� $% txtBlocLvkSrfQty��$��Right=�$	��Top=�$
��Bold"��� $ 90��$��
Net Amount�� �� $ netamt�$& {0: #,##0.00}��#   .R�� $��Right=�$	��Top=�$��90��.+��� $% txtBlocLvkSrfQty��$��Right=�$	��Top=�$
��Bold"��� $ 80��#   .R�� $��Right=�$��Center=�$��70��.+��� $ 
Horizontal=�.+��� �� ' ��Name$' hlDeleteRow��-6   ( � ToolTip$( Delete record��
) � Tag) �� $
 trsl�.�.��� $��..\Images\bin1.png��$��16��$��16��$
��Fill=��� $��10��$y�Hidden=��� �� $' 	hlEditRow��-7   $( Edit record��) �� $
 trsl�.�.��� $��..\Images\edit.png��$��18��$��20��$
��Fill=��� $��Terms and Condition : ��&   System.Windows.Controls.DataGrid  -8   *  Name$
* dtGr��$	��165��
�������$	��721��+   CanUserSortColumns+ .  ���� � -9   $ gridTransList��$��2��$	��Top=�$y�Hidden=�
�������$��	10,0,10,0q�.+��� $ Vertical=�$	��Top=�$	��380��.+��� $ 
Horizontal=�$
��Left=�.+��� $!��Existing Tranasctions List:��$��14(�$��Italic$�$
��Bold"�
����� �$	��180���� $��10��$y�Hidden=��� $>��8Transaction Date Range From: 01-Dec-2015 To: 15-Dec-2015��, �� TextAlignment$, Center=�$	��800���� -:   $ lvTransList��$	��330��$��14(�$
��Auto=�$��Hidden=����� "  ��.��� ! � ��" �	#FFE4F2E1 �� .P��� $ 60��$	��Sl#��#   .R�� $��Right=�$	��Top=�$��50��.+��� $��Right=�$	��Top=�$
��Bold"����� *slnum+�� $�.���� $	 100��$
��Date�� �� $ 	memoDate1��� $	 140��$��Memo No.��$
��Bold"� �� $ memonum1��� $	 250�� �� $ ssirName�$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��Supplier Name��$	��220���� $	 250��$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��
Store Name��$	��220��#   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *
sectName+- � ToolTip- �� *PreparByName+�� $	 250��$   .R�� $
��Left=�$	��Top=�.+��� $
��Left=�$	��Top=�$��	Reference��$	��220��#   .R�� $
��Left=�$	��Top=�$	��240��.+��� $
��Left=�$	��Top=����� *	Referance+- �� *
Naration+�� $	 100��$��Status�� �� $ 
MemoStatus��� $��5��]� .+��� $ 
Horizontal=�$
��Left=�$
��Left=�.+��� $��	Recurring��#��[ $��80��$��22���� $��10��$y�Hidden=��� -;   $ 	chkFilter��. d� 	IsChecked$. FalseY�$��Filter Records��$��Center=�$��Center=�� / � AllowsTransparency/ . $��Slide=����� $ 	chkFilter��$ 	IsChecked�0 � PlacementTarget0 �� $ 	chkFilter��.���� $	��180��$	��146������ 1 �� EndPoint$1 0.5,1&�2 �� 
StartPoint$2 0.5,0&�.��� 3 � Color$3 Black��4 � Offset$4 1Q�� $3 	#FFF1EFC9��$4 0.206Q�� $3 	#FF7D8575��$4 0.041Q�� $3 	#FFC4E2EB��$4 0.254Q�� $3 	#FFA5CF91��$4 0.911Q�z��� -<   theTransform1��.+��� $ Vertical=�.+��� $��Transaction Date Range��$��12(�$��Italic$�$	��180��$
��Left=��� $ 
Horizontal=�.+��� $��From:��$��12(�$��Italic$�$��45��$��Right=�  -=   $ xctk_dtpFromDate��#��[ $��20��$	��130���� $ 
Horizontal=�.+��� $	��To:��$��Italic$�$��45��$��Right=�  ->   $ xctk_dtpToDate��#��[ $��20��$	��130���� -?   $ 	btnFilter��$��	Go Filter��$��20��$��60��$��12(�$��Verdana)�#��[ 5 �� Left$5 60��
6 �� Top$	6 116���� $��30��$y�Hidden=��� $ 
Horizontal=�$��Right=�$��Right=�.+��� $��10��$y�Hidden=��� $
��Edit��#��[ $��80��$��22���� $��10��$y�Hidden=��� $��Delete��#��[ $��80��$��22���� $��10��$y�Hidden=��� -@   $ 	chkPrint2��$. FalseY�$��Print Memo/List��$��Center=�$��Center=�� / . $��Slide=����� $ 	chkPrint2��$ 	IsChecked�0 �� $ 	chkPrint2��.���� $	��134��$	��146������ $1 0.5,1&�$2 0.5,0&�.��� $3 Black��$4 1Q�� $3 	#FFF1EFC9��$4 0.206Q�� $3 	#FF7D8575��$4 0.041Q�� $3 	#FFC4E2EB��$4 0.254Q�� $3 	#FFA5CF91��$4 0.911Q�z��� -A   theTransform2��.+��� $ Vertical=�$	��134��.+��� $��Print Options��$��12(�$��Italic$�$
��Bold"�
���**��$
��Left=��� $ Vertical=�$��10,0,0,0q�.+�	� -B   7 	�Name$7 rb3SelectedMemo��$��Selected Memo��$
. TrueY��� $y�Hidden=�	� -C   $7 rb3TableRecoreds��$��Table Records���� �� $ Vertical=�$��10,2,0,0q�.+�	� -D   $7 rb3QuickPrint��$��Quick Print���� $y�Hidden=�	� -E   $7 rb3PrintPreview��$��Print Preview��$
. TrueY��� -F   $ 	btnPrint3��$��Go Print��$��20��$��60��$��12(�$��Verdana)�#��[ $5 34��$	6 116���� $��20��$y�Hidden=�